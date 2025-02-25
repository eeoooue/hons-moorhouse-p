using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class SmartBlockScrollingOperator : IAlignmentModifier
    {
        private IScoringMatrix Matrix;
        private CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public int MinBlockLength = 4;
        public int MaxBlockLength = 32;
        public int Range = 64;

        public SmartBlockScrollingOperator(IScoringMatrix matrix)
        {
            Matrix = matrix;
        }

        public void ModifyAlignment(Alignment alignment)
        {
            int n;
            int i;
            int j;
            SelectBlockPosition(alignment, out n, out i, out j);
            ScrollBlockAt(alignment, n, i, j);
            CharMatrixHelper.RemoveEmptyColumns(alignment);
        }

        public void SelectBlockPosition(Alignment alignment, out int n, out int i, out int j)
        {
            n = 0;
            i = 0;
            j = 0;
            n = PickBlockLength(alignment);
            i = Randomizer.Random.Next(alignment.Height);
            j = PickBlockStart(alignment, n);
        }


        public void ScrollBlockAt(Alignment alignment, int n, int i, int j)
        {

            if (Randomizer.CoinFlip())
            {
                ScrollBlockRight(alignment, n, i, j);
            }
            else
            {
                ScrollBlockLeft(alignment, n, i, j);
            }

            // TODO: scroll left on coinflip
        }


        public void ScrollBlockLeft(Alignment alignment, int n, int i, int j)
        {
            // TODO: fix terrible efficiency

            alignment.CharacterMatrix = MirrorMatrix(alignment);
            ScrollBlockRight(alignment, n, i, j);
            alignment.CharacterMatrix = MirrorMatrix(alignment);
        }

        public char[,] MirrorMatrix(Alignment alignment)
        {
            char[,] result = new char[alignment.Height, alignment.Width];
            for(int i=0; i<alignment.Height; i++)
            {
                for(int j=0; j<alignment.Width; j++)
                {
                    int jMirrored = alignment.Width - (1 + j);
                    result[i, jMirrored] = alignment.CharacterMatrix[i, j];
                }
            }

            return result;
        }

        public void ScrollBlockRight(Alignment alignment, int n, int i, int j)
        {
            int bestOffset = GetBestRightOffset(alignment, n, i, j);
            string x = CollectPayloadToPasteRightwise(alignment, n, i, j, bestOffset);
            PasteStringRightwiseFromPosition(alignment, i, j, x);
        }

        public void PasteStringRightwiseFromPosition(Alignment alignment, int i, int j, string s)
        {
            //TODO: check if there's space remaining

            // int spaceRemaining = alignment.Width - j;
            // int columnsNeeded = Math.Max(0, s.Length - spaceRemaining);
            PadRightWithEmptyColumns(alignment, s.Length);

            foreach(char x in s)
            {
                alignment.CharacterMatrix[i, j++] = x;
            }
        }

        public void PadRightWithEmptyColumns(Alignment alignment, int count)
        {
            if (count <= 0)
            {
                return;
            }

            if (alignment.Height <= 0)
            {
                return;
            }

            int m = alignment.Height;
            int n = alignment.Width + count;

            char[,] canvas = new char[m, n];
            for(int j=0; j<n; j++)
            {
                FillColumnWithGaps(canvas, m, j);
            }

            PasteAlignmentStateOntoCanvas(alignment, canvas);
            alignment.CharacterMatrix = canvas;
        }

        public void FillColumnWithGaps(char[,] matrix, int m, int j)
        {
            for (int i = 0; i < m; i++)
            {
                matrix[i, j] = '-';
            }
        }


        public void PasteAlignmentStateOntoCanvas(Alignment alignment, char[,] canvas)
        {
            for(int i=0; i<alignment.Height; i++)
            {
                for(int j=0; j<alignment.Width; j++)
                {
                    canvas[i, j] = alignment.CharacterMatrix[i, j];
                }
            }
        }

        public string CollectPayloadToPasteRightwise(Alignment alignment, int n, int i, int j, int offset)
        {
            // TODO: reduce how much this impacts the further rightwise sections

            StringBuilder sb = new StringBuilder();
            for(int r=0; r<offset; r++)
            {
                sb.Append('-');
            }

            for(int p=j; p<alignment.Width; p++)
            {
                char x = alignment.CharacterMatrix[i, p];
                sb.Append(x);
            }

            return sb.ToString();
        }


        public int GetBestRightOffset(Alignment alignment, int n, int i, int j)
        {
            char[] section = ExtractBlock(alignment, n, i, j);

            Dictionary<char, int>[] tables = GetComplementTables(alignment, n, i, j);

            int bestOffset = 0;
            double bestScore = 0; // always pick a new offset

            for (int offset = 1; offset < Range; offset++)
            {
                tables = GetComplementTables(alignment, n, i, j); // TODO: improve efficiency
                double score = ScoreSection(section, tables);
                if (score > bestScore)
                {
                    bestOffset = offset;
                    bestScore = score;
                }
            }

            return bestOffset;
        }

        public double ScoreSection(char[] section, Dictionary<char, int>[] tables)
        {
            double total = 0;
            for (int i = 0; i < section.Length; i++)
            {
                double score = GetIncreaseInScore(section[i], tables[i]);
                total += score;
            }

            return total;
        }

        public double GetIncreaseInScore(char x, Dictionary<char, int> table)
        {
            double total = 0;
            foreach (char y in table.Keys)
            {
                int count = table.Count;
                int pairScore = Matrix.ScorePair(x, y);
                total += pairScore * count;
            }

            return total;
        }


        public char[] ExtractBlock(Alignment alignment, int n, int i, int j)
        {
            char[] result = new char[n];
            for (int offset = 0; offset < n; offset++)
            {
                char x = alignment.CharacterMatrix[i, j + offset];
                result[offset] = x;
            }

            return result;
        }


        public Dictionary<char, int>[] GetComplementTables(Alignment alignment, int n, int i, int j)
        {
            Dictionary<char, int>[] tables = new Dictionary<char, int>[n];
            for (int offset = 0; offset < n; offset++)
            {
                tables[offset] = GetComplementTable(alignment, i, j + offset);
            }

            return tables;
        }

        public Dictionary<char, int> GetComplementTable(Alignment alignment, int i, int j)
        {
            Dictionary<char, int> table = new Dictionary<char, int>();

            if (j < 0 || j >= alignment.Width)
            {
                return table;
            }

            table = PopulateTable(alignment, j, table);
            char x = alignment.CharacterMatrix[i, j];

            if (!Bioinformatics.IsGapChar(x))
            {
                table[x]--;
            }

            return table;
        }

        public Dictionary<char, int> PopulateTable(Alignment alignment, int j, Dictionary<char, int> table)
        {
            table.Clear();

            char[,] matrix = alignment.CharacterMatrix;

            for (int i = 0; i < alignment.Height; i++)
            {
                char x = alignment.CharacterMatrix[i, j];
                if (Bioinformatics.IsGapChar(x))
                {
                    continue;
                }
                if (!table.ContainsKey(x))
                {
                    table[x] = 0;
                }
                table[x]++;
            }

            return table;
        }


        public int PickBlockLength(Alignment alignment)
        {
            int limit = Math.Min(alignment.Width + 1, MaxBlockLength + 1);
            int length = Randomizer.Random.Next(MinBlockLength, limit);
            return length;
        }

        public int PickBlockStart(Alignment alignment, int length)
        {
            while (true)
            {
                int j = Randomizer.Random.Next(alignment.Width + 1 - length);

                if (j < 0)
                {
                    continue;
                }

                if (j + length <= alignment.Width)
                {
                    return j;
                }
            }
        }
    }
}
