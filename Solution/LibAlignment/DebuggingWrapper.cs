using LibAlignment.Helpers;
using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment
{
    public class DebuggingWrapper : IIterativeAligner
    {
        private IterativeAligner Aligner;
        private AlignmentDebugHelper DebugHelper = new AlignmentDebugHelper();

        public int IterationsCompleted { get { return Aligner.IterationsCompleted; } }

        public int IterationsLimit { get { return Aligner.IterationsLimit; } set { Aligner.IterationsLimit = value; } }

        public double AlignmentScore { get { return Aligner.AlignmentScore; } }

        public IObjectiveFunction Objective { get { return Aligner.Objective; } }

        public Alignment? CurrentAlignment { get { return Aligner.CurrentAlignment; } }


        public int DebugCursorStart = -1;

        public DebuggingWrapper(IterativeAligner aligner)
        {
            Aligner = aligner;
        }

        public void Iterate()
        {
            Aligner.Iterate();
            ShowDebuggingInfo();
        }

        public void Initialize(List<BioSequence> sequences)
        {
            Aligner.Initialize(sequences);
        }

        public void InitializeForRefinement(Alignment alignment)
        {
            Aligner.InitializeForRefinement(alignment);
        }

        public Alignment AlignSequences(List<BioSequence> sequences)
        {
            Initialize(sequences);
            while (IterationsCompleted < IterationsLimit)
            {
                Iterate();
            }
            return CurrentAlignment!;
        }

        public void ShowDebuggingInfo()
        {
            if (DebugCursorStart == -1)
            {
                int cursorPos = Console.GetCursorPosition().Top;
                DebugCursorStart = cursorPos + 1;
            }

            List<string> lines = new List<string>() { "Debugging:", "" };
            CollectAlignmentStrategy(lines);
            CollectAlignmentStateInfo(lines);
            List<string> output = DebugHelper.PadInfoLines(lines);
            string info = ConcatenateLines(output);
            Console.SetCursorPosition(0, DebugCursorStart);

            Console.WriteLine(info);
            TryDisplayAlignment(CurrentAlignment);
            Console.WriteLine();
        }

        public string ConcatenateLines(List<string> lines)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string line in lines)
            {
                sb.Append(line);
                sb.Append("\n");
            }

            return sb.ToString();
        }


        public void CollectAlignmentStrategy(List<string> lines)
        {
            double percentIterationsComplete = Math.Round(100.0 * (double)IterationsCompleted / (double)IterationsLimit, 3);
            string percentValue = percentIterationsComplete.ToString("0.0");

            lines.Add(Aligner.GetName());
            lines.Add($" - completed {IterationsCompleted} of {IterationsLimit} iterations ({percentValue}%)");
            lines.Add("");
        }

        public void CollectAlignmentStateInfo(List<string> lines)
        {
            if (CurrentAlignment is Alignment alignment)
            {
                int m = alignment.Height;
                int n = alignment.Width;

                lines.Add($"Current Alignment: ");
                lines.Add($" - dimensions: ({m} x {n})");
                lines.Add($" - objective function: {Objective.GetName()}");
                lines.Add($" - score: {AlignmentScore}");
            }
        }

        public void TryDisplayAlignment(Alignment? alignment)
        {
            if (alignment is Alignment current)
            {
                DebugHelper.PaintAlignment(alignment);
            }
        }

        
    }
}
