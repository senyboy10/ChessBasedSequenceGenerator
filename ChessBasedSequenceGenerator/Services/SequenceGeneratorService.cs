using ChessBasedSequenceGenerator.Interfaces;
using ChessBasedSequenceGenerator.Models;

namespace ChessBasedSequenceGenerator.Services
{
	public class SequenceGeneratorService
    {
        private readonly IGrid _grid;
        private readonly ISequenceConfiguration _configuration;
        private readonly IGridTraversalFactory _traversalFactory;

        public SequenceGeneratorService(IGrid grid, ISequenceConfiguration configuration, IGridTraversalFactory traversalFactory)
        {
            _grid = grid;
            _configuration = configuration;
            _traversalFactory = traversalFactory;
        }

        public int CountValidSequences(string pieceName, char startValue)
        {
            var maxLength = _configuration.Length();
            var workingSequence = startValue.ToString();
            var currentPosition = _grid.GetPosition(startValue);
            var piece = _traversalFactory.Create(pieceName, _grid.GetRowCount(), _grid.GetColumnCount());
            return CountValidSequences(piece, currentPosition, workingSequence, maxLength);
        }

        public int CountValidSequences(IGridTraversal piece, (int, int) currentPosition, string workingSequence, int maxSize)
        {
            /*
             * The first value of the variable workingSequence is the phonekeypad value 
             * the user selects. While the chesspiece next position is inside the grid bounds, 
             * workingSequence is incremented until its length equals the max length 
             * (eg: 7 for phone numbers).
             * 1 is returned if the workingSequence is valid or 0 otherwise.
             */

            if (workingSequence.Length == maxSize)
            {
                return _configuration.IsValid(workingSequence) ? 1 : 0;
            }
            
            // recursive case
            var nextPositions = piece.GetNextPositions(currentPosition);
            int count = 0;
            foreach (var nextPosition in nextPositions)
            {
                var nextValue = _grid.GetValue(nextPosition.Item1, nextPosition.Item2);
              
                count += CountValidSequences(piece, nextPosition, workingSequence+nextValue, maxSize);
            }
            return count;
        }

        //Option for returning list of sequences
        private List<string> GenerateValidSequences(IGridTraversal piece, (int, int) currentPosition, string workingSequence, int maxSize)
        {
            /*
             * The first value of the variable workingSequence is the phonekeypad value 
             * the user selects. While the chesspiece next position is inside the grid bounds, 
             * workingSequence is incremented until its length equals the max length 
             * (eg: 7 for phone numbers).
             * The valid sequence is returned if the workingSequence is valid.
             */

            List<string> validSequences = new List<string>();

            if (workingSequence.Length == maxSize)
            {
                if (_configuration.IsValid(workingSequence))
                {
                    validSequences.Add(workingSequence);
                }
                return validSequences;
            }

            // recursive case
            var nextPositions = piece.GetNextPositions(currentPosition);
            foreach (var nextPosition in nextPositions)
            {
                var nextValue = _grid.GetValue(nextPosition.Item1, nextPosition.Item2);
                validSequences.AddRange(GenerateValidSequences(piece, nextPosition, workingSequence + nextValue, maxSize));
            }
            return validSequences;
        }
    }
}