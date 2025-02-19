using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class AlignmentCore
    {
        public List<BioSequence> Sequences;

        public AlignmentCore(List<BioSequence> sequences)
        {
            Sequences = sequences;
        }

        public int GetLongestPayloadLength(List<BioSequence> sequences)
        {
            int result = 0;
            foreach (BioSequence sequence in sequences)
            {
                result = Math.Max(sequence.Payload.Length, result);
            }

            return result;
        }

        public bool ContainsNucleicsOnly()
        {
            foreach (BioSequence sequence in Sequences)
            {
                if (!sequence.IsNucleic())
                {
                    return false;
                }
            }

            return true;
        }

        public bool ContainsProteinsOnly()
        {
            foreach (BioSequence sequence in Sequences)
            {
                if (!sequence.IsProtein())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
