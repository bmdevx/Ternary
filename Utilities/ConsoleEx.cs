using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ConsoleEx
    {
        private const int STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr ConsoleOutput, bool MaximumWindow, CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, [In, Out] CONSOLE_FONT_INFO_EX lpConsoleCurrentFont);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int dwType);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int SetConsoleFont(IntPtr hOut, uint dwFontNum);


        public static void SetTernaryFont()
        {
            SetConsoleFont("Ternary");
        }

        public static void SetConsoleFont(string fontName = "Consolas")
        {
            CONSOLE_FONT_INFO_EX cfiex = new CONSOLE_FONT_INFO_EX();

            IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);

            GetCurrentConsoleFontEx(hnd, false, cfiex);

            cfiex.FaceName = fontName;

            SetCurrentConsoleFontEx(hnd, false, cfiex);
        }
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

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class CONSOLE_FONT_INFO_EX
    {
        private int cbSize;
        public CONSOLE_FONT_INFO_EX()
        {
            cbSize = Marshal.SizeOf(typeof(CONSOLE_FONT_INFO_EX));
        }
        public int FontIndex;
        public short FontWidth;
        public short FontHeight;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }
}
