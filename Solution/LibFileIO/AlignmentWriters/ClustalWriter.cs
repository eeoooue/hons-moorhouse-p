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
            string destination = $"{filename}.{FileExtension}";
            List<string> lines = CreateAlignmentLines(alignment);
            File.WriteAllLines(destination, lines);
            Console.WriteLine($"Alignment written to destination: '{destination}'");
        }

        public List<string> CreateAlignmentLines(Alignment alignment)
        {
            List<string> result = new List<string>();
            List<string> header = GetHeader();
            foreach(string s in header)
            {
                result.Add(s);
            }
            List<string> body = GetBody(alignment);
            foreach (string s in body)
            {
                result.Add(s);
            }

            return result;
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
                result.Add("");
            }

            return result;
        }

        public List<string> CollectBlock(Alignment alignment, int startPoint)
        {
            char[,] blockMatrix = ExtractBlockMatrix(alignment, startPoint);
            char[] conservation = ComputeConservation(blockMatrix);

            List<string> result = new List<string>();
            for(int i=0; i<alignment.Height; i++)
            {
                string payload = ExtractRowOfMatrix(blockMatrix, i);
                result.Add(payload);
            }

            string markers = ConvertArrToString(conservation);
            result.Add(markers);

            return result;
        }


        public string ExtractRowOfMatrix(char[,] matrix, int i)
        {
            int n = matrix.GetLength(1);

            StringBuilder sb = new StringBuilder();
            for(int j=0; j<n; j++)
            {
                sb.Append(matrix[i, j]);
            }

            return sb.ToString();
        }

        public string ConvertArrToString(char[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char x in arr)
            {
                sb.Append(x);
            }

            return sb.ToString();
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
