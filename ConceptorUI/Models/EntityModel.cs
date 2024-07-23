using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptorUI.Models
{
    internal class EntityModel
    {
        public string? Name {  get; set; }
        public string? Code { get; set; }
        public DateTime Date { get; set; }
        public List<FieldModel>? Fields { get; set; }
        public List<List<string>>? Datas { get; set; }
    }
}
