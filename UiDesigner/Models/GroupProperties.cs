using System.Collections.Generic;
using System.Linq;
using ConceptorUI.Models;


namespace UiDesigner.Models
{
    public class GroupProperties
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
        public string Visibility { get; set; }
        
        public int GetPosition(PropertyNames propertyName)
        {
            var position = -1;
            foreach (var prop in Properties.Where(prop => prop.Name == propertyName.ToString()))
            {
                position = Properties.IndexOf(prop);
                break;
            }
            return position;
        }
        
        public string GetValue(PropertyNames propertyName)
        {
            var name = string.Empty;
            foreach (var prop in Properties.Where(prop => prop.Name == propertyName.ToString()))
            {
                name = prop.Value;
                break;
            }
            return name;
        }
    }
}
