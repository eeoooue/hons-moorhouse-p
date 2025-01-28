using LibBioInfo;
using LibFileIO.AlignmentWriters;
using LibFileIO.AlignmentReaders;

namespace LibFileIO
{
    public class FileHelper : IAlignmentReader, IAlignmentWriter
    {
        private IAlignmentReader Reader;
        private IAlignmentWriter Writer;

        public FileHelper()
        {
            List<IAlignmentReader> readers = new List<IAlignmentReader>()
            {
                new ClustalReader(),
                new FastaReader(),
            };

            Reader = new DynamicReader(readers);
            Writer = new FastaWriter();
        }

        public bool FileAppearsReadable(string filename)
        {
            return Reader.FileAppearsReadable(filename);
        }

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
