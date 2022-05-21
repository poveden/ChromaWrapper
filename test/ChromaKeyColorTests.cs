using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ChromaWrapper.Sdk;
using ChromaWrapper.Tests.Internal;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class ChromaKeyColorTests
    {
        public const int SizeOfCOLORREF = 4; // Reference: https://docs.microsoft.com/en-us/windows/win32/gdi/colorref

        [Fact]
        public void CanBeCreatedFromSystemDrawingColor()
        {
            var c = Color.AliceBlue;

            var color = ChromaKeyColor.FromColor(c);

            Assert.Equal(c.R, color.R);
            Assert.Equal(c.G, color.G);
            Assert.Equal(c.B, color.B);
            Assert.False(color.IsTransparent);

            c = Color.Transparent;

            color = ChromaKeyColor.FromColor(c);

            Assert.Equal(0, color.R);
            Assert.Equal(0, color.G);
            Assert.Equal(0, color.B);
            Assert.True(color.IsTransparent);
        }

        [Fact]
        public void CanBeExplicitlyCastFromSystemDrawingColor()
        {
            var c = Color.AliceBlue;

            var color = (ChromaKeyColor)c;

            Assert.Equal(c.R, color.R);
            Assert.Equal(c.G, color.G);
            Assert.Equal(c.B, color.B);

            c = Color.Transparent;

            color = (ChromaKeyColor)c;

            Assert.Equal(0, color.R);
            Assert.Equal(0, color.G);
            Assert.Equal(0, color.B);
            Assert.True(color.IsTransparent);
        }

        [Fact]
        public void ConvertsToChromaColor()
        {
            var color = (ChromaKeyColor)ChromaColor.FromRgb(0x993377);

            var cc = color.ToChromaColor();
            Assert.Equal(0x993377, cc.ToRgb());

            color = ChromaColor.Transparent;

            cc = color.ToChromaColor();
            Assert.Equal(0, cc.ToRgb());
        }

        [Fact]
        public void CanBeExplicitlyCastToChromaColor()
        {
            var color = (ChromaKeyColor)ChromaColor.FromRgb(0x993377);

            var cc = (ChromaColor)color;
            Assert.Equal(0x993377, cc.ToRgb());

            color = ChromaColor.Transparent;

            cc = (ChromaColor)color;
            Assert.Equal(0, cc.ToRgb());
        }

        [Fact]
        public void IgnoresAlphaColorComponent()
        {
            var c = Color.FromArgb(128, Color.AntiqueWhite);

            var color = ChromaKeyColor.FromColor(c);

            Assert.Equal(c.R, color.R);
            Assert.Equal(c.G, color.G);
            Assert.Equal(c.B, color.B);
            Assert.Equal(1, color.GetPrivateField<int>("_value") >> 24);

            int argb = 0x12345678;

            color = ChromaColor.FromRgb(argb);

            Assert.Equal((byte)0x34, color.R);
            Assert.Equal((byte)0x56, color.G);
            Assert.Equal((byte)0x78, color.B);
            Assert.Equal(1, color.GetPrivateField<int>("_value") >> 24);
        }

        [Fact]
        public void ConvertsToSystemDrawingColor()
        {
            int rgb = 0x336699;

            var color = (ChromaKeyColor)ChromaColor.FromRgb(rgb);

            var c = color.ToColor();

            Assert.Equal(color.R, c.R);
            Assert.Equal(color.G, c.G);
            Assert.Equal(color.B, c.B);
            Assert.Equal((byte)0xFF, c.A);

            color = ChromaKeyColor.Transparent;

            c = color.ToColor();

            Assert.Equal(Color.Transparent, c);
        }

        [Fact]
        public void CanBeImplicitlyCastToSystemDrawingColor()
        {
            int rgb = 0x336699;

            var color = (ChromaKeyColor)ChromaColor.FromRgb(rgb);

            Color c = color;

            Assert.Equal(color.R, c.R);
            Assert.Equal(color.G, c.G);
            Assert.Equal(color.B, c.B);
            Assert.Equal((byte)0xFF, c.A);
        }

        [Fact]
        public void IEquatableIsCorrectlyImplemented()
        {
            static bool IsCompilerGenerated(MemberInfo mi)
            {
                return mi.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
            }

            var t = typeof(ChromaColor);

            Assert.True(t.IsAssignableTo(typeof(IEquatable<ChromaColor>)));

            Assert.True(IsCompilerGenerated(t.GetMethod("Equals", new[] { typeof(object) })!));
            Assert.True(IsCompilerGenerated(t.GetMethod("op_Equality", new[] { t, t })!));
            Assert.True(IsCompilerGenerated(t.GetMethod("op_Inequality", new[] { t, t })!));

            Assert.False(IsCompilerGenerated(t.GetMethod("GetHashCode")!));
            Assert.False(IsCompilerGenerated(t.GetMethod("Equals", new[] { t })!));

            int bgr1 = 0x01020100;
            int bgr2 = 0x01020101;

            var c1a = ChromaKeyColor.FromInt32(bgr1);
            var c1b = ChromaKeyColor.FromInt32(bgr1);
            var c2 = ChromaKeyColor.FromInt32(bgr2);

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
        public void InternalValueIsCOLORREFWithKeySetFlag()
        {
            int rgb = 0x996633;

            var color = (ChromaKeyColor)ChromaColor.FromRgb(rgb);

            int fbgr = color.GetPrivateField<int>("_value");

            Assert.Equal(0x01336699, fbgr);
        }

        [Fact]
        public void MarshaledSizeIs4Bytes()
        {
            int sz = Marshal.SizeOf<ChromaKeyColor>();
            Assert.Equal(SizeOfCOLORREF, sz);
        }

        [Fact]
        public void ProvidesAStringRepresentation()
        {
            var color = (ChromaKeyColor)ChromaColor.FromRgb(0x00aa22);

            Assert.Equal("#00AA22", color.ToString());

            color = ChromaColor.Transparent;

            Assert.Equal("(Transparent)", color.ToString());
        }

        [Fact]
        public void CanBeDeconstructed()
        {
            byte r1 = 113;
            byte g1 = 221;
            byte b1 = 15;

            var color = (ChromaKeyColor)ChromaColor.FromRgb(r1, g1, b1);
            (byte r2, byte g2, byte b2, bool isTransparent) = color;

            Assert.Equal(r1, r2);
            Assert.Equal(g1, g2);
            Assert.Equal(b1, b2);
            Assert.False(isTransparent);

            (r2, g2, b2, isTransparent) = ChromaKeyColor.Transparent;

            Assert.Equal(0, r2);
            Assert.Equal(0, g2);
            Assert.Equal(0, b2);
            Assert.True(isTransparent);
        }
    }
}
