using LibBioInfo;
using LibScoring;

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
                Console.Clear();
                PresentAlignmentProgress();
                TryPresentAlignment();
            }
        }

        public void PresentAlignmentProgress()
        {
            double percentIterationsComplete = Math.Round((double)IterationsCompleted/(double)IterationsLimit, 3);

            Console.WriteLine($"Aligning Sequences");
            Console.WriteLine($" - completed {IterationsCompleted} of {IterationsLimit} iterations ({percentIterationsComplete}%)");
        }

        public void TryPresentAlignment()
        {
            const int maxWidth = 100;
            const int maxHeight = 20;
            bool previewShown = false;


            if (CurrentAlignment is Alignment alignment)
            {
                int m = alignment.Height;
                int n = alignment.Width;

                Console.WriteLine($"Current Alignment: ");
                Console.WriteLine($" - dimensions: ({m} x {n})");
                Console.WriteLine($" - score: {AlignmentScore}");

                if (m <= maxHeight && n <= maxWidth)
                {
                    PresentCurrentAlignmentState(alignment);
                    previewShown = true;
                }
            }

            if (!previewShown)
            {
                Console.WriteLine("[ preview unavailable ]");
            }
        }

        public void PresentCurrentAlignmentState(Alignment alignment)
        {


        }

        public void CheckNewBest(ScoredAlignment candidate)
        {
            if (candidate.Score > AlignmentScore)
            {
                CurrentAlignment = candidate.Alignment.GetCopy();
                AlignmentScore = candidate.Score;

                if (Debug)
                {
                    Console.WriteLine($"iterations = {IterationsCompleted} | new best score = {AlignmentScore}");
                }
            }
        }

        public double ScoreAlignment(Alignment alignment)
        {
            return Objective.ScoreAlignment(alignment);
        }
    }
}
