using ChromaWrapper.Internal;
using ChromaWrapper.Sdk;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class LedGridTests
    {
        [Fact]
        public void VirtualLedsCanBeAdressed()
        {
            int nr = 5;
            int nc = 10;
            ILedGrid g = new LedGrid(nr, nc);

            Assert.Equal(nr, g.Rows);
            Assert.Equal(nc, g.Columns);

            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < nc; j++)
                {
                    var c = ChromaColor.FromRgb(0, (byte)i, (byte)j);
                    g[i, j] = c;
                    Assert.Equal(c, g[i, j]);
                }
            }
        }

        [Fact]
        public void ChecksForGridBounds()
        {
            int nr = 5;
            int nc = 10;
            var g = new LedGrid(nr, nc);

            Assert.Throws<ArgumentOutOfRangeException>("row", () => g[-1, 0] = ChromaColor.FromRgb(1));
            Assert.Throws<ArgumentOutOfRangeException>("column", () => g[0, -1] = ChromaColor.FromRgb(2));
            Assert.Throws<ArgumentOutOfRangeException>("row", () => g[nr, 0] = ChromaColor.FromRgb(3));
            Assert.Throws<ArgumentOutOfRangeException>("column", () => g[0, nc] = ChromaColor.FromRgb(4));
        }

        [Fact]
        public void VirtualLedsCanBeAllSetToTheSameColor()
        {
            int nr = 5;
            int nc = 10;
            ILedGrid g = new LedGrid(nr, nc);

            var c = ChromaColor.FromRgb(0x885511);
            g.Fill(c);

            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < nc; j++)
                {
                    Assert.Equal(c, g[i, j]);
                }
            }
        }

        [Fact]
        public void VirtualLedsCanBeResetToTheirDefaultValues()
        {
            int nr = 5;
            int nc = 10;
            ILedGrid g = new LedGrid(nr, nc);

            g.Fill(ChromaColor.FromRgb(0x885511));

            g.Clear();

            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < nc; j++)
                {
                    Assert.Equal(default, g[i, j]);
                }
            }
        }
    }
}
