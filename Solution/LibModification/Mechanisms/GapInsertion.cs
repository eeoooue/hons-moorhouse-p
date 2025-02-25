using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public class GapInsertion
    {
        public void InsertGaps(Alignment alignment, bool[] mask, int width, int j1, int j2)
        {
            // j denotes where gaps start. so j can be 0 to n (alignment width)

            char[,] matrix = InsertGaps(alignment.CharacterMatrix, mask, width, j1, j2);
            alignment.CharacterMatrix = matrix;
        }

        public char[,] InsertGaps(char[,] original, bool[] mask, int width, int j1, int j2)
        {
            int m = original.GetLength(0);
            int n = original.GetLength(1) + width;

            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                int gapPosition = mask[i] ? j1 : j2;
                // Console.WriteLine($"Inserting gap of width = {width} @ {gapPosition}");
                PasteSequenceWithGapAt(original, result, i, gapPosition, width);
            }

            return result;
        }

        public void PasteSequenceWithGapAt(in char[,] source, char[,] destination, int i, int gapPosition, int width)
        {
            string original = ReadMatrixRow(source, i);
            string payload = InsertGapInPayload(original, width, gapPosition);

            for(int j=0; j<payload.Length; j++)
            {
                destination[i, j] = payload[j];
            }
        }

        public string ReadMatrixRow(in char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);
            StringBuilder sb = new StringBuilder();
            for(int j=0; j<n; j++)
            {
                sb.Append(matrix[i, j]);
            }

            return sb.ToString();
        }

        public string InsertGapInPayload(string payload, int gapWidth, int position)
        {
            string before = payload.Substring(0, position);
            string after = payload.Substring(position);

            StringBuilder sb = new StringBuilder();
            sb.Append(before);
            for (int i = 0; i < gapWidth; i++)
            {
                sb.Append('-');
            }
            sb.Append(after);

            return sb.ToString();
        }
    }
}
