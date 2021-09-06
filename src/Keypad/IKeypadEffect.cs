using ChromaWrapper.Sdk;

namespace ChromaWrapper.Keypad
{
    /// <summary>
    /// Marks a class as a keypad effect.
    /// </summary>
    public interface IKeypadEffect
    {
        /// <summary>
        /// Gets the keypad effect type.
        /// </summary>
        KeypadEffectType EffectType { get; }
    }
}
