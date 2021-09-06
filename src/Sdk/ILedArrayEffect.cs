namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents an effect with a LED array.
    /// </summary>
    public interface ILedArrayEffect
    {
        /// <summary>
        /// Gets the LED color array.
        /// </summary>
        ILedArray Color { get; }
    }
}
