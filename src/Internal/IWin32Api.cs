using System;
using static ChromaWrapper.Internal.NativeMethods;

namespace ChromaWrapper.Internal
{
    /// <summary>
    /// Exposes native Win32 API methods.
    /// </summary>
    internal interface IWin32Api
    {
        /// <summary>
        /// Registers a window class for subsequent use in calls to the CreateWindow or <see cref="CreateWindowEx"/> function.
        /// </summary>
        /// <param name="lpwcx">A pointer to a WNDCLASSEX structure.</param>
        /// <returns>
        /// If the function succeeds, the return value is a class atom that uniquely identifies the class being registered.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        ushort RegisterClassEx(WNDCLASSEX lpwcx);

        /// <summary>
        /// Retrieves a module handle for the specified module.
        /// </summary>
        /// <param name="moduleName">The name of the loaded module (either a .dll or .exe file).</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the specified module.
        /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlew">GetModuleHandleW function (libloaderapi.h)</seealso>
        IntPtr GetModuleHandle(string? moduleName);

        /// <summary>
        /// Unregisters a window class, freeing the memory required for the class.
        /// </summary>
        /// <param name="lpClassName">A null-terminated string or a class atom.</param>
        /// <param name="hInstance">A handle to the instance of the module that created the class.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the class could not be found or if a window still exists that was created with the class, the return value is zero.
        /// </returns>
        bool UnregisterClass(ushort lpClassName, IntPtr hInstance);

        /// <summary>
        /// Creates an overlapped, pop-up, or child window.
        /// </summary>
        /// <param name="dwExStyle">The extended window style of the window being created.</param>
        /// <param name="lpClassName">A null-terminated string or a class atom created by a previous call to the <see cref="RegisterClassEx"/> or RegisterClassEx function.</param>
        /// <param name="lpWindowName">The window name.</param>
        /// <param name="dwStyle">The style of the window being created.</param>
        /// <param name="x">The initial horizontal position of the window.</param>
        /// <param name="y">The initial vertical position of the window.</param>
        /// <param name="nWidth">The width, in device units, of the window.</param>
        /// <param name="nHeight">The height, in device units, of the window.</param>
        /// <param name="hWndParent">A handle to the parent or owner window of the window being created.</param>
        /// <param name="hMenu">A handle to a menu, or specifies a child-window identifier, depending on the window style.</param>
        /// <param name="hInstance">A handle to the instance of the module to be associated with the window.</param>
        /// <param name="lpParam">Pointer to a value to be passed to the window.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the new window.
        /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createwindowexw">CreateWindowExW function (winuser.h)</seealso>
        IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        /// <summary>
        /// Calls the default window procedure to provide default processing for any window messages that an application does not process.
        /// </summary>
        /// <param name="hWnd">A handle to the window procedure that received the message.</param>
        /// <param name="uMsg">The message.</param>
        /// <param name="wParam">Additional message information (1/2).</param>
        /// <param name="lParam">Additional message information (2/2).</param>
        /// <returns>The return value is the result of the message processing and depends on the message.</returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-defwindowprocw">DefWindowProcW function (winuser.h)</seealso>
        IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Destroys the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window to be destroyed.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.
        /// </returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroywindow">DestroyWindow function (winuser.h)</seealso>
        bool DestroyWindow(IntPtr hwnd);

        /// <summary>
        /// Retrieves a message from the calling thread's message queue.
        /// </summary>
        /// <param name="lpMsg">A pointer to an MSG structure that receives message information from the thread's message queue.</param>
        /// <param name="hWnd">A handle to the window whose messages are to be retrieved. The window must belong to the current thread.</param>
        /// <param name="wMsgFilterMin">The integer value of the lowest message value to be retrieved.</param>
        /// <param name="wMsgFilterMax">The integer value of the highest message value to be retrieved.</param>
        /// <returns>
        /// If the function retrieves a message other than WM_QUIT, the return value is nonzero.
        /// If the function retrieves the WM_QUIT message, the return value is zero.
        /// If there is an error, the return value is -1.
        /// </returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmessage">GetMessage function (winuser.h)</seealso>
        int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        /// <summary>
        /// Translates virtual-key messages into character messages.
        /// </summary>
        /// <param name="lpMsg">A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue by using the GetMessage or PeekMessage function.</param>
        /// <returns>
        /// If the message is translated (that is, a character message is posted to the thread's message queue), the return value is nonzero.
        /// If the message is WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP, the return value is nonzero, regardless of the translation.
        /// If the message is not translated (that is, a character message is not posted to the thread's message queue), the return value is zero.
        /// </returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-translatemessage">TranslateMessage function (winuser.h)</seealso>
        bool TranslateMessage(ref MSG lpMsg);

        /// <summary>
        /// Dispatches a message to a window procedure.
        /// </summary>
        /// <param name="lpmsg">A pointer to a structure that contains the message.</param>
        /// <returns>The return value specifies the value returned by the window procedure.</returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-dispatchmessage">DispatchMessage function (winuser.h)</seealso>
        IntPtr DispatchMessage(ref MSG lpmsg);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure is to receive the message.</param>
        /// <param name="msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information (1/2).</param>
        /// <param name="lParam">Additional message-specific information (2/2).</param>
        /// <returns>
        /// The return value specifies the result of the message processing; it depends on the message sent.
        /// </returns>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagew">SendMessageW function (winuser.h)</seealso>
        IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Indicates to the system that a thread has made a request to terminate (quit).
        /// </summary>
        /// <param name="nExitCode">The application exit code.</param>
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-postquitmessage">PostQuitMessage function (winuser.h)</seealso>
        void PostQuitMessage(int nExitCode);
    }
}
