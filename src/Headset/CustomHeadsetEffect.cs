using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Headset
{
    /// <summary>
    /// Represents a custom effect for headsets.
    /// Supports the <see href="https://developer.razer.com/works-with-chroma/razer-chroma-led-profiles/">Generic Super Headset LED Profile</see>.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_headset_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::Headset::CUSTOM_EFFECT_TYPE</seealso>.
    public sealed class CustomHeadsetEffect : IHeadsetEffect, ILedArrayEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> array.
        /// </summary>
        public const int TotalLeds = 5;

        private readonly LedArray _array;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeadsetEffect"/> class.
        /// </summary>
        public CustomHeadsetEffect()
        {
            _array = new LedArray(TotalLeds);
        }

        /// <inheritdoc/>
        public ILedArray Color => _array;

        /// <inheritdoc/>
        HeadsetEffectType IHeadsetEffect.EffectType => HeadsetEffectType.Custom;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_array).Buffer;
    }
}
