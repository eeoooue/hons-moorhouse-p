using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    internal class SimilarityGuide
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



        public List<BioSequence> GetFuzzySet()
        {

        }

    }
}
