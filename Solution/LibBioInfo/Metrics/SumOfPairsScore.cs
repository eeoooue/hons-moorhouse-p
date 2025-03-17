using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.Metrics
{
    public class SumOfPairsScore
    {
        public IScoringMatrix Matrix;

        private const string Characters = "CSTAGPDEQNHRKMILVWYFBZX-";
        private Dictionary<char, int> CharacterIndices = new Dictionary<char, int>();

        public SumOfPairsScore(IScoringMatrix matrix)
        {
            Matrix = matrix;
            for(int i=0; i< Characters.Length; i++)
            {
                char x = Characters[i];
                CharacterIndices[x] = i;
            }
        }

        public double ScoreAlignment(in char[,] alignment)
        {
            int n = alignment.GetLength(1);

            double result = 0;
            for (int j = 0; j < n; j++)
            {
                result += ScoreColumn(alignment, j);
            }

            return result;
        }

        public double GetBestPossibleScore(in char[,] alignment)
        {
            double bestScore = Matrix.GetBestPairwiseScorePossible();
            int possiblePairs = GetNumberOfPossiblePairs(alignment);
            return possiblePairs * bestScore;
        }

        public double GetWorstPossibleScore(in char[,] alignment)
        {
            double worstScore = Matrix.GetWorstPairwiseScorePossible();
            int possiblePairs = GetNumberOfPossiblePairs(alignment);
            return possiblePairs * worstScore;
        }

        private double ScoreColumn(in char[,] alignment, int j)
        {
            int[] counts = ConstructCounterArrayForColumn(alignment, j);
            double result = ScorePairwiseCombinations(counts);

            return result;
        }

        private int[] ConstructCounterArrayForColumn(in char[,] matrix, int j)
        {
            int[] result = new int[Characters.Length];

            int m = matrix.GetLength(0);
            for (int i = 0; i < m; i++)
            {
                char x = matrix[i, j];
                int index = CharacterIndices[x];
                result[index]++;
            }

            return result;
        }

        private double ScorePairwiseCombinations(int[] counts)
        {
            double result = 0;

            for (int i = 0; i < Characters.Length; i++)
            {
                char a = Characters[i];
                int a_count = counts[i];

                for (int j = i + 1; j < Characters.Length; j++)
                {
                    char b = Characters[j];
                    int b_count = counts[j];

                    int combinations = a_count * b_count;
                    int score = Matrix.ScorePair(a, b);
                    int contribution = score * combinations;
                    result += contribution;
                }

                if (a_count > 1)
                {
                    int combinations = a_count * (a_count - 1) / 2;
                    int score = Matrix.ScorePair(a, a);
                    int contribution = score * combinations;
                    result += contribution;
                }
            }

            return result;
        }

        private int GetNumberOfPossiblePairs(in char[,] alignment)
        {
            int m = alignment.GetLength(0);
            int n = GetNumberOfResiduesInFirstRow(alignment);
            int pairsPerColumn = GetPossiblePairsInColumnOfHeight(m);
            return pairsPerColumn * n;
        }

        private int GetNumberOfResiduesInFirstRow(in char[,] alignment)
        {
            int n = alignment.GetLength(1);
            int total = 0;

            for (int j = 0; j < n; j++)
            {
                if (alignment[0, j] != Bioinformatics.GapCharacter)
                {
                    total++;
                }
            }

            return total;
        }

        private int GetPossiblePairsInColumnOfHeight(int n)
        {
            if (n < 2)
            {
                return 0;
            }

            int permutations = n * (n - 1);
            int combinations = permutations / 2;
            return combinations;
        }
    }
}
