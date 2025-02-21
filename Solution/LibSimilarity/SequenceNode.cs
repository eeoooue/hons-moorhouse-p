using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    public class SequenceNode
    {
        public List<SimilarityLink> Connections = new List<SimilarityLink>();

        private HashSet<string> ConnectedNodes = new HashSet<string>();

        public BioSequence Sequence;

        private static RouletteWheel RouletteWheel = new RouletteWheel();

        public string Identifier { get { return Sequence.Identifier; } }

        public SequenceNode(BioSequence sequence)
        {
            Sequence = sequence;
        }

        public void AddConnection(SimilarityLink link)
        {
            SequenceNode neighbour = link.GetNeighbour(this);
            string identifier = neighbour.Identifier;

            if (!IsConnectedTo(identifier))
            {
                Connections.Add(link);
                ConnectedNodes.Add(identifier);
            }
        }

        public bool IsConnectedTo(SequenceNode node)
        {
            return ConnectedNodes.Contains(node.Identifier);
        }

        public bool IsConnectedTo(string identifier)
        {
            return ConnectedNodes.Contains(identifier);
        }

        public List<SequenceNode> GetNeighbours()
        {
            List<SequenceNode> result = new List<SequenceNode>();
            foreach(SimilarityLink link in Connections)
            {
                SequenceNode nodeA = link.NodeA;
                SequenceNode nodeB = link.NodeB;
                if (nodeA.Identifier != Identifier)
                {
                    result.Add(nodeA);
                }
                else
                {
                    result.Add(nodeB);
                }
            }

            return result;
        }

        public SequenceNode? SuggestNeighbour()
        {
            if (Connections.Count > 0)
            {
                SimilarityLink link = RouletteWheel.PerformSelectionOn(Connections);
                SequenceNode neighbour = link.GetNeighbour(this);
                return neighbour;
            }

            return null;
        }
    }
}
