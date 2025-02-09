using LibAlignment;
using LibBioInfo;
using LibFileIO;
using LibParetoAlignment;
using MAli.ParetoAlignmentConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class ParetoAlignmentHelper
    {
        private FileHelper FileHelper = new FileHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();
        private ParetoDebuggingHelper DebuggingHelper = new ParetoDebuggingHelper();
        private ParetoAlignmentConfig Config;

        private bool DebugMode = false;

        public ParetoAlignmentHelper(ParetoAlignmentConfig config)
        {
            Config = config;
        }

        public void PerformAlignment(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            AlignmentInstructions instructions = ArgumentHelper.UnpackInstructions(inputPath, outputPath, table);
            DebuggingHelper = new ParetoDebuggingHelper();
            DebugMode = instructions.Debug;

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    ParetoIterativeAligner aligner = Config.InitialiseAligner(alignment, instructions);
                    AlignIteratively(aligner, instructions);

                    List<Alignment> solutions = aligner.CollectTradeoffSolutions();
                    SaveAlignments(solutions, outputPath);
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


        public void SaveAlignments(List<Alignment> solutions, string outPath)
        {
            Console.WriteLine($"Saving {solutions.Count} alignments:");

            int counter = 0;
            foreach(Alignment solution in solutions)
            {
                string filepath = $"{outPath}_{++counter}";
                FileHelper.WriteAlignmentTo(solution, filepath);
                Console.WriteLine($"saved: {filepath}");
            }
        }


        public void AlignIteratively(ParetoIterativeAligner aligner, AlignmentInstructions instructions)
        {
            Console.WriteLine(instructions.GetContextString());

            if (instructions.LimitedByIterations())
            {
                AlignUntilIterationLimit(aligner, instructions);
            }
            else if (instructions.LimitedBySeconds())
            {
                AlignUntilSecondsDeadline(aligner, instructions);
            }

            if (DebugMode)
            {
                DebuggingHelper.ShowDebuggingInfo(aligner);
            }
        }

        public void AlignUntilIterationLimit(ParetoIterativeAligner aligner, AlignmentInstructions instructions)
        {
            while (aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                if (DebugMode)
                {
                    DebuggingHelper.ShowDebuggingInfo(aligner);
                }
                PerformIterationOfAlignment(aligner, instructions);
            }

            if (DebugMode)
            {
                DebuggingHelper.ShowDebuggingInfo(aligner);
            }
        }


        public void AlignUntilSecondsDeadline(ParetoIterativeAligner aligner, AlignmentInstructions instructions)
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
                    DebuggingHelper.ShowDebuggingInfo(aligner);
                }

                if (time >= deadline)
                {
                    break;
                }
            }
        }

        public void PerformIterationOfAlignment(ParetoIterativeAligner aligner, AlignmentInstructions instructions)
        {
            aligner.Iterate();
        }
    }
}
