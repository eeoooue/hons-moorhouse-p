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
        public string Version = "v0.1";

        public MAliSpecification()
        {
            AddSupportedCommands();
        }

        public void AddSupportedCommands()
        {
            SupportedCommands.Add("input");
            SupportedCommands.Add("output");
            SupportedCommands.Add("help");
            SupportedCommands.Add("info");
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
                Console.WriteLine($"-{command}");
            }
            Console.WriteLine();
        }
    }
}
