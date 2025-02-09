using MAli;
using MAli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.MAli.Helpers
{
    [TestClass]
    public class ArgumentHelperTests
    {
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();

        #region Testing tagging

        [DataTestMethod]
        [DataRow("output", "one", "output_one.faa")]
        [DataRow("output_this_please", "two", "output_this_please_two.faa")]

        public void TestingIterationsAreUnpacked(string outputpath, string tag, string expected)
        {
            Dictionary<string, string?> table = new Dictionary<string, string?>();
            table["tag"] = tag;
            string actual = ArgumentHelper.BuildFullOutputFilename(outputpath, table);
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

            int actual = ArgumentHelper.UnpackSpecifiedIterations(table);
            Assert.AreEqual(counter, actual);
        }

        #endregion

        #region Testing Argument Unpacking

        [TestMethod]
        public void TestInterpretInputOutputArguments()
        {
            string[] args = { "-input", "input.txt", "-output", "output.txt" };
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);
            Assert.AreEqual("input.txt", table["input"]);
            Assert.AreEqual("output.txt", table["output"]);
        }

        [TestMethod]
        public void TestInterpretRandomArguments()
        {
            string[] args = { "-random", "abcabc", "-hello", "-unusual" };
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);
            Assert.AreEqual("abcabc", table["random"]);
            Assert.AreEqual(null, table["hello"]);
            Assert.AreEqual(null, table["unusual"]);
        }

        #endregion


        #region Testing Request Type Identification

        [TestMethod]
        public void CanIdentifyAlignRequest()
        {
            string[] args = { "-input", "input.txt", "-output", "output.txt" };
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);
            bool verdict1 = ArgumentHelper.IsAlignmentRequest(table);
            bool verdict2 = ArgumentHelper.IsHelpRequest(table);
            bool verdict4 = ArgumentHelper.IsAmbiguousRequest(table);
            Assert.AreEqual(true, verdict1);
            Assert.AreEqual(false, verdict2);
            Assert.AreEqual(false, verdict4);
        }


        [TestMethod]
        public void CanIdentifyHelpRequest()
        {
            string[] args = { "-help" };
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);
            bool verdict1 = ArgumentHelper.IsAlignmentRequest(table);
            bool verdict2 = ArgumentHelper.IsHelpRequest(table);
            bool verdict4 = ArgumentHelper.IsAmbiguousRequest(table);
            Assert.AreEqual(false, verdict1);
            Assert.AreEqual(true, verdict2);
            Assert.AreEqual(false, verdict4);
        }


        [TestMethod]
        public void CanIdentifyInfoRequest()
        {
            string[] args = { "-info" };
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);
            bool verdict1 = ArgumentHelper.IsAlignmentRequest(table);
            bool verdict2 = ArgumentHelper.IsHelpRequest(table);
            bool verdict4 = ArgumentHelper.IsAmbiguousRequest(table);
            Assert.AreEqual(false, verdict1);
            Assert.AreEqual(false, verdict2);
            Assert.AreEqual(false, verdict4);
        }


        [TestMethod]
        public void CanIdentifyAmbiguousRequest()
        {
            MAliInterface mAliInterface = new MAliInterface();
            string[] args = { "-info", "-help" };
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);
            bool verdict1 = ArgumentHelper.IsAlignmentRequest(table);
            bool verdict2 = ArgumentHelper.IsHelpRequest(table);
            bool verdict4 = ArgumentHelper.IsAmbiguousRequest(table);
            Assert.AreEqual(false, verdict1);
            Assert.AreEqual(false, verdict2);
            Assert.AreEqual(true, verdict4);
        }


        [TestMethod]
        public void CanIdentifyForeignCommands()
        {
            string[] args = { "-buypizza", "-playgolf" };
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);
            bool verdict1 = ArgumentHelper.IsAlignmentRequest(table);
            bool verdict2 = ArgumentHelper.IsHelpRequest(table);
            bool verdict4 = ArgumentHelper.IsAmbiguousRequest(table);
            bool verdict5 = ArgumentHelper.ContainsForeignCommands(table);
            Assert.AreEqual(false, verdict1);
            Assert.AreEqual(false, verdict2);
            Assert.AreEqual(false, verdict4);
            Assert.AreEqual(true, verdict5);
        }

        #endregion
    }
}
