using LibBioInfo;
using LibFileIO.AlignmentWriters;
using LibFileIO.AlignmentReaders;

namespace LibFileIO
{
    public class FileHelper : IAlignmentReader, IAlignmentWriter
    {
        private IAlignmentReader Reader;

        public FileHelper()
        {
            List<IAlignmentReader> readers = new List<IAlignmentReader>()
            {
                new ClustalReader(),
                new FastaReader(),
            };

            Reader = new DynamicReader(readers);
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
            WriteAlignment(alignment, AlignmentOutputFormat.FASTA, filename);
        }

        public void WriteAlignment(Alignment alignment, AlignmentOutputFormat format, string filename)
        {
            IAlignmentWriter writer = GetAssociatedWriter(format);
            writer.WriteAlignmentTo(alignment, filename);
        }

        private IAlignmentWriter GetAssociatedWriter(AlignmentOutputFormat format)
        {
            switch (format)
            {
                case AlignmentOutputFormat.ClustalW:
                    return new ClustalWriter();
                default:
                    return new FastaWriter();
            }
        }
    }
}
