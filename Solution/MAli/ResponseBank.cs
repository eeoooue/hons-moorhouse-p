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

        public void ProvideInfo()
        {
            Specification.ListCurrentVersion();
            Console.WriteLine($"For more information, see 'readme.txt' or try the '-help' command.");
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
            }
        }
    }
}
