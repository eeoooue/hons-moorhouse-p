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
    public class SequenceNodeTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        [TestMethod]
        public void CanCheckIsConnected()
        {
            List<BioSequence> sequences = GetExampleSequences();

            SequenceNode nodeA = new SequenceNode(sequences[0]);
            SequenceNode nodeB = new SequenceNode(sequences[1]);

            Assert.IsFalse(nodeA.IsConnectedTo(nodeB));
            Assert.IsFalse(nodeB.IsConnectedTo(nodeA));

            Assert.IsFalse(nodeA.IsConnectedTo(nodeA));
            Assert.IsFalse(nodeB.IsConnectedTo(nodeB));

            SimilarityLink link = new SimilarityLink(nodeA, nodeB, 200);

            nodeA.AddConnection(link);
            nodeB.AddConnection(link);

            Assert.IsTrue(nodeA.IsConnectedTo(nodeB));
            Assert.IsTrue(nodeB.IsConnectedTo(nodeA));

            Assert.IsFalse(nodeA.IsConnectedTo(nodeA));
            Assert.IsFalse(nodeB.IsConnectedTo(nodeB));
        }

        [TestMethod]
        public void CanSuggestNeighbours()
        {
            List<BioSequence> sequences = GetExampleSequences();

            SequenceNode nodeA = new SequenceNode(sequences[0]);
            SequenceNode nodeB = new SequenceNode(sequences[1]);

            SequenceNode? suggestion1a = nodeA.SuggestNeighbour();
            Assert.IsTrue(suggestion1a is null);

            SequenceNode? suggestion1b = nodeB.SuggestNeighbour();
            Assert.IsTrue(suggestion1b is null);

            SimilarityLink link = new SimilarityLink(nodeA, nodeB, 200);
            nodeA.AddConnection(link);
            nodeB.AddConnection(link);

            SequenceNode? suggestion2a = nodeA.SuggestNeighbour();
            Assert.IsTrue(suggestion2a is SequenceNode);
            Assert.IsTrue(suggestion2a!.Identifier == nodeB.Identifier);

            SequenceNode? suggestion2b = nodeB.SuggestNeighbour();
            Assert.IsTrue(suggestion2b is SequenceNode);
            Assert.IsTrue(suggestion2b!.Identifier == nodeA.Identifier);
        }

        public SequenceNode GetExampleNode()
        {
            List<BioSequence> sequences = GetExampleSequences();
            return new SequenceNode(sequences[0]);
        }

        public List<BioSequence> GetExampleSequences()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            List<BioSequence> sequences = alignment.Sequences;

            return sequences;
        }
    }
}
