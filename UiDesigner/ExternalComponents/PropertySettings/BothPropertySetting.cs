using ConceptorUI.ExternalComponents;

namespace DesignForge.ExternalComponents.PropertySettings;

public static class BothPropertySetting
{
    public static void BothSet(this ExternalComponent component, ExternalComponent holder)
    {
        if (component.Property == null || holder.Property == null) return;

        if (holder.Property.HorizontalAlignment != null)
        {
            component.Property.HorizontalAlignment = holder.Property.HorizontalAlignment;
        }

        if (holder.Property.VerticalAlignment != null)
        {
            component.Property.VerticalAlignment = holder.Property.VerticalAlignment;
        }

        if (holder.Property.HorizontalSelfAlignment != null)
        {
            component.Property.HorizontalSelfAlignment = holder.Property.HorizontalSelfAlignment;
        }

        if (holder.Property.VerticalSelfAlignment != null)
        {
            component.Property.VerticalSelfAlignment = holder.Property.VerticalSelfAlignment;
        }

        if (holder.Property.Width != null)
        {
            component.Property.Width = holder.Property.Width;
        }

        if (holder.Property.Height != null)
        {
            component.Property.Height = holder.Property.Height;
        }

        if (holder.Property.X != null)
        {
            component.Property.X = holder.Property.X;
        }

        if (holder.Property.Y != null)
        {
            component.Property.Y = holder.Property.Y;
        }

        if (holder.Property.Stretch != null)
        {
            component.Property.Stretch = holder.Property.Stretch;
        }

        if (holder.Property.Margin != null)
        {
            component.Property.Margin = holder.Property.Margin;
        }

        if (holder.Property.MarginLeft != null)
        {
            component.Property.MarginLeft = holder.Property.MarginLeft;
        }

        if (holder.Property.MarginRight != null)
        {
            component.Property.MarginRight = holder.Property.MarginRight;
        }

        if (holder.Property.MarginTop != null)
        {
            component.Property.MarginTop = holder.Property.MarginTop;
        }

        if (holder.Property.MarginBottom != null)
        {
            component.Property.MarginBottom = holder.Property.MarginBottom;
        }

        if (holder.Property.MarginHorizontal != null)
        {
            component.Property.MarginHorizontal = holder.Property.MarginHorizontal;
        }

        if (holder.Property.MarginVertical != null)
        {
            component.Property.MarginVertical = holder.Property.MarginVertical;
        }
    }
}