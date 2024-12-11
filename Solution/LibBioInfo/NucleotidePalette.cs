using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class NucleotidePalette
    {
        public Bioinformatics Bioinformatics = new Bioinformatics();

        public ConsoleColor GetForegroundColour(char x)
        {
            return ConsoleColor.White;
        }

        public ConsoleColor GetBackgroundColour(char x)
        {
            if (Bioinformatics.IsGapChar(x))
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
