using LibBioInfo;
using LibParetoAlignment.Helpers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Aligners
{
    internal class ParetoHillClimbAligner : ParetoIterativeAligner
    {
        public int ArchiveGoalSize = 10;

        Queue<TradeoffAlignment> Archive = new Queue<TradeoffAlignment>();
        ParetoHelper ParetoHelper = new ParetoHelper();

        public ParetoHillClimbAligner(List<IFitnessFunction> objectives) : base(objectives)
        {

        }

        public override List<Alignment> CollectTradeoffSolutions()
        {
            List<Alignment> result = new List<Alignment>();
            List<TradeoffAlignment> population = Archive.ToList();
            foreach(TradeoffAlignment tradeoff in population)
            {
                result.Add(tradeoff.Alignment);
            }

            return result;
        }

        public bool ShouldAddSolutionToArchive(TradeoffAlignment alignment)
        {
            if (Archive.Count < ArchiveGoalSize)
            {
                return true;
            }

            List<TradeoffAlignment> population = Archive.ToList();

            return ParetoHelper.SolutionIsNonDominated(alignment, population);
        }
    }
}
