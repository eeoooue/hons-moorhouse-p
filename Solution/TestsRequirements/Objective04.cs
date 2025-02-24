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
        [DoNotParallelize]
        [DataRow("1a0cA_1ubpC")]
        public void Req4x01(string inputPath)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
            Assert.IsTrue(sequences.Count >= 30);

            string outputPath = "Req4x01";
            RunMAli($"-input {inputPath} -output {outputPath} -iterations 10");
            AssertAlignmentExists($"{outputPath}.faa");
        }

        /// <summary>
        /// Can align a structural benchmarking test case of at least 20 sequences within 60 seconds.
        /// </summary>
        [TestMethod]
        [DoNotParallelize]
        [DataRow("1a0cA_1ubpC")]
        [Timeout(60000)]
        public void Req4x02(string inputPath)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
            Assert.IsTrue(sequences.Count >= 20);

            string outputPath = "Req4x02";
            RunMAli($"-input {inputPath} -output {outputPath}");
            AssertAlignmentExists($"{outputPath}.faa");
        }

        /// <summary>
        /// Can specify a randomness seed to support reproduction of results under the same settings.
        /// </summary>
        [DataTestMethod]
        [DoNotParallelize]
        [DataRow("BB11001", 17)]
        [DataRow("BB11002", 56)]
        public void Req4x03(string inputPath, int seed)
        {
            string outputPath1 = $"Req4x03_1_seed{seed}";
            RunMAli($"-input {inputPath} -output {outputPath1} -seed {seed} -iterations 10");
            Alignment alignment1 = ReadAlignmentFrom($"{outputPath1}.faa");

            string outputPath2 = $"Req4x03_2_seed{seed}";
            RunMAli($"-input {inputPath} -output {outputPath2} -seed {seed} -iterations 10");
            Alignment alignment2 = ReadAlignmentFrom($"{outputPath2}.faa");

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(alignment1, alignment2);
            Assert.IsTrue(alignmentsMatch);
        }

        private void AssertAlignmentExists(string outputPath)
        {
            Alignment alignment = ReadAlignmentFrom(outputPath);
            Assert.IsTrue(alignment is Alignment);
        }

        private Alignment ReadAlignmentFrom(string outputPath)
        {
            bool fileExists = File.Exists(outputPath);
            Assert.IsTrue(fileExists);
            return FileHelper.ReadAlignmentFrom(outputPath);
        }

        private void RunMAli(string command)
        {
            string[] args = command.Split(' ');
            MAli.ProcessArguments(args);
        }
    }
}
