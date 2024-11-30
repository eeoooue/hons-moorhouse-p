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

namespace MAli.AlignmentConfigs
{
    public class GeneticAlgorithmAlignerConfig : AlignmentConfig
    {
        public override Aligner CreateAligner()
        {
            return GetVersion01();
        }

        private Aligner GetVersion01()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            const int maxIterations = 100;
            GeneticAlgorithmAligner aligner = new GeneticAlgorithmAligner(objective, maxIterations);
            aligner.PopulationSize = 16;
            aligner.SelectionSize = 8;

            return aligner;
        }
    }
}
