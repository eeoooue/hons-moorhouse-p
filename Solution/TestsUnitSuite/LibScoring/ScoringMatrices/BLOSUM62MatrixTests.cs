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

        public void RowCIsCorrect(char pair, int expected)
        {
            int scoreAB = Matrix.ScorePair('C', pair);
            int scoreBA = Matrix.ScorePair(pair, 'C');
            Assert.AreEqual(expected, scoreAB);
            Assert.AreEqual(expected, scoreBA);
        }
    }
}
