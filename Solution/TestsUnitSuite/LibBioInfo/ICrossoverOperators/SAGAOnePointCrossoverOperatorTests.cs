using LibBioInfo;
using LibBioInfo.ICrossoverOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite.HarnessTools;
using TestsUnitSuite.LiteratureAssets;

namespace TestsUnitSuite.LibBioInfo.ICrossoverOperators
{
    [TestClass]
    public class SAGAOnePointCrossoverOperatorTests
    {
        SAGAAssets SAGAAssets = Harness.LiteratureHelper.SAGAAssets;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        OnePointCrossoverOperator Operator = new OnePointCrossoverOperator();

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [Ignore]

        public void TestFigure2Example(int childIndex)
        {
            Alignment a = SAGAAssets.GetFigure2ParentAlignment1();
            Alignment b = SAGAAssets.GetFigure2ParentAlignment2();

            List<Alignment> expectedChildren = new List<Alignment>()
            {
                SAGAAssets.GetFigure2ChildAlignment1(),
                SAGAAssets.GetFigure2ChildAlignment2(),
            };

            List<Alignment> actualChildren = Operator.CrossoverAtPosition(a, b, 4);

            Alignment expected = expectedChildren[childIndex];
            Alignment actual = actualChildren[childIndex];

            bool verdict = AlignmentEquality.AlignmentsMatch(expected, actual);
            Assert.IsTrue(verdict);
        }
    }
}
