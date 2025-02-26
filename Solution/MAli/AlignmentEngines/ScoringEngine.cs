using LibAlignment;
using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibFileIO;
using LibFileIO.AlignmentWriters;
using LibScoring;
using LibScoring.FitnessFunctions;
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
                    Alignment freshAlignment = new Alignment(sequences, true);
                    SaveRichScoreFile(freshAlignment);
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

        public void SaveRichScoreFile(Alignment alignment)
        {
            List<IFitnessFunction> objectives = GetObjectives();
            MAliScoreWriter writer = new MAliScoreWriter(objectives);
            writer.WriteAlignmentTo(alignment, Instructions.OutputPath);
        }

        public List<IFitnessFunction> GetObjectives()
        {
            IScoringMatrix blosum62 = new BLOSUM62Matrix();
            IFitnessFunction sopBlosum = new SumOfPairsFitnessFunction(blosum62);
            IFitnessFunction sopBlosumGapPen = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(blosum62);

            IScoringMatrix pam250 = new PAM250Matrix();
            IFitnessFunction sopPam = new SumOfPairsFitnessFunction(pam250);
            IFitnessFunction sopPamGapPen = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(pam250);

            IFitnessFunction affineGapPenalty = new AffineGapPenaltyFitnessFunction();
            IFitnessFunction nonGapPenalty = new NonGapsFitnessFunction();
            IFitnessFunction totallyConservedColumns = new TotallyConservedColumnsFitnessFunction();

            List<IFitnessFunction> result = new List<IFitnessFunction>()
            {
                sopBlosum,
                sopBlosumGapPen,
                sopPam,
                sopPamGapPen,
                affineGapPenalty,
                nonGapPenalty,
                totallyConservedColumns,
            };

            return result;
        }

    }
}
