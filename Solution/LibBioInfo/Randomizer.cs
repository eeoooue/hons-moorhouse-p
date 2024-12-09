using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public static class Randomizer
    {
        public static Random Random = new Random();

        public static void SetSeed(int value)
        {
            Random = new Random(value);
        }

        public static bool CoinFlip()
        {
            int result = Random.Next(0, 2);
            return result == 1;
        }

        public static bool PercentageChanceEvent(double chanceOfEvent)
        {
            double result = Random.NextDouble();
            return result <= chanceOfEvent;
        }
    }
}
