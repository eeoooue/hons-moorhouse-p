using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ICrossoverOperators
{
    public class RowBasedCrossoverOperator : ICrossoverOperator
    {
        BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();


        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            List<BioSequence> sequencesA = a.GetAlignedSequences();
            List<BioSequence> sequencesB = b.GetAlignedSequences();


            throw new NotImplementedException();
        }

        public List<BioSequence> CrossoverSequences(BioSequence a, BioSequence b)
        {
            int n = a.Payload.Length;
            int i = Randomizer.Random.Next(1, n);
            return CrossoverSequencesAtPosition(a, b, i);
        }

        public List<BioSequence> CrossoverSequencesAtPosition(BioSequence a, BioSequence b, int i)
        {
            List<string> partsA = PayloadHelper.PartitionPayloadAtPosition(a, i);

            throw new NotImplementedException();
        }

        public string ExtractLeftComplement(BioSequence sequence, string right)
        {
            string payload = sequence.Payload;
            int totalResidues = PayloadHelper.CountResiduesInPayload(payload);
            int residuesInRight = PayloadHelper.CountResiduesInPayload(right);

            if (residuesInRight == 0)
            {
                return sequence.Payload;
            }

            if (residuesInRight == totalResidues)
            {
                return "";
            }

            int residuesInLeft = totalResidues - residuesInRight;
            int residueToStopBefore = residuesInLeft + 1;
            int i = PayloadHelper.GetPositionOfNthResidue(sequence, residueToStopBefore);

            return payload.Substring(0, i);
        }

        public string ExtractRightComplement(string left, BioSequence sequence)
        {
            string payload = sequence.Payload;
            int totalResidues = PayloadHelper.CountResiduesInPayload(payload);
            int residuesInLeft = PayloadHelper.CountResiduesInPayload(left);

            if (residuesInLeft == 0)
            {
                return sequence.Payload;
            }

            if (residuesInLeft == totalResidues)
            {
                return "";
            }

            int residuesInRight = totalResidues - residuesInLeft;
            int residueToStartFrom = residuesInLeft + 1;
            int i = PayloadHelper.GetPositionOfNthResidue(sequence, residueToStartFrom);

            return payload.Substring(i);
        }

    }
}
