using LibBioInfo;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibBioInfo
{
    [TestClass]
    public class BioSequenceTests
    {
        private FileHelper FileHelper = new FileHelper();

        #region Can evaluate real sequences

        [DataTestMethod]
        [DataRow("BB11001")]
        [DataRow("BB11002")]
        [DataRow("BB11003")]
        [DataRow("1axkA_2nlrA")]
        [DataRow("1eagA_1smrA")]
        [DataRow("1ggxA_1h4uA")]
        public void RecognisesRealProteinSequences(string filename)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);

            bool purity = true;
            foreach(BioSequence sequence in sequences)
            {
                bool verdict = sequence.IsProtein();
                if (verdict == false)
                {
                    purity = false;
                    Console.WriteLine($"{sequence.Identifier} was impure");
                }
            }

            Assert.IsTrue(purity);

        }

        #endregion

        #region Testing basic data representation

        [TestMethod]
        public void GaplessSequenceDataIsConserved()
        {
            string identifier = "exampleSequence";
            string payload = "ACGTACGTACGT";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.AreEqual(identifier, sequence.Identifier);
            Assert.AreEqual(payload, sequence.Payload);
            Assert.AreEqual(payload, sequence.Residues);
        }

        [TestMethod]
        public void GappedSequenceDataIsConserved()
        {
            string identifier = "exampleSequence";
            string payload = "ACGT-ACGT--ACGT";
            string residues = "ACGTACGTACGT";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.AreEqual(identifier, sequence.Identifier);
            Assert.AreEqual(payload, sequence.Payload);
            Assert.AreEqual(residues, sequence.Residues);
        }

        #endregion


        #region Testing sequence validation - nucleic

        [TestMethod]
        public void CanIdentifyNucleicSequence()
        {
            string identifier = "exampleSequence";
            string payload = "ACGTA-CGT--ACGT";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.IsTrue(sequence.IsNucleic());
        }

        public void CanIdentifyNonNucleicSequence()
        {
            string identifier = "exampleSequence";
            string payload = "XXX-XXX";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.IsFalse(sequence.IsNucleic());
        }

        #endregion


        #region Testing sequence validation - protein

        [TestMethod]
        public void CanIdentifyProteinSequence()
        {
            string identifier = "exampleSequence";
            string payload = "KKHPDFPKKPL--TPYFRFFMEKRAKYA--KLHPEMSNLDLTKI";


            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.IsTrue(sequence.IsProtein());
        }

        [TestMethod]
        public void CanIdentifyNonProteinSequence()
        {
            string identifier = "exampleSequence";
            string payload = "AC?!?-CGT--ACGT";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.IsFalse(sequence.IsProtein());
        }

        #endregion


        #region Testing payload validation

        [TestMethod]
        public void CanIdentifyInvalidPayload()
        {
            string identifier = "exampleSequence";
            string payload = "AGCT???--XYXXX--XXX";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.IsFalse(sequence.PayloadIsValid());
        }

        #endregion


    }
}
