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
            SequenceA = $"-{sequenceA}";
            SequenceB = $"-{sequenceB}";
            M = SequenceA.Length;
            N = SequenceB.Length;
            Scores = new int[M, N];
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

            int bestScore = int.MinValue;

            if (i > 0 && j > 0)
            {
                int option1 = Scores[i - 1, j - 1] + GetPairwiseScore(i, j);
                bestScore = Math.Max(bestScore, option1);
            }

            if (i > 0)
            {
                int option2 = Scores[i - 1, j] + GapScore;
                bestScore = Math.Max(bestScore, option2);
            }

            if (j > 0)
            {
                int option3 = Scores[i, j - 1] + GapScore;
                bestScore = Math.Max(bestScore, option3);
            }

            Scores[i, j] = bestScore;
        }

        public int GetPairwiseScore(int i, int j)
        {
            return (SequenceA[i] == SequenceB[j]) ? MatchScore : MismatchScore;
        }


        #region extracting alignment via backtracking

        public char[,] ExtractPairwiseAlignment()
        {
            if (!ScoresPopulated)
            {
                PopulateTable();
            }

            StringBuilder rowA = new StringBuilder();
            StringBuilder rowB = new StringBuilder();

            Backtrack(ref rowA, ref rowB, M - 1, N - 1);

            return ConstructAsMatrix(rowA, rowB);
        }

        public char[,] ConstructAsMatrix(StringBuilder a, StringBuilder b)
        {
            string seqA = RecoverPayload(a);
            string seqB = RecoverPayload(b);

            int n = seqA.Length;

            char[,] result = new char[2, n];

            for (int j = 0; j < n; j++)
            {
                result[0, j] = seqA[j];
                result[1, j] = seqB[j];
            }

            return result;
        }

        public string RecoverPayload(StringBuilder sb)
        {
            string reversed = sb.ToString();
            StringBuilder result = new StringBuilder();
            for (int i = reversed.Length - 1; i >= 0; i--)
            {
                result.Append(reversed[i]);
            }

            return result.ToString();
        }

        public void ExtractPairwiseAlignment(out string sequenceA, out string sequenceB)
        {
            if (!ScoresPopulated)
            {
                PopulateTable();
            }

            StringBuilder a = new StringBuilder();
            StringBuilder b = new StringBuilder();

            Backtrack(ref a, ref b, M - 1, N - 1);

            sequenceA = RecoverPayload(a);
            sequenceB = RecoverPayload(b);
        }

        public void Backtrack(ref StringBuilder a, ref StringBuilder b, int i, int j)
        {
            if (i == 0 && j == 0)
            {
                return;
            }

            if (CanReachByPair(i, j))
            {
                a.Append(SequenceA[i]);
                b.Append(SequenceB[j]);
                Backtrack(ref a, ref b, i - 1, j - 1);
            }
            else if (CanReachUpwards(i, j))
            {
                a.Append(SequenceA[i]);
                b.Append('-');
                Backtrack(ref a, ref b, i - 1, j);
            }
            else if (CanReachLeftwise(i, j))
            {
                a.Append('-');
                b.Append(SequenceB[j]);
                Backtrack(ref a, ref b, i, j - 1);
            }
        }

        public bool CanReachByPair(int i, int j)
        {
            if (i > 0 && j > 0)
            {
                int destinationScore = Scores[i - 1, j - 1];
                int pairwiseScore = GetPairwiseScore(i, j);
                int currentScore = Scores[i, j];

                if (currentScore - pairwiseScore == destinationScore)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanReachUpwards(int i, int j)
        {
            if (i > 0)
            {
                int destinationScore = Scores[i - 1, j];
                int currentScore = Scores[i, j];
                if (currentScore - GapScore == destinationScore)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanReachLeftwise(int i, int j)
        {
            if (j > 0)
            {
                int destinationScore = Scores[i, j - 1];
                int currentScore = Scores[i, j];
                if (currentScore - GapScore == destinationScore)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
