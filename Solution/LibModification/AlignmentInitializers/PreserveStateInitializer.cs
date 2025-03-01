using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentInitializers
{
    public class PreserveStateInitializer : IAlignmentInitializer
    {
        public Alignment CreateInitialAlignment(List<BioSequence> sequences)
        {
            return new Alignment(sequences, true);
        }
    }
}
