using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibSimilarity
{
    public class SimilarityGuide : ISimilarityGuide
    {
        private SimilarityGraph Graph;

        public SimilarityGuide(List<BioSequence> sequences)
        {
            Graph = new SimilarityGraph(sequences);
        }

        public void RecordSimilarity(BioSequence a, BioSequence b, double score)
        {
            Graph.RecordSimilarity(a, b, score);
        }

        public List<BioSequence> GetSetOfSimilarSequences()
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

        public List<SequenceNode> GetRandomSetAroundNode(SequenceNode start, int attempts)
        {
            List<SequenceNode> members = new List<SequenceNode>();
            HashSet<string> blacklist = new HashSet<string>();
            members.Add(start);
            blacklist.Add(start.Identifier);

            for(int i=0; i<attempts; i++)
            {
                SequenceNode current = PickNodeFromListRandomly(members);
                SequenceNode? suggestion = current.SuggestNeighbour();
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

        private SequenceNode PickNodeFromListRandomly(List<SequenceNode> nodes)
        {
            int n = nodes.Count;
            int i = Randomizer.Random.Next(n);
            return nodes[i];
        }
    }
}
