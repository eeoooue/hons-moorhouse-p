using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ScoringMatrices
{
    public class PAM250Matrix : IScoringMatrix
    {
        public int[,] ScoreValues;
        public Dictionary<char, int> ResidueIndexes = new Dictionary<char, int>();

        public PAM250Matrix()
        {
            ScoreValues = new int[20, 20] {
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
            };

            ResidueIndexes['C'] = 0;

            ResidueIndexes['S'] = 1;
            ResidueIndexes['T'] = 2;
            ResidueIndexes['P'] = 3;
            ResidueIndexes['A'] = 4;
            ResidueIndexes['G'] = 5;

            ResidueIndexes['N'] = 6;
            ResidueIndexes['D'] = 7;
            ResidueIndexes['E'] = 8;
            ResidueIndexes['Q'] = 9;

            ResidueIndexes['H'] = 10;
            ResidueIndexes['R'] = 11;
            ResidueIndexes['K'] = 12;

            ResidueIndexes['M'] = 13;
            ResidueIndexes['I'] = 14;
            ResidueIndexes['L'] = 15;
            ResidueIndexes['V'] = 16;

            ResidueIndexes['F'] = 17;
            ResidueIndexes['Y'] = 18;
            ResidueIndexes['W'] = 19;
        }

        public string GetName()
        {
            return "PAM250 Matrix (INCOMPLETE)";
        }

        public List<char> GetResidues()
        {
            return ResidueIndexes.Keys.ToList();
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
