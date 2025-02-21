using LibBioInfo;

namespace LibSimilarity
{
    public class SimilarityGraph
    {
        private Dictionary<string, SequenceNode> Nodes = new Dictionary<string, SequenceNode>();
        private List<string> Identifiers = new List<string>();
        private HashSet<string> HasConnection = new HashSet<string>();
        private List<string> ConnectedIdentifiers = new List<string>();

        public int Population { get { return Identifiers.Count; } }

        public SimilarityGraph(List<BioSequence> sequences)
        {
            List<SequenceNode> nodes = CreateNodes(sequences);
            foreach(SequenceNode node in nodes)
            {
                Nodes[node.Identifier] = node;
                Identifiers.Add(node.Identifier);
            }
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
            nodeA.AddConnection(nodeB.Identifier, link);
            nodeB.AddConnection(nodeA.Identifier, link);

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
    }
}
