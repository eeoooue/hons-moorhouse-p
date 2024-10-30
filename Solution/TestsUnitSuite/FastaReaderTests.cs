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
                "",
            };

            BioSequence expected = ExampleSequences.GetSequence(ExampleSequence.ExampleA);
            BioSequence actual = FastaReader.ParseAsSequence(contents);

            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.Payload, actual.Payload);
        }

        [TestMethod]
        public void CanParseSequenceB()
        {


        }

        [TestMethod]
        public void CanParseSequenceC()
        {



        }

        [TestMethod]
        public void CanParseSequenceD()
        {



        }
    }
}