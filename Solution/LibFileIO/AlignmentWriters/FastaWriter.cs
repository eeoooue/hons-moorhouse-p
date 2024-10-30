using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentWriters
{
    public class FastaWriter : IAlignmentWriter
    {
        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            throw new NotImplementedException();
        }

        public List<string> CreateWriteableLines(Alignment alignment)
        {
            throw new NotImplementedException();
        }

        public List<string> CreateSequenceLines(BioSequence sequence)
        {
            throw new NotImplementedException();
        }
    }
}
