using ChessBasedSequenceGenerator.Interfaces;
using ChessBasedSequenceGenerator.Models;

namespace ChessBasedSequenceGenerator.Factories
{
    public class ChessPieceFactory : IGridTraversalFactory
    {
        public IGridTraversal Create(string piece, int rows, int columns)
        {
            if (string.IsNullOrEmpty(piece))
                throw new ArgumentNullException(nameof(piece));

            var gridTraversal = piece.ToLower();
            switch (gridTraversal)
            {
                case "rook":
                    return new Rook(rows, columns);
                case "queen":
                    return new Queen(rows, columns);
                case "king":
                    return new King(rows, columns);
                case "knight":
                    return new Knight(rows, columns);
                case "bishop":
                    return new Bishop(rows, columns);
                case "pawn":
                    return new Pawn(rows, columns);
                default:
                    throw new ArgumentException($"No chess piece found for {piece}");
            }
        }
    }
}

