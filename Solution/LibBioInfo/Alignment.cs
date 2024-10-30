namespace LibBioInfo
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

        #region methods to be implemented

        public bool ContainsNucleicsOnly()
        {
            throw new NotImplementedException();
        }

        public bool ContainsProteinsOnly()
        {
            throw new NotImplementedException();
        }

        public List<BioSequence> GetAlignedSequences()
        {
            throw new NotImplementedException();
        }

        public char GetCharacterAt(int i, int j)
        {
            throw new NotImplementedException();
        }

        public string GetColumn(int j)
        {
            throw new NotImplementedException();
        }

        public void InitializeAlignmentState()
        {
            throw new NotImplementedException();
        }

        public bool SequencesCanBeAligned()
        {
            throw new NotImplementedException();
        }

        #endregion

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
