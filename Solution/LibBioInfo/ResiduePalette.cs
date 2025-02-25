using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class ResiduePalette
    {
        public ConsoleColor GetForegroundColour(char x)
        {
            return ConsoleColor.White;
        }

        public ConsoleColor GetBackgroundColour(char x)
        {
            if (Bioinformatics.IsGapChar(x) || x == ' ')
            {
                return ConsoleColor.Black;
            }

            if ("C".Contains(x))
            {
                return ConsoleColor.DarkBlue;
            }

            if ("STAGP".Contains(x))
            {
                return ConsoleColor.DarkCyan;
            }

            if ("DEQN".Contains(x))
            {
                return ConsoleColor.DarkGreen;
            }

            if ("HRK".Contains(x))
            {
                return ConsoleColor.DarkYellow;
            }

            if ("MILV".Contains(x))
            {
                return ConsoleColor.DarkMagenta;
            }

            if ("WYF".Contains(x))
            {
                return ConsoleColor.DarkRed;
            }

            return ConsoleColor.DarkGray;
        }
    }
}
