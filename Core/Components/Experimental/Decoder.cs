using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class Decoder
    {
        public void Decode(object sender, AInstruction instruction)
        {
            switch (instruction)
            {
                case AInstruction.INST:
                    break;
                case AInstruction.MOV:
                    break;
                case AInstruction.CMP:
                    break;
                case AInstruction.STR:
                    break;
                case AInstruction.STR12:
                    break;
                case AInstruction.LOAD:
                    break;
                case AInstruction.LOAD12:
                    break;
                case AInstruction.SLT:
                    break;
                case AInstruction.SRT:
                    break;
                case AInstruction.GET:
                    break;
                case AInstruction.PUT:
                    break;
                case AInstruction.BREAK:
                    break;
                case AInstruction.HALT:
                    break;
                case AInstruction.JMP:
                    break;
                case AInstruction.JMPL:
                    break;
                case AInstruction.JMPX:
                    break;
                case AInstruction.JE:
                    break;
                case AInstruction.JNE:
                    break;
                case AInstruction.JB:
                    break;
                case AInstruction.JBE:
                    break;
                case AInstruction.JA:
                    break;
                case AInstruction.JAE:
                    break;
                case AInstruction.ADD:
                    break;
                case AInstruction.SUB:
                    break;
                case AInstruction.MAX:
                    break;
                case AInstruction.MIN:
                    break;
                case AInstruction.MXNOT:
                    break;
                case AInstruction.MNNOT:
                    break;
            }
        }
    }
}
