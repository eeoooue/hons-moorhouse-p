using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitSuite.HarnessTools
{
    [TestClass]
    public class AlignmentStateConverterTests
    {

        AlignmentStateConverter Converter = Harness.AlignmentStateConverter;

        [TestMethod]
        public void CanConvertState1()
        {
            List<string> mapping = new List<string>()
            {
                "XXX---",
                "---XXX",
            };

            bool[,] expected = new bool[,]
            {
                {false, false, false, true, true, true },
                {true, true, true, false, false, false },
            };

            bool[,] actual = Converter.ConvertToAlignmentState(mapping);

            int positionsDiffer = 0;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (actual[i, j] != expected[i, j])
                    {
                        positionsDiffer += 1;
                    }
                }
            }

            Assert.AreEqual(0, positionsDiffer);
        }


    }





    internal class AlignmentStateConverter
    {



        public bool[,] ConvertToAlignmentState(List<string> state)
        {
            int m = state.Count;
            int n = state[0].Length;

            bool[,] result = new bool[m, n];

            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    char character = state[i][j];
                    result[i, j] = character == '-' ? true : false;
                }
            }

            return result;
        }
    }
}
