using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class BioSequence
    {
        public string Identifier { get; private set; }
        public string Payload { get; private set; }

        public BioSequence(string identifier, string payload)
        {
            Identifier = identifier;
            Payload = payload;
        }
    }
}
