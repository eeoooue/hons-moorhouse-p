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
        public double OpeningCost { get; private set; } 
        public double NullCost { get; private set; }

        private static Bioinformatics Bioinformatics = new Bioinformatics();

        public AffineGapPenaltyObjectiveFunction(double openingCost=4, double nullCost=1)
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
            string trimmed = TrimPayload(payload);
            List<int> gapSizes = CollectGapSizes(trimmed);



            throw new NotImplementedException();
        }

        public List<int> CollectGapSizes(string payload)
        {
            List<int> result = new List<int>();

            return result;
        }

        public string TrimPayload(string payload)
        {
            int i = GetIndexOfFirstResidue(payload);
            int j = GetIndexOfLastResidue(payload);
            int length = 1 + j - i;
            string trimmed = payload.Substring(i, length);

            return trimmed;
        }

        public int GetIndexOfFirstResidue(string payload)
        {
            for(int i=0; i<payload.Length; i++)
            {
                if (!Bioinformatics.IsGapChar(payload[i]))
                {
                    return i;
                }
            }

            return payload.Length;
        }

        public int GetIndexOfLastResidue(string payload)
        {
            for (int i = payload.Length-1; i >= 0; i--)
            {
                if (!Bioinformatics.IsGapChar(payload[i]))
                {
                    return i;
                }
            }

            return payload.Length;
        }
    }
}
