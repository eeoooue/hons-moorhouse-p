using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibModification.AlignmentModifiers
{
    /// <summary>
    /// Permutes the position of gaps within an n-length block within a single row.
    /// Looks to maximise the pairwise score of this block relative to the current alignment state.
    /// </summary>
    public class SmartBlockPermutationOperator : IAlignmentModifier
    {
        private IScoringMatrix Matrix;
        private CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public int MinBlockLength = 4;
        public int MaxBlockLength = 8;

        public SmartBlockPermutationOperator(IScoringMatrix matrix)
        {
            Matrix = matrix;
        }

        public void ModifyAlignment(Alignment alignment)
        {
            int n;
            int i;
            int j;
            bool validBlockFound = SelectBlockPosition(alignment, out n, out i, out j);
            if (validBlockFound)
            {
                PermuteBlockAt(alignment, n, i, j);
            }

            CharMatrixHelper.RemoveEmptyColumns(alignment);
        }

        public bool SelectBlockPosition(Alignment alignment, out int n, out int i, out int j)
        {
            n = 0;
            i = 0;
            j = 0;
            
            for(int r=0; r<10; r++)
            {
                n = PickBlockLength(alignment);
                i = Randomizer.Random.Next(alignment.Height);
                j = PickBlockStart(alignment, n);
                bool valid = ContainsResiduesAndGaps(alignment, n, i, j);
                if (valid)
                {
                    return true;
                }
            }

            return false;
        }


        public bool ContainsResiduesAndGaps(Alignment alignment, int n, int i, int j)
        {
            bool residuesFound = false;
            bool gapsFound = false;

            for (int offset = 0; offset < n; offset++)
            {
                char x = alignment.CharacterMatrix[i, j + offset];
                if (Bioinformatics.IsGapChar(x))
                {
                    gapsFound = true;
                }
                else
                {
                    residuesFound = true;
                }
                if (residuesFound && gapsFound)
                {
                    return true;
                }
            }

            return false;
        }

        public void PermuteBlockAt(Alignment alignment, int n, int i, int j)
        {
            char[] bestSection = GetBestPerformingBlockPermutation(alignment, n, i, j);
            for(int offset = 0; offset < n; offset++)
            {
                alignment.CharacterMatrix[i, j + offset] = bestSection[offset];
            }
        }

        public char[] GetBestPerformingBlockPermutation(Alignment alignment, int n, int i, int j)
        {
            Dictionary<char, int>[] tables = GetComplementTables(alignment, n, i, j);
            char[] originalBlock = ExtractBlock(alignment, n, i, j);
            List<char> residues = ExtractResidues(originalBlock);
            bool[] blockBitmask = ConvertBlockToBitmask(originalBlock);

            List<bool[]> permutations = GetValidBitmaskPermutations(blockBitmask);
            permutations.Remove(blockBitmask);

            double bestScore = 0.0;
            char[] bestSection = originalBlock;
            foreach (bool[] option in permutations)
            {
                char[] section = ConvertBitmaskToBlock(option, residues);
                double score = ScoreSection(section, tables);
                if (score > bestScore)
                {
                    bestSection = section;
                    bestScore = score;
                }
            }

            return bestSection;
        }


        public double ScoreSection(char[] section, Dictionary<char, int>[] tables)
        {
            double total = 0;
            for (int i= 0; i < section.Length; i++)
            {
                double score = GetIncreaseInScore(section[i], tables[i]);
                total += score;
            }

            return total;
        }

        public double GetIncreaseInScore(char x, Dictionary<char, int> table)
        {
            double total = 0;
            foreach(char y in table.Keys)
            {
                int count = table.Count;
                int pairScore = Matrix.ScorePair(x, y);
                total += pairScore * count;
            }

            return total;
        }

        public List<char> ExtractResidues(char[] block)
        {
            List<char> result = new List<char>();

            foreach(char x in block)
            {
                if (!Bioinformatics.IsGapChar(x))
                {
                    result.Add(x);
                }
            }

            return result;
        }

        public List<bool[]> GetValidBitmaskPermutations(bool[] original)
        {
            int requiredTotal = GetBitmaskSum(original);

            List<bool[]> permutations = GetBitmaskPermutations(original.Length);
            List<bool[]> result = new List<bool[]>();
            foreach (bool[] mask in permutations)
            {
                int total = GetBitmaskSum(mask);
                if (total == requiredTotal)
                {
                    result.Add(mask);
                }
            }

            return result;
        }

        public int GetBitmaskSum(in bool[] mask)
        {
            int total = 0;
            foreach(bool bit in mask)
            {
                if (bit)
                {
                    total++;
                }
            }

            return total;
        }

        public List<bool[]> GetBitmaskPermutations(int length)
        {
            int totalPermutations = 1 << length;
            List<bool[]> result = new List<bool[]>();

            for (int i = 0; i < totalPermutations; i++)
            {
                bool[] bitmask = new bool[length];
                for (int j = 0; j < length; j++)
                {
                    bitmask[j] = (i & (1 << j)) != 0;
                }
                result.Add(bitmask);
            }

            return result;
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

        public bool[] ConvertBlockToBitmask(char[] array)
        {
            bool[] result = new bool[array.Length];

            for(int i=0; i<array.Length; i++)
            {
                char x = array[i];
                if (Bioinformatics.IsGapChar(x))
                {
                    result[i] = false;
                }
                else
                {
                    result[i] = true;
                }
            }

            return result;
        }

        public char[] ConvertBitmaskToBlock(bool[] mask, List<char> residues)
        {
            char[] result = new char[mask.Length];

            int p = 0;
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i])
                {
                    result[i] = residues[p++];
                }
                else
                {
                    result[i] = '-';
                }
            }

            return result;
        }

        public Dictionary<char, int>[] GetComplementTables(Alignment alignment, int n, int i, int j)
        {
            Dictionary<char, int>[] tables = new Dictionary<char, int>[n];

            for (int offset = 0; offset < n; offset++)
            {
                Dictionary<char, int> table = new Dictionary<char, int>();
                PopulateTable(alignment, j + offset, table);
                char x = alignment.CharacterMatrix[i, j + offset];

                if (!Bioinformatics.IsGapChar(x))
                {
                    table[x]--;
                }

                tables[offset] = table;
            }

            return tables;
        }

        public void PopulateTable(Alignment alignment, int j, Dictionary<char, int> table)
        {
            char[,] matrix = alignment.CharacterMatrix;

            for(int i=0; i<alignment.Height; i++)
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
        }


        public int PickBlockLength(Alignment alignment)
        {
            int length = Randomizer.Random.Next(MinBlockLength, MaxBlockLength + 1);
            return Math.Min(alignment.Width, length);
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
