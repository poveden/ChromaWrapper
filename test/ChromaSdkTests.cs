using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.InteropServices;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Data;
using ChromaWrapper.Events;
using ChromaWrapper.Headset;
using ChromaWrapper.Internal;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using ChromaWrapper.Sdk;
using ChromaWrapper.Tests.Internal;
using ChromaWrapper.Tests.Internal.Xunit;
using Moq;
using Xunit;
using Xunit.Abstractions;
using static ChromaWrapper.Internal.NativeMethods;

namespace ChromaWrapper.Tests
{
    [Collection(PInvokeTestsCollection.Name)]
    public class ChromaSdkTests : SdkTestBase
    {
        private static readonly ResourceManager _resources = typeof(ChromaSdkException).GetPrivateStaticField<ResourceManager>("_resources")!;

        public ChromaSdkTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
            NativeChromaSdkApiMock = IsNativeSdkTest
                ? new Mock<ChromaSdkApiProxy>(MockBehavior.Loose) { CallBase = true }.As<IChromaSdkApi>()
                : new Mock<ChromaSdkApiMock>(MockBehavior.Loose) { CallBase = true }.As<IChromaSdkApi>();
        }

        private Mock<IChromaSdkApi> NativeChromaSdkApiMock { get; }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void ChecksIfTheSdkIsAvailable()
        {
            Assert.True(ChromaSdk.IsSdkAvailable(NativeChromaSdkApiMock.Object));
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsInitAndUnInit()
        {
            using (var sdk = CreateInstance(null, true))
            {
                // Just dispose.
            }

            NativeChromaSdkApiMock.Verify(x => x.Init(), Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.UnInit(), Times.Once());
            NativeChromaSdkApiMock.VerifyNoOtherCalls();
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsInitSDKAndUnInit()
        {
            var ai = new ChromaAppInfo
            {
                Title = "ChromaSdk Unit Tests",
                Description = "ChromaSdk Unit Tests",
                AuthorName = "John Doe",
                AuthorContact = "john.doe@example.com",
                Category = AppCategory.Utility,
                SupportedDevice = SupportedDevices.All,
            };

            using (var sdk = CreateInstance(ai, true))
            {
                // Just dispose.
            }

            NativeChromaSdkApiMock.Verify(x => x.InitSDK(It.IsAny<ChromaAppInfo>()), Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.UnInit(), Times.Once());
            NativeChromaSdkApiMock.VerifyNoOtherCalls();
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsRegisterAndUnregisterEventNotification()
        {
            using (var sdk = CreateInstance(null, false))
            {
                Assert.True(IsClassRegistered());
                Assert.True(IsMessageOnlyWindowActive());
            }

            Assert.False(IsMessageOnlyWindowActive());
            Assert.False(IsClassRegistered());

            NativeChromaSdkApiMock.Verify(x => x.Init(), Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.UnInit(), Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.RegisterEventNotification(It.IsAny<IntPtr>()), Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.UnregisterEventNotification(), Times.Once());
            NativeChromaSdkApiMock.VerifyNoOtherCalls();
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsCreateEffectWithChromaLinkEffect()
        {
            using (var sdk = CreateInstance())
            {
                Assert.Throws<ArgumentNullException>("effect", () => sdk.CreateEffect((IChromaLinkEffect)null!));

                var effectId = sdk.CreateEffect(new StaticChromaLinkEffect());
                Assert.NotEqual(Guid.Empty, effectId);
            }

            NativeChromaSdkApiMock.Verify(x => x.Init(), Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.UnInit(), Times.Once());
            NativeChromaSdkApiMock.Verify(
                x => x.CreateChromaLinkEffect(
                It.Is<ChromaLinkEffectType>(x => x == ChromaLinkEffectType.Static),
                It.IsAny<StaticChromaLinkEffect>(),
                out It.Ref<Guid>.IsAny),
                Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.RegisterEventNotification(It.IsAny<IntPtr>()), Times.Once());
            NativeChromaSdkApiMock.Verify(x => x.UnregisterEventNotification(), Times.Once());
            NativeChromaSdkApiMock.VerifyNoOtherCalls();
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsCreateEffectWithHeadsetEffect()
        {
            using var sdk = CreateInstance();

            Assert.Throws<ArgumentNullException>("effect", () => sdk.CreateEffect((IHeadsetEffect)null!));

            var effectId = sdk.CreateEffect(new StaticHeadsetEffect());
            Assert.NotEqual(Guid.Empty, effectId);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsCreateEffectWithKeyboardEffect()
        {
            using var sdk = CreateInstance();

            Assert.Throws<ArgumentNullException>("effect", () => sdk.CreateEffect((IKeyboardEffect)null!));

            var effectId = sdk.CreateEffect(new StaticKeyboardEffect());
            Assert.NotEqual(Guid.Empty, effectId);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsCreateEffectWithKeypadEffect()
        {
            using var sdk = CreateInstance();

            Assert.Throws<ArgumentNullException>("effect", () => sdk.CreateEffect((IKeypadEffect)null!));

            var effectId = sdk.CreateEffect(new StaticKeypadEffect());
            Assert.NotEqual(Guid.Empty, effectId);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsCreateEffectWithMouseEffect()
        {
            using var sdk = CreateInstance();

            Assert.Throws<ArgumentNullException>("effect", () => sdk.CreateEffect((IMouseEffect)null!));

            var effectId = sdk.CreateEffect(new StaticMouseEffect());
            Assert.NotEqual(Guid.Empty, effectId);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsCreateEffectWithMousepadEffect()
        {
            using var sdk = CreateInstance();

            Assert.Throws<ArgumentNullException>("effect", () => sdk.CreateEffect((IMousepadEffect)null!));

            var effectId = sdk.CreateEffect(new StaticMousepadEffect());
            Assert.NotEqual(Guid.Empty, effectId);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsSetEffect()
        {
            using var sdk = CreateInstance();

            var effectId = sdk.CreateEffect(new StaticMousepadEffect());
            Assert.NotEqual(Guid.Empty, effectId);

            sdk.SetEffect(effectId);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsDeleteEffect()
        {
            using var sdk = CreateInstance();

            var effectId = sdk.CreateEffect(new StaticMousepadEffect());
            Assert.NotEqual(Guid.Empty, effectId);

            sdk.DeleteEffect(effectId);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void KeepsTrackOfAllCreatedEffects()
        {
            using var sdk = CreateInstance();

            var expected = new List<Guid>
            {
                sdk.CreateEffect(new StaticChromaLinkEffect()),
                sdk.CreateEffect(new StaticHeadsetEffect()),
                sdk.CreateEffect(new StaticKeyboardEffect()),
                sdk.CreateEffect(new StaticKeypadEffect()),
                sdk.CreateEffect(new StaticMouseEffect()),
                sdk.CreateEffect(new StaticMousepadEffect()),
            };

            var actual = new List<Guid>(sdk.CreatedEffects);

            expected.Sort();
            actual.Sort();

            Assert.Equal(expected, actual);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void RemovesDeletedEffectsFromTheCreatedEffectsCollection()
        {
            using var sdk = CreateInstance();

            var e1 = sdk.CreateEffect(new StaticKeyboardEffect());
            var e2 = sdk.CreateEffect(new StaticKeyboardEffect());
            var e3 = sdk.CreateEffect(new StaticKeyboardEffect());

            Assert.Equal(3, sdk.CreatedEffects.Count);
            Assert.Contains(e1, sdk.CreatedEffects);
            Assert.Contains(e2, sdk.CreatedEffects);
            Assert.Contains(e3, sdk.CreatedEffects);

            sdk.DeleteEffect(e2);

            Assert.Equal(2, sdk.CreatedEffects.Count);
            Assert.Contains(e1, sdk.CreatedEffects);
            Assert.DoesNotContain(e2, sdk.CreatedEffects);
            Assert.Contains(e3, sdk.CreatedEffects);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void CanDeleteAllCreatedEffects()
        {
            using var sdk = CreateInstance();

            var e1 = sdk.CreateEffect(new StaticKeyboardEffect());
            var e2 = sdk.CreateEffect(new StaticKeyboardEffect());
            Assert.Equal(2, sdk.CreatedEffects.Count);

            sdk.DeleteAllEffects();

            Assert.Empty(sdk.CreatedEffects);
            NativeChromaSdkApiMock.Verify(x => x.DeleteEffect(It.Is<Guid>(x => x == e1)), Times.Once);
            NativeChromaSdkApiMock.Verify(x => x.DeleteEffect(It.Is<Guid>(x => x == e2)), Times.Once);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void DeleteAllEffectsReportsIndividualFailures()
        {
            using var sdk = CreateInstance();

            NativeChromaSdkApiMock
                .Setup(x => x.DeleteEffect(It.IsAny<Guid>()))
                .Returns(ChromaResult.Failed);

            var e1 = sdk.CreateEffect(new StaticKeyboardEffect());
            var e2 = sdk.CreateEffect(new StaticKeyboardEffect());

            var ex = Assert.Throws<AggregateException>(() => sdk.DeleteAllEffects());
            Assert.Empty(sdk.CreatedEffects);

            Assert.Equal(2, ex.InnerExceptions.Count);
            Assert.All(ex.InnerExceptions, x => Assert.IsType<ChromaSdkException>(x));

            var data = new HashSet<Guid>
            {
                (Guid)ex.InnerExceptions[0].Data["effectId"]!,
                (Guid)ex.InnerExceptions[1].Data["effectId"]!,
            };

            Assert.Contains(e1, data);
            Assert.Contains(e2, data);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void WrapsQueryDevice()
        {
            using var sdk = CreateInstance();

            var res = sdk.QueryDevice(ChromaDeviceIds.Chromabox);

            Assert.Equal(ChromaDeviceInfo.HardwareType.System, res.DeviceType);
            Assert.Equal(0, res.Connected);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void QueryDeviceThrowsOnAnInvalidDeviceId()
        {
            using var sdk = CreateInstance();

            Assert.Throws<ArgumentException>("deviceId", () => sdk.QueryDevice(Guid.Empty));
        }

        [SdkFact]
        public void QueryDeviceReturnsDeviceInfoOnConnectedDevice()
        {
            var deviceId = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);

            NativeChromaSdkApiMock
                .Setup(x => x.QueryDevice(deviceId, It.IsAny<ChromaDeviceInfo>()))
                .Callback<Guid, ChromaDeviceInfo>((deviceId, deviceInfo) =>
                {
                    deviceInfo.DeviceType = ChromaDeviceInfo.HardwareType.Unknown;
                    deviceInfo.Connected = 1;
                })
                .Returns(ChromaResult.Success);

            using var sdk = CreateInstance();

            var res = sdk.QueryDevice(deviceId);
            Assert.Equal(ChromaDeviceInfo.HardwareType.Unknown, res.DeviceType);
            Assert.Equal(1, res.Connected);
        }

        [Theory]
        [InlineData("CreateEffect", "effect", ChromaResult.InvalidParameter, typeof(ArgumentException), "InvalidParameter_CreateEffect")]
        [InlineData("CreateEffect", "effect", ChromaResult.NotSupported, typeof(ArgumentException), "NotSupported_CreateEffect")]
        [InlineData("SetEffect", "effect", ChromaResult.NotFound, typeof(ArgumentException), "NotFound_SetEffect")]
        [InlineData("CreateEffect", "effect", ChromaResult.DeviceNotAvailable, typeof(ArgumentException), "DeviceNotAvailable_CreateEffect")]
        [InlineData("Init", "appInfo", ChromaResult.AlreadyInitialized, typeof(ArgumentException), "AlreadyInitialized_Init")]
        [InlineData("*", null, ChromaResult.NotValidState, typeof(InvalidOperationException), "NotValidState")]
        [InlineData("*", null, ChromaResult.AlreadyInitialized, typeof(InvalidOperationException), "AlreadyInitialized")]
        [InlineData("*", null, ChromaResult.AccessDenied, typeof(UnauthorizedAccessException), "AccessDenied")]
        [InlineData("*", null, ChromaResult.ServiceNotActive, typeof(ChromaSdkException), "ServiceNotActive")]
        public void ThrowExceptionHandlesSdkErrors(string methodName, string? paramName, ChromaResult result, Type exceptionType, string expectedResName)
        {
            var ex = Assert.Throws(exceptionType, () => typeof(ChromaSdk).InvokePrivateStaticMethod<object?>("ThrowIfError", result, methodName, paramName));

            string? expectedMessage = _resources.GetString(expectedResName, CultureInfo.CurrentCulture);
            Assert.StartsWith(expectedMessage, ex.Message, StringComparison.Ordinal);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        public void MathodsThrowsWhenObjectHasBeenDisposed()
        {
            var sdk = CreateInstance();
            sdk.Dispose();

            Assert.Throws<ObjectDisposedException>(() => sdk.CreateEffect(new StaticChromaLinkEffect()));
            Assert.Throws<ObjectDisposedException>(() => sdk.CreateEffect(new StaticHeadsetEffect()));
            Assert.Throws<ObjectDisposedException>(() => sdk.CreateEffect(new StaticKeyboardEffect()));
            Assert.Throws<ObjectDisposedException>(() => sdk.CreateEffect(new StaticKeypadEffect()));
            Assert.Throws<ObjectDisposedException>(() => sdk.CreateEffect(new StaticMouseEffect()));
            Assert.Throws<ObjectDisposedException>(() => sdk.CreateEffect(new StaticMousepadEffect()));
            Assert.Throws<ObjectDisposedException>(() => sdk.SetEffect(Guid.NewGuid()));
            Assert.Throws<ObjectDisposedException>(() => sdk.DeleteEffect(Guid.NewGuid()));
            Assert.Throws<ObjectDisposedException>(() => sdk.QueryDevice(Guid.NewGuid()));
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        public void CanBeDisposedTwice()
        {
            var sdk = CreateInstance();
            Assert.False(sdk.GetPrivateField<bool>("_disposed"));

            sdk.Dispose();
            Assert.True(sdk.GetPrivateField<bool>("_disposed"));

            sdk.Dispose();
            Assert.True(sdk.GetPrivateField<bool>("_disposed"));
        }

        [SdkFact]
        public void RaisesEventsFromSdkNotifications()
        {
            using var sdk = CreateInstance();

            void Post(ChromaEvent wParam, bool lParam)
            {
                var ml = sdk!.GetPrivateField<MessageListener>("_messageListener")!;
                Instance.SendMessage(ml.HWnd, WM_CHROMA_EVENT, (IntPtr)wParam, (IntPtr)(lParam ? 1 : 0));
            }

            var ev1 = Assert.Raises<ChromaSdkSupportEventArgs>(h => sdk.ChromaSdkSupport += h, h => sdk.ChromaSdkSupport -= h, () => Post(ChromaEvent.ChromaSdkSupport, true));
            Assert.Same(sdk, ev1.Sender);
            Assert.True(ev1.Arguments.Enabled);

            var ev2 = Assert.Raises<ChromaDeviceAccessEventArgs>(h => sdk.DeviceAccess += h, h => sdk.DeviceAccess -= h, () => Post(ChromaEvent.DeviceAccess, false));
            Assert.Same(sdk, ev2.Sender);
            Assert.False(ev2.Arguments.AccessGranted);

            var ev3 = Assert.Raises<ChromaApplicationStateEventArgs>(h => sdk.ApplicationState += h, h => sdk.ApplicationState -= h, () => Post(ChromaEvent.ApplicationState, true));
            Assert.Same(sdk, ev3.Sender);
            Assert.True(ev3.Arguments.Enabled);
        }

        [SdkFact]
        public void UnknownSdkNotificationsAreIgnored()
        {
            using var sdk = CreateInstance();

            int n1 = 0;
            int n2 = 0;
            int n3 = 0;

            sdk.ChromaSdkSupport += (s, e) => n1++;
            sdk.DeviceAccess += (s, e) => n2++;
            sdk.ApplicationState += (s, e) => n3++;

            var ml = sdk!.GetPrivateField<MessageListener>("_messageListener")!;
            Instance.SendMessage(ml.HWnd, WM_CHROMA_EVENT, (IntPtr)4, (IntPtr)1);

            Assert.Equal(0, n1);
            Assert.Equal(0, n2);
            Assert.Equal(0, n3);
        }

        [SdkFact]
        public void DisposesOfMessageListenerOnFailedConstructor()
        {
            NativeChromaSdkApiMock.Setup(x => x.RegisterEventNotification(It.IsAny<IntPtr>())).Returns(ChromaResult.AlreadyInitialized);

#pragma warning disable IDISP005
            Assert.Throws<InvalidOperationException>(() => CreateInstance());
#pragma warning restore IDISP005

            Assert.False(IsMessageOnlyWindowActive());
            Assert.False(IsClassRegistered());
        }

        private static bool IsClassRegistered()
        {
            var hInstance = Instance.GetModuleHandle(null);
            string lpClassName = typeof(MessageListener).GetPrivateStaticField<string>("_windowClassName")!;

            var info = new Internal.NativeMethods.WNDCLASSEX
            {
                cbSize = (uint)Marshal.SizeOf<Internal.NativeMethods.WNDCLASSEX>(),
            };

            return Internal.NativeMethods.GetClassInfoEx(hInstance, lpClassName, ref info);
        }

        private static bool IsMessageOnlyWindowActive()
        {
            string lpClassName = typeof(MessageListener).GetPrivateStaticField<string>("_windowClassName")!;

            var hWnd = Internal.NativeMethods.FindWindowEx(HWND_MESSAGE, IntPtr.Zero, lpClassName, null);

            return hWnd != IntPtr.Zero;
        }

        private ChromaSdk CreateInstance(ChromaAppInfo? appInfo = null, bool suppressEvents = false)
        {
            return new ChromaSdk(appInfo, suppressEvents, NativeChromaSdkApiMock.Object);
        }
    }
}
