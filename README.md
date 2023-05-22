# Chess Based Sequence Generator

The Chess Based Sequence Generator is a .NET console application that generates sequences
of moves based on the movement rules of different chess pieces.
The sequences are generated using the layout of a phone keypad,
modeled as a 4x3 grid, with each key on the keypad representing a square on the chessboard.

## Getting Started


### Prerequisites
- - .NET Core SDK
- I used .NET 7 for this project
- Visual Studio, Visual Studio Code

### Installing

1. Clone the GitHub repository to your local machine: `git clone https://github.com/senyboy10/ChessBasedSequenceGenerator.git`
2. Navigate to the root directory of the project where the `.csproj` file is located.
3. Run the command `dotnet build` to build the project.This will restore any necessary NuGet packages, compile the project, and create an executable file.
4. Run the command `dotnet run` to start the program.

## Usage

After running the program, follow the instructions provided in the console to interact with it.
You will be asked to input a chess piece and either a starting position on the phone keypad or
a phone keypad value. The program will then generate valid sequences of moves for that chess piece.


## Project Structure

The project contains several key classes and interfaces:

1. `SequenceGeneratorService`: This service class is responsible for generating sequences
    based on the chess piece movement rules. It uses `IGridTraversalFactory` to create a specific
    chess piece object and then generates valid sequences starting from a specified value on the grid.

2. `ChessPieceFactory`: This class implements the `IGridTraversalFactory` interface and is responsible
    for creating specific chess piece objects, such as Rook, Queen, King, Knight, Bishop, and Pawn.

3. `PhoneKeypad`: This class represents a specific implementation of `IGrid`. It is designed to represent
    the phone keypad grid on which the chess pieces move to generate sequences.

4. `StandardPhoneNumber`: This is a specific implementation of `ISequenceConfiguration`.
    It represents the configuration for a standard phone number sequence.

## Example Usage

Here is an example usage of these classes:

```csharp
// Create chess piece factory
ChessPieceFactory chessPieceFactory = new ChessPieceFactory();

// Create phone keypad
PhoneKeypad phoneKeypad = new PhoneKeypad();

// Create standard phone number sequence configuration
StandardPhoneNumber standardPhoneNumber = new StandardPhoneNumber();

// Create sequence generator service
SequenceGeneratorService service = new SequenceGeneratorService(phoneKeypad, standardPhoneNumber, chessPieceFactory);

// Count valid sequences for a knight starting from the '5' key on the phone keypad
int count = service.CountValidSequences("knight", '5');

Console.WriteLine($"Count of valid sequences: {count}");
