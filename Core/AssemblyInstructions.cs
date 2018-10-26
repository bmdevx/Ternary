using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary
{
    //IIII RRRR MMMM
    public enum AInstruction
    {
        INST = 0, //Instruction
        MOV = 1,
        CMP = -1, //EQ

        STR = 2,
        STR12 = -2, //TRORT
        LOAD = 3,
        LOAD12 = -3, //TRORT
        
        SLT = 4,
        SRT = -4,

        GET = -5,
        PUT = 5,

        BREAK = 6,
        HALT = -6,

        JMP = 7,
        JMPL = -7,  //Jump lin
        JMPX = 8,  //Jump X #

        JE = 9,  //Jump eq
        JNE = -9, //Jump not eq
        JB = -10,  //Jump below
        JBE = -11, //Jump below eq
        JA = 10,  //Jump above
        JAE = 11, //Jump above eq

        ADD = 12,
        SUB = -12,
        MAX = 13,
        MIN = -13,
        
        MXNOT = 14,
        MNNOT = -14
    }
}
