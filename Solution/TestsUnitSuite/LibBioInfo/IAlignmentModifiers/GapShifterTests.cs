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
using TestsUnitSuite.HarnessTools;
using LibFileIO;

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
        FileHelper FileHelper = new FileHelper();

        #region Is Time Efficient

        [DataTestMethod]
        [DataRow("BB11003", 8)]
        [DataRow("BB11003", 16)]
        [DataRow("BB11003", 32)]
        [DataRow("BB11003", 64)]
        [DataRow("BB11003", 128)]
        [Timeout(500)]
        public void CanModifyBBSAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < times; i++)
            {
                GapShifter.ModifyAlignment(alignment);
            }
        }

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [DataRow("1ggxA_1h4uA", 64)]
        [DataRow("1ggxA_1h4uA", 128)]
        [Timeout(500)]
        public void CanModifyPREFABAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < times; i++)
            {
                GapShifter.ModifyAlignment(alignment);
            }
        }

        #endregion








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
