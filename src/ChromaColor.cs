using System.Drawing;

namespace ChromaWrapper
{
    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/gdi/colorref">COLORREF</seealso>
    public readonly partial record struct ChromaColor
    {
        /// <summary>
        /// Gets a transparent keyboard key color.
        /// </summary>
        public static readonly Sdk.ChromaKeyColor Transparent = Sdk.ChromaKeyColor.Transparent;

        internal const int ColorMask = 0xFFFFFF;

        private readonly int _value;

        private ChromaColor(int bgr)
        {
            _value = bgr & ColorMask;
        }

        /// <summary>
        /// Gets the red component value of this <see cref="ChromaColor"/> structure.
        /// </summary>
        public byte R => (byte)(_value & 0xFF);

        /// <summary>
        /// Gets the green component value of this <see cref="ChromaColor"/> structure.
        /// </summary>
        public byte G => (byte)((_value >> 8) & 0xFF);

        /// <summary>
        /// Gets the blue component value of this <see cref="ChromaColor"/> structure.
        /// </summary>
        public byte B => (byte)((_value >> 16) & 0xFF);

        /// <summary>
        /// Converts a <see cref="Color"/> structure to a <see cref="ChromaColor"/> structure.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        public static explicit operator ChromaColor(Color color)
        {
            return FromColor(color);
        }

        /// <summary>
        /// Converts a <see cref="ChromaColor"/> structure to a <see cref="Color"/> structure.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        public static implicit operator Color(ChromaColor color)
        {
            return color.ToColor();
        }

        /// <summary>
        /// Creates a <see cref="ChromaColor"/> structure from a <see cref="Color"/> structure.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> structure to copy the RGB components from.</param>
        /// <returns>The new <see cref="ChromaColor"/> structure.</returns>
        /// <remarks>Only RGB components are copied; the alpha (opacity) component is discarded.</remarks>
        public static ChromaColor FromColor(Color color)
        {
            return FromRgb(color.R, color.G, color.B);
        }

        /// <summary>
        /// Creates a <see cref="ChromaColor"/> structure from red, green and blue component values.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        /// <returns>The new <see cref="ChromaColor"/> structure.</returns>
        public static ChromaColor FromRgb(byte red, byte green, byte blue)
        {
            return new ChromaColor((blue << 16) | (green << 8) | red);
        }

        /// <summary>
        /// Creates a <see cref="ChromaColor"/> structure from red, green and blue component values.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        /// <returns>The new <see cref="ChromaColor"/> structure.</returns>
        public static ChromaColor FromRgb(double red, double green, double blue)
        {
            byte r = (byte)(Math.Clamp(red, 0, 1) * 255);
            byte g = (byte)(Math.Clamp(green, 0, 1) * 255);
            byte b = (byte)(Math.Clamp(blue, 0, 1) * 255);

            return FromRgb(r, g, b);
        }

        /// <summary>
        /// Creates a <see cref="ChromaColor"/> structure from a 32-bit RGB value.
        /// </summary>
        /// <param name="rgb">The 32-bit RGB value in the form <c>0x00rrggbb</c>).</param>
        /// <returns>The new <see cref="ChromaColor"/> structure.</returns>
        public static ChromaColor FromRgb(int rgb)
        {
            byte b = (byte)(rgb & 0xFF);
            byte g = (byte)((rgb >> 8) & 0xFF);
            byte r = (byte)((rgb >> 16) & 0xFF);

            return FromRgb(r, g, b);
        }

        /// <summary>
        /// Gets the <see cref="Color"/> representation of this <see cref="ChromaColor"/> structure.
        /// </summary>
        /// <returns>The new <see cref="Color"/> structure.</returns>
        public Color ToColor()
        {
            return Color.FromArgb(R, G, B);
        }

        /// <summary>
        /// Gets the 32-bit RGB value of this <see cref="ChromaColor"/> structure.
        /// </summary>
        /// <returns>The 32-bit RGB value.</returns>
        public int ToRgb()
        {
            return (R << 16) | (G << 8) | B;
        }

        /// <summary>
        /// Indicates whether the current color object is equal to another color object.
        /// </summary>
        /// <param name="other">A color to compare with this color.</param>
        /// <returns><c>true</c> if the current color is equal to the color in <paramref name="other"/>; otherwise, false.</returns>
        public bool Equals(ChromaColor other)
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
        /// Converts this <see cref="ChromaColor"/> structure to a human-readable string.
        /// </summary>
        /// <returns>An HTML hex triplet string representation of this structure's color.</returns>
        public override string ToString()
        {
            return $"#{ToRgb():X6}";
        }

        /// <summary>
        /// Deconstructs this <see cref="ChromaColor"/> structure into its components.
        /// </summary>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        public void Deconstruct(out byte r, out byte g, out byte b)
        {
            r = R;
            g = G;
            b = B;
        }

        internal static ChromaColor FromInt32(int value)
        {
            return new ChromaColor(value);
        }

        internal int ToInt32()
        {
            return _value;
        }
    }
}
