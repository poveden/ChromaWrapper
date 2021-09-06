using System;

namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Specifies the type of a keypad effect.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/namespace_chroma_s_d_k_1_1_keypad.html">ChromaSDK::Keypad Namespace Reference</seealso>
    public enum KeypadEffectType
    {
        /// <summary>No effect.</summary>
        None = 0,

        /// <summary>Breathing effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Breathing = 1,

        /// <summary>Custom effect type.</summary>
        Custom = 2,

        /// <summary>Reactive effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Reactive = 3,

        /// <summary>Spectrum cycling effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        SpectrumCycling = 4,

        /// <summary>Static single color effect.</summary>
        Static = 5,

        /// <summary>Wave effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Wave = 6,

        /// <summary>Invalid effect.</summary>
        Invalid = 7,
    }
}
