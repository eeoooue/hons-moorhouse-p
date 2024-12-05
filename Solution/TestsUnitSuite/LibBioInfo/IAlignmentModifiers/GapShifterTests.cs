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

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class GapShifterTests
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
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
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapShifter.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentStateIsDifferentAfterShift()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            Alignment copy = original.GetCopy();
            GapShifter.ModifyAlignment(copy);

            bool alignmentsStatesMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsStatesMatch);
        }
    }
}
