using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChromaWrapper.Internal
{
    internal sealed class MessageListener : IDisposable
    {
        private const string _windowClassName = "ChromaWrapper_EventListener";

        private readonly IWin32Api _nativeMethods;
        private readonly MessageHandler _messageHandler;
        private readonly Task<uint> _winMainTask;
        private readonly NativeMethods.WNDPROC _wndProc;

        private bool _disposed;

        public MessageListener(IWin32Api nativeMethods, MessageHandler messageHandler)
        {
            _nativeMethods = nativeMethods;
            _messageHandler = messageHandler;
            _wndProc = new NativeMethods.WNDPROC(WndProc);

            var tcsInitialized = new TaskCompletionSource<IntPtr>();

            _winMainTask = new Task<uint>(WinMain, tcsInitialized, TaskCreationOptions.LongRunning);
            _winMainTask.Start();

            // At this point, EITHER the initialization in WinMain failed, OR we're entering the message loop.
            if (Task.WaitAny(tcsInitialized.Task, _winMainTask) == 1)
            {
                throw _winMainTask.Exception?.InnerException ?? new InvalidOperationException();
            }

            HWnd = tcsInitialized.Task.Result;
        }

        public delegate bool MessageHandler(uint msg, IntPtr wParam, IntPtr lParam);

        public IntPtr HWnd { get; }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            if (_winMainTask.Status == TaskStatus.Running)
            {
                _ = _nativeMethods.SendMessage(HWnd, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _winMainTask.Wait();
            }

            _winMainTask.Dispose();
            _disposed = true;
        }

        private uint WinMain(object? winMainState)
        {
            using DisposableHandle<ushort> wcAtom = RegisterWindowClass();
            using DisposableHandle<IntPtr> hWnd = CreateMessageOnlyWindow();

            // If the previous code didn't throw, we report back that we're about to enter the message loop.
            var tcsInitialized = (TaskCompletionSource<IntPtr>)winMainState!;
            tcsInitialized.SetResult(hWnd.Handle);

            NativeMethods.MSG msg;
            int bRet;

            while ((bRet = _nativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                if (bRet == -1)
                {
                    throw new Win32Exception();
                }
                else
                {
                    _ = _nativeMethods.TranslateMessage(ref msg);
                    _ = _nativeMethods.DispatchMessage(ref msg);
                }
            }

            return unchecked((uint)msg.wParam.ToInt32());
        }

        private DisposableHandle<ushort> RegisterWindowClass()
        {
            IntPtr hInstance = _nativeMethods.GetModuleHandle(null);

            var wc = new NativeMethods.WNDCLASSEX
            {
                cbSize = (uint)Marshal.SizeOf<NativeMethods.WNDCLASSEX>(),
                lpfnWndProc = _wndProc,
                hInstance = hInstance,
                lpszClassName = _windowClassName,
            };

            ushort wcAtom = _nativeMethods.RegisterClassEx(wc);

            if (wcAtom != 0)
            {
                return new DisposableHandle<ushort>(wcAtom, handle => _nativeMethods.UnregisterClass(handle, hInstance));
            }

            var ex = new Win32Exception();

            if (ex.NativeErrorCode == NativeMethods.ERROR_CLASS_ALREADY_EXISTS)
            {
                return new DisposableHandle<ushort>(0, handle => { });
            }

            throw ex;
        }

        private DisposableHandle<IntPtr> CreateMessageOnlyWindow()
        {
            IntPtr hWnd = _nativeMethods.CreateWindowEx(0, _windowClassName, _windowClassName, 0, 0, 0, 0, 0, NativeMethods.HWND_MESSAGE, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            if (hWnd == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            return new DisposableHandle<IntPtr>(hWnd, handle => _nativeMethods.DestroyWindow(handle));
        }

        private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == NativeMethods.WM_DESTROY)
            {
                _nativeMethods.PostQuitMessage(0);
                return IntPtr.Zero;
            }

            if (_messageHandler(msg, wParam, lParam))
            {
                return IntPtr.Zero;
            }

            return _nativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
        }

        private sealed class DisposableHandle<T> : IDisposable
        {
            private readonly Action<T> _disposeAction;

            public DisposableHandle(T handle, Action<T> disposeAction)
            {
                _disposeAction = disposeAction;
                Handle = handle;
            }

            public T Handle { get; }

            public void Dispose()
            {
                _disposeAction(Handle);
            }
        }
    }
}
