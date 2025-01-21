using LibAlignment.Helpers;
using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
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

            RunMAli("-input BB11001 -output test -iterations 10 -debug");
            // RunMAli("-input synth_polarizer_one -output test -iterations 1000 -debug");
            // RunMAli("-input synth_cropped_segments -output test -iterations 1000 -debug");
            // RunMAli("-input synth_polarizing_checkerboard -output test -iterations 1000 -debug");
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
