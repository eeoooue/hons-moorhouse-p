using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class AlignmentDebugHelper
    {
        public ResiduePalette ResiduePalette = new ResiduePalette();
        public NucleotidePalette NucleotidePalette = new NucleotidePalette();

        public int WidthLimit = 100;
        public string GapFiller = "";
        public int InfoWidthLimit = 110;

        public AlignmentDebugHelper()
        {
            GapFiller = BuildGapFiller(WidthLimit + 3);
        }

        public List<string> PadInfoLines(List<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string line in lines)
            {
                string padded = PadInfoLine(line);
                result.Add(padded);
            }

            return result;
        }

        public string PadInfoLine(string line)
        {
            if (line.Length > InfoWidthLimit)
            {
                return line.Substring(0, InfoWidthLimit - 3) + "...";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(line);
            while (sb.Length < InfoWidthLimit)
            {
                sb.Append(' ');
            }
            return sb.ToString();
        }

        public string BuildGapFiller(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(' ');
            }

            return sb.ToString();
        }

        public void PaintAlignment(Alignment alignment)
        {
            foreach (BioSequence sequence in alignment.GetAlignedSequences())
            {
                PaintSequence(sequence);
            }
        }

        public void PaintSequence(BioSequence sequence)
        {
            Console.ResetColor();

            Console.Write("   ");

            bool cropNeeded = sequence.Payload.Length > WidthLimit;

            string payload = sequence.Payload;
            if (cropNeeded)
            {
                payload = payload.Substring(0, WidthLimit) + "...";
            }
            else
            {
                int gapSize = GapFiller.Length - payload.Length;
                payload += GapFiller.Substring(0, gapSize);
            }

            if (sequence.IsNucleic())
            {
                foreach (char x in payload)
                {
                    PaintNucleotide(x);
                }
            }
            else
            {
                foreach (char x in payload)
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
