using System.Drawing;
using System.Runtime.InteropServices;
using ChromaWrapper.Tests.Internal;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class ChromaColorTests
    {
        public const int SizeOfCOLORREF = 4; // Reference: https://docs.microsoft.com/en-us/windows/win32/gdi/colorref

        [Fact]
        public void CanBeCreatedFromRgbByteComponents()
        {
            byte r = 123;
            byte g = 212;
            byte b = 1;

            var color = ChromaColor.FromRgb(r, g, b);

            Assert.Equal(r, color.R);
            Assert.Equal(g, color.G);
            Assert.Equal(b, color.B);
        }

        [Fact]
        public void CanBeCreatedFromRgbDoubleComponents()
        {
            double r = 0.68;
            double g = 0.29;
            double b = 1;

            var color = ChromaColor.FromRgb(r, g, b);

            Assert.Equal(173, color.R);
            Assert.Equal(73, color.G);
            Assert.Equal(255, color.B);
        }

        [Fact]
        public void CanBeCreatedFromSystemDrawingColor()
        {
            var c = Color.AliceBlue;

            var color = ChromaColor.FromColor(c);

            Assert.Equal(c.R, color.R);
            Assert.Equal(c.G, color.G);
            Assert.Equal(c.B, color.B);
        }

        [Fact]
        public void CanBeExplicitlyCastFromSystemDrawingColor()
        {
            var c = Color.AliceBlue;

            var color = (ChromaColor)c;

            Assert.Equal(c.R, color.R);
            Assert.Equal(c.G, color.G);
            Assert.Equal(c.B, color.B);
        }

        [Fact]
        public void CanBeCreatedFromRgbInteger()
        {
            int rgb = 0x123456;

            var color = ChromaColor.FromRgb(rgb);

            Assert.Equal((byte)0x12, color.R);
            Assert.Equal((byte)0x34, color.G);
            Assert.Equal((byte)0x56, color.B);
        }

        [Fact]
        public void IgnoresAlphaColorComponent()
        {
            var c = Color.FromArgb(128, Color.AntiqueWhite);

            var color = ChromaColor.FromColor(c);

            Assert.Equal(c.R, color.R);
            Assert.Equal(c.G, color.G);
            Assert.Equal(c.B, color.B);
            Assert.Equal(0, color.GetPrivateField<int>("_value") >> 24);

            int argb = 0x12345678;

            color = ChromaColor.FromRgb(argb);

            Assert.Equal((byte)0x34, color.R);
            Assert.Equal((byte)0x56, color.G);
            Assert.Equal((byte)0x78, color.B);
            Assert.Equal(0, color.GetPrivateField<int>("_value") >> 24);
        }

        [Fact]
        public void ConvertsToSystemDrawingColor()
        {
            int rgb = 0x336699;

            var color = ChromaColor.FromRgb(rgb);

            var c = color.ToColor();

            Assert.Equal(color.R, c.R);
            Assert.Equal(color.G, c.G);
            Assert.Equal(color.B, c.B);
            Assert.Equal((byte)0xFF, c.A);
        }

        [Fact]
        public void CanBeImplicitlyCastToSystemDrawingColor()
        {
            int rgb = 0x336699;

            var color = ChromaColor.FromRgb(rgb);

            Color c = color;

            Assert.Equal(color.R, c.R);
            Assert.Equal(color.G, c.G);
            Assert.Equal(color.B, c.B);
            Assert.Equal((byte)0xFF, c.A);
        }

        [Fact]
        public void ConvertToRgbInteger()
        {
            int rgb = 0x224466;

            var color = ChromaColor.FromRgb(rgb);

            int cRgb = color.ToRgb();

            Assert.Equal(rgb, cRgb);
        }

        [Fact]
        public void IEquatableIsCorrectlyImplemented()
        {
            int bgr1 = 0x020100;
            int bgr2 = 0x020101;

            var c1a = ChromaColor.FromInt32(bgr1);
            var c1b = ChromaColor.FromInt32(bgr1);
            var c2 = ChromaColor.FromInt32(bgr2);

            Assert.Equal(bgr1, c1a.ToInt32());
            Assert.Equal(bgr1, c1b.ToInt32());
            Assert.Equal(bgr2, c2.ToInt32());

            Assert.Equal(bgr1, c1a.GetHashCode());
            Assert.Equal(bgr1, c1b.GetHashCode());
            Assert.Equal(bgr2, c2.GetHashCode());

            Assert.True(c1a.Equals(c1b));
            Assert.True(c1b.Equals(c1a));
            Assert.False(c1a.Equals(c2));
            Assert.False(c2.Equals(c1a));

            Assert.False(c1a.Equals(null));
            Assert.False(c1a.Equals(new object()));
            Assert.False(c1a.Equals(bgr1));
            Assert.True(c1a.Equals((object)c1b));

            Assert.True(c1a == c1b);
            Assert.True(c1b == c1a);
            Assert.False(c1a == c2);
            Assert.False(c2 == c1a);

            Assert.False(c1a != c1b);
            Assert.False(c1b != c1a);
            Assert.True(c1a != c2);
            Assert.True(c2 != c1a);
        }

        [Fact]
        public void InternalValueIsCOLORREF()
        {
            int rgb = 0x996633;

            var color = ChromaColor.FromRgb(rgb);

            int bgr = color.GetPrivateField<int>("_value");

            Assert.Equal(0x336699, bgr);
        }

        [Fact]
        public void MarshaledSizeIs4Bytes()
        {
            int sz = Marshal.SizeOf<ChromaColor>();
            Assert.Equal(SizeOfCOLORREF, sz);
        }

        [Fact]
        public void ProvidesAStringRepresentation()
        {
            var color = ChromaColor.FromRgb(0x00aa22);

            Assert.Equal("#00AA22", color.ToString());
        }
    }
}
