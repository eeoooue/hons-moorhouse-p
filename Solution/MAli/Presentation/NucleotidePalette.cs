using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Presentation
{
    internal class NucleotidePalette
    {
        public ConsoleColor GetForegroundColour(char x)
        {
            return ConsoleColor.White;
        }

        public ConsoleColor GetBackgroundColour(char x)
        {
            if (x == Bioinformatics.GapCharacter || x == ' ')
            {
                return ConsoleColor.Black;
            }

            switch (x)
            {
                case 'A':
                    return ConsoleColor.DarkRed;
                case 'C':
                    return ConsoleColor.DarkYellow;
                case 'G':
                    return ConsoleColor.DarkGreen;
                case 'T':
                    return ConsoleColor.DarkCyan;
                default:
                    return ConsoleColor.DarkGray;
            }
        }
    }
}
