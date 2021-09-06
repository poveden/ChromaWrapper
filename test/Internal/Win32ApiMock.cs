using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ChromaWrapper.Internal;
using static ChromaWrapper.Internal.NativeMethods;

namespace ChromaWrapper.Tests.Internal
{
    internal class Win32ApiMock : IWin32Api
    {
#pragma warning disable SA1310
        public const uint WM_CREATE = 0x0001;
        public const uint WM_GETMINMAXINFO = 0x0024;
        public const uint WM_NCCREATE = 0x0081;
        public const uint WM_NCCALCSIZE = 0x0083;

        public const uint WM_UAHDESTROYWINDOW = 0x0090;
        public const uint WM_NCDESTROY = 0x0082;

        public const uint WM_QUIT = 0x0012;
#pragma warning restore SA1310

        private readonly Dictionary<ushort, WNDPROC> _registeredWindowClasses = new Dictionary<ushort, WNDPROC>();
        private readonly Dictionary<IntPtr, string> _hWndClass = new Dictionary<IntPtr, string>();
        private readonly ConcurrentQueue<MSG> _messageQueue = new ConcurrentQueue<MSG>();

        public virtual IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
        {
            var hWnd = (IntPtr)Thread.CurrentThread.ManagedThreadId;
            _hWndClass.Add(hWnd, lpClassName);

            SendMessage(hWnd, WM_GETMINMAXINFO, IntPtr.Zero, IntPtr.Zero);
            SendMessage(hWnd, WM_NCCREATE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(hWnd, WM_NCCALCSIZE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(hWnd, WM_CREATE, IntPtr.Zero, IntPtr.Zero);

            return hWnd;
        }

        public virtual IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            if (uMsg == WM_CLOSE)
            {
                SendMessage(hWnd, WM_UAHDESTROYWINDOW, IntPtr.Zero, IntPtr.Zero);
                SendMessage(hWnd, WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
                SendMessage(hWnd, WM_NCDESTROY, IntPtr.Zero, IntPtr.Zero);
                _hWndClass.Remove(hWnd);
                return IntPtr.Zero;
            }

            return IntPtr.Zero;
        }

        public virtual bool DestroyWindow(IntPtr hwnd)
        {
            return true;
        }

        public virtual IntPtr DispatchMessage(ref MSG lpmsg)
        {
            return IntPtr.Zero;
        }

        public virtual int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax)
        {
            while (!_messageQueue.TryDequeue(out lpMsg))
            {
                Thread.Sleep(100);
            }

            return lpMsg.message != WM_QUIT ? 1 : 0;
        }

        public virtual IntPtr GetModuleHandle(string? moduleName)
        {
            return Process.GetCurrentProcess().Handle;
        }

        public virtual IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            string className = _hWndClass[hWnd]!;
            ushort atom = BuildMockAtom(className);
            var wndProc = _registeredWindowClasses[atom]!;

            return wndProc.Invoke(hWnd, msg, wParam, lParam);
        }

        public virtual void PostQuitMessage(int nExitCode)
        {
            PostMessageInternal(IntPtr.Zero, WM_QUIT, IntPtr.Zero, IntPtr.Zero);
        }

        public virtual ushort RegisterClassEx(WNDCLASSEX lpwcx)
        {
            ushort atom = BuildMockAtom(lpwcx.lpszClassName);
            _registeredWindowClasses.Add(atom, lpwcx.lpfnWndProc);
            return atom;
        }

        public virtual bool TranslateMessage(ref MSG lpMsg)
        {
            return true;
        }

        public virtual bool UnregisterClass(ushort lpClassName, IntPtr hInstance)
        {
            return _registeredWindowClasses.Remove(lpClassName);
        }

        internal bool PostMessageInternal(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            _messageQueue.Enqueue(new MSG
            {
                hwnd = hWnd,
                message = msg,
                wParam = wParam,
                lParam = lParam,
                time = Environment.TickCount,
            });

            return true;
        }

        private static ushort BuildMockAtom(string className)
        {
            return (ushort)(className.GetHashCode(StringComparison.Ordinal) & 0xFFFF);
        }
    }
}
