using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public static class ExternalTransform
{
    public static GroupProperties Convert(this ExternalComponent component)
    {
        var group = new GroupProperties
        {
            Name = GroupNames.Transform.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (component.Property?.Width != null)
        {
            var value = component.Property?.Width;
            value = value == "expand" ? "Expand" : value == "auto" ? "Auto" : value;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Width.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Height != null)
        {
            var value = component.Property?.Height;
            value = value == "expand" ? "Expand" : value == "auto" ? "Auto" : value;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Height.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.X != null)
        {
            var value = component.Property?.X;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.X.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Y != null)
        {
            var value = component.Property?.Y;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Y.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Gap != null)
        {
            var value = component.Property?.Gap;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Gap.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Stretch != null)
        {
            var value = component.Property?.Stretch;
            value = value == "contain" ? "Uniform" : value == "cover" ? "UniformToFill" : "Fill";
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Stretch.ToString(),
                Value = value,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        return group;
    }
}