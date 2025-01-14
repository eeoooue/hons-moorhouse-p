using LibAlignment;
using LibAlignment.Aligners;
using LibAlignment.Aligners.PopulationBased;
using LibAlignment.Aligners.SingleState;
using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibScoring;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentConfigs
{
    internal class Sprint04ComparisonConfig : AlignmentConfig
    {
        private IObjectiveFunction Objective { get { return GetObjective(); } }
        private IAlignmentModifier Modifier {  get { return GetOperator(); } }

        private int Iterations = 1000;

        public override IterativeAligner CreateAligner()
        {
            return CreateSingleStateAligner();
        }

        public IAlignmentModifier GetOperator()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new MultiRowStochasticSwapOperator(),
                new SwapOperator(),
                new GapInserter(),
            };

            return new MultiOperatorModifier(modifiers);
        }
        public IObjectiveFunction GetObjective()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);

            return objective;
        }

        public PopulationBasedAligner CreatePopulationBasedAligner()
        {
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(Objective, Iterations);
            aligner.MutationOperator = Modifier;

            return aligner;
        }

        public SingleStateAligner CreateSingleStateAligner()
        {
            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(Objective, Iterations);
            aligner.PerturbModifier = Modifier;
            aligner.TweakModifier = Modifier;

            return aligner;
        }
    }
}
