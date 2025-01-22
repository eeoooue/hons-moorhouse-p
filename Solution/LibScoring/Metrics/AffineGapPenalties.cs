using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.Metrics
{
    public class AffineGapPenalties
    {
        public double OpeningCost { get; private set; }
        public double NullCost { get; private set; }

        private Bioinformatics Bioinformatics = new Bioinformatics();

        public AffineGapPenalties(double openingCost = 4, double nullCost = 1)
        {
            OpeningCost = openingCost;
            NullCost = nullCost;
        }

        public double GetMaximumPossiblePenalty(char[,] alignment)
        {
            int m = alignment.GetLength(0);
            int n = alignment.GetLength(1);
            int maximumNumberOfOpeningsPerRow = n - (n / 2);
            int maximumNumberOfOpenings = m * maximumNumberOfOpeningsPerRow;
            int maximumNumberOfGapsPerRow = n - 1;
            int maximumNumberOfGaps = m * maximumNumberOfGapsPerRow;

            double result = 0.0;
            result += maximumNumberOfOpenings * OpeningCost;
            result += maximumNumberOfGaps * NullCost;

            return result;
        }

        public double GetMinimumPossiblePenalty()
        {
            return 0.0;
        }


        public double GetPenaltyForAlignmentMatrix(char[,] alignment)
        {
            double result = 0;
            foreach(string payload in ExtractPayloads(alignment))
            {
                result += ScorePayload(payload);
            }

            return result;
        }
        
        public List<string> ExtractPayloads(char[,] alignment)
        {
            List<string> result = new List<string>();
            for(int i=0; i<alignment.GetLength(0); i++)
            {
                string payload = ExtractPayload(alignment, i);
                result.Add(payload);
            }

            return result;
        }

        public string ExtractPayload(char[,] alignment, int i)
        {
            StringBuilder sb = new StringBuilder();
            for(int j=0; j < alignment.GetLength(1); j++)
            {
                char x = alignment[i, j];
                sb.Append(x);
            }

            return sb.ToString();
        }

        public double ScorePayload(string payload)
        {
            string trimmed = TrimPayload(payload);
            List<int> sizes = CollectGapSizes(trimmed);

            double result = 0;
            foreach (int size in sizes)
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
            for (int i = 0; i < payload.Length; i++)
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
            for (int i = 0; i < payload.Length; i++)
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
            for (int i = payload.Length - 1; i >= 0; i--)
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
