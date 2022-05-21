using ChromaWrapper.Internal;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Sdk;
using ChromaWrapper.Tests.Internal;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class LedKeyGridTests
    {
        [Fact]
        public void VirtualLedsCanBeAdressed()
        {
            ILedGrid g = new LedKeyGrid(5, 10);

            Assert.Equal(5 * 10, g.Count);
            Assert.Equal(5, g.Rows);
            Assert.Equal(10, g.Columns);

            int k = 0;
            for (int i = 0; i < g.Rows; i++)
            {
                for (int j = 0; j < g.Columns; j++)
                {
                    Assert.Equal(default, g[i, j]);
                    Assert.Equal(default, g[k]);

                    var c = ChromaColor.FromRgb(0, (byte)i, (byte)j);
                    g[i, j] = c;
                    Assert.Equal(c, g[i, j]);
                    Assert.Equal(c, g[k]);

                    g[k] = ChromaColor.FromRgb((byte)i, (byte)j, 0);
                    Assert.Equal(g[k], g[i, j]);

                    k++;
                }
            }
        }

        [Fact]
        public void ChecksForLedGridBounds()
        {
            int nr = 5;
            int nc = 10;
            ILedGrid g = new LedKeyGrid(nr, nc);

            Assert.Throws<ArgumentOutOfRangeException>("index", () => g[-1] = ChromaColor.FromRgb(1));
            Assert.Throws<ArgumentOutOfRangeException>("row", () => g[-1, 0] = ChromaColor.FromRgb(1));
            Assert.Throws<ArgumentOutOfRangeException>("column", () => g[0, -1] = ChromaColor.FromRgb(2));
            Assert.Throws<ArgumentOutOfRangeException>("index", () => g[nr * nc] = ChromaColor.FromRgb(4));
            Assert.Throws<ArgumentOutOfRangeException>("row", () => g[nr, 0] = ChromaColor.FromRgb(3));
            Assert.Throws<ArgumentOutOfRangeException>("column", () => g[0, nc] = ChromaColor.FromRgb(4));
        }

        [Fact]
        public void VirtualKeysCanBeAdressed()
        {
            IKeyGrid g = new LedKeyGrid(1, 1);

            Assert.Equal(6 * 22, g.Count);
            Assert.Equal(6, g.Rows);
            Assert.Equal(22, g.Columns);

            int k = 0;
            for (int i = 0; i < g.Rows; i++)
            {
                for (int j = 0; j < g.Columns; j++)
                {
                    Assert.Equal(ChromaKeyColor.Transparent, g[i, j]);
                    Assert.Equal(ChromaKeyColor.Transparent, g[k]);

                    ChromaKeyColor c = ChromaColor.FromRgb(0, (byte)i, (byte)j);
                    g[i, j] = c;
                    Assert.Equal(c, g[i, j]);
                    Assert.Equal(c, g[k]);

                    g[k] = ChromaColor.FromRgb((byte)i, (byte)j, 0);
                    Assert.Equal(g[k], g[i, j]);

                    k++;
                }
            }

            foreach (var key in Enum.GetValues<KeyboardKey>().Except(new[] { KeyboardKey.None, KeyboardKey.Invalid }))
            {
                int row = ((int)key >> 8) & 0xFF;
                int col = (int)key & 0xFF;
                int index = (row * g.Columns) + col;

                Assert.Equal(g[row, col], g[key]);
                Assert.Equal(g[row, col], g[index]);

                ChromaKeyColor c = ChromaColor.FromRgb((int)key);
                g[key] = c;
                Assert.Equal(c, g[key]);
                Assert.Equal(c, g[index]);

                g[key] = ChromaKeyColor.Transparent;
                Assert.Equal(ChromaKeyColor.Transparent, g[key]);
                Assert.Equal(ChromaKeyColor.Transparent, g[index]);
            }
        }

        [Fact]
        public void ChecksForKeyGridBounds()
        {
            IKeyGrid g = new LedKeyGrid(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>("index", () => g[-1] = ChromaColor.FromRgb(1));
            Assert.Throws<ArgumentOutOfRangeException>("row", () => g[-1, 0] = ChromaColor.FromRgb(2));
            Assert.Throws<ArgumentOutOfRangeException>("column", () => g[0, -1] = ChromaColor.FromRgb(3));
            Assert.Throws<ArgumentOutOfRangeException>("index", () => g[6 * 22] = ChromaColor.FromRgb(4));
            Assert.Throws<ArgumentOutOfRangeException>("row", () => g[6, 0] = ChromaColor.FromRgb(5));
            Assert.Throws<ArgumentOutOfRangeException>("column", () => g[0, 22] = ChromaColor.FromRgb(6));
        }

        [Fact]
        public void InternalKeyGridRepresentationSetsHighestByteWhenColorIsNotNull()
        {
            IKeyGrid g = new LedKeyGrid(1, 1);
            int[] grid = g.GetPrivateField<int[]>("_grid")!;

            for (int i = 0; i < g.Rows; i++)
            {
                for (int j = 0; j < g.Columns; j++)
                {
                    int index = g.InvokePrivateMethod<int>("KeyIndexOf", i, j);

                    Assert.Equal(ChromaKeyColor.Transparent, g[i, j]);
                    Assert.Equal(0, grid[index]);

                    var c = ChromaColor.FromRgb((byte)i, 0, (byte)j);
                    g[i, j] = c;

                    int fbgr = (0x01 << 24) | (j << 16) | i;
                    Assert.Equal(fbgr, grid[index]);
                }
            }
        }

        [Fact]
        public void VirtualLedsCanBeAllSetToTheSameColor()
        {
            int nr = 5;
            int nc = 10;
            ILedGrid g = new LedKeyGrid(nr, nc);

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
            ILedGrid g = new LedKeyGrid(nr, nc);

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

        [Fact]
        public void VirtualKeysCanBeAllSetToTheSameColor()
        {
            int nr = 5;
            int nc = 10;
            IKeyGrid g = new LedKeyGrid(nr, nc);

            ChromaKeyColor c = ChromaColor.FromRgb(0x885511);
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
        public void VirtualKeysCanBeResetToTheirDefaultValues()
        {
            int nr = 5;
            int nc = 10;
            IKeyGrid g = new LedKeyGrid(nr, nc);

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

        [Fact]
        public void VirtualLedsCanBeAddressedByRange()
        {
            int nr = 2;
            int nc = 5;
            ILedGrid g = new LedKeyGrid(nr, nc);

            g[4..8].Fill(ChromaColor.FromRgb(0x220033));

            for (int i = 0; i < g.Count; i++)
            {
                if (i is >= 4 and < 8)
                {
                    Assert.Equal(0x220033, g[i].ToRgb());
                }
                else
                {
                    Assert.Equal(0, g[i].ToRgb());
                }
            }
        }

        [Fact]
        public void VirtualKeysCanBeAddressedByRange()
        {
            int nr = 2;
            int nc = 5;
            IKeyGrid g = new LedKeyGrid(nr, nc);

            g[4..8].Fill(ChromaColor.FromRgb(0x220033));

            for (int i = 0; i < g.Count; i++)
            {
                if (i is >= 4 and < 8)
                {
                    Assert.Equal(0x220033, g[i].ToChromaColor().ToRgb());
                }
                else
                {
                    Assert.True(g[i].IsTransparent);
                }
            }
        }
    }
}
