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
    }
}
