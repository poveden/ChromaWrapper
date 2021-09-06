namespace ChromaWrapper
{
    /// <content>
    /// Defines common colors.
    /// </content>
    public readonly partial struct ChromaColor
    {
        /// <summary>Gets a color that has an RGB value of #000000.</summary>
        public static readonly ChromaColor Black;

        /// <summary>Gets a color that has an RGB value of #FF0000.</summary>
        public static readonly ChromaColor Red = FromRgb(0xFF0000);

        /// <summary>Gets a color that has an RGB value of #00FF00.</summary>
        public static readonly ChromaColor Green = FromRgb(0x00FF00);

        /// <summary>Gets a color that has an RGB value of #0000FF.</summary>
        public static readonly ChromaColor Blue = FromRgb(0x0000FF);

        /// <summary>Gets a color that has an RGB value of #FFFF00.</summary>
        public static readonly ChromaColor Yellow = FromRgb(0xFFFF00);

        /// <summary>Gets a color that has an RGB value of #FF00FF.</summary>
        public static readonly ChromaColor Magenta = FromRgb(0xFF00FF);

        /// <summary>Gets a color that has an RGB value of #00FFFF.</summary>
        public static readonly ChromaColor Cyan = FromRgb(0x00FFFF);

        /// <summary>Gets a color that has an RGB value of #FFFFFF.</summary>
        public static readonly ChromaColor White = FromRgb(0xFFFFFF);
    }
}
