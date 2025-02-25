using LibAlignment;
using LibAlignment.Aligners;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAlignment.Aligners.SingleState;
using LibAlignment.Aligners.PopulationBased;
using LibBioInfo;
using LibScoring.FitnessFunctions;
using LibModification;
using LibModification.AlignmentModifiers;
using LibBioInfo.ScoringMatrices;
using LibModification.AlignmentModifiers.MultiRowStochastic;
using LibModification.AlignmentModifiers.Guided;

namespace MAli.AlignmentConfigs
{
    public class DevelopmentConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetAligner();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objective = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);

            return objective;
        }

        private IterativeAligner GetEvoAligner()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new GuidedGapInserter(),
                new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
                new ResidueShifter(),
                // new SmartBlockPermutationOperator(new PAM250Matrix()),
                // new SmartBlockScrollingOperator(new PAM250Matrix()),
            };

            aligner.MutationOperator = new MultiOperatorModifier(modifiers);


            return aligner;
        }

        private IterativeAligner GetAligner()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, maxIterations);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new GuidedGapInserter(),
                new GuidedResidueShifter(),
                new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
                new ResidueShifter(),
                //new SmartBlockPermutationOperator(new PAM250Matrix()),
                //new SmartBlockScrollingOperator(new PAM250Matrix()),
            };

            aligner.TweakModifier = new MultiOperatorModifier(modifiers);
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }

    }
}
