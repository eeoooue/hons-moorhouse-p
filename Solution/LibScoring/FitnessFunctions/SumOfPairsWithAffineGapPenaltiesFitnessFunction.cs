﻿using LibScoring.Metrics;
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

        public override double GetBestPossibleScore(char[,] alignment)
        {
            double maxScore = SumOfPairsScore.GetBestPossibleScore(alignment);
            double minPenalty = AffineGapPenalties.GetMinimumPossiblePenalty();
            return maxScore - minPenalty;
        }

        public override double GetWorstPossibleScore(char[,] alignment)
        {
            double minScore = SumOfPairsScore.GetWorstPossibleScore(alignment);
            double maxPenalty = AffineGapPenalties.GetMaximumPossiblePenalty(alignment);
            return minScore - maxPenalty;
        }

        public override double ScoreAlignment(char[,] alignment)
        {
            double score = SumOfPairsScore.ScoreAlignment(alignment);
            double penalty = AffineGapPenalties.GetPenaltyForAlignmentMatrix(alignment);
            return score - penalty;
        }
    }
}
