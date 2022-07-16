namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Specifies the type of a mousepad effect.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/namespace_chroma_s_d_k_1_1_mousepad.html">ChromaSDK::Mousepad Namespace Reference</seealso>
    public enum MousepadEffectType
    {
        /// <summary>No effect.</summary>
        None = 0,

        /// <summary>Breathing effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Breathing = 1,

        /// <summary>Custom effect type.</summary>
        Custom = 2,

        /// <summary>Spectrum cycling effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        SpectrumCycling = 3,

        /// <summary>Static single color effect.</summary>
        Static = 4,

        /// <summary>Wave effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Wave = 5,

        /// <summary>Custom effect with 20 virtual LEDs. First element starts from top-right and it goes clockwise full circle with 5 LEDs on each side.</summary>
        Custom2 = 6,

        /// <summary>Invalid effect.</summary>
        Invalid = 7,
    }
}
