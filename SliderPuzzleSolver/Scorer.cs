public class Scorer
{
    public static int CalculateScore(int moveCount)
    {
        int maxScore = 100;
        int minMoves = 10;

        if (moveCount <= minMoves)
        {
            return maxScore;
        }
        else
        {
            double scorePercentage = 1.0 - ((double)(moveCount - minMoves) / (double)minMoves);
            int score = (int)(scorePercentage * maxScore);

            return score;
        }
    }
}
