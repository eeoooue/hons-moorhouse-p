using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO
{
    public interface IAlignmentReader
    {
        public List<BioSequence> ReadSequencesFrom(string filename);
    }
}
