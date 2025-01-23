using LibAlignment;
using LibBioInfo;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibFileIO;
using LibAlignment.Aligners.SingleState;
using LibScoring.FitnessFunctions;

namespace TestsPerformance.LibAlignment
{
    [TestClass]
    public class SelectiveRandomWalkAlignerTests
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        private FileHelper FileHelper = new FileHelper();

        #region Testing time-efficiency of alignment process

        [DataTestMethod]
        [DataRow("BB11003", 8)]
        [DataRow("BB11003", 16)]
        [DataRow("BB11003", 32)]
        [Timeout(5000)]
        public void CanAlignBBSEfficiently(string filename, int iterations)
        {
            IterativeAligner aligner = GetAligner();
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            aligner.IterationsLimit = iterations;
            Alignment result = aligner.AlignSequences(sequences);
        }

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [Timeout(5000)]
        public void CanAlignPREFABEfficiently(string filename, int iterations)
        {
            IterativeAligner aligner = GetAligner();
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            aligner.IterationsLimit = iterations;
            Alignment result = aligner.AlignSequences(sequences);
        }

        #endregion

        public IterativeAligner GetAligner()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objective = new SumOfPairsFitnessFunction(matrix);
            const int maxIterations = 50;
            SelectiveRandomWalkAligner aligner = new SelectiveRandomWalkAligner(objective, maxIterations);
            return aligner;
        }
    }
}
