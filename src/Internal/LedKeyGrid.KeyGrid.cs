using System.Runtime.InteropServices;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Sdk;

namespace ChromaWrapper.Internal
{
    internal sealed partial class LedKeyGrid : IKeyGrid
    {
        int IKeyGrid.Count => TotalKeyLeds;

        int IKeyGrid.Rows => TotalKeyRows;

        int IKeyGrid.Columns => TotalKeyColumns;

        ChromaKeyColor IKeyGrid.this[int index]
        {
            get => ChromaKeyColor.FromInt32(_grid[KeyIndex(index)]);
            set => _grid[KeyIndex(index)] = value.ToInt32();
        }

        Span<ChromaKeyColor> IKeyGrid.this[Range range] => MemoryMarshal.Cast<int, ChromaKeyColor>(_grid.AsSpan(_ledLength, TotalKeyLeds)[range]);

        ChromaKeyColor IKeyGrid.this[int row, int column]
        {
            get => ChromaKeyColor.FromInt32(_grid[KeyIndexOf(row, column)]);
            set => _grid[KeyIndexOf(row, column)] = value.ToInt32();
        }

        ChromaKeyColor IKeyGrid.this[KeyboardKey key]
        {
            get => ((IKeyGrid)this)[RowOf(key), ColumnOf(key)];
            set => ((IKeyGrid)this)[RowOf(key), ColumnOf(key)] = value;
        }

        void IKeyGrid.Clear()
        {
            Array.Clear(_grid, _ledLength, TotalKeyLeds);
        }

        void IKeyGrid.Fill(ChromaKeyColor color)
        {
            Array.Fill(_grid, color.ToInt32(), _ledLength, TotalKeyLeds);
        }

        private static int RowOf(KeyboardKey key)
        {
            return ((int)key >> 8) & 0xFF;
        }

        private static int ColumnOf(KeyboardKey key)
        {
            return (int)key & 0xFF;
        }

        private int KeyIndex(int index)
        {
            index += _ledLength;

            if (index < _ledLength || index >= _grid.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return index;
        }

        private int KeyIndexOf(int row, int column)
        {
            if (row is < 0 or >= TotalKeyRows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (column is < 0 or >= TotalKeyColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            return _ledLength + (row * TotalKeyColumns) + column;
        }
    }
}
