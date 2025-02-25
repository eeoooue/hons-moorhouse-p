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

        public bool HasMissingConnections(int alignmentHeight)
        {
            int possibleNeighbours = alignmentHeight - 1;
            return Connections.Count < possibleNeighbours;
        }

        public List<BioSequence> ListMissingConnections(List<BioSequence> sequences)
        {
            List<BioSequence> result = new List<BioSequence>();
            foreach (BioSequence sequence in sequences)
            {
                if (!IsConnectedTo(sequence.Identifier))
                {
                    if (sequence.Identifier != Identifier)
                    {
                        result.Add(sequence);
                    }
                }
            }

            return result;
        }

        public BioSequence SelectRandomMissingNeighbour(List<BioSequence> sequences)
        {
            List<BioSequence> candidates = ListMissingConnections(sequences);
            int i = Randomizer.Random.Next(candidates.Count);
            return candidates[i];
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
