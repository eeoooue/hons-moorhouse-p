using LibBioInfo;
using LibFileIO;
using MAli;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TestsHarness;
using TestsHarness.Tools;

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

        #region Testing high-level behaviour

        [DataTestMethod]
        [Timeout(5000)]
        [DataRow("BB11001", "testoutput1")]

        public void ProducesValidAlignment(string inputFile, string outputFile)
        {
            AlignmentRequest instructions = new AlignmentRequest();
            instructions.InputPath = inputFile;
            instructions.OutputPath = outputFile;
            instructions.IterationsLimit = 3;

            MAliFacade.PerformAlignment(instructions);

            List<BioSequence> original = FileHelper.ReadSequencesFrom(inputFile);
            List<BioSequence> aligned = FileHelper.ReadSequencesFrom($"{outputFile}.faa");

            Alignment leftJustified = new Alignment(original);
            Alignment alignment = new Alignment(aligned);
            Assert.IsTrue(alignment.SequencesCanBeAligned());

            SequenceConservation.AssertDataIsConserved(original, aligned);
            AlignmentConservation.AssertAlignmentsAreConserved(leftJustified, alignment);
        }


        [DataTestMethod]
        [Timeout(5000)]
        [DataRow("BB11001", "testoutput1", "1756")]

        public void ProducesIdenticalAlignmentsWhenSeeded(string inputFile, string outputFile, string seed)
        {
            string filename_a = $"a_{outputFile}";
            string filename_b = $"b_{outputFile}";

            AlignmentRequest instructionsA = new AlignmentRequest();
            instructionsA.InputPath = inputFile;
            instructionsA.OutputPath = filename_a;
            instructionsA.IterationsLimit = 3;

            MAliFacade.SetSeed(seed);
            MAliFacade.PerformAlignment(instructionsA);
            List<BioSequence> alignedA = FileHelper.ReadSequencesFrom($"{filename_a}.faa");
            Alignment alignmentA = new Alignment(alignedA);

            AlignmentRequest instructionsB = new AlignmentRequest();
            instructionsA.InputPath = inputFile;
            instructionsA.OutputPath = filename_b;
            instructionsA.IterationsLimit = 3;

            MAliFacade.SetSeed(seed);
            MAliFacade.PerformAlignment(instructionsB);
            List<BioSequence> alignedB = FileHelper.ReadSequencesFrom($"{filename_b}.faa");
            Alignment alignmentB = new Alignment(alignedB);

            AlignmentConservation.AssertAlignmentsAreConserved(alignmentA, alignmentB);
            AlignmentEquality.AssertAlignmentsMatch(alignmentA, alignmentB);
        }

        #endregion
    }
}
