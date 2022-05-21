namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents a collection of LEDs that can be addressed by index. Implemented by <see cref="ILedArrayEffect"/>.<see cref="ILedArrayEffect.Color"/>.
    /// </summary>
    public interface ILedArray
    {
        /// <summary>
        /// Gets the number of LEDs in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets or sets the LED color at the specified index.
        /// </summary>
        /// <param name="index">The index of the LED.</param>
        /// <returns>The color of the LED.</returns>
        ChromaColor this[int index] { get; set; }

        /// <summary>
        /// Gets an editable subset of the LED colors in the specified range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns>The subset of LED colors.</returns>
        Span<ChromaColor> this[Range range] { get; }

        /// <summary>
        /// Sets all LEDs to their default value (black color).
        /// </summary>
        void Clear();

        /// <summary>
        /// Sets all LEDs to the specified color.
        /// </summary>
        /// <param name="color">To color to set all LEDs to.</param>
        void Fill(ChromaColor color);
    }
}
