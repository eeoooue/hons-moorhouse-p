using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsHarness.Tools
{
    public class SequenceConservation
    {

        public void AssertDataIsConserved(List<BioSequence> expected, List<BioSequence> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                AssertDataIsConserved(expected[i], actual[i]);
            }
        }

        public void AssertDataIsConserved(BioSequence expected, BioSequence actual)
        {
            AssertIdentifiersMatch(expected, actual);
            AssertResidueSequencesMatch(expected, actual);
        }

        public void AssertIdentifiersMatch(List<BioSequence> expected, List<BioSequence> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                AssertIdentifiersMatch(expected[i], actual[i]);
            }
        }

        public void AssertIdentifiersMatch(BioSequence expected, BioSequence actual)
        {
            Assert.AreEqual(expected.Identifier, actual.Identifier);
        }

        public void AssertResidueSequencesMatch(List<BioSequence> expected, List<BioSequence> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                AssertResidueSequencesMatch(expected[i], actual[i]);
            }
        }

        public void AssertResidueSequencesMatch(BioSequence expected, BioSequence actual)
        {
            Assert.AreEqual(expected.Residues, actual.Residues);
        }
    }
}
