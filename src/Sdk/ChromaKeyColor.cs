using System.Drawing;

namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents a special case of <see cref="ChromaColor"/> used in custom keyboard effects that implement the <see cref="IKeyGridEffect"/> interface.
    /// </summary>
    /// <remarks>
    /// The Razer Chroma SDK's keyboard effects that have a <c>Key</c> grid require that a <c>0x01000000</c> mask
    /// be applied to the corresponding color in order to be actually rendered; otherwise, the color is treated
    /// as transparent and therefore is not rendered.
    /// </remarks>
    /// <seealso cref="ChromaColor"/>
    public readonly record struct ChromaKeyColor
    {
        internal const int KeySetFlag = 0x1000000;

        internal static readonly ChromaKeyColor Transparent;

        private readonly int _value;

        private ChromaKeyColor(int fbgr)
        {
            _value = (fbgr & KeySetFlag) != 0
                ? (fbgr & (KeySetFlag | ChromaColor.ColorMask))
                : 0;
        }

        /// <summary>
        /// Gets the red component value of this <see cref="ChromaKeyColor"/> structure.
        /// </summary>
        /// <remarks>
        /// If <see cref="IsTransparent"/> is <c>true</c>, this property always returns 0.
        /// </remarks>
        public byte R => (byte)(_value & 0xFF);

        /// <summary>
        /// Gets the green component value of this <see cref="ChromaKeyColor"/> structure.
        /// </summary>
        /// <remarks>
        /// If <see cref="IsTransparent"/> is <c>true</c>, this property always returns 0.
        /// </remarks>
        public byte G => (byte)((_value >> 8) & 0xFF);

        /// <summary>
        /// Gets the blue component value of this <see cref="ChromaKeyColor"/> structure.
        /// </summary>
        /// <remarks>
        /// If <see cref="IsTransparent"/> is <c>true</c>, this property always returns 0.
        /// </remarks>
        public byte B => (byte)((_value >> 16) & 0xFF);

        /// <summary>
        /// Gets a value indicating whether this <see cref="ChromaKeyColor"/> structure is the <see cref="Transparent"/> key color.
        /// </summary>
        public bool IsTransparent => (_value & KeySetFlag) == 0;

        /// <summary>
        /// Converts a <see cref="Color"/> structure to a <see cref="ChromaKeyColor"/> structure.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        public static explicit operator ChromaKeyColor(Color color)
        {
            return FromColor(color);
        }

        /// <summary>
        /// Converts a <see cref="ChromaKeyColor"/> structure to a <see cref="Color"/> structure.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        public static implicit operator Color(ChromaKeyColor color)
        {
            return color.ToColor();
        }

        /// <summary>
        /// Convert a <see cref="ChromaColor"/> structure to a <see cref="ChromaKeyColor"/> structure.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        public static implicit operator ChromaKeyColor(ChromaColor color)
        {
            return FromChromaColor(color);
        }

        /// <summary>
        /// Convert a <see cref="ChromaKeyColor"/> structure to a <see cref="ChromaColor"/> structure.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        public static explicit operator ChromaColor(ChromaKeyColor color)
        {
            return color.ToChromaColor();
        }

        /// <summary>
        /// Creates a <see cref="ChromaKeyColor"/> structure from a <see cref="Color"/> structure.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> structure to copy the RGB components from.</param>
        /// <returns>The new <see cref="ChromaKeyColor"/> structure.</returns>
        /// <remarks>If <paramref name="color"/> is the transparent color, <see cref="Transparent"/> will be produced; otherwise, only RGB components are copied.</remarks>
        public static ChromaKeyColor FromColor(Color color)
        {
            return color == Color.Transparent
                ? Transparent
                : ChromaColor.FromColor(color);
        }

        /// <summary>
        /// Creates a <see cref="ChromaKeyColor"/> structure from a <see cref="ChromaColor"/> structure.
        /// </summary>
        /// <param name="color">The <see cref="ChromaColor"/> structure to copy the RGB components from.</param>
        /// <returns>The new <see cref="ChromaKeyColor"/> structure.</returns>
        public static ChromaKeyColor FromChromaColor(ChromaColor color)
        {
            return new ChromaKeyColor(color.ToInt32() | KeySetFlag);
        }

        /// <summary>
        /// Gets the <see cref="Color"/> representation of this <see cref="ChromaKeyColor"/> structure.
        /// </summary>
        /// <returns>The new <see cref="Color"/> structure.</returns>
        public Color ToColor()
        {
            return IsTransparent
                ? Color.Transparent
                : Color.FromArgb(R, G, B);
        }

        /// <summary>
        /// Gets the <see cref="ChromaColor"/> representation of this <see cref="ChromaKeyColor"/> structure.
        /// </summary>
        /// <returns>The new <see cref="ChromaColor"/> structure.</returns>
        public ChromaColor ToChromaColor()
        {
            return ChromaColor.FromInt32(_value & ChromaColor.ColorMask);
        }

        /// <summary>
        /// Indicates whether the current color object is equal to another color object.
        /// </summary>
        /// <param name="other">A color to compare with this color.</param>
        /// <returns><c>true</c> if the current color is equal to the color in <paramref name="other"/>; otherwise, false.</returns>
        public bool Equals(ChromaKeyColor other)
        {
            return _value.Equals(other._value);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _value;
        }

        /// <summary>
        /// Converts this <see cref="ChromaKeyColor"/> structure to a human-readable string.
        /// </summary>
        /// <returns>An HTML hex triplet string representation of this structure's color, or '(Transparent)' if there's no color set.</returns>
        public override string ToString()
        {
            return _value == 0
                ? "(Transparent)"
                : ((ChromaColor)this).ToString();
        }

        /// <summary>
        /// Deconstructs this <see cref="ChromaKeyColor"/> structure into its components.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        /// <param name="isTransparent">Whether this <see cref="ChromaKeyColor"/> structure is the <see cref="Transparent"/> key color.</param>
        public void Deconstruct(out byte r, out byte g, out byte b, out bool isTransparent)
        {
            r = R;
            g = G;
            b = B;
            isTransparent = IsTransparent;
        }

        internal static ChromaKeyColor FromInt32(int value)
        {
            return new ChromaKeyColor(value);
        }

        internal int ToInt32()
        {
            return _value;
        }
    }
}
