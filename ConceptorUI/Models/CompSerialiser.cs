using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptorUI.Models
{
    public class CompSerialiser
    {
        public string? Name { get; set; }
        public List<GroupProperties>? Properties { get; set; }
        public List<CompSerialiser>? Children { get; set; }
    }
}
