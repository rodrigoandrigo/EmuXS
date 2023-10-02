using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iced.Intel;
using static Iced.Intel.AssemblerRegisters;
using Xunit;

namespace EmuXS.X86
{
    internal class CPU
    {
        
    }
    sealed class UsedMemoryEqualityComparer : IEqualityComparer<UsedMemory>
    {
        public static readonly UsedMemoryEqualityComparer Instance = new UsedMemoryEqualityComparer();
        UsedMemoryEqualityComparer() { }

        public bool Equals(UsedMemory x, UsedMemory y) =>
            x.Segment == y.Segment &&
            x.Base == y.Base &&
            x.Index == y.Index &&
            x.Scale == y.Scale &&
            x.Displacement == y.Displacement &&
            x.MemorySize == y.MemorySize &&
            x.Access == y.Access &&
            x.AddressSize == y.AddressSize &&
            x.VsibSize == y.VsibSize;

        public int GetHashCode(UsedMemory obj)
        {
            int hc = 0;
            hc ^= (int)obj.Segment;
            hc ^= (int)obj.Base << 8;
            hc ^= (int)obj.Index << 16;
            hc ^= obj.Scale << 28;
            hc ^= obj.Displacement.GetHashCode();
            hc ^= (int)obj.MemorySize << 12;
            hc ^= (int)obj.Access << 24;
            hc ^= (int)obj.AddressSize << 3;
            hc ^= (int)obj.VsibSize << 11;
            return hc;
        }

    }
}
