using System;
using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Keyboard
{
    /// <summary>
    /// Represents a custom effect for keyboards, addressable by named keys.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// Colors set in the <see cref="Key"/> grid will take precedence over those set in the <see cref="Color"/> grid.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_keyboard_1_1_c_u_s_t_o_m___k_e_y___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::Keyboard::CUSTOM_KEY_EFFECT_TYPE</seealso>.
    public sealed class CustomKeyKeyboardEffect : IKeyboardEffect, ILedGridEffect, IKeyGridEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> and <see cref="Key"/> LED grid.
        /// </summary>
        public const int TotalLeds = TotalRows * TotalColumns;

        /// <summary>
        /// Gets the total number of rows in the <see cref="Color"/> and <see cref="Key"/> LED grids.
        /// </summary>
        public const int TotalRows = 6;

        /// <summary>
        /// Gets the total number of columns in the <see cref="Color"/> and <see cref="Key"/> LED grids.
        /// </summary>
        public const int TotalColumns = 22;

        private readonly LedKeyGrid _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeyKeyboardEffect"/> class.
        /// </summary>
        public CustomKeyKeyboardEffect()
        {
            _grid = new LedKeyGrid(TotalRows, TotalColumns);
        }

        /// <inheritdoc/>
        public ILedGrid Color => _grid;

        /// <inheritdoc/>
        public IKeyGrid Key => _grid;

        /// <inheritdoc/>
        KeyboardEffectType IKeyboardEffect.EffectType => KeyboardEffectType.CustomKey;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_grid).Buffer;
    }
}
