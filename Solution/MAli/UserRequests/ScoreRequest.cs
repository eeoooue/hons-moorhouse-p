using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.UserRequests
{
    public class ScoreRequest : UserRequest
    {
        public string InputPath = "";
        public string OutputPath = "";

        public bool SpecifiesCustomConfig = false;
        public string ConfigPath = "";
    }
}
