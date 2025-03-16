using LibBioInfo;
using LibBioInfo.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{
    public class SumOfPairsWithAffineGapPenaltiesFitnessFunction : NormalisedFitnessFunction
    {
        SumOfPairsScore SumOfPairsScore;
        AffineGapPenalties AffineGapPenalties;

        private bool _rangeUndefined = true;
        private double _maximum = 0.0;
        private double _minimum = 0.0;

        public SumOfPairsWithAffineGapPenaltiesFitnessFunction(IScoringMatrix matrix, double openingCost = 4, double nullCost = 1)
        {
            SumOfPairsScore = new SumOfPairsScore(matrix);
            AffineGapPenalties = new AffineGapPenalties(openingCost, nullCost);
        }

        public override string GetName()
        {
            return $"Sum of Pairs ({SumOfPairsScore.Matrix.GetName()}) Affine Gap Penalties (open={AffineGapPenalties.OpeningCost}) (null={AffineGapPenalties.NullCost})";
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

        public override double ScoreAlignment(in char[,] alignment)
        {
            double score = SumOfPairsScore.ScoreAlignment(alignment);
            double penalty = AffineGapPenalties.GetPenaltyForAlignmentMatrix(alignment);
            return score - penalty;
        }

        public override string GetAbbreviation()
        {
            return "SumOfPairs+AGP";
        }


        private void CalculateRangeEndpoints(in char[,] alignment)
        {
            _maximum = CalculateBestPossibleScore(alignment);
            _minimum = CalculateWorstPossibleScore(alignment);
            _rangeUndefined = false;
        }

        private double CalculateBestPossibleScore(in char[,] alignment)
        {
            double maxScore = SumOfPairsScore.GetBestPossibleScore(alignment);
            double minPenalty = AffineGapPenalties.GetMinimumPossiblePenalty();
            return maxScore - minPenalty;
        }

        private double CalculateWorstPossibleScore(in char[,] alignment)
        {
            double minScore = SumOfPairsScore.GetWorstPossibleScore(alignment);
            double maxPenalty = AffineGapPenalties.GetMaximumPossiblePenalty(alignment);
            return minScore - maxPenalty;
        }
    }
}
