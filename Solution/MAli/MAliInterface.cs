using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    internal class MAliInterface
    {
        private MAliFacade Facade = new MAliFacade();

        public void ProcessArguments(string[] args)
        {

            if (IsAlignmentRequest(args))
            {
                Facade.PerformAlignment(args);
            }

            if (IsHelpRequest(args))
            {
                Facade.ProvideHelp(args);
            }

            if (IsInfoRequest(args))
            {
                Facade.ProvideInfo(args);
            }
        }

        public bool ContainsForeignCommands(string[] args)
        {
            return false;
        }

        public bool IsAlignmentRequest(string[] args)
        {
            return false;
        }

        public bool IsInfoRequest(string[] args)
        {
            return false;
        }

        public bool IsHelpRequest(string[] args)
        {
            return false;
        }
    }
}
