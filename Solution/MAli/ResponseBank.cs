using MAli.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public class ResponseBank
    {
        private MAliSpecification Specification = new MAliSpecification();

        public void ProvideHelp()
        {
            Specification.ListCurrentVersion();
            Specification.ListSupportedCommands();
        }

        public void ExplainException(Exception exception)
        {
            switch (exception)
            {
                case FileNotFoundException:
                    Console.WriteLine("Error: File not found.");
                    break;
                case FileLoadException:
                    Console.WriteLine("Error: File could not be loaded.");
                    break;
                default:
                    Console.WriteLine("Error: Runtime error.");
                    break;
            }
        }

        public void NotifyUserError(MalformedRequest request)
        {
            Console.WriteLine(request.Info);
        }

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
                case UserRequestError.NoArgumentsGiven:
                    Console.WriteLine("Error: No arguments given. Try '-help' to view a list of commands.");
                    break;
                case UserRequestError.RequestIsInvalid:
                    Console.WriteLine("Error: Request is invalid. Try '-help' to view a list of commands.");
                    break;
                case UserRequestError.MultipleLimitations:
                    Console.WriteLine("Error: MAli doesn't support limiting both 'iterations' & 'seconds' at once.");
                    break;
                default:
                    Console.WriteLine("Error: Alignment couldn't be performed.");
                    break;
            }
        }
    }
}
