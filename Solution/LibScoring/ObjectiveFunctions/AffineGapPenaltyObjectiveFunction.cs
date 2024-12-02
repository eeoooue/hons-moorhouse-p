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
            double result = 0;
            foreach(BioSequence sequence in alignment.GetAlignedSequences())
            {
                result += ScorePayload(sequence.Payload);
            }

            return result;
        }

        public double ScorePayload(string payload)
        {
            string trimmed = TrimPayload(payload);
            List<int> sizes = CollectGapSizes(trimmed);

            double result = 0;
            foreach(int size in sizes)
            {
                result += OpeningCost;
                result += size * NullCost;
            }

            return result;
        }

        public List<int> CollectGapSizes(string payload)
        {
            List<int> result = new List<int>();

            int gaplength = 0;
            for(int i=0; i<payload.Length; i++)
            {
                char x = payload[i];
                if (Bioinformatics.IsGapChar(x))
                {
                    gaplength++;
                }
                else
                {
                    if (gaplength > 0)
                    {
                        result.Add(gaplength);
                    }
                    gaplength = 0;
                }
            }

            if (gaplength > 0)
            {
                result.Add(gaplength);
            }

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
