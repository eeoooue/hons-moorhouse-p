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
            return GetBestPerformer();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objective = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);

            return objective;
        }

        private IterativeAligner GetBestPerformer()
        {
            return GetCandidateC();
        }

        private IterativeAligner GetCandidateA()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;
            const int mew = 5;
            const int lambda = mew * 7;
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations, mew, lambda);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new MultiRowStochasticSwapOperator(),
                new GapInserter(),
                new HeuristicPairwiseModifier(),
            };

            aligner.MutationOperator = new MultiOperatorModifier(modifiers);
            return aligner;
        }

        private IterativeAligner GetCandidateB()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;
            const int populationSize = 35;
            GeneticAlgorithmAligner aligner = new GeneticAlgorithmAligner(objective, maxIterations, populationSize);

            aligner.CrossoverOperator = new ColBasedCrossoverOperator();

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new MultiRowStochasticSwapOperator(),
                new GapInserter(),
                new HeuristicPairwiseModifier(),
            };

            aligner.MutationOperator = new MultiOperatorModifier(modifiers);
            return aligner;
        }

        private IterativeAligner GetCandidateC()
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

        private IterativeAligner GetCandidateD()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            HillClimbWithRandomRestartsAligner aligner = new HillClimbWithRandomRestartsAligner(objective, maxIterations);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new GapInserter(),
                new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
            };

            aligner.Modifier = new MultiOperatorModifier(modifiers);

            return aligner;
        }
    }
}
