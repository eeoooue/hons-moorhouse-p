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
        public string Version = "v0.1";

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
        }

        public void AddCommandDescriptions()
        {
            CommandDescriptions.Add("input", "Specify the input file of sequences to be aligned.");
            CommandDescriptions.Add("output", "Specify the output file path for the alignment of sequences.");
            CommandDescriptions.Add("help", "Display the list of supported commands.");
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
