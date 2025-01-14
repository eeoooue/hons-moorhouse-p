using LibAlignment;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAlignment.Aligners.SingleState;

namespace MAli.AlignmentConfigs
{
    internal class NaiveHillClimbAlignerConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetVersion01();
        }

        private IterativeAligner GetVersion01()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            const int maxIterations = 10;
            NaiveHillClimbAligner aligner = new NaiveHillClimbAligner(objective, maxIterations);
            return aligner;
        }
    }
}
