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
    public class DevelopmentConfig : BaseAlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetMewLambda();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new PAM250Matrix();
            IFitnessFunction objective1 = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);

            return objective1;
        }

        private IterativeAligner GetMewLambda()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            MewPlusLambdaEvolutionStrategyAligner aligner = new MewPlusLambdaEvolutionStrategyAligner(objective, maxIterations, 5, 20);

            aligner.MutationOperator = GetMutationOperator();

            return aligner;
        }

        private IAlignmentModifier GetMutationOperator()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                // Pairwise Alignment
                new HeuristicPairwiseModifier(),

                // MSASA
                new SwapOperator(),
                new MultiRowStochasticSwapOperator(),

                // SAGA
                new BlockShuffler(),
                new GapShuffler(),
                // new GapInserter(),

                // Shifters
                // new ResidueShifter(),
                // new GapShifter(),
            };

            return new MultiOperatorModifier(modifiers);
        }
    }
}
