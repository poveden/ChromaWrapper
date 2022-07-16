using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Keyboard
{
    /// <summary>
    /// Represents a custom effect for extended keyboards, addressable by named keys.
    /// Supports the <see href="https://developer.razer.com/works-with-chroma/razer-chroma-led-profiles/">Generic Super Keyboard LED Profile</see>.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// Colors set in the <see cref="Key"/> grid will take precedence over those set in the <see cref="Color"/> grid.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_keyboard_1_1v2_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::Keyboard::v2::CUSTOM_EFFECT_TYPE</seealso>.
    public sealed class CustomKeyboardEffect2 : IKeyboardEffect, ILedGridEffect, IKeyGridEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalColorLeds = TotalColorRows * TotalColorColumns;

        /// <summary>
        /// Gets the total number of LED rows in the <see cref="Color"/> array.
        /// </summary>
        public const int TotalColorRows = 8;

        /// <summary>
        /// Gets the total number of LED columns in the <see cref="Color"/> array.
        /// </summary>
        public const int TotalColorColumns = 24;

        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Key"/> LED grid.
        /// </summary>
        public const int TotalKeyLeds = LedKeyGrid.TotalKeyLeds;

        /// <summary>
        /// Gets the total number of LED rows in the <see cref="Key"/> array.
        /// </summary>
        public const int TotalKeyRows = LedKeyGrid.TotalKeyRows;

        /// <summary>
        /// Gets the total number of LED columns in the <see cref="Key"/> array.
        /// </summary>
        public const int TotalKeyColumns = LedKeyGrid.TotalKeyColumns;

        private readonly LedKeyGrid _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeyboardEffect2"/> class.
        /// </summary>
        public CustomKeyboardEffect2()
        {
            _grid = new LedKeyGrid(TotalColorRows, TotalColorColumns);
        }

        /// <inheritdoc/>
        public ILedGrid Color => _grid;

        /// <inheritdoc/>
        public IKeyGrid Key => _grid;

        /// <inheritdoc/>
        KeyboardEffectType IKeyboardEffect.EffectType => KeyboardEffectType.Custom2;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_grid).Buffer;
    }
}
