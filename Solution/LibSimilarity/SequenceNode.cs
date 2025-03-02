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

        public int MaxConnections {  get { return SimilarityGuide.NodeEdgeLimit; } }

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
                if (Connections.Count > MaxConnections)
                {
                    RemoveWeakestLink();
                }
            }
        }

        private void RemoveWeakestLink()
        {
            SimilarityLink weakest = Connections[0];
            for(int i=1; i<Connections.Count; i++)
            {
                SimilarityLink current = Connections[i];
                if (current.SimilarityScore < weakest.SimilarityScore)
                {
                    weakest = current;
                }
            }
            Connections.Remove(weakest);

            SequenceNode neighbour = weakest.GetNeighbour(this);
            ConnectedNodes.Remove(neighbour.Identifier);
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
