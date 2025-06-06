﻿using LibBioInfo.PairwiseAligners;
using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibScoring;
using LibBioInfo.ScoringMatrices;
using LibScoring.FitnessFunctions;
using LibSimilarity;

namespace LibModification.Helpers
{
    public class PairwiseAlignmentHelper
    {
        public void GetNewSequenceLayout(Alignment alignment, int i, int j, out string layoutA, out string layoutB)
        {
            char[,] matrix = PerformPairwiseAlignment(alignment, i, j);

            int n = matrix.GetLength(1);

            StringBuilder a = new StringBuilder();
            StringBuilder b = new StringBuilder();

            for (int p=0; p<n; p++)
            {
                a.Append(matrix[0, p]);
                b.Append(matrix[1, p]);
            }

            layoutA = a.ToString();
            layoutB = b.ToString();
        }

        public char[,] PerformPairwiseAlignment(Alignment alignment, int i, int j)
        {
            string seqAresidues = alignment.Sequences[i].Residues;
            string seqBresidues = alignment.Sequences[j].Residues;

            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(seqAresidues, seqBresidues);
            char[,] result = aligner.ExtractPairwiseAlignment();

            return result;
        }
    }
}
