using LibScoring.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{
    public class SumOfPairsFitnessFunction : NormalisedFitnessFunction
    {
        SumOfPairsScore SumOfPairsScore;

        public SumOfPairsFitnessFunction(IScoringMatrix matrix)
        {
            SumOfPairsScore = new SumOfPairsScore(matrix);
        }

        public override string GetName()
        {
            return $"Sum of Pairs ({SumOfPairsScore.Matrix.GetName()})";
        }

        public override double ScoreAlignment(char[,] alignment)
        {
            return SumOfPairsScore.ScoreAlignment(alignment);
        }

        public override double GetBestPossibleScore(char[,] alignment)
        {
            return SumOfPairsScore.GetBestPossibleScore(alignment);
        }

        public override double GetWorstPossibleScore(char[,] alignment)
        {
            return SumOfPairsScore.GetWorstPossibleScore(alignment);
        }
    }
}
