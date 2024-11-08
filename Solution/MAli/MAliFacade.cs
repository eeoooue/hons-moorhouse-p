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
        private FileHelper FileHelper = new FileHelper();
        private ResponseBank ResponseBank = new ResponseBank();

        public void PerformAlignment(string inputPath, string outputPath)
        {
            Console.WriteLine($"Performing Multiple Sequence Alignment:");

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences);

                if (alignment.SequencesCanBeAligned())
                {
                    IAlignmentModifier modifier = new AlignmentRandomizer();
                    modifier.ModifyAlignment(alignment);
                    FileHelper.WriteAlignmentTo(alignment, outputPath);
                    Console.WriteLine($"Alignment written to destination: '{outputPath}'");
                }
                else
                {
                    Console.WriteLine("Error: Sequences cannot be aligned.");
                }
            }
            catch (Exception e)
            {
                ResponseBank.ExplainException(e);
            }
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
