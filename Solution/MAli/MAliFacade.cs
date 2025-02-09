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

        public void PerformAlignment(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            AlignmentHelper.PerformAlignment(inputPath, outputPath, table);
        }

        public void PerformParetoAlignment(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            throw new NotImplementedException();
        }

        public void PerformBatchAlignment(string inDirectory, string outDirectory, Dictionary<string, string?> table)
        {
            BatchAlignmentHelper.PerformBatchAlignment(inDirectory, outDirectory, table);
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
