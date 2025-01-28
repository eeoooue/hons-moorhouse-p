using LibBioInfo;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibFileIO
{

    [TestClass]
    public class FileHelperTests
    {
        private FileHelper FileHelper = new FileHelper();

        [DataTestMethod]
        [DataRow("BB11001")]
        [DataRow("BB11002")]
        [DataRow("BB11003")]
        [DataRow("1axkA_2nlrA")]
        [DataRow("1eagA_1smrA")]
        [DataRow("1ggxA_1h4uA")]
        [DataRow("clustalformat_BB11001.aln")]
        public void CanReadSequencesFrom(string filename)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
        }
    }
}
