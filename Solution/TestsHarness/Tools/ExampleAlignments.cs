using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsHarness.Tools
{
    public enum ExampleAlignment
    {
        ExampleA,
    }

    public class ExampleAlignments
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;

        public Alignment GetAlignment(ExampleAlignment identifier)
        {
            switch (identifier)
            {
                case ExampleAlignment.ExampleA:
                    return GetExampleA();
                default:
                    throw new ArgumentException("Invalid identifier");
            }
        }

        public Alignment GetExampleA()
        {
            List<BioSequence> sequences = new List<BioSequence>();
            sequences.Add(ExampleSequences.GetSequence(ExampleSequence.ExampleA));
            sequences.Add(ExampleSequences.GetSequence(ExampleSequence.ExampleB));
            sequences.Add(ExampleSequences.GetSequence(ExampleSequence.ExampleD));

            return new Alignment(sequences);
        }
    }
}
