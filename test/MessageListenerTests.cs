using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using ChromaWrapper.Internal;
using ChromaWrapper.Tests.Internal;
using Moq;
using Xunit;
using static ChromaWrapper.Internal.NativeMethods;
using static ChromaWrapper.Tests.Internal.Win32ApiMock;

namespace ChromaWrapper.Tests
{
    [Collection(PInvokeTestsCollection.Name)]
    public class MessageListenerTests
    {
        [Fact]
        public void SetsUpAMessageLoop()
        {
            const uint WM_APP = 0x8000;

            var msgs = new List<uint>();
            using var mre = new ManualResetEvent(false);

            bool Handler(uint msg, IntPtr wParam, IntPtr lParam)
            {
                msgs.Add(msg);

                if (msg == WM_APP)
                {
                    mre.Set();
                    return true;
                }

                return false;
            }

            var ml = new MessageListener(Instance, Handler);

            Assert.Contains(WM_CREATE, msgs);
            msgs.Clear();

            Assert.True(Internal.NativeMethods.PostMessage(ml.HWnd, WM_APP, IntPtr.Zero, IntPtr.Zero));
            Assert.True(mre.WaitOne(1000));
            msgs.Clear();

#pragma warning disable IDISP017
            ml.Dispose();
#pragma warning restore IDISP017

            Assert.Contains(WM_CLOSE, msgs);
        }

        [Fact]
        public void ToleratesRunningMoreThanOneConcurrentInstance()
        {
            using var ml1 = new MessageListener(Instance, (m, w, l) => false);
            using var ml2 = new MessageListener(Instance, (m, w, l) => false);

            Assert.NotEqual(IntPtr.Zero, ml1.HWnd);
            Assert.NotEqual(IntPtr.Zero, ml2.HWnd);
            Assert.NotEqual(ml1.HWnd, ml2.HWnd);
        }

        [Fact]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        public void CanBeDisposedTwice()
        {
            var ml = new MessageListener(Instance, (m, w, l) => false);
            Assert.False(ml.GetPrivateField<bool>("_disposed"));

            ml.Dispose();
            Assert.True(ml.GetPrivateField<bool>("_disposed"));

            ml.Dispose();
            Assert.True(ml.GetPrivateField<bool>("_disposed"));
        }

        [Fact]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP005:Return type should indicate that the value should be disposed.", Justification = "Never gets to dispose")]
        public void ThrowsOnWin32ExceptionsAndCleansUpWin32ResourcesWhenTheWindowClassCannotBeRegistered()
        {
            var mock = new Mock<Win32ApiMock>(MockBehavior.Loose) { CallBase = true };
            mock.Setup(x => x.RegisterClassEx(It.IsAny<WNDCLASSEX>())).Returns(0);

            Assert.Throws<Win32Exception>(() => new MessageListener(mock.Object, (m, w, l) => false));
            VerifyWin32ClassAndWindowMethods(mock, Times.Once(), Times.Never(), Times.Never(), Times.Never());
        }

        [Fact]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP005:Return type should indicate that the value should be disposed.", Justification = "Never gets to dispose")]
        public void ThrowsOnWin32ExceptionsAndCleansUpWin32ResourcesWhenTheWindowCannotBeCreated()
        {
            var mock = new Mock<Win32ApiMock>(MockBehavior.Loose) { CallBase = true };
            mock.Setup(x => x.CreateWindowEx(It.IsAny<uint>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<uint>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), It.IsAny<IntPtr>())).Returns(IntPtr.Zero);

            Assert.Throws<Win32Exception>(() => new MessageListener(mock.Object, (m, w, l) => false));
            VerifyWin32ClassAndWindowMethods(mock, Times.Once(), Times.Once(), Times.Never(), Times.Once());
        }

        [Fact]
        public void MessageLoopExitsOnUnhandledError()
        {
            var mock = new Mock<Win32ApiMock>(MockBehavior.Loose) { CallBase = true };
            mock.Setup(x => x.GetMessage(out It.Ref<MSG>.IsAny, It.IsAny<IntPtr>(), It.IsAny<uint>(), It.IsAny<uint>())).Returns(-1);

            using var ml = new MessageListener(mock.Object, (m, w, l) => false);
            var winMainTask = ml.GetPrivateField<Task<uint>>("_winMainTask")!;
            winMainTask.ContinueWith(t => { }, TaskScheduler.Default).Wait();
            Assert.IsType<Win32Exception>(winMainTask.Exception!.InnerException);
        }

        private static void VerifyWin32ClassAndWindowMethods(Mock<Win32ApiMock> mock, Times registerClass, Times createWindow, Times destroyWindow, Times unregisterClass)
        {
            mock.Verify(x => x.RegisterClassEx(It.IsAny<WNDCLASSEX>()), registerClass);
            mock.Verify(x => x.CreateWindowEx(It.IsAny<uint>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<uint>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), It.IsAny<IntPtr>()), createWindow);
            mock.Verify(x => x.DestroyWindow(It.IsAny<IntPtr>()), destroyWindow);
            mock.Verify(x => x.UnregisterClass(It.IsAny<ushort>(), It.IsAny<IntPtr>()), unregisterClass);
        }
    }
}
