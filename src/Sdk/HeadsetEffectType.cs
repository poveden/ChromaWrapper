namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Specifies the type of a headset effect.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/namespace_chroma_s_d_k_1_1_headset.html">ChromaSDK::Headset Namespace Reference</seealso>
    public enum HeadsetEffectType
    {
        /// <summary>No effect.</summary>
        None = 0,

        /// <summary>Static single color effect.</summary>
        Static = 1,

        /// <summary>Breathing effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        Breathing = 2,

        /// <summary>Spectrum cycling effect.</summary>
        [Obsolete("Deprecated and should not be used")]
        SpectrumCycling = 3,

        /// <summary>Custom effect type.</summary>
        Custom = 4,

        /// <summary>Invalid effect.</summary>
        Invalid = 5,
    }
}
