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
    public class SimilarityGraphTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        [TestMethod]
        public void CanRecordSimilarity()
        {
            List<BioSequence> sequences = GetExampleSequences();
            SimilarityGraph.SetSequences(sequences);
            SimilarityGraph.RecordSimilarity(sequences[0], sequences[1], 200);
        }

        [TestMethod]
        public void CanGetStartingNode()
        {
            List<BioSequence> sequences = GetExampleSequences();

            SimilarityGraph.SetSequences(sequences);
            SequenceNode node1 = SimilarityGraph.GetRandomStartingNode();
            SimilarityGraph.RecordSimilarity(sequences[0], sequences[1], 200);
            SequenceNode node2 = SimilarityGraph.GetRandomStartingNode();

            bool isSeq0 = node2.Identifier == sequences[0].Identifier;
            bool isSeq1 = node2.Identifier == sequences[1].Identifier;

            Assert.IsTrue(isSeq0 || isSeq1);
        }

        public List<BioSequence> GetExampleSequences()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            List<BioSequence> sequences = alignment.Sequences;

            return sequences;
        }
    }
}
