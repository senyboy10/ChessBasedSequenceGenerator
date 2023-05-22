using ChessBasedSequenceGenerator.Factories;
using ChessBasedSequenceGenerator.Models;
using ChessBasedSequenceGenerator.Services;

Console.WriteLine("Welcome to the Chess-Based Phone Number Generator!");

Board phoneKeyBad = new PhoneKeypad();
ChessPieceFactory chessPieceFactory = new();
SequenceConfiguration phoneNumberSequence = new StandardPhoneNumber();
var service = new SequenceGeneratorService(phoneKeyBad, phoneNumberSequence, chessPieceFactory);
int maxRow = phoneKeyBad.GetRowCount() - 1;
int maxCol = phoneKeyBad.GetColumnCount() - 1;

while (true)
{
    Console.WriteLine("Please enter a chess piece (rook, bishop, king, queen, "
        + "knight, pawn) or 'exit' to quit:");

    var pieceName = Console.ReadLine()?.ToLower();

    if (string.IsNullOrEmpty(pieceName))
    {
        Console.WriteLine($"invalid piece name: {pieceName}");
        continue;
    }

    if (pieceName == "exit")
    {
        break;
    }

    Console.WriteLine($"Please enter a starting position as a grid coordinate "
        + $"(e.g. '0,2') " + $"within the grid bounds of (0,0) to ({maxRow}," +
        $"{maxCol}), " + $"or a phone keypad number (like 3):");

    var positionInput = Console.ReadLine();

    // Try to parse as a phone keypad number
    if (char.TryParse(positionInput, out var keypadNum))
    {
        try
        {
            var count = service.CountValidSequences(pieceName, keypadNum);

            Console.WriteLine($"==> Generated {count} sequences for {pieceName} " +
                $"starting from keypad number {keypadNum}");
            continue;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            continue;
        }
    }

    // Otherwise, parse as a grid coordinate
    var positionParts = positionInput?.Split(",");
    if (positionParts?.Length != 2 || !int.TryParse(positionParts[0], out var row) ||
        !int.TryParse(positionParts[1], out var col))
    {
        Console.WriteLine("Invalid position input. Please enter again.");
        continue;
    }

    if (row < 0 || row > maxRow || col < 0 || col > maxCol)
    {
        Console.WriteLine("Position is out of grid bounds. Please enter again.");
        continue;
    }

    try
    {
        var traversal = chessPieceFactory.Create(pieceName, maxRow + 1, maxCol + 1);
        var positionValue = phoneKeyBad.GetValue(row, col);
        var count = service.CountValidSequences(traversal, (row, col), positionValue.ToString(), phoneNumberSequence.Length());
        Console.WriteLine($"==> Generated {count} sequences for {pieceName} starting from position ({row},{col})");
    }
    catch (Exception e)
    {
        Console.WriteLine($"An error occurred: {e.Message}");
    }
}
