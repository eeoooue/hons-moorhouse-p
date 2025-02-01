using LibBioInfo;
using LibFileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class FrameHelper
    {
        FileHelper FileHelper = new FileHelper();

        public void SaveCurrentFrame(Alignment alignment, int iterations)
        {
            string suffix = Frontload(iterations);
            FileHelper.WriteAlignmentTo(alignment, $"frames\\frame_{suffix}");
        }

        public string Frontload(int number)
        {
            string s = number.ToString();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5 - s.Length; i++)
            {
                sb.Append('0');
            }
            sb.Append(s);

            return sb.ToString();
        }

        public void CheckCreateFramesFolder()
        {
            Directory.CreateDirectory("frames");
        }
    }
}
