using System.Collections.Generic;
using System.Text;

namespace LibBioInfo
{
    public class Alignment
    {
        public List<BioSequence> Sequences { get { return AlignmentCore.Sequences; } }
        public int Height { get { return CharacterMatrix.GetLength(0); } }
        public int Width { get { return CharacterMatrix.GetLength(1); } }

        public char[,] CharacterMatrix;

        public AlignmentCore AlignmentCore;

        public Alignment(List<BioSequence> sequences, bool conserveState=false)
        {
            AlignmentCore = new AlignmentCore(sequences);
            if (conserveState)
            {
                CharacterMatrix = ConstructConservedAlignmentState(sequences);
            }
            else
            {
                CharacterMatrix = ConstructInitialAlignmentState(sequences);
            }
        }

        public Alignment(Alignment other)
        {
            AlignmentCore = other.AlignmentCore;
            CharacterMatrix = CopyCharacterMatrix(other.CharacterMatrix);
        }

        public char[,] CopyCharacterMatrix(char[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            char[,] result = new char[m, n];
            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }

            return result;
        }

        public char[,] ConstructConservedAlignmentState(List<BioSequence> sequences)
        {
            int m = sequences.Count;
            int n = AlignmentCore.GetLongestPayloadLength(sequences);

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
            int extra = 8;
            int width = 0;
            foreach (BioSequence seq in sequences)
            {
                width = Math.Max(width, seq.Residues.Length);
            }

            return width + extra;
        }

        public bool SequencesCanBeAligned()
        {
            return AlignmentCore.ContainsNucleicsOnly() || AlignmentCore.ContainsProteinsOnly();
        }
    }
}
