using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class ResiduePalette
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

            return ConsoleColor.Red;
        }
    }
}
