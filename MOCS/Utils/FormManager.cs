using MOCS.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Utils
{
    /// <summary>
    /// 全局窗口管理工具类（所有窗口共用，实现单例打开/置顶）
    /// </summary>
    public static class FormManager
    {
        // 全局存储已打开的窗口实例（确保每个窗口仅一个实例）
        private static readonly Dictionary<string, Form> _openForms = new Dictionary<string, Form>();
        
        /// <summary>
        /// 新增：手动注册已打开的窗口实例（用于主窗口初始化）
        /// </summary>
        /// <param name="form">已打开的窗口实例</param>
        public static void RegisterOpenedForm(Form form)
        {
            string formName = form.Name; // 用窗口Name（和类名一致）
            if (!_openForms.ContainsKey(formName))
            {
                _openForms.Add(formName, form);
                // 绑定关闭事件：移除字典
                form.FormClosed += (s, args) =>
                {
                    if (_openForms.ContainsKey(formName))
                    {
                        _openForms.Remove(formName);
                    }
                };
            }
        }

        /// <summary>
        /// 打开指定窗口（单例），已打开则置顶显示
        /// </summary>
        /// <typeparam name="T">窗口类型（如MCU_UI、LCU_UI等）</typeparam>
        public static void OpenOrBringToFront<T>() where T : Form, new()
        {
            string formName = typeof(T).Name;

            // 检查窗口是否已打开
            if (_openForms.ContainsKey(formName))
            {
                Form targetForm = _openForms[formName];
                // 窗口已被关闭则移除字典，重新创建
                if (targetForm.IsDisposed)
                {
                    _openForms.Remove(formName);
                }
                else
                {
                    // 置顶显示，若最小化则还原
                    targetForm.BringToFront();
                    if (targetForm.WindowState == FormWindowState.Minimized)
                    {
                        targetForm.WindowState = FormWindowState.Normal;
                    }
                    return;
                }
            }

            // 未打开则创建新实例并显示
            T newForm = new T();
            newForm.Show();
            _openForms.Add(formName, newForm);

            // 窗口关闭时自动从字典移除
            newForm.FormClosed += (s, args) =>
            {
                if (_openForms.ContainsKey(formName))
                {
                    _openForms.Remove(formName);
                }
            };
        }

        /// <summary>
        /// 关闭所有已打开的窗口（主窗口关闭时调用）
        /// </summary>
        public static void CloseAllForms()
        {
            // 遍历关闭所有窗口（倒序避免遍历中集合变化）
            foreach (var form in new List<Form>(_openForms.Values))
            {
                if (!form.IsDisposed && form.GetType() != typeof(MOCS_UI))
                {
                    form.Close();
                }
            }
            _openForms.Clear();
        }
    }
}
