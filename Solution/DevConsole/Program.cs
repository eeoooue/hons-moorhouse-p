using MAli;

namespace DevConsole
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();
        private static MAliInterface Interface = new MAliInterface();

        static void Main(string[] args)
        {
            RunMAli("-input BB11001 -output test -iterations 10");
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
