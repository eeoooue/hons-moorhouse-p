using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.PairwiseAligners
{
    public class NeedlemanWunschPairwiseAligner
    {
        public int MatchScore = 1;
        public int MismatchScore = -1;
        public int GapScore = -2;

        public int M;
        public int N;
        public int[,] Scores;

        public bool ScoresPopulated = false;

        public string SequenceA;
        public string SequenceB;


        public NeedlemanWunschPairwiseAligner(string sequenceA, string sequenceB)
        {
            SequenceA = sequenceA;
            SequenceB = sequenceB;
            M = SequenceA.Length + 1;
            N = SequenceB.Length + 1;
            Scores = new int[M, N];
        }

        public char[,] ExtractPairwiseAlignment(string sequenceA, string sequenceB)
        {
            if (!ScoresPopulated)
            {
                PopulateTable();
            }

            return new char[2, 10];
        }

        public void PopulateTable()
        {
            if (ScoresPopulated)
            {
                return;
            }

            for(int i= 0; i<M; i++)
            {
                for(int j=0; j<N; j++)
                {
                    PopulateCoordinates(i, j);
                }
            }

            ScoresPopulated = true;
        }

        public void PopulateCoordinates(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                Scores[i, j] = 0;
                return;
            }

            List<int> options = new List<int>();

            if (i > 0 && j > 0)
            {
                int pairwiseScore = (SequenceA[i - 1] == SequenceB[j - 1]) ? MatchScore : MismatchScore;
                int option1 = Scores[i - 1, j - 1] + pairwiseScore;
                options.Add(option1);
            }

            if (i > 0)
            {
                int option2 = Scores[i - 1, j] + GapScore;
                options.Add(option2);
            }

            if (j > 0)
            {
                int option3 = Scores[i, j - 1] + GapScore;
                options.Add(option3);
            }

            int bestScore = int.MinValue;
            foreach(int option in options)
            {
                bestScore = Math.Max(bestScore, option);
            }

            Scores[i, j] = bestScore;
        }
    }
}
