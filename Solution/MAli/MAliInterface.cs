using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    internal class MAliInterface
    {
        private MAliFacade Facade = new MAliFacade();

        public void ProcessArguments(string[] args)
        {
            Dictionary<string, string?> table = InterpretArguments(args);

            if (IsAlignmentRequest(table))
            {
                Facade.PerformAlignment(table["input"]!, table["output"]!);
            }

            if (IsHelpRequest(table))
            {
                Facade.ProvideHelp();
            }

            if (IsInfoRequest(table))
            {
                Facade.ProvideInfo();
            }
        }

        private Dictionary<string, string?> InterpretArguments(string[] args)
        {
            Dictionary<string, string?> table = new Dictionary<string, string?>();

            for(int i=0; i < args.Length; i++)
            {
                if (IsCommand(args[i]))
                {
                    string command = args[i].Substring(1);
                    string? argument = null;

                    if (i < args.Length - 1)
                    {
                        if (IsArgument(args[i + 1])){
                            argument = args[i + 1];
                        }
                    }
                    table[command] = argument;
                }
            }

            return table;
        }

        private bool ContainsForeignCommands(Dictionary<string, string?> table)
        {
            MAliSpecification spec = new MAliSpecification();

            foreach(string command in table.Keys)
            {
                if (!spec.SupportedCommands.Contains(command))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsAlignmentRequest(Dictionary<string, string?> table)
        {
            if (table.ContainsKey("input") && table.ContainsKey("output"))
            {
                if (table["input"] is string && table["output"] is string)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInfoRequest(Dictionary<string, string?> table)
        {
            return table.ContainsKey("info");
        }

        private bool IsHelpRequest(Dictionary<string, string?> table)
        {
            return table.ContainsKey("help");
        }

        private bool IsCommand(string candidate)
        {
            return candidate.StartsWith('-');
        }

        private bool IsArgument(string candidate)
        {
            return !candidate.StartsWith('-');
        }

        
    }
}
