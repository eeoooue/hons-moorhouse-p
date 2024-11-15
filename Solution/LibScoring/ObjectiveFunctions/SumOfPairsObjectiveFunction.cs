using LibBioInfo;
using LibScoring.ScoringMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ObjectiveFunctions
{
    public class SumOfPairsObjectiveFunction : IObjectiveFunction
    {
        IScoringMatrix Matrix;

        public SumOfPairsObjectiveFunction(IScoringMatrix matrix)
        {
            Matrix = matrix;
        }

        public double ScoreAlignment(Alignment alignment)
        {
            double result = 0;
            for(int j=0; j<alignment.Width; j++)
            {
                result += ScoreColumn(alignment, j);
            }

            return 0;
        }

        public double ScoreColumn(Alignment alignment, int j)
        {
            for(int i1 = 0; i1 < alignment.Height; i1++)
            {
                char a = alignment.GetCharacterAt(i1, j);

                for (int i2 = 0; i2 < alignment.Height; i2++)
                {
                    char b = alignment.GetCharacterAt(i2, j);
                }
            }





            return 0;
        }
    }
}
