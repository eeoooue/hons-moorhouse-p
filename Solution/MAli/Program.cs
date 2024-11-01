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
                TestFastaReadAndWrite();
            }
            else
            {
                Interface.ProcessArguments(args);
            }
        }

        static void TestFastaReader()
        {
            FastaReader reader = new FastaReader();
            string filename = "BB11001";

            List<BioSequence> sequences = reader.ReadSequencesFrom(filename);
            Helper.PrintSequences(sequences);
        }

        static void TestFastaReadAndWrite()
        {
            FastaReader reader = new FastaReader();
            string filename = "BB11001";

            List<BioSequence> sequences = reader.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);
            FastaWriter writer = new FastaWriter();
            writer.WriteAlignmentTo(alignment, "testoutput.faa");
        }

        static void TestAlignmentCanBeInitialized()
        {
            FastaReader reader = new FastaReader();
            string filename = "BB11001";

            List<BioSequence> sequences = reader.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);
            Helper.PrintAlignmentState(alignment);
        }
    }
}
