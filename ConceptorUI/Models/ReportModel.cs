using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptorUI.Models
{
    public class ReportModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime Date { get; set; }
        public CompSerialiser? Report { get; set; }
    }
}
