using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{
    internal class NonGapsFitnessFunction : NormalisedFitnessFunction
    {
        private Bioinformatics Bioinformatics = new Bioinformatics();

        public override string GetName()
        {
            return "Percentage Non-Gaps";
        }

        public override double ScoreAlignment(char[,] alignment)
        {
            double result = 0.0;
            int m = alignment.GetLength(0);
            int n = alignment.GetLength(1);
            int totalPositions = m * n;

            for (int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    char x = alignment[i, j];
                    if (!Bioinformatics.IsGapChar(x))
                    {
                        result += 1.0;
                    }
                }
            }

            return result / totalPositions;
        }

        public override double GetBestPossibleScore(char[,] alignment)
        {
            return 1.0;
        }

        public override double GetWorstPossibleScore(char[,] alignment)
        {
            return 0.0;
        }
    }
}
