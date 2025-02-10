using LibAlignment;
using LibAlignment.Aligners.SingleState;
using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibModification;
using LibScoring;
using LibScoring.FitnessFunctions;
using MAli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MAli.AlignmentConfigs
{
    public class CustomConfig : AlignmentConfig
    {
        // allows the user to provide a customized configuration of objectives

        private AlignmentConfig BaseConfig;
        private JsonConfigHelper Helper = new JsonConfigHelper();

        public CustomConfig(AlignmentConfig baseConfig)
        {
            BaseConfig = baseConfig;
        }

        public override IterativeAligner CreateAligner()
        {
            IterativeAligner aligner = BaseConfig.CreateAligner();
            JsonElement root = Helper.ReadConfigFrom("config.json");
            JsonElement objElement = root.GetProperty("Objective");
            aligner.Objective = ExtractObjective(objElement);

            return aligner;
        }

        public IFitnessFunction ExtractObjective(JsonElement element)
        {
            string name = element.GetProperty("Name").GetString()!;

            if (name == "WeightedCombination")
            {
                List<IFitnessFunction> objectives = new List<IFitnessFunction>();
                JsonElement objElement = element.GetProperty("Objectives");
                foreach(JsonElement objChild in objElement.EnumerateArray())
                {
                    IFitnessFunction objective = ExtractObjective(objChild);
                    objectives.Add(objective);

                }

                List<double> weights = new List<double>();
                JsonElement weightsElement = element.GetProperty("Weights");
                foreach(JsonElement weightChild in weightsElement.EnumerateArray())
                {
                    weights.Add(weightsElement.GetDouble());
                }

                return new WeightedCombinationOfFitnessFunctions(objectives, weights);
            }

            if (name == "SumOfPairs")
            {
                string matrixValue = element.GetProperty("Matrix").GetString()!;
                IScoringMatrix matrix = GetMatchingMatrix(matrixValue);
                return new SumOfPairsFitnessFunction(matrix);
            }

            if (name == "PercentTCCs")
            {
                return new TotallyConservedColumnsFitnessFunction();
            }

            if (name == "PercentNonGaps")
            {
                return new NonGapsFitnessFunction();
            }

            throw new KeyNotFoundException("Failed to find matching objective function.");
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
