using LibBioInfo;
using LibScoring.FitnessFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibScoring.FitnessFunctions
{
    [TestClass]
    public class TotallyConservedColumnsFitnessFunctionTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        TotallyConservedColumnsFitnessFunction FitnessFunction = new TotallyConservedColumnsFitnessFunction();

        [TestMethod]
        public void FitnessScoreIsNormalized()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            double score = FitnessFunction.GetFitness(alignment.CharacterMatrix);
            Assert.IsTrue(0 <= score && score <= 1.0);
        }

        [TestMethod]
        public void BestScoreIsGreaterThanWorstScore()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            double best = FitnessFunction.GetBestPossibleScore(alignment.CharacterMatrix);
            double worst = FitnessFunction.GetWorstPossibleScore(alignment.CharacterMatrix);
            Assert.IsTrue(best > worst);
        }

        [TestMethod]
        public void RawScoreIsBetweenExtremes()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            double score = FitnessFunction.ScoreAlignment(alignment.CharacterMatrix);
            double best = FitnessFunction.GetBestPossibleScore(alignment.CharacterMatrix);
            double worst = FitnessFunction.GetWorstPossibleScore(alignment.CharacterMatrix);
            Assert.IsTrue(worst <= score && score <= best);
        }
    }
}
