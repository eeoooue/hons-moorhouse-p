﻿using LibBioInfo;

namespace LibFileIO
{
    public class FileHelper
    {
        private SequenceReader Reader = new SequenceReader();
        private AlignmentWriter Writer = new AlignmentWriter();

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            throw new NotImplementedException();
        }

        public void WriteAlignmentTo(Alignment alignment, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
