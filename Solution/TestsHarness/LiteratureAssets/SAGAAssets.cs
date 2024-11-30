using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;

namespace TestsHarness.LiteratureAssets
{
    public class SAGAAssets
    {
        AlignmentStateConverter Converter = Harness.AlignmentStateConverter;

        public List<BioSequence> GetFigure2Sequences()
        {
            BioSequence seq1 = new BioSequence("1", "MGKVNVDEVGGEAL");
            BioSequence seq2 = new BioSequence("2", "MDKVNEEEVGGKAL");
            BioSequence seq3 = new BioSequence("3", "MGKVGAHAGEYGAEAL");
            BioSequence seq4 = new BioSequence("4", "MGKVGGHAGEYGAEAL");

            return new List<BioSequence>() { seq1, seq2, seq3, seq4 };
        }

        public Alignment GetFigure2ParentAlignment1()
        {
            List<BioSequence> sequences = GetFigure2Sequences();

            List<string> mapping = new List<string>()
            {
                "XXXXX---XXXXXXXXX-",
                "XXXXXXXX---XXXXXX-",
                "XXXXX--XXXXXXXXXXX",
                "XXXXXXXX--XXXXXXXX",
            };

            Alignment alignment = new Alignment(sequences);
            bool[,] state = Converter.ConvertToAlignmentState(mapping);
            alignment.State = state;

            return alignment;
        }

        public Alignment GetFigure2ParentAlignment2()
        {
            List<BioSequence> sequences = GetFigure2Sequences();

            List<string> mapping = new List<string>()
            {
                "--XXXXXXXXXX-XXXX",
                "XX--XXXXXXXX-XXXX",
                "XXXXXX-XXXXXXXXXX",
                "XXXXXXXXXX-XXXXXX",
            };

            Alignment alignment = new Alignment(sequences);
            bool[,] state = Converter.ConvertToAlignmentState(mapping);
            alignment.State = state;

            return alignment;
        }


        public Alignment GetFigure2ChildAlignment1()
        {
            List<BioSequence> sequences = GetFigure2Sequences();

            List<string> mapping = new List<string>()
            {
                "XXXX--XXXXXX-XXXX",
                "XXXX--XXXXXX-XXXX",
                "XXXXXX-XXXXXXXXXX",
                "XXXXXXXXXX-XXXXXX",
            };

            Alignment alignment = new Alignment(sequences);
            bool[,] state = Converter.ConvertToAlignmentState(mapping);
            alignment.State = state;

            return alignment;
        }

        public Alignment GetFigure2ChildAlignment2()
        {
            List<BioSequence> sequences = GetFigure2Sequences();

            List<string> mapping = new List<string>()
            {
                "--XXXXX---XXXXXXXXX-",
                "XX--XXXXXX---XXXXXX-",
                "XXXX--X--XXXXXXXXXXX",
                "XXXX--XXXX--XXXXXXXX",
            };

            Alignment alignment = new Alignment(sequences);
            bool[,] state = Converter.ConvertToAlignmentState(mapping);
            alignment.State = state;

            return alignment;
        }
    }
}
