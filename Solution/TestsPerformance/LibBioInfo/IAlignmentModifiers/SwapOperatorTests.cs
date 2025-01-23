using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibFileIO;
using LibBioInfo.LegacyAlignmentModifiers;

namespace TestsPerformance.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class SwapOperatorTests
    {
        SwapOperator Modifier = new SwapOperator();
        FileHelper FileHelper = new FileHelper();

        #region Is Time Efficient

        [DataTestMethod]
        [DataRow("BB11003", 8)]
        [DataRow("BB11003", 16)]
        [DataRow("BB11003", 32)]
        [DataRow("BB11003", 64)]
        [DataRow("BB11003", 128)]
        [Timeout(500)]
        public void CanModifyBBSAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < times; i++)
            {
                Modifier.ModifyAlignment(alignment);
            }
        }

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [DataRow("1ggxA_1h4uA", 64)]
        [DataRow("1ggxA_1h4uA", 128)]
        [Timeout(500)]
        public void CanModifyPREFABAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < times; i++)
            {
                Modifier.ModifyAlignment(alignment);
            }
        }

        #endregion
    }
}
