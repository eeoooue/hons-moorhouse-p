﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.PairwiseAligners
{
    enum BacktrackingDirection
    {
        Diagonal,
        Leftwise,
        Upwards,
    }

    public class NeedlemanWunschPairwiseAligner
    {
        public int MatchScore { get { return ScoringScheme.ResidueMatch; } }
        public int MismatchScore { get { return ScoringScheme.ResidueMismatch; } }
        public int GapScore { get { return ScoringScheme.ResidueWithGap; } }

        public PairwiseScoringScheme ScoringScheme;

        public int M;
        public int N;
        public int[,] Scores;
        public bool ScoresPopulated = false;
        public string SequenceA;
        public string SequenceB;

        public NeedlemanWunschPairwiseAligner(string sequenceA, string sequenceB)
        {
            ScoringScheme = new PairwiseScoringScheme(0, -20, -25);
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

        public int GetSimilarityScore()
        {
            if (!ScoresPopulated)
            {
                PopulateTable();
            }

            return Scores[M - 1, N - 1];
        }

        private void PopulateCoordinates(int i, int j)
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

        private int GetPairwiseScore(int i, int j)
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

            string sequenceA = RecoverPayload(rowA);
            string sequenceB = RecoverPayload(rowB);

            return ConstructAsMatrix(sequenceA, sequenceB);
        }

        private char[,] ConstructAsMatrix(string seqA, string seqB)
        {
            int n = seqA.Length;

            char[,] result = new char[2, n];
            for (int j = 0; j < n; j++)
            {
                result[0, j] = seqA[j];
                result[1, j] = seqB[j];
            }

            return result;
        }

        private string RecoverPayload(StringBuilder sb)
        {
            string reversed = sb.ToString();
            StringBuilder result = new StringBuilder();
            for (int i = reversed.Length - 1; i >= 0; i--)
            {
                result.Append(reversed[i]);
            }

            return result.ToString();
        }

        private void Backtrack(ref StringBuilder a, ref StringBuilder b, int i, int j)
        {
            if (i == 0 && j == 0)
            {
                return;
            }

            List<BacktrackingDirection> options = CollectOptions(i, j);
            BacktrackingDirection choice = PickDirectionRandomly(options);

            switch (choice)
            {
                case BacktrackingDirection.Diagonal:
                    a.Append(SequenceA[i]);
                    b.Append(SequenceB[j]);
                    Backtrack(ref a, ref b, i - 1, j - 1);
                    return;
                case BacktrackingDirection.Upwards:
                    a.Append(SequenceA[i]);
                    b.Append('-');
                    Backtrack(ref a, ref b, i - 1, j);
                    return;
                case BacktrackingDirection.Leftwise:
                    a.Append('-');
                    b.Append(SequenceB[j]);
                    Backtrack(ref a, ref b, i, j - 1);
                    return;
                default:
                    return;
            }
        }

        private BacktrackingDirection PickDirectionRandomly(List<BacktrackingDirection> options)
        {
            int n = options.Count;
            int i = Randomizer.Random.Next(n);

            return options[i];
        }


        private List<BacktrackingDirection> CollectOptions(int i, int j)
        {
            List<BacktrackingDirection> options = new List<BacktrackingDirection>();

            if (CanReachByPair(i, j))
            {
                options.Add(BacktrackingDirection.Diagonal);
            }
            if (CanReachUpwards(i, j))
            {
                options.Add(BacktrackingDirection.Upwards);
            }
            if (CanReachLeftwise(i, j))
            {
                options.Add(BacktrackingDirection.Leftwise);
            }

            return options;
        }

        private bool CanReachByPair(int i, int j)
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

        private bool CanReachUpwards(int i, int j)
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

        private bool CanReachLeftwise(int i, int j)
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
