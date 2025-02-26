﻿using LibBioInfo;
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

        public static int CurrentSetSize = 0;

        public static int NodeEdgeLimit = 10;


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

        public static bool[] GetSetOfSimilarSequencesAsMask(Alignment alignment)
        {
            HashSet<string> selected = GetIdentifiersOfSetOfSimilarSequences();
            int m = alignment.Height;

            bool[] result = new bool[m];

            for (int i = 0; i < m; i++)
            {
                BioSequence sequence = alignment.Sequences[i];
                result[i] = selected.Contains(sequence.Identifier);
            }

            return result;
        }

        public static HashSet<string> GetIdentifiersOfSetOfSimilarSequences()
        {
            List<BioSequence> selection = GetSetOfSimilarSequences();

            HashSet<string> result = new HashSet<string>();
            foreach (BioSequence sequence in selection)
            {
                result.Add(sequence.Identifier);
            }

            return result;
        }


        public static List<BioSequence> GetSetOfSimilarSequences()
        {
            TryUpdateSimilarity();

            int n = Graph.Population;
            int attempts = Randomizer.Random.Next(0, n-1);

            SequenceNode source = Graph.GetRandomStartingNode();
            List<SequenceNode> group = GetRandomSetAroundNode(source, attempts);

            List<BioSequence> result = new List<BioSequence>();
            foreach(SequenceNode node in group)
            {
                result.Add(node.Sequence);
            }

            CurrentSetSize = result.Count;

            return result;
        }

        public static string GetDebugString()
        {

            double graphSaturation = Math.Round(Graph.GetPercentageSaturation(), 0);
            int edges = Math.Min(NodeEdgeLimit, Graph.Population - 1);

            return $"Similarity Graph: Saturation: {graphSaturation}% (max. {edges} links per node) | Previous Set: {CurrentSetSize} seq(s) ";
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

        private static SequenceNode PickNodeFromListRandomly(List<SequenceNode> nodes)
        {
            int n = nodes.Count;
            int i = Randomizer.Random.Next(n);
            return nodes[i];
        }
    }
}
