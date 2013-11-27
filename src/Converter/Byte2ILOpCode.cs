using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKVM.Reflection.Emit;

namespace Converter
{
    public class Byte2ILOpCode
    {
     
     
         

        public IEnumerable<ILOpCode> CreateIL(byte[] code) //, Type[] genericTypeArguments, Type[] genericMethodArguments)
        {
            var  result = new List<ILOpCode>(); 

            int pos = 0;
            while (pos < code.Length)
            {
                short OpCodeValue = code[pos++];
                if (OpCodeValue == 0xFE)
                    OpCodeValue = (short)(0xFE00 + code[pos++]);
                var opCode =  ILOpCode.GetOpCode(OpCodeValue);
                result.Add(opCode);

                switch (opCode.OpCode.OperandType)
                {
                    case OperandType.InlineNone:
                        break;
                    case OperandType.InlineBrTarget:
                        opCode.Value = ReadInt32(code, ref pos) + pos;
                        break;
                    case OperandType.ShortInlineBrTarget:
                        opCode.Value = (sbyte)code[pos++] + pos;
                        break;
                    case OperandType.InlineTok:
                    case OperandType.InlineString:
                    case OperandType.InlineType:
                    case OperandType.InlineMethod:
                    case OperandType.InlineSwitch:
                    case OperandType.InlineI:
                    case OperandType.InlineField:
                    case OperandType.InlineSig:
                            int token = ReadInt32(code, ref pos);
                            opCode.Value = token ;

                            //MethodBase methodOrConstructor = ResolveMethod(token, genericTypeArguments, genericMethodArguments);
                            //if ((methodOrConstructor.CallingConvention & CallingConventions.Any) == CallingConventions.VarArgs)
                            //{
                            //    CustomModifiers[] customModifiers;
                            //    Type[] optionalParameterTypes = ResolveOptionalParameterTypes(token, genericTypeArguments, genericMethodArguments, out customModifiers);
                            //    WriteInlineMethod(lw, methodOrConstructor, optionalParameterTypes, customModifiers);
                            //}
                            //else
                            //{
                            //    WriteInlineMethod(lw, methodOrConstructor, Type.EmptyTypes, null);
                            //}
                        break;
             
              
                    case OperandType.InlineI8:
                        opCode.Value = ReadInt64(code, ref pos);
                        break;
                    case OperandType.ShortInlineVar:
                    case OperandType.ShortInlineI:
                         opCode.Value =  (sbyte)code[pos++];
                        break;
                    case OperandType.InlineR:
                        opCode.FValue =  ReadDouble(code, ref pos);
                        break;
                    case OperandType.ShortInlineR:
                        opCode.FValue = ReadSingle(code, ref pos);
                        break;
                    case OperandType.InlineVar:
                        opCode.Value =  ReadInt16(code, ref pos);
                        break;

                    default:
                        throw new InvalidOperationException();
                } // switch
            } // while
            return result;
        }

        short ReadInt16(byte[] code, ref int pos)
        {
            short s = BitConverter.ToInt16(code, pos);
            pos += 2;
            return s;
        }

        int ReadInt32(byte[] code, ref int pos)
        {
            int i = BitConverter.ToInt32(code, pos);
            pos += 4;
            return i;
        }

        long ReadInt64(byte[] code, ref int pos)
        {
            long l = BitConverter.ToInt64(code, pos);
            pos += 8;
            return l;
        }

        float ReadSingle(byte[] code, ref int pos)
        {
            float f = BitConverter.ToSingle(code, pos);
            pos += 4;
            return f;
        }

        double ReadDouble(byte[] code, ref int pos)
        {
            double d = BitConverter.ToDouble(code, pos);
            pos += 8;
            return d;
        }

    }
}
