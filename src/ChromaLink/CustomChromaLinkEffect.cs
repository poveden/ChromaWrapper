using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.ChromaLink
{
    /// <summary>
    /// Represents a custom effect for Chroma Link devices.
    /// Supports the <see href="https://developer.razer.com/works-with-chroma/chroma-link-guide/">Chroma Link Virtual LED Pattern</see>.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_chroma_link_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::ChromaLink::CUSTOM_EFFECT_TYPE</seealso>.
    public sealed class CustomChromaLinkEffect : IChromaLinkEffect, ILedArrayEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> array.
        /// </summary>
        public const int TotalLeds = 5;

        private readonly LedArray _array;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomChromaLinkEffect"/> class.
        /// </summary>
        public CustomChromaLinkEffect()
        {
            _array = new LedArray(TotalLeds);
        }

        /// <inheritdoc/>
        public ILedArray Color => _array;

        /// <inheritdoc/>
        ChromaLinkEffectType IChromaLinkEffect.EffectType => ChromaLinkEffectType.Custom;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_array).Buffer;
    }
}
