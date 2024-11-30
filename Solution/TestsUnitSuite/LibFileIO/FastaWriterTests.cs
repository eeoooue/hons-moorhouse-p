using LibBioInfo;
using LibFileIO.AlignmentWriters;
using LibFileIO.SequenceReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibFileIO
{
    [TestClass]
    public class FastaWriterTests
    {
        private FastaWriter FastaWriter = new FastaWriter();
        private ExampleSequences ExampleSequences = Harness.ExampleSequences;
        private SequenceEquality SequenceEquality = Harness.SequenceEquality;


        #region Testing individual sequences can turned into lines

        [TestMethod]
        public void CanBuildSequenceLinesA()
        {
            List<string> expected = new List<string>()
            {
                ">ExampleA",
                "ACGTACGTACGTACGTACGT",
            };

            BioSequence sequence = ExampleSequences.GetSequence(ExampleSequence.ExampleA);
            List<string> actual = FastaWriter.CreateSequenceLines(sequence);
            
            for(int i=0; i<expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void CanBuildSequenceLinesB()
        {
            List<string> expected = new List<string>()
            {
                ">ExampleB",
                "ACGTTTTTTTT",
            };

            BioSequence sequence = ExampleSequences.GetSequence(ExampleSequence.ExampleB);
            List<string> actual = FastaWriter.CreateSequenceLines(sequence);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void CanBuildSequenceLinesC()
        {
            List<string> expected = new List<string>()
            {
                ">ExampleC",
                "CCCCCCCCCCCCCCCCCCCCCCCCCCCC",
            };

            BioSequence sequence = ExampleSequences.GetSequence(ExampleSequence.ExampleC);
            List<string> actual = FastaWriter.CreateSequenceLines(sequence);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void CanBuildSequenceLinesD()
        {
            List<string> expected = new List<string>()
            {
                ">ExampleD",
                "ACGTACGT----ACGT",
            };

            BioSequence sequence = ExampleSequences.GetSequence(ExampleSequence.ExampleD);
            List<string> actual = FastaWriter.CreateSequenceLines(sequence);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }


        #endregion

    }
}
