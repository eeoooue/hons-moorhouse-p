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

        public string GetName()
        {
            return $"Sum of Pairs ({Matrix.GetName()})";
        }

        public double ScoreAlignment(Alignment alignment)
        {
            double result = 0;
            for(int j=0; j<alignment.Width; j++)
            {
                result += ScoreColumn(alignment, j);
            }

            return result;
        }

        public double ScoreColumn(Alignment alignment, int j)
        {
            string column = alignment.GetColumn(j);

            Dictionary<char, int> table = ConstructCounterHashTable(column);

            List<char> residues = Matrix.GetResidues();
            double result = 0;

            for (int i1=0; i1 < residues.Count; i1++)
            {
                char a = residues[i1];
                int a_count = table[a];

                for(int i2=i1; i2 < residues.Count; i2++)
                {
                    char b = residues[i2];
                    int b_count = table[b];

                    int combinations = 0;

                    if (a != b)
                    {
                        combinations = (a_count * b_count)/2;
                    }
                    else
                    {
                        combinations = (a_count * (a_count - 1)) / 2;
                    }

                    int score = Matrix.ScorePair(a, b);
                    int contribution = score * combinations;
                    result += contribution;
                }
            }

            return result;
        }

        public Dictionary<char, int> ConstructCounterHashTable(string column)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();
            foreach (char residue in Matrix.GetResidues())
            {
                result[residue] = 0;
            }

            foreach (char x in column)
            {
                if (result.ContainsKey(x))
                {
                    result[x] += 1;
                }
            }

            return result;
        }
    }
}
