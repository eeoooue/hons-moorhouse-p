using LibBioInfo;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    internal class MAliFacade
    {
        private MAliSpecification Specification = new MAliSpecification();
        private FileHelper FileHelper = new FileHelper();

        public void PerformAlignment(string inputPath, string outputPath)
        {
            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
            Alignment alignment = new Alignment(sequences);
            FileHelper.WriteAlignmentTo(alignment, outputPath);
        }

        public void ProvideHelp()
        {
            Specification.ListCurrentVersion();
            Specification.ListSupportedCommands();
        }

        public void ProvideInfo()
        {
            Specification.ListCurrentVersion();
            Console.WriteLine($"For directions try 'readme.txt' or use '-help'");
        }
        
    }
}
