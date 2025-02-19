using MAli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsRequirements
{
    [TestClass]
    public class Objective06
    {
        private MAliInterface MAli = new MAliInterface();

        /// <summary>
        /// Supports batch alignment of a series of sets of sequences from a directory.
        /// </summary>
        [DataTestMethod]
        [DataRow("batchin", "batchout")]
        public void Req6x01(string inputDirPath, string outputDirPath)
        {
            Directory.CreateDirectory(outputDirPath);
            RunMAli($"-input {inputDirPath} -output {outputDirPath} -iterations 1 -batch");

            string[] inputs = Directory.GetFiles(inputDirPath);
            Assert.IsTrue(inputs.Length > 1);

            string[] outputs = Directory.GetFiles(outputDirPath);
            Assert.IsTrue(inputs.Length == outputs.Length);
        }

        /// <summary>
        /// Interface displays progress on the current alignment task - in terms of time or iterations.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Req6x02()
        {
            throw new NotImplementedException("Cannot automate tests for this requirement.");
        }

        private void RunMAli(string command)
        {
            string[] args = command.Split(' ');
            MAli.ProcessArguments(args);
        }
    }
}
