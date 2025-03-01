using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification
{
    public interface IAlignmentInitializer
    {
        public Alignment CreateInitialAlignment(List<BioSequence> sequences);
    }
}
