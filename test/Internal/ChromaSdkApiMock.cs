using System;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Data;
using ChromaWrapper.Headset;
using ChromaWrapper.Internal;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Tests.Internal
{
    internal class ChromaSdkApiMock : IChromaSdkApi
    {
        public virtual bool IsSdkAvailable()
        {
            return true;
        }

        public virtual ChromaResult CreateChromaLinkEffect(ChromaLinkEffectType effect, IChromaLinkEffect pParam, out Guid pEffectId)
        {
            pEffectId = Guid.NewGuid();
            return ChromaResult.Success;
        }

        public virtual ChromaResult CreateHeadsetEffect(HeadsetEffectType effect, IHeadsetEffect pParam, out Guid pEffectId)
        {
            pEffectId = Guid.NewGuid();
            return ChromaResult.Success;
        }

        public virtual ChromaResult CreateKeyboardEffect(KeyboardEffectType effect, IKeyboardEffect pParam, out Guid pEffectId)
        {
            pEffectId = Guid.NewGuid();
            return ChromaResult.Success;
        }

        public virtual ChromaResult CreateKeypadEffect(KeypadEffectType effect, IKeypadEffect pParam, out Guid pEffectId)
        {
            pEffectId = Guid.NewGuid();
            return ChromaResult.Success;
        }

        public virtual ChromaResult CreateMouseEffect(MouseEffectType effect, IMouseEffect pParam, out Guid pEffectId)
        {
            pEffectId = Guid.NewGuid();
            return ChromaResult.Success;
        }

        public virtual ChromaResult CreateMousepadEffect(MousepadEffectType effect, IMousepadEffect pParam, out Guid pEffectId)
        {
            pEffectId = Guid.NewGuid();
            return ChromaResult.Success;
        }

        public virtual ChromaResult DeleteEffect(Guid effectId)
        {
            return ChromaResult.Success;
        }

        public virtual ChromaResult Init()
        {
            return ChromaResult.Success;
        }

        public virtual ChromaResult InitSDK(ChromaAppInfo pAppInfo)
        {
            return ChromaResult.Success;
        }

        public virtual ChromaResult QueryDevice(Guid deviceId, ChromaDeviceInfo deviceInfo)
        {
            if (deviceId == ChromaDeviceIds.Chromabox)
            {
                deviceInfo.DeviceType = ChromaDeviceInfo.HardwareType.System;
                deviceInfo.Connected = 0;
                return ChromaResult.DeviceNotConnected;
            }

            return ChromaResult.NotSupported;
        }

        public virtual ChromaResult RegisterEventNotification(IntPtr hWnd)
        {
            return ChromaResult.Success;
        }

        public virtual ChromaResult SetEffect(Guid effectId)
        {
            return ChromaResult.Success;
        }

        public virtual ChromaResult UnInit()
        {
            return ChromaResult.Success;
        }

        public virtual ChromaResult UnregisterEventNotification()
        {
            return ChromaResult.Success;
        }
    }
}
