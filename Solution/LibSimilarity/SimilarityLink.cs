using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    public class SimilarityLink
    {
        public SequenceNode NodeA;
        public SequenceNode NodeB;
        public double SimilarityScore;

        public SimilarityLink(SequenceNode a, SequenceNode b, double score)
        {
            NodeA = a;
            NodeB = b;
            SimilarityScore = score;
        }

        public SequenceNode GetNeighbour(SequenceNode node)
        {
            if (node.Identifier == NodeA.Identifier)
            {
                return NodeB;
            }

            if (node.Identifier == NodeB.Identifier)
            {
                return NodeA;
            }

            throw new ArgumentException("node passed into GetOtherNode() was not a member of the link");
        }
    }
}
