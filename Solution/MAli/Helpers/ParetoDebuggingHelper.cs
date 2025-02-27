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
            lines.Add("");
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

            foreach (string item in aligner.GetAlignerInfo())
            {
                lines.Add(item);
            }
        }

        public void CollectAlignmentStateInfo(ParetoIterativeAligner aligner, List<string> lines)
        {
            foreach (string item in aligner.GetSolutionInfo())
            {
                lines.Add(item);
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
