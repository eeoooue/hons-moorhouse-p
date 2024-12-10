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
        public string Version = "v1.0";

        public MAliSpecification()
        {
            AddSupportedCommands();
            AddCommandDescriptions();
        }

        public void AddSupportedCommands()
        {
            SupportedCommands.Add("input");
            SupportedCommands.Add("output");
            SupportedCommands.Add("help");
            SupportedCommands.Add("seed");
            SupportedCommands.Add("iterations");
            SupportedCommands.Add("timestamp");
            SupportedCommands.Add("tag");
            SupportedCommands.Add("debug");
        }

        public void AddCommandDescriptions()
        {
            CommandDescriptions.Add("input", "Specify the input file of sequences to be aligned.");
            CommandDescriptions.Add("output", "Specify the output file path for the alignment of sequences.");
            CommandDescriptions.Add("help", "Display the list of supported commands.");
            CommandDescriptions.Add("seed", "Specify a seed value for reproducible results.");
            CommandDescriptions.Add("iterations", "Specify the number of iterations of refinement to be performed.");
            CommandDescriptions.Add("timestamp", "Includes a completion date-time timestamp in the output filename.");
            CommandDescriptions.Add("tag", "Specify a suffix to be included in the output filename.");
            CommandDescriptions.Add("debug", "View debugging information throughout the alignment process.");
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
