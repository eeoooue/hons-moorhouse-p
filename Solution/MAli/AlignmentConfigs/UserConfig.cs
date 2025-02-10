using LibAlignment;
using LibAlignment.Aligners.SingleState;
using LibModification;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAli.AlignmentConfigs
{
    public class CustomConfig : AlignmentConfig
    {
        // allows the user to provide a customized configuration of objectives

        private AlignmentConfig BaseConfig;
        private JsonDocument Document;

        public CustomConfig(AlignmentConfig baseConfig, JsonDocument document)
        {
            BaseConfig = baseConfig;
            Document = document;
        }

        public override IterativeAligner CreateAligner()
        {
            IterativeAligner aligner = BaseConfig.CreateAligner();
            aligner.Objective = ExtractObjective(Document);

            return aligner;
        }

        public IFitnessFunction ExtractObjective(JsonDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
