using System;
using ChessBasedSequenceGenerator.Factories;
using ChessBasedSequenceGenerator.Interfaces;
using System.Data;
using ChessBasedSequenceGenerator.Models;
using ChessBasedSequenceGenerator.Services;

namespace ChessBasedSequenceGenerator.Tests
{
	public class TwoCellsGridTests
	{
        private SequenceConfiguration _sequenceConfiguration;
        private ChessPieceFactory _chessPieceFactory;
        private SequenceGeneratorService? _service;
        private List<ISequenceRule> _rules;

        [SetUp]
        public void Setup()
        {
            _rules = new List<ISequenceRule> {
                new LengthRule(7),
                new DoesNotStartWith('0', '1'),
                new ValidCharacters(Enumerable.Range(0, 10)
                    .Select(i => i.ToString()[0]).ToArray())
            };
            _sequenceConfiguration = new SequenceConfiguration(_rules);
            _chessPieceFactory = new ChessPieceFactory();
        }

        [Test] //Two cells grid with valid starting phone number characters
        public void TwoCellsGridWithValidStartingPhoneNumbers()
        {
            var board = new Board(1, 2);
            board.SetValue(0, 0, '2');
            board.SetValue(0, 1, '3');

            _service = new SequenceGeneratorService(board,
                _sequenceConfiguration, _chessPieceFactory);


            #region BOARD TEST
            Assert.Multiple(() =>
            {
                Assert.That(board.GetRowCount(), Is.EqualTo(1));
                Assert.That(board.GetColumnCount(), Is.EqualTo(2));
                Assert.That(board.GetValue(0, 0), Is.EqualTo('2'));
            });
            #endregion


            #region PAWN TEST Variations: 0
            /*
             * A pawn in a standard chess game can only move forward, 
             * so it cannot move back and forth between two cells in the same row. 
             * Therefore, it cannot generate a sequence of length 7 in this scenario. Variations: 0
             */

            var piece = _chessPieceFactory.Create("pawn", 1, 2);

            var count1 = _service.CountValidSequences(piece, (0, 0), "2", 7);
            var count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
            });
            #endregion


            #region KNIGHT TEST Variations: 0
            /*
             * Knight: A knight moves in an L shape (two squares in one 
             * direction, and one square in a perpendicular direction), 
             * so it cannot move back and forth between two cells in the 
             * same row either. 
             */
            piece = _chessPieceFactory.Create("knight", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
            });
            #endregion


            #region BISHOP  Variations: 0
            /*
             * Bishop: A bishop can only move diagonally, 
             * and so it cannot move back and forth between two cells in the same row.
             */
            piece = _chessPieceFactory.Create("bishop", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
            });
            #endregion

            #region ROOK TEST  Variations: 2
            /*
             * Rook: As discussed earlier, a rook can move back and forth between two cells in the same row. 
             * There are two variations of sequences of length 7, one starting with each cell.
             */
            piece = _chessPieceFactory.Create("Rook", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(1));
                Assert.That(count2, Is.EqualTo(1));
            });

            #endregion

            #region QUEEN Variations: 2
            /*
             * Queen: A queen, having the combined moves of a rook and a bishop, 
             * can also move back and forth between two cells in the same row, 
             * generating the same sequences as the rook.
             */

            piece = _chessPieceFactory.Create("queen", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(1));
                Assert.That(count2, Is.EqualTo(1));
            });
            #endregion

            #region KING Variations: 2
            /*
             * King: A king can move one square in any direction, so it can move
             * back and forth between two cells in the same row, generating the
             * same sequences as the rook and queen. 
             */
            piece = _chessPieceFactory.Create("king", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(1));
                Assert.That(count2, Is.EqualTo(1));
            });
            #endregion
        }


        [Test] //Two cells grid with one INVALID starting phone number character
        public void TwoCellsGridWithValidKeypadCharacters()
        {
            var board = new Board(1, 2);
            board.SetValue(0, 0, '1');
            board.SetValue(0, 1, '3');

            _service = new SequenceGeneratorService(board,
                _sequenceConfiguration, _chessPieceFactory);

            #region PAWN TEST Variations: 0
            /*
             * A pawn in a standard chess game can only move forward, 
             * so it cannot move back and forth between two cells in the same row. 
             * Therefore, it cannot generate a sequence of length 7 in this scenario. Variations: 0
             */

            var piece = _chessPieceFactory.Create("pawn", 1, 2);

            var count1 = _service.CountValidSequences(piece, (0, 0), "1", 7);
            var count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
            });
            #endregion


            #region KNIGHT TEST Variations: 0
            /*
             * Knight: A knight moves in an L shape (two squares in one 
             * direction, and one square in a perpendicular direction), 
             * so it cannot move back and forth between two cells in the 
             * same row either. 
             */
            piece = _chessPieceFactory.Create("knight", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "1", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
            });
            #endregion


            #region BISHOP  Variations: 0
            /*
             * Bishop: A bishop can only move diagonally, 
             * and so it cannot move back and forth between two cells in the same row.
             */
            piece = _chessPieceFactory.Create("bishop", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "1", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
            });
            #endregion

            #region ROOK TEST  Variations: 1
            /*
             * Rook: As discussed earlier, a rook can move back and forth between two cells in the same row. 
             * There are two variations of sequences of length 7, one starting with each cell.
             */
            piece = _chessPieceFactory.Create("Rook", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "1", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(1));
            });

            #endregion

            #region QUEEN Variations: 2
            /*
             * Queen: A queen, having the combined moves of a rook and a bishop, 
             * can also move back and forth between two cells in the same row, 
             * generating the same sequences as the rook.
             */

            piece = _chessPieceFactory.Create("queen", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "1", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(1));
            });
            #endregion

            #region KING Variations: 1
            /*
             * King: A king can move one square in any direction, so it can move
             * back and forth between two cells in the same row, generating the
             * same sequences as the rook and queen. 
             */
            piece = _chessPieceFactory.Create("king", 1, 2);

            count1 = _service.CountValidSequences(piece, (0, 0), "1", 7);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", 7);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(1));
            });
            #endregion
        }
    }

}