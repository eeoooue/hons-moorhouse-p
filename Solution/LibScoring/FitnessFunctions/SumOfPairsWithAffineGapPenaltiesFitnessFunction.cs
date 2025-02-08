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

        public SumOfPairsWithAffineGapPenaltiesFitnessFunction(IScoringMatrix matrix, double openingCost = 4, double nullCost = 1)
        {
            SumOfPairsScore = new SumOfPairsScore(matrix);
            AffineGapPenalties = new AffineGapPenalties(openingCost, nullCost);
        }
        public override string GetName()
        {
            return $"Sum of Pairs ({SumOfPairsScore.Matrix.GetName()}) Affine Gap Penalties (open={AffineGapPenalties.OpeningCost}, null={AffineGapPenalties.NullCost})";
        }

        public override double GetBestPossibleScore(in char[,] alignment)
        {
            double maxScore = SumOfPairsScore.GetBestPossibleScore(alignment);
            double minPenalty = AffineGapPenalties.GetMinimumPossiblePenalty();
            return maxScore - minPenalty;
        }

        public override double GetWorstPossibleScore(in char[,] alignment)
        {
            double minScore = SumOfPairsScore.GetWorstPossibleScore(alignment);
            double maxPenalty = AffineGapPenalties.GetMaximumPossiblePenalty(alignment);
            return minScore - maxPenalty;
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
    }
}
