using LibBioInfo;
using LibFileIO.AlignmentWriters;
using LibFileIO.SequenceReaders;

namespace MAli
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();
        private static MAliInterface Interface = new MAliInterface();

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("MAli - dev. build");
            }
            else
            {
                Interface.ProcessArguments(args);
            }
        }

        public void CheckTestcaseCanBeAligned(string filename)
        {
            throw new NotImplementedException();
        }

        
    }
}
