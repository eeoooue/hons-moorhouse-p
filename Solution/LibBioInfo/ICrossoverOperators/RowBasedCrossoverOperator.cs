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
            if (Randomizer.CoinFlip())
            {
                int i = Randomizer.Random.Next(2, a.Width);
                return CrossoverAtPosition(a, b, i);
            }
            else
            {
                int i = Randomizer.Random.Next(2, b.Width);
                return CrossoverAtPosition(b, a, i);
            }
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            List<BioSequence> sequencesA = a.GetAlignedSequences();
            List<BioSequence> sequencesB = b.GetAlignedSequences();

            List<BioSequence> xParts = new List<BioSequence>();
            List<BioSequence> yParts = new List<BioSequence>();

            for (int i = 0; i < a.Height; i++)
            {
                List<BioSequence> pair = CrossoverSequencesAtPosition(sequencesA[i], sequencesB[i], position);
                xParts.Add(pair[0]);
                yParts.Add(pair[1]);
            }

            Alignment x = new Alignment(xParts, true);
            Alignment y = new Alignment(yParts, true);

            return new List<Alignment> { x, y };
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
            string aLeft = partsA[0];
            string aRight = partsA[1];

            string bRight = ExtractRightComplement(aLeft, b);
            string bLeft = ExtractLeftComplement(b, aRight);

            string xPayload = $"{aLeft}{bRight}";
            string yPayload = $"{bLeft}{aRight}";

            BioSequence x = new BioSequence(a.Identifier, xPayload);
            BioSequence y = new BioSequence(a.Identifier, yPayload);

            return new List<BioSequence> { x, y };
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
