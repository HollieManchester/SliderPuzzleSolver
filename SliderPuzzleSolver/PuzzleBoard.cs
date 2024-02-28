using System;
using System.Collections.Generic;

public class PuzzleBoard
{
    private int[,] puzzle;
    private int emptyRow;
    private int emptyCol;
    private Random random;
    private char lastMove;
    private List<char> solutionMoves;
    private int moveCount;
    private PuzzleBoard board;

    public int Size { get; private set; }
    public int EmptyRow { get { return emptyRow; } }
    public int EmptyCol { get { return emptyCol; } }
    public int MoveCount { get { return moveCount; } }

    public PuzzleBoard(int size)
    {
        Size = size;
        puzzle = new int[size, size];
        random = new Random();
        solutionMoves = new List<char>();
        Initialize();
    }

    public PuzzleBoard(PuzzleBoard board)
    {
        this.board = board;
    }

    public int GetTileValue(int row, int col)
    {
        return puzzle[row, col];
    }

    private void Initialize()
    {
        int count = 1;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                puzzle[i, j] = count++;
            }
        }

        emptyRow = Size - 1;
        emptyCol = Size - 1;
        puzzle[emptyRow, emptyCol] = 0;
        moveCount = 0;
    }

    public void Shuffle()
    {
        int maxPossibleMoves = 4;
        int moves = Size * 50;

        for (int i = 0; i < moves; i++)
        {
            char[] possibleMoves = GetPossibleMoves();

            if (possibleMoves.Length > 0)
            {
                char randomMove = possibleMoves[random.Next(possibleMoves.Length)];
                MoveTile(randomMove);
            }
            else
            {
                Console.WriteLine("Error: Unable to shuffle the puzzle further. Please retry.");
                break;
            }
        }
    }

    public bool MoveTile(char move)
    {
        int newEmptyRow = emptyRow;
        int newEmptyCol = emptyCol;

        switch (move)
        {
            case 'W':
                newEmptyRow--;
                break;
            case 'A':
                newEmptyCol--;
                break;
            case 'S':
                newEmptyRow++;
                break;
            case 'D':
                newEmptyCol++;
                break;
            default:
                return false;
        }

        if (IsValidMove(newEmptyRow, newEmptyCol))
        {
            SwapTiles(newEmptyRow, newEmptyCol);
            lastMove = move;
            moveCount++;
            solutionMoves.Add(move);
            return true;
        }

        return false;
    }

    public bool IsValidMove(int row, int col)
    {
        return row >= 0 && row < Size && col >= 0 && col < Size;
    }

    public void SwapTiles(int row, int col)
    {
        int temp = puzzle[row, col];
        puzzle[row, col] = puzzle[emptyRow, emptyCol];
        puzzle[emptyRow, emptyCol] = temp;
        emptyRow = row;
        emptyCol = col;
    }

    public bool IsSolved()
    {
        int count = 1;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (puzzle[i, j] != count++ && !(i == Size - 1 && j == Size - 1))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Display()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (puzzle[i, j] == 0)
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.Write($"{puzzle[i, j]} ");
                }
            }
            Console.WriteLine();
        }
    }

    public bool IsOppositeMove(char move)
    {
        return (lastMove == 'W' && move == 'S') ||
               (lastMove == 'A' && move == 'D') ||
               (lastMove == 'S' && move == 'W') ||
               (lastMove == 'D' && move == 'A');
    }

    public char[] GetPossibleMoves()
    {
        List<char> moves = new List<char>();

        if (emptyRow > 0) moves.Add('W');
        if (emptyCol > 0) moves.Add('A');
        if (emptyRow < Size - 1) moves.Add('S');
        if (emptyCol < Size - 1) moves.Add('D');

        return moves.ToArray();
    }

    public void DisplaySolutionMoves()
    {
        if (solutionMoves.Count > 0)
        {
            Console.WriteLine("Moves Needed:");

            for (int i = 0; i < solutionMoves.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {GetMoveDescription(solutionMoves[i])}");
            }

            Console.WriteLine("\nPuzzle Board:");
            Display();
        }
        else
        {
            Console.WriteLine("No solution moves recorded.");
        }
    }

    private string GetMoveDescription(char move)
    {
        switch (move)
        {
            case 'W':
                return "Move up";
            case 'A':
                return "Move left";
            case 'S':
                return "Move down";
            case 'D':
                return "Move right";
            default:
                return "Invalid move";
        }
    }

    public void Reset()
    {
        Initialize();
        Shuffle();
        moveCount = 0;
        solutionMoves.Clear();
    }
}
