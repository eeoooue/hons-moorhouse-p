using LibAlignment;
using LibBioInfo;
using LibFileIO;
using LibFileIO.AlignmentWriters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class AlignmentHelper
    {
        private FileHelper FileHelper = new FileHelper();
        private FrameHelper FrameHelper = new FrameHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();
        private AlignmentConfig Config;
        private DebuggingHelper DebuggingHelper = new DebuggingHelper();

        private bool DebugMode = false;
        private AlignmentInstructions Instructions = null!;

        public AlignmentHelper(AlignmentConfig config)
        {
            Config = config;
        }

        public void PerformAlignment(AlignmentInstructions instructions)
        {
            Instructions = instructions;
            DebuggingHelper = new DebuggingHelper();
            DebugMode = Instructions.Debug;

            try
            {
                Console.WriteLine($"Reading sequences from source: '{Instructions.InputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(Instructions.InputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    IterativeAligner aligner = Config.InitialiseAligner(alignment, Instructions);
                    AlignIteratively(aligner, Instructions);
                    SaveAlignment(aligner.CurrentAlignment!, Instructions.OutputPath);
                    CheckSaveScorefile(aligner, aligner.CurrentAlignment!, Instructions);
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

        public void SaveAlignment(Alignment alignment, string filepath)
        {
            FileHelper.WriteAlignmentTo(alignment, filepath);
            Console.WriteLine($"Alignment written to destination: '{filepath}'");
        }

        public void CheckSaveScorefile(IterativeAligner aligner, Alignment alignment, AlignmentInstructions instructions)
        {
            if (!instructions.IncludeScoreFile)
            {
                return;
            }

            MAliScoreWriter writer = new MAliScoreWriter(aligner.Objective);
            writer.WriteAlignmentTo(alignment, instructions.OutputPath);
            Console.WriteLine("Saved .maliscore file.");
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

            if (DebugMode && aligner is IterativeAligner obj)
            {
                DebuggingHelper.ShowDebuggingInfo(obj);
            }
        }

        public void AlignUntilIterationLimit(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            while (aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                if (aligner is IterativeAligner obj)
                {
                    DebuggingHelper.ProgressContext = GetIterationProgressContext(aligner, instructions);
                    DebuggingHelper.ShowDebuggingInfo(obj);
                }
                PerformIterationOfAlignment(aligner, instructions);
            }

            if (aligner is IterativeAligner obj2)
            {
                DebuggingHelper.ProgressContext = GetIterationProgressContext(aligner, instructions);
                DebuggingHelper.ShowDebuggingInfo(obj2);
            }
        }

        public string GetIterationProgressContext(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            int completed = aligner.IterationsCompleted;
            int limit = aligner.IterationsLimit;

            double percentIterationsComplete = Math.Round(100.0 * (double)completed / (double)limit, 3);
            string percentValue = percentIterationsComplete.ToString("0.0");

            string result = $"completed {completed} of {limit} iterations ({percentValue}%)";

            return result;
        }

        public void AlignUntilSecondsDeadline(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            DateTime start = DateTime.Now;
            DateTime deadline = DateTime.Now.AddSeconds(instructions.SecondsLimit);

            DateTime time = DateTime.Now;

            while (DateTime.Now < deadline)
            {
                PerformIterationOfAlignment(aligner, instructions);
                time = DateTime.Now;
                if (aligner is IterativeAligner obj)
                {
                    DebuggingHelper.ProgressContext = GetTimelimitProgress(aligner, start, time, instructions.SecondsLimit);
                    DebuggingHelper.ShowDebuggingInfo(obj);
                }
                if (time >= deadline)
                {
                    break;
                }
            }
        }

        public string GetTimelimitProgress(IIterativeAligner aligner, DateTime start, DateTime time, double limit)
        {
            TimeSpan span = time - start;
            string result = $"completed {aligner.IterationsCompleted} iterations in {span.Seconds} of {limit} seconds";

            return result;
        }

        public void PerformIterationOfAlignment(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            aligner.Iterate();
            if (instructions.EmitFrames && aligner.CurrentAlignment is Alignment alignment)
            {
                FrameHelper.SaveCurrentFrame(alignment, aligner.IterationsCompleted);
            }
        }
    }
}
