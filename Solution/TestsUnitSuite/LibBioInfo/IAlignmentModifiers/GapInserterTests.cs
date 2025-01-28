using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibModification;
using LibModification.AlignmentModifiers;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class GapInserterTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        GapInserter GapInserter = new GapInserter();

        [DataTestMethod]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void CanInsertGapsOfWidth(int width)
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            GapInserter.InsertGapOfWidth(alignment.CharacterMatrix, width);

            List<BioSequence> sequences = alignment.GetAlignedSequences();
            foreach (BioSequence sequence in sequences)
            {
                bool verdict = SequenceContainsGapOfWidth(sequence, width);
                Assert.IsTrue(verdict);
            }
        }

        public bool SequenceContainsGapOfWidth(BioSequence sequence, int width)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<width; i++)
            {
                sb.Append('-');
            }
            string gap = sb.ToString();

            return sequence.Payload.Contains(gap);
        }

        [TestMethod]
        public void AlignmentIsDifferentAfterInsertion()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapInserter.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentStateIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapInserter.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }
    }
}
