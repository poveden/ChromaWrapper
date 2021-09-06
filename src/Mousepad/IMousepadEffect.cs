using ChromaWrapper.Sdk;

namespace ChromaWrapper.Mousepad
{
    /// <summary>
    /// Marks a class as a mousepad effect.
    /// </summary>
    public interface IMousepadEffect
    {
        /// <summary>
        /// Gets the mousepad effect type.
        /// </summary>
        MousepadEffectType EffectType { get; }
    }
}
