using System;
using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public static class ExternalAlignment
{
    public static GroupProperties Convert(this ExternalComponent component)
    {
        var group = new GroupProperties
        {
            Name = GroupNames.Alignment.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (component.Property?.HorizontalAlignment != null)
        {
            var value = component.Property?.HorizontalAlignment;
            var alignmentName = value == "left" ? "Hl" : value == "right" ? "Hr" : "Hc";
            
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Hl.ToString(),
                Value = alignmentName == "Hl" ? "1" : "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Hc.ToString(),
                Value = alignmentName == "Hc" ? "1" : "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Hr.ToString(),
                Value = alignmentName == "Hr" ? "1" : "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.VerticalAlignment != null)
        {
            var value = component.Property?.VerticalAlignment;
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