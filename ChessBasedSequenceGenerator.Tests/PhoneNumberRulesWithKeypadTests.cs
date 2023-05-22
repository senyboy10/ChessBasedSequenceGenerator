using ChessBasedSequenceGenerator.Models;
using ChessBasedSequenceGenerator.Services;
using ChessBasedSequenceGenerator.Interfaces;
using ChessBasedSequenceGenerator.Factories;

namespace ChessBasedSequenceGenerator.Tests
{
    public class PhoneNumberRulesWithKeypadTests
    {
        private PhoneKeypad _phoneKeypad;
        private SequenceConfiguration _sequenceConfiguration;
        private ChessPieceFactory _chessPieceFactory;
        private SequenceGeneratorService _sequenceGeneratorService;
        private List<ISequenceRule> _rules;

        [SetUp]
        public void Setup()
        {
            _phoneKeypad = new PhoneKeypad();
            _rules = new List<ISequenceRule> {
                new LengthRule(7),
                new DoesNotStartWith('0', '1'),
                new ValidCharacters(Enumerable.Range(0, 10)
                    .Select(i => i.ToString()[0]).ToArray())
            };
            _sequenceConfiguration = new SequenceConfiguration(_rules);
            _chessPieceFactory = new ChessPieceFactory();
            _sequenceGeneratorService = new SequenceGeneratorService(_phoneKeypad, _sequenceConfiguration, _chessPieceFactory);
        }

        // Tests for the PhoneKeypad class
        [Test]
        public void TestPhoneKeypadInitialization()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_phoneKeypad.GetRowCount(), Is.EqualTo(Constants.ROWS));
                Assert.That(_phoneKeypad.GetColumnCount(), Is.EqualTo(Constants.COLUMNS));
                Assert.That(_phoneKeypad.GetValue(0, 0), Is.EqualTo('1'));
            });
            Assert.That(_phoneKeypad.GetValue(3, 2), Is.EqualTo('#'));
        }

        // Tests for the SequenceConfiguration class
        [Test]
        public void TestSequenceConfigurationInitialization()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_sequenceConfiguration.Length(), Is.EqualTo(7));
                Assert.That(_sequenceConfiguration.IsValid("1234567"), Is.False);
            });
            Assert.Multiple(() =>
            {
                Assert.That(_sequenceConfiguration.IsValid("12345678"), Is.False);
                Assert.That(_sequenceConfiguration.IsValid("123456"), Is.False);
            });
        }

        // Tests for the LengthRule class
        [Test]
        public void TestLengthRuleInitialization()
        {
            LengthRule lengthRule = new(5);
            Assert.Multiple(() =>
            {
                Assert.That(lengthRule.Length, Is.EqualTo(5));
                Assert.That(lengthRule.IsValid("12345"), Is.True);
                Assert.That(lengthRule.IsValid("123456"), Is.False);
            });
        }

        // Tests for the ValidCharacters class
        [Test]
        public void TestValidCharactersInitialization()
        {
            ValidCharacters validCharacters = new('1', '2', '3');
            Assert.Multiple(() =>
            {
                Assert.That(validCharacters.IsValid("123"));
                Assert.That(validCharacters.IsValid("1234"), Is.False);
            });
        }

        // Tests for the DoesNotStartWith class
        [Test]
        public void TestDoesNotStartWithInitialization()
        {
            DoesNotStartWith doesNotStartWith = new('0', '1');
            Assert.Multiple(() =>
            {
                Assert.That(doesNotStartWith.IsValid("2345"), Is.True);
                Assert.That(doesNotStartWith.IsValid("1234"), Is.False);
            });
        }
    }
}
