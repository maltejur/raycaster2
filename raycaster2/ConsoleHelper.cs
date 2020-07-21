using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

public struct SMALL_RECT
{
    public short Left;
    public short Top;
    public short Right;
    public short Bottom;

}

public struct CONSOLE_SCREEN_BUFFER_INFO
{

    public COORD dwSize;
    public COORD dwCursorPosition;
    public short wAttributes;
    public SMALL_RECT srWindow;
    public COORD dwMaximumWindowSize;

}

public static class ConsoleHelper
{
    private const int FixedWidthTrueType = 54;
    private const int StandardOutputHandle = -11;

    static private IntPtr consoleHandle;
    static private IntPtr prevConsoleHandle;
    static private uint bytesWritten;
    static public CONSOLE_SCREEN_BUFFER_INFO screenBufferInfo;

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr GetStdHandle(int nStdHandle);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);


    private static readonly IntPtr ConsoleOutputHandle = GetStdHandle(StandardOutputHandle);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FontInfo
    {
        internal int cbSize;
        internal int FontIndex;
        internal short FontWidth;
        public short FontSize;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.wc, SizeConst = 32)]
        public string FontName;
    }

    public static FontInfo[] SetCurrentFont(string font, short fontSize = 0)
    {
        Debug.WriteLine("Set Current Font: " + font);

        FontInfo before = new FontInfo
        {
            cbSize = Marshal.SizeOf<FontInfo>()
        };

        if (GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
        {

            FontInfo set = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>(),
                FontIndex = 0,
                FontFamily = FixedWidthTrueType,
                FontName = font,
                FontWeight = 400,
                FontSize = fontSize > 0 ? fontSize : before.FontSize
            };

            // Get some settings from current font.
            if (!SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref set))
            {
                var ex = Marshal.GetLastWin32Error();
                Debug.WriteLine("Set error " + ex);
                throw new System.ComponentModel.Win32Exception(ex);
            }

            FontInfo after = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>()
            };
            GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref after);

            return new[] { before, set, after };
        }
        else
        {
            var er = Marshal.GetLastWin32Error();
            Debug.WriteLine("Get error " + er);
            throw new System.ComponentModel.Win32Exception(er);
        }
    }

    [DllImport("Kernel32.dll")]
    static public extern IntPtr CreateConsoleScreenBuffer(
        uint dwDesiredAccess,
        uint dwShareMode,
        IntPtr secutiryAttributes,
        uint flags,
        IntPtr screenBufferData
    );

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    static public extern bool WriteConsoleOutputCharacter(
        IntPtr hConsoleOutput,
        string lpCharacter,
        uint nLength,
        COORD dwWriteCoord,
        out uint lpNumberOfCharsWritten
    );

    [DllImport("kernel32.dll")]
    static public extern bool SetConsoleActiveScreenBuffer(
        IntPtr hConsoleOutput
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool GetConsoleScreenBufferInfo(
        IntPtr hConsoleOutput,
        out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo
    );

    static public COORD GetConsoleSize()
    {
        GetConsoleScreenBufferInfo(consoleHandle, out screenBufferInfo);
        int columns = screenBufferInfo.srWindow.Right - screenBufferInfo.srWindow.Left + 1;
        int rows = screenBufferInfo.srWindow.Bottom - screenBufferInfo.srWindow.Top + 1;
        return new COORD((short)columns, (short)rows);
    }


    static public void InitConsoleOutput()
    {
        prevConsoleHandle = GetStdHandle(11);
        consoleHandle = CreateConsoleScreenBuffer(0x40000000 | 0x80000000, 0x00000001, IntPtr.Zero, 1, IntPtr.Zero);
        SetConsoleActiveScreenBuffer(consoleHandle);
    }

    static public void Write(string str, uint lenght)
    {
        WriteConsoleOutputCharacter(consoleHandle, str, lenght, new COORD(0, 0), out bytesWritten);
    }
}