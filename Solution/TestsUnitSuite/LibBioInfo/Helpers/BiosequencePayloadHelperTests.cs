using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibBioInfo.Helpers
{
    [TestClass]
    public class BiosequencePayloadHelperTests
    {
        BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();

        [DataTestMethod]
        [DataRow("AC-GT-AC", 4, "AC-G", "T-AC")]
        [DataRow("----T-AC", 4, "----", "T-AC")]
        public void CanPartitionPayloadAtPosition(string payload, int position, string left, string right)
        {
            PayloadHelper.PartitionPayloadAtPosition(payload, position);
        }


        [DataTestMethod]
        [DataRow("AC-GT-AC", 1, 0)]
        [DataRow("AC-GT-AC", 4, 4)]
        [DataRow("A---CGT-AC", 2, 4)]
        public void CanGetPositionOfNthResidue(string payload, int n, int expected)
        {
            int actual = PayloadHelper.GetPositionOfNthResidue(payload, n);
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("AC-GT-AC", 6)]
        [DataRow("--AC-GT", 4)]
        public void CanCountResiduesInPayload(string payload, int expected)
        {
            int actual = PayloadHelper.CountResiduesInPayload(payload);
            Assert.AreEqual(expected, actual);
        }
    }
}
