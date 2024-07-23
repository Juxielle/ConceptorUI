using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptorUI.Models
{
    public class GroupProperties
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
        public string Visibility { get; set; }
    }
}
