using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public class AlignmentInstructions
    {
        public bool Debug = false;
        public bool EmitFrames = false;
        public bool RefineOnly = false;
        public int IterationsLimit = 0;
        public double SecondsLimit = 0.0;

        public string InputPath = "";
        public string OutputPath = "";

        public bool LimitedByIterations()
        {
            return IterationsLimit > 0;
        }

        public bool LimitedBySeconds()
        {
            return SecondsLimit > 0.0;
        }

        public void AddDefaultRestrictions()
        {
            if (LimitedByIterations() || LimitedBySeconds())
            {
                return;
            }
            SecondsLimit = 2.0;
        }
    }
}
