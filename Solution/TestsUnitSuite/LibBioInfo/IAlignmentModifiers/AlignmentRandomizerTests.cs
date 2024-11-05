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
    public class AlignmentRandomizerTests
    {

        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;

        AlignmentRandomizer AlignmentRandomizer = new AlignmentRandomizer();

        [TestMethod]
        public void AlignmentIsDifferentAfterRandomization()
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
            AlignmentRandomizer.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            List<BioSequence> originalSeqs = original.GetAlignedSequences();
            List<BioSequence> randomizedSeqs = copy.GetAlignedSequences();
            SequenceConservation.AssertDataIsConserved(originalSeqs, randomizedSeqs);
        }

        private void PrintAlignmentState(Alignment alignment)
        {
            bool[,] state = alignment.State;

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    Console.Write(state[i, j] ? "1" : "0");
                }
                Console.WriteLine();
            }
        }
    }
}
