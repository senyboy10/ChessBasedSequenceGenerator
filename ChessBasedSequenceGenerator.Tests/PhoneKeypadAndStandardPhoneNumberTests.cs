using System;
using System.Data.Common;
using System.Diagnostics.Metrics;
using ChessBasedSequenceGenerator.Factories;
using ChessBasedSequenceGenerator.Interfaces;
using ChessBasedSequenceGenerator.Models;
using ChessBasedSequenceGenerator.Services;

namespace ChessBasedSequenceGenerator.Tests
{
	public class PhoneKeypadAndStandardPhoneNumberTests
    {
        private SequenceGeneratorService _service;
    
        [SetUp]
        public void Setup()
        {
            IGrid phoneKeypad = new PhoneKeypad();
            IGridTraversalFactory chessPieceFactory = new ChessPieceFactory();
            ISequenceConfiguration phoneNumberSequence = new StandardPhoneNumber();
            _service = new SequenceGeneratorService(phoneKeypad, phoneNumberSequence, chessPieceFactory);
        }

        [Test] // Starting from 3
        public void PhoneKeyPadStartingWithThree()
        {
            #region PAWN TEST Variations: 0
            var count = _service.CountValidSequences("pawn", '3');
            Assert.That(count, Is.EqualTo(0));
            #endregion

            #region KNIGHT TEST Variations: 136
            count = _service.CountValidSequences("knight", '3');
            Assert.That(count, Is.EqualTo(136));
            #endregion


            #region BISHOP  Variations: 415
            count = _service.CountValidSequences("bishop", '3');
            Assert.That(count, Is.EqualTo(415));
            #endregion

            #region ROOK Variations: 5595
            count = _service.CountValidSequences("rook", '3');
            Assert.That(count, Is.EqualTo(5595));
            #endregion

            #region QUEEN TEST Variation: 84898
            count = _service.CountValidSequences("queen", '3');
            Assert.That(count, Is.EqualTo(84898));
            #endregion

            #region KING Variations: 10811
            count = _service.CountValidSequences("king", '3');
            Assert.That(count, Is.EqualTo(10811));
            #endregion
        }
    }
}