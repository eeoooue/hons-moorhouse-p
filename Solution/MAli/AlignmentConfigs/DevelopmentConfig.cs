﻿using LibAlignment;
using LibAlignment.Aligners;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAlignment.Aligners.SingleState;
using LibAlignment.Aligners.PopulationBased;
using LibBioInfo.IAlignmentModifiers;
using LibBioInfo;
using LibScoring.FitnessFunctions;
using LibBioInfo.LegacyAlignmentModifiers;

namespace MAli.AlignmentConfigs
{
    internal class DevelopmentConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetSprint04Version();
        }

        private MewLambdaEvolutionaryAlgorithmAligner GetSprint04Version()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objective = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);

            const int maxIterations = 100;
            const int mew = 10;
            const int lambda = 10 * 7;
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations, mew, lambda);

            List<ILegacyAlignmentModifier> modifiers = new List<ILegacyAlignmentModifier>()
            {
                new MultiRowStochasticSwapOperator(),
                new SwapOperator(),
                new GapInserter(),
            };

            MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);
            aligner.MutationOperator = modifier;

            return aligner;
        }


        public IterativeAligner GetRandomSearchAligner()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IFitnessFunction sumOfPairsWithAffine = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(blosum);
            int iterations = 1000;

            return new RandomSearchAligner(sumOfPairsWithAffine, iterations);
        }

        public IterativeAligner GetILSAligner()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IFitnessFunction sumOfPairsWithAffine = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(blosum);
            int iterations = 10000;

            return new IteratedLocalSearchAligner(sumOfPairsWithAffine, iterations);
        }
    }
}
