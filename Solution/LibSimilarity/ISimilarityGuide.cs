using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    public interface ISimilarityGuide
    {
        public void RecordSimilarity(BioSequence a, BioSequence b, double score);

        public List<BioSequence> GetSetOfSimilarSequences();
    }
}
