using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Mousepad
{
    /// <summary>
    /// Represents a custom effect for extended mousepads with 20 LEDs.
    /// Supports the <see href="https://developer.razer.com/works-with-chroma/razer-chroma-led-profiles/">Generic Super Ring Pattern LED Profile</see>.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_mousepad_1_1v2_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::Mousepad::v2::CUSTOM_EFFECT_TYPE</seealso>.
    public sealed class CustomMousepadEffect2 : IMousepadEffect, ILedArrayEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> array.
        /// </summary>
        public const int TotalLeds = 20;

        private readonly LedArray _array;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMousepadEffect2"/> class.
        /// </summary>
        public CustomMousepadEffect2()
        {
            _array = new LedArray(TotalLeds);
        }

        /// <summary>
        /// Gets the LED color array.
        /// </summary>
        /// <remarks>
        /// First element starts from top-right and it goes clockwise full circle with 5 LEDs on each side.
        /// </remarks>
        public ILedArray Color => _array;

        /// <inheritdoc/>
        MousepadEffectType IMousepadEffect.EffectType => MousepadEffectType.Custom2;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_array).Buffer;
    }
}
