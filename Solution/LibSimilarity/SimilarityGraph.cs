using LibBioInfo;

namespace LibSimilarity
{
    public static class SimilarityGraph
    {
        private static Dictionary<string, SequenceNode> Nodes = new Dictionary<string, SequenceNode>();
        private static List<string> Identifiers = new List<string>();
        private static HashSet<string> HasConnection = new HashSet<string>();
        private static List<string> ConnectedIdentifiers = new List<string>();
        public static List<BioSequence> Sequences = new List<BioSequence>();

        public static int ConnectedNodes { get { return ConnectedIdentifiers.Count; } }

        public static int NodeCount { get { return Identifiers.Count; } }

        public static int Population { get { return Identifiers.Count; } }

        private static void ClearState()
        {
            Sequences.Clear();
            Nodes.Clear();
            Identifiers.Clear();
            HasConnection.Clear();
            ConnectedIdentifiers.Clear();
        }

        public static double GetPercentageSaturation()
        {
            int currentTotal = 0;
            int maximumTotal = 0;

            foreach (string identifier in Identifiers)
            {
                SequenceNode node = Nodes[identifier];
                currentTotal += node.Connections.Count;
                maximumTotal += Identifiers.Count - 1;
            }

            return (double) 100.0 * currentTotal / maximumTotal;
        }

        public static void DebugConnections()
        {
            foreach(string identifier in Identifiers)
            {
                SequenceNode node = Nodes[identifier];
                foreach(SimilarityLink link in node.Connections)
                {
                    SequenceNode other = link.GetNeighbour(node);

                    string a = node.Identifier;
                    string b = other.Identifier;
                    Console.WriteLine($"SimilarityLink: {a} -> {b} ({link.SimilarityScore})");
                }
                Console.WriteLine();
            }
        }

        public static void SetSequences(List<BioSequence> sequences)
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

        public static List<SequenceNode> CreateNodes(List<BioSequence> sequences)
        {
            List<SequenceNode> result = new List<SequenceNode>();
            foreach (BioSequence sequence in sequences)
            {
                SequenceNode node = new SequenceNode(sequence);
                result.Add(node);
            }

            return result;
        }

        public static SequenceNode GetNode(string identifier)
        {
            return Nodes[identifier];
        }

        public static void RecordSimilarity(BioSequence a, BioSequence b, double score)
        {
            SequenceNode nodeA = GetNode(a.Identifier);
            SequenceNode nodeB = GetNode(b.Identifier);
            ConnectNodes(nodeA, nodeB, score);
        }

        public static void ConnectNodes(SequenceNode nodeA, SequenceNode nodeB, double score)
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

        private static void RecordConnected(string identifier)
        {
            if (!HasConnection.Contains(identifier))
            {
                HasConnection.Add(identifier);
                ConnectedIdentifiers.Add(identifier);
            }
        }

        public static SequenceNode GetRandomStartingNode()
        {
            string identifier = GetRandomConnectedIdentifierIfPossible();
            return Nodes[identifier];
        }

        public static string GetRandomConnectedIdentifierIfPossible()
        {
            if (ConnectedIdentifiers.Count > 0)
            {
                return GetRandomIdentifier(ConnectedIdentifiers);
            }

            return GetRandomIdentifier(Identifiers);
        }

        public static string GetRandomIdentifier(List<string> identifiers)
        {
            int n = identifiers.Count;
            int i = Randomizer.Random.Next(n);

            return identifiers[i];
        }
    }
}
