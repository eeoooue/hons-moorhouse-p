﻿using LibAlignment;
using LibBioInfo;
using LibFileIO;
using MAli.AlignmentConfigs;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentEngines
{
    public class BatchAlignmentEngine : IAlignmentEngine
    {
        private AlignmentEngine AlignmentEngine;
        private AlignmentRequest Instructions = null!;

        public BatchAlignmentEngine(BaseAlignmentConfig config)
        {
            AlignmentEngine = new AlignmentEngine(config);
        }

        public void PerformAlignment(AlignmentRequest instructions)
        {
            Instructions = instructions;

            if (!CheckInputDirectoryExists(Instructions.InputPath))
            {
                throw new Exception("Input directory not found");
            }

            EnsureOutputDirectoryExists(Instructions.OutputPath);

            List<string> inputPaths = CollectInputPaths(Instructions.InputPath);
            List<string> outputPaths = CreateOutputPaths(Instructions.InputPath, Instructions.OutputPath);

            int n = inputPaths.Count;

            for (int i = 0; i < n; i++)
            {
                if (Instructions.Debug)
                {
                    Console.Clear();
                }
                Console.WriteLine($"Batch Operation: Alignment {i + 1} of {n}");
                PerformIndividualAlignment(inputPaths[i], outputPaths[i]);
                Console.WriteLine();
            }
        }

        public void PerformIndividualAlignment(string inputPath, string outputPath)
        {
            AlignmentRequest instructions = Instructions.GetCopy();
            instructions.InputPath = inputPath;
            instructions.OutputPath = outputPath;
            AlignmentEngine.PerformAlignment(instructions);
        }

        public bool CheckInputDirectoryExists(string inDirectory)
        {
            return Directory.Exists(inDirectory);
        }

        public void EnsureOutputDirectoryExists(string outDirectory)
        {
            if (!Directory.Exists(outDirectory))
            {
                Directory.CreateDirectory(outDirectory);
            }
        }

        public List<string> CollectInputPaths(string inDirectory)
        {
            return Directory.GetFiles(inDirectory).ToList();
        }

        public List<string> CollectInputFilenames(string inDirectory)
        {
            int n = inDirectory.Length + 1;
            List<string> inputPaths = CollectInputPaths(inDirectory);
            List<string> result = new List<string>();
            foreach (string path in inputPaths)
            {
                string filename = path.Substring(n);
                result.Add(filename);
            }

            return result;
        }

        public List<string> CreateOutputPaths(string inDirectory, string outDirectory)
        {
            List<string> inputFilenames = CollectInputFilenames(inDirectory);
            List<string> result = new List<string>();
            foreach (string input in inputFilenames)
            {
                string filepath = $"{outDirectory}\\{input}";
                result.Add(filepath);
            }
            return result;
        }
    }
}
