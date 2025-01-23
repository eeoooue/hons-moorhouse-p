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

        public List<string> CollectResidueChains(in char[,] matrix)
        {
            int m = matrix.GetLength(0);

            List<string> result = new List<string>();
            for(int i=0; i<m; i++)
            {
                string chain = CollectResidueChain(in matrix, i);
            }

            return result;
        }

        public string CollectResidueChain(in char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);

            StringBuilder sb = new StringBuilder();

            for(int j=0; j<n; j++)
            {
                char x = matrix[i, j];
                if (Bioinformatics.IsGapChar(x) == false)
                {
                    sb.Append(x);
                }
            }

            return sb.ToString();
        }

        public string GetCharRowAsString(in char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);

            StringBuilder sb = new StringBuilder();
            for(int j=0; j<n; j++)
            {
                sb.Append(matrix[i, j]);
            }

            return sb.ToString();
        }

        public char[,] RemoveEmptyColumns(in char[,] matrix)
        {
            int n = matrix.GetLength(1);
            List<int> whitelist = new List<int>();

            for (int j = 0; j < n; j++)
            {
                if (!ColumnIsEmpty(matrix, j))
                {
                    whitelist.Add(j);
                }
            }

            return GetMatrixOfOnlyWhitelistedColumns(in matrix, whitelist);
        }

        private char[,] GetMatrixOfOnlyWhitelistedColumns(in char[,] matrix, List<int> whitelist)
        {
            int m = matrix.GetLength(0);
            int n = whitelist.Count;

            if (n == matrix.GetLength(1))
            {
                return matrix;
            }

            char[,] result = new char[m, n];

            for(int i=0; i<m; i++)
            {
                for(int j1=0; j1<n; j1++)
                {
                    int j2 = whitelist[j1];
                    result[i, j1] = matrix[i, j2];
                }
            }

            return result;
        }

        public bool ColumnIsEmpty(in char[,] matrix, int j)
        {
            int m = matrix.GetLength(0);

            for(int i=0; i<m; i++)
            {
                char x = matrix[i, j];
                if (!Bioinformatics.IsGapChar(x))
                {
                    return false;
                }
            }
            return true;
        }

        public void WriteStringOverMatrixRow(ref char[,] matrix, int i, string s)
        {
            for (int j = 0; j < s.Length; j++)
            {
                matrix[i, j] = s[j];
            }
        }

        public bool RowContainsGap(in char[,] matrix, int i)
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

        public List<int> GetGapPositionsInRow(in char[,] matrix, int i)
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

        public List<int> GetResiduePositionsInRow(in char[,] matrix, int i)
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
