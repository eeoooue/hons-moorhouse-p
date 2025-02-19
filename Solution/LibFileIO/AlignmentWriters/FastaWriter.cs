using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentWriters
{
    public class FastaWriter : IAlignmentWriter
    {
        public string FileExtension = "faa";

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            string destination = $"{filename}.{FileExtension}";
            List<string> lines = CreateAlignmentLines(alignment);
            File.WriteAllLines(destination, lines);
            Console.WriteLine($"Alignment written to destination: '{destination}'");
        }

        public List<string> CreateAlignmentLines(Alignment alignment)
        {
            List<string> result = new List<string>();

            List<BioSequence> aligned = alignment.GetAlignedSequences();
            foreach(BioSequence sequence in aligned)
            {
                List<string> lines = CreateSequenceLines(sequence);
                result.AddRange(lines);
            }

            return result;
        }

        public List<string> CreateSequenceLines(BioSequence sequence)
        {
            List<string> result = new List<string>();

            result.Add($">{sequence.Identifier}");
            result.Add(sequence.Payload);

            return result;
        }
    }
}
