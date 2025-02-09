using LibAlignment;
using LibAlignment.Aligners;
using LibBioInfo;
using LibFileIO;
using LibScoring;
using MAli.AlignmentConfigs;
using MAli.Helpers;
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

        private AlignmentHelper AlignmentHelper = new AlignmentHelper(Config);
        private ParetoAlignmentHelper ParetoAlignmentHelper = new ParetoAlignmentHelper(ParetoConfig);

        private BatchAlignmentHelper BatchAlignmentHelper = new BatchAlignmentHelper(Config);


        public void SetSeed(string value)
        {
            if (int.TryParse(value, out int seed))
            {
                Randomizer.SetSeed(seed);
            }
        }

        public void PerformAlignment(AlignmentInstructions instructions)
        {
            AlignmentHelper.PerformAlignment(instructions);
        }

        public void PerformParetoAlignment(AlignmentInstructions instructions)
        {
            ParetoAlignmentHelper.PerformAlignment(instructions);
        }

        public void PerformBatchAlignment(AlignmentInstructions instructions)
        {
            BatchAlignmentHelper.PerformBatchAlignment(instructions);
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
