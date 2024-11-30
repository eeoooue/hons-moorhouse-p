using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsHarness.Tools
{
    public class SequenceEquality
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
            Assert.IsTrue(SequencesMatch(expected, actual));
        }

        public bool SequencesMatch(BioSequence expected, BioSequence actual)
        {
            bool identifiersMatch = expected.Identifier == actual.Identifier;
            bool payloadsMatch = expected.Payload == actual.Payload;
            bool residuesMatch = expected.Residues == actual.Residues;
            return identifiersMatch && payloadsMatch && residuesMatch;
        }
    }
}
