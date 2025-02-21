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

        public BioSequence Sequence;

        public string Identifier { get { return Sequence.Identifier; } }

        public SequenceNode(BioSequence sequence)
        {
            Sequence = sequence;
        }

        public void AddConnection(SimilarityLink link)
        {
            Connections.Add(link);
        }
    }
}
