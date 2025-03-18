using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibModification.AlignmentModifiers.Guided;
using LibModification.AlignmentModifiers;
using LibModification;
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
using LibFileIO;
using MAli.AlignmentConfigs;

namespace TestsRequirements
{
    [TestClass]
    public class Objective05
    {
        BaseParetoAlignmentConfig Config = new ParetoDevConfig();
        FileHelper FileHelper = new FileHelper();

        /// <summary>
        /// Leverages multiple objective functions to guide the alignment optimization process.
        /// </summary>
        [TestMethod]
        [DoNotParallelize]
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
        [DoNotParallelize]
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
            Alignment alignment = FileHelper.ReadAlignmentFrom("BB11001");
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
            return Config.CreateAligner();
        }
    }
}
