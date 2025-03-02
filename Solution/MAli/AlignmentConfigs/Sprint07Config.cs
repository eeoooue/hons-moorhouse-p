using LibAlignment;
using LibAlignment.Aligners.PopulationBased;
using LibAlignment.Aligners.SingleState;
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

namespace MAli.AlignmentConfigs
{
    internal class Sprint07Config : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetILSAligner();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new PAM250Matrix();
            IFitnessFunction objective = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);

            return objective;
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
    }
}
