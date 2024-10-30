using LibBioInfo;
using LibFileIO.AlignmentWriters;
using LibFileIO.SequenceReaders;

namespace LibFileIO
{
    public class FileHelper : ISequenceReader, IAlignmentWriter
    {
        private ISequenceReader Reader = new FastaReader();
        private IAlignmentWriter Writer = new FastaWriter();

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
