using LibAlignment.Aligners.SingleState;
using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibParetoAlignment;
using LibParetoAlignment.Aligners;
using LibScoring;
using LibScoring.FitnessFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.ParetoAlignmentConfigs
{
    public class ParetoDevConfig : ParetoAlignmentConfig
    {

        public IFitnessFunction GetObjectiveOne()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IFitnessFunction result = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(blosum);
            return result;
        }

        public IFitnessFunction GetObjectiveTwo()
        {
            IFitnessFunction result = new TotallyConservedColumnsFitnessFunction();
            return result;
        }

        public IFitnessFunction GetObjectiveThree()
        {
            IFitnessFunction result = new NonGapsFitnessFunction();
            return result;
        }

        public List<IFitnessFunction> GetObjectives()
        {
            List<IFitnessFunction> result = new List<IFitnessFunction>()
            {
                GetObjectiveOne(),
                GetObjectiveTwo(),
                GetObjectiveThree(),
            };

            return result;
        }

        public override ParetoIterativeAligner CreateAligner()
        {
            List<IFitnessFunction> objectives = GetObjectives();
            ParetoIterativeAligner aligner = new ParetoHillClimbAligner(objectives);

            return aligner;
        }
    }
}
