using LibScoring.FitnessFunctions;
using LibBioInfo.ScoringMatrices;

using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibBioInfo;

namespace TestsUnitSuite.LibScoring.FitnessFunctions
{
    [TestClass]
    public class SumOfPairsWithAffineGapPenaltiesFitnessFunctionTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        NormalisedFitnessFunction FitnessFunction = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(new BLOSUM62Matrix());

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
