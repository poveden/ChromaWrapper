using System;
using System.Runtime.InteropServices;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Internal
{
    internal sealed partial class LedKeyGrid : ILedGrid
    {
        int ILedArray.Count => _ledLength;

        int ILedGrid.Rows => _ledRows;

        int ILedGrid.Columns => _ledColumns;

        ChromaColor ILedArray.this[int index]
        {
            get => ChromaColor.FromInt32(_grid[LedIndex(index)]);
            set => _grid[LedIndex(index)] = value.ToInt32();
        }

        Span<ChromaColor> ILedArray.this[Range range] => MemoryMarshal.Cast<int, ChromaColor>(_grid.AsSpan(0, _ledLength)[range]);

        ChromaColor ILedGrid.this[int row, int column]
        {
            get => ChromaColor.FromInt32(_grid[LedIndexOf(row, column)]);
            set => _grid[LedIndexOf(row, column)] = value.ToInt32();
        }

        void ILedArray.Clear()
        {
            Array.Clear(_grid, 0, _ledLength);
        }

        void ILedArray.Fill(ChromaColor color)
        {
            Array.Fill(_grid, color.ToInt32(), 0, _ledLength);
        }

        private int LedIndex(int index)
        {
            if (index < 0 || index >= _ledLength)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return index;
        }

        private int LedIndexOf(int row, int column)
        {
            if (row < 0 || row >= _ledRows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (column < 0 || column >= _ledColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            return (row * _ledColumns) + column;
        }
    }
}
