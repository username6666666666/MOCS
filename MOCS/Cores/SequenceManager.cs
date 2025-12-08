using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Cores
{
    public enum PacketCategory
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
    }

    public class SequenceManager<T>
        where T : IBinaryInteger<T>
    {
        private readonly Dictionary<PacketCategory, T> _sequences = [];
        private readonly T _maxValue;

        public SequenceManager(T maxValue)
        {
            if (maxValue <= T.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }
            _maxValue = maxValue;
        }

        //public T GetCurrentSequence(PacketCategory category)
        //{
        //    return _sequences.GetValueOrDefault(category, T.Zero);
        //}

        public T GetNextSequenceNum(PacketCategory category)
        {
            T current = _sequences.GetValueOrDefault(category, T.Zero);
            T next = current + T.One;

            if (next > _maxValue)
            {
                next = T.Zero;
            }

            _sequences[category] = next;
            return current;
        }
    }
}
