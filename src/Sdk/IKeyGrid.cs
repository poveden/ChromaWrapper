using ChromaWrapper.Keyboard;

namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents a 2-dimensional grid of keyboard LEDs. Implemented by <see cref="IKeyGridEffect"/>.<see cref="IKeyGridEffect.Key"/>.
    /// </summary>
    public interface IKeyGrid
    {
        /// <summary>
        /// Gets the number of rows of the grid.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Gets the number of columns of the grid.
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Gets the number of LEDs in the grid.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets or sets the LED color at the specified row and column.
        /// </summary>
        /// <param name="row">The LED grid row.</param>
        /// <param name="column">The LED grid column.</param>
        /// <returns>The color of the LED, or <see cref="ChromaColor.Transparent"/> if no color has been set.</returns>
        ChromaKeyColor this[int row, int column] { get; set; }

        /// <summary>
        /// Gets or sets the LED color at the specified index.
        /// </summary>
        /// <param name="index">The index of the LED.</param>
        /// <returns>The color of the LED, or <see cref="ChromaColor.Transparent"/> if no color has been set.</returns>
        ChromaKeyColor this[int index] { get; set; }

        /// <summary>
        /// Gets an editable subset of the LED colors in the specified range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns>The subset of LED colors.</returns>
        Span<ChromaKeyColor> this[Range range] { get; }

        /// <summary>
        /// Gets or sets the LED color of the specified key.
        /// </summary>
        /// <param name="key">The keyboard key.</param>
        /// <returns>The color of the LED, or <see cref="ChromaColor.Transparent"/> if no color has been set.</returns>
        ChromaKeyColor this[KeyboardKey key] { get; set; }

        /// <summary>
        /// Sets all LEDs to their default value (no color set).
        /// </summary>
        void Clear();

        /// <summary>
        /// Sets all LEDs to the specified color, or clears them.
        /// </summary>
        /// <param name="color">To color to set all LEDs to, or <see cref="ChromaColor.Transparent"/> to clear all LEDs.</param>
        void Fill(ChromaKeyColor color);
    }
}
