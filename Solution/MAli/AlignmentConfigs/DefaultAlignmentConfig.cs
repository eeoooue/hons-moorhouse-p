using LibAlignment.Aligners.SingleState;
using LibAlignment;
using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using LibModification.AlignmentModifiers.Guided;
using LibModification.AlignmentModifiers.MultiRowStochastic;
using LibModification.AlignmentModifiers;
using LibModification;
using LibScoring.FitnessFunctions;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAlignment.Aligners.PopulationBased;

namespace MAli.AlignmentConfigs
{
    internal class DefaultAlignmentConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetILSAligner();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new PAM250Matrix();
            IFitnessFunction objectiveA = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);
            IFitnessFunction objectiveB = new NonGapsFitnessFunction();

            List<IFitnessFunction> objectives = new List<IFitnessFunction>() { objectiveA, objectiveB };
            // List<double> weights = new List<double>() { 0.9, 0.1 };
            List<double> weights = new List<double>() { 0.90, 0.1 };

            WeightedCombinationOfFitnessFunctions combo = new WeightedCombinationOfFitnessFunctions(objectives, weights);
            return combo;
        }

        private IAlignmentModifier GetMultiOperatorModifier()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GuidedSwap(),
                new GuidedGapInserter(),
                new BlockShuffler(),
                new GapShuffler(),
                new HeuristicPairwiseModifier(),
            };

            return new MultiOperatorModifier(modifiers);
        }

        private IterativeAligner GetILSAligner()
        {
            IFitnessFunction objective = GetObjective();
            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, 100);

            aligner.TweakModifier = GetMultiOperatorModifier();
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }

        private IterativeAligner GetMewLambdaEVAligner()
        {
            IFitnessFunction objective = GetObjective();
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, 100);
            aligner.MutationOperator = GetMultiOperatorModifier();

            return aligner;
        }
    }
}
