using LibBioInfo;
using LibFileIO;
using MAli;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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




        #region Testing high-level behaviour

        [DataTestMethod]
        [Timeout(5000)]
        [DataRow("BB11001", "testoutput1.faa")]
        [DataRow("BB11002", "testoutput2.faa")]
        [DataRow("BB11003", "testoutput3.faa")]

        public void ProducesValidAlignment(string inputFile, string outputFile)
        {
            Dictionary<string, string?> table = new Dictionary<string, string?>();
            table["iterations"] = "3";

            MAliFacade.PerformAlignment(inputFile, outputFile, table);

            List<BioSequence> original = FileHelper.ReadSequencesFrom(inputFile);
            List<BioSequence> aligned = FileHelper.ReadSequencesFrom(outputFile);

            Alignment leftJustified = new Alignment(original);
            Alignment alignment = new Alignment(aligned);
            Assert.IsTrue(alignment.SequencesCanBeAligned());

            SequenceConservation.AssertDataIsConserved(original, aligned);
            AlignmentConservation.AssertAlignmentsAreConserved(leftJustified, alignment);
        }


        [DataTestMethod]
        [Timeout(5000)]
        [DataRow("BB11001", "testoutput1.faa", "1756")]
        [DataRow("BB11002", "testoutput2.faa", "81")]
        [DataRow("BB11003", "testoutput3.faa", "1")]
        public void ProducesIdenticalAlignmentsWhenSeeded(string inputFile, string outputFile, string seed)
        {
            Dictionary<string, string?> table = new Dictionary<string, string?>();
            table["iterations"] = "3";

            string filename_a = $"a_{outputFile}";
            string filename_b = $"b_{outputFile}";

            MAliFacade.SetSeed(seed);
            MAliFacade.PerformAlignment(inputFile, filename_a, table);
            List<BioSequence> alignedA = FileHelper.ReadSequencesFrom(filename_a);
            Alignment alignmentA = new Alignment(alignedA);

            MAliFacade.SetSeed(seed);
            MAliFacade.PerformAlignment(inputFile, filename_b, table);
            List<BioSequence> alignedB = FileHelper.ReadSequencesFrom(filename_b);
            Alignment alignmentB = new Alignment(alignedB);

            AlignmentConservation.AssertAlignmentsAreConserved(alignmentA, alignmentB);
            AlignmentEquality.AssertAlignmentsMatch(alignmentA, alignmentB);
        }

        #endregion


        #region Testing tagging

        [DataTestMethod]
        [DataRow("output", "one", "output_one.faa")]
        [DataRow("output_this_please", "two", "output_this_please_two.faa")]

        public void TestingIterationsAreUnpacked(string outputpath, string tag, string expected)
        {
            Dictionary<string, string?> table = new Dictionary<string, string?>();
            table["tag"] = tag;
            string actual = MAliFacade.BuildFullOutputFilename(outputpath, table);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Testing iterations are unpacked correctly 

        [DataTestMethod]
        [DataRow(1756)]
        [DataRow(81)]
        [DataRow(1)]

        public void TestingIterationsAreUnpacked(int counter)
        {
            Dictionary<string, string?> table = new Dictionary<string, string?>();
            table["iterations"] = counter.ToString();

            int actual = MAliFacade.UnpackSpecifiedIterations(table);
            Assert.AreEqual(counter, actual);
        }

        #endregion
    }
}
