using ConceptorUI.Models;
using DesignForge.ExternalComponents;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public static class ExternalShadow
{
    public static GroupProperties Convert(this ExternalComponent component)
    {
        var group = new GroupProperties
        {
            Name = GroupNames.Shadow.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (component.Property?.ShadowDepth != null)
        {
            var value = component.Property?.ShadowDepth;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.ShadowDepth.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.ShadowBlurRadius != null)
        {
            var value = component.Property?.ShadowBlurRadius;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BlurRadius.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.ShadowDirection != null)
        {
            var value = component.Property?.ShadowDirection;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.ShadowDirection.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.ShadowColor != null)
        {
            var value = component.Property?.ShadowColor;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.ShadowColor.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        return group;
    }
}