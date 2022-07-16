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
    /// <summary>
    /// Exposes the Razer Chroma SDK API.
    /// </summary>
    internal interface IChromaSdkApi
    {
        /// <summary>
        /// Gets a value indicating whether the Razer Chroma SDK is available.
        /// </summary>
        /// <returns><c>true</c> is the SDK is available; otherwise, <c>false</c>.</returns>
        bool IsSdkAvailable();

        /// <summary>
        /// Initialize Chroma SDK.
        /// </summary>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult Init();

        /// <summary>
        /// Initialize Chroma SDK.
        /// </summary>
        /// <param name="pAppInfo">Application information.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult InitSDK(ChromaAppInfo pAppInfo);

        /// <summary>
        /// UnInitialize Chroma SDK.
        /// </summary>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult UnInit();

        /// <summary>
        /// Create effects on Chroma Linked devices.
        /// </summary>
        /// <param name="effect">Chroma Link effect type.</param>
        /// <param name="pParam">Pointer to a parameter type specified by Effect.</param>
        /// <param name="pEffectId">Valid effect Id if successful. Set it to NULL if not required.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult CreateChromaLinkEffect(ChromaLinkEffectType effect, IChromaLinkEffect pParam, out Guid pEffectId);

        /// <summary>
        /// Create headset effect.
        /// </summary>
        /// <param name="effect">Headset effect type.</param>
        /// <param name="pParam">Pointer to a parameter type specified by Effect.</param>
        /// <param name="pEffectId">Valid effect Id if successful. Set it to NULL if not required.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult CreateHeadsetEffect(HeadsetEffectType effect, IHeadsetEffect pParam, out Guid pEffectId);

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effect">Keyboard effect type.</param>
        /// <param name="pParam">Pointer to a parameter type specified by Effect.</param>
        /// <param name="pEffectId">Valid effect Id if successful. Set it to NULL if not required.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult CreateKeyboardEffect(KeyboardEffectType effect, IKeyboardEffect pParam, out Guid pEffectId);

        /// <summary>
        /// Create keypad effect.
        /// </summary>
        /// <param name="effect">Keypad effect type.</param>
        /// <param name="pParam">Pointer to a parameter type specified by Effect.</param>
        /// <param name="pEffectId">Valid effect Id if successful. Set it to NULL if not required.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult CreateKeypadEffect(KeypadEffectType effect, IKeypadEffect pParam, out Guid pEffectId);

        /// <summary>
        /// Create mouse effect.
        /// </summary>
        /// <param name="effect">Mouse effect type.</param>
        /// <param name="pParam">Pointer to a parameter type specified by Effect.</param>
        /// <param name="pEffectId">Valid effect Id if successful. Set it to NULL if not required.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult CreateMouseEffect(MouseEffectType effect, IMouseEffect pParam, out Guid pEffectId);

        /// <summary>
        /// Create mousepad effect.
        /// </summary>
        /// <param name="effect">Mousemat effect type.</param>
        /// <param name="pParam">Pointer to a parameter type specified by Effect.</param>
        /// <param name="pEffectId">Valid effect Id if successful. Set it to NULL if not required.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult CreateMousepadEffect(MousepadEffectType effect, IMousepadEffect pParam, out Guid pEffectId);

        /// <summary>
        /// Delete effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be deleted.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult DeleteEffect(Guid effectId);

        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be set.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult SetEffect(Guid effectId);

        /// <summary>
        /// Register for event notification.
        /// </summary>
        /// <param name="hWnd">Application window handle.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult RegisterEventNotification(IntPtr hWnd);

        /// <summary>
        /// Un-register for event notification.
        /// </summary>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult UnregisterEventNotification();

        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="deviceId">Device ID.</param>
        /// <param name="deviceInfo">Contains device information specified by <paramref name="deviceId"/>.</param>
        /// <returns>The result code.</returns>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/_rz_chroma_s_d_k_8h.html">Razer Chroma SDK v3.3: RzChromaSDK.h File Reference</seealso>
        ChromaResult QueryDevice(Guid deviceId, ChromaDeviceInfo deviceInfo);
    }
}
