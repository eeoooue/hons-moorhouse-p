using LibAlignment;
using LibBioInfo;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    internal class AlignmentHelper
    {
        private FileHelper FileHelper = new FileHelper();
        private FrameHelper FrameHelper = new FrameHelper();
        private ResponseBank ResponseBank = new ResponseBank();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();

        private AlignmentConfig Config;

        public AlignmentHelper(AlignmentConfig config)
        {
            Config = config;
        }

        public void PerformAlignment(string inputPath, string outputPath, Dictionary<string, string?> table)
        {
            bool debugging = ArgumentHelper.CommandsIncludeFlag(table, "debug");
            bool emitFrames = ArgumentHelper.CommandsIncludeFlag(table, "frames");
            bool refineOnly = ArgumentHelper.CommandsIncludeFlag(table, "refine");
            string outputFilename = BuildFullOutputFilename(outputPath, table);

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    IIterativeAligner aligner = InitialiseAligner(alignment, debugging, refineOnly, table);
                    AlignIteratively(aligner, emitFrames, refineOnly);

                    FileHelper.WriteAlignmentTo(aligner.CurrentAlignment!, outputFilename);
                    Console.WriteLine($"Alignment written to destination: '{outputFilename}'");
                }
                else
                {
                    Console.WriteLine("Error: Sequences cannot be aligned.");
                }
            }
            catch (Exception e)
            {
                ResponseBank.ExplainException(e);
            }
        }


        public IIterativeAligner InitialiseAligner(Alignment alignment, bool debugging, bool refineOnly, Dictionary<string, string?> table)
        {
            IIterativeAligner aligner = Config.CreateAligner();

            if (debugging && aligner is IterativeAligner instance)
            {
                aligner = new DebuggingWrapper(instance);
            }

            int iterations = ArgumentHelper.UnpackSpecifiedIterations(table);
            if (iterations > 0)
            {
                aligner.IterationsLimit = iterations;
            }

            if (refineOnly)
            {
                aligner.InitializeForRefinement(alignment);
            }
            else
            {
                aligner.Initialize(alignment.Sequences);
            }

            return aligner;
        }


        public void AlignIteratively(IIterativeAligner aligner, bool emitFrames, bool refineOnly)
        {
            string context = $"Performing Multiple Sequence Alignment: {aligner.IterationsLimit} iterations.";
            if (refineOnly)
            {
                context += " (iterative refinement)";
            }

            Console.WriteLine(context);

            if (emitFrames)
            {
                FrameHelper.CheckCreateFramesFolder();
            }

            while (aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                aligner.Iterate();
                if (emitFrames && aligner.CurrentAlignment is Alignment alignment)
                {
                    FrameHelper.SaveCurrentFrame(alignment, aligner.IterationsCompleted);
                }
            }
        }

        public string BuildFullOutputFilename(string outputName, Dictionary<string, string?> table)
        {
            string result = outputName;
            if (ArgumentHelper.CommandsIncludeFlag(table, "timestamp"))
            {
                result += $"_{GetTimeStamp()}";
            }
            if (ArgumentHelper.CommandsIncludeFlag(table, "tag"))
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
    }
}
