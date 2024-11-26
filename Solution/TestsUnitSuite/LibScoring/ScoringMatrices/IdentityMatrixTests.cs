using LibScoring.ScoringMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibScoring.ScoringMatrices
{
    [TestClass]
    public class IdentityMatrixTests
    {
        IdentityMatrix Matrix = new IdentityMatrix();

        [DataTestMethod]
        [DataRow('A', 'A', 1)]
        [DataRow('A', 'B', 0)]
        [DataRow('B', 'B', 1)]

        public void PairScoresMatchExpectations(char a, char b, int expected)
        {
            int score = Matrix.ScorePair(a, b);
            Assert.AreEqual(expected, score);
        }
    }
}
