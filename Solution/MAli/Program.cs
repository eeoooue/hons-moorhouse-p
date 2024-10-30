using LibBioInfo;
using LibFileIO.Readers;

namespace MAli
{
    internal class Program
    {
        private static DevHelper Helper = new DevHelper();

        static void Main(string[] args)
        {
            Console.WriteLine("MAli - dev. build");
            TestFastaReader();
        }

        static void TestFastaReader()
        {
            FastaReader reader = new FastaReader();
            string filename = "BB11001";

            List<BioSequence> sequences = reader.ReadSequencesFrom(filename);
            Helper.PrintSequences(sequences);
        }
    }
}
