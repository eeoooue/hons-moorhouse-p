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
    public class UserConfig : AlignmentConfig
    {
        // allows the user to provide a customized configuration of objectives

        private AlignmentConfig BaseConfig;
        private JsonConfigHelper Helper = new JsonConfigHelper();

        public UserConfig(AlignmentConfig baseConfig)
        {
            BaseConfig = baseConfig;
        }

        public override IterativeAligner CreateAligner()
        {
            IterativeAligner aligner = BaseConfig.CreateAligner();
            JsonElement root = Helper.ReadConfigFrom("config.json");
            JsonElement objElement = root.GetProperty("Objective");
            aligner.Objective = Helper.ExtractObjective(objElement);

            return aligner;
        }
    }
}
