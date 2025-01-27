using LibModification.AlignmentModifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.LibBioInfo.IAlignmentModifiers
{
    [TestClass]
    public class HeuristicPairwiseModifierTests
    {
        HeuristicPairwiseModifier Modifier = new HeuristicPairwiseModifier();




        [TestMethod]
        public void CanCollectDistancesBetweenResiduesA()
        {
            string stuff = "--X--";
            List<int> expected = new List<int>() { 2, 2 };
            List<int> actual = Modifier.CollectDistancesBetweenResidues(stuff);
            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        [TestMethod]
        public void CanCollectDistancesBetweenResiduesB()
        {
            string stuff = "X----X--X";
            List<int> expected = new List<int>() { 0, 4, 2, 0 };
            List<int> actual = Modifier.CollectDistancesBetweenResidues(stuff);
            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        [TestMethod]
        public void CanGetCanvasRecipeEasy()
        {
            List<int> expected = new List<int>() { 0, 1, 2, 3, 4 };
            string before = "--X--";
            string after =  "--X--";

            List<int> actual = Modifier.GetCanvasRecipe(before, after);

            bool listsMatch = ListsMatch(expected, actual);
            Assert.IsTrue(listsMatch);
        }

        [TestMethod]
        public void CanGetCanvasRecipeMedium()
        {
            List<int> expected = new List<int>() { 0, 1, -1, 2, 3, 4, -1 };
            string before = "--X--";
            string after = "---X---";

            List<int> actual = Modifier.GetCanvasRecipe(before, after);

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

            List<int> actual = Modifier.GetCanvasRecipe(before, after);

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

            for (int i=0; i<expected.Count; i++)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void PrintList(string context, List<int> stuff)
        {
            Console.WriteLine(context);
            foreach(int x in stuff)
            {
                Console.Write(x + ", ");
            }
            Console.WriteLine();
        }

    }
}
