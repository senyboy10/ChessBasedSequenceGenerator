namespace ChessBasedSequenceGenerator.Interfaces
{
	public interface ISequenceConfiguration
	{
        int Length();
		bool IsValid(string sequence);
	}

    public interface ISequenceRule
    {
        bool IsValid(string sequence);
    }
}