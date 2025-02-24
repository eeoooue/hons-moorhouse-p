using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibSimilarity
{
    public static class SimilarityGuide
    {
        public static SimilarityJudge Judge = new SimilarityJudge();

        public static SimilarityGraph Graph = new SimilarityGraph();


        public static void SetSequences(List<BioSequence> sequences)
        {
            Graph.SetSequences(sequences);
        }

        public static void TryUpdateSimilarity()
        {
            List<BioSequence> sequences = Graph.TryFindPairOfUnconnectedSequences();
            if (sequences.Count == 2)
            {
                UpdateSimilarity(sequences[0], sequences[1]);
            }
        }

        public static void UpdateSimilarity(BioSequence a, BioSequence b)
        {
            double similarity = Judge.GetSimilarity(a, b);
            Graph.RecordSimilarity(a, b, similarity);
        }

        public static List<BioSequence> GetSetOfSimilarSequences()
        {
            int n = Graph.Population / 2;
            int attempts = Randomizer.Random.Next(1, n+1);

            SequenceNode source = Graph.GetRandomStartingNode();
            List<SequenceNode> group = GetRandomSetAroundNode(source, attempts);

            List<BioSequence> result = new List<BioSequence>();
            foreach(SequenceNode node in group)
            {
                result.Add(node.Sequence);
            }

            return result;
        }

        public static List<SequenceNode> GetRandomSetAroundNode(SequenceNode start, int attempts)
        {
            List<SequenceNode> members = new List<SequenceNode>();
            HashSet<string> blacklist = new HashSet<string>();
            members.Add(start);
            blacklist.Add(start.Identifier);

            for(int i=0; i<attempts; i++)
            {
                SequenceNode current = PickNodeFromListRandomly(members);
                SequenceNode? suggestion = start.SuggestNeighbour();
                if (suggestion is SequenceNode option)
                {
                    if (!blacklist.Contains(suggestion.Identifier))
                    {
                        members.Add(suggestion);
                        blacklist.Add(suggestion.Identifier);
                    }
                }
            }

            return members;
        }

        private static SequenceNode PickNodeFromListRandomly(List<SequenceNode> nodes)
        {
            int n = nodes.Count;
            int i = Randomizer.Random.Next(n);
            return nodes[i];
        }
    }
}
