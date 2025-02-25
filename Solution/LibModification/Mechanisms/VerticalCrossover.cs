using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public static class VerticalCrossover
    {
        public static void ProduceChildrenFromMapping(Alignment a, Alignment b, bool[] mapping, out Alignment child1, out Alignment child2)
        {
            child1 = ProduceChildUsingMapping(a, b, mapping);
            child2 = ProduceChildUsingMapping(b, a, mapping);
        }

        public static Alignment ProduceChildUsingMapping(Alignment a, Alignment b, bool[] mapping)
        {
            Alignment child = a.GetCopy();
            child.CharacterMatrix = GetCrossoverMatrix(a, b, mapping);

            return child;
        }

        public static char[,] GetCrossoverMatrix(Alignment a, Alignment b, bool[] mapping)
        {
            int m = a.Height;
            int n = Math.Max(a.Width, b.Width);

            char[,] result = new char[m, n];

            for(int i=0; i<m; i++)
            {
                Alignment current = mapping[i] ? a : b;
                PastePayloadOntoMatrix(current.CharacterMatrix, result, i);
            }

            return result;
        }

        public static void PastePayloadOntoMatrix(in char[,] source, char[,] destination, int i)
        {
            int n1 = source.GetLength(1);
            for (int j1=0; j1<n1; j1++)
            {
                destination[i, j1] = source[i, j1];
            }

            int n2 = destination.GetLength(1);
            for (int j2=n1; j2<n2; j2++)
            {
                destination[i, j2] = '-';
            }
        }

    }
}
