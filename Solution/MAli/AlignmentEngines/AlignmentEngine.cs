using LibAlignment;
using LibBioInfo;
using LibFileIO;
using LibFileIO.AlignmentWriters;
using MAli.Helpers;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentEngines
{
    public class AlignmentEngine : IAlignmentEngine
    {
        private FileHelper FileHelper = new FileHelper();
        private FrameHelper FrameHelper = new FrameHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private AlignmentConfig Config;
        private DebuggingHelper DebuggingHelper = new DebuggingHelper();

        private bool DebugMode = false;
        private AlignmentRequest Instructions = null!;

        public AlignmentEngine(AlignmentConfig config)
        {
            Config = config;
        }

        public void PerformAlignment(AlignmentRequest instructions)
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

        public void CheckSaveScorefile(IterativeAligner aligner, Alignment alignment, AlignmentRequest instructions)
        {
            if (!instructions.IncludeScoreFile)
            {
                return;
            }

            MAliScoreWriter writer = new MAliScoreWriter(aligner.Objective);
            writer.WriteAlignmentTo(alignment, instructions.OutputPath);
            Console.WriteLine("Saved .maliscore file.");
        }

        public void AlignIteratively(IterativeAligner aligner, AlignmentRequest instructions)
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

        public void AlignUntilIterationLimit(IterativeAligner aligner, AlignmentRequest instructions)
        {
            while (aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                if (DebugMode)
                {
                    DebuggingHelper.ProgressContext = GetIterationProgressContext(aligner, instructions);
                    DebuggingHelper.ShowDebuggingInfo(aligner);
                }
                PerformIterationOfAlignment(aligner, instructions);
            }

            if (DebugMode)
            {
                DebuggingHelper.ProgressContext = GetIterationProgressContext(aligner, instructions);
                DebuggingHelper.ShowDebuggingInfo(aligner);
            }
        }

        public string GetIterationProgressContext(IterativeAligner aligner, AlignmentRequest instructions)
        {
            int completed = aligner.IterationsCompleted;
            int limit = aligner.IterationsLimit;

            double percentIterationsComplete = Math.Round(100.0 * completed / limit, 3);
            string percentValue = percentIterationsComplete.ToString("0.0");

            string result = $"completed {completed} of {limit} iterations ({percentValue}%)";

            return result;
        }

        public void AlignUntilSecondsDeadline(IterativeAligner aligner, AlignmentRequest instructions)
        {
            DateTime start = DateTime.Now;
            DateTime deadline = DateTime.Now.AddSeconds(instructions.SecondsLimit);

            DateTime time = DateTime.Now;

            while (DateTime.Now < deadline)
            {
                PerformIterationOfAlignment(aligner, instructions);
                time = DateTime.Now;
                if (DebugMode)
                {
                    DebuggingHelper.ProgressContext = GetTimelimitProgress(aligner, start, time, instructions.SecondsLimit);
                    DebuggingHelper.ShowDebuggingInfo(aligner);
                }
                if (time >= deadline)
                {
                    break;
                }
            }
        }

        public string GetTimelimitProgress(IterativeAligner aligner, DateTime start, DateTime time, double limit)
        {
            TimeSpan span = time - start;
            string result = $"completed {aligner.IterationsCompleted} iterations in {span.Seconds} of {limit} seconds";

            return result;
        }

        public void PerformIterationOfAlignment(IterativeAligner aligner, AlignmentRequest instructions)
        {
            aligner.Iterate();
            if (instructions.EmitFrames && aligner.CurrentAlignment is Alignment alignment)
            {
                FrameHelper.SaveCurrentFrame(alignment, aligner.IterationsCompleted);
            }
        }
    }
}
