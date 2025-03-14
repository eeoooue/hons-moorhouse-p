using LibBioInfo;

namespace LibSimilarity
{
    public class SimilarityGraph
    {
        private Dictionary<string, SequenceNode> Nodes = new Dictionary<string, SequenceNode>();
        private List<string> Identifiers = new List<string>();
        private HashSet<string> HasConnection = new HashSet<string>();
        private List<string> ConnectedIdentifiers = new List<string>();
        public List<BioSequence> Sequences = new List<BioSequence>();

        public int Population { get { return Sequences.Count; } }

        private void ClearState()
        {
            Sequences.Clear();
            Nodes.Clear();
            Identifiers.Clear();
            HasConnection.Clear();
            ConnectedIdentifiers.Clear();
        }

        public void SetSequences(List<BioSequence> sequences)
        {
            ClearState();
            Sequences = sequences;
            List<SequenceNode> nodes = CreateNodes(sequences);
            foreach (SequenceNode node in nodes)
            {
                Nodes[node.Identifier] = node;
                Identifiers.Add(node.Identifier);
            }
        }


        public double GetPercentageSaturation()
        {
            int currentTotal = 0;
            int maximumTotal = 0;

            foreach (string identifier in Identifiers)
            {
                SequenceNode node = Nodes[identifier];
                currentTotal += node.Connections.Count;
                maximumTotal += Math.Min(Identifiers.Count - 1, node.MaxConnections);
            }

            return (double) 100.0 * currentTotal / maximumTotal;
        }

        public List<SequenceNode> CreateNodes(List<BioSequence> sequences)
        {
            List<SequenceNode> result = new List<SequenceNode>();
            foreach (BioSequence sequence in sequences)
            {
                SequenceNode node = new SequenceNode(sequence);
                result.Add(node);
            }

            return result;
        }

        public SequenceNode GetNode(string identifier)
        {
            return Nodes[identifier];
        }

        public void RecordSimilarity(BioSequence a, BioSequence b, double score)
        {
            SequenceNode nodeA = GetNode(a.Identifier);
            SequenceNode nodeB = GetNode(b.Identifier);
            ConnectNodes(nodeA, nodeB, score);
        }

        public void ConnectNodes(SequenceNode nodeA, SequenceNode nodeB, double score)
        {
            if (nodeA.IsConnectedTo(nodeB.Identifier))
            {
                return;
            }

            SimilarityLink link = new SimilarityLink(nodeA, nodeB, score);
            nodeA.AddConnection(link);
            nodeB.AddConnection(link);
            RecordConnected(nodeA.Identifier);
            RecordConnected(nodeB.Identifier);
        }

        private void RecordConnected(string identifier)
        {
            if (!HasConnection.Contains(identifier))
            {
                HasConnection.Add(identifier);
                ConnectedIdentifiers.Add(identifier);
            }
        }

        public SequenceNode GetRandomStartingNode()
        {
            string identifier = GetRandomConnectedIdentifierIfPossible();
            return Nodes[identifier];
        }

        public string GetRandomConnectedIdentifierIfPossible()
        {
            if (ConnectedIdentifiers.Count > 0)
            {
                return GetRandomIdentifier(ConnectedIdentifiers);
            }

            return GetRandomIdentifier(Identifiers);
        }

        public string GetRandomIdentifier(List<string> identifiers)
        {
            int n = identifiers.Count;
            int i = Randomizer.Random.Next(n);

            return identifiers[i];
        }

        public List<BioSequence> TryFindPairOfUnconnectedSequences()
        {
            List<BioSequence> result = new List<BioSequence>();

            string identifier = GetRandomIdentifier(Identifiers);
            SequenceNode node = GetNode(identifier);

            if (node.HasMissingConnections(Population))
            {
                BioSequence target = node.SelectRandomMissingNeighbour(Sequences);
                result.Add(node.Sequence);
                result.Add(target);
            }

            return result;
        }
    }
}
