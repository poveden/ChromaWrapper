using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ChromaWrapper.Internal
{
    // Reference: https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/_rz_chroma_s_d_k_8h.html
    internal sealed partial class NativeMethods : IWin32Api
    {
        public const uint WM_DESTROY = 0x0002;
        public const uint WM_CLOSE = 0x0010;

        public const int ERROR_CLASS_ALREADY_EXISTS = 1410;

        public static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

        public delegate IntPtr WNDPROC(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public ushort RegisterClassEx(WNDCLASSEX lpwcx)
        {
            return Impl.RegisterClassEx(lpwcx);
        }

        public IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
        {
            return Impl.CreateWindowEx(dwExStyle, lpClassName, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);
        }

        public IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            return Impl.DefWindowProc(hWnd, uMsg, wParam, lParam);
        }

        public bool DestroyWindow(IntPtr hwnd)
        {
            return Impl.DestroyWindow(hwnd);
        }

        public int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax)
        {
            return Impl.GetMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax);
        }

        public bool TranslateMessage(ref MSG lpMsg)
        {
            return Impl.TranslateMessage(ref lpMsg);
        }

        public IntPtr DispatchMessage(ref MSG lpmsg)
        {
            return Impl.DispatchMessage(ref lpmsg);
        }

        public IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return Impl.SendMessage(hWnd, msg, wParam, lParam);
        }

        public void PostQuitMessage(int nExitCode)
        {
            Impl.PostQuitMessage(nExitCode);
        }

        public bool UnregisterClass(ushort lpClassName, IntPtr hInstance)
        {
            return Impl.UnregisterClass(lpClassName, hInstance);
        }

        public IntPtr GetModuleHandle(string? moduleName)
        {
            return Impl.GetModuleHandle(moduleName);
        }

        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Interop struct")]
        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASSEX
        {
            public uint cbSize;

            public uint style;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WNDPROC lpfnWndProc;

            public int cbClsExtra;

            public int cbWndExtra;

            public IntPtr hInstance;

            public IntPtr hIcon;

            public IntPtr hCursor;

            public IntPtr hbrBackground;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszMenuName;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszClassName;

            public IntPtr hIconSm;
        }

        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Interop struct")]
        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int time;
            public POINT pt;
            public int lPrivate;
        }

        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Interop struct")]
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        private static partial class Impl
        {
            [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern ushort RegisterClassEx([In] WNDCLASSEX lpwcx);

            [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnregisterClass(ushort lpClassName, IntPtr hInstance);

            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr GetModuleHandle(string? moduleName);

            [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr CreateWindowEx(uint dwExStyle, [MarshalAs(UnmanagedType.LPWStr)] string lpClassName, [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

            [DllImport("user32", CharSet = CharSet.Unicode)]
            public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DestroyWindow(IntPtr hwnd);

            [DllImport("user32")]
            public static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool TranslateMessage([In] ref MSG lpMsg);

            [DllImport("user32")]
            public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

            [DllImport("user32", CharSet = CharSet.Unicode)]
            public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32")]
            public static extern void PostQuitMessage(int nExitCode);
        }
    }
}
