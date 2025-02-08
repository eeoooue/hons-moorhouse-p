using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{
    public class TotallyConservedColumnsFitnessFunction : NormalisedFitnessFunction
    {
        private Bioinformatics Bioinformatics = new Bioinformatics();

        public override string GetName()
        {
            return "Percentage of Totally Conserved Columns";
        }

        public override double ScoreAlignment(in char[,] alignment)
        {
            double total = 0;
            int n = alignment.GetLength(1);

            for (int j = 0; j < n; j++)
            {
                bool verdict = IsTotallyConservedColumn(alignment, j);
                if (verdict)
                {
                    total += 1.0;
                }
            }

            double value = total / n;

            return value;
        }

        public bool IsTotallyConservedColumn(in char[,] alignment, int j)
        {
            int m = alignment.GetLength(0);
            char target = alignment[0, j];

            if (Bioinformatics.IsGapChar(target))
            {
                return false;
            }

            for (int i=1; i<m; i++)
            {
                if (alignment[i, j] != target)
                {
                    return false;
                }
            }

            return true;
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
            return "%TCCs";
        }
    }
}
