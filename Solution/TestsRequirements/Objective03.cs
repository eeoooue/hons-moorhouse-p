using LibBioInfo;
using LibFileIO;
using MAli;
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
    public class Objective03
    {
        private MAliInterface MAli = new MAliInterface();
        private FileHelper FileHelper = new FileHelper();

        /// <summary>
        /// Can load a set of biological sequences from an appropriate bioinformatics file format. 
        /// </summary>
        [DataTestMethod]
        [DataRow("BB11001")]
        public void Req3x01(string inputPath)
        {
            string outputPath = "Req3x01";
            RunMAli($"-input {inputPath} -output {outputPath} -iterations 1");
        }

        /// <summary>
        /// Can output aligned sets of sequences using an appropriate bioinformatics file format. 
        /// </summary>
        [DataTestMethod]
        [DataRow("BB11001")]
        public void Req3x02(string inputPath)
        {
            string outputPath = "Req3x02";
            RunMAli($"-input {inputPath} -output {outputPath} -iterations 10");
            AssertAlignmentExists($"{outputPath}.faa");
        }

        /// <summary>
        /// Can load an existing alignment state from an appropriate bioinformatics file format, for iterative refinement.
        /// </summary>
        [DataTestMethod]
        [DataRow("clustalformat_BB11001.aln")]
        public void Req3x03(string inputPath)
        {
            string outputPath = "Req3x03";
            RunMAli($"-input {inputPath} -output {outputPath} -iterations 10 -refine");
            AssertAlignmentExists($"{outputPath}.faa");
        }

        /// <summary>
        /// Supports multiple bioinformatics file formats for outputting alignments.
        /// </summary>
        [DataTestMethod]
        [DataRow("BB11001", "fasta", "faa")]
        [DataRow("BB11001", "clustal", "aln")]
        public void Req3x04(string inputPath, string format, string extension)
        {
            string outputPath = "Req3x04";
            RunMAli($"-input {inputPath} -output {outputPath} -iterations 10 -format {format}");
            AssertAlignmentExists($"{outputPath}.{extension}");
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
