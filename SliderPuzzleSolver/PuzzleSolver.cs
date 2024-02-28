using System;
using System.Collections.Generic;

public class PuzzleSolver
{
    public static List<char> SolvePuzzle(PuzzleBoard board)
    {
        List<char> moves = new List<char>();

        // Skip puzzle solvability check for brevity
        // Implement puzzle solvability check if needed

        SortedSet<Node> openSet = new SortedSet<Node>(Comparer<Node>.Create((node1, node2) =>
        {
            int fScore1 = node1.Cost + node1.Heuristic;
            int fScore2 = node2.Cost + node2.Heuristic;

            return fScore1.CompareTo(fScore2);
        }));
        HashSet<Node> closedSet = new HashSet<Node>();
        Node startNode = new Node(board, null, 0, CalculateHeuristic(board), '\0');
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.Min;
            openSet.Remove(currentNode);

            if (currentNode.Board.IsSolved())
            {
                moves = ReconstructPath(currentNode);
                break;
            }

            closedSet.Add(currentNode);

            foreach (char move in currentNode.Board.GetPossibleMoves())
            {
                PuzzleBoard nextBoard = new PuzzleBoard(currentNode.Board);
                nextBoard.MoveTile(move);
                Node neighbor = new Node(nextBoard, currentNode, currentNode.Cost + 1, CalculateHeuristic(nextBoard), move);
                if (closedSet.Contains(neighbor))
                    continue;

                int tentativeGScore = currentNode.Cost + 1;

                if (openSet.Contains(neighbor) && tentativeGScore >= neighbor.Cost)
                    continue;

                neighbor.Cost = tentativeGScore;

                if (!openSet.Contains(neighbor))
                    openSet.Add(neighbor);
            }
        }

        return moves;
    }

    private static List<char> ReconstructPath(Node currentNode)
    {
        List<char> path = new List<char>();

        while (currentNode.Parent != null)
        {
            path.Insert(0, currentNode.Move);
            currentNode = currentNode.Parent;
        }

        return path;
    }

    private static int CalculateHeuristic(PuzzleBoard board)
    {
        int heuristic = 0;

        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                int value = board.GetTileValue(i, j);

                if (value != 0)
                {
                    int targetRow = (value - 1) / board.Size;
                    int targetCol = (value - 1) % board.Size;

                    heuristic += Math.Abs(i - targetRow) + Math.Abs(j - targetCol);
                }
            }
        }

        return heuristic;
    }

    private class Node : IComparable<Node>
    {
        public PuzzleBoard Board { get; }
        public Node Parent { get; }
        public int Cost { get; set; }
        public int Heuristic { get; }
        public char Move { get; }

        public Node(PuzzleBoard board, Node parent, int cost, int heuristic, char move)
        {
            Board = board;
            Parent = parent;
            Cost = cost;
            Heuristic = heuristic;
            Move = move;
        }

        public int CompareTo(Node other)
        {
            int fScore = Cost + Heuristic;
            int otherFScore = other.Cost + other.Heuristic;

            return fScore.CompareTo(otherFScore);
        }
    }
}
