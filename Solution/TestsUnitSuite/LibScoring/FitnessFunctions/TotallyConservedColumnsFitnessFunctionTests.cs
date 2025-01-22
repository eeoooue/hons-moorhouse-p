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


        [TestMethod]
        public void HomogenousAlignmentScores100Percent()
        {
            List<BioSequence> sequences = new List<BioSequence>()
            {
                new BioSequence("a", "ACGT"),
                new BioSequence("b", "ACGT"),
                new BioSequence("c", "ACGT"),
            };

            bool[,] state = new bool[3, 4]
            {
                { false,false,false,false },
                { false,false,false,false },
                { false,false,false,false },
            };

            Alignment alignment = new Alignment(sequences);
            alignment.SetState(state);

            double expected = 1.0;
            double actual = FitnessFunction.ScoreAlignment(alignment.GetCharacterMatrix());

            Assert.AreEqual(expected, actual, 0.01);
        }

        [TestMethod]
        public void PartialHomogeneityScoresAsExpected()
        {
            List<BioSequence> sequences = new List<BioSequence>()
            {
                new BioSequence("a", "ACGT"),
                new BioSequence("b", "ACGT"),
                new BioSequence("c", "ACGT"),
                new BioSequence("d", "ACGT"),
            };

            bool[,] state = new bool[4, 5]
            {
                { false,false,false,true,false },
                { false,false,false,false,true },
                { false,false,false,false,true },
                { false,false,false,false,true },
            };

            Alignment alignment = new Alignment(sequences);
            alignment.SetState(state);

            double expected = 0.6;
            double actual = FitnessFunction.ScoreAlignment(alignment.GetCharacterMatrix());

            Assert.AreEqual(expected, actual, 0.01);
        }
    }
}
