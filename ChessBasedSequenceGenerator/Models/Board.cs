using ChessBasedSequenceGenerator.Interfaces;

namespace ChessBasedSequenceGenerator.Models
{
    public class Board : IGrid
    {
        protected readonly char[,] _boardLayout;

        public Board(int rows, int columns)
        {
            _boardLayout = new char[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    _boardLayout[row, column] = ' ';
                }
            }
        }

        public virtual void SetValue(int row, int column, char value)
        {
            if (row < 0 || row >= _boardLayout.GetLength(0) ||
                column < 0 || column >= _boardLayout.GetLength(1))
            {
                throw new ArgumentOutOfRangeException($"Invalid position: ({row}, {column}) is outside the board");
            }

            if (!IsValidValue(value))
            {
                throw new ArgumentException($"Invalid value: {value} is not compatible with the board");
            }

            _boardLayout[row, column] = value;
        }

        protected virtual bool IsValidValue(char value)
        {
            return true;
        }

        public char GetValue(int row, int column)
        {
            if (row < 0 || row >= _boardLayout.GetLength(0) ||
                column < 0 || column >= _boardLayout.GetLength(1))
            {
                throw new ArgumentOutOfRangeException($"Invalid position: ({row}, {column}) is outside the board");
            }

            return _boardLayout[row, column];
        }

        public (int, int) GetPosition(char value)
        {
            for (int row = 0; row < _boardLayout.GetLength(0); row++)
            {
                for (int column = 0; column < _boardLayout.GetLength(1); column++)
                {
                    if (_boardLayout[row, column] == value)
                    {
                        return (row, column);
                    }
                }
            }

            throw new ArgumentException("The value is not found on the board");
        }

        public int GetRowCount()
        {
            return _boardLayout.GetLength(0);
        }

        public int GetColumnCount()
        {
            return _boardLayout.GetLength(1);
        }
    }
}

