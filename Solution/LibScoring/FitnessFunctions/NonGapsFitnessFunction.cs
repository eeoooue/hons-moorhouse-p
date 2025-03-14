using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{
    public class NonGapsFitnessFunction : NormalisedFitnessFunction
    {

        public override string GetName()
        {
            return "Percentage Non-Gaps";
        }

        public override double ScoreAlignment(in char[,] alignment)
        {
            int m = alignment.GetLength(0);
            int n = alignment.GetLength(1);
            int totalPositions = m * n;

            int totalResidues = 0;
            for (int i=0; i<m; i++)
            {
                totalResidues += CountResiduesInRow(alignment, i);
            }

            return totalResidues / totalPositions;
        }

        private int CountResiduesInRow(in char[,] alignment, int i)
        {
            int n = alignment.GetLength(1);
            int result = 0;
            for(int j=0; j<n; j++)
            {
                if (!Bioinformatics.IsGapChar(alignment[i, j]))
                {
                    result += 1;
                }
            }

            return result;
        }

        public override double GetBestPossibleScore(in char[,] alignment)
        {
            return 1.0;
        }

        public override double GetWorstPossibleScore(in char[,] alignment)
        {
            return 0.0;
        }

        public override string GetAbbreviation()
        {
            return "%NonGaps";
        }
    }
}
