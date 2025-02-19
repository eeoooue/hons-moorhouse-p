using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentWriters
{
    public class ClustalWriter : IAlignmentWriter
    {
        public string FileExtension = "aln";
        public int BlockWidth = 60;

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            List<string> header = GetHeader();
            List<string> body = GetBody(alignment);


            throw new NotImplementedException();
        }

        public List<string> GetHeader()
        {
            List<string> result = new List<string>()
            {
                "CLUSTAL W (1.82) multiple sequence alignment",
                "",
            };

            return result;
        }

        public List<string> GetBody(Alignment alignment)
        {
            List<string> result = new List<string>();

            List<BioSequence> sequences = alignment.GetAlignedSequences();
            for(int b = 0; b < alignment.Width; b += BlockWidth)
            {
                List<string> block = CollectBlock(alignment, b);
                foreach(string line in block)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public List<string> CollectBlock(Alignment alignment, int startPoint)
        {
            char[,] blockMatrix = ExtractBlockMatrix(alignment, startPoint);
            char[] conservation = ComputeConservation(blockMatrix);



            throw new NotImplementedException();
        }

        public char[,] ExtractBlockMatrix(Alignment alignment, int startPoint)
        {
            int m = alignment.Height;
            int n = Math.Min(BlockWidth, 1 + alignment.Width - startPoint); // TODO: check this

            char[,] result = new char[m, n];

            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    result[i, j] = alignment.CharacterMatrix[i, startPoint + j];
                }
            }

            return result;
        }

        public char[] ComputeConservation(char[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            char[] result = new char[n];
            for(int j=0; j<n; j++)
            {
                result[j] = GetConservationOfColumn(matrix, m, j);
            }

            return result;
        }

        public char GetConservationOfColumn(char[,] matrix, int m, int j)
        {
            for(int i=0; i<m; i++)
            {
                if (matrix[i, j] != matrix[0, j])
                {
                    return ' ';
                }
            }

            return '*';
        }
    }
}
