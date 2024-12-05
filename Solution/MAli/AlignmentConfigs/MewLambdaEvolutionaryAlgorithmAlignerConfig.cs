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

namespace MAli.AlignmentConfigs
{
    public class MewLambdaEvolutionaryAlgorithmAlignerConfig : AlignmentConfig
    {
        public override MewLambdaEvolutionaryAlgorithmAligner CreateAligner()
        {
            return GetVersion01();
        }

        private MewLambdaEvolutionaryAlgorithmAligner GetVersion01()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);
            const int maxIterations = 100;
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations);
            aligner.Lambda = 20;
            aligner.Mew = 5;

            return aligner;
        }
    }
}
