using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Keyboard
{
    /// <summary>
    /// Represents a custom effect for keyboards.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_keyboard_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::Keyboard::CUSTOM_EFFECT_TYPE</seealso>.
    public sealed class CustomKeyboardEffect : IKeyboardEffect, ILedGridEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalLeds = TotalRows * TotalColumns;

        /// <summary>
        /// Gets the total number of rows in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalRows = 6;

        /// <summary>
        /// Gets the total number of columns in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalColumns = 22;

        private readonly LedGrid _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeyboardEffect"/> class.
        /// </summary>
        public CustomKeyboardEffect()
        {
            _grid = new LedGrid(TotalRows, TotalColumns);
        }

        /// <inheritdoc/>
        public ILedGrid Color => _grid;

        /// <inheritdoc/>
        KeyboardEffectType IKeyboardEffect.EffectType => KeyboardEffectType.Custom;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_grid).Buffer;
    }
}
