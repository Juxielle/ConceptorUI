using ConceptorUI.ExternalComponents;

namespace DesignForge.ExternalComponents.PropertySettings;

public static class CheckProperty
{
    public static bool IsProperty(this ExternalComponent component, string property)
    {
        if(component.Property == null) return false;

        return property == nameof(component.Property.HorizontalAlignment) ||
               property == nameof(component.Property.VerticalAlignment) ||
               property == nameof(component.Property.HorizontalSelfAlignment) ||
               property == nameof(component.Property.VerticalSelfAlignment) ||
               property == nameof(component.Property.Width) ||
               property == nameof(component.Property.Height) ||
               property == nameof(component.Property.X) ||
               property == nameof(component.Property.Y) ||
               property == nameof(component.Property.Stretch) ||
               property == nameof(component.Property.Gap) ||
               property == nameof(component.Property.Text) ||
               property == nameof(component.Property.FontFamily) ||
               property == nameof(component.Property.FontWeight) ||
               property == nameof(component.Property.FontStyle) ||
               property == nameof(component.Property.FontSize) ||
               property == nameof(component.Property.TextAlign) ||
               property == nameof(component.Property.TextDecoration) ||
               property == nameof(component.Property.Foreground) ||
               property == nameof(component.Property.TextWrapping) ||
               property == nameof(component.Property.LineSpacing) ||
               property == nameof(component.Property.TextTrimming) ||
               property == nameof(component.Property.Icon) ||
               property == nameof(component.Property.Image) ||
               property == nameof(component.Property.Background) ||
               property == nameof(component.Property.Opacity) ||
               property == nameof(component.Property.Margin) ||
               property == nameof(component.Property.MarginLeft) ||
               property == nameof(component.Property.MarginRight) ||
               property == nameof(component.Property.MarginTop) ||
               property == nameof(component.Property.MarginBottom) ||
               property == nameof(component.Property.MarginHorizontal) ||
               property == nameof(component.Property.MarginVertical) ||
               property == nameof(component.Property.Padding) ||
               property == nameof(component.Property.PaddingLeft) ||
               property == nameof(component.Property.PaddingRight) ||
               property == nameof(component.Property.PaddingTop) ||
               property == nameof(component.Property.PaddingBottom) ||
               property == nameof(component.Property.PaddingHorizontal) ||
               property == nameof(component.Property.PaddingVertical) ||
               property == nameof(component.Property.BorderWidth) ||
               property == nameof(component.Property.BorderColor) ||
               property == nameof(component.Property.BorderLeftWidth) ||
               property == nameof(component.Property.BorderRightWidth) ||
               property == nameof(component.Property.BorderTopWidth) ||
               property == nameof(component.Property.BorderBottomWidth) ||
               property == nameof(component.Property.BorderRadius) ||
               property == nameof(component.Property.BorderRadiusTopLeft) ||
               property == nameof(component.Property.BorderRadiusTopRight) ||
               property == nameof(component.Property.BorderRadiusBottomLeft) ||
               property == nameof(component.Property.BorderRadiusBottomRight) ||
               property == nameof(component.Property.ShadowDepth) ||
               property == nameof(component.Property.ShadowBlurRadius) ||
               property == nameof(component.Property.ShadowDirection) ||
               property == nameof(component.Property.ShadowColor);
    }
}