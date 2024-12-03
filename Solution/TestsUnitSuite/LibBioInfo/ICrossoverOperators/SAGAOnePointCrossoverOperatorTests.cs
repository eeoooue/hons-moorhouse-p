using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
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

        SAGAOnePointCrossoverOperator Operator = new SAGAOnePointCrossoverOperator();



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



        [TestMethod]
        public void CanCreateAlignmentChildren()
        {
            Alignment a = ExampleAlignments.GetExampleA();
            Alignment b = ExampleAlignments.GetExampleA();
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(a);
            randomizer.ModifyAlignment(b);

            Operator.CreateAlignmentChildren(a, b);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
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

        [TestMethod]
        public void CheckingABWorks()
        {
            Alignment a = ExampleAlignments.GetExampleA();
            Alignment b = ExampleAlignments.GetExampleA();
            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(b);

            int n = a.Width;

            for(int i=1; i<n; i++)
            {
                Operator.GetABCrossover(a, b, i);
                Operator.GetBACrossover(a, b, i);
            }
        }


        [TestMethod]
        public void TestFigure2ABExample()
        {
            Alignment a = SAGAAssets.GetFigure2ParentAlignment1();
            Alignment b = SAGAAssets.GetFigure2ParentAlignment2();

            Alignment expected = SAGAAssets.GetFigure2ChildAlignment1();
            Alignment actual = Operator.GetABCrossover(a, b, 4);

            bool verdict = AlignmentEquality.AlignmentsMatch(expected, actual);
            Assert.IsTrue(verdict);
        }

        [TestMethod]
        public void TestFigure2BAExample()
        {
            Alignment a = SAGAAssets.GetFigure2ParentAlignment1();
            Alignment b = SAGAAssets.GetFigure2ParentAlignment2();

            Alignment expected = SAGAAssets.GetFigure2ChildAlignment2();
            Alignment actual = Operator.GetBACrossover(a, b, 4);

            bool verdict = AlignmentEquality.AlignmentsMatch(expected, actual);
            Assert.IsTrue(verdict);
        }

        [TestMethod]
        public void ProducesParent1VerticalSplitLeftCorrectly()
        {
            Alignment a = SAGAAssets.GetFigure2ParentAlignment1();

            List<string> mapping = new List<string>()
            {
                "XXXX",
                "XXXX",
                "XXXX",
                "XXXX",
            };

            bool[,] expected = AlignmentStateConverter.ConvertToAlignmentState(mapping);
            bool[,] actual = Operator.GetVerticalSplitLeft(a, 4);
            bool verdict = StateEquality.StatesMatch(expected, actual);
            Assert.IsTrue(verdict);
        }

        [TestMethod]
        public void ProducesParent1VerticalSplitRightCorrectly()
        {
            Alignment a = SAGAAssets.GetFigure2ParentAlignment1();

            List<string> mapping = new List<string>()
            {
                "X---XXXXXXXXX-",
                "XXXX---XXXXXX-",
                "X--XXXXXXXXXXX",
                "XXXX--XXXXXXXX",
            };

            bool[,] expected = AlignmentStateConverter.ConvertToAlignmentState(mapping);
            bool[,] actual = Operator.GetVerticalSplitRight(a, 3);
            Assert.AreEqual(expected.GetLength(1), actual.GetLength(1));


            bool verdict = StateEquality.StatesMatch(expected, actual);
            Assert.IsTrue(verdict);
        }


        [TestMethod]
        public void ProducesParent2JaggedSplitLeftCorrectly()
        {
            Alignment a = SAGAAssets.GetFigure2ParentAlignment2();

            List<string> mapping = new List<string>()
            {
                "--XXXX",
                "XX--XX",
                "XXXX--",
                "XXXX--",
            };

            List<int> positions = new List<int>() { 6, 6, 4, 4 };

            bool[,] expected = AlignmentStateConverter.ConvertToAlignmentState(mapping);
            bool[,] actual = Operator.CollectLeftsUntilPositions(a.State, positions);
            bool verdict = StateEquality.StatesMatch(expected, actual);
            Assert.IsTrue(verdict);
        }


        [TestMethod]
        public void ProducesParent2JaggedSplitRightCorrectly()
        {
            Alignment a = SAGAAssets.GetFigure2ParentAlignment2();

            List<string> mapping = new List<string>()
            {
                "--XXXXXX-XXXX",
                "--XXXXXX-XXXX",
                "XX-XXXXXXXXXX",
                "XXXXXX-XXXXXX",
            };

            List<int> positions = new List<int>() { 5, 5, 3, 3 };

            bool[,] expected = AlignmentStateConverter.ConvertToAlignmentState(mapping);
            bool[,] actual = Operator.CollectRightsUntilPositions(a.State, positions);
            bool verdict = StateEquality.StatesMatch(expected, actual);
            Assert.IsTrue(verdict);
        }

        public bool[] ExtractRow(bool[,] state, int i)
        {
            bool[] result = new bool[state.Length];
            for(int j=0; j<state.GetLength(1); j++)
            {
                result[j] = state[i, j];
            }
            return result;
        }
    }
}
