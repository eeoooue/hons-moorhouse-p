using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    public interface IObjectiveFunction
    {
        public double ScoreAlignment(Alignment alignment);

        public string GetName();
    }
}
