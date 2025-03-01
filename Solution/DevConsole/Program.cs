using LibAlignment.Helpers;
using LibBioInfo;
using LibFileIO;
using LibFileIO.AlignmentReaders;
using LibFileIO.AlignmentWriters;
using LibModification;
using LibModification.AlignmentModifiers;
using LibModification.Mechanisms;
using LibSimilarity;
using MAli;
using MAli.AlignmentConfigs;
using MAli.Helpers;

namespace DevConsole
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();
        private static MAliInterface Interface = new MAliInterface();
        private static BlockShufflingPlayground BlockShufflingPlayground = new BlockShufflingPlayground();

        public static void Main(string[] args)
        {
            // TestGapInsertion();

            // TestingParetoAlignment();

            RunMAli("-input BB11001 -output test -debug -seconds 100");

            // RunMAli("-input BB11002 -output test -debug -seconds 100 -pareto 3");
            // RunMAli("-input real_marine_life -output test -debug -seconds 100");
            // RunMAli("-input 1a0cA_1ubpC -output test -debug -seconds 100");


            // TestingConfigParsing();
        }


        static void TestingMAli()
        {
            // RunMAli("-input clustalformat_BB11001.aln -output test -iterations 1000 -debug -refine");
            // RunMAli("-help");
            // RunMAli("-input BB11001 -output test -debug -format clustal");

            RunMAli("-input BB11001 -output test -debug -seconds 100");

            // RunMAli("-input BB11001 -output test -debug");
            // RunMAli("-input 1a0cA_1ubpC -output test -debug");

            // RunMAli("-input synth_polarizer_one -output test -iterations 1000 -debug");
            // RunMAli("-input synth_cropped_segments -output test -iterations 1000 -debug");
            // RunMAli("-input synth_polarizing_checkerboard -output test -iterations 1000 -debug"); // crashes with refine
            // RunMAli("-input synth_polarizer_two -output test -iterations 1000 -debug");
            // RunMAli("-input real_marine_life -output test -iterations 1000 -debug");
        }

        static void TestingMAliScoreonly()
        {
            RunMAli("-input clustalformat_BB11001.aln -output test -scoreonly");
        }


        static void TestingMAliPareto()
        {
            // RunMAli("-input BB11001 -output test -debug -scorefile -pareto");
        }

        static void TestingBatchAlignment()
        {
            RunMAli("-input batchin -output batchout -batch");
            // RunMAli("-input batchin -output batchout -debug -batch");
        }

        static void TestingBatchScoring()
        {
            RunMAli("-input batchin -output batchout -batch -scoreonly");
            // RunMAli("-input batchin -output batchout -debug -batch");
        }

        static void TestingParetoAlignment()
        {
            // RunMAli("-input BB11001 -output test -seconds 100 -debug");
            // RunMAli("-input 1a0cA_1ubpC -output test -iterations 1000 -debug -pareto 20");
            RunMAli("-input BB11001 -output test -iterations 1000 -debug -pareto 10");
            // RunMAli("-input BB11001 -output test -iterations 1000 -pareto");
        }

        static void RunMAli(string arguments)
        {
            string[] args = UnpackArguments(arguments);
            Interface.ProcessArguments(args);
        }
        static string[] UnpackArguments(string arguments)
        {
            if (arguments.Length > 0)
            {
                return arguments.Split(' ');
            }

            return new string[] { };
        }
    }
}
