using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibScoring.ObjectiveFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibScoring.ObjectiveFunctions
{
    [TestClass]
    public class AffineGapPenaltyObjectiveTests
    {
        AffineGapPenaltyObjectiveFunction ObjectiveFunction = new AffineGapPenaltyObjectiveFunction();
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;


        [TestMethod]
        public void LeftJustifiedAlignmentHasNoPenalty()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            double penalty = ObjectiveFunction.ScoreAlignment(alignment);
            Assert.AreEqual(0, penalty, 0.001);
        }

        [TestMethod]
        public void RandomizedAlignmentStateHasPenalty()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            AlignmentRandomizer randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(alignment);

            double penalty = ObjectiveFunction.ScoreAlignment(alignment);
            Assert.IsTrue(penalty > 0);
        }

        [DataTestMethod]
        [DataRow(4, 1, "----ACGT----", 0)]
        [DataRow(0, 1, "-AC--GT-", 2)]
        [DataRow(5, 1, "-AC--GG-", 7)]
        [DataRow(4, 1, "-A-G---C-T-", 17)]
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
