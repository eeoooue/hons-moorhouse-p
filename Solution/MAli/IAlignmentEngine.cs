using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public interface IAlignmentEngine
    {
        public void PerformAlignment(AlignmentInstructions instructions);
    }
}
