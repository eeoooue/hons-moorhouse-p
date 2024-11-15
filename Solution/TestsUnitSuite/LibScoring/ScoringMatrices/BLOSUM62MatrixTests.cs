using LibScoring.ScoringMatrices;
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



    }
}
