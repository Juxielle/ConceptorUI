using System.Linq;
using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;

namespace ConceptorUi.ViewModels.Operations;

static class ManageProperties
{
    public static GroupProperties GetGroupProperties(this Component component, GroupNames groupName)
    {
        foreach (var propertyGroup in component.PropertyGroups!.Where(propertyGroup =>
                     propertyGroup.Name == groupName.ToString()))
            return propertyGroup;
        return new GroupProperties();
    }

    public static void SetPropertyValue(this Component component, GroupNames groupName, PropertyNames propertyName, string value)
    {
        var i = -1;
        foreach (var group in component.PropertyGroups!)
        {
            i++;
            var j = -1;
            if (group.Name != groupName.ToString()) continue;
            foreach (var property in group.Properties)
            {
                j++;
                if (property.Name != propertyName.ToString()) continue;
                component.PropertyGroups[i].Properties[j].Value = value;
                return;
            }
        }
    }

    public static void SetGroupVisibility(this Component component, GroupNames groupName, bool isVisible = true)
    {
        var i = -1;
        foreach (var group in component.PropertyGroups!)
        {
            i++;
            if (group.Name != groupName.ToString()) continue;
            component.PropertyGroups[i].Visibility =
                isVisible ? VisibilityValue.Visible.ToString() : VisibilityValue.Collapsed.ToString();
            foreach (var property in group.Properties)
            {
                var j = group.Properties.IndexOf(property);
                component.PropertyGroups[i].Properties[j].Visibility = isVisible
                    ? VisibilityValue.Visible.ToString()
                    : VisibilityValue.Collapsed.ToString();
            }

            return;
        }
    }

    public static void SetGroupOnlyVisibility(this Component component, GroupNames groupName, bool isVisible = true)
    {
        var i = -1;
        foreach (var group in component.PropertyGroups!)
        {
            i++;
            if (group.Name != groupName.ToString()) continue;
            component.PropertyGroups[i].Visibility =
                isVisible ? VisibilityValue.Visible.ToString() : VisibilityValue.Collapsed.ToString();
            return;
        }
    }

    public static void SetPropertiesOnlyVisibility(this Component component, GroupNames groupName, bool isVisible = true)
    {
        var i = -1;
        foreach (var group in component.PropertyGroups!)
        {
            i++;
            if (group.Name != groupName.ToString()) continue;
            foreach (var property in group.Properties)
            {
                var j = group.Properties.IndexOf(property);
                component.PropertyGroups[i].Properties[j].Visibility = isVisible
                    ? VisibilityValue.Visible.ToString()
                    : VisibilityValue.Collapsed.ToString();
            }

            return;
        }
    }

    public static void SetPropertyVisibility(this Component component, GroupNames groupName, PropertyNames propertyName, bool isVisible = true)
    {
        var i = -1;
        foreach (var group in component.PropertyGroups!)
        {
            i++;
            if (group.Name != groupName.ToString()) continue;
            var j = -1;
            foreach (var property in group.Properties)
            {
                j++;
                if (property.Name != propertyName.ToString()) continue;
                component.PropertyGroups[i].Properties[j].Visibility = isVisible
                    ? VisibilityValue.Visible.ToString()
                    : VisibilityValue.Collapsed.ToString();
                return;
            }
        }
    }
}