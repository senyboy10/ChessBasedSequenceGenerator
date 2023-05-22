namespace ChessBasedSequenceGenerator.Models
{
    public class PhoneKeypad : Board
    {
        public PhoneKeypad() : base(Constants.ROWS, Constants.COLUMNS)
        {
            SetValue(0, 0, '1');
            SetValue(0, 1, '2');
            SetValue(0, 2, '3');
            SetValue(1, 0, '4');
            SetValue(1, 1, '5');
            SetValue(1, 2, '6');
            SetValue(2, 0, '7');
            SetValue(2, 1, '8');
            SetValue(2, 2, '9');
            SetValue(3, 0, '*');
            SetValue(3, 1, '0');
            SetValue(3, 2, '#');
        }

        protected override bool IsValidValue(char value)
        {
            return Constants.VALID_CHARACTERS.Contains(value);
        }
    }

    public class Constants
    { 
        public const int ROWS = 4;
        public const int COLUMNS = 3;
        public const string VALID_CHARACTERS = "01234567890*#";
    }
}