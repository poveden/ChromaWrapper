namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents a 2-dimensional grid of LEDs. Implemented by <see cref="ILedGridEffect"/>.<see cref="ILedGridEffect.Color"/>.
    /// </summary>
    public interface ILedGrid : ILedArray
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
        /// Gets or sets the LED color at the specified row and column.
        /// </summary>
        /// <param name="row">The LED grid row.</param>
        /// <param name="column">The LED grid column.</param>
        /// <returns>The color of the LED.</returns>
        ChromaColor this[int row, int column] { get; set; }
    }
}
