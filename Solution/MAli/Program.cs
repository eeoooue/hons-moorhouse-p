using LibBioInfo;
using LibFileIO.AlignmentWriters;
using LibFileIO.SequenceReaders;

namespace MAli
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();

        static void Main(string[] args)
        {
            Console.WriteLine("MAli - dev. build");
            TestFastaReadAndWrite();
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
