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
        public bool IncludeScoreFile = false;

        public int IterationsLimit = 0;
        public double SecondsLimit = 0.0;

        public string InputPath = "";
        public string OutputPath = "";


        public AlignmentInstructions GetCopy()
        {
            AlignmentInstructions result = new AlignmentInstructions();

            result.Debug = Debug;
            result.EmitFrames = EmitFrames;
            result.RefineOnly = RefineOnly;
            result.IncludeScoreFile = IncludeScoreFile;

            result.IterationsLimit = IterationsLimit;
            result.SecondsLimit = SecondsLimit;

            result.InputPath = InputPath;
            result.OutputPath = OutputPath;

            return result;
        }

        public string GetContextString()
        {
            string context = $"Performing Multiple Sequence Alignment";
            if (RefineOnly)
            {
                context += " (iterative refinement)";
            }
            if (LimitedByIterations())
            {
                context += $" [ limit: {IterationsLimit} iterations ]";
            }
            else if (LimitedBySeconds())
            {
                context += $" [ limit: {SecondsLimit.ToString("#.00")} seconds ]";
            }

            return context;
        }

        public bool LimitedByIterations()
        {
            return IterationsLimit > 0;
        }

        public bool LimitedBySeconds()
        {
            return SecondsLimit > 0.0;
        }

        public void CheckAddDefaultRestrictions()
        {
            if (LimitedByIterations() || LimitedBySeconds())
            {
                return;
            }
            SecondsLimit = 5.0;
        }
    }
}
