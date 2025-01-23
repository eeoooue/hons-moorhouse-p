using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibModification;
using LibModification.AlignmentModifiers;
using LibModification.ICrossoverOperators;

namespace TestsUnitSuite.LibBioInfo.ICrossoverOperators
{
    [TestClass]
    public class ColBasedCrossoverOperatorTests
    {
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentStateConverter AlignmentStateConverter = Harness.AlignmentStateConverter;
        StateEquality StateEquality = Harness.StateEquality;
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        ColBasedCrossoverOperator Operator = new ColBasedCrossoverOperator();

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



        [DataTestMethod]
        [DataRow(1)]
        [DataRow(12)]
        [DataRow(123)]
        [DataRow(1234)]
        [DataRow(12345)]
        public void CrossoverModifiesAlignment(int seed)
        {
            Randomizer.SetSeed(seed);

            Alignment a = ExampleAlignments.GetExampleA();
            Alignment b = ExampleAlignments.GetExampleA();
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            List<Alignment> children = Operator.CreateAlignmentChildren(a, b);
            Alignment child1 = children[0];
            Alignment child2 = children[1];

            bool verdict = AlignmentEquality.AlignmentsMatch(child1, child2);
            Assert.IsFalse(verdict);

            bool aMatchesChild1 = AlignmentEquality.AlignmentsMatch(a, child1);
            Assert.IsFalse(aMatchesChild1);

            bool bMatchesChild1 = AlignmentEquality.AlignmentsMatch(b, child1);
            Assert.IsFalse(bMatchesChild1);

            bool aMatchesChild2 = AlignmentEquality.AlignmentsMatch(a, child2);
            Assert.IsFalse(aMatchesChild2);

            bool bMatchesChild2 = AlignmentEquality.AlignmentsMatch(b, child2);
            Assert.IsFalse(bMatchesChild2);
        }
    }
}
