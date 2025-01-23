using LibAlignment;
using LibBioInfo;
using LibBioInfo.LegacyAlignmentModifiers;
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
using LibAlignment.Aligners.SingleState;
using LibScoring.FitnessFunctions;


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

        public IterativeAligner GetAligner()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objective = new SumOfPairsFitnessFunction(matrix);
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

            IterativeAligner climber = GetAligner();
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
