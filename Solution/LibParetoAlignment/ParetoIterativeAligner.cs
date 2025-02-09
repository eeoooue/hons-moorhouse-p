using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment
{
    public abstract class ParetoIterativeAligner
    {
        List<IFitnessFunction> Objectives;

        public ParetoIterativeAligner(List<IFitnessFunction> objectives)
        {
            Objectives = objectives;
        }


        public abstract List<Alignment> CollectTradeoffSolutions();


        public TradeoffAlignment EvaluateAlignment(Alignment alignment)
        {
            TradeoffAlignment result = new TradeoffAlignment(alignment);
            foreach(IFitnessFunction objective in Objectives)
            {
                string name = objective.GetName();
                double score = objective.GetFitness(alignment.CharacterMatrix);
                result.SetScore(name, score);
            }

            return result;
        }
    }
}
