using LibBioInfo;
using LibFileIO.Readers;

namespace LibFileIO
{
    public class FileHelper
    {
        private ISequenceReader Reader = new FastaReader();

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            return Reader.ReadSequencesFrom(filename);
        }

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
