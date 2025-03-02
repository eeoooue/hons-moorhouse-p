using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentInitializers
{
    public class RelativeOffsetInitializer : IAlignmentInitializer
    {
        public Alignment CreateInitialAlignment(List<BioSequence> sequences)
        {
            List<BioSequence> offsetSequences = GetSequencesWithOffsetPayloads(sequences);
            return new Alignment(offsetSequences, true);
        }

        public List<BioSequence> GetSequencesWithOffsetPayloads(List<BioSequence> sequences)
        {
            List<BioSequence> result = new List<BioSequence>();
                
            int width = GetMaximumSequenceWidth(sequences);
            int offsetLimit = Math.Max(20, width / 4);

            foreach (BioSequence sequence in sequences)
            {
                int offset = Randomizer.Random.Next(0, offsetLimit + 1);
                string payload = GetPayloadWithOffset(sequence.Residues, offset);
                BioSequence offsetSequence = new BioSequence(sequence.Identifier, payload);
                result.Add(offsetSequence);
            }

            return result;
        }

        public int GetMaximumSequenceWidth(List<BioSequence> sequences)
        {
            int result = 0;
            foreach(BioSequence sequence in sequences)
            {
                int length = sequence.Residues.Length;
                result = Math.Max(result, length);
            }

            return result;
        }

        public string GetPayloadWithOffset(string payload, int offset)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<offset; i++)
            {
                sb.Append('-');
            }
            sb.Append(payload);

            return sb.ToString();
        }
    }
}
