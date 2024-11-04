using MAli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.MAli
{
    [TestClass]
    public class MAliInterfaceTests
    {

        #region Testing Argument Unpacking

        [TestMethod]
        public void TestInterpretInputOutputArguments()
        {
            MAliInterface mAliInterface = new MAliInterface();
            string[] args = { "-input", "input.txt", "-output", "output.txt" };
            Dictionary<string, string?> table = mAliInterface.InterpretArguments(args);
            Assert.AreEqual("input.txt", table["input"]);
            Assert.AreEqual("output.txt", table["output"]);
        }

        [TestMethod]
        public void TestInterpretRandomArguments()
        {
            MAliInterface mAliInterface = new MAliInterface();
            string[] args = { "-random", "abcabc", "-hello", "-unusual" };
            Dictionary<string, string?> table = mAliInterface.InterpretArguments(args);
            Assert.AreEqual("abcabc", table["random"]);
            Assert.AreEqual(null, table["hello"]);
            Assert.AreEqual(null, table["unusual"]);
        }

        #endregion


        #region Testing Request Type Identification

        [TestMethod]
        public void CanIdentifyAlignRequest()
        {
            MAliInterface mAliInterface = new MAliInterface();
            string[] args = { "-input", "input.txt", "-output", "output.txt" };
            Dictionary<string, string?> table = mAliInterface.InterpretArguments(args);
            bool verdict1 = mAliInterface.IsAlignmentRequest(table);
            bool verdict2 = mAliInterface.IsHelpRequest(table);
            bool verdict3 = mAliInterface.IsInfoRequest(table);
            Assert.AreEqual(true, verdict1);
            Assert.AreEqual(false, verdict2);
            Assert.AreEqual(false, verdict3);
        }


        [TestMethod]
        public void CanIdentifyHelpRequest()
        {
            MAliInterface mAliInterface = new MAliInterface();
            string[] args = { "-help" };
            Dictionary<string, string?> table = mAliInterface.InterpretArguments(args);
            bool verdict1 = mAliInterface.IsAlignmentRequest(table);
            bool verdict2 = mAliInterface.IsHelpRequest(table);
            bool verdict3 = mAliInterface.IsInfoRequest(table);
            Assert.AreEqual(false, verdict1);
            Assert.AreEqual(true, verdict2);
            Assert.AreEqual(false, verdict3);
        }


        [TestMethod]
        public void CanIdentifyInfoRequest()
        {
            MAliInterface mAliInterface = new MAliInterface();
            string[] args = { "-info" };
            Dictionary<string, string?> table = mAliInterface.InterpretArguments(args);
            bool verdict1 = mAliInterface.IsAlignmentRequest(table);
            bool verdict2 = mAliInterface.IsHelpRequest(table);
            bool verdict3 = mAliInterface.IsInfoRequest(table);
            Assert.AreEqual(false, verdict1);
            Assert.AreEqual(false, verdict2);
            Assert.AreEqual(true, verdict3);
        }




        #endregion


    }
}
