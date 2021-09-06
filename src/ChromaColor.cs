using System;
using System.Drawing;

namespace ChromaWrapper
{
    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/gdi/colorref">COLORREF</seealso>
    public readonly partial struct ChromaColor : IEquatable<ChromaColor>
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
        /// Returns a value indicating whether two <see cref="ChromaColor"/> values are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> represent the same color; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ChromaColor left, ChromaColor right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value indicating whether two <see cref="ChromaColor"/> values are different.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> represent different colors; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ChromaColor left, ChromaColor right)
        {
            return !(left == right);
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
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> and this instance are the same type and represents the same value; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ChromaColor color && Equals(color);
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
        ///  Converts this <see cref="ChromaColor"/> structure to a human-readable string.
        /// </summary>
        /// <returns>An HTML hex triplet string representation of this structure's color.</returns>
        public override string ToString()
        {
            return $"#{ToRgb():X6}";
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
