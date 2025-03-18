using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.Metrics
{
    public class AffineGapPenalties
    {
        public double OpeningCost { get; private set; }
        public double NullCost { get; private set; }

        public AffineGapPenalties(double openingCost = 4, double nullCost = 1)
        {
            OpeningCost = openingCost;
            NullCost = nullCost;
        }

        public double GetMaximumPossiblePenalty(in char[,] alignment)
        {
            int m = alignment.GetLength(0);
            int residueCount = GetNumberOfResiduesInFirstRow(alignment);
            int pseudoCount = m * residueCount;
            double result = pseudoCount * (OpeningCost + NullCost);

            return result;
        }

        public double GetMinimumPossiblePenalty()
        {
            return 0.0;
        }

        public double GetPenaltyForAlignmentMatrix(in char[,] alignment)
        {
            double result = 0;
            foreach (string payload in ExtractPayloads(alignment))
            {
                result += ScorePayload(payload);
            }

            return result;
        }

        public double ScorePayload(string payload)
        {
            List<int> sizes = CollectGapSizes(payload);

            double result = 0;
            foreach (int size in sizes)
            {
                result += OpeningCost;
                result += (size - 1) * NullCost;
            }

            return result;
        }

        private List<int> CollectGapSizes(string payload)
        {
            payload = TrimPayload(payload);

            List<int> result = new List<int>();

            int gaplength = 0;
            for (int i = 0; i < payload.Length; i++)
            {
                char x = payload[i];
                if (x == Bioinformatics.GapCharacter)
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

        

        private int GetNumberOfResiduesInFirstRow(in char[,] alignment)
        {
            int n = alignment.GetLength(1);
            int total = 0;

            for (int j = 0; j < n; j++)
            {
                if (alignment[0, j] != Bioinformatics.GapCharacter)
                {
                    total++;
                }
            }

            return total;
        }


        private List<string> ExtractPayloads(in char[,] alignment)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < alignment.GetLength(0); i++)
            {
                string payload = ExtractPayload(alignment, i);
                result.Add(payload);
            }

            return result;
        }

        private string ExtractPayload(in char[,] alignment, int i)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < alignment.GetLength(1); j++)
            {
                char x = alignment[i, j];
                sb.Append(x);
            }

            return sb.ToString();
        }



        private string TrimPayload(string payload)
        {
            int i = GetIndexOfFirstResidue(payload);
            int j = GetIndexOfLastResidue(payload);
            int length = 1 + j - i;
            string trimmed = payload.Substring(i, length);

            return trimmed;
        }

        private int GetIndexOfFirstResidue(string payload)
        {
            for (int i = 0; i < payload.Length; i++)
            {
                if (payload[i] != Bioinformatics.GapCharacter)
                {
                    return i;
                }
            }

            return payload.Length;
        }

        private int GetIndexOfLastResidue(string payload)
        {
            for (int i = payload.Length - 1; i >= 0; i--)
            {
                if (payload[i] != Bioinformatics.GapCharacter)
                {
                    return i;
                }
            }

            return payload.Length;
        }
    }
}
