﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.Metrics
{
    public class SumOfPairsScore
    {
        public IScoringMatrix Matrix;

        public SumOfPairsScore(IScoringMatrix matrix)
        {
            Matrix = matrix;
        }

        public double ScoreAlignment(in char[,] alignment)
        {
            int n = alignment.GetLength(1);

            double result = 0;
            for (int j = 0; j < n; j++)
            {
                result += ScoreColumn(alignment, j);
            }

            return result;
        }

        public double ScoreColumn(in char[,] alignment, int j)
        {
            Dictionary<char, int> table = ConstructCounterTableForColumn(alignment, j);
            double result = ScorePairwiseCombinations(table);

            return result;
        }

        public Dictionary<char, int> ConstructCounterTableForColumn(in char[,] matrix, int j)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();
            foreach (char residue in Matrix.GetResidues())
            {
                result[residue] = 0;
            }

            int m = matrix.GetLength(0);
            for (int i = 0; i < m; i++)
            {
                char x = matrix[i, j];
                if (result.ContainsKey(x))
                {
                    result[x] += 1;
                }
            }

            return result;
        }

        public double ScorePairwiseCombinations(Dictionary<char, int> table)
        {
            List<char> residues = Matrix.GetResidues();
            double result = 0;

            for (int i1 = 0; i1 < residues.Count; i1++)
            {
                char a = residues[i1];
                int a_count = table[a];

                for (int i2 = i1; i2 < residues.Count; i2++)
                {
                    char b = residues[i2];
                    int b_count = table[b];

                    int combinations = 0;

                    if (a != b)
                    {
                        combinations = a_count * b_count;
                    }
                    else
                    {
                        combinations = a_count * (a_count - 1) / 2;
                    }

                    int score = ScorePair(a, b);
                    int contribution = score * combinations;
                    result += contribution;
                }
            }

            return result;
        }

        public int ScorePair(char a, char b)
        {
            return Matrix.ScorePair(a, b);
        }

        public int GetNumberOfPossiblePairs(in char[,] alignment)
        {
            int m = alignment.GetLength(0);
            int n = GetNumberOfResiduesInFirstRow(alignment);
            int pairsPerColumn = GetPossiblePairsInColumnOfHeight(m);
            return pairsPerColumn * n;
        }

        public int GetNumberOfResiduesInFirstRow(in char[,] alignment)
        {
            int n = alignment.GetLength(1);
            int total = 0;

            for (int j = 0; j < n; j++)
            {
                if (!Bioinformatics.IsGap(alignment[0, j]))
                {
                    total++;
                }
            }

            return total;
        }

        public int GetPossiblePairsInColumnOfHeight(int n)
        {
            if (n < 2)
            {
                return 0;
            }

            int permutations = n * (n - 1);
            int combinations = permutations / 2;
            return combinations;
        }

        public double GetBestPossibleScore(in char[,] alignment)
        {
            double bestScore = Matrix.GetBestPairwiseScorePossible();
            int possiblePairs = GetNumberOfPossiblePairs(alignment);
            return possiblePairs * bestScore;
        }

        public double GetWorstPossibleScore(in char[,] alignment)
        {
            double worstScore = Matrix.GetWorstPairwiseScorePossible();
            int possiblePairs = GetNumberOfPossiblePairs(alignment);
            return possiblePairs * worstScore;
        }
    }
}
