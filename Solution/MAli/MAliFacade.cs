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
        public AlignmentConfig Config = new DevelopmentConfig();

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
            bool debugging = CommandsIncludeFlag(table, "debug");
            bool emitFrames = CommandsIncludeFlag(table, "frames");
            bool refineOnly = CommandsIncludeFlag(table, "refine");

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    IIterativeAligner aligner = Config.CreateAligner();

                    if (debugging && aligner is IterativeAligner instance)
                    {
                        aligner = new DebuggingWrapper(instance);
                    }

                    int iterations = UnpackSpecifiedIterations(table);
                    if (iterations > 0)
                    {
                        aligner.IterationsLimit = iterations;
                    }

                    if (refineOnly)
                    {
                        aligner.InitializeForRefinement(alignment);
                    }
                    else
                    {
                        aligner.Initialize(alignment.Sequences);
                    }

                    AlignIteratively(aligner, emitFrames);

                    string outputFilename = BuildFullOutputFilename(outputPath, table);

                    FileHelper.WriteAlignmentTo(aligner.CurrentAlignment!, outputFilename);
                    Console.WriteLine($"Alignment written to destination: '{outputFilename}'");
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

        public bool CommandsIncludeFlag(Dictionary<string, string?> table, string flag)
        {
            bool result = table.ContainsKey(flag);
            return result;
        }

        public void AlignIteratively(IIterativeAligner aligner, bool emitFrames)
        {
            Console.WriteLine($"Performing Multiple Sequence Alignment: {aligner.IterationsLimit} iterations.");

            if (emitFrames)
            {
                CheckCreateFramesFolder();
            }

            while(aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                aligner.Iterate();
                if (emitFrames && aligner.CurrentAlignment is Alignment alignment)
                {
                    SaveCurrentFrame(alignment, aligner.IterationsCompleted);
                }
            }
        }

        public void SaveCurrentFrame(Alignment alignment, int iterations)
        {
            string suffix = Frontload(iterations);
            FileHelper.WriteAlignmentTo(alignment, $"frames\\frame_{suffix}");
        }

        public string Frontload(int number)
        {
            string s = number.ToString();

            StringBuilder sb = new StringBuilder();
            for(int i=0; i<5-s.Length; i++)
            {
                sb.Append('0');
            }
            sb.Append(s);

            return sb.ToString();
        }

        public void CheckCreateFramesFolder()
        {
            Directory.CreateDirectory("frames");
        }

        public int UnpackSpecifiedIterations(Dictionary<string, string?> table)
        {
            if (table.ContainsKey("iterations"))
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

            if (CommandsIncludeFlag(table, "timestamp"))
            {
                result += $"_{GetTimeStamp()}";
            }

            if (CommandsIncludeFlag(table, "tag"))
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
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
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
