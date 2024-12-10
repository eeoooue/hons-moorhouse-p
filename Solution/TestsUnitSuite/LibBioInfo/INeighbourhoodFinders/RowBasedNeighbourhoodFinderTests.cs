using LibBioInfo.INeighbourhoodFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibBioInfo.INeighbourhoodFinders
{
    [TestClass]
    public class RowBasedNeighbourhoodFinderTests
    {
        public RowBasedNeighbourhoodFinder Finder = new RowBasedNeighbourhoodFinder();
        StateEquality StateEquality = Harness.StateEquality;

        #region testing general neighbourhood finding (via interface)

        [TestMethod]
        public void HomogenousStateHasNoNeighbours()
        {
            bool[,] state =
            {
                { true, true, true, true, },
                { true, true, true, true, },
                { true, true, true, true, },
            };

            List<bool[,]> options = Finder.FindNeighbours(state);
            Assert.AreEqual(0, options.Count);
        }

        [TestMethod]
        public void ComplexStateHasNeighbours()
        {
            bool[,] state =
            {
                { true, true, false, false, },
                { true, false, true, false, },
                { true, false, false, true, },
            };

            List<bool[,]> options = Finder.FindNeighbours(state);
            Assert.IsTrue(options.Count > 0);
        }

        #endregion




        #region testing row-based neighbourhood finding 

        [TestMethod]
        public void HomogenousStateHasNoRowBasedNeighbours()
        {
            bool[,] state =
            {
                { true, true, true, true, },
                { true, true, true, true, },
                { true, true, true, true, },
            };

            for(int i=0; i<3; i++)
            {
                List<bool[,]> options = Finder.FindNeighboursForRow(state, i);
                Assert.AreEqual(0, options.Count);
            }
        }

        [TestMethod]
        public void ComplexStateHasRowBasedNeighbours()
        {
            bool[,] state =
            {
                { true, true, false, false, },
                { true, false, true, false, },
                { true, false, false, true, },
            };

            for (int i = 0; i < 3; i++)
            {
                List<bool[,]> options = Finder.FindNeighboursForRow(state, i);
                Assert.IsTrue(options.Count > 0);
            }
        }

        #endregion




        #region testing replacing individual rows of a state

        [TestMethod]
        public void CanReplaceSpecificRow()
        {
            bool[,] state =
            {
                { true, true, true, true, },
                { true, true, true, true, },
            };

            bool[] replacement = { false, false, false, false };

            bool[,] expected =
            {
                { true, true, true, true, },
                { false, false, false, false },
            };

            bool[,] result = Finder.GetStateWithReplacedRow(state, 1, replacement);

            bool verdict = StateEquality.StatesMatch(expected, result);
            Assert.IsTrue(verdict);
        }


        #endregion


        #region testing finding neighbours of a row state

        [TestMethod]
        public void HomogenousRowHasNoNeighbours()
        {
            bool[] original = { true, true, true, true };
            List<bool[]> neighbours = Finder.GetNeighboursOfRow(original);
            Assert.AreEqual(0, neighbours.Count);
        }

        [TestMethod]
        public void SpecificRowHasTwoNeighbours()
        {
            bool[] original = { true, false, true };
            List<bool[]> neighbours = Finder.GetNeighboursOfRow(original);
            Assert.AreEqual(2, neighbours.Count);
        }

        #endregion


        #region testing i j swaps within a row

        [TestMethod]
        public void CanMakeIJSwapInRowA()
        {
            bool[] before = { true, false, true, true };
            bool[] expected = { false, true, true, true };
            bool[] result = Finder.GetRowAfterSwap(before, 0, 1);
            bool verdict = StateEquality.RowsMatch(expected, result);
        }

        [TestMethod]
        public void CanMakeIJSwapInRowB()
        {
            bool[] before = { true, false, true, true };
            bool[] expected = { true, true, false, true };
            bool[] result = Finder.GetRowAfterSwap(before, 1, 0);
            bool verdict = StateEquality.RowsMatch(expected, result);
        }

        #endregion
    }
}
