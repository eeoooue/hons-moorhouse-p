using LibAlignment;
using LibAlignment.Aligners.SingleState;
using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibFileIO;
using LibScoring;
using LibScoring.FitnessFunctions;
using MAli;
using MAli.AlignmentEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsRequirements
{
    [TestClass]
    public class Objective04
    {
        private FileHelper FileHelper = new FileHelper();
        private MAliInterface MAli = new MAliInterface();
        private AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;

        /// <summary>
        /// Can align structural benchmarking test cases containing as many as 30 sequences.
        /// </summary>
        [DataTestMethod]
        [DataRow("", "")]
        public void Req4x01(string inputPath, string outputPath)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
            Assert.IsTrue(sequences.Count >= 30);

            string command = "";
            string[] args = command.Split(' ');
            MAli.ProcessArguments(args);

            bool producedAlignment = File.Exists(outputPath);
            Assert.IsTrue(producedAlignment);

            Alignment alignment = FileHelper.ReadAlignmentFrom(outputPath);
        }

        /// <summary>
        /// Can align a structural benchmarking test case of at least 20 sequences within 60 seconds.
        /// </summary>
        [TestMethod]
        [DataRow("", "")]
        [Timeout(60000)]
        public void Req4x02(string inputPath, string outputPath)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
            Assert.IsTrue(sequences.Count >= 20);

            string command = "";
            string[] args = command.Split(' ');
            MAli.ProcessArguments(args);

            bool producedAlignment = File.Exists(outputPath);
            Assert.IsTrue(producedAlignment);

            Alignment alignment = FileHelper.ReadAlignmentFrom(outputPath);
        }

        /// <summary>
        /// Can specify a randomness seed to support reproduction of results under the same settings.
        /// </summary>
        [DataTestMethod]
        [DataRow("", "", 17)]
        [DataRow("", "", 56)]
        public void Req4x03(string outputPath1, string outputPath2, int seed)
        {
            string command1 = "";
            string[] args1 = command1.Split(' ');
            MAli.ProcessArguments(args1);
            Alignment alignment1 = FileHelper.ReadAlignmentFrom(outputPath1);

            string command2 = "";
            string[] args2 = command2.Split(' ');
            MAli.ProcessArguments(args2);
            Alignment alignment2 = FileHelper.ReadAlignmentFrom(outputPath2);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(alignment1, alignment2);
            Assert.IsTrue(alignmentsMatch);
        }
    }
}
