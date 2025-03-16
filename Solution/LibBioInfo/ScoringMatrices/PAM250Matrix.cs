using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ScoringMatrices
{
    public class PAM250Matrix : IScoringMatrix
    {
        public int[,] ScoreValues;
        public Dictionary<char, int> ResidueIndexes = new Dictionary<char, int>();

        public PAM250Matrix()
        {
            ScoreValues = new int[20, 20] {
                { 12 ,    0,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                {  0 ,    2,0,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -2 ,    1,3,0,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -3 ,    1,0,6,0,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -2 ,    1,1,1,2,0,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -3 ,    1,0,0,1,5,  0,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },

                { -4 ,  1  ,0  ,0  ,0  ,0  ,  2,0,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -5 ,  0  ,0  ,-1  ,0  ,1  ,  2,4,0,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -5 ,  0  ,0  ,-1  ,0  ,0  ,  1,3,4,0,    0,0,0,  0,0,0,0,    0,0,0 },
                { -5 ,  -1  ,-1  ,0  ,0  ,-1  ,  1,2,2,4,    0,0,0,  0,0,0,0,    0,0,0 },

                { -3 ,  -1  ,-1  ,0  ,-1  ,-2  ,    2  ,1  ,1  ,3  ,    6,0,0,  0,0,0,0,    0,0,0 },
                { -4 ,  0  ,-1  ,0  ,-2  ,-3  ,    0  ,-1  ,-1  ,1  ,    2,6,0,  0,0,0,0,    0,0,0 },
                { -5 ,  0  ,0  ,-1  ,-1  ,-2  ,    1  ,0  ,0  ,1  ,    0,3,5,  0,0,0,0,    0,0,0 },

                { -5 ,  -2  ,-1 ,-2 ,-1 ,-3 ,    -2 ,-3 ,-2 ,-1 ,    -2 ,0  ,0  ,    6  ,0  ,0  ,0  ,    0,0,0 },
                { -2 ,  -1  ,0  ,-2 ,-1 ,-3 ,    -2 ,-2 ,-2 ,-2 ,    -2 ,-2 ,-2 ,    2  ,5  ,0  ,0  ,    0,0,0 },
                { -6 ,  -3  ,-2 ,-3 ,-2 ,-4 ,    -3 ,-4 ,-3 ,-2 ,    -2 ,-3 ,-3 ,    4  ,2  ,6  ,0  ,    0,0,0 },
                { -2 ,  -1  ,0  ,-1 ,0  ,-1 ,    -2 ,-2 ,-2 ,-2 ,    -2 ,-2 ,-2 ,  2  ,4  ,2  ,4  ,  0  ,0  ,0   },

                { -4 ,  -3 ,-3 ,-5 ,-3 ,-5 ,    -3 ,-6 ,-5 ,-5 ,    -2 ,-4 ,-5 ,   0 , 1 , 2 ,-1 ,  9  , 0  , 0   },
                { 0  ,  -3 ,-3 ,-5 ,-3 ,-5 ,    -2 ,-4 ,-4 ,-4 ,     0 ,-4 ,-4 ,  -2 ,-1 ,-1 ,-2 ,  7  , 10 , 0   },
                { -8 ,  -2 ,-5 ,-6 ,-6 ,-7 ,    -4 ,-7 ,-7 ,-5 ,    -3 , 2 ,-3 ,  -4 ,-5 ,-2 ,-6 ,  0  , 0  , 17  },
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
            return "PAM250 Matrix";
        }

        public List<char> GetResidues()
        {
            return ResidueIndexes.Keys.ToList();
        }

        public double GetBestPairwiseScorePossible()
        {
            return 17;
        }

        public double GetWorstPairwiseScorePossible()
        {
            return -8;
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
                if ("STAN".Contains(b))
                {
                    return 0;
                }
                if ("FY".Contains(b))
                {
                    return -2;
                }
                if ("C".Contains(b))
                {
                    return -3;
                }
                if ("W".Contains(b))
                {
                    return -4;
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
