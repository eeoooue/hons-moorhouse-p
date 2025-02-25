using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public class GapInsertion
    {
        private BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();

        public void InsertGaps(Alignment alignment, bool[] mask, int width, int j1, int j2)
        {
            // j denotes where gaps start. so j can be 0 to n (alignment width)

            int m = alignment.Height;
            int n = alignment.Width + width;

            char[,] matrix = new char[m, n];

            for(int i=0; i<m; i++)
            {
                int gapPosition = mask[i] ? j1 : j2;
                // Console.WriteLine($"Inserting gap of width = {width} @ {gapPosition}");
                PasteSequenceWithGapAt(alignment, matrix, i, gapPosition, width);
            }

            alignment.CharacterMatrix = matrix;
        }

        public void PasteSequenceWithGapAt(Alignment alignment, char[,] destination, int i, int gapPosition, int width)
        {

            string payload = alignment.GetAlignedPayload(i);
            payload = PayloadHelper.GetPayloadWithGapInserted(payload, width, gapPosition);

            for(int j=0; j<payload.Length; j++)
            {
                destination[i, j] = payload[j];
            }
        }
    }
}
