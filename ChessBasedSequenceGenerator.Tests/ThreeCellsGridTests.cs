using System;
using ChessBasedSequenceGenerator.Factories;
using ChessBasedSequenceGenerator.Interfaces;
using ChessBasedSequenceGenerator.Models;
using ChessBasedSequenceGenerator.Services;

namespace ChessBasedSequenceGenerator.Tests
{
	public class ThreeCellsGridTests
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

        [Test] //One row three cells grid with valid starting phone number characters
        public void OneRowThreeCellsGridWithValidStartingPhoneNumberCharacters()
        {
            const int ROW = 1;
            const int COLUMN = 3;
            const int MAX_LENGTH = 7;

            var board = new Board(ROW, COLUMN);
            board.SetValue(0, 0, '2');
            board.SetValue(0, 1, '3');
            board.SetValue(0, 2, '4');

            _service = new SequenceGeneratorService(board,
                _sequenceConfiguration, _chessPieceFactory);

            #region PAWN TEST Variations: 0
            /*
             * Pawn: In a normal game of chess, a pawn can only move forward 
             * one square (or two on its first move), so it can't move between 
             * cells in a row. Therefore, it generates no sequences in this scenario
             */

            var piece = _chessPieceFactory.Create("pawn", ROW, COLUMN);

            var count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            var count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            var count3 = _service.CountValidSequences(piece, (0, 2), "4", MAX_LENGTH);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
                Assert.That(count3, Is.EqualTo(0));
            });
            #endregion


            #region KNIGHT TEST Variations: 0
            /*
             * The knight moves in an 'L' shape, two squares in one direction 
             * and then one square perpendicular to that, so it also can't move 
             * between cells in a row. Therefore, it also generates no sequences.
             */
            piece = _chessPieceFactory.Create("knight", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (0, 2), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
                Assert.That(count3, Is.EqualTo(0));
            });
            #endregion


            #region BISHOP  Variations: 0
            /*
             *Bishop: Bishops move diagonally, so they also can't move between 
             *cells in a row. They generate no sequences.
             */
            piece = _chessPieceFactory.Create("bishop", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (0, 2), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
                Assert.That(count3, Is.EqualTo(0));
            });
            #endregion

            #region ROOK and QUEEN TEST  Variations: 192 EACH

            piece = _chessPieceFactory.Create("Rook", ROW, COLUMN);
            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (0, 2), "4", MAX_LENGTH);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(64));
                Assert.That(count2, Is.EqualTo(64));
                Assert.That(count3, Is.EqualTo(64));
                Assert.That(count1 + count2 + count3, Is.EqualTo(192));
            });


            piece = _chessPieceFactory.Create("queen", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (0, 2), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(64));
                Assert.That(count2, Is.EqualTo(64));
                Assert.That(count3, Is.EqualTo(64));
                Assert.That(count1 + count2 + count3, Is.EqualTo(192));
            });
            #endregion


            #region KING Variations: 24
            piece = _chessPieceFactory.Create("king", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (0, 2), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(8));
                Assert.That(count2, Is.EqualTo(8));
                Assert.That(count3, Is.EqualTo(8));
                Assert.That(count1 + count2 + count3, Is.EqualTo(24));
            });
            #endregion
        }

        [Test] //Tree cells grid in two rows and two columns
        public void ThreeCellsGridTwoRowsTwoColumnsWithValidStartingPhoneNumberValues()
        {
            const int ROW = 2;
            const int COLUMN = 2;
            const int MAX_LENGTH = 7;

            var board = new Board(ROW, COLUMN);
            board.SetValue(0, 0, '2');
            board.SetValue(0, 1, '3');
            board.SetValue(1, 0, '4');

            _service = new SequenceGeneratorService(board,
                _sequenceConfiguration, _chessPieceFactory);


            #region PAWN TEST Variations: 0
            /*
             * Pawn: limited so still 0. no backward movement
             */

            var piece = _chessPieceFactory.Create("pawn", ROW, COLUMN);

            var count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            var count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            var count3 = _service.CountValidSequences(piece, (1, 0), "4", MAX_LENGTH);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
                Assert.That(count3, Is.EqualTo(0));
            });
            #endregion


            #region KNIGHT TEST Variations: 0
            /*
             * The knight moves in an 'L' shape, two squares in one direction 
             * and then one square perpendicular to that, 
             * Starting from 2 (cell (0,0)): 0 sequences
             * Starting from 3 (cell (0,1)): 0 sequences
             * Starting from 4 (cell (1,0)): 0 sequences
             */
            piece = _chessPieceFactory.Create("knight", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (1, 0), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(0));
                Assert.That(count3, Is.EqualTo(0));
            });
            #endregion


            #region BISHOP  Variations: 2
            /*
             *Starting from 2 (cell (0,0)): 0 sequences
             *Starting from 3 (cell (0,1)): 1 sequence
             *Starting from 4 (cell (1,0)): 1 sequence
             *Total for Bishop: 2 sequences
             */
            piece = _chessPieceFactory.Create("bishop", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (1, 0), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(0));
                Assert.That(count2, Is.EqualTo(1));
                Assert.That(count3, Is.EqualTo(1));
            });
            #endregion

            #region ROOK Variations: 24 EACH
            /*
             * Total for ROOK or Rook: 8 sequences: 
             */

            piece = _chessPieceFactory.Create("Rook", ROW, COLUMN);
            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (1, 0), "4", MAX_LENGTH);

            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(8));
                Assert.That(count2, Is.EqualTo(8));
                Assert.That(count3, Is.EqualTo(8));
                Assert.That(count1 + count2 + count3, Is.EqualTo(24));
            });
            #endregion

            #region QUEEN TEST Variation: 192

            piece = _chessPieceFactory.Create("queen", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (1, 0), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(64));
                Assert.That(count2, Is.EqualTo(64));
                Assert.That(count3, Is.EqualTo(64));
                Assert.That(count1 + count2 + count3, Is.EqualTo(192));
            });
            #endregion


            #region KING Variations: 160

            piece = _chessPieceFactory.Create("king", ROW, COLUMN);

            count1 = _service.CountValidSequences(piece, (0, 0), "2", MAX_LENGTH);
            count2 = _service.CountValidSequences(piece, (0, 1), "3", MAX_LENGTH);
            count3 = _service.CountValidSequences(piece, (0, 2), "4", MAX_LENGTH);
            Assert.Multiple(() =>
            {
                Assert.That(count1, Is.EqualTo(64));
                Assert.That(count2, Is.EqualTo(64));
                Assert.That(count3, Is.EqualTo(32));
                Assert.That(count1 + count2 + count3, Is.EqualTo(160));
            });
            #endregion
        }
    }
}

