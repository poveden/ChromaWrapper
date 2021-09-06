using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class LedArrayTests
    {
        [Fact]
        public void VirtualLedsCanBeAddressed()
        {
            int n = 10;
            ILedArray a = new LedArray(n);

            Assert.Equal(n, a.Count);

            for (int i = 0; i < n; i++)
            {
                var c = ChromaColor.FromRgb(0, 0, (byte)i);
                a[i] = c;
                Assert.Equal(c, a[i]);
            }
        }

        [Fact]
        public void VirtualLedsCanBeAllSetToTheSameColor()
        {
            int n = 10;
            ILedArray a = new LedArray(n);

            var c = ChromaColor.FromRgb(0x885511);
            a.Fill(c);

            for (int i = 0; i < n; i++)
            {
                Assert.Equal(c, a[i]);
            }
        }

        [Fact]
        public void VirtualLedsCanBeResetToTheirDefaultValues()
        {
            int n = 10;
            ILedArray a = new LedArray(n);

            a.Fill(ChromaColor.FromRgb(0x885511));

            a.Clear();

            for (int i = 0; i < n; i++)
            {
                Assert.Equal(default, a[i]);
            }
        }

        [Fact]
        public void VirtualLedsCanBeAddressedByRange()
        {
            int n = 10;
            ILedArray a = new LedArray(n);

            a[2..4].Fill(ChromaColor.FromRgb(0x220033));

            for (int i = 0; i < n; i++)
            {
                if (i is >= 2 and < 4)
                {
                    Assert.Equal(0x220033, a[i].ToRgb());
                }
                else
                {
                    Assert.Equal(0, a[i].ToRgb());
                }
            }
        }
    }
}
