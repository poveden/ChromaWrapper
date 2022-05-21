namespace ChromaWrapper.Internal
{
    /// <summary>
    /// Represents a color buffer, used to store LED and key layouts in custom effects.
    /// </summary>
    internal interface IColorBuffer
    {
        /// <summary>
        /// Gets the color buffer.
        /// </summary>
        Array Buffer { get; }
    }
}
