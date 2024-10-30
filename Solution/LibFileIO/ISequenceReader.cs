using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO
{
    interface ISequenceReader
    {
        public List<BioSequence> ReadSequencesFrom(string filename);
    }
}
