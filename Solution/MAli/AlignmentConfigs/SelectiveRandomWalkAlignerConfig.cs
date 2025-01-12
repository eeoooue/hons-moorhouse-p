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
    public class SelectiveRandomWalkAlignerConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetVersion02();
        }

        private IterativeAligner GetVersion01()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            const int maxIterations = 1000;
            SelectiveRandomWalkAligner aligner = new SelectiveRandomWalkAligner(objective, maxIterations);
            return aligner;
        }

        private IterativeAligner GetVersion02()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);
            const int maxIterations = 1000;
            SelectiveRandomWalkAligner aligner = new SelectiveRandomWalkAligner(objective, maxIterations);
            return aligner;
        }
    }
}
