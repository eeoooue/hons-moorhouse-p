using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public enum UserRequestError
    {
        ContainsForeignCommands,
        RequestIsAmbiguous,
        NoArgumentsGiven,
        RequestIsInvalid
    }

    public class MAliInterface
    {
        private MAliFacade Facade = new MAliFacade();

        public void ProcessArguments(string[] args)
        {
            Dictionary<string, string?> table = InterpretArguments(args);

            if (args.Length == 0)
            {
                Facade.NotifyUserError(UserRequestError.NoArgumentsGiven);
                return;
            }

            if (SpecifiesSeed(table))
            {
                Facade.SetSeed(table["seed"]!);
            }

            if (ContainsForeignCommands(table))
            {
                Facade.NotifyUserError(UserRequestError.ContainsForeignCommands);
                return;
            }

            if (IsAmbiguousRequest(table))
            {
                Facade.NotifyUserError(UserRequestError.RequestIsAmbiguous);
                return;
            }

            if (IsAlignmentRequest(table))
            {
                Facade.PerformAlignment(table["input"]!, table["output"]!, table);
                return;
            }

            if (IsHelpRequest(table))
            {
                Facade.ProvideHelp();
                return;
            }

            if (IsInfoRequest(table))
            {
                Facade.ProvideInfo();
                return;
            }

            Facade.NotifyUserError(UserRequestError.RequestIsInvalid);
        }

        public Dictionary<string, string?> InterpretArguments(string[] args)
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

        public bool ContainsForeignCommands(Dictionary<string, string?> table)
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

        public bool IsAlignmentRequest(Dictionary<string, string?> table, bool checkAmbiguity=true)
        {
            if (checkAmbiguity && IsAmbiguousRequest(table))
            {
                return false;
            }

            if (table.ContainsKey("input") && table.ContainsKey("output"))
            {
                if (table["input"] is string && table["output"] is string)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsInfoRequest(Dictionary<string, string?> table, bool checkAmbiguity = true)
        {
            if (checkAmbiguity && IsAmbiguousRequest(table))
            {
                return false;
            }

            return table.ContainsKey("info");
        }

        public bool IsHelpRequest(Dictionary<string, string?> table, bool checkAmbiguity = true)
        {
            if (checkAmbiguity && IsAmbiguousRequest(table))
            {
                return false;
            }

            return table.ContainsKey("help");
        }

        public bool SpecifiesSeed(Dictionary<string, string?> table)
        {
            return table.ContainsKey("seed");
        }

        public bool IsAmbiguousRequest(Dictionary<string, string?> table)
        {
            int counter = 0;

            if (IsAlignmentRequest(table, false))
            {
                counter++;
            }

            if (IsHelpRequest(table, false))
            {
                counter++;
            }

            if (IsInfoRequest(table, false))
            {
                counter++;
            }

            return counter > 1;
        }

        public bool IsCommand(string candidate)
        {
            return candidate.StartsWith('-');
        }

        public bool IsArgument(string candidate)
        {
            return !candidate.StartsWith('-');
        }
    }
}
