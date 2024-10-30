using LibBioInfo;
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
            string payload = "ACXXA-CGT--ACGT";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.IsFalse(sequence.IsProtein());
        }

        #endregion


        #region Testing payload validation

        [TestMethod]
        public void CanIdentifyInvalidPayload()
        {
            string identifier = "exampleSequence";
            string payload = "AGCTXXX--XYXXX--XXX";

            BioSequence sequence = new BioSequence(identifier, payload);
            Assert.IsFalse(sequence.PayloadIsValid());
        }

        #endregion


    }
}
