using LibBioInfo;
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
    }
}
