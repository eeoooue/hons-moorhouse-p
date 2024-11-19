using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite.HarnessTools;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class GapShifterTests
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        GapShifter GapShifter = new GapShifter();

        [TestMethod]
        public void AlignmentIsDifferentAfterShift()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment original = new Alignment(inputs);
            Alignment copy = original.GetCopy();
            GapShifter.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentStateIsDifferentAfterShift()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment original = new Alignment(inputs);

            Alignment copy = original.GetCopy();
            GapShifter.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentStatesMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }

        public bool AlignmentStatesMatch(Alignment a, Alignment b)
        {
            int m = Math.Min(a.Height, b.Height);
            int n = Math.Min(a.Width, b.Width);

            if (a.Height != b.Height)
            {
                return true;
            }

            if (a.Width != b.Width)
            {
                return true;
            }

            for (int i=0; i<m; i++)
            {
                for (int j=0; j<n; j++)
                {
                    bool aValue = a.State[i, j];
                    bool bValue = b.State[i, j];
                    if (aValue != bValue)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
