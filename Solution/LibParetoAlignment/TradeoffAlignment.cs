using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment
{
    public class TradeoffAlignment
    {
        public Alignment Alignment;
        public Dictionary<string, double> Scores = new Dictionary<string, double>();

        public double CrowdingDistance = 0.0;

        public int FrontRank = 0;
        public int DominationCounter = 0;
        public List<TradeoffAlignment> DominatedSolutions = new List<TradeoffAlignment>();
        public bool AddedToFront = false;

        public TradeoffAlignment(Alignment alignment)
        {
            Alignment = alignment;
        }

        public void ResetDominationVariables()
        {
            FrontRank = 0;
            DominationCounter = 0;
            DominatedSolutions.Clear();
            AddedToFront = false;
        }

        public void SetFrontRank(int rank)
        {
            FrontRank = rank;
        }

        public void SetScore(string key, double value)
        {
            Scores[key] = value;
        }

        public double GetScore(string key)
        {
            return Scores[key];
        }

        public TradeoffAlignment GetCopy()
        {
            Alignment alignment = Alignment.GetCopy();
            TradeoffAlignment result = new TradeoffAlignment(alignment);

            foreach (string key in Scores.Keys)
            {
                result.SetScore(key, Scores[key]);
            }

            return result;
        }
    }
}
