using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibBioInfo.ICrossoverOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TestsHarness;
using TestsHarness.LiteratureAssets;
using TestsHarness.Tools;



namespace TestsUnitSuite.LibBioInfo.ICrossoverOperators
{
    [TestClass]
    public class OnePointCrossoverOperatorTests
    {
        SAGAAssets SAGAAssets = Harness.LiteratureHelper.SAGAAssets;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        OnePointCrossoverOperator Operator = new OnePointCrossoverOperator();

        [TestMethod]
        public void CanCrossoverAlignments()
        {
            Alignment a = ExampleAlignments.GetExampleA();
            Alignment b = a.GetCopy();

            AlignmentRandomizer randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            List<Alignment> results = Operator.CreateAlignmentChildren(a, b);
            AlignmentConservation.AssertAlignmentsAreConserved(a, results[0]);
            AlignmentConservation.AssertAlignmentsAreConserved(a, results[1]);
        }


        [DataTestMethod]
        [DataRow("ACGT", "ACGT", 1, "ACGT")]
        [DataRow("AC--GT", "ACGT--", 1, "ACGT--")]
        [DataRow("AC--GT", "ACGT--", 4, "AC--GT--")]
        [DataRow("AC--GT", "--ACGT", 6, "AC--GT")]

        public void CanCrossoverSequencesAtPosition(string payloadA, string payloadB, int position, string expected)
        {
            BioSequence seqA = new BioSequence("test", payloadA);
            BioSequence seqB = new BioSequence("test", payloadB);
            BioSequence seqX = Operator.CrossoverSequenceAtPosition(seqA, seqB, position);
            string actual = seqX.Payload;

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("A--CGTACGT", 1, "A")]
        [DataRow("A--CGTACGT", 3, "A--")]
        [DataRow("A--CGTACGT", 5, "A--CG")]
        [DataRow("A--CGTACGT", 8, "A--CGTAC")]
        public void CanGetPayloadUntilPosition(string payload, int position, string expected)
        {
            BioSequence seq = new BioSequence("test", payload);
            string actual = Operator.GetPayloadUntilPosition(seq, position);
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("A", "A--CGTACGT", "--CGTACGT")]
        [DataRow("A--CGT", "A--CGTACGT", "ACGT")]

        public void CanExtractComplementForPrefix(string prefix, string payload, string expected)
        {
            BioSequence source = new BioSequence("test", payload);
            string actual = Operator.ExtractComplementForPrefix(prefix, source);
            Assert.AreEqual(expected, actual);
        }
    }
}
