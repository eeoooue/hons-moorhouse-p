using LibAlignment;
using LibAlignment.Aligners;
using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibFileIO;


using TestsHarness;
using TestsHarness.Tools;


namespace TestsUnitSuite.LibAlignment
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
            Aligner aligner = GetAligner();
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
            Aligner aligner = GetAligner();
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            aligner.IterationsLimit = iterations;
            Alignment result = aligner.AlignSequences(sequences);
        }

        #endregion



        public Aligner GetAligner()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            const int maxIterations = 50;
            SelectiveRandomWalkAligner aligner = new SelectiveRandomWalkAligner(objective, maxIterations);
            return aligner;
        }



        [TestMethod]
        public void AlignerModifiesAlignment()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Aligner climber = GetAligner();
            climber.Initialize(inputs);
            Alignment initial = climber.CurrentAlignment!.GetCopy();
            
            for (int i=0; i<100; i++)
            {
                climber.Iterate();
            }

            Alignment result = climber.CurrentAlignment!;

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(initial, result);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(initial, result);
        }
    }
}
