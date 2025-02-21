using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    public class SimilarityLink
    {
        public SequenceNode NodeA;
        public SequenceNode NodeB;
        public double SimilarityScore;

        public SimilarityLink(SequenceNode a, SequenceNode b, double score)
        {
            NodeA = a;
            NodeB = b;
            SimilarityScore = score;
        }
    }
}
