using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibParetoAlignment;
using LibParetoAlignment.Aligners;
using LibScoring;
using LibScoring.FitnessFunctions;
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
    public class Objective05
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        /// <summary>
        /// Leverages multiple objective functions to guide the alignment optimization process.
        /// </summary>
        [TestMethod]
        public void Req5x01()
        {
            ParetoIterativeAligner aligner = GetMultiObjectiveAligner();
            InitializeAlignerWithSequences(aligner);
            PerformIterations(aligner, 100);
        }

        /// <summary>
        /// Approximates the Pareto Front, outputting a set of solutions that offer different trade-offs.
        /// </summary>
        [TestMethod]
        public void Req5x02()
        {
            ParetoIterativeAligner aligner = GetMultiObjectiveAligner();
            InitializeAlignerWithSequences(aligner);
            PerformIterations(aligner, 100);

            List<Alignment> tradeoffs = aligner.CollectTradeoffSolutions();
            Assert.IsTrue(tradeoffs.Count > 1);
        }


        private void InitializeAlignerWithSequences(ParetoIterativeAligner aligner)
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            List<BioSequence> sequences = alignment.Sequences;
            aligner.Initialize(sequences);
        }

        private void PerformIterations(ParetoIterativeAligner aligner, int count)
        {
            for (int i = 0; i < count; i++)
            {
                aligner.Iterate();
            }
        }

        private ParetoIterativeAligner GetMultiObjectiveAligner()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();

            List<IFitnessFunction> objectives = new List<IFitnessFunction>()
            {
                new SumOfPairsFitnessFunction(matrix),
                new TotallyConservedColumnsFitnessFunction(),
                new NonGapsFitnessFunction(),
            };

            ParetoIterativeAligner aligner = new ParetoHillClimbAligner(objectives);
            return aligner;
        }
    }
}
