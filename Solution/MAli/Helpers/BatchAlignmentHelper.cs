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
        private FileHelper FileHelper = new FileHelper();
        private FrameHelper FrameHelper = new FrameHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();
        private AlignmentConfig Config;
        private AlignmentHelper AlignmentHelper;

        public BatchAlignmentHelper(AlignmentConfig config = null)
        {
            Config = config;
            AlignmentHelper = new AlignmentHelper(config);
        }

        public void PerformBatchAlignment(string inDirectory, string outDirectory, Dictionary<string, string?> table = null)
        {
            if (!CheckInputDirectoryExists(inDirectory))
            {
                throw new Exception("Input directory not found");
            }

            EnsureOutputDirectoryExists(outDirectory);

            List<string> inputPaths = CollectInputPaths(inDirectory);
            List<string> outputPaths = CreateOutputPaths(inDirectory, outDirectory);

            int n = inputPaths.Count;

            for(int i=0; i<n; i++)
            {
                if (table.ContainsKey("debug"))
                {
                    Console.Clear();
                }
                Console.WriteLine($"Batch Alignment: Alignment {i+1} of {n}");
                AlignmentHelper.PerformAlignment(inputPaths[i], outputPaths[i], table);
                Console.WriteLine();
            }
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
