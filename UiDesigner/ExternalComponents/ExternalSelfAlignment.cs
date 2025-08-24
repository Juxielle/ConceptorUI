using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public static class ExternalSelfAlignment
{
    public static GroupProperties Convert(this ExternalComponent component)
    {
        var group = new GroupProperties
        {
            Name = GroupNames.SelfAlignment.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (component.Property?.HorizontalSelfAlignment != null)
        {
            var value = component.Property?.HorizontalSelfAlignment;
            var alignmentName = value == "left" ? "Hl" : value == "right" ? "Hr" : "Hc";
            group.Properties.Add(new Property
            {
                Name = alignmentName,
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.VerticalSelfAlignment != null)
        {
            var value = component.Property?.VerticalSelfAlignment;
            var alignmentName = value == "top" ? "Vt" : value == "bottom" ? "Vb" : "Vc";
            group.Properties.Add(new Property
            {
                Name = alignmentName,
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        return group;
    }
}