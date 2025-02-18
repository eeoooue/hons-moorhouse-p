using LibBioInfo;
using LibFileIO;
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
        private FileHelper FileHelper = new FileHelper();
        private ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        /// <summary>
        /// Can load a set of biological sequences from an appropriate bioinformatics file format. 
        /// </summary>
        [DataTestMethod]
        [DataRow("")]
        public void Req3x01(string filepath)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filepath);
        }

        /// <summary>
        /// Can output aligned sets of sequences using an appropriate bioinformatics file format. 
        /// </summary>
        [DataTestMethod]
        [DataRow("Req3x02_output")]
        public void Req3x02(string filepath)
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            FileHelper.WriteAlignmentTo(alignment, filepath);
            File.Exists(filepath);
        }

        /// <summary>
        /// Can load an existing alignment state from an appropriate bioinformatics file format, for iterative refinement.
        /// </summary>
        [DataTestMethod]
        [DataRow("")]
        public void Req3x03(string filepath)
        {
            Alignment alignment = FileHelper.ReadAlignmentFrom(filepath);
        }

        /// <summary>
        /// Supports multiple bioinformatics file formats for outputting alignments.
        /// </summary>
        [DataTestMethod]
        [DataRow("", "")]
        public void Req3x04(string fastaPath, string clustalPath)
        {
            throw new NotImplementedException();
        }
    }
}
