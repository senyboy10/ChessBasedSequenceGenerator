namespace ChessBasedSequenceGenerator.Interfaces
{
    public interface IGrid
    {
        void SetValue(int row, int column, char value);
        char GetValue(int row, int column);
        (int, int) GetPosition(char value);
        int GetRowCount();
        int GetColumnCount();
    }

    public interface IGridTraversal
    {
        List<(int, int)> GetNextPositions((int, int) currentPosition);
    }

    public interface IGridTraversalFactory
    {
        IGridTraversal Create(string pieceName, int rows, int columns);
    }
}