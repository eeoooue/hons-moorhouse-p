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


        public void CollectSequenceResidues(Alignment alignment, int i, int j, out string residuesA, out string residuesB)
        {
            string sequenceA = CharMatrixHelper.GetCharRowAsString(alignment.CharacterMatrix, i);
            string sequenceB = CharMatrixHelper.GetCharRowAsString(alignment.CharacterMatrix, j);
            residuesA = PayloadHelper.ExtractResiduesFromString(sequenceA);
            residuesB = PayloadHelper.ExtractResiduesFromString(sequenceB);
        }

        public void GetNewSequenceLayout(Alignment alignment, in string residuesA, in string residuesB, out string layoutA, out string layoutB)
        {
            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(residuesA, residuesB);
            aligner.ExtractPairwiseAlignment(out layoutA, out layoutB);
        }
    }
}
