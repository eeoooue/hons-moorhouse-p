using LibBioInfo;
using LibModification;
using LibModification.AlignmentModifiers;
using LibModification.Mechanisms;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class HeuristicPairwiseModifierTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;


        HeuristicPairwiseModifier Modifier = new HeuristicPairwiseModifier();

        #region

        [TestMethod]
        public void AlignmentIsConserved()
        {
            Alignment original = ExampleAlignments.GetExampleA();
            SimilarityGuide.SetSequences(original.Sequences);

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
            SimilarityGuide.SetSequences(original.Sequences);

            IAlignmentModifier randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(original);

            Alignment copy = original.GetCopy();
            Modifier.ModifyAlignment(copy);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(original, copy);
            Assert.IsFalse(alignmentsMatch);
        }

        #endregion



        #region Canvas recipe creation

        [TestMethod]
        public void CanCollectDistancesBetweenResiduesA()
        {
            string stuff = "--X--";
            List<int> expected = new List<int>() { 2, 2 };
            List<int> actual = PairwiseAlignment.CollectDistancesBetweenResidues(stuff);
            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        [TestMethod]
        public void CanCollectDistancesBetweenResiduesB()
        {
            string stuff = "X----X--X";
            List<int> expected = new List<int>() { 0, 4, 2, 0 };
            List<int> actual = PairwiseAlignment.CollectDistancesBetweenResidues(stuff);
            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        [TestMethod]
        public void CanGetCanvasRecipeEasy()
        {
            List<int> expected = new List<int>() { 0, 1, 2, 3, 4 };
            string before = "--X--";
            string after =  "--X--";

            List<int> actual = PairwiseAlignment.GetCanvasRecipe(before, after);

            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        [TestMethod]
        public void CanGetCanvasRecipeMedium()
        {
            List<int> expected = new List<int>() { 0, 1, -1, 2, 3, 4, -1 };
            string before = "--X--";
            string after = "---X---";

            List<int> actual = PairwiseAlignment.GetCanvasRecipe(before, after);

            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        [TestMethod]
        public void CanGetCanvasRecipeHard()
        {
            List<int> expected = new List<int>() { 0, 1, 2, 3, -1, 4, -1, 5, 6, 7, 8, -1 };
            string before = "--X-XX--X";
            string after = "-X--X-X-X-";

            // --X- X X--X 
            // - X--X-X- X-
            // --X--X-X--X-

            List<int> actual = PairwiseAlignment.GetCanvasRecipe(before, after);

            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        public bool ListsMatch(List<int> expected, List<int> actual)
        {
            PrintList("expected:", expected);
            PrintList("actual:", actual);

            if (expected.Count != actual.Count)
            {
                return false;
            }

            for (int i = 0; i < expected.Count; i++)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion


        #region debugging

        public void PrintList(string context, List<int> stuff)
        {
            Console.WriteLine(context);
            foreach(int x in stuff)
            {
                Console.Write(x + ", ");
            }
            Console.WriteLine();
        }

        #endregion

    }
}
