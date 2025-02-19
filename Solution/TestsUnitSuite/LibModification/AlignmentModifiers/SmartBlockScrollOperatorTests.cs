using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using LibModification.AlignmentModifiers;
using LibModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;

namespace TestsUnitSuite.LibModification.AlignmentModifiers
{
    [TestClass]
    public class SmartBlockScrollOperatorTests
    {
        SmartBlockScrollingOperator Modifier = new SmartBlockScrollingOperator(new BLOSUM62Matrix());

        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        #region

        [TestMethod]
        public void AlignmentIsConserved()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(original);

            Alignment copy = original.GetCopy();
            Modifier.ModifyAlignment(copy);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }

        [TestMethod]
        public void AlignmentIsDifferent()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(original);

            Alignment copy = original.GetCopy();
            Modifier.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);
        }

        #endregion
    }
}
