using LibAlignment;
using LibAlignment.Aligners;
using LibBioInfo;
using LibFileIO;
using LibScoring;
using MAli.AlignmentConfigs;
using MAli.AlignmentEngines;
using MAli.ParetoAlignmentConfigs;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public class MAliFacade
    {
        public AlignmentConfig Config = new Sprint08Config();
        public ParetoAlignmentConfig ParetoConfig = new ParetoDevConfig();

        public void CheckSetSeed(AlignmentRequest request)
        {
            if (request.SpecifiesSeed)
            {
                if (int.TryParse(request.Seed, out int seed))
                {
                    Randomizer.SetSeed(seed);
                }
            }
        }

        public void CheckCustomConfig(AlignmentRequest request)
        {
            if (request.SpecifiesCustomConfig)
            {
                Config = new UserConfig(Config, request.ConfigPath);
            }
        }

        public void PerformAlignment(AlignmentRequest request)
        {
            CheckSetSeed(request);
            CheckCustomConfig(request);

            IAlignmentEngine engine = ConstructEngine(request);
            engine.PerformAlignment(request);
            Console.WriteLine();
        }

        private IAlignmentEngine ConstructEngine(AlignmentRequest request)
        {
            AlignmentConfig config = Config;
            if (request.RefineOnly)
            {
                config = new RefinementConfig();
            }

            switch (request)
            {
                case ParetoAlignmentRequest:
                    return new ParetoAlignmentEngine(ParetoConfig);
                case BatchAlignmentRequest:
                    return new BatchAlignmentEngine(config);
                default:
                    return new AlignmentEngine(config);
            }
        }
    }
}
