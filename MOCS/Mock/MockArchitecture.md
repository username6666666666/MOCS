# MOCS Mock 模块架构设计

## 一、目标

在不需要真实硬件设备（MCU、VCU、EMS、VSPS、OBC）的情况下，通过软件模拟对端设备发送 UDP 报文，验证 MOCS 系统的：
- 报文接收/解析链路是否正常
- UI 界面是否根据数据正确更新
- 状态机流转是否正确
- 发送报文是否符合协议规范（通过抓包验证）

---

## 二、Mock 模块在整个系统中的位置

```
┌──────────────────────────────────────────────────────────────────────┐
│                          Forms 层 (UI)                                │
│   MCU_UI / OBC_UI / VSPS_UI / LCU_UI / MOCS_UI                      │
└──────────────────────────────┬───────────────────────────────────────┘
                               │ INotifyPropertyChanged
┌──────────────────────────────▼───────────────────────────────────────┐
│                        Cores 层 (业务逻辑)                            │
│   MCUInterface  ←─状态机─→  VCUInterface                             │
│   监听 6002 端口              监听 6001 端口                           │
└─────────────────┬─────────────────────────┬───────────────────────────┘
                  │                         │
       ┌──────────▼──────────┐    ┌─────────▼───────────┐
       │  UdpMessageService   │    │  UdpMessageService   │
       │  <BaseMessage>       │    │  <BaseMessage>       │
       │  port: 6002          │    │  port: 6001          │
       └──────────┬───────────┘    └─────────┬───────────┘
                  │                          │
                  │     UDP 网络层            │
                  │                          │
       ┌──────────▼──────────────────────────▼───────────┐
       │                                                  │
       │             ★ Mock 模块 (新增) ★                  │
       │                                                  │
       │  ┌────────────────┐  ┌──────────────────────┐   │
       │  │ MockMCUSender   │  │  MockVCUSender       │   │
       │  │ → 目标端口 6002  │  │  → 目标端口 6001      │   │
       │  │ 模拟MCU发送报文  │  │  模拟VCU发送报文      │   │
       │  └────────────────┘  └──────────────────────┘   │
       │                                                  │
       │  ┌──────────────────────────────────────────┐   │
       │  │        MockDataGenerator                 │   │
       │  │  生成各类报文的模拟 UserData              │   │
       │  └──────────────────────────────────────────┘   │
       │                                                  │
       │  ┌──────────────────────────────────────────┐   │
       │  │        MockScenarioPlayer                 │   │
       │  │  按时间线/脚本播放一组报文序列              │   │
       │  └──────────────────────────────────────────┘   │
       │                                                  │
       │  ┌──────────────────────────────────────────┐   │
       │  │        MockControlPanel (Form)            │   │
       │  │  用户操作界面：手动/自动发送，查看结果      │   │
       │  └──────────────────────────────────────────┘   │
       └──────────────────────────────────────────────────┘
```

---

## 三、模块划分

### 3.1 命名空间结构

```
MOCS.Mock/
├── MockDataGenerator.cs       # 报文数据生成器（核心）
├── MockSender.cs              # 报文发送器（封装 UdpClient）
├── MockScenarioPlayer.cs      # 场景播放器（批量/定时发送）
├── MockControlPanel.cs        # Mock 控制面板 UI
├── MockControlPanel.Designer.cs
└── MockScenarios/             # 预定义场景脚本
    ├── MCU_NormalOperation.cs
    ├── MCU_FaultInjection.cs
    └── VCU_FullStatus.cs
```

### 3.2 复用现有组件

Mock 模块**不修改**现有代码，只**复用**以下现有组件：

| 现有组件 | Mock 中的用途 |
|----------|--------------|
| `MessageFactory<BaseMessage>` | 序列化报文为传输字节（加帧头帧尾CRC） |
| `BaseSendMsg / BaseMessage` | 报文对象模型 |
| `MCUStatusMsg / MCUReplyMsg` | 入站报文类型定义 |
| `EMSStatusMsgA/B / VSPSStatusMsg / OBCMsg` | 入站报文类型定义 |
| `MOCSStatus.ToByteArray()` 等 | 参考出站报文格式，验证收发一致性 |
| `SequenceManager<ushort>` | 生成模拟序列号 |

---

## 四、各类详细设计

### 4.1 MockDataGenerator — 报文数据生成器

**职责**：根据报文类型，生成符合协议规范的 `UserData` 字节数组。

**设计要点**：
- 每种入站报文对应一个生成方法
- 参数化：可指定正常值/边界值/异常值
- 返回 `ReadOnlyMemory<byte>`，可直接赋值给 `BaseMessage.UserData`

```csharp
// MockDataGenerator.cs (伪代码示意)
public class MockDataGenerator
{
    // ===== MCU 入站报文 =====
    
    /// 生成 MCU 状态报文的 UserData (164 bytes)
    public static byte[] GenerateMCUStatusUserData(
        ChannelRecvStatusEnum ch1Status = ChannelRecvStatusEnum.Normal,
        ChannelRecvStatusEnum ch2Status = ChannelRecvStatusEnum.Normal,
        int currentPos = 0,
        short velocity = 0,
        short acc = 0,
        byte dcsNum = 1,
        // ... 所有字段
    )
    
    /// 生成 MCU 应答报文的 UserData (12 bytes)
    public static byte[] GenerateMCUReplyUserData(
        MCUReplyErrorIdentifierEnum errorId = MCUReplyErrorIdentifierEnum.NoError,
        // ...
    )
    
    // ===== VCU 入站报文 =====
    
    /// 生成 EMS A 类状态 UserData (9 bytes: CANID + 8 data)
    public static byte[] GenerateEMSStatusAUserData(
        byte canId,      // 0x21~0x34 LCU, 0x41~0x46 GCU
        byte life = 0,
        EMSCmdStatusEnum cmd = EMSCmdStatusEnum.DeEnergize,
        float gap = 0,       // 0~20mm
        float voltage = 0,   // 0~530V
        float current = 0,   // 0~160A
        float acc = 0,       // -50~50 m/s²
        // ...
    )
    
    /// 生成 EMS B 类状态 UserData
    public static byte[] GenerateEMSStatusBUserData(byte canId, ...)
    
    /// 生成 VSPS 状态 UserData (12 bytes)
    public static byte[] GenerateVSPSStatusUserData(
        ushort life = 0,
        bool forward = true,
        ushort relativePos = 0,
        ushort speed = 0
    )
    
    /// 生成 OBC 状态 UserData (16 bytes)
    public static byte[] GenerateOBCStatusUserData(
        bool isLeviated = false,
        bool isGuideEnabled = false,
        // ...
    )
    
    // ===== 便捷方法：随机/边界值 =====
    
    /// 生成一组合法的随机 MCU 状态数据
    public static byte[] GenerateRandomMCUStatusData() { ... }
    
    /// 生成边界值数据（最大/最小值）
    public static byte[] GenerateBoundaryMCUStatusData() { ... }
}
```

**关键：UserData 字节布局必须与现有解析逻辑严格一致。**

以 `MCUStatusMsg`（164 bytes）为例，布局为：

```
Offset  Size  字段
0       1     Channel1RecvStatus (低6位)
1       1     (保留)
2       1     Channel2RecvStatus (低6位)
3       1     (保留)
4       1     MCUSendReason
5       1     MsgNumToRepeat
6       2     A_SequenceNumRefPoint (LittleEndian)
8       1     MCUStatusChangeReadinessInfo (低3位)
9       1     CurrentMaglevVehicleIdentifier
10      1     ClearCurrentMaglevVehicleReadinessInfo (低2位)
11      1     DCSNum
12      4     CurrentMaglevVehiclePos (LittleEndian, int32)
16      2     CurrentMaglevVehicleVelocity (LittleEndian, int16)
18      2     CurrentMaglevVehicleAcc (LittleEndian, int16)
20      1     TractionCapacityForCurrentVehicle
21      1     CurrentMaglevVehicleSPRStatus (低4位)
22      1     VirtualMaglevVehicleIdentifier
23      1     ClearVirtualMaglevVehicleReadinessInfo (低2位)
24      1     TractionCapacityForVirtualVehicle
25      1     MCUFaultStatus
26      1     ParkingPointsNum
27      5     当前悬浮架5个停车点标识
32      5     虚拟悬浮架5个停车点标识
37~163  ...   (剩余保留字节)
```

---

### 4.2 MockSender — 报文发送器

**职责**：将构造好的报文对象通过 UDP 发送到 MOCS 的监听端口。

**设计要点**：
- 封装 `UdpClient` 和 `MessageFactory<BaseMessage>`
- 直接发送到 MOCS 的 localhost 监听端口
- 可指定发送的目标（MCU 端口 6002 或 VCU 端口 6001）
- 提供同步和异步发送接口
- 发送后记录日志

```csharp
// MockSender.cs (伪代码示意)
public class MockSender : IDisposable
{
    private readonly UdpClient _udpClient;
    private readonly MessageFactory<BaseMessage> _messageFactory;
    private readonly IPEndPoint _targetEndPoint;
    
    /// <param name="targetPort">MOCS监听端口: 6001(VCU) 或 6002(MCU)</param>
    public MockSender(int targetPort, ILogger? logger = null)
    {
        _udpClient = new UdpClient();
        _messageFactory = new MessageFactory<BaseMessage>();
        _targetEndPoint = new IPEndPoint(IPAddress.Loopback, targetPort);
    }
    
    /// 发送一条报文
    public async Task SendAsync<T>(T message) where T : BaseMessage, IOutgoingMsg
    {
        var payload = message.ToByteArray();          // 报文主体序列化
        var bytes = _messageFactory.ToTransmitByteArray(payload);  // 加帧头帧尾CRC
        await _udpClient.SendAsync(bytes, bytes.Length, _targetEndPoint);
        // 记录日志...
    }
    
    /// 快速发送（自动构建 BaseMessage）
    public async Task SendRawAsync(byte msgId, ReadOnlyMemory<byte> userData)
    {
        var msg = new BaseSendMsg
        {
            MsgId = msgId,
            SequenceNumber = _sequenceManager.GetNextSequenceNum(...),
            UserData = userData,
        };
        await SendAsync(msg);
    }
}
```

---

### 4.3 MockScenarioPlayer — 场景播放器

**职责**：按预设时间线或脚本，自动连续发送一系列报文，模拟真实设备的行为。

**设计要点**：
- 支持定义报文序列（时间偏移 + 报文内容）
- 支持循环播放
- 支持随机抖动（模拟真实网络延迟）
- 支持暂停/继续/停止
- 场景可序列化保存/加载

```csharp
// MockScenarioPlayer.cs (伪代码示意)
public class MockScenarioPlayer
{
    /// 场景条目
    public record ScenarioStep(
        TimeSpan Delay,           // 从上一步到这一步的间隔
        int TargetPort,           // 6001 或 6002
        byte MsgId,               // 报文标识
        byte[] UserData           // 报文数据
    );
    
    private readonly MockSender _mcuSender;  // → 6002
    private readonly MockSender _vcuSender;  // → 6001
    private CancellationTokenSource? _cts;
    
    /// 加载场景
    public void LoadScenario(IEnumerable<ScenarioStep> steps) { ... }
    
    /// 播放场景
    public async Task PlayAsync(CancellationToken ct) { ... }
    
    /// 停止
    public void Stop() { ... }
    
    // 预置场景工厂方法
    public static MockScenarioPlayer CreateMCUStartupScenario() { ... }
    public static MockScenarioPlayer CreateMCUDataStreamScenario(int intervalMs = 100) { ... }
    public static MockScenarioPlayer CreateVCUFullScenario() { ... }
}
```

---

### 4.4 MockControlPanel — 控制面板 UI

**职责**：提供给用户的图形化操作界面。

**设计要点**：
- 左侧：MCU 报文模拟区
- 右侧：VCU 报文模拟区
- 手动模式：选择报文类型 → 填写参数 → 点击发送
- 自动模式：选择预设场景 → 播放/暂停/停止
- 底部：日志显示区（已发送报文记录）

```
┌──────────────────────────────────────────────────────────────────┐
│  Mock 控制面板                                          [×] [-][□] │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌─────── MCU 模拟区 ─────────┐  ┌─────── VCU 模拟区 ─────────┐ │
│  │                            │  │                            │ │
│  │  报文类型: [MCU状态  ▼]    │  │  报文类型: [EMS状态A ▼]    │ │
│  │                            │  │                            │ │
│  │  ┌─ 字段编辑区 ─────────┐  │  │  ┌─ 字段编辑区 ─────────┐  │ │
│  │  │ Ch1状态: [Normal  ▼] │  │  │  │ CAN ID:   [0x21    ] │  │ │
│  │  │ Ch2状态: [Normal  ▼] │  │  │  │ 间隙:     [8.5  ]mm │  │ │
│  │  │ 位置:    [1234  ]cm  │  │  │  │ 电压:     [330  ]V  │  │ │
│  │  │ 速度:    [500   ]cm/s│  │  │  │ 电流:     [80   ]A  │  │ │
│  │  │ 加速度:  [20    ]... │  │  │  │ 加速度:   [0.5  ]... │  │ │
│  │  │ ...                  │  │  │  │ ...                  │  │ │
│  │  └──────────────────────┘  │  │  └──────────────────────┘  │ │
│  │                            │  │                            │ │
│  │  [发送一次] [每100ms发送]  │  │  [发送一次] [每100ms发送]  │ │
│  │                            │  │                            │ │
│  └────────────────────────────┘  └────────────────────────────┘ │
│                                                                  │
│  ┌─────── 场景播放 ──────────────────────────────────────────┐  │
│  │  场景: [MCU启动流程     ▼]  [▶ 播放] [⏸ 暂停] [⏹ 停止]  │  │
│  │  进度: ████████░░░░░░░░ 50%                               │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                  │
│  ┌─────── 发送日志 ──────────────────────────────────────────┐  │
│  │  [19:05:01.234] MCU → 6002: MCUStatusMsg (164B) Seq=1    │  │
│  │  [19:05:01.335] MCU → 6002: MCUStatusMsg (164B) Seq=2    │  │
│  │  [19:05:02.001] VCU → 6001: EMSStatusMsgA (9B) Seq=1     │  │
│  └──────────────────────────────────────────────────────────┘  │
└──────────────────────────────────────────────────────────────────┘
```

---

## 五、数据流

```
用户操作 MockControlPanel
         │
         ├── 手动模式 ──→ 填写参数 → MockDataGenerator.GenerateXXX()
         │                                      │
         │                            ReadOnlyMemory<byte>
         │                                      │
         │                              MockSender.SendRaw()
         │                                      │
         │                          ┌───────────┴───────────┐
         │                          │  new BaseSendMsg {     │
         │                          │    MsgId = ...,        │
         │                          │    UserData = ...,     │
         │                          │  }                     │
         │                          │  → ToByteArray()       │
         │                          │  → MessageFactory      │
         │                          │    .ToTransmitByteArray│
         │                          │  → UdpClient.SendAsync │
         │                          └───────────┬───────────┘
         │                                      │
         ├── 自动模式 ──→ MockScenarioPlayer.PlayAsync()
         │                    │
         │              循环发送 ScenarioStep
         │                    │
         └────────────────────┴──── UDP → localhost:6001 / localhost:6002
                                              │
                              ┌───────────────┴───────────────┐
                              │  MOCS 系统                      │
                              │  UdpMessageService.ReceiveLoop │
                              │  → MessageFactory.TryParse     │
                              │  → MessageDispatcher.Dispatch  │
                              │  → Interface Handler 更新单例   │
                              │  → INotifyPropertyChanged      │
                              │  → UI 刷新                      │
                              └───────────────────────────────┘
```

---

## 六、与现有代码的关系

```
现有代码（不修改）                    Mock 模块（新增）
─────────────────────────────────    ─────────────────────
MOCS.Protocals.BaseMessage         ← MockSender 使用
MOCS.Protocals.BaseSendMsg         ← MockSender 使用
MOCS.Protocals.MessageFactory      ← MockSender 使用
MOCS.Coms.IOutgoingMsg             ← MockSender 使用
MOCS.Cores.SequenceManager         ← MockSender 使用
MOCS.Protocals.*.*Msg (入站类型)    ← MockDataGenerator 参考其 Parse 逻辑
MOCS.Cores.MCU.MCUStatus (单例)     ← 不直接引用，通过 UI 观察
MOCS.Cores.VCU.EMSStatus (单例)     ← 不直接引用，通过 UI 观察
MOCS.Cores.MCU.MCUInterface         ← 不引用（Mock 绕过 Interface 层）
MOCS.Cores.VCU.VCUInterface         ← 不引用（Mock 绕过 Interface 层）
```

---

## 七、实现优先级

| 优先级 | 模块 | 理由 |
|--------|------|------|
| P0 | MockSender | 基础发送能力，所有功能依赖它 |
| P0 | MockDataGenerator (MCU部分) | MCU 报文数据量大(164B)，验证价值最高 |
| P1 | MockControlPanel (手动模式) | 用户交互界面 |
| P1 | MockDataGenerator (VCU部分) | VCU 报文种类多(4种) |
| P2 | MockScenarioPlayer | 自动化测试场景 |
| P2 | MockControlPanel (自动模式) | 场景播放 UI |
| P3 | MockScenarios/ 预设场景库 | 丰富的测试用例 |

---

## 八、关键设计决策

1. **不修改现有代码**：Mock 模块完全独立，通过 UDP 与本系统通信，就像真实硬件一样。
2. **发送目标为 localhost**：默认发送到 `127.0.0.1:6001` 和 `127.0.0.1:6002`，与 MOCS 的监听端口一致。
3. **复用 MessageFactory**：保证 Mock 发送的报文帧格式与真实设备完全一致。
4. **MockDataGenerator 严格遵循现有 Parse 逻辑**：确保 UserData 字节布局与 `OnRecvXXX` 中的解析逻辑匹配。
5. **场景可扩展**：`ScenarioStep` 是纯数据对象，后续可序列化为 JSON 文件。
