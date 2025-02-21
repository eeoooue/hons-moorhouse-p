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

        public string Identifier { get { return Sequence.Identifier; } }

        public SequenceNode(BioSequence sequence)
        {
            Sequence = sequence;
        }

        public void AddConnection(string identifier, SimilarityLink link)
        {
            if (!IsConnectedTo(identifier))
            {
                Connections.Add(link);
                ConnectedNodes.Add(identifier);
            }
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


        // TODO: replace this with a roulette selection implementation
        public SequenceNode? SuggestNeighbour()
        {
            List<SequenceNode> options = GetNeighbours();
            int n = options.Count;

            if (n > 0)
            {
                int i = Randomizer.Random.Next(n);
                return options[i];
            }

            return null;
        }
    }
}
