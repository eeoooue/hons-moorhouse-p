using LibAlignment;
using LibBioInfo;
using LibFileIO;
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

        public AlignmentHelper(AlignmentConfig config)
        {
            Config = config;
        }

        public void PerformAlignment(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            AlignmentInstructions instructions = ArgumentHelper.UnpackInstructions(inputPath, outputPath, table);

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    IIterativeAligner aligner = Config.InitialiseAligner(alignment, instructions);
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

            if (aligner is DebuggingWrapper wrapper)
            {
                wrapper.ShowDebuggingInfo();
            }
        }

        public void AlignUntilIterationLimit(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            while (aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                if (aligner is DebuggingWrapper debug)
                {
                    debug.ProgressContext = GetIterationProgressContext(aligner, instructions);
                }
                PerformIterationOfAlignment(aligner, instructions);
            }

            if (aligner is DebuggingWrapper wrapper)
            {
                wrapper.ProgressContext = GetIterationProgressContext(aligner, instructions);
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
                if (aligner is DebuggingWrapper debug)
                {
                    debug.ProgressContext = GetTimelimitProgress(aligner, start, time, instructions.SecondsLimit);
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
