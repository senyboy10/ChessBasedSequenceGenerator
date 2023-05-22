using ChessBasedSequenceGenerator.Interfaces;

namespace ChessBasedSequenceGenerator.Models
{
    public class StandardPhoneNumber : SequenceConfiguration
    {
        public StandardPhoneNumber() :base(CreateDefaultRules())
        {
        }

        private static List<ISequenceRule> CreateDefaultRules()
        {
            return new List<ISequenceRule>
            {
                new LengthRule(7),
                new DoesNotStartWith('0', '1'),
                new ValidCharacters(Enumerable.Range(0, 10)
                    .Select(i => i.ToString()[0]).ToArray())
            };
        }
    }
}