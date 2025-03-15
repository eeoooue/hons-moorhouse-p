using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.CrossoverOperators
{
    public class ColBasedCrossoverOperator : ICrossoverOperator
    {
        BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();

        // similar to One-Point Crossover operation described in SAGA (Notredame & Higgins, 1996)

        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            List<Alignment> result = new List<Alignment>();

            if (Randomizer.CoinFlip())
            {
                int i = Randomizer.Random.Next(2, a.Width);
                result = CrossoverAtPosition(a, b, i);
            }
            else
            {
                int i = Randomizer.Random.Next(2, b.Width);
                result = CrossoverAtPosition(b, a, i);
            }

            return result;
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            List<string> payloadsA = a.GetAlignedPayloads();
            List<string> payloadsB = b.GetAlignedPayloads();

            List<string> xParts = new List<string>();
            List<string> yParts = new List<string>();

            for (int i = 0; i < a.Height; i++)
            {
                List<string> pair = CrossoverSequencesAtPosition(payloadsA[i], payloadsB[i], position);
                xParts.Add(pair[0]);
                yParts.Add(pair[1]);
            }

            Alignment x = a.GetCopy();
            x.CharacterMatrix = CharMatrixHelper.ConstructAlignmentStateFromStrings(xParts);
            CharMatrixHelper.RemoveEmptyColumns(x);

            Alignment y = b.GetCopy();
            y.CharacterMatrix = CharMatrixHelper.ConstructAlignmentStateFromStrings(yParts);
            CharMatrixHelper.RemoveEmptyColumns(y);

            return new List<Alignment> { x, y };
        }

        public List<string> CrossoverSequences(string a, string b)
        {
            int n = a.Length;
            int i = Randomizer.Random.Next(1, n);
            return CrossoverSequencesAtPosition(a, b, i);
        }

        public List<string> CrossoverSequencesAtPosition(string a, string b, int i)
        {
            List<string> partsA = PayloadHelper.PartitionPayloadAtPosition(a, i);
            string aLeft = partsA[0];
            string aRight = partsA[1];

            string bRight = ExtractRightComplement(aLeft, b);
            string bLeft = ExtractLeftComplement(b, aRight);

            string xPayload = $"{aLeft}{bRight}";
            string yPayload = $"{bLeft}{aRight}";

            return new List<string> { xPayload, yPayload };
        }

        public string ExtractLeftComplement(string payload, string right)
        {
            int totalResidues = PayloadHelper.CountResiduesInPayload(payload);
            int residuesInRight = PayloadHelper.CountResiduesInPayload(right);

            if (residuesInRight == 0)
            {
                return payload;
            }

            if (residuesInRight == totalResidues)
            {
                return "";
            }

            int residuesInLeft = totalResidues - residuesInRight;
            int residueToStopBefore = residuesInLeft + 1;
            int i = PayloadHelper.GetPositionOfNthResidue(payload, residueToStopBefore);

            return payload.Substring(0, i);
        }

        public string ExtractRightComplement(string left, string payload)
        {
            int totalResidues = PayloadHelper.CountResiduesInPayload(payload);
            int residuesInLeft = PayloadHelper.CountResiduesInPayload(left);

            if (residuesInLeft == 0)
            {
                return payload;
            }

            if (residuesInLeft == totalResidues)
            {
                return "";
            }

            int residueToStartFrom = residuesInLeft + 1;
            int i = PayloadHelper.GetPositionOfNthResidue(payload, residueToStartFrom);

            return payload.Substring(i);
        }

    }
}
