using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ScoringMatrices
{
    public class BLOSUM62Matrix : IScoringMatrix
    {
        public int[,] ScoreValues;
        public Dictionary<char, int> ResidueIndexes = new Dictionary<char, int>();

        public BLOSUM62Matrix()
        {
            ScoreValues = new int[20, 20] {
                { 9,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    4,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,5,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,4,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,6,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,7,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  6,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,5,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,5,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,6,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    8,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,5,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,5,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  5,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,4,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,4,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,4,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    11,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,7,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,6 },
            };

            ResidueIndexes['C'] = 0;

            ResidueIndexes['S'] = 1;
            ResidueIndexes['T'] = 2;
            ResidueIndexes['A'] = 3;
            ResidueIndexes['G'] = 4;
            ResidueIndexes['P'] = 5;

            ResidueIndexes['D'] = 6;
            ResidueIndexes['E'] = 7;
            ResidueIndexes['Q'] = 8;
            ResidueIndexes['N'] = 9;

            ResidueIndexes['H'] = 10;
            ResidueIndexes['R'] = 11;
            ResidueIndexes['K'] = 12;

            ResidueIndexes['M'] = 13;
            ResidueIndexes['I'] = 14;
            ResidueIndexes['L'] = 15;
            ResidueIndexes['V'] = 16;

            ResidueIndexes['W'] = 17;
            ResidueIndexes['Y'] = 18;
            ResidueIndexes['F'] = 19;
        }


        public int ScorePair(char a, char b)
        {
            if (ResidueIndexes.ContainsKey(a) && ResidueIndexes.ContainsKey(b))
            {
                int val1 = ResidueIndexes[a];
                int val2 = ResidueIndexes[b];
                int i = Math.Max(val1, val2);
                int j = Math.Min(val1, val2);
                return ScoreValues[i, j];
            }

            return 0;
        }
    }
}
