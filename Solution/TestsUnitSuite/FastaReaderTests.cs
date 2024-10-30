using LibBioInfo;
using LibFileIO.Readers;
using TestsUnitSuite.Harness;

namespace TestsUnitSuite
{
    [TestClass]
    public class FastaReaderTests
    {
        private FastaReader FastaReader = new FastaReader();
        private ExampleSequences ExampleSequences = new ExampleSequences();
        private TestingHarness Harness = new TestingHarness();

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

            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.Payload, actual.Payload);
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

            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.Payload, actual.Payload);
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

            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.Payload, actual.Payload);
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

            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.Payload, actual.Payload);
        }
    }
}