using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO
{
    public interface IAlignmentWriter
    {
        public void WriteAlignmentTo(Alignment alignment, string filename);
    }
}
