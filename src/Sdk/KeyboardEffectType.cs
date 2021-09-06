using System;
using System.Diagnostics.CodeAnalysis;

namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Specifies the type of a keyboard effect.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/namespace_chroma_s_d_k_1_1_keyboard.html">ChromaSDK::Keyboard Namespace Reference</seealso>
    public enum KeyboardEffectType
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

        /// <summary>Static single color effect.</summary>
        Static = 4,

        /// <summary>Spectrum cycling effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        SpectrumCycling = 5,

        /// <summary>Wave effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Wave = 6,

        /// <summary>Reserved.</summary>
        [SuppressMessage("Naming", "CA1700:Do not name enum values 'Reserved'", Justification = "Defined in the SDK")]
        [Obsolete("Reserved")]
        Reserved = 7,

        /// <summary>Custom effects with keys.</summary>
        CustomKey = 8,

        /// <summary>Custom effects with keys using 8x24 grid.</summary>
        Custom2 = 9,

        /// <summary>Invalid effect.</summary>
        Invalid = 10,
    }
}
