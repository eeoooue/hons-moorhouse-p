using LibBioInfo;
using System.Xml.Linq;

namespace LibSimilarity
{
    public class SimilarityGraph
    {
        private Dictionary<string, SequenceNode> Nodes = new Dictionary<string, SequenceNode>();

        public SimilarityGraph(List<BioSequence> sequences)
        {
            CreateNodes(sequences);
        }

        public void CreateNodes(List<BioSequence> sequences)
        {
            foreach (BioSequence sequence in sequences)
            {
                SequenceNode node = new SequenceNode(sequence);
                Nodes[sequence.Identifier] = node;
            }
        }

        public SequenceNode GetNode(string identifier)
        {
            return Nodes[identifier];
        }

        public void RecordSimilarity(BioSequence a, BioSequence b, double score)
        {
            SequenceNode nodeA = GetNode(a.Identifier);
            SequenceNode nodeB = GetNode(b.Identifier);

            SimilarityLink link = new SimilarityLink(nodeA, nodeB, score);
            nodeA.AddConnection(link);
            nodeB.AddConnection(link);
        }
    }
}
