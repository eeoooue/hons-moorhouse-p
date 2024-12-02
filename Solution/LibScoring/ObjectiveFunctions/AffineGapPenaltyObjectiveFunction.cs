using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ObjectiveFunctions
{
    public class AffineGapPenaltyObjectiveFunction : IObjectiveFunction
    {
        public double OpeningCost { get; private set; } = 4;
        public double NullCost { get; private set; } = 1;

        public AffineGapPenaltyObjectiveFunction(double openingCost, double nullCost)
        {
            OpeningCost = openingCost;
            NullCost = nullCost;
        }

        public double ScoreAlignment(Alignment alignment)
        {
            throw new NotImplementedException();
        }

        public double ScorePayload(string payload)
        {
            throw new NotImplementedException();
        }
    }
}
