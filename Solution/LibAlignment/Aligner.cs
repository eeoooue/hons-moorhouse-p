using LibBioInfo;
using LibScoring;
using System.Text;

namespace LibAlignment
{
    public abstract class Aligner
    {
        private IObjectiveFunction Objective;
        public Alignment? CurrentAlignment = null;

        public int IterationsCompleted { get; protected set; } = 0;

        public bool Debug = false;

        public int IterationsLimit { get; set; } = 0;

        public double AlignmentScore { get; protected set; } = 0;

        public Aligner(IObjectiveFunction objective, int iterations)
        {
            Objective = objective;
            IterationsLimit = iterations;
        }

        public abstract Alignment AlignSequences(List<BioSequence> sequences);

        public abstract void Initialize(List<BioSequence> sequences);

        public abstract void Iterate();

        public void CheckShowDebuggingInfo()
        {
            if (Debug)
            {
                List<string> lines = new List<string>();
                CollectAlignmentProgress(lines);
                CollectAlignmentStateInfo(lines);

                string info = ConcatenateLines(lines);
                Console.Clear();
                Console.WriteLine(info);
                Thread.Sleep(500);
            }
        }

        public string ConcatenateLines(List<string> lines)
        {
            StringBuilder sb = new StringBuilder();

            foreach(string line in lines)
            {
                sb.Append(line);
                sb.Append("\n");
            }

            return sb.ToString();
        }


        public void CollectAlignmentProgress(List<string> lines)
        {
            double percentIterationsComplete = Math.Round(100.0 * (double)IterationsCompleted/(double)IterationsLimit, 3);
            lines.Add($"Aligning Sequences");
            lines.Add($" - completed {IterationsCompleted} of {IterationsLimit} iterations ({percentIterationsComplete}%)");
            lines.Add("");
        }

        public void CollectAlignmentStateInfo(List<string> lines)
        {
            const int maxWidth = 200;
            const int maxHeight = 20;

            if (CurrentAlignment is Alignment alignment)
            {
                int m = alignment.Height;
                int n = alignment.Width;

                lines.Add($"Current Alignment: ");
                lines.Add($" - dimensions: ({m} x {n})");
                lines.Add($" - objective function: {Objective.GetName()}");
                lines.Add($" - score: {AlignmentScore}");

                if (m <= maxHeight && n <= maxWidth)
                {
                    List<BioSequence> sequences = alignment.GetAlignedSequences();
                    foreach (BioSequence sequence in sequences)
                    {
                        lines.Add(sequence.Payload);
                    }
                }
                else
                {
                    lines.Add("[ alignment too big for preview ]");
                }
            }
            else
            {
                lines.Add("[ alignment missing ]");
            }
        }

        public void CheckNewBest(ScoredAlignment candidate)
        {
            if (candidate.Score > AlignmentScore)
            {
                CurrentAlignment = candidate.Alignment.GetCopy();
                AlignmentScore = candidate.Score;
            }
        }

        public double ScoreAlignment(Alignment alignment)
        {
            return Objective.ScoreAlignment(alignment);
        }
    }
}
