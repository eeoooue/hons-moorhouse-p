using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    internal class MAliSpecification
    {
        HashSet<string> SupportedCommands = new HashSet<string>();

        public MAliSpecification()
        {
            AddSupportedCommands();
        }

        public void AddSupportedCommands()
        {
            SupportedCommands.Add("input");
            SupportedCommands.Add("output");
            SupportedCommands.Add("help");
            SupportedCommands.Add("info");
        }
    }
}
