using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{
    public class SumOfPairsFitnessFunction : NormalisedFitnessFunction
    {
        IScoringMatrix Matrix;

        public SumOfPairsFitnessFunction(IScoringMatrix matrix)
        {
            Matrix = matrix;
        }

        public override string GetName()
        {
            return $"Sum of Pairs ({Matrix.GetName()})";
        }

        public override double ScoreAlignment(char[,] alignment)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfPossiblePairs(char[,] alignment)
        {
            int m = alignment.GetLength(0);
            int n = alignment.GetLength(1);
            int pairsPerColumn = GetPossiblePairsInColumnOfHeight(m);
            return pairsPerColumn * n;
        }

        public int GetPossiblePairsInColumnOfHeight(int n)
        {
            if (n < 2)
            {
                return 0;
            }

            int permutations = n * (n - 1);
            int combinations = permutations / 2;
            return combinations;
        }

        public override double GetBestPossibleScore(char[,] alignment)
        {
            double bestScore = Matrix.GetBestPairwiseScorePossible();
            int possiblePairs = GetNumberOfPossiblePairs(alignment);
            return possiblePairs * bestScore;
        }

        public override double GetWorstPossibleScore(char[,] alignment)
        {
            double worstScore = Matrix.GetWorstPairwiseScorePossible();
            int possiblePairs = GetNumberOfPossiblePairs(alignment);
            return possiblePairs * worstScore;
        }
    }
}
