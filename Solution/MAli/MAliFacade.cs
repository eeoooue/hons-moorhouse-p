using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public class MAliFacade
    {
        private MAliSpecification Specification = new MAliSpecification();
        private FileHelper FileHelper = new FileHelper();
        private ResponseBank ResponseBank = new ResponseBank();

        public void PerformAlignment(string inputPath, string outputPath)
        {
            Console.WriteLine($"Performing Multiple Sequence Alignment (low quality - produced randomly):");
            Console.WriteLine($" - specified source: {inputPath}");
            Console.WriteLine($" - specified destination: {outputPath}");

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
            Alignment alignment = new Alignment(sequences);
            IAlignmentModifier modifier = new AlignmentRandomizer();
            modifier.ModifyAlignment(alignment);
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

        public void NotifyUserError(UserRequestError error)
        {
            ResponseBank.NotifyUserError(error);
        }
    }
}
