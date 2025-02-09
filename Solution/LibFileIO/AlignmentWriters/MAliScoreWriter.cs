using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentWriters
{
    public class MAliScoreWriter : IAlignmentWriter
    {
        public List<IFitnessFunction> Objectives;
        public string FileExtension = "maliscore";

        public MAliScoreWriter(List<IFitnessFunction> objectives)
        {
            Objectives = objectives;
        }

        public MAliScoreWriter(IFitnessFunction objective) : this([objective]) { }

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            List<string> contents = GetFileContents(alignment);
            File.WriteAllLines($"{filename}.{FileExtension}", contents);
        }

        public List<string> GetFileContents(Alignment alignment)
        {
            List<string> result = new List<string>();
            foreach (IFitnessFunction objective in Objectives)
            {
                string line = GetScoreLine(alignment, objective);
            }

            return result;
        }

        public string GetScoreLine(Alignment alignment, IFitnessFunction objective)
        {
            string nameSection = $"[Objective: <{objective.GetName()}>]";
            double score = objective.GetFitness(alignment.CharacterMatrix);
            string scoreSection = $"[Score: <{Math.Round(score, 5)}>]";

            return $"{nameSection} {scoreSection}";
        }
    }
}
