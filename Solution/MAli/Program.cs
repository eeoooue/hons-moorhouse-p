using LibBioInfo;
using LibFileIO.SequenceReaders;

namespace MAli
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();

        static void Main(string[] args)
        {
            Console.WriteLine("MAli - dev. build");
            TestAlignmentCanBeInitialized();
        }

        static void TestFastaReader()
        {
            FastaReader reader = new FastaReader();
            string filename = "BB11001";

            List<BioSequence> sequences = reader.ReadSequencesFrom(filename);
            Helper.PrintSequences(sequences);
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
