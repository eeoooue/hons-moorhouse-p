using LibBioInfo.INeighbourhoodFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite.HarnessTools;

namespace TestsUnitSuite.LibBioInfo.INeighbourhoodFinders
{
    [TestClass]
    public class RowBasedNeighbourhoodFinderTests
    {
        public RowBasedNeighbourhoodFinder Finder = new RowBasedNeighbourhoodFinder();
        StateEquality StateEquality = Harness.StateEquality;

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
    }
}
