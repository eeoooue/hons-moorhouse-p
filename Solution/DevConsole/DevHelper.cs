﻿using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConsole
{
    internal class DevHelper
    {
        public void PrintSequences(List<BioSequence> sequences)
        {
            foreach (BioSequence sequence in sequences)
            {
                PrintSequenceInfo(sequence);
            }
        }

        public void PrintSequenceInfo(BioSequence sequence)
        {
            Console.WriteLine($"Printing Sequence Info:");
            Console.WriteLine($"Identifier: {sequence.Identifier}");
            Console.WriteLine($"Payload: {sequence.Payload}");
            Console.WriteLine($"");
        }

        public void PrintAlignmentState(Alignment alignment)
        {
            Console.WriteLine($"Printing Alignment State:");

            for (int i = 0; i < alignment.Height; i++)
            {
                string alignedSequence = alignment.GetAlignedPayload(i);
                Console.WriteLine($"    {alignedSequence}");
            }
        }
    }
}
