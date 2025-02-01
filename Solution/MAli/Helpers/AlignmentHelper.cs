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
    internal class AlignmentHelper
    {
        private FileHelper FileHelper = new FileHelper();
        private FrameHelper FrameHelper = new FrameHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();
        private AlignmentConfig Config;

        public AlignmentHelper(AlignmentConfig config)
        {
            Config = config;
        }

        public AlignmentInstructions UnpackInstructions(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            AlignmentInstructions instructions = new AlignmentInstructions();
            instructions.Debug = ArgumentHelper.CommandsIncludeFlag(table, "debug");
            instructions.EmitFrames = ArgumentHelper.CommandsIncludeFlag(table, "frames");
            instructions.RefineOnly = ArgumentHelper.CommandsIncludeFlag(table, "refine");
            instructions.IterationsLimit = ArgumentHelper.UnpackSpecifiedIterations(table);
            instructions.SecondsLimit = ArgumentHelper.UnpackSpecifiedSeconds(table);
            instructions.InputPath = inputPath;
            instructions.OutputPath = BuildFullOutputFilename(outputPath, table);

            instructions.CheckAddDefaultRestrictions();

            return instructions;
        }

        public void PerformAlignment(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            AlignmentInstructions instructions = UnpackInstructions(inputPath, outputPath, table);

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    IIterativeAligner aligner = InitialiseAligner(alignment, instructions);
                    AlignIteratively(aligner, instructions);
                    FileHelper.WriteAlignmentTo(aligner.CurrentAlignment!, instructions.OutputPath);
                    Console.WriteLine($"Alignment written to destination: '{instructions.OutputPath}'");
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

        public IIterativeAligner InitialiseAligner(Alignment alignment, AlignmentInstructions instructions)
        {
            IIterativeAligner aligner = Config.CreateAligner();

            if (instructions.Debug && aligner is IterativeAligner instance)
            {
                aligner = new DebuggingWrapper(instance);
            }

            if (instructions.IterationsLimit > 0)
            {
                aligner.IterationsLimit = instructions.IterationsLimit;
            }
            else
            {
                aligner.IterationsLimit = instructions.IterationsLimit;
            }

            if (instructions.RefineOnly)
            {
                aligner.InitializeForRefinement(alignment);
            }
            else
            {
                aligner.Initialize(alignment.Sequences);
            }

            return aligner;
        }


        public void AlignIteratively(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            Console.WriteLine(instructions.GetContextString());

            if (instructions.EmitFrames)
            {
                FrameHelper.CheckCreateFramesFolder();
            }

            if (instructions.LimitedByIterations())
            {
                AlignUntilIterationLimit(aligner, instructions);
            }
            else if (instructions.LimitedBySeconds())
            {
                AlignUntilSecondsDeadline(aligner, instructions);
            }
        }

        public void AlignUntilIterationLimit(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            while (aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                PerformIterationOfAlignment(aligner, instructions);
            }
        }

        public void AlignUntilSecondsDeadline(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            DateTime deadline = DateTime.Now.AddSeconds(instructions.SecondsLimit);
            while (DateTime.Now < deadline)
            {
                PerformIterationOfAlignment(aligner, instructions);
            }
        }

        public void PerformIterationOfAlignment(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            aligner.Iterate();
            if (instructions.EmitFrames && aligner.CurrentAlignment is Alignment alignment)
            {
                FrameHelper.SaveCurrentFrame(alignment, aligner.IterationsCompleted);
            }
        }

        public string BuildFullOutputFilename(string outputName, Dictionary<string, string?> table)
        {
            string result = outputName;
            if (ArgumentHelper.CommandsIncludeFlag(table, "timestamp"))
            {
                result += $"_{GetTimeStamp()}";
            }
            if (ArgumentHelper.CommandsIncludeFlag(table, "tag"))
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
    }
}
