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

        public ResiduePalette ResiduePalette = new ResiduePalette();
        public NucleotidePalette NucleotidePalette = new NucleotidePalette();



        public void PaintAlignment(Alignment alignment)
        {
            foreach(BioSequence sequence in alignment.GetAlignedSequences())
            {
                PaintSequence(sequence);
            }
        }


        public void PaintSequence(BioSequence sequence)
        {
            Console.ResetColor();

            if (sequence.IsNucleic())
            {
                foreach (char x in sequence.Payload)
                {
                    PaintNucleotide(x);
                }
            }
            else
            {
                foreach (char x in sequence.Payload)
                {
                    PaintResidue(x);
                }
            }

            Console.ResetColor();
            Console.WriteLine();
        }



        public void SetThemeToMatchResidue(char residue)
        {
            Console.BackgroundColor = ResiduePalette.GetBackgroundColour(residue);
            Console.ForegroundColor = ResiduePalette.GetForegroundColour(residue);
        }

        public void SetThemeToMatchNucleotide(char residue)
        {
            Console.BackgroundColor = NucleotidePalette.GetBackgroundColour(residue);
            Console.ForegroundColor = NucleotidePalette.GetForegroundColour(residue);
        }

        public void PaintNucleotide(char unit)
        {
            SetThemeToMatchNucleotide(unit);
            Console.Write(unit);
        }

        public void PaintResidue(char residue)
        {
            SetThemeToMatchResidue(residue);
            Console.Write(residue);
        }
    }
}
