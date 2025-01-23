﻿using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.AlignmentModifiers
{
    internal class NewGapInserter : ILegacyAlignmentModifier, IAlignmentModifier
    {
        public BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();

        public int GapWidthLimit;

        public NewGapInserter(int gapSizeLimit = 4)
        {
            GapWidthLimit = gapSizeLimit;
        }

        public void ModifyAlignment(Alignment alignment)
        {
            char[,] result = GetModifiedAlignmentState(alignment.GetCopy());
            alignment.CharacterMatrix = result;
        }

        public char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int gapWidth = PickGapWidth();
            InsertGapOfWidth(alignment, gapWidth);
            alignment.CheckResolveEmptyColumns();
            return alignment.CharacterMatrix;
        }

        public void InsertGapOfWidth(Alignment alignment, int gapWidth)
        {
            bool[] mapping = GetRowMapping(alignment.Height);

            int m = alignment.Height;
            int n = alignment.Width + gapWidth;
            int position1 = SuggestGapPosition(gapWidth, n);
            int position2 = SuggestGapPosition(gapWidth, n);

            List<BioSequence> originals = alignment.GetAlignedSequences();
            List<BioSequence> modified = new List<BioSequence>();

            for (int i = 0; i < m; i++)
            {
                BioSequence sequence;
                if (mapping[i])
                {
                    sequence = GetSequenceWithGapInserted(originals[i], gapWidth, position1);
                }
                else
                {
                    sequence = GetSequenceWithGapInserted(originals[i], gapWidth, position2);
                }
                modified.Add(sequence);
            }

            Alignment temp = new Alignment(modified, true);
            alignment.SetState(temp.State);
        }


        public BioSequence GetSequenceWithGapInserted(BioSequence sequence, int width, int i)
        {
            return PayloadHelper.GetSequenceWithGapInserted(sequence, width, i);
        }

        public int SuggestGapPosition(int gapWidth, int stateWidth)
        {
            int n = stateWidth - gapWidth;
            return Randomizer.Random.Next(0, n);
        }

        public bool[] GetRowMapping(int n)
        {
            bool[] result = new bool[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = Randomizer.CoinFlip();
            }

            return result;
        }

        public int PickGapWidth()
        {
            int gapWidth = Randomizer.Random.Next(1, GapWidthLimit + 1);
            return gapWidth;
        }
    }
}
