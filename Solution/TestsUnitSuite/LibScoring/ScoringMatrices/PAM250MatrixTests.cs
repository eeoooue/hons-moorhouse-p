﻿using LibBioInfo;
using LibBioInfo.ScoringMatrices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibScoring.ScoringMatrices
{
    [TestClass]
    public class PAM250MatrixTests
    {
        IScoringMatrix Matrix = new PAM250Matrix();


        [DataTestMethod]
        [DataRow('C', 12)]

        [DataRow('S', 2)]
        [DataRow('T', 3)]
        [DataRow('P', 6)]
        [DataRow('A', 2)]
        [DataRow('G', 5)]

        [DataRow('N', 2)]
        [DataRow('D', 4)]
        [DataRow('E', 4)]
        [DataRow('Q', 4)]

        [DataRow('H', 6)]
        [DataRow('R', 6)]
        [DataRow('K', 5)]

        [DataRow('M', 6)]
        [DataRow('I', 5)]
        [DataRow('L', 6)]
        [DataRow('V', 4)]

        [DataRow('F', 9)]
        [DataRow('Y', 10)]
        [DataRow('W', 17)]

        public void MatchingPairScoresAreCorrect(char a, int expected)
        {
            int score = Matrix.ScorePair(a, a);
            Assert.AreEqual(expected, score);
        }


        [DataTestMethod]
        [DataRow('T', 'C', -2)]
        [DataRow('K', 'C', -5)]
        [DataRow('R', 'S', 0)]
        [DataRow('N', 'P', -1)]
        [DataRow('I', 'P', -2)]
        [DataRow('E', 'G', 0)]
        [DataRow('M', 'N', -2)]
        [DataRow('V', 'Q', -2)]
        [DataRow('K', 'Y', -4)]

        public void SpotCheckedPairsAreCorrect(char a, char b, int expected)
        {
            int scoreAB = Matrix.ScorePair(a, b);
            int scoreBA = Matrix.ScorePair(b, a);
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }



        #region Tests Generated Using BioPython


        [DataTestMethod]
        [DataRow('C', 'C', 12.0)]
        [DataRow('C', 'S', 0.0)]
        [DataRow('C', 'T', -2.0)]
        [DataRow('C', 'A', -2.0)]
        [DataRow('C', 'G', -3.0)]
        [DataRow('C', 'P', -3.0)]
        [DataRow('C', 'D', -5.0)]
        [DataRow('C', 'E', -5.0)]
        [DataRow('C', 'Q', -5.0)]
        [DataRow('C', 'N', -4.0)]
        [DataRow('C', 'H', -3.0)]
        [DataRow('C', 'R', -4.0)]
        [DataRow('C', 'K', -5.0)]
        [DataRow('C', 'M', -5.0)]
        [DataRow('C', 'I', -2.0)]
        [DataRow('C', 'L', -6.0)]
        [DataRow('C', 'V', -2.0)]
        [DataRow('C', 'W', -8.0)]
        [DataRow('C', 'Y', 0.0)]
        [DataRow('C', 'F', -4.0)]
        [DataRow('S', 'S', 2.0)]
        [DataRow('S', 'T', 1.0)]
        [DataRow('S', 'A', 1.0)]
        [DataRow('S', 'G', 1.0)]
        [DataRow('S', 'P', 1.0)]
        [DataRow('S', 'D', 0.0)]
        [DataRow('S', 'E', 0.0)]
        [DataRow('S', 'Q', -1.0)]
        [DataRow('S', 'N', 1.0)]
        [DataRow('S', 'H', -1.0)]
        [DataRow('S', 'R', 0.0)]
        [DataRow('S', 'K', 0.0)]
        [DataRow('S', 'M', -2.0)]
        [DataRow('S', 'I', -1.0)]
        [DataRow('S', 'L', -3.0)]
        [DataRow('S', 'V', -1.0)]
        [DataRow('S', 'W', -2.0)]
        [DataRow('S', 'Y', -3.0)]
        [DataRow('S', 'F', -3.0)]
        [DataRow('T', 'T', 3.0)]
        [DataRow('T', 'A', 1.0)]
        [DataRow('T', 'G', 0.0)]
        [DataRow('T', 'P', 0.0)]
        [DataRow('T', 'D', 0.0)]
        [DataRow('T', 'E', 0.0)]
        [DataRow('T', 'Q', -1.0)]
        [DataRow('T', 'N', 0.0)]
        [DataRow('T', 'H', -1.0)]
        [DataRow('T', 'R', -1.0)]
        [DataRow('T', 'K', 0.0)]
        [DataRow('T', 'M', -1.0)]
        [DataRow('T', 'I', 0.0)]
        [DataRow('T', 'L', -2.0)]
        [DataRow('T', 'V', 0.0)]
        [DataRow('T', 'W', -5.0)]
        [DataRow('T', 'Y', -3.0)]
        [DataRow('T', 'F', -3.0)]
        [DataRow('A', 'A', 2.0)]
        [DataRow('A', 'G', 1.0)]
        [DataRow('A', 'P', 1.0)]
        [DataRow('A', 'D', 0.0)]
        [DataRow('A', 'E', 0.0)]
        [DataRow('A', 'Q', 0.0)]
        [DataRow('A', 'N', 0.0)]
        [DataRow('A', 'H', -1.0)]
        [DataRow('A', 'R', -2.0)]
        [DataRow('A', 'K', -1.0)]
        [DataRow('A', 'M', -1.0)]
        [DataRow('A', 'I', -1.0)]
        [DataRow('A', 'L', -2.0)]
        [DataRow('A', 'V', 0.0)]
        [DataRow('A', 'W', -6.0)]
        [DataRow('A', 'Y', -3.0)]
        [DataRow('A', 'F', -3.0)]
        [DataRow('G', 'G', 5.0)]
        [DataRow('G', 'P', 0.0)]
        [DataRow('G', 'D', 1.0)]
        [DataRow('G', 'E', 0.0)]
        [DataRow('G', 'Q', -1.0)]
        [DataRow('G', 'N', 0.0)]
        [DataRow('G', 'H', -2.0)]
        [DataRow('G', 'R', -3.0)]
        [DataRow('G', 'K', -2.0)]
        [DataRow('G', 'M', -3.0)]
        [DataRow('G', 'I', -3.0)]
        [DataRow('G', 'L', -4.0)]
        [DataRow('G', 'V', -1.0)]
        [DataRow('G', 'W', -7.0)]
        [DataRow('G', 'Y', -5.0)]
        [DataRow('G', 'F', -5.0)]
        [DataRow('P', 'P', 6.0)]
        [DataRow('P', 'D', -1.0)]
        [DataRow('P', 'E', -1.0)]
        [DataRow('P', 'Q', 0.0)]
        [DataRow('P', 'N', 0.0)]
        [DataRow('P', 'H', 0.0)]
        [DataRow('P', 'R', 0.0)]
        [DataRow('P', 'K', -1.0)]
        [DataRow('P', 'M', -2.0)]
        [DataRow('P', 'I', -2.0)]
        [DataRow('P', 'L', -3.0)]
        [DataRow('P', 'V', -1.0)]
        [DataRow('P', 'W', -6.0)]
        [DataRow('P', 'Y', -5.0)]
        [DataRow('P', 'F', -5.0)]
        [DataRow('D', 'D', 4.0)]
        [DataRow('D', 'E', 3.0)]
        [DataRow('D', 'Q', 2.0)]
        [DataRow('D', 'N', 2.0)]
        [DataRow('D', 'H', 1.0)]
        [DataRow('D', 'R', -1.0)]
        [DataRow('D', 'K', 0.0)]
        [DataRow('D', 'M', -3.0)]
        [DataRow('D', 'I', -2.0)]
        [DataRow('D', 'L', -4.0)]
        [DataRow('D', 'V', -2.0)]
        [DataRow('D', 'W', -7.0)]
        [DataRow('D', 'Y', -4.0)]
        [DataRow('D', 'F', -6.0)]
        [DataRow('E', 'E', 4.0)]
        [DataRow('E', 'Q', 2.0)]
        [DataRow('E', 'N', 1.0)]
        [DataRow('E', 'H', 1.0)]
        [DataRow('E', 'R', -1.0)]
        [DataRow('E', 'K', 0.0)]
        [DataRow('E', 'M', -2.0)]
        [DataRow('E', 'I', -2.0)]
        [DataRow('E', 'L', -3.0)]
        [DataRow('E', 'V', -2.0)]
        [DataRow('E', 'W', -7.0)]
        [DataRow('E', 'Y', -4.0)]
        [DataRow('E', 'F', -5.0)]
        [DataRow('Q', 'Q', 4.0)]
        [DataRow('Q', 'N', 1.0)]
        [DataRow('Q', 'H', 3.0)]
        [DataRow('Q', 'R', 1.0)]
        [DataRow('Q', 'K', 1.0)]
        [DataRow('Q', 'M', -1.0)]
        [DataRow('Q', 'I', -2.0)]
        [DataRow('Q', 'L', -2.0)]
        [DataRow('Q', 'V', -2.0)]
        [DataRow('Q', 'W', -5.0)]
        [DataRow('Q', 'Y', -4.0)]
        [DataRow('Q', 'F', -5.0)]
        [DataRow('N', 'N', 2.0)]
        [DataRow('N', 'H', 2.0)]
        [DataRow('N', 'R', 0.0)]
        [DataRow('N', 'K', 1.0)]
        [DataRow('N', 'M', -2.0)]
        [DataRow('N', 'I', -2.0)]
        [DataRow('N', 'L', -3.0)]
        [DataRow('N', 'V', -2.0)]
        [DataRow('N', 'W', -4.0)]
        [DataRow('N', 'Y', -2.0)]
        [DataRow('N', 'F', -3.0)]
        [DataRow('H', 'H', 6.0)]
        [DataRow('H', 'R', 2.0)]
        [DataRow('H', 'K', 0.0)]
        [DataRow('H', 'M', -2.0)]
        [DataRow('H', 'I', -2.0)]
        [DataRow('H', 'L', -2.0)]
        [DataRow('H', 'V', -2.0)]
        [DataRow('H', 'W', -3.0)]
        [DataRow('H', 'Y', 0.0)]
        [DataRow('H', 'F', -2.0)]
        [DataRow('R', 'R', 6.0)]
        [DataRow('R', 'K', 3.0)]
        [DataRow('R', 'M', 0.0)]
        [DataRow('R', 'I', -2.0)]
        [DataRow('R', 'L', -3.0)]
        [DataRow('R', 'V', -2.0)]
        [DataRow('R', 'W', 2.0)]
        [DataRow('R', 'Y', -4.0)]
        [DataRow('R', 'F', -4.0)]
        [DataRow('K', 'K', 5.0)]
        [DataRow('K', 'M', 0.0)]
        [DataRow('K', 'I', -2.0)]
        [DataRow('K', 'L', -3.0)]
        [DataRow('K', 'V', -2.0)]
        [DataRow('K', 'W', -3.0)]
        [DataRow('K', 'Y', -4.0)]
        [DataRow('K', 'F', -5.0)]
        [DataRow('M', 'M', 6.0)]
        [DataRow('M', 'I', 2.0)]
        [DataRow('M', 'L', 4.0)]
        [DataRow('M', 'V', 2.0)]
        [DataRow('M', 'W', -4.0)]
        [DataRow('M', 'Y', -2.0)]
        [DataRow('M', 'F', 0.0)]
        [DataRow('I', 'I', 5.0)]
        [DataRow('I', 'L', 2.0)]
        [DataRow('I', 'V', 4.0)]
        [DataRow('I', 'W', -5.0)]
        [DataRow('I', 'Y', -1.0)]
        [DataRow('I', 'F', 1.0)]
        [DataRow('L', 'L', 6.0)]
        [DataRow('L', 'V', 2.0)]
        [DataRow('L', 'W', -2.0)]
        [DataRow('L', 'Y', -1.0)]
        [DataRow('L', 'F', 2.0)]
        [DataRow('V', 'V', 4.0)]
        [DataRow('V', 'W', -6.0)]
        [DataRow('V', 'Y', -2.0)]
        [DataRow('V', 'F', -1.0)]
        [DataRow('W', 'W', 17.0)]
        [DataRow('W', 'Y', 0.0)]
        [DataRow('W', 'F', 0.0)]
        [DataRow('Y', 'Y', 10.0)]
        [DataRow('Y', 'F', 7.0)]
        [DataRow('F', 'F', 9.0)]

        public void MatrixAgreesWithBiopythonScores(char a, char b, double expected)
        {
            double actual = Matrix.ScorePair(a, b);
            Assert.AreEqual(expected, actual, 0.01);
        }



        #endregion
    }
}
