using LibAlignment;
using LibBioInfo;
using LibParetoAlignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class ParetoDebuggingHelper
    {
        private AlignmentDebugHelper DebugHelper = new AlignmentDebugHelper();

        public int DebugCursorStart = -1;
        public string ProgressContext = "";


        public void ShowDebuggingInfo(ParetoIterativeAligner aligner)
        {
            if (DebugCursorStart == -1)
            {
                int cursorPos = Console.GetCursorPosition().Top;
                DebugCursorStart = cursorPos + 1;
            }

            List<string> lines = new List<string>() { "Debugging:", "" };
            CollectAlignmentStrategy(aligner, lines);

            


            CollectAlignmentStateInfo(aligner, lines);
            List<string> output = DebugHelper.PadInfoLines(lines);
            string info = ConcatenateLines(output);
            Console.SetCursorPosition(0, DebugCursorStart);

            Console.WriteLine(info);
            TryDisplayAlignment(aligner.GetCurrentAlignment());
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


        public void CollectAlignmentStrategy(ParetoIterativeAligner aligner, List<string> lines)
        {
            double percentIterationsComplete = Math.Round(100.0 * aligner.IterationsCompleted / aligner.IterationsLimit, 3);
            string percentValue = percentIterationsComplete.ToString("0.0");

            lines.Add(aligner.GetName());
            lines.Add($" - {ProgressContext}");

            // lines.Add($" - completed {IterationsCompleted} of {IterationsLimit} iterations ({percentValue}%)");
            lines.Add("");
        }

        public void CollectAlignmentStateInfo(ParetoIterativeAligner aligner, List<string> lines)
        {
            if (aligner.GetCurrentAlignment() is Alignment alignment)
            {
                int m = alignment.Height;
                int n = alignment.Width;

                lines.Add($"Current Alignment: ");
                lines.Add($" - dimensions: ({m} x {n})");

                foreach (string item in aligner.GetDebuggingInfo())
                {
                    lines.Add(item);
                }
                // lines.Add($" - objective function: {aligner.Objective.GetName()}");
                // lines.Add($" - score: {aligner.AlignmentScore}");
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
