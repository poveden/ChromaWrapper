using ChromaWrapper.Sdk;

namespace ChromaWrapper.ChromaLink
{
    /// <summary>
    /// Marks a class as a Chroma Link effect.
    /// </summary>
    public interface IChromaLinkEffect
    {
        /// <summary>
        /// Gets the Chroma Link effect type.
        /// </summary>
        ChromaLinkEffectType EffectType { get; }
    }
}
