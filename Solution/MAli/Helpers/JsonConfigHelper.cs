using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAli.Helpers
{
    public class JsonConfigHelper
    {
        public JsonElement ReadConfigFrom(string filename)
        {
            string text = File.ReadAllText(filename);
            JsonDocument document = JsonDocument.Parse(text);

            return document.RootElement;
        }
    }
}
