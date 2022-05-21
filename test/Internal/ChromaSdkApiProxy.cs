using ChromaWrapper.ChromaLink;
using ChromaWrapper.Data;
using ChromaWrapper.Headset;
using ChromaWrapper.Internal;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using ChromaWrapper.Sdk;
using static ChromaWrapper.Internal.NativeMethods;

namespace ChromaWrapper.Tests.Internal
{
    // Since ChromaSdk is sealed, we wrap it so we can setup mocks and verify calls.
    internal class ChromaSdkApiProxy : IChromaSdkApi
    {
        public virtual ChromaResult CreateChromaLinkEffect(ChromaLinkEffectType effect, IChromaLinkEffect pParam, out Guid pEffectId)
        {
            return Instance.CreateChromaLinkEffect(effect, pParam, out pEffectId);
        }

        public virtual ChromaResult CreateHeadsetEffect(HeadsetEffectType effect, IHeadsetEffect pParam, out Guid pEffectId)
        {
            return Instance.CreateHeadsetEffect(effect, pParam, out pEffectId);
        }

        public virtual ChromaResult CreateKeyboardEffect(KeyboardEffectType effect, IKeyboardEffect pParam, out Guid pEffectId)
        {
            return Instance.CreateKeyboardEffect(effect, pParam, out pEffectId);
        }

        public virtual ChromaResult CreateKeypadEffect(KeypadEffectType effect, IKeypadEffect pParam, out Guid pEffectId)
        {
            return Instance.CreateKeypadEffect(effect, pParam, out pEffectId);
        }

        public virtual ChromaResult CreateMouseEffect(MouseEffectType effect, IMouseEffect pParam, out Guid pEffectId)
        {
            return Instance.CreateMouseEffect(effect, pParam, out pEffectId);
        }

        public virtual ChromaResult CreateMousepadEffect(MousepadEffectType effect, IMousepadEffect pParam, out Guid pEffectId)
        {
            return Instance.CreateMousepadEffect(effect, pParam, out pEffectId);
        }

        public virtual ChromaResult DeleteEffect(Guid effectId)
        {
            return Instance.DeleteEffect(effectId);
        }

        public virtual ChromaResult Init()
        {
            return Instance.Init();
        }

        public virtual ChromaResult InitSDK(ChromaAppInfo pAppInfo)
        {
            return Instance.InitSDK(pAppInfo);
        }

        public virtual bool IsSdkAvailable()
        {
            return Instance.IsSdkAvailable();
        }

        public virtual ChromaResult QueryDevice(Guid deviceId, ChromaDeviceInfo deviceInfo)
        {
            return Instance.QueryDevice(deviceId, deviceInfo);
        }

        public virtual ChromaResult RegisterEventNotification(IntPtr hWnd)
        {
            return Instance.RegisterEventNotification(hWnd);
        }

        public virtual ChromaResult SetEffect(Guid effectId)
        {
            return Instance.SetEffect(effectId);
        }

        public virtual ChromaResult UnInit()
        {
            return Instance.UnInit();
        }

        public virtual ChromaResult UnregisterEventNotification()
        {
            return Instance.UnregisterEventNotification();
        }
    }
}
