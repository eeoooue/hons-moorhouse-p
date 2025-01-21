using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentReaders
{
    internal class DynamicReader : IAlignmentReader
    {
        List<IAlignmentReader> Readers = new List<IAlignmentReader>();

        public DynamicReader(List<IAlignmentReader> readers)
        {
            Readers = readers;
        }

        public bool FileAppearsReadable(string filename)
        {
            foreach(IAlignmentReader reader in Readers)
            {
                if (reader.FileAppearsReadable(filename))
                {
                    return true;
                }
            }

            return false;
        }

        public Alignment ReadAlignmentFrom(string filename)
        {
            List<BioSequence> sequences = ReadSequencesFrom(filename);
            return new Alignment(sequences, true);
        }

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            foreach (IAlignmentReader reader in Readers)
            {
                if (reader.FileAppearsReadable(filename))
                {
                    try
                    {
                        List<BioSequence> sequences = reader.ReadSequencesFrom(filename);
                        return sequences;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            throw new InvalidOperationException("The file format could not be read");
        }
    }
}
