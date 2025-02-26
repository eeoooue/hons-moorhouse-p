using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.UserRequests
{
    public class AlignmentRequest : UserRequest
    {
        public bool Debug;
        public bool EmitFrames;
        public bool RefineOnly;
        public bool IncludeScoreFile;

        public int IterationsLimit;
        public double SecondsLimit;

        public string InputPath;
        public string OutputPath;

        public bool SpecifiesSeed;
        public string Seed;

        public bool SpecifiesCustomConfig;
        public string ConfigPath;

        public bool SpecifiesScoreOnly;

        public AlignmentOutputFormat OutputFormat;


        public AlignmentRequest()
        {
            Debug = false;
            EmitFrames = false;
            RefineOnly = false;
            IncludeScoreFile = false;

            IterationsLimit = 0;
            SecondsLimit = 0.0;

            InputPath = "";
            OutputPath = "";

            SpecifiesSeed = false;
            Seed = "";

            SpecifiesCustomConfig = false;
            ConfigPath = "";

            SpecifiesScoreOnly = false;

            OutputFormat = AlignmentOutputFormat.FASTA;
        }


        public AlignmentRequest(AlignmentRequest other)
        {
            Debug = other.Debug;
            EmitFrames = other.EmitFrames;
            RefineOnly = other.RefineOnly;
            IncludeScoreFile = other.IncludeScoreFile;

            IterationsLimit = other.IterationsLimit;
            SecondsLimit = other.SecondsLimit;

            InputPath = other.InputPath;
            OutputPath = other.OutputPath;

            SpecifiesSeed = other.SpecifiesSeed;
            Seed = other.Seed;

            SpecifiesCustomConfig = other.SpecifiesCustomConfig;
            ConfigPath = other.ConfigPath;

            SpecifiesScoreOnly = other.SpecifiesScoreOnly;

            OutputFormat = other.OutputFormat;
        }

        public AlignmentRequest GetCopy()
        {
            return new AlignmentRequest(this);
        }

        public void SetOutputFormat(string format)
        {
            switch (format)
            {
                case "clustal":
                case "clustalw":
                    OutputFormat = AlignmentOutputFormat.ClustalW;
                    return;
                default:
                    OutputFormat = AlignmentOutputFormat.FASTA;
                    return;
            }
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
