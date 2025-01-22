using LibBioInfo;
using LibScoring.FitnessFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibScoring;
using LibScoring.ScoringMatrices;

namespace TestsUnitSuite.LibScoring.FitnessFunctions
{
    [TestClass]
    public class SumOfPairsFitnessFunctionTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        NormalisedFitnessFunction FitnessFunction = new SumOfPairsFitnessFunction(new BLOSUM62Matrix());

        [TestMethod]
        public void FitnessScoreIsNormalized()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            double score = FitnessFunction.GetFitness(alignment.GetCharacterMatrix());
            Assert.IsTrue(0 <= score && score <= 1.0);
        }

        [TestMethod]
        public void BestScoreIsGreaterThanWorstScore()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            double best = FitnessFunction.GetBestPossibleScore(alignment.GetCharacterMatrix());
            double worst = FitnessFunction.GetWorstPossibleScore(alignment.GetCharacterMatrix());
            Assert.IsTrue(best > worst);
        }

        [TestMethod]
        public void RawScoreIsBetweenExtremes()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            double score = FitnessFunction.ScoreAlignment(alignment.GetCharacterMatrix());
            double best = FitnessFunction.GetBestPossibleScore(alignment.GetCharacterMatrix());
            double worst = FitnessFunction.GetWorstPossibleScore(alignment.GetCharacterMatrix());
            Assert.IsTrue(worst <= score && score <= best);
        }
    }
}
