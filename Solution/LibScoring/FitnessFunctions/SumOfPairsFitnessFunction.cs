using LibBioInfo;
using LibBioInfo.Metrics;
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

        private bool _rangeUndefined = true;
        private double _maximum = 0.0;
        private double _minimum = 0.0;

        public SumOfPairsFitnessFunction(IScoringMatrix matrix)
        {
            SumOfPairsScore = new SumOfPairsScore(matrix);
        }

        public override string GetName()
        {
            return $"Sum of Pairs ({SumOfPairsScore.Matrix.GetName()})";
        }

        public override double ScoreAlignment(in char[,] alignment)
        {
            return SumOfPairsScore.ScoreAlignment(alignment);
        }

        public override double GetBestPossibleScore(in char[,] alignment)
        {
            if (_rangeUndefined)
            {
                CalculateRangeEndpoints(alignment);
            }

            return _maximum;
        }

        public override double GetWorstPossibleScore(in char[,] alignment)
        {
            if (_rangeUndefined)
            {
                CalculateRangeEndpoints(alignment);
            }

            return _minimum;
        }

        public override string GetAbbreviation()
        {
            return "SumOfPairs";
        }

        private void CalculateRangeEndpoints(in char[,] alignment)
        {
            _maximum = SumOfPairsScore.GetBestPossibleScore(alignment);
            _minimum = SumOfPairsScore.GetWorstPossibleScore(alignment);
            _rangeUndefined = false;
        }
    }
}
