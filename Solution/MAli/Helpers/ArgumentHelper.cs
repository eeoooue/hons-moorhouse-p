﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class ArgumentHelper
    {

        public AlignmentInstructions UnpackInstructions(Dictionary<string, string?> table)
        {
            AlignmentInstructions instructions = new AlignmentInstructions();
            instructions.Debug = CommandsIncludeFlag(table, "debug");
            instructions.EmitFrames = CommandsIncludeFlag(table, "frames");
            instructions.RefineOnly = CommandsIncludeFlag(table, "refine");
            instructions.IncludeScoreFile = CommandsIncludeFlag(table, "scorefile");
            instructions.IterationsLimit = UnpackSpecifiedIterations(table);
            instructions.SecondsLimit = UnpackSpecifiedSeconds(table);
            instructions.InputPath = table["input"]!;
            instructions.OutputPath = BuildFullOutputFilename(table["output"]!, table);

            instructions.CheckAddDefaultRestrictions();

            return instructions;
        }

        public AlignmentInstructions UnpackInstructions(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            AlignmentInstructions instructions = new AlignmentInstructions();
            instructions.Debug = CommandsIncludeFlag(table, "debug");
            instructions.EmitFrames = CommandsIncludeFlag(table, "frames");
            instructions.RefineOnly = CommandsIncludeFlag(table, "refine");
            instructions.IncludeScoreFile = CommandsIncludeFlag(table, "scorefile");
            instructions.IterationsLimit = UnpackSpecifiedIterations(table);
            instructions.SecondsLimit = UnpackSpecifiedSeconds(table);
            instructions.InputPath = inputPath;
            instructions.OutputPath = BuildFullOutputFilename(outputPath, table);

            instructions.CheckAddDefaultRestrictions();

            return instructions;
        }

        public string BuildFullOutputFilename(string outputName, Dictionary<string, string?> table)
        {
            string result = outputName;
            if (CommandsIncludeFlag(table, "timestamp"))
            {
                result += $"_{GetTimeStamp()}";
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

        public string GetTimeStamp()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            return timestamp;
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

        public bool IsBatchAlignmentRequest(Dictionary<string, string?> table, bool checkAmbiguity = true)
        {
            return table.ContainsKey("batch") && IsAlignmentRequest(table);
        }

        public bool IsParetoAlignmentRequest(Dictionary<string, string?> table, bool checkAmbiguity = true)
        {
            return table.ContainsKey("pareto") && IsAlignmentRequest(table);
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
