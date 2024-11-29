using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public interface ICrossoverOperator
    {
        public Alignment CreateChildAlignment(Alignment a, Alignment b);

    }
}
