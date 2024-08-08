using System.Collections.Generic;


namespace ConceptorUI.Models
{
    public class CompSerializer
    {
        public string? Name { get; set; }
        public List<GroupProperties>? Properties { get; set; }
        public List<CompSerializer>? Children { get; set; }
    }
}
