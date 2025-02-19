using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibBioInfo;
using LibModification.AlignmentModifiers;
using LibModification;

namespace TestsUnitSuite.LibModification.Helpers
{
    [TestClass]
    public class CharMatrixHelperTests
    {
        CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;


        [TestMethod]
        public void Stuff()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(original);

            Alignment copy = original.GetCopy();

            CharMatrixHelper.SprinkleEmptyColumnsIntoAlignment(copy, 10);

            AlignmentConservation.AssertAlignmentsAreConserved(copy, original);
        }


    }
}
