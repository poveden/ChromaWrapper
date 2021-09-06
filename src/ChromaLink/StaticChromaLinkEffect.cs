﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.ChromaLink
{
    /// <summary>
    /// Represents a static single color effect for Chroma Link devices.
    /// </summary>
    /// <remarks>
    /// Use it to create effects for all LEDs.
    /// </remarks>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_chroma_link_1_1_s_t_a_t_i_c___e_f_f_e_c_t___t_y_p_e.html">ChromaSDK::ChromaLink::STATIC_EFFECT_TYPE</seealso>.
    [SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Interop marshaling")]
    [StructLayout(LayoutKind.Sequential)]
    public sealed class StaticChromaLinkEffect : IChromaLinkEffect, IStaticEffect
    {
        private ChromaColor _color;

        /// <inheritdoc/>
        [SuppressMessage("Minor Code Smell", "S2292:Trivial properties should be auto-implemented", Justification = "Interop marshaling")]
        public ChromaColor Color
        {
            get => _color;
            set => _color = value;
        }

        /// <inheritdoc/>
        ChromaLinkEffectType IChromaLinkEffect.EffectType => ChromaLinkEffectType.Static;
    }
}
