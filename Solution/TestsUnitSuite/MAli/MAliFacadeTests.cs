using LibBioInfo;
using LibFileIO;
using MAli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite.HarnessTools;

namespace TestsUnitSuite.MAli
{
    [TestClass]
    public class MAliFacadeTests
    {
        FileHelper FileHelper = new FileHelper();
        MAliFacade MAliFacade = new MAliFacade();
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        [DataTestMethod]
        [DataRow("BB11001", "testoutput1.faa")]
        [DataRow("BB11002", "testoutput2.faa")]
        [DataRow("BB11003", "testoutput3.faa")]

        public void ProducesValidAlignment(string inputFile, string outputFile)
        {
            MAliFacade.PerformAlignment(inputFile, outputFile);

            List<BioSequence> original = FileHelper.ReadSequencesFrom(inputFile);
            List<BioSequence> aligned = FileHelper.ReadSequencesFrom(outputFile);

            Alignment leftJustified = new Alignment(original);
            Alignment alignment = new Alignment(aligned);
            Assert.IsTrue(alignment.SequencesCanBeAligned());

            SequenceConservation.AssertDataIsConserved(original, aligned);
            AlignmentConservation.AssertAlignmentsAreConserved(leftJustified, alignment);
        }

    }
}
