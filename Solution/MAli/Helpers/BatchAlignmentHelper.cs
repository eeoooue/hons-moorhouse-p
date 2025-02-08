using LibAlignment;
using LibBioInfo;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    internal class BatchAlignmentHelper
    {
        private FileHelper FileHelper = new FileHelper();
        private FrameHelper FrameHelper = new FrameHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();
        private AlignmentConfig Config;
        private AlignmentHelper AlignmentHelper;

        public BatchAlignmentHelper(AlignmentConfig config)
        {
            Config = config;
            AlignmentHelper = new AlignmentHelper(config);
        }

        public void PerformBatchAlignment(string inDirectory, string outDirectory, Dictionary<string, string?> table)
        {
            throw new NotImplementedException();
        }
    }
}
