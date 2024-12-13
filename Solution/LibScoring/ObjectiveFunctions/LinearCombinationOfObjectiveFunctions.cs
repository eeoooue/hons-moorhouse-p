using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ObjectiveFunctions
{
    public class LinearCombinationOfObjectiveFunctions : IObjectiveFunction
    {
        public List<IObjectiveFunction> Objectives;
        public List<double> Weights;
        public string Title = "Linear Combination of Weights";

        public LinearCombinationOfObjectiveFunctions(List<IObjectiveFunction> objectives, List<double> weights)
        {
            Objectives = objectives;
            Weights = weights;
        }

        public string GetName()
        {
            return Title;
        }

        public double ScoreAlignment(Alignment alignment)
        {
            double result = 0;
            for(int i=0; i<Objectives.Count; i++)
            {
                result += GetWeightedScore(alignment, Objectives[i], Weights[i]);
            }

            return result;
        }

        public double GetWeightedScore(Alignment alignment, IObjectiveFunction objective, double weight)
        {
            return objective.ScoreAlignment(alignment) * weight;
        }
    }
}
