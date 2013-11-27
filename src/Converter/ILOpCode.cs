using IKVM.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public struct ILOpCode
    {
        private object opcode; // force box
        public OpCode OpCode
        {
            get
            {
                return (OpCode) opcode;
            }
            set
            {
                this.opcode = value;
            }
        }

        public long Value { get; set; }
        public double FValue { get; set; }


        static readonly OpCode[] opCodeTypes = GetOpCodes();


        // TYPE
        static OpCode[] GetOpCodes()
        {
            OpCode[] OpCodes = new OpCode[768];
            foreach (System.Reflection.FieldInfo field in typeof(OpCodes).GetFields())
            {
                OpCode opc = (OpCode)field.GetValue(null);
                OpCodes[opc.Value + 512] = opc;
            }
            return OpCodes;
        }

        //        public static ILOpCode GetOpCode( byte code )
        //        {
        //                    return GetOpCode( (short) code); 
        //}


        public static ILOpCode GetOpCode(short codeValue)
        {

            return new ILOpCode() { OpCode = opCodeTypes[codeValue + 512] };

        }

        //short OpCodeValue = code[pos++];
        //    if (OpCodeValue == 0xFE)
        //        OpCodeValue = (short)(0xFE00 + code[pos++]);
        //    OpCode OpCode = opCodeTypes[OpCodeValue + 512];


        public static ILOpCode GetOpCode(short code, long value)
        {
            var ret = GetOpCode(code);
            ret.Value = value;
            return ret;

        }



        internal void Accept(IVisitor aVisitor)
        {
            throw new NotImplementedException();
        }
    }

}
