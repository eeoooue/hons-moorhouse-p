using MAli.AlignmentConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public class MAliSpecification
    {
        public HashSet<string> SupportedCommands = new HashSet<string>();
        public Dictionary<string, string> CommandDescriptions = new Dictionary<string, string>();
        public string Version = "v1.3";

        public MAliSpecification()
        {
            AddSupportedCommands();
            AddCommandDescriptions();
        }

        public void AddSupportedCommands()
        {
            SupportedCommands.Add("input");
            SupportedCommands.Add("output");
            SupportedCommands.Add("seed");
            SupportedCommands.Add("tag");

            SupportedCommands.Add("iterations");
            SupportedCommands.Add("seconds");

            SupportedCommands.Add("refine");
            SupportedCommands.Add("batch");

            SupportedCommands.Add("timestamp");
            SupportedCommands.Add("debug");
            SupportedCommands.Add("frames");
            SupportedCommands.Add("help");

            SupportedCommands.Add("pareto");
            SupportedCommands.Add("scorefile");
        }

        public void AddCommandDescriptions()
        {
            CommandDescriptions.Add("input", "Specify the input file of sequences to be aligned.");
            CommandDescriptions.Add("output", "Specify the output file path for the alignment of sequences.");
            CommandDescriptions.Add("seed", "Specify a seed value for reproducible results.");
            CommandDescriptions.Add("tag", "Specify a suffix to be included in the output filename.");

            CommandDescriptions.Add("iterations", "Specify the number of iterations of alignment to be performed.");
            CommandDescriptions.Add("seconds", "Specify the number of seconds of alignment to be performed.");
            CommandDescriptions.Add("refine", "(flag) Refine the alignment provided rather than starting from a randomized state.");
            CommandDescriptions.Add("batch", "(flag) Align a series of inputs. Specify the directories as 'input' and 'output' arguments.");

            CommandDescriptions.Add("timestamp", "(flag) Includes a completion date-time timestamp in the output filename.");
            CommandDescriptions.Add("debug", "(flag) View debugging information throughout the alignment process.");
            CommandDescriptions.Add("frames", "(flag) Saves the alignment state to a 'frames' subfolder after each iteration.");
            CommandDescriptions.Add("help", "Display the list of supported commands.");

            CommandDescriptions.Add("pareto", "(flag) Output a selection of alignments, approximating the Pareto front");
            CommandDescriptions.Add("scorefile", "(flag) Output a separate .maliscore file containing the alignment's objective scores");
        }

        public void ListCurrentVersion()
        {
            Console.WriteLine($"MAli ({Version}) - Metaheuristic Aligner");
        }

        public void ListSupportedCommands()
        {
            Console.WriteLine("Supported commands:");
            foreach (string command in SupportedCommands)
            {
                Console.WriteLine($"   -{command} : {CommandDescriptions[command]}");
            }
            Console.WriteLine();
        }
    }
}
