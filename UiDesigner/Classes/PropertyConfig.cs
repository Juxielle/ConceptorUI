using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiDesigner.Models;

namespace UiDesigner.Classes
{
    internal class PropertyConfig
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public PropertyNames PropertyName { get; set; }
    }
}
