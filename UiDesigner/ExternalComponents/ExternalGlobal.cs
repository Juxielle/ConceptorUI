using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public static class ExternalGlobal
{
    public static GroupProperties Convert(this ExternalComponent component)
    {
        var group = new GroupProperties
        {
            Name = GroupNames.Global.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (component.Property?.Icon != null)
        {
            var value = component.Property?.Icon;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.FilePicker.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Image != null)
        {
            var value = component.Property?.Image;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.FilePicker.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        return group;
    }
}