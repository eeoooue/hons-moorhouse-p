﻿using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Helpers
{
    public class BiosequencePayloadHelper
    {
        

        public string GetPayloadWithGapRemovedAt(string payload, int gapPosition)
        {
            string front = payload.Substring(0, gapPosition);
            string back = payload.Substring(gapPosition + 1);
            return $"{front}{back}";
        }

        public string GetPayloadWithGapInsertedAt(string payload, int gapPosition)
        {
            string front = payload.Substring(0, gapPosition);
            string back = payload.Substring(gapPosition);
            return $"{front}-{back}";
        }

        public List<string> PartitionPayloadAtPosition(string payload, int i)
        {
            string left = payload.Substring(0, i);
            string right = payload.Substring(i);

            return new List<string> { left, right };
        }

        public int GetPositionOfNthResidue(string payload, int n)
        {
            int total = 0;
            for (int i = 0; i < payload.Length; i++)
            {
                if (payload[i] != Bioinformatics.GapCharacter)
                {
                    total++;
                    if (total == n)
                    {
                        return i;
                    }
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        public int CountResiduesInPayload(string payload)
        {
            int total = 0;
            foreach (char x in payload)
            {
                if (x != Bioinformatics.GapCharacter)
                {
                    total++;
                }
            }

            return total;
        }
    }
}
