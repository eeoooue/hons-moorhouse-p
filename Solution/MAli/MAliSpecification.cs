using LibBioInfo.ScoringMatrices;
using LibBioInfo;
using LibScoring.FitnessFunctions;
using LibScoring;
using MAli.AlignmentConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public class MAliSpecification
    {
        public HashSet<string> SupportedCommands = new HashSet<string>();
        public Dictionary<string, string> CommandDescriptions = new Dictionary<string, string>();
        public string Version = "v1.3";

        public MAliSpecification()
        {
            AddSupportedCommands();
            AddCommandDescriptions();
        }

        public void AddSupportedCommands()
        {
            SupportedCommands.Add("input");
            SupportedCommands.Add("output");
            SupportedCommands.Add("seed");
            SupportedCommands.Add("tag");
            SupportedCommands.Add("format");

            SupportedCommands.Add("iterations");
            SupportedCommands.Add("seconds");

            SupportedCommands.Add("refine");
            SupportedCommands.Add("batch");

            SupportedCommands.Add("timestamp");
            SupportedCommands.Add("debug");
            SupportedCommands.Add("frames");
            SupportedCommands.Add("help");

            SupportedCommands.Add("pareto");
            SupportedCommands.Add("scorefile");
            SupportedCommands.Add("scoreonly");

            SupportedCommands.Add("config");
        }

        public void AddCommandDescriptions()
        {
            CommandDescriptions.Add("input", "Specify the input file of sequences to be aligned.");
            CommandDescriptions.Add("output", "Specify the output file path for the alignment of sequences.");
            CommandDescriptions.Add("seed", "Specify a seed value for reproducible results.");
            CommandDescriptions.Add("tag", "Specify a suffix to be included in the output filename.");
            CommandDescriptions.Add("format", "Specify the file format to use for the output alignment. ('fasta' or 'clustal')");

            CommandDescriptions.Add("iterations", "Specify the number of iterations of alignment to be performed.");
            CommandDescriptions.Add("seconds", "Specify the number of seconds of alignment to be performed.");
            CommandDescriptions.Add("refine", "(flag) Refine the alignment provided rather than starting from a randomized state.");
            CommandDescriptions.Add("batch", "(flag) Align a series of inputs. Specify the directories as 'input' and 'output' arguments.");

            CommandDescriptions.Add("timestamp", "(flag) Includes a completion date-time timestamp in the output filename.");
            CommandDescriptions.Add("debug", "(flag) View debugging information throughout the alignment process.");
            CommandDescriptions.Add("frames", "(flag) Saves the alignment state to a 'frames' subfolder after each iteration.");
            CommandDescriptions.Add("help", "Display the list of supported commands.");

            CommandDescriptions.Add("pareto", "Output a selection of (n) tradeoff alignments, approximating the Pareto front");
            CommandDescriptions.Add("scorefile", "(flag) Output a separate .maliscore file containing the alignment's objective scores");
            CommandDescriptions.Add("scoreonly", "(flag) Output only a .maliscore file containing the alignment's objective scores");

            CommandDescriptions.Add("config", "Specify a custom .json config defining the objective to guide the alignment process.");
        }

        public static List<IFitnessFunction> GetSupportedObjectives()
        {
            IScoringMatrix blosum62 = new BLOSUM62Matrix();
            IFitnessFunction sopBlosum = new SumOfPairsFitnessFunction(blosum62);
            IFitnessFunction sopBlosumGapPen = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(blosum62);

            IScoringMatrix pam250 = new PAM250Matrix();
            IFitnessFunction sopPam = new SumOfPairsFitnessFunction(pam250);
            IFitnessFunction sopPamGapPen = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(pam250);

            IFitnessFunction affineGapPenalty = new AffineGapPenaltyFitnessFunction();
            IFitnessFunction nonGapPenalty = new NonGapsFitnessFunction();
            IFitnessFunction totallyConservedColumns = new TotallyConservedColumnsFitnessFunction();

            List<IFitnessFunction> result = new List<IFitnessFunction>()
            {
                sopBlosum,
                sopBlosumGapPen,
                sopPam,
                sopPamGapPen,
                affineGapPenalty,
                nonGapPenalty,
                totallyConservedColumns,
            };

            return result;
        }
    }
}
