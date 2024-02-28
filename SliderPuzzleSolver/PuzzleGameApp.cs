using System;

public class PuzzleGameApp
{
    private PuzzleBoard board;

    public PuzzleGameApp(int size)
    {
        board = new PuzzleBoard(size);
    }

    public void ShufflePuzzle()
    {
        board.Shuffle();
    }

    public void Play()
    {
        Console.WriteLine("Welcome to the Puzzle Solving Game!");
        Console.WriteLine("Instructions: Use W/A/S/D to move empty space, R to reset, and Q to quit.");

        while (true)
        {
            board.Display();
            Console.Write("Enter move: ");
            char move = char.ToUpper(Console.ReadKey().KeyChar); // Make move input case-insensitive
            Console.Clear(); // Clear console only when necessary

            if (move == 'Q' || board.IsSolved())
            {
                break;
            }

            if (move == 'R')
            {
                board.Reset();
                Console.WriteLine("Puzzle reset successfully.");
            }
            else
            {
                if (board.MoveTile(move))
                {
                    board.DisplaySolutionMoves();
                }
                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }
        }

        if (board.IsSolved())
        {
            Console.WriteLine("Congratulations! Puzzle solved!");
            int score = Scorer.CalculateScore(board.MoveCount);
            Console.WriteLine($"Score: {score}");
        }
        else
        {
            Console.WriteLine("Quitting the game.");
        }
    }
}
