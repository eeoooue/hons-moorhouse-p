using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Helpers
{
    public class AlignmentDebugHelper
    {
        public Bioinformatics Bioinformatics = new Bioinformatics();

        public ResiduePalette Palette = new ResiduePalette();


        public void PaintSequence(BioSequence sequence)
        {
            Console.ResetColor();
            foreach (char x in sequence.Payload)
            {
                PaintResidue(x);
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public void SetThemeToMatchResidue(char residue)
        {
            Console.BackgroundColor = Palette.GetBackgroundColour(residue);
            Console.ForegroundColor = Palette.GetForegroundColour(residue);
        }

        public void PaintResidue(char residue)
        {
            SetThemeToMatchResidue(residue);
            Console.Write(residue);
        }
    }
}
