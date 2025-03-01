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

        public static void PickPairOfSequences(Alignment alignment, out int i, out int j)
        {
            PickPairOfSequences(alignment.Height, out i, out j);
        }

        public static void PickPairOfSequences(int height, out int i, out int j) {

            i = Random.Next(height);
            j = i;
            while (i == j)
            {
                j = Random.Next(height);
            }
        }

        public static int PickIntFromList(List<int> options)
        {
            int n = options.Count;
            int i = Randomizer.Random.Next(n);
            return options[i];
        }
    }
}
