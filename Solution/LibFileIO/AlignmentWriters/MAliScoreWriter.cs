﻿using LibBioInfo;
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
        public string FileExtension = "csv";

        public MAliScoreWriter(List<IFitnessFunction> objectives)
        {
            Objectives = objectives;
        }

        public MAliScoreWriter(IFitnessFunction objective) : this([objective]) { }

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            string destination = $"{filename}.{FileExtension}";
            List<string> contents = GetFileContents(alignment);
            File.WriteAllLines($"{filename}.{FileExtension}", contents);
            Console.WriteLine($"Scorefile written to destination: '{destination}'");
        }

        public List<string> GetFileContents(Alignment alignment)
        {
            List<string> result = new List<string>();
            result.Add("Objective,Score");
            foreach (IFitnessFunction objective in Objectives)
            {
                string line = GetScoreLine(alignment, objective);
                result.Add(line);
            }

            return result;
        }

        public string GetScoreLine(Alignment alignment, IFitnessFunction objective)
        {
            double score = objective.GetFitness(alignment.CharacterMatrix);
            return $"{objective.GetName()},{Math.Round(score, 5)}";
        }
    }
}
