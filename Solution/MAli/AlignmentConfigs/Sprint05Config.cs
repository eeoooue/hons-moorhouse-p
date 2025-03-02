using LibAlignment;
using LibAlignment.Aligners.PopulationBased;
using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using LibModification.AlignmentModifiers;
using LibModification;
using LibScoring.FitnessFunctions;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibModification.CrossoverOperators;
using System.Security.AccessControl;
using LibAlignment.Aligners.SingleState;
using LibModification.AlignmentModifiers.MultiRowStochastic;

namespace MAli.AlignmentConfigs
{
    public class Sprint05Config : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetILSAligner();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objective = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);

            return objective;
        }

        public IAlignmentModifier GetModifier()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new GapInserter(),
                new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
            };

            return new MultiOperatorModifier(modifiers);
        }

        private IterativeAligner GetILSAligner()
        {
            IFitnessFunction objective = GetObjective();
            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, 100);

            aligner.TweakModifier = GetModifier();
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }
    }
}
