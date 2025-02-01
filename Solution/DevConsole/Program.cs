using LibAlignment.Helpers;
using LibBioInfo;
using LibFileIO.AlignmentReaders;
using MAli;

namespace DevConsole
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();
        private static MAliInterface Interface = new MAliInterface();
        private static AlignmentDebugHelper Painter = new AlignmentDebugHelper();

        static void Main(string[] args)
        {
            // clustalformat_BB11001.aln

            // RunMAli("-input clustalformat_BB11001.aln -output test -iterations 1000 -debug -refine");

            // RunMAli("-help");

            // RunMAli("-input BB11001 -output test -debug");
            RunMAli("-input BB11001 -output test -seconds 10 -iterations 1000 -debug");


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
