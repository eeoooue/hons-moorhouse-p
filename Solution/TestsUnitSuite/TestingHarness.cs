using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite.Harness;

namespace TestsUnitSuite
{
    internal class TestingHarness
    {
        public ExampleSequences ExampleSequences = new ExampleSequences();


        public void AssertSequencesAreEqual(List<BioSequence> expected, List<BioSequence> actual)
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
        }
    }
}
