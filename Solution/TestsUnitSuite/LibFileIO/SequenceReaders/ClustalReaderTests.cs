using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibBioInfo;
using LibFileIO.AlignmentReaders;

namespace TestsUnitSuite.LibFileIO.SequenceReaders
{
    [TestClass]
    public class ClustalReaderTests
    {
        private ClustalReader ClustalReader = new ClustalReader();
        private SequenceEquality SequenceEquality = Harness.SequenceEquality;

        #region Testing identifiers can be collected

        [TestMethod]
        public void CanCollectIdentifiers()
        {
            List<string> contents = new List<string>()
            {
                "CLUSTAL 2.1 multiple sequence alignment",
                "",
                "",
                "1j46_A          ------MQDRVKRPMNAFIVWSRDQRRKMALENPRMRN--SEISKQLGYQWKMLTEAEKW",
                "2lef_A          --------MHIKKPLNAFMLYMKEMRANVVAESTLKES--AAINQILGRRWHALSREEQA",
                "1k99_A          MKKLKKHPDFPKKPLTPYFRFFMEKRAKYAKLHPEMSN--LDLTKILSKKYKELPEKKKM",
                "1aab_           ---GKGDPKKPRGKMSSYAFFVQTSREEHKKKHPDASVNFSEFSKKCSERWKTMSAKEKG",
                "                           :  :..:  :    * :     .        :.:  . ::: :.  ::",
                "",
                "1j46_A          PFFQEAQKLQAMHREKYPNYKYRP---RRKAKMLPK",
                "2lef_A          KYYELARKERQLHMQLYPGWSARDNYGKKKKRKREK",
                "1k99_A          KYIQDFQREKQEFERNLARFREDH---PDLIQNAKK",
                "1aab_           KFEDMAKADKARYEREMKTYIPPK---GE-------",
                "                 : :  :  :  . .    :       ",
                "",
            };

            List<string> expected = new List<string> { "1j46_A", "2lef_A", "1k99_A", "1aab_" };

            contents = ClustalReader.RemoveClustalHeader(contents);
            List<string> actual = ClustalReader.CollectUniqueIdentifiers(contents);

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        #endregion


        #region Testing multiple sequences can be read from lines of a FASTA file

        [TestMethod]
        public void CanReadAlignmentState()
        {
            List<string> contents = new List<string>()
            {
                "CLUSTAL 2.1 multiple sequence alignment",
                "",
                "",
                "1j46_A          ------MQDRVKRPMNAFIVWSRDQRRKMALENPRMRN--SEISKQLGYQWKMLTEAEKW",
                "2lef_A          --------MHIKKPLNAFMLYMKEMRANVVAESTLKES--AAINQILGRRWHALSREEQA",
                "1k99_A          MKKLKKHPDFPKKPLTPYFRFFMEKRAKYAKLHPEMSN--LDLTKILSKKYKELPEKKKM",
                "1aab_           ---GKGDPKKPRGKMSSYAFFVQTSREEHKKKHPDASVNFSEFSKKCSERWKTMSAKEKG",
                "                           :  :..:  :    * :     .        :.:  . ::: :.  ::",
                "",
                "1j46_A          PFFQEAQKLQAMHREKYPNYKYRP---RRKAKMLPK",
                "2lef_A          KYYELARKERQLHMQLYPGWSARDNYGKKKKRKREK",
                "1k99_A          KYIQDFQREKQEFERNLARFREDH---PDLIQNAKK",
                "1aab_           KFEDMAKADKARYEREMKTYIPPK---GE-------",
                "                 : :  :  :  . .    :       ",
                "",
            };

            List<BioSequence> expected = new List<BioSequence>()
            {
                new BioSequence("1j46_A", "------MQDRVKRPMNAFIVWSRDQRRKMALENPRMRN--SEISKQLGYQWKMLTEAEKWPFFQEAQKLQAMHREKYPNYKYRP---RRKAKMLPK"),
                new BioSequence("2lef_A", "--------MHIKKPLNAFMLYMKEMRANVVAESTLKES--AAINQILGRRWHALSREEQAKYYELARKERQLHMQLYPGWSARDNYGKKKKRKREK"),
                new BioSequence("1k99_A", "MKKLKKHPDFPKKPLTPYFRFFMEKRAKYAKLHPEMSN--LDLTKILSKKYKELPEKKKMKYIQDFQREKQEFERNLARFREDH---PDLIQNAKK"),
                new BioSequence("1aab_", "---GKGDPKKPRGKMSSYAFFVQTSREEHKKKHPDASVNFSEFSKKCSERWKTMSAKEKGKFEDMAKADKARYEREMKTYIPPK---GE-------"),
            };

            List<BioSequence> actual = ClustalReader.UnpackAlignment(contents);

            Assert.AreEqual(expected.Count, actual.Count);

            for(int i=0; i<expected.Count; i++)
            {
                SequenceEquality.AssertSequencesMatch(expected[i], actual[i]);
            }
        }


        #endregion
    }
}
