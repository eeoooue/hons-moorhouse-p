using LibAlignment;
using LibBioInfo;
using LibFileIO;
using LibFileIO.AlignmentWriters;
using LibScoring;
using LibSimilarity;
using MAli.Helpers;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentEngines
{
    public class ScoringEngine : IAlignmentEngine
    {
        private FileHelper FileHelper = new FileHelper();
        private AlignmentConfig Config;
        private ScoringRequest Instructions = null!;
        private ResponseBank ResponseBank = new ResponseBank();

        public ScoringEngine(AlignmentConfig config)
        {
            Config = config;
        }

        public void PerformAlignment(AlignmentRequest instructions)
        {
            if (instructions is ScoringRequest request)
            {
                ScoreAlignment(request);
            }
        }

        public void ScoreAlignment(ScoringRequest instructions)
        {
            Instructions = instructions;

            try
            {
                Console.WriteLine($"Reading sequences from source: '{Instructions.InputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(Instructions.InputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    IterativeAligner aligner = Config.InitialiseAligner(alignment, Instructions);
                    Alignment freshAlignment = new Alignment(sequences, true);
                    SaveScoreFile(aligner.Objective, freshAlignment);
                }
                else
                {
                    Console.WriteLine("Error: Invalid alignment.");
                }
            }
            catch (Exception e)
            {
                ResponseBank.ExplainException(e);
            }
        }

        public void SaveScoreFile(IFitnessFunction objective, Alignment alignment)
        {
            MAliScoreWriter writer = new MAliScoreWriter(objective);
            writer.WriteAlignmentTo(alignment, Instructions.OutputPath);
        }
    }
}
