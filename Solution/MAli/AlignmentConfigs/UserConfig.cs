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
        private JsonDocument Document;

        public CustomConfig(JsonDocument document)
        {
            Document = document;
        }

        public override IterativeAligner CreateAligner()
        {
            IFitnessFunction objective = ExtractObjective(Document);
            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, 1000);
            aligner.TweakModifier = ExtractModifier(Document);

            return aligner;
        }

        public IFitnessFunction ExtractObjective(JsonDocument document)
        {
            throw new NotImplementedException();
        }

        public IAlignmentModifier ExtractModifier(JsonDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
