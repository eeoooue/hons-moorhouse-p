using LibAlignment;
using LibAlignment.Aligners.SingleState;
using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibScoring;
using LibScoring.FitnessFunctions;
using MAli;
using MAli.AlignmentEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsRequirements
{
    [TestClass]
    public class Objective01
    {

        private ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        private AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        /// <summary>
        /// Given sequences to align, produces a valid solution - independent of quality.
        /// </summary>
        [DataTestMethod]
        public void Req1x01()
        {
            IterativeAligner aligner = GetAligner();
            Alignment original = ExampleAlignments.GetExampleA();
            List<BioSequence> sequences = original.Sequences;
            Alignment alignment = aligner.AlignSequences(sequences);
            AlignmentConservation.AssertAlignmentsAreConserved(alignment, original);
        }

        /// <summary>
        /// Employs a heuristic to estimate a number of iterations needed to align each set of sequences.
        /// </summary>
        [TestMethod]
        public void Req1x02()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Aligns sets of 6 typical protein sequences within 10 seconds on a university machine.
        /// </summary>
        [DataTestMethod]
        [Timeout(10000)]
        public void Req1x03()
        {
            IterativeAligner aligner = GetAligner();
            List<BioSequence> sequences = GetTypicalSequences();
            Assert.IsTrue(sequences.Count == 6);
            Alignment alignment = aligner.AlignSequences(sequences);
        }

        private IterativeAligner GetAligner()
        {
            IScoringMatrix matrix = new PAM250Matrix();
            IFitnessFunction objective = new SumOfPairsFitnessFunction(matrix);
            IterativeAligner aligner = new IteratedLocalSearchAligner(objective, 100);

            return aligner;
        }

        private List<BioSequence> GetTypicalSequences()
        {
            Alignment original = ExampleAlignments.GetExampleB();
            List<BioSequence> sequences = original.Sequences;

            return sequences;
        }
    }
}
