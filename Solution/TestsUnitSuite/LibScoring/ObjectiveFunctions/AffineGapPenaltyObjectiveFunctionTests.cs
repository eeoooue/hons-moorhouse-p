using LibScoring.ObjectiveFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibScoring.ObjectiveFunctions
{
    [TestClass]
    public class AffineGapPenaltyObjectiveFunctionTests
    {
        AffineGapPenaltyObjectiveFunction ObjectiveFunction = new AffineGapPenaltyObjectiveFunction();

        [DataTestMethod]
        [DataRow(4, 1, "----AAAA----", 0)]
        public void PayloadPenalitiesAreAsExpected(double openingCost, double nullCost, string payload, double expected)
        {
            AffineGapPenaltyObjectiveFunction objective = new AffineGapPenaltyObjectiveFunction(openingCost, nullCost);
            double actual = objective.ScorePayload(payload);
            Assert.AreEqual(expected, actual, 0.001);
        }





        [DataTestMethod]
        [DataRow("-A-", "A")]
        [DataRow("-G", "G")]
        [DataRow("C-", "C")]
        [DataRow("---T", "T")]
        [DataRow("T---", "T")]
        [DataRow("-TACG-AG---", "TACG-AG")]

        public void CanTrimPayload(string payload, string expected)
        {
            string actual = ObjectiveFunction.TrimPayload(payload);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CanCollectGapSizes()
        {
            string payload = "A---A--A---A----A-A";
            List<int> expected = new List<int> { 3, 2, 3, 4, 1 };
            List<int> actual = ObjectiveFunction.CollectGapSizes(payload);

            Assert.AreEqual(expected.Count, actual.Count);

            for(int i=0; i<expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
