using System;

namespace ChromaWrapper.Data
{
    /// <summary>
    /// Specifies Chroma device IDs.
    /// </summary>
    public static class ChromaDeviceIds
    {
        // Keyboards

        /// <summary>Razer Blackwidow Chroma.</summary>
        public static readonly Guid BlackwidowChroma = unchecked(new Guid(0x2ea1bb63, (short)0xca28, 0x428d, new byte[] { 0x9f, 0x06, 0x19, 0x6b, 0x88, 0x33, 0x0b, 0xbb }));

        /// <summary>Razer Blackwidow Chroma Tournament Edition.</summary>
        public static readonly Guid BlackwidowChromaTE = unchecked(new Guid((int)0xed1c1b82, (short)0xbfbe, 0x418f, new byte[] { 0xb4, 0x9d, 0xd0, 0x3f, 0x5, 0xb1, 0x49, 0xdf }));

        /// <summary>Razer Deathstalker.</summary>
        public static readonly Guid DeathstalkerChroma = unchecked(new Guid(0x18c5ad9b, 0x4326, 0x4828, new byte[] { 0x92, 0xc4, 0x26, 0x69, 0xa6, 0x6d, 0x22, 0x83 }));

        /// <summary>Overwatch Keyboard.</summary>
        public static readonly Guid OverwatchKeyboard = unchecked(new Guid((int)0x872ab2a9, 0x7959, 0x4478, new byte[] { 0x9f, 0xed, 0x15, 0xf6, 0x18, 0x6e, 0x72, 0xe4 }));

        /// <summary>Razer Blackwidow X Chroma.</summary>
        public static readonly Guid BlackwidowXChroma = unchecked(new Guid(0x5af60076, (short)0xade9, 0x43d4, new byte[] { 0xb5, 0x74, 0x52, 0x59, 0x92, 0x93, 0xb5, 0x54 }));

        /// <summary>Razer Blackwidow X TE Chroma.</summary>
        public static readonly Guid BlackwidowXTEChroma = unchecked(new Guid(0x2d84dd51, 0x3290, 0x4aac, new byte[] { 0x9a, 0x89, 0xd8, 0xaf, 0xde, 0x38, 0xb5, 0x7c }));

        /// <summary>Razer Ornata Chroma.</summary>
        public static readonly Guid OrnataChroma = unchecked(new Guid((int)0x803378c1, (short)0xcc48, 0x4970, new byte[] { 0x85, 0x39, 0xd8, 0x28, 0xcc, 0x1d, 0x42, 0xa }));

        /// <summary>Razer Blade Stealth.</summary>
        public static readonly Guid BladeStealth = unchecked(new Guid((int)0xc83bdfe8, (short)0xe7fc, 0x40e0, new byte[] { 0x99, 0xdb, 0x87, 0x2e, 0x23, 0xf1, 0x98, 0x91 }));

        /// <summary>Razer Blade.</summary>
        public static readonly Guid Blade = unchecked(new Guid((int)0xf2bedfaf, (short)0xa0fe, 0x4651, new byte[] { 0x9d, 0x41, 0xb6, 0xce, 0x60, 0x3a, 0x3d, 0xdd }));

        /// <summary>Razer Blade Pro.</summary>
        public static readonly Guid BladePro = unchecked(new Guid((int)0xa73ac338, (short)0xf0e5, 0x4bf7, new byte[] { 0x91, 0xae, 0xdd, 0x1f, 0x7e, 0x17, 0x37, 0xa5 }));

        /// <summary>Razer Huntsman.</summary>
        public static readonly Guid Huntsman = unchecked(new Guid((int)0xf85e7473, (short)0x8f03, 0x45b6, new byte[] { 0xa1, 0x6e, 0xce, 0x26, 0xcb, 0x8d, 0x24, 0x41 }));

        /// <summary>Razer Blackwidow Elite.</summary>
        public static readonly Guid BlackwidowElite = unchecked(new Guid(0x16bb5abd, (short)0xc1cd, 0x4cb3, new byte[] { 0xbd, 0xf7, 0x62, 0x43, 0x87, 0x48, 0xbd, 0x98 }));

        // Mice

        /// <summary>Razer Deathadder Chroma.</summary>
        public static readonly Guid DeathadderChroma = unchecked(new Guid((int)0xaec50d91, (short)0xb1f1, 0x452f, new byte[] { 0x8e, 0x16, 0x7b, 0x73, 0xf3, 0x76, 0xfd, 0xf3 }));

        /// <summary>Razer Mamba Chroma Tournament Edition.</summary>
        public static readonly Guid MambaChromaTE = unchecked(new Guid(0x7ec00450, (short)0xe0ee, 0x4289, new byte[] { 0x89, 0xd5, 0xd, 0x87, 0x9c, 0x19, 0x6, 0x1a }));

        /// <summary>Razer Diamondback.</summary>
        public static readonly Guid DiamondbackChroma = unchecked(new Guid((int)0xff8a5929, 0x4512, 0x4257, new byte[] { 0x8d, 0x59, 0xc6, 0x47, 0xbf, 0x99, 0x35, 0xd0 }));

        /// <summary>Razer Mamba.</summary>
        public static readonly Guid MambaChroma = unchecked(new Guid((int)0xd527cbdc, (short)0xeb0a, 0x483a, new byte[] { 0x9e, 0x89, 0x66, 0xd5, 0x4, 0x63, 0xec, 0x6c }));

        /// <summary>Razer Naga Epic.</summary>
        public static readonly Guid NagaEpicChroma = unchecked(new Guid((int)0xd714c50b, 0x7158, 0x4368, new byte[] { 0xb9, 0x9c, 0x60, 0x1a, 0xcb, 0x98, 0x5e, 0x98 }));

        /// <summary>Razer Naga.</summary>
        public static readonly Guid NagaChroma = unchecked(new Guid((int)0xf1876328, 0x6ca4, 0x46ae, new byte[] { 0xbe, 0x4, 0xbe, 0x81, 0x2b, 0x41, 0x44, 0x33 }));

        /// <summary>Razer Orochi Chroma.</summary>
        public static readonly Guid OrochiChroma = unchecked(new Guid(0x52c15681, 0x4ece, 0x4dd9, new byte[] { 0x8a, 0x52, 0xa1, 0x41, 0x84, 0x59, 0xeb, 0x34 }));

        /// <summary>Razer Naga Hex Chroma.</summary>
        public static readonly Guid NagaHexChroma = unchecked(new Guid(0x195d70f5, (short)0xf285, 0x4cff, new byte[] { 0x99, 0xf2, 0xb8, 0xc0, 0xe9, 0x65, 0x8d, 0xb4 }));

        /// <summary>Razer DeathAdder Elite Chroma.</summary>
        public static readonly Guid DeathadderEliteChroma = unchecked(new Guid(0x77834867, 0x3237, 0x4a9f, new byte[] { 0xad, 0x77, 0x4a, 0x46, 0xc4, 0x18, 0x30, 0x3 }));

        // Headsets

        /// <summary>Razer Kraken 7.1 Chroma.</summary>
        public static readonly Guid Kraken71Chroma = unchecked(new Guid((int)0xcd1e09a5, (short)0xd5e6, 0x4a6c, new byte[] { 0xa9, 0x3b, 0xe6, 0xd9, 0xbf, 0x1d, 0x20, 0x92 }));

        /// <summary>Razer ManO'War.</summary>
        public static readonly Guid ManOWarChroma = unchecked(new Guid((int)0xdf3164d7, 0x5408, 0x4a0e, new byte[] { 0x8a, 0x7f, 0xa7, 0x41, 0x2f, 0x26, 0xbe, 0xbf }));

        /// <summary>Razer Kraken 7.1 Chroma Refresh.</summary>
        public static readonly Guid Kraken71RefreshChroma = unchecked(new Guid(0x7fb8a36e, (short)0x9e74, 0x4bb3, new byte[] { 0x8c, 0x86, 0xca, 0xc7, 0xf7, 0x89, 0x1e, 0xbd }));

        /// <summary>Razer Kraken Kitty Edition.</summary>
        public static readonly Guid KrakenKitty = unchecked(new Guid((int)0xfb357780, 0x4617, 0x43a7, new byte[] { 0x96, 0xf, 0xd1, 0x19, 0xe, 0xd5, 0x48, 0x6 }));

        // Mouse mat

        /// <summary>Razer Firefly.</summary>
        public static readonly Guid FireflyChroma = unchecked(new Guid((int)0x80f95a94, 0x73d2, 0x48ca, new byte[] { 0xae, 0x9a, 0x9, 0x86, 0x78, 0x9a, 0x9a, 0xf2 }));

        // Keypads

        /// <summary>Razer Tartarus.</summary>
        public static readonly Guid TartarusChroma = unchecked(new Guid(0xf0545c, (short)0xe180, 0x4ad1, new byte[] { 0x8e, 0x8a, 0x41, 0x90, 0x61, 0xce, 0x50, 0x5e }));

        /// <summary>Razer Orbweaver.</summary>
        public static readonly Guid OrbweaverChroma = unchecked(new Guid((int)0x9d24b0ab, 0x162, 0x466c, new byte[] { 0x96, 0x40, 0x7a, 0x92, 0x4a, 0xa4, 0xd9, 0xfd }));

        // Chroma Linked devices

        /// <summary>Lenovo Y900.</summary>
        public static readonly Guid LenovoY900 = unchecked(new Guid(0x35f6f18d, 0x1ae5, 0x436c, new byte[] { 0xa5, 0x75, 0xab, 0x44, 0xa1, 0x27, 0x90, 0x3a }));

        /// <summary>Lenovo Y27.</summary>
        public static readonly Guid LenovoY27 = unchecked(new Guid(0x47db1fa7, 0x6b9b, 0x4ee6, new byte[] { 0xb6, 0xf4, 0x40, 0x71, 0xa3, 0xb2, 0x5, 0x3b }));

        /// <summary>Razer Core Chroma.</summary>
        public static readonly Guid CoreChroma = unchecked(new Guid(0x201203b, 0x62f3, 0x4c50, new byte[] { 0x83, 0xdd, 0x59, 0x8b, 0xab, 0xd2, 0x8, 0xe0 }));

        /// <summary>Chromabox.</summary>
        public static readonly Guid Chromabox = unchecked(new Guid((int)0xbb2e9c9b, (short)0xb0d2, 0x461a, new byte[] { 0xba, 0x52, 0x23, 0xb, 0x5d, 0x6c, 0x36, 0x9 }));

        // Speakers

        /// <summary>Razer Nommo.</summary>
        public static readonly Guid NommoChroma = unchecked(new Guid(0x45b308f2, (short)0xcd44, 0x4594, new byte[] { 0x83, 0x75, 0x4d, 0x59, 0x45, 0xad, 0x88, 0xe }));

        /// <summary>Razer Nommo Pro.</summary>
        public static readonly Guid NommoChromaPro = unchecked(new Guid(0x3017280b, (short)0xd7f9, 0x4d7b, new byte[] { 0x93, 0xe, 0x7b, 0x47, 0x18, 0x1b, 0x46, 0xb5 }));
    }
}
