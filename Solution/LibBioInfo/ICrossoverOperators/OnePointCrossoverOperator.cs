using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ICrossoverOperators
{
    public class OnePointCrossoverOperator : ICrossoverOperator
    {
        private Bioinformatics Bioinformatics = new Bioinformatics();

        // based on One-Point Crossover operation described in SAGA (Notredame & Higgins, 1996)

        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {

            // the alignment A is cut at a randomly chosen position

            // complement segments are taken from B such that the resulting child is a valid state

            int i = Randomizer.Random.Next(a.Width);
            return CrossoverAtPosition(a, b, i);
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            List<BioSequence> sequencesA = a.GetAlignedSequences();
            List<BioSequence> sequencesB = b.GetAlignedSequences();
            Alignment ab = CrossoverSequencesAtPosition(sequencesA, sequencesB, position);
            Alignment ba = CrossoverSequencesAtPosition(sequencesB, sequencesA, position);

            return new List<Alignment>() { ab, ba };
        }


        public Alignment CrossoverSequencesAtPosition(List<BioSequence> leftSeqs, List<BioSequence> rightSeqs, int position)
        {
            List<BioSequence> sequences = new List<BioSequence>();

            for(int i=0; i<leftSeqs.Count; i++)
            {
                BioSequence leftSeq = leftSeqs[i];
                BioSequence rightSeq = rightSeqs[i];
                BioSequence crossed = CrossoverSequenceAtPosition(leftSeq, rightSeq, position);
                sequences.Add(crossed);
            }

            return new Alignment(sequences);
        }

        public BioSequence CrossoverSequenceAtPosition(BioSequence left, BioSequence right, int position)
        {
            string prefix = GetPayloadUntilPosition(left, position);
            string suffix = ExtractComplementForPrefix(prefix, right);
            string payload = $"{prefix}{suffix}";
            return new BioSequence(left.Identifier, payload);
        }

        public string GetPayloadUntilPosition(BioSequence sequence, int i)
        {
            string payload = sequence.Payload;
            return payload.Substring(0, i);
        }

        public string ExtractComplementForPrefix(string prefix, BioSequence source)
        {
            int residuesToSkip = CountResidues(prefix);
            return CropFirstNResidues(source.Payload, residuesToSkip);
        }

        public string CropFirstNResidues(string payload, int count)
        {
            int n = payload.Length;

            for(int i=0; i<n; i++)
            {
                if (count == 0)
                {
                    return payload.Substring(i);
                }

                char x = payload[i];
                if (!Bioinformatics.IsGapChar(x))
                {
                    count--;
                }
            }

            return "";
        }

        public int CountResidues(string content)
        {
            int counter = 0;
            foreach(char x in content)
            {
                if (!Bioinformatics.IsGapChar(x))
                {
                    counter += 1;
                }
            }

            return counter;
        }
    }
}
