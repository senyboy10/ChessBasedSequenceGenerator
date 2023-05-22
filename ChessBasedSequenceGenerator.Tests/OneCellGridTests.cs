using System;
using ChessBasedSequenceGenerator.Factories;
using ChessBasedSequenceGenerator.Interfaces;
using System.Data;
using ChessBasedSequenceGenerator.Models;
using ChessBasedSequenceGenerator.Services;

namespace ChessBasedSequenceGenerator.Tests
{
	public class OnceCellGridTests
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

        [Test]
        public void OnceCellGridWithStandPhoneNumber()
        {
            var board = new Board(1, 1);
            board.SetValue(0, 0, '1');

            _service = new SequenceGeneratorService(board,
                _sequenceConfiguration, _chessPieceFactory);


            Assert.Multiple(() =>
            {
                Assert.That(board.GetRowCount(), Is.EqualTo(1));
                Assert.That(board.GetColumnCount(), Is.EqualTo(1));
                Assert.That(board.GetValue(0, 0), Is.EqualTo('1'));
            });

            var rook = _chessPieceFactory.Create("Rook", 1, 1);
            var count1 = _service.CountValidSequences(rook, (0, 0), "1", 7);
            Assert.That(count1, Is.EqualTo(0));
          
            board.SetValue(0, 0, '3');
            count1 = _service.CountValidSequences(rook, (0, 0), "3", 7);
            Assert.That(count1, Is.EqualTo(0));       
        }
    }
}

