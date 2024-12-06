using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.Helpers
{
    public class BiosequencePayloadHelper
    {
        public Bioinformatics Bioinformatics = new Bioinformatics();

        public List<string> PartitionPayloadAtPosition(BioSequence a, int i)
        {
            return PartitionPayloadAtPosition(a.Payload, i);
        }

        public List<string> PartitionPayloadAtPosition(string payload, int i)
        {
            string left = payload.Substring(0, i);
            string right = payload.Substring(i);

            return new List<string> { left, right };
        }

        public int GetPositionOfNthResidue(BioSequence sequence, int n)
        {
            return GetPositionOfNthResidue(sequence.Payload, n);
        }

        public int GetPositionOfNthResidue(string payload, int n)
        {
            int total = 0;
            for (int i = 0; i < payload.Length; i++)
            {
                if (!Bioinformatics.IsGapChar(payload[i]))
                {
                    total++;
                    if (total == n)
                    {
                        return i;
                    }
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        public int CountResiduesInPayload(string payload)
        {
            int total = 0;
            foreach(char x in payload)
            {
                if (!Bioinformatics.IsGapChar(x))
                {
                    total++;
                }
            }

            return total;
        }
    }
}
