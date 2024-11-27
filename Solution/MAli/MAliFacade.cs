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

        public void PerformAlignment(string inputPath, string outputPath, Dictionary<string, string?> table)
        {

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences);

                if (alignment.SequencesCanBeAligned())
                {
                    Aligner aligner = Config.CreateAligner();
                    int iterations = UnpackSpecifiedIterations(table);
                    if (iterations > 0)
                    {
                        aligner.IterationsLimit = iterations;
                    }

                    Console.WriteLine($"Performing Multiple Sequence Alignment: {aligner.IterationsLimit} iterations.");
                    alignment = aligner.AlignSequences(sequences);

                    string outputFilename = BuildFullOutputFilename(outputPath, table);

                    FileHelper.WriteAlignmentTo(alignment, outputFilename);
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

        public int UnpackSpecifiedIterations(Dictionary<string, string?> table)
        {
            if (table.ContainsKey(""))
            {
                string? iterationsValue = table["iterations"];

                if (iterationsValue is string iterations)
                {
                    int result = 0;
                    if (int.TryParse(iterations, out result))
                    {
                        return result;
                    }
                }
            }

            return 0;
        }


        public string BuildFullOutputFilename(string outputName, Dictionary<string, string?> table)
        {
            string result = outputName;

            if (table.ContainsKey("timestamp"))
            {
                result += GetTimeStamp();
            }

            if (table.ContainsKey("tag"))
            {
                string? specifiedTag = table["tag"];
                if (specifiedTag is string tag)
                {
                    result += $"_{tag}";
                }
            }

            result += ".faa";

            return result;
        }

        public string GetTimeStamp()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss");
            return timestamp;
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
