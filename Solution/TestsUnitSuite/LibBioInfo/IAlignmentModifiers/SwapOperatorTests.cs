using LibBioInfo;
using LibModification.AlignmentModifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class SwapOperatorTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        SwapOperator SwapOperator = new SwapOperator();

        [TestMethod]
        public void OperationResultMatchesFigure()
        {
            // Reproducing Fig 1. example from Kim et. al. (1994)
            // https://doi.org/10.1093/bioinformatics/10.4.419
            // this implementation assumes the "swap (2,4,3, right)" in the original caption is a typo of "swap (2,3,4, right)"

            BioSequence sequence1 = new BioSequence("seq1", "MKQIGGA--MGSLA-");
            BioSequence sequence2 = new BioSequence("seq2", "MKK---IGGATGALG");
            List<BioSequence> sequences1and2 = new List<BioSequence>() { sequence1, sequence2 };
            Alignment original = new Alignment(sequences1and2, true);

            BioSequence sequence1after = new BioSequence("seq1", "MKQIGGA--MGSLA-");
            BioSequence sequence2after = new BioSequence("seq2", "MKKIGGA---TGALG");
            List<BioSequence> sequences1and2after = new List<BioSequence>() { sequence1after, sequence2after };
            Alignment expected = new Alignment(sequences1and2after, true);
            
            // my swap call uses 1 instead of 2 due to the figure sequences being 1-indexed while I use 0-indexing
            SwapOperator.Swap(ref original.CharacterMatrix, 1, 3, 4, SwapDirection.Right);

            Harness.AlignmentPrinter.PrintAlignment(original);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(expected, original);
            Assert.IsTrue(alignmentsMatch);
        }

        [TestMethod]
        public void AlignmentIsDifferentAfterModification()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            SwapOperator.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentStateIsDifferentAfterModification()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            SwapOperator.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }
    }
}
