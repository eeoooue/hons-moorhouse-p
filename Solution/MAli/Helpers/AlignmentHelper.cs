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
    public class AlignmentHelper
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
            AlignmentInstructions instructions = ArgumentHelper.UnpackInstructions(inputPath, outputPath, table);

            try
            {
                Console.WriteLine($"Reading sequences from source: '{inputPath}'");
                List<BioSequence> sequences = FileHelper.ReadSequencesFrom(inputPath);
                Alignment alignment = new Alignment(sequences, true);

                if (alignment.SequencesCanBeAligned())
                {
                    IIterativeAligner aligner = Config.InitialiseAligner(alignment, instructions);
                    AlignIteratively(aligner, instructions);
                    FileHelper.WriteAlignmentTo(aligner.CurrentAlignment!, instructions.OutputPath);
                    Console.WriteLine($"Alignment written to destination: '{instructions.OutputPath}'");
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

        public void AlignIteratively(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            Console.WriteLine(instructions.GetContextString());

            if (instructions.EmitFrames)
            {
                FrameHelper.CheckCreateFramesFolder();
            }

            if (instructions.LimitedByIterations())
            {
                AlignUntilIterationLimit(aligner, instructions);
            }
            else if (instructions.LimitedBySeconds())
            {
                AlignUntilSecondsDeadline(aligner, instructions);
            }
        }

        public void AlignUntilIterationLimit(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            while (aligner.IterationsCompleted < aligner.IterationsLimit)
            {
                PerformIterationOfAlignment(aligner, instructions);
            }
        }

        public void AlignUntilSecondsDeadline(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            DateTime deadline = DateTime.Now.AddSeconds(instructions.SecondsLimit);
            while (DateTime.Now < deadline)
            {
                PerformIterationOfAlignment(aligner, instructions);
            }
        }

        public void PerformIterationOfAlignment(IIterativeAligner aligner, AlignmentInstructions instructions)
        {
            aligner.Iterate();
            if (instructions.EmitFrames && aligner.CurrentAlignment is Alignment alignment)
            {
                FrameHelper.SaveCurrentFrame(alignment, aligner.IterationsCompleted);
            }
        }
    }
}
