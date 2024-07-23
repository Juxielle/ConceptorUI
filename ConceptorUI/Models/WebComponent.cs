using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ConceptorUI.Models
{
    internal class WebComponent
    {
        public string? Name { get; set; }
        public string? Body { get; set; }
        public Dictionary<string, Dictionary<string, string>>? Styles { get; set; }

        public string ToString()
        {
            Name = "TestPage";
            return GetBody();
        }

        private string GetBody()
        {
            string head = "<!Doctype html>\n" +
                "<html>\n" +
                "    <head>\n" +
                //"      <meta />\n" +
                "        <title>report</title>\n" + GetStyles() + "\n    </head>\n";
            string body = $"    <body>{Body}\n    </body>\n</html>";
            return head + body;
        }

        private string GetStyles()
        {
            string styles = "        <style>";
            styles += "\n            body {" +
                "\n                background-color: gray;" +
                "\n                margin: 0px;" +
                "\n                padding: 0px;" +
                "\n            }";
            foreach (var key in Styles!.Keys)
            {
                styles += "\n            ." + key + " {";
                foreach (var styleKey in Styles![key].Keys)
                {
                    if (Styles[key][styleKey] != "invalide" && Styles[key][styleKey] != "invalidepx")
                        styles += "\n                " + styleKey + ": " + Styles[key][styleKey] + ";";
                }
                styles += "\n            }";
            }
            styles += "\n        </style>";
            return styles;
        }

        public bool EqualStyles(Dictionary<string, string> style1, Dictionary<string, string> style2)
        {
            bool equal = true;
            if (style1.Keys.Count == style2.Keys.Count)
            {
                foreach (var key in style1.Keys)
                    if(!style2.ContainsKey(key) || style2[key] != style1[key]) { equal = false; break; }
            } else equal = false;
            return equal;
        }
    }
}
