using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.HarnessTools
{
    internal class SequenceEquality
    {
        public void AssertSequencesMatch(List<BioSequence> expected, List<BioSequence> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                AssertSequencesMatch(expected[i], actual[i]);
            }
        }

        public void AssertSequencesMatch(BioSequence expected, BioSequence actual)
        {
            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.Payload, actual.Payload);
            Assert.AreEqual(expected.Residues, actual.Residues);
        }
    }
}
