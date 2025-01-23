using LibBioInfo;
using LibBioInfo.LegacyAlignmentModifiers;
using LibBioInfo.ICrossoverOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TestsHarness;
using TestsHarness.LiteratureAssets;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibBioInfo.ICrossoverOperators
{
    [TestClass]
    public class SAGAOnePointCrossoverOperatorTests
    {
        SAGAAssets SAGAAssets = Harness.LiteratureHelper.SAGAAssets;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentStateConverter AlignmentStateConverter = Harness.AlignmentStateConverter;
        StateEquality StateEquality = Harness.StateEquality;
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;
        AlignmentPrinter AlignmentPrinter = Harness.AlignmentPrinter;

        SAGAOnePointCrossoverOperator Operator = new SAGAOnePointCrossoverOperator();

        #region testing edge cases 


        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void CanCrossoverGaplessAlignments(int position)
        {
            List<string> mapping = new List<string>()
            {
                "XXXXX",
                "XXXXX",
                "XXXXX",
            };

            BioSequence seq1 = new BioSequence("testA", "AAAAA");
            BioSequence seq2 = new BioSequence("testB", "AACAA");
            BioSequence seq3 = new BioSequence("testC", "AAGAA");
            List<BioSequence> sequences = new List<BioSequence>() { seq1, seq2, seq3 };

            Alignment a = new Alignment(sequences);
            bool[,] state = AlignmentStateConverter.ConvertToAlignmentState(mapping);
            a.SetState(state);

            Alignment b = a.GetCopy();
            Operator.CrossoverAtPosition(a, b, position);
        }

        #endregion

        [DataTestMethod]
        [DataRow(1, 0)]
        [DataRow(1, 1)]
        [DataRow(12, 0)]
        [DataRow(12, 1)]
        [DataRow(123, 0)]
        [DataRow(123, 1)]
        [DataRow(1234, 0)]
        [DataRow(1234, 1)]
        [DataRow(12345, 0)]
        [DataRow(12345, 1)]
        public void CrossoverConservesSequenceData(int seed, int i)
        {
            Randomizer.SetSeed(seed);

            Alignment a = ExampleAlignments.GetExampleA();
            Alignment b = ExampleAlignments.GetExampleA();
            ILegacyAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            List<Alignment> children = Operator.CreateAlignmentChildren(a, b);
            Alignment child = children[i];

            AlignmentConservation.AssertAlignmentsAreConserved(a, child);
        }

        [TestMethod]
        public void CanCreateAlignmentChildren()
        {
            Alignment a = ExampleAlignments.GetExampleA();
            Alignment b = ExampleAlignments.GetExampleA();
            ILegacyAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            Operator.CreateAlignmentChildren(a, b);
        }
    }
}
