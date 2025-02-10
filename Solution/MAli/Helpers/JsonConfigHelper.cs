using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using LibScoring.FitnessFunctions;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class JsonConfigHelper
    {
        public JsonElement ReadConfigFrom(string filename)
        {
            string text = File.ReadAllText(filename);
            JsonDocument document = JsonDocument.Parse(text);

            return document.RootElement;
        }

        public IFitnessFunction ExtractObjective(JsonElement element)
        {
            string name = element.GetProperty("Name").GetString()!;

            switch (name)
            {
                case "WeightedCombination":
                    return ExtractWeightedCombination(element);
                case "AffineGapPenalties":
                    return ExtractAffineGapPenalties(element);
                case "SumOfPairs":
                    return ExtractSumOfPairs(element);
                case "SumOfPairsWithAffineGapPenalties":
                    return ExtractSumOfPairsWithAffineGapPenalties(element);
                case "PercentTCCs":
                    return new TotallyConservedColumnsFitnessFunction();
                case "PercentNonGaps":
                    return new NonGapsFitnessFunction();
                default:
                    throw new KeyNotFoundException($"Failed to find matching objective function for key: '{name}'.");
            }
        }

        private IFitnessFunction ExtractWeightedCombination(JsonElement element)
        {
            List<IFitnessFunction> objectives = new List<IFitnessFunction>();
            JsonElement objElement = element.GetProperty("Objectives");
            foreach (JsonElement objChild in objElement.EnumerateArray())
            {
                IFitnessFunction objective = ExtractObjective(objChild);
                objectives.Add(objective);

            }

            List<double> weights = new List<double>();
            JsonElement weightsElement = element.GetProperty("Weights");
            foreach (JsonElement weightChild in weightsElement.EnumerateArray())
            {
                weights.Add(weightChild.GetDouble());
            }

            return new WeightedCombinationOfFitnessFunctions(objectives, weights);
        }

        private IFitnessFunction ExtractSumOfPairs(JsonElement element)
        {
            string matrixValue = element.GetProperty("Matrix").GetString()!;
            IScoringMatrix matrix = GetMatchingMatrix(matrixValue);
            return new SumOfPairsFitnessFunction(matrix);
        }

        private IFitnessFunction ExtractAffineGapPenalties(JsonElement element)
        {
            double openingCost = element.GetProperty("OpeningCost").GetDouble()!;
            double nullCost = element.GetProperty("NullCost").GetDouble()!;
            return new AffineGapPenaltyFitnessFunction(openingCost, nullCost);
        }

        private IFitnessFunction ExtractSumOfPairsWithAffineGapPenalties(JsonElement element)
        {
            string matrixValue = element.GetProperty("Matrix").GetString()!;
            double openingCost = element.GetProperty("OpeningCost").GetDouble()!;
            double nullCost = element.GetProperty("NullCost").GetDouble()!;

            IScoringMatrix matrix = GetMatchingMatrix(matrixValue);
            return new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, openingCost, nullCost);
        }

        private IScoringMatrix GetMatchingMatrix(string value)
        {
            switch (value)
            {
                case "BLOSUM62":
                    return new BLOSUM62Matrix();
                case "PAM250":
                    return new PAM250Matrix();
                default:
                    return new IdentityMatrix();
            }
        }
    }
}
