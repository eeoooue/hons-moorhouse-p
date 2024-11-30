using System.Text;

namespace LibBioInfo
{
    public class Alignment
    {
        public List<BioSequence> Sequences;
        public int Height { get { return State.GetLength(0); } }
        public int Width { get { return State.GetLength(1); } }

        public bool[,] State; // state[i,j] being true means a gap is placed at position (i,j)

        private static Bioinformatics Bioinformatics = new Bioinformatics();

        public Alignment(List<BioSequence> sequences, bool conserveState=false)
        {
            Sequences = sequences;

            if (conserveState)
            {
                State = ConstructStateBasedOnSequences(sequences);
            }
            else
            {
                int width = DecideWidth();
                State = new bool[sequences.Count, width];
                InitializeAlignmentState();
            }
        }

        public bool[,] ConstructStateBasedOnSequences(List<BioSequence> sequences)
        {
            int width = 0;
            foreach(BioSequence sequence in sequences)
            {
                width = Math.Max(width, sequence.Payload.Length);
            }

            bool[,] result = new bool[sequences.Count, width];

            for(int i=0; i<sequences.Count; i++)
            {
                string payload = sequences[i].Payload;

                for(int j=0; j<payload.Length; j++)
                {
                    result[i, j] = Bioinformatics.IsGapChar(payload[j]);
                }

                for(int j=payload.Length; j<width; j++)
                {
                    result[i, j] = true;
                }
            }

            return result;
        }


        public Alignment(Alignment other)
        {
            Sequences = other.GetAlignedSequences();
            State = new bool[other.Height, other.Width];

            for(int i=0; i<other.Height; i++)
            {
                for(int j=0; j<other.Width; j++)
                {
                    State[i, j] = other.State[i, j];
                }
            }
        }

        public List<BioSequence> GetAlignedSequences()
        {
            List<BioSequence> result = new List<BioSequence>();

            for (int i = 0; i < Height; i++)
            {
                string identifier = Sequences[i].Identifier;
                string alignedPayload = GetAlignedPayload(i);
                BioSequence aligned = new BioSequence(identifier, alignedPayload);
                result.Add(aligned);
            }

            return result;
        }

        public Alignment GetCopy()
        {
            return new Alignment(this);
        }

        public string GetAlignedPayload(int i)
        {
            BioSequence sequence = Sequences[i];

            StringBuilder sb = new StringBuilder();

            string residues = sequence.Residues;
            int residuesPlaced = 0;

            for (int j = 0; j < Width; j++)
            {
                bool positionIsEmpty = State[i, j];
                if (positionIsEmpty)
                {
                    sb.Append('-');
                }
                else
                {
                    sb.Append(residues[residuesPlaced]);
                    residuesPlaced++;
                }
            }

            return sb.ToString();
        }

        public char GetCharacterAt(int i, int j)
        {
            string alignedPayload = GetAlignedPayload(i);
            return alignedPayload[j];
        }

        public string GetColumn(int j)
        {
            // using an inefficient strategy temporarily

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Height; i++)
            {
                char c = GetCharacterAt(i, j);
                sb.Append(c);
            }

            return sb.ToString();
        }

        private int DecideWidth()
        {
            // placeholder logic

            int width = 0;
            foreach (BioSequence seq in Sequences)
            {
                width = Math.Max(width, seq.Residues.Length);
            }
            // int extra = (int)Math.Ceiling(width * 0.4);
            int extra = 8;

            return width + extra;
        }

        public bool ContainsNucleicsOnly()
        {
            foreach (BioSequence sequence in Sequences)
            {
                if (!sequence.IsNucleic())
                {
                    return false;
                }
            }

            return true;
        }

        public bool ContainsProteinsOnly()
        {
            foreach (BioSequence sequence in Sequences)
            {
                if (!sequence.IsProtein())
                {
                    return false;
                }
            }

            return true;
        }

        public void InitializeAlignmentState()
        {
            for (int i = 0; i < Height; i++)
            {
                InitializeAlignmentRow(i);
            }
        }

        private void InitializeAlignmentRow(int i)
        {
            int unusedCharacters = Sequences[i].Residues.Length;

            for (int j = 0; j < Width; j++)
            {
                if (unusedCharacters > 0)
                {
                    unusedCharacters--;
                    State[i, j] = false;
                }
                else
                {
                    State[i, j] = true; // indicates that a gap is placed at state[i,j]
                }
            }
        }

        public List<int> GetResiduePositionsInRow(Alignment alignment, int i)
        {
            List<int> result = new List<int>();

            for(int j=0; j<alignment.Width; j++)
            {
                if (State[i,j] == false)
                {
                    result.Add(j);
                }
            }

            return result;
        }

        public List<int> GetGapPositionsInRow(Alignment alignment, int i)
        {
            List<int> result = new List<int>();

            for (int j = 0; j < alignment.Width; j++)
            {
                if (State[i, j] == true)
                {
                    result.Add(j);
                }
            }

            return result;
        }

        public bool SequencesCanBeAligned()
        {
            return ContainsNucleicsOnly() || ContainsProteinsOnly();
        }
    }
}
