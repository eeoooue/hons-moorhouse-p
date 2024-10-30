using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO
{
    internal class SequenceReader
    {
        public string Directory = "";

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            throw new NotImplementedException();
        }

        public List<int> CollectIdentifierLocations(List<string> contents)
        {
           throw new NotImplementedException();
        }

        public BioSequence ParseAsSequence(List<string> contents)
        {
            throw new NotImplementedException();
        }
    }
}
