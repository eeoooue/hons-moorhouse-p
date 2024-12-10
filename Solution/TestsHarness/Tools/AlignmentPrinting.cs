using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsHarness.Tools
{
    public class AlignmentPrinter
    {




        public void PrintAlignment(Alignment alignment)
        {
            Console.WriteLine($"Printing alignment ({alignment.Height} x {alignment.Width})");
            foreach(BioSequence sequence in alignment.GetAlignedSequences())
            {
                string payload = sequence.Payload;
                Console.WriteLine(payload);
            }
        }

        public void PrintState(bool[,] state)
        {
            for(int i=0; i<state.GetLength(0); i++)
            {
                PrintStateRow(state, i);
            }
        }

        public void PrintStateRow(bool[,] state, int i)
        {
            StringBuilder sb = new StringBuilder();

            for(int j=0; j<state.GetLength(1); j++)
            {
                char x = 'X';
                if (state[i, j])
                {
                    x = '-';
                }
                sb.Append(x);
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
