namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents a static single color effect.
    /// </summary>
    public interface IStaticEffect
    {
        /// <summary>
        /// Gets or sets the color for all LEDs.
        /// </summary>
        ChromaColor Color { get; set; }
    }
}
