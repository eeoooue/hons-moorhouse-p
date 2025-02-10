using LibAlignment;
using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using LibScoring.FitnessFunctions;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibModification;
using LibModification.AlignmentModifiers;
using LibAlignment.Aligners.PopulationBased;
using LibAlignment.Aligners.SingleState;

namespace MAli.AlignmentConfigs
{
    internal class Sprint06MatrixComparisonConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetVersionB();
        }

        private IterativeAligner GetVersionA()
        {
            return CreateAligner(new PAM250Matrix());
        }

        private IterativeAligner GetVersionB()
        {
            return CreateAligner(new BLOSUM62Matrix());
        }

        private IterativeAligner CreateAligner(IScoringMatrix matrix)
        {
            IFitnessFunction objective = new SumOfPairsFitnessFunction(matrix);
            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, 100);
            aligner.TweakModifier = GetModifier();
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }

        public IAlignmentModifier GetModifier()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new MultiRowStochasticSwapOperator(),
                new GapInserter(),
                new GapShifter(),
                new MultiRowStochasticGapShifter(),
                new HeuristicPairwiseModifier(),
            };

            IAlignmentModifier modifier = new MultiOperatorModifier(modifiers);

            return modifier;
        }
    }
}
