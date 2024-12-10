using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsHarness.Tools
{
    public enum ExampleSequence
    {
        ExampleA,
        ExampleB,
        ExampleC,
        ExampleD
    }

    public class ExampleSequences
    {
        public BioSequence GetSequence(ExampleSequence identifier)
        {
            switch (identifier)
            {
                case ExampleSequence.ExampleA:
                    return GetExampleA();
                case ExampleSequence.ExampleB:
                    return GetExampleB();
                case ExampleSequence.ExampleC:
                    return GetExampleC();
                case ExampleSequence.ExampleD:
                    return GetExampleD();
                default:
                    throw new ArgumentException("Invalid identifier");
            }
        }

        private BioSequence GetExampleA()
        {
            string identifier = "ExampleA";
            string payload = "ACGTACGTACGTACGTACGT";
            return new BioSequence(identifier, payload);
        }

        private BioSequence GetExampleB()
        {
            string identifier = "ExampleB";
            string payload = "ACGTTTTTTTT";
            return new BioSequence(identifier, payload);
        }

        private BioSequence GetExampleC()
        {
            string identifier = "ExampleC";
            string payload = "CCCCCCCCCCCCCCCCCCCCCCCCCCCC";
            return new BioSequence(identifier, payload);
        }

        private BioSequence GetExampleD()
        {
            string identifier = "ExampleD";
            string payload = "ACGTACGT----ACGT";
            return new BioSequence(identifier, payload);
        }
    }
}
