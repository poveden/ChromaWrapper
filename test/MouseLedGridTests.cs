using System;
using System.Linq;
using ChromaWrapper.Mouse;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class MouseLedGridTests
    {
        [Fact]
        public void VirtualLedsCanBeAdressedByName()
        {
            var g = new CustomMouseEffect2().Color;

            Assert.NotEqual(0, g.Rows);
            Assert.NotEqual(0, g.Columns);

            for (int i = 0; i < g.Rows; i++)
            {
                for (int j = 0; j < g.Columns; j++)
                {
                    var c = ChromaColor.FromRgb(0, (byte)i, (byte)j);
                    g[i, j] = c;
                    Assert.Equal(c, g[i, j]);
                }
            }

            foreach (var led in Enum.GetValues<MouseLed2>().Except(new[] { MouseLed2.None }))
            {
                int row = ((int)led >> 8) & 0xFF;
                int col = (int)led & 0xFF;
                Assert.Equal(g[row, col], g[led]);

                var c = ChromaColor.FromRgb((int)led);
                g[led] = c;
                Assert.Equal(c, g[led]);
            }
        }
    }
}
