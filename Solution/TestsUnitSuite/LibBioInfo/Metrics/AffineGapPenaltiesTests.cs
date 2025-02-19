using LibBioInfo.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibBioInfo.Metrics
{
    [TestClass]
    public class AffineGapPenaltiesTests
    {
        AffineGapPenalties Metric = new AffineGapPenalties();

        [TestMethod]
        public void CanCorrectlyCalculatePenalty()
        {
            string payload = "--A-----A--A-A-";
            int numGaps = 3;
            int totalNulls = 8;

            double score = Metric.ScorePayload(payload);
            double totalOpeningPenalty = numGaps * Metric.OpeningCost;
            double totalNullPenalty = totalNulls * Metric.NullCost;

            Assert.AreEqual(score, totalOpeningPenalty + totalNullPenalty, 0.0001);
        }

        [TestMethod]
        public void CanCorrectlyExtractGapSizes()
        {
            string payload = "--A-----A--A-A-";

            List<int> expected = new List<int>() { 5, 2, 1 };
            List<int> actual = Metric.CollectGapSizes(payload);

            Assert.AreEqual(actual.Count, expected.Count);
            for(int i=0; i<actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
