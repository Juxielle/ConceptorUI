using System.Collections.Generic;




namespace ConceptorUI.Models
{
    internal class ReactComponent
    {
        public string? Name { get; set; }
        public string? Body { get; set; }
        public Dictionary<string, Dictionary<string, bool>>? Imports { get; set; }
        public Dictionary<string, Dictionary<string, string>>? Styles { get; set; }

        public override string ToString()
        {
            Name = "TestPage";
            return GetImports() +"\n\n"+
                   GetBody() +"\n\n"+
                   GetStyles();
        }

        private string GetImports()
        {
            var imports = "";
            var keyDef = "";
            foreach(var route in Imports!.Keys)
            {
                foreach (var key in Imports[route].Keys)
                {
                    if (!Imports[route][key]) continue;
                    keyDef = key; break;
                }

                imports += "import"+ (keyDef != "" ? " "+keyDef + (Imports[route].Keys.Count > 1 ? ", " : "") : "") + (Imports[route].Keys.Count > 1 || keyDef == "" ? " { " : "");
                foreach (var key in Imports[route].Keys)
                {
                    if (!Imports[route][key]) imports += key.ToString() + ",";
                    else keyDef = key;
                }
                imports += (Imports[route].Keys.Count > 1 || keyDef == "" ? " }" : "") + " from '" + route +"';\n";
                keyDef = "";
            }
            return imports;
        }

        private string GetBody()
        {
            return "export default function "+ Name +"(props) {\n" +
                    "   return(\n"+ Body +"\n   );\n}";
        }

        private string GetStyles()
        {
            string styles = "const styles = StyleSheet.create({";
            foreach (var key in Styles!.Keys)
            {
                styles += "\n   " + key + ": {";
                foreach (var styleKey in Styles![key].Keys)
                {
                    if(Styles[key][styleKey] != "invalide")
                        styles += "\n      " + styleKey + ": " + Styles[key][styleKey] + ",";
                }
                styles += "\n   },";
            }
            styles += "\n});";
            return styles;
        }

        public void AddImports(Dictionary<string, Dictionary<string, bool>> imports)
        {
            var froute = false;
            foreach (var rc in imports!.Keys)
            {
                foreach (var cc in imports[rc].Keys)
                {
                    foreach (var rp in Imports!.Keys)
                    {
                        if (!Imports.ContainsKey(rc))
                        {
                            Imports.Add(rc, imports[rc]);
                            froute = true; break;
                        }else if(rc == rp)
                        {
                            if (!Imports[rp].ContainsKey(cc))
                            {
                                Imports[rp].Add(cc, imports[rc][cc]);
                            }
                            break;
                        }
                    }
                    if (froute) break;
                }
            }
        }
    }
}
