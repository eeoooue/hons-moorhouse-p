using LibAlignment;
using LibBioInfo;
using LibFileIO;
using MAli.AlignmentConfigs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class BatchAlignmentHelper
    {
        private AlignmentHelper AlignmentHelper;
        private AlignmentInstructions Instructions = null!;

        public BatchAlignmentHelper(AlignmentConfig config)
        {
            AlignmentHelper = new AlignmentHelper(config);
        }

        public void PerformBatchAlignment(AlignmentInstructions instructions)
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

            for(int i=0; i<n; i++)
            {
                if (Instructions.Debug)
                {
                    Console.Clear();
                }
                Console.WriteLine($"Batch Alignment: Alignment {i+1} of {n}");
                PerformAlignment(inputPaths[i], outputPaths[i]);
                Console.WriteLine();
            }
        }

        public void PerformAlignment(string inputPath, string outputPath)
        {
            AlignmentInstructions instructions = Instructions.GetCopy();
            instructions.InputPath = inputPath;
            instructions.OutputPath = outputPath;

            AlignmentHelper.PerformAlignment(instructions);
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
            foreach(string path in inputPaths)
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
            foreach(string input in inputFilenames)
            {
                string filepath = $"{outDirectory}\\{input}";
                result.Add(filepath);
            }
            return result;
        }
    }
}
