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

        public override double GetBestPossibleScore(char[,] alignment)
        {
            return 1.0 - Penalties.GetMinimumPossiblePenalty();
        }

        public override string GetName()
        {
            return $"Affine Gap Penalties (open={Penalties.OpeningCost}, null={Penalties.NullCost})";
        }

        public override double GetWorstPossibleScore(char[,] alignment)
        {
            return 1.0 - Penalties.GetMaximumPossiblePenalty(alignment);
        }

        public override double ScoreAlignment(char[,] alignment)
        {
            return 1.0 - Penalties.GetPenaltyForAlignmentMatrix(alignment);
        }
    }
}
