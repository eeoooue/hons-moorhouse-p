namespace MAli
{
    internal class Program
    {
        private static MAliInterface Interface = new MAliInterface();

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Interface.ProcessArguments(args);
            }
            else
            {
                Interface.ProcessArguments(args);
            }
        }
    }
}
