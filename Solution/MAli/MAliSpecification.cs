﻿using MAli.AlignmentConfigs;
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
        public string Version = "v1.2";

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
            SupportedCommands.Add("iterations");
            SupportedCommands.Add("tag");

            SupportedCommands.Add("refine");
            SupportedCommands.Add("timestamp");
            SupportedCommands.Add("debug");
            SupportedCommands.Add("frames");
            SupportedCommands.Add("help");
        }

        public void AddCommandDescriptions()
        {
            CommandDescriptions.Add("input", "Specify the input file of sequences to be aligned.");
            CommandDescriptions.Add("output", "Specify the output file path for the alignment of sequences.");
            CommandDescriptions.Add("seed", "Specify a seed value for reproducible results.");
            CommandDescriptions.Add("iterations", "Specify the number of iterations of refinement to be performed.");
            CommandDescriptions.Add("tag", "Specify a suffix to be included in the output filename.");

            CommandDescriptions.Add("refine", "(flag) Refine the alignment provided rather than starting from a randomized state.");
            CommandDescriptions.Add("timestamp", "(flag) Includes a completion date-time timestamp in the output filename.");
            CommandDescriptions.Add("debug", "(flag) View debugging information throughout the alignment process.");
            CommandDescriptions.Add("frames", "(flag) Saves the alignment state to a 'frames' subfolder after each iteration.");
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
