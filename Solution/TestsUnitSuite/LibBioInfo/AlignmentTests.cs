using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite;

using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibBioInfo
{
    [TestClass]
    public class AlignmentTests
    {

        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        private FileHelper FileHelper = new FileHelper();

        #region Timing alignment duplication

        [DataTestMethod]
        [DataRow("BB11003", 8)]
        [DataRow("BB11003", 16)]
        [DataRow("BB11003", 32)]
        [DataRow("BB11003", 64)]
        [DataRow("BB11003", 128)]
        [Timeout(500)]
        public void CanDuplicateBBSAlignmentsEfficiently(string filename, int duplicates)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i=0; i<duplicates; i++)
            {
                Alignment copy = alignment.GetCopy();
                result.Add(copy);
            }
        }

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [DataRow("1ggxA_1h4uA", 64)]
        [DataRow("1ggxA_1h4uA", 128)]
        [Timeout(500)]
        public void CanDuplicatePREFABAlignmentsEfficiently(string filename, int duplicates)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < duplicates; i++)
            {
                Alignment copy = alignment.GetCopy();
                result.Add(copy);
            }
        }

        #endregion



        #region Supports alignment of real sequences


        [DataTestMethod]
        [DataRow("BB11001")]
        [DataRow("BB11002")]
        [DataRow("BB11003")]
        [DataRow("1axkA_2nlrA")]
        [DataRow("1eagA_1smrA")]
        [DataRow("1ggxA_1h4uA")]
        public void RealSequencesCanBeAligned(string filename)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment ali = new Alignment(sequences);
            Assert.IsNotNull(ali);
            Assert.IsTrue(ali.SequencesCanBeAligned());
        }

        #endregion


        #region Basic data representation tests

        [TestMethod]
        public void CanRetrieveOriginalSequences()
        {
            List<BioSequence> original = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment alignment = new Alignment(original);
            List<BioSequence> aligned = alignment.GetAlignedSequences();

            for(int i=0; i< original.Count; i++)
            {
                SequenceConservation.AssertDataIsConserved(original[i], aligned[i]);
            }
        }

        #endregion


        #region Checking initial alignment state is left-justified

        [TestMethod]
        public void CheckingInitialStateIsLeftJustified()
        {
            List<BioSequence> original = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment alignment = new Alignment(original);
            List<BioSequence> aligned = alignment.GetAlignedSequences();

            for (int i = 0; i < original.Count; i++)
            {
                string residues = original[i].Residues;
                string alignedPayload = aligned[i].Payload;
                bool isLeftJustified = alignedPayload.StartsWith(residues);
                Assert.IsTrue(isLeftJustified);
            }
        }

        #endregion


        #region Checking alignment composition validation

        [TestMethod]
        public void CanIdentifyAlignableContents()
        {
            List<BioSequence> original = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment alignment = new Alignment(original);
            bool verdict = alignment.SequencesCanBeAligned();
            Assert.IsTrue(verdict);
        }

        [TestMethod]
        public void CanIdentifyUnalignableContents()
        {
            List<BioSequence> original =
            [
                new BioSequence("bad", "X!!#-X"),
                new BioSequence("bad", "KRYPT-#"),
                new BioSequence("normal", "--ACGT"),
                new BioSequence("normal", "--ACGT"),
            ];

            Alignment alignment = new Alignment(original);
            bool verdict = alignment.SequencesCanBeAligned();
            Assert.IsFalse(verdict);
        }

        #endregion


        #region Testing alignment copy

        [TestMethod]
        public void CanCopyAlignment()
        {
            List<BioSequence> original = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment alignment = new Alignment(original);
            Alignment copy = alignment.GetCopy();

            List<BioSequence> originalAligned = alignment.GetAlignedSequences();
            List<BioSequence> copyAligned = copy.GetAlignedSequences();

            SequenceConservation.AssertDataIsConserved(originalAligned, copyAligned);
            SequenceEquality.AssertSequencesMatch(originalAligned, copyAligned);
        }

        [TestMethod]
        public void CanEditCopyIndependently()
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

            IAlignmentModifier modifier = new AlignmentRandomizer();
            modifier.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);
        }



        [TestMethod]
        public void CopyingConservesAlignmentState()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment original = new Alignment(inputs);
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(original);

            Alignment copy = original.GetCopy();

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsTrue(alignmentsMatch);
        }


        #endregion
    }
}
