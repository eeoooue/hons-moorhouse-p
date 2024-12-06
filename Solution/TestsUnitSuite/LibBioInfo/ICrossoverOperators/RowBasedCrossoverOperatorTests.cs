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

namespace TestsUnitSuite.LibBioInfo.ICrossoverOperators
{
    [TestClass]
    public class RowBasedCrossoverOperatorTests
    {

        SAGAAssets SAGAAssets = Harness.LiteratureHelper.SAGAAssets;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentStateConverter AlignmentStateConverter = Harness.AlignmentStateConverter;
        StateEquality StateEquality = Harness.StateEquality;
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        RowBasedCrossoverOperator Operator = new RowBasedCrossoverOperator();


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
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            List<Alignment> children = Operator.CreateAlignmentChildren(a, b);
            Alignment child = children[i];

            AlignmentConservation.AssertAlignmentsAreConserved(a, child);
        }

    }
}
