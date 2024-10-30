﻿namespace LibBioInfo
{
    public class Alignment
    {
        public List<BioSequence> Sequences;
        public int Height { get { return Sequences.Count; } }
        public int Width { get; private set; } = 0;

        public bool[,] State;

        public Alignment(List<BioSequence> sequences)
        {
            Sequences = sequences;
            Width = DecideWidth();
            State = new bool[Height,Width];
        }

        private int DecideWidth()
        {
            // placeholder logic - alignment width is double the length of the longest sequence

            int width = 0;
            foreach (BioSequence seq in Sequences)
            {
                int doubledWidth = seq.Payload.Length * 2;
                width = Math.Max(width, doubledWidth);
            }

            return width;
        }
    }
}
