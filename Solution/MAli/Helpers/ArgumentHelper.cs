using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class ArgumentHelper
    {
        public UserRequest UnpackInstructions(Dictionary<string, string?> table)
        {
            AlignmentRequest request = new AlignmentRequest();

            if (table.ContainsKey("pareto"))
            {
                request = new ParetoAlignmentRequest();
            }
            if (table.ContainsKey("batch"))
            {
                request = new BatchAlignmentRequest();
            }

            try
            {
                request.Debug = CommandsIncludeFlag(table, "debug");
                request.EmitFrames = CommandsIncludeFlag(table, "frames");
                request.RefineOnly = CommandsIncludeFlag(table, "refine");
                request.IncludeScoreFile = CommandsIncludeFlag(table, "scorefile");
                request.IterationsLimit = UnpackSpecifiedIterations(table);
                request.SecondsLimit = UnpackSpecifiedSeconds(table);
                request.InputPath = table["input"]!;
                request.OutputPath = BuildFullOutputFilename(table["output"]!, table);
                request.CheckAddDefaultRestrictions();

                request.SpecifiesSeed = CommandsIncludeFlag(table, "seed");
                if (request.SpecifiesSeed)
                {
                    request.Seed = table["seed"]!;
                }

                request.SpecifiesCustomConfig = CommandsIncludeFlag(table, "config");

                return request;
            }
            catch
            {
                return new MalformedRequest("Error: Failed to unpack alignment request.");
            }
        }

        public string BuildFullOutputFilename(string outputName, Dictionary<string, string?> table)
        {
            string result = outputName;
            if (CommandsIncludeFlag(table, "timestamp"))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                result += $"_{timestamp}";
            }
            if (CommandsIncludeFlag(table, "tag"))
            {
                string? specifiedTag = table["tag"];
                if (specifiedTag is string tag)
                {
                    result += $"_{tag}";
                }
            }
            result += ".faa";

            return result;
        }

        public UserRequest InterpretRequest(string[] args)
        {
            Dictionary<string, string?> table = InterpretArguments(args);

            if (ContainsForeignCommands(table))
            {
                return new MalformedRequest("Error: Request contains foreign commands.");
            }

            if (SpecifiesMultipleLimitations(table))
            {
                return new MalformedRequest("Error: MAli doesn't support limiting both 'iterations' & 'seconds' at once.");
            }

            if (IsAmbiguousRequest(table))
            {
                return new MalformedRequest("Error: Request is ambiguous.");
            }

            if (IsHelpRequest(table))
            {
                return new HelpRequest();
            }

            if (IsAlignmentRequest(table))
            {
                return InterpretAlignmentRequest(args);
            }

            return new MalformedRequest();
        }

        public UserRequest InterpretAlignmentRequest(string[] args)
        {
            Dictionary<string, string?> table = InterpretArguments(args);

            if (table.ContainsKey("batch") && table.ContainsKey("pareto"))
            {
                return new MalformedRequest();
            }

            UserRequest request = UnpackInstructions(table);
            return request;
        }

        public Dictionary<string, string?> InterpretArguments(string[] args)
        {
            Dictionary<string, string?> table = new Dictionary<string, string?>();

            for (int i = 0; i < args.Length; i++)
            {
                if (IsCommand(args[i]))
                {
                    string command = args[i].Substring(1);
                    string? argument = null;

                    if (i < args.Length - 1)
                    {
                        if (IsArgument(args[i + 1]))
                        {
                            argument = args[i + 1];
                        }
                    }
                    table[command] = argument;
                }
            }

            return table;
        }

        public bool CommandsIncludeFlag(Dictionary<string, string?> table, string flag)
        {
            bool result = table.ContainsKey(flag);
            return result;
        }

        public bool ContainsForeignCommands(Dictionary<string, string?> table)
        {
            MAliSpecification spec = new MAliSpecification();

            foreach (string command in table.Keys)
            {
                if (!spec.SupportedCommands.Contains(command))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsAlignmentRequest(Dictionary<string, string?> table, bool checkAmbiguity = true)
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

        public bool IsHelpRequest(Dictionary<string, string?> table, bool checkAmbiguity = true)
        {
            if (checkAmbiguity && IsAmbiguousRequest(table))
            {
                return false;
            }

            return table.ContainsKey("help");
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

        public int UnpackSpecifiedIterations(Dictionary<string, string?> table)
        {
            if (table.ContainsKey("iterations"))
            {
                string? iterationsValue = table["iterations"];
                if (iterationsValue is string iterations)
                {
                    if (int.TryParse(iterations, out int result))
                    {
                        if (result > 0.1)
                        {
                            return result;
                        }
                    }
                }
            }

            return 0;
        }

        public double UnpackSpecifiedSeconds(Dictionary<string, string?> table)
        {
            if (table.ContainsKey("seconds"))
            {
                string? secondsValue = table["seconds"];
                if (secondsValue is string seconds)
                {
                    if (double.TryParse(seconds, out double result))
                    {
                        return result;
                    }
                }
            }

            return 0.0;
        }

        public bool SpecifiesMultipleLimitations(Dictionary<string, string?> table)
        {
            return CommandsIncludeFlag(table, "iterations") && CommandsIncludeFlag(table, "seconds");
        }
    }
}
