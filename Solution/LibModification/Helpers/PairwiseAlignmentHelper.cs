using LibBioInfo.PairwiseAligners;
using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string seqAresidues = CollectSequenceResidues(alignment, i);
            string seqBresidues = CollectSequenceResidues(alignment, j);

            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(seqAresidues, seqBresidues);
            char[,] result = aligner.ExtractPairwiseAlignment();
            UpdateSimilarityGuide(alignment, i, j, result);

            return result;
        }

        public void UpdateSimilarityGuide(Alignment alignment, int i, int j, char[,] pairwiseMatrix)
        {
            // TODO: implement
            // throw new NotImplementedException("Update Similarity Guide method is not implemented");
        }

        public string CollectSequenceResidues(Alignment alignment, int i)
        {
            BioSequence sequence = alignment.Sequences[i];
            return sequence.Residues;
        }
    }
}
