using ChromaWrapper.Sdk;

namespace ChromaWrapper.Keyboard
{
    /// <summary>
    /// Marks a class as a keyboard effect.
    /// </summary>
    public interface IKeyboardEffect
    {
        /// <summary>
        /// Gets the keyboard effect type.
        /// </summary>
        KeyboardEffectType EffectType { get; }
    }
}
