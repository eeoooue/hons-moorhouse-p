using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ScoringMatrices
{
    public class BLOSUM62Matrix : IScoringMatrix
    {
        public int[,] ScoreValues;
        public Dictionary<char, int> ResidueIndexes = new Dictionary<char, int>();
        public List<char> Residues = new List<char>();

        public BLOSUM62Matrix()
        {
            ScoreValues = new int[20, 20] {
                { 9,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { -1,    4,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -1,    1,5,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { 0,     1,0,4,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -3,    0,-2,0,6,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -3,    -1,-1,-1,-2,7,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { -3,    0,-1,-2,-1,-1,  6,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -4,    0,-1,-1,-2,-1,   2,5,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -3,    0,-1,-1,-2,-1,  0,2,5,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -3,    1,0,-2,0,-2,  1,0,0,6,    0,0,0,  0,0,0,0,    0,0,0 },

                { -3,    -1,-2,-2,-2,-2,  -1,0,0,1,    8,0,0,  0,0,0,0,    0,0,0 },
                { -3,    -1,-1,-1,-2,-2,  -2,0,1,0,    0,5,0,  0,0,0,0,    0,0,0 },
                { -3,    0,-1,-1,-2,-1,  -1,1,1,0,    -1,2,5,  0,0,0,0,    0,0,0 },

                { -1,    -1,-1,-1,-3,-2,  -3,-2,0,-2,    -2,-1,-1,  5,0,0,0,    0,0,0 },
                { -1,    -2,-1,-1,-4,-3,  -3,-3,-3,-3,    -3,-3,-3,  1,4,0,0,    0,0,0 },
                { -1,    -2,-1,-1,-4,-3,  -4,-3,-2,-3,    -3,-2,-2,  2,2,4,0,    0,0,0 },
                { -1,    -2,0,0,-3,-2,    -3,-2,-2,-3,      -3,-3,-2,  1,3,1,4,    0,0,0 },

                { -2,    -3,-2,-3,-2,-4,  -4,-3,-2,-4,    -2,-3,-3,  -1,-3,-2,-3,    11,0,0 },
                { -2,    -2,-2,-2,-3,-3,  -3,-2,-1,-2,    2,-2,-2,  -1,-1,-1,-1,    2,7,0 },
                { -2,    -2,-2,-2,-3,-4,  -3,-3,-3,-3,    -1,-3,-3,  0,0,0,-1,    1,3,6 },
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

            Residues = ResidueIndexes.Keys.ToList();
            Residues.Add('B');
            Residues.Add('X');
            Residues.Add('Z');
        }



        public string GetName()
        {
            return "BLOSUM62 Matrix";
        }

        public List<char> GetResidues()
        {
            return Residues;
        }

        public double GetBestPairwiseScorePossible()
        {
            return 11;
        }

        public double GetWorstPairwiseScorePossible()
        {
            return -4;
        }

        public int ScorePair(char a, char b)
        {
            if (a == 'B' || b == 'B')
            {
                return ScoreBPair(a, b);
            }

            if (a == 'X' || b == 'X')
            {
                return ScoreXPair(a, b);
            }

            if (a == 'Z' || b == 'Z')
            {
                return ScoreZPair(a, b);
            }

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


        public int ScoreBPair(char a, char b)
        {
            if (a == 'B')
            {
                if (b == 'N')
                {
                    return 3;
                }

                if (b == 'D')
                {
                    return 4;
                }

                if (b == 'R')
                {
                    return -1;
                }

                if (b == 'K' || b == 'H')
                {
                    return 0;
                }

                if (b == 'E')
                {
                    return 1;
                }

                int score1 = ScorePair('D', b);
                int score2 = ScorePair('N', b);

                return Math.Min(score1, score2);
            }
            else
            {
                return ScorePair(b, a);
            }
        }

        public int ScoreXPair(char a, char b)
        {
            if (a == 'X')
            {
                if ("STA".Contains(b))
                {
                    return 0;
                }

                if ("WCP".Contains(b))
                {
                    return -2;
                }

                return -1;
            }
            else
            {
                return ScorePair(b, a);
            }
        }

        public int ScoreZPair(char a, char b)
        {
            if (a == 'Z')
            {
                if (b == 'C')
                {
                    return -3;
                }

                if (b == 'D')
                {
                    return 1;
                }

                if (b == 'E')
                {
                    return 4;
                }

                if (b == 'M')
                {
                    return -1;
                }

                if (b == 'Q')
                {
                    return 3;
                }


                int score1 = ScorePair('E', b);
                int score2 = ScorePair('Q', b);

                return Math.Min(score1, score2);
            }
            else
            {
                return ScorePair(b, a);
            }
        }
    }
}
