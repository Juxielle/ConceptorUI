using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptorUI.Models
{
    internal class FieldModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public EntityType? Type { get; set; }
        public string? TableRef { get; set; }
        public string? FieldRef { get; set; }
        public string? DefaultValue { get; set; }
        public List<string>? EnumValues { get; set; }
        public string? Format { get; set; }
        public string? DateFormat { get; set; }

        public string Validate(string value)
        {
            string formatedValue = "";
            if (Code!.Length == Format!.Length - 2) formatedValue = value;
            else
            {
                bool found = false;
                for (int i = 0; i < Format!.Length; i++)
                {
                    if (Format![i] == '[') found = true;
                    else if (Format![i] == ']')
                    {
                        formatedValue += value;
                        found = false;
                    }
                    if (!found && Format![i] != ']') formatedValue += Format![i];
                }
            }
            return formatedValue;
        }

        public void SetFomat(string value)
        {
            string formatedValue = "";
            bool found = false;
            for (int i = 0; i < Format!.Length; i++)
            {
                if (Format![i] == '[') { found = true; formatedValue += Format![i]; }
                else if (Format![i] == ']')
                {
                    formatedValue += value+Format![i];
                    found = false;
                }
                if (!found && Format![i] != ']') formatedValue += Format![i];
            }
            Format = formatedValue;
        }
    }
}
