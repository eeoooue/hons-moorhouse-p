using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibScoring.ObjectiveFunctions;
using LibBioInfo;

namespace TestsUnitSuite.LibScoring.ObjectiveFunctions
{
    [TestClass]
    public class TotallyConservedColumnsObjectiveTests
    {

        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        TotallyConservedColumnsObjectiveFunction Objective = new TotallyConservedColumnsObjectiveFunction();

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
            double actual = Objective.ScoreAlignment(alignment);

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
            double actual = Objective.ScoreAlignment(alignment);

            Assert.AreEqual(expected, actual, 0.01);
        }
    }
}
