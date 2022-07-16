using ChromaWrapper.Sdk;

namespace ChromaWrapper.Internal
{
    internal class LedGrid : LedArray, ILedGrid
    {
        public LedGrid(int rows, int columns)
            : base(rows * columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public int Rows { get; }

        public int Columns { get; }

        public ChromaColor this[int row, int column]
        {
            get => this[IndexOf(row, column)];
            set => this[IndexOf(row, column)] = value;
        }

        private int IndexOf(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            return (row * Columns) + column;
        }
    }
}
