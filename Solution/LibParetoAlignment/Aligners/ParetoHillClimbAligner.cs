using LibParetoAlignment.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Aligners
{
    internal class ParetoHillClimbAligner
    {

        public int ArchiveGoalSize = 10;

        Queue<TradeoffAlignment> Archive = new Queue<TradeoffAlignment>();
        ParetoHelper ParetoHelper = new ParetoHelper();

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
