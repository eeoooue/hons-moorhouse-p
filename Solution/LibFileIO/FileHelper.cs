using LibBioInfo;
using LibFileIO.AlignmentWriters;
using LibFileIO.AlignmentReaders;

namespace LibFileIO
{
    public class FileHelper : IAlignmentReader, IAlignmentWriter
    {
        private IAlignmentReader Reader = new FastaReader();
        private IAlignmentWriter Writer = new FastaWriter();

        public Alignment ReadAlignmentFrom(string filename)
        {
            List<BioSequence> sequences = ReadSequencesFrom(filename);
            return new Alignment(sequences, true);
        }

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            return Reader.ReadSequencesFrom(filename);
        }

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            Writer.WriteAlignmentTo(alignment, filename);
        }
    }
}
