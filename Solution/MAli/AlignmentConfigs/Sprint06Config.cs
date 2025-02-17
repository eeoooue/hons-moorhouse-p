using LibAlignment.Aligners.PopulationBased;
using LibAlignment.Aligners.SingleState;
using LibAlignment;
using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using LibModification.AlignmentModifiers;
using LibModification.CrossoverOperators;
using LibModification;
using LibScoring.FitnessFunctions;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentConfigs
{
    public class Sprint06Config : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetNewExperimentalConfig();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objective = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);

            return objective;
        }

        private IterativeAligner GetNewExperimentalConfig()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, maxIterations);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new GapInserter(),
                new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
            };

            aligner.TweakModifier = new MultiOperatorModifier(modifiers);
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }



        private IterativeAligner GetSprint05Config()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, maxIterations);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new GapInserter(),
                new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
            };

            aligner.TweakModifier = new MultiOperatorModifier(modifiers);
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }
    }
}
