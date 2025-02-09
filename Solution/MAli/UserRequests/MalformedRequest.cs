using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.UserRequests
{
    public class MalformedRequest : UserRequest
    {
        public string Info;

        public MalformedRequest(string info = "Error: Failed to interpret request.")
        {
            Info = info;
        }
    }
}
