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
        private static AlignmentDebugHelper Painter = new AlignmentDebugHelper();


        public static void Main(string[] args)
        {
            // TestSimGuide();

            TestGapInsertion();

            // TestingMAli();

            // RunMAli("-input BB11001 -output test -debug");

            // TestingConfigParsing();
        }


        public static void TestGapInsertion()
        {
            FileHelper helper = new FileHelper();
            Alignment alignment = helper.ReadAlignmentFrom("BB11001");

            Console.WriteLine("BEFORE");
            Helper.PrintAlignmentState(alignment);

            ColumnInsertion.InsertEmptyColumn(alignment, 0);

            // ResidueShift.ShiftResidue(alignment, 0, 0, ShiftDirection.Leftwise);

            Console.WriteLine("AFTER");
            Helper.PrintAlignmentState(alignment);
        }



        public static void TestSimGuide()
        {
            FileHelper helper = new FileHelper();
            Alignment alignment = helper.ReadAlignmentFrom("BB11001");

            SimilarityGuide.SetSequences(alignment.Sequences);

            int n = alignment.Sequences.Count;

            HeuristicPairwiseModifier modifier = new HeuristicPairwiseModifier();

            SayGraphState();

            for (int i=0; i<n; i++)
            {
                for(int j=i+1; j<n; j++)
                {
                    if (i != j)
                    {
                        modifier.AlignPairOfSequences(alignment, i, j);
                        SimilarityGuide.TryUpdateSimilarity();
                        SayGraphState();
                    }
                }
            }
        }

        public static void SayGraphState()
        {
            SimilarityGraph graph = SimilarityGuide.Graph;

            int connected = graph.ConnectedNodes;
            int n = graph.NodeCount;
            double saturation = Math.Round(graph.GetPercentageSaturation(), 0);
            int percent = (int)Math.Round((double)(100 * connected / n), 0);

            Console.WriteLine($"Graph contains {connected} connected nodes ({percent}%)");
            Console.WriteLine($"Graph is {saturation}% saturated.");
            Console.WriteLine();
            graph.DebugConnections();
            Console.WriteLine();
        }

        public static void TestingClustalWriter()
        {
            FileHelper helper = new FileHelper();
            Alignment alignment = helper.ReadAlignmentFrom("clustalformat_BB11001.aln");

            ClustalWriter writer = new ClustalWriter();
            List<string> lines = writer.CreateAlignmentLines(alignment);

            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
            writer.WriteAlignmentTo(alignment, "clustalaln");

            FileHelper stuff = new FileHelper();

            Alignment check = stuff.ReadAlignmentFrom("clustalaln.aln");
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

            // RunMAli("-input BB11001 -output test -debug -format clustal");


            // RunMAli("-input BB11001 -output test -debug -scorefile -pareto");

            RunMAli("-input BB11001 -output test -debug");

            // RunMAli("-input BB11002 -output test -debug");

            // RunMAli("-input 1a0cA_1ubpC -output test -debug");

            // RunMAli("-input 1a0cA_1ubpC -output test -scoreonly");



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
