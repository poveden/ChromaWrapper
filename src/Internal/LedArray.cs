using System;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Internal
{
    internal class LedArray : ILedArray, IColorBuffer
    {
        private readonly ChromaColor[] _grid;

        public LedArray(int length)
        {
            _grid = new ChromaColor[length];
        }

        public int Count => _grid.Length;

        Array IColorBuffer.Buffer => _grid;

        public ChromaColor this[int index]
        {
            get => _grid[index];
            set => _grid[index] = value;
        }

        public Span<ChromaColor> this[Range range] => _grid.AsSpan(range);

        public void Clear()
        {
            Array.Clear(_grid, 0, _grid.Length);
        }

        public void Fill(ChromaColor color)
        {
            Array.Fill(_grid, color);
        }
    }
}
