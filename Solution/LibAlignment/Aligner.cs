using LibBioInfo;
using LibScoring;
using System;
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
                List<string> lines = new List<string>() { "", "Debugging:", "" };
                CollectAlignmentStrategy(lines);
                CollectAlignmentStateInfo(lines);

                string info = ConcatenateLines(lines);
                Console.Clear();
                Console.WriteLine(info);
                // Thread.Sleep(500);
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


        public void CollectAlignmentStrategy(List<string> lines)
        {
            double percentIterationsComplete = Math.Round(100.0 * (double)IterationsCompleted/(double)IterationsLimit, 3);

            lines.Add(GetName());
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

        public abstract string GetName();

        public List<ScoredAlignment> ScorePopulation(List<Alignment> population)
        {
            List<ScoredAlignment> candidates = new List<ScoredAlignment>();
            foreach (Alignment alignment in population)
            {
                double score = ScoreAlignment(alignment);
                ScoredAlignment candidate = new ScoredAlignment(alignment, score);
                candidates.Add(candidate);
                CheckNewBest(candidate);
            }

            SetFitnesses(candidates);

            return candidates;
        }

        public void SetFitnesses(List<ScoredAlignment> candidates)
        {
            double bestScore = GetBestScore(candidates);
            double worstScore = GetWorstScore(candidates);
            double range = bestScore - worstScore;

            foreach(ScoredAlignment candidate in candidates)
            {
                SetFitness(candidate, worstScore, range);
            }
        }

        public void SetFitness(ScoredAlignment candidate, in double worstScore, in double range)
        {
            double unscaledFitness = candidate.Score - worstScore;
            double scaled = unscaledFitness / range;
            candidate.Fitness = scaled;
        }

        public double GetBestScore(List<ScoredAlignment> candidates)
        {
            double bestScore = double.MinValue;
            foreach (ScoredAlignment candidate in candidates)
            {
                double score = candidate.Score;
                bestScore = Math.Max(score, bestScore);
            }

            return bestScore;
        }

        public double GetWorstScore(List<ScoredAlignment> candidates)
        {
            double worstScore = double.MaxValue;
            foreach (ScoredAlignment candidate in candidates)
            {
                double score = candidate.Score;
                worstScore = Math.Min(score, worstScore);
            }

            return worstScore;
        }
    }
}
