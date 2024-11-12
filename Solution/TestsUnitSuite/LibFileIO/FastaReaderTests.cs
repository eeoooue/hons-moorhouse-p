using LibBioInfo;
using LibFileIO.SequenceReaders;
using TestsUnitSuite.HarnessTools;

namespace TestsUnitSuite.LibFileIO
{
    [TestClass]
    public class FastaReaderTests
    {
        private FastaReader FastaReader = new FastaReader();
        private ExampleSequences ExampleSequences = Harness.ExampleSequences;
        private SequenceEquality SequenceEquality = Harness.SequenceEquality;

        #region Testing individual sequences can be read

        [TestMethod]
        public void CanParseSequenceA()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
            };

            BioSequence expected = ExampleSequences.GetSequence(ExampleSequence.ExampleA);
            BioSequence actual = FastaReader.ParseAsSequence(contents);
            SequenceEquality.AssertSequencesMatch(expected, actual);
        }

        [TestMethod]
        public void CanParseSequenceB()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleB",
                "ACGTTTT",
                "TT",
                "TT",
                "",
            };

            BioSequence expected = ExampleSequences.GetSequence(ExampleSequence.ExampleB);
            BioSequence actual = FastaReader.ParseAsSequence(contents);
            SequenceEquality.AssertSequencesMatch(expected, actual);
        }

        [TestMethod]
        public void CanParseSequenceC()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleC",
                "CCCCCCCCCCCCCCCCCCCCCCCCCCC",
                "",
                "C",
                "",
            };

            BioSequence expected = ExampleSequences.GetSequence(ExampleSequence.ExampleC);
            BioSequence actual = FastaReader.ParseAsSequence(contents);
            SequenceEquality.AssertSequencesMatch(expected, actual);
        }

        [TestMethod]
        public void CanParseSequenceD()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleD",
                "ACGTACGT--",
                "--ACGT",
            };

            BioSequence expected = ExampleSequences.GetSequence(ExampleSequence.ExampleD);
            BioSequence actual = FastaReader.ParseAsSequence(contents);
            SequenceEquality.AssertSequencesMatch(expected, actual);
        }

        #endregion


        #region Testing identifier positions can be collected

        [TestMethod]
        public void CanFindIdentifiersTestA()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
            };

            List<int> expected = new List<int> { 0, 2, 4, 6 };
            List<int> actual = FastaReader.CollectIdentifierLocations(contents);

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void CanFindIdentifiersTestB()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleA",
                "ACGT",
                "ACGT",
                "",
                ">ExampleA",
                "ACGTA--",
                "CG--TACG",
                "TACGTACGT",
                "",
                "",
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
            };

            List<int> expected = new List<int> { 0, 4, 10 };
            List<int> actual = FastaReader.CollectIdentifierLocations(contents);

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }


        #endregion


        #region Testing multiple sequences can be read from lines of a FASTA file

        [TestMethod]
        public void CanParseFastaTestA()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
                ">ExampleA",
                "ACGT",
                "ACGT",
                "ACGT",
                "ACGT",
                "ACGT",
                ">ExampleA",
                "ACGTACGTAC",
                "GTACGTACGT",
            };

            List<BioSequence> expected = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
            };

            List<BioSequence> actual = FastaReader.UnpackSequences(contents);
            SequenceEquality.AssertSequencesMatch(expected, actual);
        }

        [TestMethod]
        public void CanParseFastaTestB()
        {
            List<string> contents = new List<string>()
            {
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
                "",
                "",
                ">ExampleB",
                "ACGTTTTTTTT",
                "",
                "",
                ">ExampleC",
                "CCCCCCCCCCC",
                "CCCCCCCCCCC",
                "CCCCCC",
                "",
                "",
                ">ExampleD",
                "ACGTACGT----ACGT",
                "",
                "",
            };

            List<BioSequence> expected = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            List<BioSequence> actual = FastaReader.UnpackSequences(contents);
            SequenceEquality.AssertSequencesMatch(expected, actual);
        }

        #endregion

    }
}