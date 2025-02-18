using MAli;
using MAli.AlignmentEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsRequirements
{
    [TestClass]
    public class Objective01
    {
        /// <summary>
        /// Given sequences to align, produces a valid solution - independent of quality.
        /// </summary>
        [DataTestMethod]
        [DataRow("", "")]
        public void Req1x01(string inputPath, string outputPath)
        {
            throw new NotImplementedException();

            MAliInterface mali = new MAliInterface();

            string command = $"-input {inputPath} -output {outputPath}";
            string[] args = command.Split(' ');
            mali.ProcessArguments(args);

            bool alignmentProduced = File.Exists(outputPath);

            Assert.IsTrue(alignmentProduced);
        }

        /// <summary>
        /// Employs a heuristic to estimate a number of iterations needed to align each set of sequences.
        /// </summary>
        [TestMethod]
        public void Req1x02()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Aligns sets of 6 typical protein sequences within 10 seconds on a university machine.
        /// </summary>
        [DataTestMethod]
        [DataRow("", "")]
        [Timeout(10000)]
        public void Req1x03(string inputPath, string outputPath)
        {
            throw new NotImplementedException();

            MAliInterface mali = new MAliInterface();

            string command = $"-input {inputPath} -output {outputPath}";
            string[] args = command.Split(' ');
            mali.ProcessArguments(args);

            bool alignmentProduced = File.Exists(outputPath);

            Assert.IsTrue(alignmentProduced);
        }
    }
}
