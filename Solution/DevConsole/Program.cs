using LibAlignment.Helpers;
using LibBioInfo;
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
            // RunMAli("-input BB11001 -output test -iterations 1000 -debug");
            BioSequence sequence = new BioSequence("asdanjsd", "ACGT-ACGTA-CG--T-A--CGT-A-C");
            Painter.PaintSequence(sequence);
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
