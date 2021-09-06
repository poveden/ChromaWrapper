using ChromaWrapper.Sdk;

namespace ChromaWrapper.Mouse
{
    /// <summary>
    /// Marks a class as a mouse effect.
    /// </summary>
    public interface IMouseEffect
    {
        /// <summary>
        /// Gets the mouse effect type.
        /// </summary>
        MouseEffectType EffectType { get; }
    }
}
