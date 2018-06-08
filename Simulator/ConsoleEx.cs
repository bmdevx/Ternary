using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public static class ConsoleEx
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(IntPtr ConsoleOutput, bool MaximumWindow, CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;

        public COORD(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_FONT_INFO_EX
    {
        public uint cbSize;
        public uint nFont;
        public COORD dwFontSize;
        public ushort FontFamily;
        public ushort FontWeight;

        UInt64 face0, face1, face2, face3, face4, face5, face6, face7;
    }
}
