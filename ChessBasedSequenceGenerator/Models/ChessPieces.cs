using ChessBasedSequenceGenerator.Interfaces;

namespace ChessBasedSequenceGenerator.Models
{
    public abstract class ChessPiece : IGridTraversal
    {
        protected readonly int _rows;
        protected readonly int _columns;

        public ChessPiece(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public abstract List<(int, int)> GetNextPositions((int, int) currentPosition);

        protected void AddHorizontalAndVertical((int, int) currentPosition, List<(int, int)> positions)
        {
            for (int i = 0; i < _rows; i++)
            {
                if (i != currentPosition.Item1)
                {
                    positions.Add((i, currentPosition.Item2));
                }
            }

            for (int i = 0; i < _columns; i++)
            {
                if (i != currentPosition.Item2)
                {
                    positions.Add((currentPosition.Item1, i));
                }
            }
        }
    }


    public class Rook : ChessPiece
    {
        public Rook(int rows, int columns) : base(rows, columns) { }

        public override List<(int, int)> GetNextPositions((int, int) currentPosition)
        {
            var positions = new List<(int, int)>();
            AddHorizontalAndVertical(currentPosition, positions);
            return positions;
        }
    }


    public class Queen : ChessPiece
    {
        /*
         * A queen in chess has the combined moves of a rook and a bishop; 
         * she can move any number of squares along a rank, file, or diagonal
         * */
        public Queen(int rows, int columns) : base(rows, columns) { }

        public override List<(int, int)> GetNextPositions((int, int) currentPosition)
        {
            var positions = new List<(int, int)>();
            AddHorizontalAndVertical(currentPosition, positions);
            // Additional movement for Queen
            for (int i = 1; i < _rows; i++)
            {
                if (currentPosition.Item1 + i < _rows && currentPosition.Item2 + i < _columns)
                    positions.Add((currentPosition.Item1 + i, currentPosition.Item2 + i));
                if (currentPosition.Item1 + i < _rows && currentPosition.Item2 - i >= 0)
                    positions.Add((currentPosition.Item1 + i, currentPosition.Item2 - i));
                if (currentPosition.Item1 - i >= 0 && currentPosition.Item2 + i < _columns)
                    positions.Add((currentPosition.Item1 - i, currentPosition.Item2 + i));
                if (currentPosition.Item1 - i >= 0 && currentPosition.Item2 - i >= 0)
                    positions.Add((currentPosition.Item1 - i, currentPosition.Item2 - i));
            }
            return positions;
        }
    }

    public class King : ChessPiece
    {
        public King(int rows, int columns) : base(rows, columns) { }
        public override List<(int, int)> GetNextPositions((int, int) currentPosition)
        {
            var positions = new List<(int, int)>();
            var directions = new List<(int, int)> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

            foreach (var dir in directions)
            {
                int newRow = currentPosition.Item1 + dir.Item1;
                int newCol = currentPosition.Item2 + dir.Item2;

                if (newRow >= 0 && newRow < _rows && newCol >= 0 && newCol < _columns)
                    positions.Add((newRow, newCol));
            }

            return positions;
        }
    }

    public class Knight : ChessPiece
    {
        public Knight(int rows, int columns) : base(rows, columns) { }
        public override List<(int, int)> GetNextPositions((int, int) currentPosition)
        {
            var positions = new List<(int, int)>();
            var knightMoves = new List<(int, int)> { (2, 1), (1, 2), (2, -1), (-1, 2), (-2, 1), (1, -2), (-2, -1), (-1, -2) };

            foreach (var move in knightMoves)
            {
                int newRow = currentPosition.Item1 + move.Item1;
                int newCol = currentPosition.Item2 + move.Item2;

                if (newRow >= 0 && newRow < _rows && newCol >= 0 && newCol < _columns)
                    positions.Add((newRow, newCol));
            }

            return positions;
        }
    }

    public class Bishop : ChessPiece
    {
        public Bishop(int rows, int columns) : base(rows, columns) { }

        public override List<(int, int)> GetNextPositions((int, int) currentPosition)
        {
            var positions = new List<(int, int)>();

            for (int i = 1; i < _rows; i++)
            {
                if (currentPosition.Item1 + i < _rows && currentPosition.Item2 + i < _columns)
                    positions.Add((currentPosition.Item1 + i, currentPosition.Item2 + i));
                if (currentPosition.Item1 + i < _rows && currentPosition.Item2 - i >= 0)
                    positions.Add((currentPosition.Item1 + i, currentPosition.Item2 - i));
                if (currentPosition.Item1 - i >= 0 && currentPosition.Item2 + i < _columns)
                    positions.Add((currentPosition.Item1 - i, currentPosition.Item2 + i));
                if (currentPosition.Item1 - i >= 0 && currentPosition.Item2 - i >= 0)
                    positions.Add((currentPosition.Item1 - i, currentPosition.Item2 - i));
            }

            return positions;
        }
    }

    public class Pawn : ChessPiece
    {

        public Pawn(int rows, int columns) : base(rows, columns) { }
        public override List<(int, int)> GetNextPositions((int, int) currentPosition)
        {
            var positions = new List<(int, int)>();
            var directions = new List<(int, int)> { (1, 0), (1, -1), (1, 1) };

            foreach (var dir in directions)
            {
                int newRow = currentPosition.Item1 + dir.Item1;
                int newCol = currentPosition.Item2 + dir.Item2;

                if (newRow >= 0 && newRow < _rows && newCol >= 0 && newCol < _columns)
                    positions.Add((newRow, newCol));
            }

            return positions;
        }
    }
}