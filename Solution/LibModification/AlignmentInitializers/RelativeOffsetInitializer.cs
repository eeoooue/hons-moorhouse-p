using LibBioInfo;
using LibModification.Helpers;
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
            Alignment alignment = new Alignment(sequences);
            char[,] matrix = ConstructMatrixWithOffsetSequences(sequences);
            alignment.CharacterMatrix = matrix;

            return alignment;
        }


        public char[,] ConstructMatrixWithOffsetSequences(List<BioSequence> sequences)
        {
            List<string> payloads = CollectOffsetPayloads(sequences);

            int m = payloads.Count;
            int n = GetLongestPayload(payloads);

            return CharMatrixHelper.ConstructAlignmentStateFromStrings(m, n, payloads);
        }

        public int GetLongestPayload(List<string> payloads)
        {
            int result = 0;
            foreach(string payload in payloads)
            {
                result = Math.Max(payload.Length, result);
            }

            return result;
        }


        public List<string> CollectOffsetPayloads(List<BioSequence> sequences)
        {
            List<string> result = new List<string>();

            int width = GetMaximumSequenceWidth(sequences);
            int offsetLimit = Math.Max(20, width / 4);

            foreach (BioSequence sequence in sequences)
            {
                int offset = Randomizer.Random.Next(0, offsetLimit + 1);
                string payload = GetPayloadWithOffset(sequence.Residues, offset);
                result.Add(payload);
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
