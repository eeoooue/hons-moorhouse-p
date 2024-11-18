using LibAlignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public abstract class AlignmentConfig
    {
        public abstract Aligner CreateAligner();
    }
}
