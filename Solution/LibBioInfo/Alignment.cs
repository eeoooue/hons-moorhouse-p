using System.Collections.Generic;
using System.Text;

namespace LibBioInfo
{
    public class Alignment
    {
        public List<BioSequence> Sequences;
        public int Height { get { return CharacterMatrix.GetLength(0); } }
        public int Width { get { return CharacterMatrix.GetLength(1); } }

        private static Bioinformatics Bioinformatics = new Bioinformatics();

        public char[,] CharacterMatrix;

        public int[] ProgressiveOrder;

        public Alignment(List<BioSequence> sequences, bool conserveState=false)
        {
            Sequences = sequences;
            if (conserveState)
            {
                CharacterMatrix = ConstructConservedAlignmentState(sequences);
            }
            else
            {
                CharacterMatrix = ConstructInitialAlignmentState(sequences);
            }

            ProgressiveOrder = new int[Height];
            for(int i=0; i<Height; i++)
            {
                ProgressiveOrder[i] = i;
            }
        }

        public Alignment(Alignment other)
        {
            Sequences = other.GetAlignedSequences();
            CharacterMatrix = ConstructConservedAlignmentState(Sequences);

            ProgressiveOrder = new int[Height];
            for (int i = 0; i < Height; i++)
            {
                ProgressiveOrder[i] = other.ProgressiveOrder[i];
            }
        }


        public char[,] ConstructConservedAlignmentState(List<BioSequence> sequences)
        {
            int m = sequences.Count;
            int n = GetLongestPayloadLength(sequences);

            List<string> payloads = new List<string>();
            foreach(BioSequence sequence in sequences)
            {
                payloads.Add(sequence.Payload);
            }

            return ConstructAlignmentStateFromStrings(m, n, payloads);
        }

        public char[,] ConstructInitialAlignmentState(List<BioSequence> sequences)
        {
            int m = sequences.Count;
            int n = DecideWidth(sequences);

            List<string> residueStrings = new List<string>();
            foreach (BioSequence sequence in sequences)
            {
                residueStrings.Add(sequence.Residues);
            }

            return ConstructAlignmentStateFromStrings(m, n, residueStrings);
        }

        public char[,] ConstructAlignmentStateFromStrings(int m, int n, List<string> payloads)
        {
            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                string payload = payloads[i];
                for (int j = 0; j < n; j++)
                {
                    if (j < payload.Length)
                    {
                        result[i, j] = payload[j];
                    }
                    else
                    {
                        result[i, j] = '-';
                    }
                }
            }

            return result;
        }

        public int GetLongestPayloadLength(List<BioSequence> sequences)
        {
            int result = 0;
            foreach(BioSequence sequence in sequences)
            {
                result = Math.Max(sequence.Payload.Length, result);
            }

            return result;
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


        public char GetCharacterAt(int i, int j)
        {
            return CharacterMatrix[i, j];
        }

        public string GetColumn(int j)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                char c = CharacterMatrix[i, j];
                sb.Append(c);
            }

            return sb.ToString();
        }

        public string GetAlignedPayload(int i)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < Width; j++)
            {
                char x = CharacterMatrix[i, j];
                sb.Append(x);
            }

            return sb.ToString();
        }

        private int DecideWidth(List<BioSequence> sequences)
        {
            // placeholder logic

            int width = 0;
            foreach (BioSequence seq in sequences)
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

        public List<int> GetResiduePositionsInRow(Alignment alignment, int i)
        {
            List<int> result = new List<int>();

            for(int j=0; j<alignment.Width; j++)
            {
                if (Bioinformatics.IsGapChar(CharacterMatrix[i,j]) == false)
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
                if (Bioinformatics.IsGapChar(CharacterMatrix[i, j]) == true)
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
