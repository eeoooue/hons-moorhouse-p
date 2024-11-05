using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.HarnessTools
{
    internal class AlignmentConservation
    {
        SequenceConservation SequenceConservation = Harness.SequenceConservation;

        public void AssertAlignmentsAreConserved(Alignment a, Alignment b)
        {
            List<BioSequence> sequences_a = a.GetAlignedSequences();
            List<BioSequence> sequences_b = b.GetAlignedSequences();
            SequenceConservation.AssertDataIsConserved(sequences_a, sequences_b);
        }
    }
}
