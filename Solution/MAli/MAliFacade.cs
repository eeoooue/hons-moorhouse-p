using LibAlignment;
using LibAlignment.Aligners;
using LibBioInfo;
using LibFileIO;
using LibScoring;
using MAli.AlignmentConfigs;
using MAli.AlignmentEngines;
using MAli.ParetoAlignmentConfigs;
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

        private AlignmentEngine AlignmentEngine = new AlignmentEngine(Config);
        private ParetoAlignmentEngine ParetoAlignmentEngine = new ParetoAlignmentEngine(ParetoConfig);
        private BatchAlignmentEngine BatchAlignmentEngine = new BatchAlignmentEngine(Config);

        public void SetSeed(string value)
        {
            if (int.TryParse(value, out int seed))
            {
                Randomizer.SetSeed(seed);
            }
        }

        public void PerformAlignment(AlignmentRequest instructions)
        {
            AlignmentEngine.PerformAlignment(instructions);
        }

        public void PerformParetoAlignment(AlignmentRequest instructions)
        {
            ParetoAlignmentEngine.PerformAlignment(instructions);
        }

        public void PerformBatchAlignment(AlignmentRequest instructions)
        {
            BatchAlignmentEngine.PerformAlignment(instructions);
        }

        public void ProvideHelp()
        {
            ResponseBank.ProvideHelp();
        }

        public void ProvideInfo()
        {
            ResponseBank.ProvideInfo();
        }

        public void NotifyUserError(UserRequestError error)
        {
            ResponseBank.NotifyUserError(error);
        }
    }
}
