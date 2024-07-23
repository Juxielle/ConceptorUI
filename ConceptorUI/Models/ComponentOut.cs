using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptorUI.Models
{
    class ComponentOut
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public List<GroupProperties> Component { get; set; }

        public void JsonToComponent(string json)
        {

        }

        public string ComponentToJson()
        {
            return null;
        }
    }
}
