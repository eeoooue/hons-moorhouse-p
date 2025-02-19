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
                "CLUSTAL W (MAli v1.3) multiple sequence alignment",
                "",
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

            List<string> margin = CollectPaddedSequenceIdentifiers(alignment);
            List<string> result = new List<string>();
            for(int i=0; i<alignment.Height; i++)
            {
                string payload = ExtractRowOfMatrix(blockMatrix, i);
                result.Add(margin[i] + payload);
            }

            string whitespace = GetWhitespace(margin[0].Length);
            string markers = ConvertArrToString(conservation);
            result.Add(whitespace + markers);

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
            int n = Math.Min(BlockWidth, alignment.Width - startPoint); // TODO: check this

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

        #region Column Conservation

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

        #endregion


        #region Identifier Margin

        public List<string> CollectPaddedSequenceIdentifiers(Alignment alignment)
        {
            int maxLength = GetMaxSequenceIdentifierLength(alignment);
            int padding = 3;
            int goal = maxLength + padding;

            List<string> result = new List<string>();
            foreach (BioSequence sequence in alignment.Sequences)
            {
                string identifier = GetPaddedIdentifier(sequence.Identifier, goal);
                result.Add(identifier);
            }

            return result;
        }

        public int GetMaxSequenceIdentifierLength(Alignment alignment)
        {
            int maxLength = 0;
            foreach (BioSequence sequence in alignment.Sequences)
            {
                string identifier = sequence.Identifier;
                maxLength = Math.Max(maxLength, identifier.Length);
            }

            return maxLength;
        }

        public string GetPaddedIdentifier(string identifier, int lengthGoal)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(identifier);
            while (sb.Length < lengthGoal)
            {
                sb.Append(' ');
            }

            return sb.ToString();
        }

        public string GetWhitespace(int n)
        {
            StringBuilder sb = new StringBuilder();
            while (sb.Length < n)
            {
                sb.Append(' ');
            }
            return sb.ToString();
        }

        #endregion
    }
}
