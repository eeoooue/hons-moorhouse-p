using LibBioInfo.Helpers;
using System.Text;

namespace LibBioInfo
{
    public class Alignment
    {
        public static AlignmentStateHelper StateHelper = new AlignmentStateHelper();

        public List<BioSequence> Sequences;
        public int Height { get { return State.GetLength(0); } }
        public int Width { get { return State.GetLength(1); } }

        public bool[,] State { get; private set; } // state[i,j] being true means a gap is placed at position (i,j)

        private static Bioinformatics Bioinformatics = new Bioinformatics();

        public char[,] CharacterMatrix;

        public bool CharacterMatrixIsUpToDate = false;

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

            CharacterMatrix = ConstructCharacterMatrix();
            CharacterMatrixIsUpToDate = true;
        }

        public Alignment(Alignment other) : this(other.GetAlignedSequences(), true) { }
        
        public void SetState(bool[,] state)
        {
            State = state;
            CharacterMatrixIsUpToDate = false;
        }

        public void CheckResolveEmptyColumns()
        {
            bool verdict = StateHelper.ContainsEmptyColumns(State);

            if (verdict)
            {
                bool[,] simplifiedState = StateHelper.RemoveEmptyColumns(State);
                SetState(simplifiedState);
            }
        }

        


        public void SetState(int i, int j, bool value)
        {
            State[i, j] = value;
            CharacterMatrixIsUpToDate = false;
        }

        public void UpdateCharacterMatrixIfNeeded()
        {
            if (!CharacterMatrixIsUpToDate)
            {
                UpdateStateRepresentationIfNeeded();
                CharacterMatrix = ConstructCharacterMatrix();
                CharacterMatrixIsUpToDate = true;
            }
        }

        public void UpdateStateRepresentationIfNeeded()
        {
            List<int> emptyColumns = CollectEmptyColumnIndexes();

            if (emptyColumns.Count > 0)
            {
                bool[,] newState = ConstructStateIgnoringColumns(State, emptyColumns);
                SetState(newState);
            }
        }

        public bool[,] ConstructStateIgnoringColumns(bool[,] state, List<int> blacklist)
        {
            List<int> whitelist = CollectColumnWhitelist(state, blacklist);
            return ConstructStateOfOnlyColumns(state, whitelist);
        }

        public bool[,] ConstructStateOfOnlyColumns(bool[,] state, List<int> columns)
        {
            int m = state.GetLength(0);
            int n = columns.Count;

            bool[,] result = new bool[m, n];

            for(int i = 0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    int j2 = columns[j];
                    result[i, j] = state[i, j2];
                }
            }

            return result;
        }

        public List<int> CollectColumnWhitelist(bool[,] state, List<int> blacklist)
        {
            int n = state.GetLength(1);

            HashSet<int> setToIgnore = new HashSet<int>(blacklist);

            List<int> result = new List<int>();

            for(int j=0; j<n; j++)
            {
                if (setToIgnore.Contains(j))
                {
                    continue;
                }
                result.Add(j);
            }

            return result;
        }

        public List<int> CollectEmptyColumnIndexes()
        {
            List<int> result = new List<int>();

            for(int j = 0; j < Width; j++)
            {
                if (ColumnIsEmpty(j))
                {
                    result.Add(j);
                }
            }

            return result;
        }

        public bool ColumnIsEmpty(int j)
        {
            for(int i=0; i<Height; i++)
            {
                if (!State[i, j])
                {
                    return false;
                }
            }

            return true;
        }

        public char[,] ConstructCharacterMatrix()
        {
            char[,] result = new char[Height, Width];

            for(int i=0; i<Height; i++)
            {
                string payload = GetAlignedPayload(i);
                for(int j=0; j<Width; j++)
                {
                    result[i, j] = payload[j];
                }
            }

            return result;
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
            UpdateCharacterMatrixIfNeeded();
            return CharacterMatrix[i, j];
        }

        public string GetColumn(int j)
        {
            UpdateCharacterMatrixIfNeeded();

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
