using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.HarnessTools
{
    internal class AlignmentEquality
    {

        SequenceEquality SequenceEquality = Harness.SequenceEquality;

        public void AssertAlignmentsMatch(Alignment a, Alignment b)
        {
            bool verdict = AlignmentsMatch(a, b);
            Assert.IsTrue(verdict);
        }


        public bool AlignmentsMatch(Alignment a, Alignment b)
        {
            List<BioSequence> sequences_a = a.GetAlignedSequences();
            List<BioSequence> sequences_b = b.GetAlignedSequences();

            if (sequences_a.Count != sequences_b.Count)
            {
                return false;
            }

            int n = sequences_a.Count;
            for (int i = 0; i < n; i++)
            {
                bool pairMatch = SequenceEquality.SequencesMatch(sequences_a[i], sequences_b[i]);
                if (!pairMatch)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
