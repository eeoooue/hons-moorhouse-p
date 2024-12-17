using LibAlignment;
using LibAlignment.Aligners;
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
    internal class DebuggingConfig : AlignmentConfig
    {
        public override Aligner CreateAligner()
        {
            return GetCurrentAligner();
        }

        private Aligner GetCurrentAligner()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);
            const int maxIterations = 100;

            ElitistGeneticAlgorithmAligner aligner = new ElitistGeneticAlgorithmAligner(objective, maxIterations);
            aligner.PopulationSize = 100;
            aligner.SelectionSize = 50;

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GapInserter(12),
                new SwapOperator(),
            };

            MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);
            aligner.MutationOperator = modifier;

            return aligner;
        }

    }
}
