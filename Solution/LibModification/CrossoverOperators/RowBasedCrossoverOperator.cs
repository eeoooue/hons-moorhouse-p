using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.CrossoverOperators
{
    public class RowBasedCrossoverOperator : ICrossoverOperator
    {
        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            bool[] mapping1 = ConstructColumnMapping(a.Height);
            Alignment child1 = ProduceChildUsingMapping(a, b, mapping1);

            bool[] mapping2 = InvertMapping(mapping1);
            Alignment child2 = ProduceChildUsingMapping(a, b, mapping2);

            return new List<Alignment> { child1, child2 };
        }

        public Alignment ProduceChildUsingMapping(Alignment a, Alignment b, bool[] mapping)
        {
            List<BioSequence> sequencesA = a.GetAlignedSequences();
            List<BioSequence> sequencesB = b.GetAlignedSequences();

            List<BioSequence> sequences = new List<BioSequence>();
            for (int i = 0; i < a.Height; i++)
            {
                if (mapping[i])
                {
                    sequences.Add(sequencesA[i]);
                }
                else
                {
                    sequences.Add(sequencesB[i]);
                }
            }

            return new Alignment(sequences, true);
        }

        public bool[] ConstructColumnMapping(int n)
        {
            bool[] result = new bool[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = Randomizer.CoinFlip();
            }

            return result;
        }

        public bool[] InvertMapping(bool[] mapping)
        {
            int n = mapping.Length;
            bool[] result = new bool[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = !mapping[i];
            }

            return result;
        }
    }
}
