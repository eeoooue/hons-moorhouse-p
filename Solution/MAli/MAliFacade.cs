using LibAlignment;
using LibAlignment.Aligners;
using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibFileIO;
using LibScoring;
using MAli.AlignmentConfigs;
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
        public AlignmentConfig Config = new SelectiveRandomWalkAlignerConfig();

        public void SetSeed(string value)
        {
            try
            {
                int seedValue = int.Parse(value);
                Randomizer.SetSeed(seedValue);
            }
            catch
            {
                return;
            }
        }

        public void PerformAlignment(string inputPath, string outputPath, int iterations=0)
        {

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences);

                if (alignment.SequencesCanBeAligned())
                {
                    Aligner aligner = Config.CreateAligner();
                    if (iterations > 0)
                    {
                        aligner.IterationsLimit = iterations;
                    }

                    Console.WriteLine($"Performing Multiple Sequence Alignment: {aligner.IterationsLimit} iterations.");

                    alignment = aligner.AlignSequences(sequences);
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
