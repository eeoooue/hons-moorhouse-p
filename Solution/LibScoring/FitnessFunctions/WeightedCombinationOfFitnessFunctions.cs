using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.FitnessFunctions
{
    public class WeightedCombinationOfFitnessFunctions : IFitnessFunction
    {
        List<IFitnessFunction> Functions = new List<IFitnessFunction>();
        List<double> Weights = new List<double>();

        public WeightedCombinationOfFitnessFunctions(List<IFitnessFunction> functions, List<double> weights)
        {
            Functions = functions;
            Weights = weights;
        }

        public double GetFitness(in char[,] alignment)
        {
            double totalScore = 0.0;
            double maxPossibleScore = 0.0;

            for(int i=0; i<Functions.Count; i++)
            {
                IFitnessFunction function = Functions[i];
                double weight = Weights[i];

                totalScore += function.GetFitness(in alignment);
                maxPossibleScore += 1.0;
            }

            return totalScore / maxPossibleScore;
        }

        public double GetFitness(in char[,] alignment, NormalisedFitnessFunction function, double weight)
        {
            return function.ScoreAlignment(in alignment) * weight;
        }

        public string GetName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Weighted Combination:");

            for(int i=0; i<Functions.Count; i++)
            {
                string function = $"{Weights[i]}({Functions[i].GetName()})";
                sb.Append(" ");
                sb.Append(function);
            }

            return sb.ToString();
        }

        public string GetAbbreviation()
        {
            return "WeightedCombo";
        }
    }
}
