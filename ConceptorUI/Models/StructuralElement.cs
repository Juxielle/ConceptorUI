using System.Collections.Generic;

namespace ConceptorUI.Models
{
    internal class StructuralElement
    {

        public string Node {  get; set; }
        public List<StructuralElement> Children { get; set; }
        public bool Selected { get; set; } = false;
        public bool IsSimpleElement { get; set; } = false;

        public int Count()
        {
            int count = 1;
            if(Children != null)
                foreach (StructuralElement child in Children) {
                    count += child.Count();
                }

            return count;
        }
    }
}
