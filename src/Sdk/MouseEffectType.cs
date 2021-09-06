using System;

namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Specifies the type of a mouse effect.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/namespace_chroma_s_d_k_1_1_mouse.html">ChromaSDK::Mouse Namespace Reference</seealso>
    public enum MouseEffectType
    {
        /// <summary>No effect.</summary>
        None = 0,

        /// <summary>Blinking effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Blinking = 1,

        /// <summary>Breathing effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Breathing = 2,

        /// <summary>Custom effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Custom = 3,

        /// <summary>Reactive effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Reactive = 4,

        /// <summary>Spectrum cycling effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        SpectrumCycling = 5,

        /// <summary>Static single color effect.</summary>
        Static = 6,

        /// <summary>Wave effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Wave = 7,

        /// <summary>Custom effects using a virtual grid.</summary>
        Custom2 = 8,

        /// <summary>Invalid effect.</summary>
        Invalid = 9,
    }
}
