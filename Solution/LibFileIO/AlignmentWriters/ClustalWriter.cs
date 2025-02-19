using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentWriters
{
    public class ClustalWriter : IAlignmentWriter
    {
        public string FileExtension = "aln";

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
