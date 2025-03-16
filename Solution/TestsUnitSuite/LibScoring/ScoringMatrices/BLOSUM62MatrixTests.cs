using LibBioInfo.ScoringMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibScoring.ScoringMatrices
{
    [TestClass]
    public class BLOSUM62MatrixTests
    {
        BLOSUM62Matrix Matrix = new BLOSUM62Matrix();

        [DataTestMethod]
        [DataRow('C', 9)]

        [DataRow('S', 4)]
        [DataRow('T', 5)]
        [DataRow('A', 4)]
        [DataRow('G', 6)]
        [DataRow('P', 7)]

        [DataRow('D', 6)]
        [DataRow('E', 5)]
        [DataRow('Q', 5)]
        [DataRow('N', 6)]

        [DataRow('H', 8)]
        [DataRow('R', 5)]
        [DataRow('K', 5)]

        [DataRow('M', 5)]
        [DataRow('I', 4)]
        [DataRow('L', 4)]
        [DataRow('V', 4)]

        [DataRow('W', 11)]
        [DataRow('Y', 7)]
        [DataRow('F', 6)]

        public void MatchingPairScoresAreCorrect(char a, int expected)
        {
            int score = Matrix.ScorePair(a, a);
            Assert.AreEqual(expected, score);
        }


        [DataTestMethod]
        [DataRow('A', 'K', -1)]
        [DataRow('P', 'T', -1)]
        [DataRow('S', 'R', -1)]
        [DataRow('G', 'Y', -3)]

        public void RandomSelectionOfPairsAreCorrectlyScored(char a, char b, int expected)
        {
            int scoreAB = Matrix.ScorePair(a, b);
            int scoreBA = Matrix.ScorePair(b, a);
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('C', 9)]

        [DataRow('S', -1)]
        [DataRow('T', -1)]
        [DataRow('A', 0)]
        [DataRow('G', -3)]
        [DataRow('P', -3)]

        [DataRow('D', -3)]
        [DataRow('E', -4)]
        [DataRow('Q', -3)]
        [DataRow('N', -3)]

        [DataRow('H', -3)]
        [DataRow('R', -3)]
        [DataRow('K', -3)]

        [DataRow('M', -1)]
        [DataRow('I', -1)]
        [DataRow('L', -1)]
        [DataRow('V', -1)]

        [DataRow('W', -2)]
        [DataRow('Y', -2)]
        [DataRow('F', -2)]

        public void ColumnCIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('C', pair);
            int scoreBA = Matrix.ScorePair(pair, 'C');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('S', 4)]
        [DataRow('T', 1)]
        [DataRow('A', 1)]
        [DataRow('G', 0)]
        [DataRow('P', -1)]

        [DataRow('D', 0)]
        [DataRow('E', 0)]
        [DataRow('Q', 0)]
        [DataRow('N', 1)]

        [DataRow('H', -1)]
        [DataRow('R', -1)]
        [DataRow('K', 0)]

        [DataRow('M', -1)]
        [DataRow('I', -2)]
        [DataRow('L', -2)]
        [DataRow('V', -2)]

        [DataRow('W', -3)]
        [DataRow('Y', -2)]
        [DataRow('F', -2)]

        public void ColumnSIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('S', pair);
            int scoreBA = Matrix.ScorePair(pair, 'S');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('T', 5)]
        [DataRow('A', 0)]
        [DataRow('G', -2)]
        [DataRow('P', -1)]

        [DataRow('D', -1)]
        [DataRow('E', -1)]
        [DataRow('Q', -1)]
        [DataRow('N', 0)]

        [DataRow('H', -2)]
        [DataRow('R', -1)]
        [DataRow('K', -1)]

        [DataRow('M', -1)]
        [DataRow('I', -1)]
        [DataRow('L', -1)]
        [DataRow('V', 0)]

        [DataRow('W', -2)]
        [DataRow('Y', -2)]
        [DataRow('F', -2)]
        public void ColumnTIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('T', pair);
            int scoreBA = Matrix.ScorePair(pair, 'T');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('A', 4)]
        [DataRow('G', 0)]
        [DataRow('P', -1)]

        [DataRow('D', -2)]
        [DataRow('E', -1)]
        [DataRow('Q', -1)]
        [DataRow('N', -2)]

        [DataRow('H', -2)]
        [DataRow('R', -1)]
        [DataRow('K', -1)]

        [DataRow('M', -1)]
        [DataRow('I', -1)]
        [DataRow('L', -1)]
        [DataRow('V', 0)]

        [DataRow('W', -3)]
        [DataRow('Y', -2)]
        [DataRow('F', -2)]
        public void ColumnAIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('A', pair);
            int scoreBA = Matrix.ScorePair(pair, 'A');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('G', 6)]
        [DataRow('P', -2)]

        [DataRow('D', -1)]
        [DataRow('E', -2)]
        [DataRow('Q', -2)]
        [DataRow('N', 0)]

        [DataRow('H', -2)]
        [DataRow('R', -2)]
        [DataRow('K', -2)]

        [DataRow('M', -3)]
        [DataRow('I', -4)]
        [DataRow('L', -4)]
        [DataRow('V', -3)]

        [DataRow('W', -2)]
        [DataRow('Y', -3)]
        [DataRow('F', -3)]
        public void ColumnGIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('G', pair);
            int scoreBA = Matrix.ScorePair(pair, 'G');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('P', 7)]

        [DataRow('D', -1)]
        [DataRow('E', -1)]
        [DataRow('Q', -1)]
        [DataRow('N', -2)]

        [DataRow('H', -2)]
        [DataRow('R', -2)]
        [DataRow('K', -1)]

        [DataRow('M', -2)]
        [DataRow('I', -3)]
        [DataRow('L', -3)]
        [DataRow('V', -2)]

        [DataRow('W', -4)]
        [DataRow('Y', -3)]
        [DataRow('F', -4)]
        public void ColumnPIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('P', pair);
            int scoreBA = Matrix.ScorePair(pair, 'P');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('D', 6)]
        [DataRow('E', 2)]
        [DataRow('Q', 0)]
        [DataRow('N', 1)]

        [DataRow('H', -1)]
        [DataRow('R', -2)]
        [DataRow('K', -1)]

        [DataRow('M', -3)]
        [DataRow('I', -3)]
        [DataRow('L', -4)]
        [DataRow('V', -3)]

        [DataRow('W', -4)]
        [DataRow('Y', -3)]
        [DataRow('F', -3)]
        public void ColumnDIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('D', pair);
            int scoreBA = Matrix.ScorePair(pair, 'D');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('E', 5)]
        [DataRow('Q', 2)]
        [DataRow('N', 0)]

        [DataRow('H', 0)]
        [DataRow('R', 0)]
        [DataRow('K', 1)]

        [DataRow('M', -2)]
        [DataRow('I', -3)]
        [DataRow('L', -3)]
        [DataRow('V', -2)]

        [DataRow('W', -3)]
        [DataRow('Y', -2)]
        [DataRow('F', -3)]
        public void ColumnEIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('E', pair);
            int scoreBA = Matrix.ScorePair(pair, 'E');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('Q', 5)]
        [DataRow('N', 0)]

        [DataRow('H', 0)]
        [DataRow('R', 1)]
        [DataRow('K', 1)]

        [DataRow('M', 0)]
        [DataRow('I', -3)]
        [DataRow('L', -2)]
        [DataRow('V', -2)]

        [DataRow('W', -2)]
        [DataRow('Y', -1)]
        [DataRow('F', -3)]
        public void ColumnQIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('Q', pair);
            int scoreBA = Matrix.ScorePair(pair, 'Q');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('N', 6)]

        [DataRow('H', 1)]
        [DataRow('R', 0)]
        [DataRow('K', 0)]

        [DataRow('M', -2)]
        [DataRow('I', -3)]
        [DataRow('L', -3)]
        [DataRow('V', -3)]

        [DataRow('W', -4)]
        [DataRow('Y', -2)]
        [DataRow('F', -3)]
        public void ColumnNIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('N', pair);
            int scoreBA = Matrix.ScorePair(pair, 'N');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('H', 8)]
        [DataRow('R', 0)]
        [DataRow('K', -1)]

        [DataRow('M', -2)]
        [DataRow('I', -3)]
        [DataRow('L', -3)]
        [DataRow('V', -3)]

        [DataRow('W', -2)]
        [DataRow('Y', 2)]
        [DataRow('F', -1)]
        public void ColumnHIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('H', pair);
            int scoreBA = Matrix.ScorePair(pair, 'H');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('R', 5)]
        [DataRow('K', 2)]

        [DataRow('M', -1)]
        [DataRow('I', -3)]
        [DataRow('L', -2)]
        [DataRow('V', -3)]

        [DataRow('W', -3)]
        [DataRow('Y', -2)]
        [DataRow('F', -3)]
        public void ColumnRIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('R', pair);
            int scoreBA = Matrix.ScorePair(pair, 'R');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('K', 5)]

        [DataRow('M', -1)]
        [DataRow('I', -3)]
        [DataRow('L', -2)]
        [DataRow('V', -2)]

        [DataRow('W', -3)]
        [DataRow('Y', -2)]
        [DataRow('F', -3)]
        public void ColumnKIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('K', pair);
            int scoreBA = Matrix.ScorePair(pair, 'K');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('M', 5)]
        [DataRow('I', 1)]
        [DataRow('L', 2)]
        [DataRow('V', 1)]

        [DataRow('W', -1)]
        [DataRow('Y', -1)]
        [DataRow('F', 0)]
        public void ColumnMIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('M', pair);
            int scoreBA = Matrix.ScorePair(pair, 'M');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('I', 4)]
        [DataRow('L', 2)]
        [DataRow('V', 3)]

        [DataRow('W', -3)]
        [DataRow('Y', -1)]
        [DataRow('F', 0)]
        public void ColumnIIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('I', pair);
            int scoreBA = Matrix.ScorePair(pair, 'I');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('L', 4)]
        [DataRow('V', 1)]

        [DataRow('W', -2)]
        [DataRow('Y', -1)]
        [DataRow('F', 0)]
        public void ColumnLIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('L', pair);
            int scoreBA = Matrix.ScorePair(pair, 'L');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        [DataTestMethod]
        [DataRow('V', 4)]

        [DataRow('W', -3)]
        [DataRow('Y', -1)]
        [DataRow('F', -1)]
        public void ColumnVIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('V', pair);
            int scoreBA = Matrix.ScorePair(pair, 'V');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataTestMethod]
        [DataRow('W', 11)]
        [DataRow('Y', 2)]
        [DataRow('F', 1)]
        public void ColumnWIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('W', pair);
            int scoreBA = Matrix.ScorePair(pair, 'W');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataRow('Y', 7)]
        [DataRow('F', 3)]
        public void ColumnYIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('Y', pair);
            int scoreBA = Matrix.ScorePair(pair, 'Y');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }

        [DataRow('F', 6)]
        public void ColumnFIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('F', pair);
            int scoreBA = Matrix.ScorePair(pair, 'F');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }


        #region Tests Generated Using BioPython

        [DataTestMethod]
        [DataRow('C', 'C', 9.0)]
        [DataRow('C', 'S', -1.0)]
        [DataRow('C', 'T', -1.0)]
        [DataRow('C', 'A', 0.0)]
        [DataRow('C', 'G', -3.0)]
        [DataRow('C', 'P', -3.0)]
        [DataRow('C', 'D', -3.0)]
        [DataRow('C', 'E', -4.0)]
        [DataRow('C', 'Q', -3.0)]
        [DataRow('C', 'N', -3.0)]
        [DataRow('C', 'H', -3.0)]
        [DataRow('C', 'R', -3.0)]
        [DataRow('C', 'K', -3.0)]
        [DataRow('C', 'M', -1.0)]
        [DataRow('C', 'I', -1.0)]
        [DataRow('C', 'L', -1.0)]
        [DataRow('C', 'V', -1.0)]
        [DataRow('C', 'W', -2.0)]
        [DataRow('C', 'Y', -2.0)]
        [DataRow('C', 'F', -2.0)]
        [DataRow('S', 'S', 4.0)]
        [DataRow('S', 'T', 1.0)]
        [DataRow('S', 'A', 1.0)]
        [DataRow('S', 'G', 0.0)]
        [DataRow('S', 'P', -1.0)]
        [DataRow('S', 'D', 0.0)]
        [DataRow('S', 'E', 0.0)]
        [DataRow('S', 'Q', 0.0)]
        [DataRow('S', 'N', 1.0)]
        [DataRow('S', 'H', -1.0)]
        [DataRow('S', 'R', -1.0)]
        [DataRow('S', 'K', 0.0)]
        [DataRow('S', 'M', -1.0)]
        [DataRow('S', 'I', -2.0)]
        [DataRow('S', 'L', -2.0)]
        [DataRow('S', 'V', -2.0)]
        [DataRow('S', 'W', -3.0)]
        [DataRow('S', 'Y', -2.0)]
        [DataRow('S', 'F', -2.0)]
        [DataRow('T', 'T', 5.0)]
        [DataRow('T', 'A', 0.0)]
        [DataRow('T', 'G', -2.0)]
        [DataRow('T', 'P', -1.0)]
        [DataRow('T', 'D', -1.0)]
        [DataRow('T', 'E', -1.0)]
        [DataRow('T', 'Q', -1.0)]
        [DataRow('T', 'N', 0.0)]
        [DataRow('T', 'H', -2.0)]
        [DataRow('T', 'R', -1.0)]
        [DataRow('T', 'K', -1.0)]
        [DataRow('T', 'M', -1.0)]
        [DataRow('T', 'I', -1.0)]
        [DataRow('T', 'L', -1.0)]
        [DataRow('T', 'V', 0.0)]
        [DataRow('T', 'W', -2.0)]
        [DataRow('T', 'Y', -2.0)]
        [DataRow('T', 'F', -2.0)]
        [DataRow('A', 'A', 4.0)]
        [DataRow('A', 'G', 0.0)]
        [DataRow('A', 'P', -1.0)]
        [DataRow('A', 'D', -2.0)]
        [DataRow('A', 'E', -1.0)]
        [DataRow('A', 'Q', -1.0)]
        [DataRow('A', 'N', -2.0)]
        [DataRow('A', 'H', -2.0)]
        [DataRow('A', 'R', -1.0)]
        [DataRow('A', 'K', -1.0)]
        [DataRow('A', 'M', -1.0)]
        [DataRow('A', 'I', -1.0)]
        [DataRow('A', 'L', -1.0)]
        [DataRow('A', 'V', 0.0)]
        [DataRow('A', 'W', -3.0)]
        [DataRow('A', 'Y', -2.0)]
        [DataRow('A', 'F', -2.0)]
        [DataRow('G', 'G', 6.0)]
        [DataRow('G', 'P', -2.0)]
        [DataRow('G', 'D', -1.0)]
        [DataRow('G', 'E', -2.0)]
        [DataRow('G', 'Q', -2.0)]
        [DataRow('G', 'N', 0.0)]
        [DataRow('G', 'H', -2.0)]
        [DataRow('G', 'R', -2.0)]
        [DataRow('G', 'K', -2.0)]
        [DataRow('G', 'M', -3.0)]
        [DataRow('G', 'I', -4.0)]
        [DataRow('G', 'L', -4.0)]
        [DataRow('G', 'V', -3.0)]
        [DataRow('G', 'W', -2.0)]
        [DataRow('G', 'Y', -3.0)]
        [DataRow('G', 'F', -3.0)]
        [DataRow('P', 'P', 7.0)]
        [DataRow('P', 'D', -1.0)]
        [DataRow('P', 'E', -1.0)]
        [DataRow('P', 'Q', -1.0)]
        [DataRow('P', 'N', -2.0)]
        [DataRow('P', 'H', -2.0)]
        [DataRow('P', 'R', -2.0)]
        [DataRow('P', 'K', -1.0)]
        [DataRow('P', 'M', -2.0)]
        [DataRow('P', 'I', -3.0)]
        [DataRow('P', 'L', -3.0)]
        [DataRow('P', 'V', -2.0)]
        [DataRow('P', 'W', -4.0)]
        [DataRow('P', 'Y', -3.0)]
        [DataRow('P', 'F', -4.0)]
        [DataRow('D', 'D', 6.0)]
        [DataRow('D', 'E', 2.0)]
        [DataRow('D', 'Q', 0.0)]
        [DataRow('D', 'N', 1.0)]
        [DataRow('D', 'H', -1.0)]
        [DataRow('D', 'R', -2.0)]
        [DataRow('D', 'K', -1.0)]
        [DataRow('D', 'M', -3.0)]
        [DataRow('D', 'I', -3.0)]
        [DataRow('D', 'L', -4.0)]
        [DataRow('D', 'V', -3.0)]
        [DataRow('D', 'W', -4.0)]
        [DataRow('D', 'Y', -3.0)]
        [DataRow('D', 'F', -3.0)]
        [DataRow('E', 'E', 5.0)]
        [DataRow('E', 'Q', 2.0)]
        [DataRow('E', 'N', 0.0)]
        [DataRow('E', 'H', 0.0)]
        [DataRow('E', 'R', 0.0)]
        [DataRow('E', 'K', 1.0)]
        [DataRow('E', 'M', -2.0)]
        [DataRow('E', 'I', -3.0)]
        [DataRow('E', 'L', -3.0)]
        [DataRow('E', 'V', -2.0)]
        [DataRow('E', 'W', -3.0)]
        [DataRow('E', 'Y', -2.0)]
        [DataRow('E', 'F', -3.0)]
        [DataRow('Q', 'Q', 5.0)]
        [DataRow('Q', 'N', 0.0)]
        [DataRow('Q', 'H', 0.0)]
        [DataRow('Q', 'R', 1.0)]
        [DataRow('Q', 'K', 1.0)]
        [DataRow('Q', 'M', 0.0)]
        [DataRow('Q', 'I', -3.0)]
        [DataRow('Q', 'L', -2.0)]
        [DataRow('Q', 'V', -2.0)]
        [DataRow('Q', 'W', -2.0)]
        [DataRow('Q', 'Y', -1.0)]
        [DataRow('Q', 'F', -3.0)]
        [DataRow('N', 'N', 6.0)]
        [DataRow('N', 'H', 1.0)]
        [DataRow('N', 'R', 0.0)]
        [DataRow('N', 'K', 0.0)]
        [DataRow('N', 'M', -2.0)]
        [DataRow('N', 'I', -3.0)]
        [DataRow('N', 'L', -3.0)]
        [DataRow('N', 'V', -3.0)]
        [DataRow('N', 'W', -4.0)]
        [DataRow('N', 'Y', -2.0)]
        [DataRow('N', 'F', -3.0)]
        [DataRow('H', 'H', 8.0)]
        [DataRow('H', 'R', 0.0)]
        [DataRow('H', 'K', -1.0)]
        [DataRow('H', 'M', -2.0)]
        [DataRow('H', 'I', -3.0)]
        [DataRow('H', 'L', -3.0)]
        [DataRow('H', 'V', -3.0)]
        [DataRow('H', 'W', -2.0)]
        [DataRow('H', 'Y', 2.0)]
        [DataRow('H', 'F', -1.0)]
        [DataRow('R', 'R', 5.0)]
        [DataRow('R', 'K', 2.0)]
        [DataRow('R', 'M', -1.0)]
        [DataRow('R', 'I', -3.0)]
        [DataRow('R', 'L', -2.0)]
        [DataRow('R', 'V', -3.0)]
        [DataRow('R', 'W', -3.0)]
        [DataRow('R', 'Y', -2.0)]
        [DataRow('R', 'F', -3.0)]
        [DataRow('K', 'K', 5.0)]
        [DataRow('K', 'M', -1.0)]
        [DataRow('K', 'I', -3.0)]
        [DataRow('K', 'L', -2.0)]
        [DataRow('K', 'V', -2.0)]
        [DataRow('K', 'W', -3.0)]
        [DataRow('K', 'Y', -2.0)]
        [DataRow('K', 'F', -3.0)]
        [DataRow('M', 'M', 5.0)]
        [DataRow('M', 'I', 1.0)]
        [DataRow('M', 'L', 2.0)]
        [DataRow('M', 'V', 1.0)]
        [DataRow('M', 'W', -1.0)]
        [DataRow('M', 'Y', -1.0)]
        [DataRow('M', 'F', 0.0)]
        [DataRow('I', 'I', 4.0)]
        [DataRow('I', 'L', 2.0)]
        [DataRow('I', 'V', 3.0)]
        [DataRow('I', 'W', -3.0)]
        [DataRow('I', 'Y', -1.0)]
        [DataRow('I', 'F', 0.0)]
        [DataRow('L', 'L', 4.0)]
        [DataRow('L', 'V', 1.0)]
        [DataRow('L', 'W', -2.0)]
        [DataRow('L', 'Y', -1.0)]
        [DataRow('L', 'F', 0.0)]
        [DataRow('V', 'V', 4.0)]
        [DataRow('V', 'W', -3.0)]
        [DataRow('V', 'Y', -1.0)]
        [DataRow('V', 'F', -1.0)]
        [DataRow('W', 'W', 11.0)]
        [DataRow('W', 'Y', 2.0)]
        [DataRow('W', 'F', 1.0)]
        [DataRow('Y', 'Y', 7.0)]
        [DataRow('Y', 'F', 3.0)]
        [DataRow('F', 'F', 6.0)]

        public void MatrixAgreesWithBiopythonScores(char a, char b, double expected)
        {
            double actual = Matrix.ScorePair(a, b);
            Assert.AreEqual(expected, actual, 0.01);
        }


        [DataTestMethod]
        [DataRow('S', 'X', 0.0)]
        [DataRow('T', 'X', 0.0)]
        [DataRow('A', 'X', 0.0)]

        [DataRow('G', 'X', -1.0)]
        [DataRow('D', 'X', -1.0)]
        [DataRow('E', 'X', -1.0)]
        [DataRow('Q', 'X', -1.0)]
        [DataRow('N', 'X', -1.0)]
        [DataRow('H', 'X', -1.0)]
        [DataRow('R', 'X', -1.0)]
        [DataRow('K', 'X', -1.0)]
        [DataRow('M', 'X', -1.0)]
        [DataRow('I', 'X', -1.0)]
        [DataRow('L', 'X', -1.0)]
        [DataRow('V', 'X', -1.0)]
        [DataRow('Y', 'X', -1.0)]
        [DataRow('F', 'X', -1.0)]

        [DataRow('W', 'X', -2.0)]
        [DataRow('C', 'X', -2.0)]
        [DataRow('P', 'X', -2.0)]

        public void MatrixAgreesWithBiopythonScoresForXResidues(char a, char b, double expected)
        {
            double actual = Matrix.ScorePair(a, b);
            Assert.AreEqual(expected, actual, 0.01);
        }



        [DataTestMethod]

        [DataRow('D', 'B', 4.0)]

        [DataRow('N', 'B', 3.0)]

        [DataRow('E', 'B', 1.0)]

        [DataRow('Q', 'B', 0.0)]
        [DataRow('S', 'B', 0.0)]
        [DataRow('H', 'B', 0.0)]
        [DataRow('K', 'B', 0.0)]

        [DataRow('G', 'B', -1.0)]
        [DataRow('T', 'B', -1.0)]
        [DataRow('R', 'B', -1.0)]
        [DataRow('P', 'B', -2.0)]
        [DataRow('A', 'B', -2.0)]

        [DataRow('C', 'B', -3.0)]
        [DataRow('M', 'B', -3.0)]
        [DataRow('I', 'B', -3.0)]
        [DataRow('V', 'B', -3.0)]
        [DataRow('Y', 'B', -3.0)]
        [DataRow('F', 'B', -3.0)]

        [DataRow('L', 'B', -4.0)]
        [DataRow('W', 'B', -4.0)]

        public void MatrixAgreesWithBiopythonScoresForBResidues(char a, char b, double expected)
        {
            double actual = Matrix.ScorePair(a, b);
            Assert.AreEqual(expected, actual, 0.01);
        }


        [DataTestMethod]
        [DataRow('C', 'Z', -3.0)]
        [DataRow('S', 'Z', 0.0)]
        [DataRow('T', 'Z', -1.0)]
        [DataRow('A', 'Z', -1.0)]
        [DataRow('G', 'Z', -2.0)]
        [DataRow('P', 'Z', -1.0)]
        [DataRow('D', 'Z', 1.0)]
        [DataRow('E', 'Z', 4.0)]
        [DataRow('Q', 'Z', 3.0)]
        [DataRow('N', 'Z', 0.0)]
        [DataRow('H', 'Z', 0.0)]
        [DataRow('R', 'Z', 0.0)]
        [DataRow('K', 'Z', 1.0)]
        [DataRow('M', 'Z', -1.0)]
        [DataRow('I', 'Z', -3.0)]
        [DataRow('L', 'Z', -3.0)]
        [DataRow('V', 'Z', -2.0)]
        [DataRow('W', 'Z', -3.0)]
        [DataRow('Y', 'Z', -2.0)]
        [DataRow('F', 'Z', -3.0)]

        public void MatrixAgreesWithBiopythonScoresForZResidues(char a, char b, double expected)
        {
            double actual = Matrix.ScorePair(a, b);
            Assert.AreEqual(expected, actual, 0.01);
        }

        #endregion
    }
}
