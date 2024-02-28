using System;

class Program
{
    static void Main()
    {
        PuzzleGameApp game = new PuzzleGameApp(3);
        game.ShufflePuzzle(); // Shuffle the puzzle initially
        game.Play();
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}
