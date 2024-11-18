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
    public class NaiveHillClimbConfig : AlignmentConfig
    {
        public override Aligner CreateAligner()
        {
            return GetVersion01();
        }

        private Aligner GetVersion01()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            const int maxIterations = 10000;
            NaiveHillClimbAligner aligner = new NaiveHillClimbAligner(objective, maxIterations);
            return aligner;
        }
    }
}
