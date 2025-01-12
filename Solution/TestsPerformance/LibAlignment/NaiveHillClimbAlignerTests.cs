using LibAlignment.Aligners;
using LibAlignment;
using LibBioInfo;
using LibScoring.ObjectiveFunctions;
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

namespace TestsPerformance.LibAlignment
{
    [TestClass]
    public class NaiveHillClimbAlignerTests
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        private FileHelper FileHelper = new FileHelper();

        #region Testing time-efficiency of alignment process

        [DataTestMethod]
        [DataRow("BB11001", 10)]
        [Timeout(5000)]
        [Ignore]
        public void CanAlignEfficiently(string filename, int iterations)
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
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            const int maxIterations = 50;
            return new NaiveHillClimbAligner(objective, maxIterations);
        }
    }
}
