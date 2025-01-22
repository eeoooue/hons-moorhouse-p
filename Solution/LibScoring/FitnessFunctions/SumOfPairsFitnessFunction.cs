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

        public int GetNumberOfPossiblePairs()
        {
            throw new NotImplementedException();
        }

        public override double GetBestPossibleScore(char[,] alignment)
        {
            Matrix.GetBestPairwiseScorePossible();

            throw new NotImplementedException();
        }

        public override double GetWorstPossibleScore(char[,] alignment)
        {
            Matrix.GetWorstPairwiseScorePossible();

            throw new NotImplementedException();
        }
    }
}
