using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public class ResponseBank
    {

        public void NotifyUserError(UserRequestError error)
        {
            switch (error)
            {
                case UserRequestError.ContainsForeignCommands:
                    Console.WriteLine("Error: Contains foreign commands.");
                    break;
                case UserRequestError.RequestIsAmbiguous:
                    Console.WriteLine("Error: Request is ambiguous.");
                    break;
            }
        }
    }
}
