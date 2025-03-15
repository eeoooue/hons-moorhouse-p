using LibAlignment;
using LibBioInfo;
using LibFileIO;
using LibFileIO.AlignmentWriters;
using LibParetoAlignment;
using LibSimilarity;
using MAli.AlignmentConfigs;
using MAli.DebugPrinters;
using MAli.Helpers;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentEngines
{
    public class ParetoAlignmentEngine : IAlignmentEngine
    {
        private FileHelper FileHelper = new FileHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private ParetoDebugPrinter DebuggingHelper = new ParetoDebugPrinter();
        private BaseParetoAlignmentConfig Config;
        private AlignmentRequest Instructions = null!;

        private bool DebugMode = false;

        public ParetoAlignmentEngine(BaseParetoAlignmentConfig config)
        {
            Config = config;
        }

        public void PerformAlignment(AlignmentRequest instructions)
        {
            if (instructions is ParetoAlignmentRequest request)
            {
                PerformAlignment(request);
            }
            else
            {
                throw new Exception("Alignment failed due to incorrect execution pathway (pareto alignment).");
            }
        }


        private void PerformAlignment(ParetoAlignmentRequest instructions)
        {
            Instructions = instructions;
            DebuggingHelper = new ParetoDebugPrinter();
            DebugMode = Instructions.Debug;

            try
            {
                Console.WriteLine($"Reading sequences from source: '{Instructions.InputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(Instructions.InputPath);
                SimilarityGuide.SetSequences(sequences);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    ParetoIterativeAligner aligner = Config.InitialiseAligner(alignment, Instructions);
                    aligner.NumberOfTradeoffs = instructions.NumberOfTradeoffs;

                    AlignIteratively(aligner, Instructions);

                    List<Alignment> solutions = aligner.CollectTradeoffSolutions();
                    SaveAlignments(instructions, solutions, Instructions.OutputPath);
                    CheckSaveScorefiles(aligner, solutions, Instructions.OutputPath, Instructions);
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

        public void CheckSaveScorefiles(ParetoIterativeAligner aligner, List<Alignment> solutions, string outPath, AlignmentRequest instructions)
        {
            if (!instructions.IncludeScoreFile)
            {
                return;
            }

            MAliScoreWriter writer = new MAliScoreWriter(aligner.Objectives);

            int counter = 0;
            foreach (Alignment solution in solutions)
            {
                string filepath = $"{outPath}_{++counter}";
                writer.WriteAlignmentTo(solution, filepath);
            }

            Console.WriteLine("Saved .maliscore files.");
        }

        public void SaveAlignments(in ParetoAlignmentRequest request, List<Alignment> solutions, string outPath)
        {
            Console.WriteLine($"Saving {solutions.Count} alignments:");

            int counter = 0;
            foreach (Alignment solution in solutions)
            {
                string filepath = $"{outPath}_{++counter}";
                FileHelper.WriteAlignment(solution, request.OutputFormat, filepath);
                Console.WriteLine($"saved: {filepath}");
            }
        }

        public void AlignIteratively(ParetoIterativeAligner aligner, AlignmentRequest instructions)
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

        public void AlignUntilIterationLimit(ParetoIterativeAligner aligner, AlignmentRequest instructions)
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

        public void AlignUntilSecondsDeadline(ParetoIterativeAligner aligner, AlignmentRequest instructions)
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

        public void PerformIterationOfAlignment(ParetoIterativeAligner aligner, AlignmentRequest instructions)
        {
            aligner.Iterate();
        }
    }
}
