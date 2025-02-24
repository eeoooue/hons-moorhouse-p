using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibScoring;
using LibScoring.FitnessFunctions;
using LibBioInfo.PairwiseAligners;

namespace LibSimilarity
{
    public class SimilarityJudge
    {
        private IFitnessFunction SimilarityScorer;

        public SimilarityJudge()
        {
            IScoringMatrix matrix = new PAM250Matrix();
            SimilarityScorer = new SumOfPairsFitnessFunction(matrix);
        }

        public double GetSimilarity(BioSequence a, BioSequence b)
        {
            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(a.Residues, b.Residues);
            char[,] alignedPair = aligner.ExtractPairwiseAlignment();
            double similarity = SimilarityScorer.GetFitness(alignedPair);

            return similarity;
        }
    }
}
