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
    internal class DevelopmentConfig : AlignmentConfig
    {
        public override Aligner CreateAligner()
        {
            return GetHCRandomRestarts();
        }

        public Aligner GetRandomSearchAligner()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IObjectiveFunction sumOfPairsWithAffine = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(blosum);
            int iterations = 1000;

            return new RandomSearchAligner(sumOfPairsWithAffine, iterations);
        }

        public Aligner GetHCRandomRestarts()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IObjectiveFunction sumOfPairsWithAffine = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(blosum);
            int iterations = 1000;

            return new HillClimbWithRandomRestartsAligner(sumOfPairsWithAffine, iterations);
        }
    }
}
