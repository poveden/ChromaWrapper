using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Mouse
{
    /// <summary>
    /// Represents a custom effect for mice.
    /// Supports the <see href="https://developer.razer.com/works-with-chroma/razer-chroma-led-profiles/">Generic Super Mouse LED Profile</see>.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for individual LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_mouse_1_1_c_u_s_t_o_m___e_f_f_e_c_t___t_y_p_e2.html">ChromaSDK::Mouse::CUSTOM_EFFECT_TYPE2</seealso>.
    public sealed class CustomMouseEffect2 : IMouseEffect, ILedGridEffect, IColorBuffer
    {
        /// <summary>
        /// Gets the total number of LEDs in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalLeds = TotalRows * TotalColumns;

        /// <summary>
        /// Gets the total number of rows in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalRows = 9;

        /// <summary>
        /// Gets the total number of columns in the <see cref="Color"/> LED grid.
        /// </summary>
        public const int TotalColumns = 7;

        private readonly Grid _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMouseEffect2"/> class.
        /// </summary>
        public CustomMouseEffect2()
        {
            _grid = new Grid();
        }

        /// <summary>
        /// Gets the LED color grid.
        /// </summary>
        public IMouseLedGrid2 Color => _grid;

        /// <inheritdoc/>
        ILedGrid ILedGridEffect.Color => _grid;

        /// <inheritdoc/>
        MouseEffectType IMouseEffect.EffectType => MouseEffectType.Custom2;

        /// <inheritdoc/>
        Array IColorBuffer.Buffer => ((IColorBuffer)_grid).Buffer;

        private sealed class Grid : LedGrid, IMouseLedGrid2
        {
            public Grid()
                : base(TotalRows, TotalColumns)
            {
            }

            public ChromaColor this[MouseLed2 led]
            {
                get => this[RowOf(led), ColumnOf(led)];
                set => this[RowOf(led), ColumnOf(led)] = value;
            }

            private static int RowOf(MouseLed2 led)
            {
                return ((int)led >> 8) & 0xFF;
            }

            private static int ColumnOf(MouseLed2 led)
            {
                return (int)led & 0xFF;
            }
        }
    }
}
