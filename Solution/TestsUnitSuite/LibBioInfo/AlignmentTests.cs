using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite;
using TestsUnitSuite.HarnessTools;

namespace TestsUnitSuite.LibBioInfo
{
    [TestClass]
    public class AlignmentTests
    {

        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;


        #region Basic data representation tests

        [TestMethod]
        public void CanRetrieveOriginalSequences()
        {
            List<BioSequence> original = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            Alignment alignment = new Alignment(original);
            List<BioSequence> aligned = alignment.GetAlignedSequences();

            for(int i=0; i< original.Count; i++)
            {
                SequenceConservation.AssertDataIsConserved(original[i], aligned[i]);
            }
        }

        #endregion
    }
}
