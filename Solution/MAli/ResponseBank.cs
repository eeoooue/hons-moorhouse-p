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
    }
}
