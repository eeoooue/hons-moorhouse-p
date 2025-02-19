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
    public class Objective01
    {
        private FileHelper FileHelper = new FileHelper();
        private MAliInterface MAli = new MAliInterface();

        /// <summary>
        /// Given sequences to align, produces a valid solution - independent of quality.
        /// </summary>
        [DataTestMethod]
        [DataRow("BB11001")]
        public void Req1x01(string inputPath)
        {
            string outputPath = "Req1x01";
            RunMAli($"-input {inputPath} -output {outputPath} -iterations 10");
            bool producedFasta = File.Exists($"{outputPath}.faa");
            Assert.IsTrue(producedFasta);
        }

        /// <summary>
        /// Employs a heuristic to estimate a number of iterations needed to align each set of sequences.
        /// </summary>
        [TestMethod]
        [DataRow("BB11001")]
        [DataRow("BB11002")]
        [DataRow("BB11003")]
        [Timeout(8000)]
        public void Req1x02(string inputPath)
        {
            string outputPath = "Req1x02";
            RunMAli($"-input {inputPath} -output {outputPath}");
            bool producedFasta = File.Exists($"{outputPath}.faa");
            Assert.IsTrue(producedFasta);
        }

        /// <summary>
        /// Aligns sets of 6 typical protein sequences within 10 seconds on a university machine.
        /// </summary>
        [TestMethod]
        [DataTestMethod]
        [DataRow("BB11002")]
        [Timeout(10000)]
        public void Req1x03(string inputPath)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
            Assert.IsTrue(sequences.Count >= 6);

            string outputPath = "Req1x03";
            RunMAli($"-input {inputPath} -output {outputPath}");
            bool producedFasta = File.Exists($"{outputPath}.faa");
            Assert.IsTrue(producedFasta);
        }

        private void RunMAli(string command)
        {
            string[] args = command.Split(' ');
            MAli.ProcessArguments(args);
        }
    }
}
