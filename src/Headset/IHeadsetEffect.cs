using ChromaWrapper.Sdk;

namespace ChromaWrapper.Headset
{
    /// <summary>
    /// Marks a class as a headset effect.
    /// </summary>
    public interface IHeadsetEffect
    {
        /// <summary>
        /// Gets the headset effect type.
        /// </summary>
        HeadsetEffectType EffectType { get; }
    }
}
