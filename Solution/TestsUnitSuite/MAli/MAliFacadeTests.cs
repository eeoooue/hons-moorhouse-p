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
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;

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


        [DataTestMethod]
        [DataRow("BB11001", "testoutput1.faa", "1756")]
        [DataRow("BB11002", "testoutput2.faa", "81")]
        [DataRow("BB11003", "testoutput3.faa", "0")]
        public void ProducesIdenticalAlignmentsWhenSeeded(string inputFile, string outputFile, string seed)
        {
            string filename_a = $"a_{outputFile}";
            string filename_b = $"b_{outputFile}";

            MAliFacade.SetSeed(seed);
            MAliFacade.PerformAlignment(inputFile, filename_a);
            List<BioSequence> alignedA = FileHelper.ReadSequencesFrom(filename_a);
            Alignment alignmentA = new Alignment(alignedA);

            MAliFacade.SetSeed(seed);
            MAliFacade.PerformAlignment(inputFile, filename_b);
            List<BioSequence> alignedB = FileHelper.ReadSequencesFrom(filename_b);
            Alignment alignmentB = new Alignment(alignedB);

            AlignmentConservation.AssertAlignmentsAreConserved(alignmentA, alignmentB);
            AlignmentEquality.AssertAlignmentsMatch(alignmentA, alignmentB);
        }
    }
}
