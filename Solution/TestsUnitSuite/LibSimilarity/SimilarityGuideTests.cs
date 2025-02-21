using LibBioInfo;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibSimilarity
{
    [TestClass]
    public class SimilarityGuideTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        [TestMethod]
        public void CanRecordSimilarity()
        {
            List<BioSequence> sequences = GetExampleSequences();
            SimilarityGuide guide = new SimilarityGuide(sequences);
            guide.RecordSimilarity(sequences[0], sequences[1], 200);
        }

        [TestMethod]
        public void CanGetSetOfSequences()
        {
            List<BioSequence> sequences = GetExampleSequences();
            SimilarityGuide guide = new SimilarityGuide(sequences);
            guide.RecordSimilarity(sequences[0], sequences[1], 200);

            List<BioSequence> set = guide.GetSetOfSimilarSequences();
            Assert.IsTrue(set.Count > 0);
        }

        public List<BioSequence> GetExampleSequences()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            List<BioSequence> sequences = alignment.Sequences;

            return sequences;
        }
    }
}
