using LibBioInfo.PairwiseAligners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;

namespace TestsUnitSuite.LibBioInfo.PairwiseAligners
{
    [TestClass]
    public class NeedlemanWunschAlignerTests
    {
        public NeedlemanWunschPairwiseAligner GetAlignerWithScores(string a, string b)
        {
            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(a, b);

            aligner.MatchScore = 1;
            aligner.MismatchScore = -1;
            aligner.GapScore = -2;

            return aligner;
        }

        [TestMethod]
        public void CanPopulateTable()
        {
            string a = "ATCGT";
            string b = "TGGTG";

            int[,] expected = new int[,]
            {
                {   0,  -2,  -4,  -6,  -8, -10 },
                {  -2,  -1,  -3,  -5,  -7,  -9 },
                {  -4,  -1,  -2,  -4,  -4,  -6 },
                {  -6,  -3,  -2,  -3,  -5,  -5 },
                {  -8,  -5,  -2,  -1,  -3,  -4 },
                { -10,  -7,  -4,  -3,   0,  -2 },
            };

            NeedlemanWunschPairwiseAligner aligner = GetAlignerWithScores(a, b);
            aligner.PopulateTable();

            PrintTable(aligner.Scores);

            bool tableIsCorrect = TablesMatch(expected, aligner.Scores);
            Assert.IsTrue(tableIsCorrect);
        }


        [TestMethod]

        public void CanExtractPair()
        {
            string a = "ATCGT";
            string b = "TGGTG";

            int[,] expected = new int[,]
            {
                {   0,  -2,  -4,  -6,  -8, -10 },
                {  -2,  -1,  -3,  -5,  -7,  -9 },
                {  -4,  -1,  -2,  -4,  -4,  -6 },
                {  -6,  -3,  -2,  -3,  -5,  -5 },
                {  -8,  -5,  -2,  -1,  -3,  -4 },
                { -10,  -7,  -4,  -3,   0,  -2 },
            };

            NeedlemanWunschPairwiseAligner aligner = GetAlignerWithScores(a, b);
            aligner.PopulateTable();
            char[,] result = aligner.ExtractPairwiseAlignment();

            PrintAlignment(result);
        }

        public void PrintAlignment(char[,] state)
        {
            int m = state.GetLength(0);
            int n = state.GetLength(1);

            for (int i=0; i<m; i++)
            {
                StringBuilder sb = new StringBuilder();
                for(int j=0; j<n; j++)
                {
                    sb.Append(state[i, j]);
                }

                Console.WriteLine(sb.ToString());
                sb.Clear();
            }

        }

        public bool TablesMatch(int[,] expected, int[,] actual)
        {
            int m = expected.GetLength(0);
            int n = expected.GetLength(1);

            int countMatching = CountMatchingPositions(expected, actual);
            int goal = m * n;

            return countMatching == goal;
        }

        public int CountMatchingPositions(int[,] expected, int[,] actual)
        {
            int m = expected.GetLength(0);
            int n = expected.GetLength(1);

            int result = 0;
            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if (expected[i, j] == actual[i, j])
                    {
                        result++;
                    }
                }
            }

            return result;
        }




        #region debugging

        public void PrintTable(int[,] table)
        {
            int m = table.GetLength(0);
            int n = table.GetLength(1);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int value = table[i, j];
                    string contrib = GetPaddedValue(value);
                    sb.Append(contrib);
                }

                string s = sb.ToString();
                Console.WriteLine(s);
                sb.Clear();
            }
        }

        public string GetPaddedValue(int x)
        {
            string starter = x.ToString();

            while (starter.Length < 3)
            {
                starter = " " + starter;
            }

            return $": {starter} :";
        }

        #endregion
    }
}
