﻿using LibAlignment;
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
        public static AlignmentConfig Config = new DevelopmentConfig();
        public static ParetoAlignmentConfig ParetoConfig = new ParetoDevConfig();

        private ResponseBank ResponseBank = new ResponseBank();

        public void SetSeed(string value)
        {
            if (int.TryParse(value, out int seed))
            {
                Randomizer.SetSeed(seed);
            }
        }

        public void PerformAlignment(AlignmentRequest request)
        {
            if (request.SpecifiesSeed)
            {
                SetSeed(request.Seed);
            }

            IAlignmentEngine engine = ConstructEngine(request);
            engine.PerformAlignment(request);
        }

        private IAlignmentEngine ConstructEngine(AlignmentRequest request)
        {
            switch (request)
            {
                case ParetoAlignmentRequest:
                    return new ParetoAlignmentEngine(ParetoConfig);
                case BatchAlignmentRequest:
                    return new BatchAlignmentEngine(Config);
                default:
                    return new AlignmentEngine(Config);
            }
        }

        public void ProvideHelp()
        {
            ResponseBank.ProvideHelp();
        }

        public void NotifyUserError(UserRequestError error)
        {
            ResponseBank.NotifyUserError(error);
        }
    }
}
