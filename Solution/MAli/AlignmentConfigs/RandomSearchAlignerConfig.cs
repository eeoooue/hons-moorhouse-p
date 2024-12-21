using LibAlignment;
using LibAlignment.Aligners;
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
    public class RandomSearchAlignerConfig : AlignmentConfig
    {
        public override Aligner CreateAligner()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IObjectiveFunction sumOfPairsWithAffine = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(blosum);
            int iterations = 1000;

            return new RandomSearchAligner(sumOfPairsWithAffine, iterations);
        }
    }
}
