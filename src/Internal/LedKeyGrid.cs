namespace ChromaWrapper.Internal
{
    internal sealed partial class LedKeyGrid : IColorBuffer
    {
        public const int TotalKeyRows = 6;
        public const int TotalKeyColumns = 22;
        public const int TotalKeyLeds = TotalKeyRows * TotalKeyColumns;

        /*
         * Both grids are stored contiguously in a single array.
         * |     Colors     |       Keys       |
         * |<- _ledLength ->|<- TotalKeyLeds ->|
         */

        private readonly int[] _grid;

        private readonly int _ledLength;

        private readonly int _ledRows;
        private readonly int _ledColumns;

        public LedKeyGrid(int ledRows, int ledColumns)
        {
            _ledLength = ledRows * ledColumns;

            _grid = new int[_ledLength + TotalKeyLeds];

            _ledRows = ledRows;
            _ledColumns = ledColumns;
        }

        Array IColorBuffer.Buffer => _grid;
    }
}
