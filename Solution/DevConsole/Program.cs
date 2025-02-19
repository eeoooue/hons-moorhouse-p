using LibAlignment.Helpers;
using LibBioInfo;
using LibFileIO;
using LibFileIO.AlignmentReaders;
using LibFileIO.AlignmentWriters;
using MAli;
using MAli.AlignmentConfigs;
using MAli.Helpers;

namespace DevConsole
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();
        private static MAliInterface Interface = new MAliInterface();
        private static AlignmentDebugHelper Painter = new AlignmentDebugHelper();


        static void Main(string[] args)
        {
            TestingClustalWriter();
            // TestingMAli();

            // RunMAli("-input BB11001 -output test -debug");

            // TestingConfigParsing();
        }

        static void TestingClustalWriter()
        {
            FileHelper helper = new FileHelper();
            Alignment alignment = helper.ReadAlignmentFrom("clustalformat_BB11001.aln");

            ClustalWriter writer = new ClustalWriter();
            List<string> lines = writer.CreateAlignmentLines(alignment);

            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        static void TestingConfigParsing()
        {
            //Sprint05Config baseCfg = new Sprint05Config();
            //UserConfig config = new UserConfig(baseCfg);

            //config.CreateAligner();

            RunMAli("-input BB11001 -output test -debug -config config.json");
        }

        static void TestingBatchAlignment()
        {
            RunMAli("-input batchin -output batchout -batch");
            // RunMAli("-input batchin -output batchout -debug -batch");
        }

        static void TestingParetoAlignment()
        {
            // RunMAli("-input BB11001 -output test -seconds 100 -debug");
            RunMAli("-input BB11001 -output test -iterations 1000 -debug -pareto 5");
            // RunMAli("-input BB11001 -output test -iterations 1000 -pareto");
        }

        static void TestingMAli()
        {
            // clustalformat_BB11001.aln

            // RunMAli("-input clustalformat_BB11001.aln -output test -iterations 1000 -debug -refine");

            // RunMAli("-help");

            RunMAli("-input BB11001 -output test -debug");


            // RunMAli("-input BB11001 -output test -debug -scorefile -pareto");
            // RunMAli("-input BB11001 -output test -debug -scorefile");


            // RunMAli("-input BB11001 -output test -debug -scorefile");


            // RunMAli("-input BB11001 -output test -iterations 1000 -debug");

            // RunMAli("-input BB11001 -output test -iterations 1000 -debug");
            // RunMAli("-input synth_polarizer_one -output test -iterations 1000 -debug");
            // RunMAli("-input synth_cropped_segments -output test -iterations 1000 -debug");
            // RunMAli("-input synth_polarizing_checkerboard -output test -iterations 1000 -debug"); // crashes with refine
            // RunMAli("-input synth_polarizer_two -output test -iterations 1000 -debug");
            // RunMAli("-input real_marine_life -output test -iterations 1000 -debug");
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
