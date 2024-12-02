using LibBioInfo.IAlignmentModifiers;
using LibBioInfo;
using LibScoring.ObjectiveFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibScoring.ScoringMatrices;
using LibScoring;
using LibFileIO;

namespace TestsPerformance.LibScoring.ObjectiveFunctions
{
    [TestClass]
    public class AffineGapPenaltyObjectiveTests
    {
        FileHelper FileHelper = new FileHelper();
        static IObjectiveFunction ObjectiveFunction = new AffineGapPenaltyObjectiveFunction();


        [DataTestMethod]
        [DataRow("BB11001", 8)]
        [DataRow("BB11001", 16)]
        [DataRow("BB11001", 32)]
        [Timeout(500)]
        public void CanScoreBBSAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < times; i++)
            {
                double score = ObjectiveFunction.ScoreAlignment(alignment);
            }
        }

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [Timeout(5000)]
        public void CanScorePREFABAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < times; i++)
            {
                double score = ObjectiveFunction.ScoreAlignment(alignment);
            }
        }

    }
}
