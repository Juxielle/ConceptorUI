using UiDesigner.Constants;
using UiDesigner.Models;

namespace ConceptorUI.Models
{
    public class Property
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Value { get; set; } = PropertyType.String.ToString();
        public string SubGroup { get; set; } = PropertyType.None.ToString();
        public int SpaceCount { get; set; } = 1;
        public string Visibility { get; set; } = VisibilityValue.Collapsed.ToString();
        public string ComponentVisibility { get; set; } = VisibilityValue.Visible.ToString();
    }
}
