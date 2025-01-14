using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ObjectiveFunctions
{
    public class TotallyConservedColumnsObjectiveFunction : IObjectiveFunction
    {
        private Bioinformatics Bioinformatics = new Bioinformatics();

        public string GetName()
        {
            return $"Totally Conserved Columns";
        }

        public double ScoreAlignment(Alignment alignment)
        {
            int total = 0;

            for(int j=0; j<alignment.Width; j++)
            {
                bool verdict = IsTotallyConservedColumn(alignment, j);
                if (verdict)
                {
                    total++;
                }
            }

            double value = (double)total / (double)alignment.Width;

            return value;
        }

        public bool IsTotallyConservedColumn(Alignment alignment, int j)
        {
            char target = alignment.GetCharacterAt(0, j);

            if (Bioinformatics.IsGapChar(target)){
                return false;
            }

            for(int i=1; i<alignment.Height; i++)
            {
                char residue = alignment.GetCharacterAt(i, j);
                if (residue != target)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
