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
        BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public void GetNewSequenceLayout(Alignment alignment, int i, int j, out string layoutA, out string layoutB)
        {
            string seqAresidues = CollectSequenceResidues(alignment, i);
            string seqBresidues = CollectSequenceResidues(alignment, j);

            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(seqAresidues, seqBresidues);
            aligner.ExtractPairwiseAlignment(out layoutA, out layoutB);
        }

        public string CollectSequenceResidues(Alignment alignment, int i)
        {
            BioSequence sequence = alignment.Sequences[i];
            return sequence.Residues;
        }
    }
}
