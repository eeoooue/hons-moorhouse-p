using LibAlignment;
using LibBioInfo;
using LibSimilarity;
using MAli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.DebugPrinters
{
    internal class DefaultDebugPrinter
    {
        private AlignmentPreviewHelper DebugHelper = new AlignmentPreviewHelper();

        public int DebugCursorStart = -1;
        public string ProgressContext = "";

        public void ShowDebuggingInfo(IterativeAligner aligner)
        {
            if (DebugCursorStart == -1)
            {
                int cursorPos = Console.GetCursorPosition().Top;
                DebugCursorStart = cursorPos + 1;
            }

            List<string> lines = new List<string>() { "Debugging:", "" };
            CollectAlignmentStrategy(aligner, lines);
            CollectSimilarityGuideInfo(lines);
            CollectAlignmentStateInfo(aligner, lines);
            List<string> output = DebugHelper.PadInfoLines(lines);
            string info = ConcatenateLines(output);
            Console.SetCursorPosition(0, DebugCursorStart);

            Console.WriteLine(info);
            TryDisplayAlignment(aligner.CurrentAlignment);
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


        public void CollectAlignmentStrategy(IterativeAligner aligner, List<string> lines)
        {
            double percentIterationsComplete = Math.Round(100.0 * aligner.IterationsCompleted / aligner.IterationsLimit, 3);
            string percentValue = percentIterationsComplete.ToString("0.0");

            lines.Add(aligner.GetName());
            lines.Add($" - {ProgressContext}");
            lines.Add($" - {BioSequence.SequencesInstantiated} sequence(s) instantiated @ runtime");

            // lines.Add($" - completed {IterationsCompleted} of {IterationsLimit} iterations ({percentValue}%)");
            lines.Add("");
        }

        public void CollectSimilarityGuideInfo(List<string> lines)
        {
            lines.Add(SimilarityGuide.GetDebugString());
            lines.Add("");
        }



        public void CollectAlignmentStateInfo(IterativeAligner aligner, List<string> lines)
        {
            if (aligner.CurrentAlignment is Alignment alignment)
            {
                int m = alignment.Height;
                int n = alignment.Width;

                lines.Add($"Current Alignment: ");
                lines.Add($" - dimensions: ({m} x {n})");
                lines.Add($" - objective function: {aligner.Objective.GetName()}");
                lines.Add($" - score: {aligner.AlignmentScore}");
            }
        }

        public void TryDisplayAlignment(Alignment? alignment)
        {
            if (alignment is Alignment current && alignment.Height < 10)
            {
                DebugHelper.PaintAlignment(alignment);
            }
            else
            {
                Console.WriteLine("[ Alignment contains too many sequences to display. ]");
            }
        }
    }
}
