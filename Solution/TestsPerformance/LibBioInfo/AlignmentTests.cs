using LibBioInfo.LegacyAlignmentModifiers;
using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibFileIO;

namespace TestsPerformance.LibBioInfo
{

    [TestClass]
    public class AlignmentTests
    {
        private FileHelper FileHelper = new FileHelper();

        #region Timing alignment duplication

        [DataTestMethod]
        [DataRow("BB11003", 8)]
        [DataRow("BB11003", 16)]
        [DataRow("BB11003", 32)]
        [DataRow("BB11003", 64)]
        [DataRow("BB11003", 128)]
        [Timeout(500)]
        public void CanDuplicateBBSAlignmentsEfficiently(string filename, int duplicates)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < duplicates; i++)
            {
                Alignment copy = alignment.GetCopy();
                result.Add(copy);
            }
        }

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [DataRow("1ggxA_1h4uA", 64)]
        [DataRow("1ggxA_1h4uA", 128)]
        [Timeout(500)]
        public void CanDuplicatePREFABAlignmentsEfficiently(string filename, int duplicates)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < duplicates; i++)
            {
                Alignment copy = alignment.GetCopy();
                result.Add(copy);
            }
        }

        #endregion


    }
}
