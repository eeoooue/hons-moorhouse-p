﻿using LibAlignment;
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
            return GetGeneticAligner();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new PAM250Matrix();
            IFitnessFunction objective1 = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 2);

            IFitnessFunction objective2 = new TotallyConservedColumnsFitnessFunction();

            List<IFitnessFunction> objectives = new List<IFitnessFunction> () { objective1, objective2 };
            List<double> weights = new List<double>() { 1.0, 0.0 };

            return new WeightedCombinationOfFitnessFunctions(objectives, weights);
        }

        private IterativeAligner GetGeneticAligner()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            ElitistGeneticAlgorithmAligner aligner = new ElitistGeneticAlgorithmAligner(objective, maxIterations, 50);
            aligner.SelectionSize = 5;
            aligner.MutationOperator = GetMultiOperatorModifier();

            return aligner;
        }

        private IterativeAligner GetEvoAligner()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations);

            aligner.MutationOperator = GetMultiOperatorModifier();

            return aligner;
        }

        private IAlignmentModifier GetMultiOperatorModifier()
        {

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GuidedSwap(),
                new GuidedGapInserter(),
                new GuidedResidueShifter(),
                //new GuidedRelativeScroll(),

                //new SwapOperator(),
                //new ResidueShifter(),
                //new GapInserter(),

                new BlockShuffler(),
                new GapShuffler(),

                //new GapShifter(),
                //new MultiRowStochasticSwapOperator(),
                // new HeuristicPairwiseModifier(),
                // new ResidueShifter(),
                //new SmartBlockPermutationOperator(new PAM250Matrix()),
                //new SmartBlockScrollingOperator(new PAM250Matrix()),
            };

            return new MultiOperatorModifier(modifiers);
        }

        private IterativeAligner GetAligner()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, maxIterations);

            aligner.TweakModifier = GetMultiOperatorModifier();
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }

    }
}
