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
    public class MewLambdaEvolutionaryAlgorithmAlignerConfig : AlignmentConfig
    {
        public override MewLambdaEvolutionaryAlgorithmAligner CreateAligner()
        {
            return GetVersion01();
        }

        public MultiOperatorModifier ConstructMutationOperator()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GapInserter(1),
                new GapShifter(),
            };

            MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);
            return modifier;
        }

        private MewLambdaEvolutionaryAlgorithmAligner GetVersion01()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);
            const int maxIterations = 100;
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations);
            aligner.Mew = 10;
            aligner.Lambda = aligner.Mew * 7;
            aligner.MutationOperator = ConstructMutationOperator();

            return aligner;
        }
    }
}
