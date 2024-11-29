using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsUnitSuite.HarnessTools;

namespace TestsUnitSuite
{
    internal class Harness
    {
        public static ExampleSequences ExampleSequences = new ExampleSequences();
        public static SequenceConservation SequenceConservation = new SequenceConservation();
        public static SequenceEquality SequenceEquality = new SequenceEquality();
        public static AlignmentEquality AlignmentEquality = new AlignmentEquality();
        public static AlignmentConservation AlignmentConservation = new AlignmentConservation();
        public static ExampleAlignments ExampleAlignments = new ExampleAlignments();
        public static StateEquality StateEquality = new StateEquality();
        public static AlignmentStateConverter AlignmentStateConverter = new AlignmentStateConverter();
        public static LiteratureHelper LiteratureHelper = new LiteratureHelper();
    }
}
