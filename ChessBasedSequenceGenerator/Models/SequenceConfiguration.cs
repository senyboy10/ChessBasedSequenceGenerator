using ChessBasedSequenceGenerator.Interfaces;

namespace ChessBasedSequenceGenerator.Models
{
    public class SequenceConfiguration : ISequenceConfiguration
    {
        private readonly List<ISequenceRule> _rules;

        public SequenceConfiguration(List<ISequenceRule> defaultRules)
        {
            _rules = defaultRules;
        }

        public int Length()
        {
            var lengthRule = _rules.OfType<LengthRule>().FirstOrDefault();
            if (lengthRule != null) return lengthRule.Length;
            throw new InvalidOperationException("No LengthRule found in the rules list");
        }

        public bool IsValid(string sequence)
        {
            return _rules.All(rule => rule.IsValid(sequence));
        }
    }

    public class LengthRule : ISequenceRule
    {
        private readonly int _length;

        public LengthRule(int length)
        {
            _length = length;
        }

        public int Length => _length;

        public bool IsValid(string sequence)
        {
            return sequence.Length == _length;
        }
    }

    public class ValidCharacters : ISequenceRule
    {
        private readonly char[] _validCharacters;

        public ValidCharacters(params char[] validCharacters)
        {
            _validCharacters = validCharacters;
        }

        public bool IsValid(string sequence)
        {
            return sequence.All(ch => _validCharacters.Contains(ch));
        }
    }

    public class DoesNotStartWith : ISequenceRule
    {
        private readonly char[] _invalidStartCharacters;

        public DoesNotStartWith(params char[] invalidStartCharacters)
        {
            _invalidStartCharacters = invalidStartCharacters;
        }

        public bool IsValid(string sequence)
        {
            return !_invalidStartCharacters.Contains(sequence[0]);
        }
    }
}