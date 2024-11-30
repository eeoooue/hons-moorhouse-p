using LibBioInfo.IAlignmentModifiers;
using LibBioInfo.ICrossoverOperators;
using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.LiteratureAssets;
using TestsHarness.Tools;
using TestsHarness;
using LibFileIO;
using LibAlignment;

namespace TestsPerformance.LibBioInfo.ICrossoverOperators
{

    [TestClass]
    public class OnePointCrossoverOperatorTests
    {
        SAGAAssets SAGAAssets = Harness.LiteratureHelper.SAGAAssets;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        OnePointCrossoverOperator Operator = new OnePointCrossoverOperator();
        private FileHelper FileHelper = new FileHelper();


        #region

        [DataTestMethod]
        [DataRow("BB11003", 8)]
        [DataRow("BB11003", 16)]
        [DataRow("BB11003", 32)]
        [Timeout(5000)]

        public void CanCrossoverBBSEfficiently(string filename, int repetitions)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment a = new Alignment(sequences);
            Alignment b = new Alignment(sequences);

            AlignmentRandomizer randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            for(int i=0; i<repetitions; i++)
            {
                List<Alignment> results = Operator.CreateAlignmentChildren(a, b);
                a = results[0];
                b = results[1];
            }
        }

        #endregion


        #region

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [Timeout(5000)]

        public void CanCrossoverPREFABEfficiently(string filename, int repetitions)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment a = new Alignment(sequences);
            Alignment b = new Alignment(sequences);

            AlignmentRandomizer randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            for (int i = 0; i < repetitions; i++)
            {
                List<Alignment> results = Operator.CreateAlignmentChildren(a, b);
                a = results[0];
                b = results[1];
            }
        }

        #endregion
    }
}
