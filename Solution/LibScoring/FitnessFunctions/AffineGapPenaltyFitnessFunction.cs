using LibScoring.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{

    public class AffineGapPenaltyFitnessFunction : NormalisedFitnessFunction
    {
        private AffineGapPenalties Penalties;

        public AffineGapPenaltyFitnessFunction(double openingCost = 4, double nullCost = 1)
        {
            Penalties = new AffineGapPenalties(openingCost, nullCost);
        }

        public override string GetName()
        {
            return $"Affine Gap Penalties (open={Penalties.OpeningCost}, null={Penalties.NullCost})";
        }

        public override double ScoreAlignment(char[,] alignment)
        {
            return -1 * Penalties.GetPenaltyForAlignmentMatrix(alignment);
        }

        public override double GetBestPossibleScore(char[,] alignment)
        {
            return -1 * Penalties.GetMinimumPossiblePenalty();
        }

        public override double GetWorstPossibleScore(char[,] alignment)
        {
            return -1 * Penalties.GetMaximumPossiblePenalty(alignment);
        }
    }
}
