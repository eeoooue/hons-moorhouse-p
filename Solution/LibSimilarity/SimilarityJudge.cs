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
        public double GetSimilarity(BioSequence a, BioSequence b)
        {
            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(a.Residues, b.Residues);
            char[,] alignedPair = aligner.ExtractPairwiseAlignment();
            double similarity = aligner.GetSimilarityScore();

            return similarity;
        }
    }
}
