using System;
using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Mousepad
{
    /// <summary>
    /// Represents a custom effect for mousepads.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_mousepad_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::Mousepad::CUSTOM_EFFECT_TYPE</seealso>.
    public sealed class CustomMousepadEffect : IMousepadEffect, ILedArrayEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> array.
        /// </summary>
        public const int TotalLeds = 15;

        private readonly LedArray _array;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMousepadEffect"/> class.
        /// </summary>
        public CustomMousepadEffect()
        {
            _array = new LedArray(TotalLeds);
        }

        /// <summary>
        /// Gets the LED color array.
        /// </summary>
        /// <remarks>
        /// First LED starts from top-right corner. LED 0-4 right side, 5-9 bottom side, 10-14 left side.
        /// </remarks>
        public ILedArray Color => _array;

        /// <inheritdoc/>
        MousepadEffectType IMousepadEffect.EffectType => MousepadEffectType.Custom;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_array).Buffer;
    }
}
