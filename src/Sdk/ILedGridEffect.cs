namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents an effect with a LED array.
    /// </summary>
    public interface ILedGridEffect
    {
        /// <summary>
        /// Gets the LED color grid.
        /// </summary>
        ILedGrid Color { get; }
    }
}
