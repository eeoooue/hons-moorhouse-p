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
    internal class IteratedLocalSearchConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetILSAligner();
        }

        public IterativeAligner GetILSAligner()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IObjectiveFunction sumOfPairsWithAffine = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(blosum);
            int iterations = 10000;

            return new IteratedLocalSearchAligner(sumOfPairsWithAffine, iterations);
        }
    }
}
