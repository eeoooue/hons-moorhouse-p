﻿using LibAlignment.Aligners;
using LibAlignment;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibBioInfo.IAlignmentModifiers;
using LibBioInfo;

namespace MAli.AlignmentConfigs
{
    public class GeneticAlgorithmAlignerConfig : AlignmentConfig
    {
        public override Aligner CreateAligner()
        {
            return GetVersion01();
        }

        public MultiOperatorModifier ConstructMutationOperator()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GapInserter(8),
                new GapShifter(),
                new MultiRowStochasticGapShifter(),
                new PercentileGapShifter(),
            };

            MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);
            return modifier;
        }

        private Aligner GetVersion01()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);
            const int maxIterations = 100;
            GeneticAlgorithmAligner aligner = new GeneticAlgorithmAligner(objective, maxIterations);
            aligner.PopulationSize = 100;
            aligner.MutationOperator = ConstructMutationOperator();

            return aligner;
        }
    }
}
