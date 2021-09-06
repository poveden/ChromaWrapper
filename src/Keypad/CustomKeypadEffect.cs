using System;
using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Keypad
{
    /// <summary>
    /// Represents a custom effect for keypads.
    /// Supports the <see href="https://developer.razer.com/works-with-chroma/razer-chroma-led-profiles/">Generic Super Keypad LED Profile</see>.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_keypad_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::Keypad::CUSTOM_EFFECT_TYPE</seealso>.
    public sealed class CustomKeypadEffect : IKeypadEffect, ILedGridEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalLeds = TotalRows * TotalColumns;

        /// <summary>
        /// Gets the total number of rows in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalRows = 4;

        /// <summary>
        /// Gets the total number of columns in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalColumns = 5;

        private readonly LedGrid _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeypadEffect"/> class.
        /// </summary>
        public CustomKeypadEffect()
        {
            _grid = new LedGrid(TotalRows, TotalColumns);
        }

        /// <inheritdoc/>
        public ILedGrid Color => _grid;

        /// <inheritdoc/>
        KeypadEffectType IKeypadEffect.EffectType => KeypadEffectType.Custom;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_grid).Buffer;
    }
}
