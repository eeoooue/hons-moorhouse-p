using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.Helpers
{
    public class CharMatrixHelper
    {
        private Bioinformatics Bioinformatics = new Bioinformatics();

        public string GetCharRowAsString(char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);

            StringBuilder sb = new StringBuilder();
            for(int j=0; j<n; j++)
            {
                sb.Append(matrix[i, j]);
            }

            return sb.ToString();
        }

        public char[,] WriteStringOverMatrixRow(char[,] matrix, int i, string s)
        {
            for (int j = 0; j < s.Length; j++)
            {
                matrix[i, j] = s[j];
            }

            return matrix;
        }


        public bool RowContainsGap(char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);

            for(int j=0; j<n; j++)
            {
                char x = matrix[i, j];
                if (Bioinformatics.IsGapChar(x))
                {
                    return true;
                }
            }

            return false;
        }


        public List<int> GetGapPositionsInRow(char[,] matrix, int i)
        {
            List<int> result = new List<int>();

            int n = matrix.GetLength(1);

            for(int j=0; j<n; j++)
            {
                char x = matrix[i, j];
                if (Bioinformatics.IsGapChar(x))
                {
                    result.Add(j);
                }
            }

            return result;
        }

        public List<int> GetResiduePositionsInRow(char[,] matrix, int i)
        {
            List<int> result = new List<int>();

            int n = matrix.GetLength(1);

            for (int j = 0; j < n; j++)
            {
                char x = matrix[i, j];
                if (!Bioinformatics.IsGapChar(x))
                {
                    result.Add(j);
                }
            }

            return result;
        }
    }
}
