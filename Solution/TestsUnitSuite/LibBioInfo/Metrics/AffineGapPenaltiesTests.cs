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
            int countedNulls = 4 + 1 + 0;

            double score = Metric.ScorePayload(payload);
            double totalOpeningPenalty = numGaps * Metric.OpeningCost;
            double totalNullPenalty = countedNulls * Metric.NullCost;

            Assert.AreEqual(score, totalOpeningPenalty + totalNullPenalty, 0.0001);
        }
    }
}
