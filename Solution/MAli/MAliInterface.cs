using MAli.Helpers;
using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public enum UserRequestError
    {
        ContainsForeignCommands,
        RequestIsAmbiguous,
        NoArgumentsGiven,
        RequestIsInvalid,
        MultipleLimitations,
    }

    public class MAliInterface
    {
        private MAliFacade Facade = new MAliFacade();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();
        private ResponseBank ResponseBank = new ResponseBank();

        public void ProcessArguments(string[] args)
        {
            UserRequest request = ArgumentHelper.InterpretRequest(args);
            ProcessRequest(request);
        }

        public void ProcessRequest(UserRequest request)
        {
            if (request is AlignmentRequest ali)
            {
                Facade.PerformAlignment(ali);
            }
            else if (request is MalformedRequest mal)
            {
                ResponseBank.NotifyUserError(mal);
            }
            else if (request is HelpRequest)
            {
                ResponseBank.ProvideHelp();
            }
        }
    }
}
