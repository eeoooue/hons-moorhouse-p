using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ObjectiveFunctions
{
    public class SumOfPairsWithAffineGapPenaltiesObjectiveFunction : IObjectiveFunction
    {
        public SumOfPairsObjectiveFunction SumOfPairsOF;
        public AffineGapPenaltyObjectiveFunction AffineGapPenaltyOF;

        public SumOfPairsWithAffineGapPenaltiesObjectiveFunction(IScoringMatrix matrix, double openingCost = 4, double nullCost = 1)
        {
            SumOfPairsOF = new SumOfPairsObjectiveFunction(matrix);
            AffineGapPenaltyOF = new AffineGapPenaltyObjectiveFunction(openingCost, nullCost);
        }

        public double ScoreAlignment(Alignment alignment)
        {
            double sumOfPairsScore = SumOfPairsOF.ScoreAlignment(alignment);
            double penalty = AffineGapPenaltyOF.ScoreAlignment(alignment);

            return sumOfPairsScore - penalty;
        }
    }
}
