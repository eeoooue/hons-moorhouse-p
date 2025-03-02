using LibBioInfo;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Mechanisms
{
    public static class VerticalCrossover
    {
        private static CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();

        public static List<Alignment> ProduceChildrenFromMapping(Alignment a, Alignment b, bool[] mapping)
        {
            Alignment child1 = ProduceChildUsingMapping(a, b, mapping);
            Alignment child2 = ProduceChildUsingMapping(b, a, mapping);

            CharMatrixHelper.RemoveEmptyColumns(child1);
            CharMatrixHelper.RemoveEmptyColumns(child2);

            return new List<Alignment> { child1, child2 };
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

            return CharMatrixHelper.RemoveEmptyColumns(result);
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
