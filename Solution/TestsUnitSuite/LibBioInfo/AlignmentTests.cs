﻿using LibBioInfo;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite;

using TestsHarness;
using TestsHarness.Tools;
using LibModification;
using LibModification.AlignmentModifiers;

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
        StateEquality StateEquality = Harness.StateEquality;
        AlignmentPrinter AlignmentPrinter = Harness.AlignmentPrinter;

        private FileHelper FileHelper = new FileHelper();

        #region Can create alignment conserving state

        [DataTestMethod]
        [DataRow(17)]
        [DataRow(56)]
        [DataRow(525)]
        [DataRow(1756)]
        public void CanOptToConserveAlignmentState(int seed)
        {
            Randomizer.SetSeed(seed);
            Alignment expected = ExampleAlignments.GetExampleA();
            AlignmentRandomizer randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(expected);

            Alignment actual = new Alignment(expected.GetAlignedSequences(), true);

            AlignmentConservation.AssertAlignmentsAreConserved(expected, actual);
            AlignmentEquality.AssertAlignmentsMatch(expected, actual);
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
