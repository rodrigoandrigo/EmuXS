using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iced.Intel;
using static Iced.Intel.AssemblerRegisters;

namespace EmuXS.X86
{
    internal class CPU
    {
        //
        public void CPU64()
        {
            byte[] bytes = new byte[] { 0x48, 0x8B, 0xC8 }; // mov rcx, rax
            var codeReader = new ByteArrayCodeReader(bytes);
            var decoder = Iced.Intel.Decoder.Create(64, codeReader);
            var instruction = decoder.Decode();

            Console.WriteLine(instruction.Mnemonic); // Outputs: Mov
            Console.WriteLine(instruction.Op0Kind);  // Outputs: Register
            Console.WriteLine(instruction.Op1Kind);  // Outputs: Register
            Console.WriteLine(instruction.Op0Register); // Outputs: RCX
            Console.WriteLine(instruction.Op1Register); // Outputs: RAX
        }

    }

}
