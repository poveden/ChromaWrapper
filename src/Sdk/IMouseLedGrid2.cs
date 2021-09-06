using ChromaWrapper.Mouse;

namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents a 2-dimensional grid of mice LEDs.
    /// </summary>
    public interface IMouseLedGrid2 : ILedGrid
    {
        /// <summary>
        /// Gets or sets the LED color at the specified index.
        /// </summary>
        /// <param name="index">The index of the LED.</param>
        /// <returns>The color of the LED.</returns>
        ChromaColor this[MouseLed2 index] { get; set; }
    }
}
