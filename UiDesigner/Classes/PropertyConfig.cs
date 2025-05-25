using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConceptorUI.Models;
using UiDesigner.Models;

namespace UiDesigner.Classes
{
    public class PropertyConfig
    {
        public string Name { get; set; }
        public bool IsEditable { get; set; }
        public bool IsFocusable { get; set; }
        public PropertyNames PropertyName { get; set; }
    }
}
