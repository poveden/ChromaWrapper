using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Data;
using ChromaWrapper.Headset;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Internal
{
    internal sealed partial class NativeMethods : IChromaSdkApi
    {
        public const int WM_CHROMA_EVENT = 0xA000; // WM_APP + 0x2000

        public enum ChromaEvent
        {
            ChromaSdkSupport = 1,
            DeviceAccess = 2,
            ApplicationState = 3,
        }

        public bool IsSdkAvailable()
        {
            if (NativeLibrary.TryLoad(Impl.SdkDllFilename, Assembly.GetExecutingAssembly(), DllImportSearchPath.SafeDirectories, out IntPtr handle))
            {
                NativeLibrary.Free(handle);
                return true;
            }

            return false;
        }

        public ChromaResult Init()
        {
            return Impl.Init();
        }

        public ChromaResult InitSDK(ChromaAppInfo pAppInfo)
        {
            return Impl.InitSDK(pAppInfo);
        }

        public ChromaResult UnInit()
        {
            return Impl.UnInit();
        }

        public ChromaResult CreateChromaLinkEffect(ChromaLinkEffectType effect, IChromaLinkEffect pParam, out Guid pEffectId)
        {
            return Impl.CreateChromaLinkEffect(effect, pParam, out pEffectId);
        }

        public ChromaResult CreateHeadsetEffect(HeadsetEffectType effect, IHeadsetEffect pParam, out Guid pEffectId)
        {
            return Impl.CreateHeadsetEffect(effect, pParam, out pEffectId);
        }

        public ChromaResult CreateKeyboardEffect(KeyboardEffectType effect, IKeyboardEffect pParam, out Guid pEffectId)
        {
            return Impl.CreateKeyboardEffect(effect, pParam, out pEffectId);
        }

        public ChromaResult CreateKeypadEffect(KeypadEffectType effect, IKeypadEffect pParam, out Guid pEffectId)
        {
            return Impl.CreateKeypadEffect(effect, pParam, out pEffectId);
        }

        public ChromaResult CreateMouseEffect(MouseEffectType effect, IMouseEffect pParam, out Guid pEffectId)
        {
            return Impl.CreateMouseEffect(effect, pParam, out pEffectId);
        }

        public ChromaResult CreateMousepadEffect(MousepadEffectType effect, IMousepadEffect pParam, out Guid pEffectId)
        {
            return Impl.CreateMousepadEffect(effect, pParam, out pEffectId);
        }

        public ChromaResult DeleteEffect(Guid effectId)
        {
            return Impl.DeleteEffect(effectId);
        }

        public ChromaResult SetEffect(Guid effectId)
        {
            return Impl.SetEffect(effectId);
        }

        public ChromaResult RegisterEventNotification(IntPtr hWnd)
        {
            return Impl.RegisterEventNotification(hWnd);
        }

        public ChromaResult UnregisterEventNotification()
        {
            return Impl.UnregisterEventNotification();
        }

        public ChromaResult QueryDevice(Guid deviceId, ChromaDeviceInfo deviceInfo)
        {
            return Impl.QueryDevice(deviceId, deviceInfo);
        }

        private static partial class Impl
        {
            public static readonly string SdkDllFilename = Environment.Is64BitProcess ? _sdkDll64Bit : _sdkDll32Bit;

            private const string _sdkDll = "RzChromaSDK";
            private const string _sdkDll64Bit = "RzChromaSDK64.dll";
            private const string _sdkDll32Bit = "RzChromaSDK.dll";

            static Impl()
            {
                NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);
            }

            [DllImport(_sdkDll)]
            public static extern ChromaResult Init();

            [DllImport(_sdkDll)]
            public static extern ChromaResult InitSDK([In] ChromaAppInfo pAppInfo);

            [DllImport(_sdkDll)]
            public static extern ChromaResult UnInit();

            [DllImport(_sdkDll)]
            public static extern ChromaResult CreateChromaLinkEffect(ChromaLinkEffectType effect, [In][MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ChromaEffectMarshaler))] IChromaLinkEffect pParam, out Guid pEffectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult CreateHeadsetEffect(HeadsetEffectType effect, [In][MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ChromaEffectMarshaler))] IHeadsetEffect pParam, out Guid pEffectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult CreateKeyboardEffect(KeyboardEffectType effect, [In][MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ChromaEffectMarshaler))] IKeyboardEffect pParam, out Guid pEffectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult CreateKeypadEffect(KeypadEffectType effect, [In][MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ChromaEffectMarshaler))] IKeypadEffect pParam, out Guid pEffectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult CreateMouseEffect(MouseEffectType effect, [In][MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ChromaEffectMarshaler))] IMouseEffect pParam, out Guid pEffectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult CreateMousepadEffect(MousepadEffectType effect, [In][MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ChromaEffectMarshaler))] IMousepadEffect pParam, out Guid pEffectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult DeleteEffect(Guid effectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult SetEffect(Guid effectId);

            [DllImport(_sdkDll)]
            public static extern ChromaResult RegisterEventNotification(IntPtr hWnd);

            [DllImport(_sdkDll)]
            public static extern ChromaResult UnregisterEventNotification();

            [DllImport(_sdkDll)]
            public static extern ChromaResult QueryDevice(Guid deviceId, [Out] ChromaDeviceInfo deviceInfo);

            private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
            {
                if (libraryName == _sdkDll)
                {
                    libraryName = SdkDllFilename;
                }

                _ = NativeLibrary.TryLoad(libraryName, assembly, searchPath, out IntPtr handle);
                return handle;
            }
        }
    }
}
