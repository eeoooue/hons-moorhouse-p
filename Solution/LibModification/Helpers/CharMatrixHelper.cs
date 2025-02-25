using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Helpers
{
    public class CharMatrixHelper
    {
        public void ClearAlignmentRow(ref char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);
            for(int j=0; j<n; j++)
            {
                matrix[i, j] = '-';
            }
        }

        public void RemoveEmptyColumns(Alignment alignment)
        {
            alignment.CharacterMatrix = RemoveEmptyColumns(in alignment.CharacterMatrix);
        }

        public void SprinkleEmptyColumnsIntoAlignment(Alignment alignment, int n)
        {
            if (n == 0)
            {
                return;
            }

            List<int> insertions = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int position = Randomizer.Random.Next(alignment.Width);
                insertions.Add(position);
            }
            insertions.Sort();

            List<int> recipe = CreateColumnInsertionRecipe(alignment, insertions);

            alignment.CharacterMatrix = InsertEmptyColumns(alignment, recipe);
        }


        public char[,] InsertEmptyColumns(Alignment alignment, List<int> recipe)
        {
            return InsertEmptyColumns(alignment.CharacterMatrix, recipe);
        }

        public char[,] InsertEmptyColumns(char[,] alignment, List<int> recipe)
        {
            int m = alignment.GetLength(0);
            int n = recipe.Count;
            char[,] result = new char[m, n];

            for(int j=0; j<n; j++)
            {
                if (recipe[j] == -1)
                {
                    FillColumnWithGaps(result, j);
                }
                else
                {
                    CopyColumnFromSourceToDestination(alignment, recipe[j], result, j);
                }
            }

            return result;
        }

        public void CopyColumnFromSourceToDestination(char[,] source, int srcIndex, char[,] destination, int destIndex)
        {
            int m = source.GetLength(0);

            for(int i=0; i<m; i++)
            {
                destination[i, destIndex] = source[i, srcIndex];
            }
        }

        public void FillColumnWithGaps(char[,] matrix, int j)
        {
            int m = matrix.GetLength(0);
            for (int i = 0; i < m; i++)
            {
                matrix[i, j] = '-';
            }
        }

        public List<int> CreateColumnInsertionRecipe(Alignment alignment, List<int> insertions)
        {
            int n = alignment.Width;
            int expected = alignment.Width + insertions.Count;

            List<int> result = new List<int>();

            int currentPosition = 0;
            foreach (int insertion in insertions)
            {
                while (currentPosition < insertion)
                {
                    result.Add(currentPosition++);
                }
                result.Add(-1);
            }

            while (currentPosition < alignment.Width)
            {
                result.Add(currentPosition++);
            }

            return result;
        }

        public string GetCharRowAsString(in char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);

            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < n; j++)
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

            for (int i = 0; i < m; i++)
            {
                for (int j1 = 0; j1 < n; j1++)
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

            for (int i = 0; i < m; i++)
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

            for (int j = 0; j < n; j++)
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

            for (int j = 0; j < n; j++)
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
