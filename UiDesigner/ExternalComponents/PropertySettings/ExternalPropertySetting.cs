using ConceptorUI.ExternalComponents;

namespace DesignForge.ExternalComponents.PropertySettings;

public static class ExternalPropertySetting
{
    public static void Set(this ExternalComponent component, string property, string value)
    {
        if(component.Property == null) return;
        
        if (property == nameof(component.Property.HorizontalAlignment))
        {
            component.Property.HorizontalAlignment = value;
        }
        else if (property == nameof(component.Property.VerticalAlignment))
        {
            component.Property.VerticalAlignment = value;
        }
        else if (property == nameof(component.Property.HorizontalSelfAlignment))
        {
            component.Property.HorizontalSelfAlignment = value;
        }
        else if (property == nameof(component.Property.VerticalSelfAlignment))
        {
            component.Property.VerticalSelfAlignment = value;
        }
        else if (property == nameof(component.Property.Width))
        {
            component.Property.Width = value;
        }
        else if (property == nameof(component.Property.Height))
        {
            component.Property.Height = value;
        }
        else if (property == nameof(component.Property.X))
        {
            component.Property.X = value;
        }
        else if (property == nameof(component.Property.Y))
        {
            component.Property.Y = value;
        }
        else if (property == nameof(component.Property.Stretch))
        {
            component.Property.Stretch = value;
        }
        else if (property == nameof(component.Property.Gap))
        {
            component.Property.Gap = value;
        }
        else if (property == nameof(component.Property.Text))
        {
            component.Property.Text = value;
        }
        else if (property == nameof(component.Property.FontFamily))
        {
            component.Property.FontFamily = value;
        }
        else if (property == nameof(component.Property.FontWeight))
        {
            component.Property.FontWeight = value;
        }
        else if (property == nameof(component.Property.FontStyle))
        {
            component.Property.FontStyle = value;
        }
        else if (property == nameof(component.Property.FontSize))
        {
            component.Property.FontSize = value;
        }
        else if (property == nameof(component.Property.TextAlign))
        {
            component.Property.TextAlign = value;
        }
        else if (property == nameof(component.Property.TextDecoration))
        {
            component.Property.TextDecoration = value;
        }
        else if (property == nameof(component.Property.Foreground))
        {
            component.Property.Foreground = value;
        }
        else if (property == nameof(component.Property.TextWrapping))
        {
            component.Property.TextWrapping = value;
        }
        else if (property == nameof(component.Property.LineSpacing))
        {
            component.Property.LineSpacing = value;
        }
        else if (property == nameof(component.Property.TextTrimming))
        {
            component.Property.TextTrimming = value;
        }
        else if (property == nameof(component.Property.Icon))
        {
            component.Property.Icon = value;
        }
        else if (property == nameof(component.Property.Image))
        {
            component.Property.Image = value;
        }
        else if (property == nameof(component.Property.Background))
        {
            component.Property.Background = value;
        }
        else if (property == nameof(component.Property.Opacity))
        {
            component.Property.Opacity = value;
        }
        else if (property == nameof(component.Property.Margin))
        {
            component.Property.Margin = value;
        }
        else if (property == nameof(component.Property.MarginLeft))
        {
            component.Property.MarginLeft = value;
        }
        else if (property == nameof(component.Property.MarginRight))
        {
            component.Property.MarginRight = value;
        }
        else if (property == nameof(component.Property.MarginTop))
        {
            component.Property.MarginTop = value;
        }
        else if (property == nameof(component.Property.MarginBottom))
        {
            component.Property.MarginBottom = value;
        }
        else if (property == nameof(component.Property.MarginHorizontal))
        {
            component.Property.MarginHorizontal = value;
        }
        else if (property == nameof(component.Property.MarginVertical))
        {
            component.Property.MarginVertical = value;
        }
        else if (property == nameof(component.Property.Padding))
        {
            component.Property.Padding = value;
        }
        else if (property == nameof(component.Property.PaddingLeft))
        {
            component.Property.PaddingLeft = value;
        }
        else if (property == nameof(component.Property.PaddingRight))
        {
            component.Property.PaddingRight = value;
        }
        else if (property == nameof(component.Property.PaddingTop))
        {
            component.Property.PaddingTop = value;
        }
        else if (property == nameof(component.Property.PaddingBottom))
        {
            component.Property.PaddingBottom = value;
        }
        else if (property == nameof(component.Property.PaddingHorizontal))
        {
            component.Property.PaddingHorizontal = value;
        }
        else if (property == nameof(component.Property.PaddingVertical))
        {
            component.Property.PaddingVertical = value;
        }
        else if (property == nameof(component.Property.BorderWidth))
        {
            component.Property.BorderWidth = value;
        }
        else if (property == nameof(component.Property.BorderColor))
        {
            component.Property.BorderColor = value;
        }
        else if (property == nameof(component.Property.BorderLeftWidth))
        {
            component.Property.BorderLeftWidth = value;
        }
        else if (property == nameof(component.Property.BorderRightWidth))
        {
            component.Property.BorderRightWidth = value;
        }
        else if (property == nameof(component.Property.BorderTopWidth))
        {
            component.Property.BorderTopWidth = value;
        }
        else if (property == nameof(component.Property.BorderBottomWidth))
        {
            component.Property.BorderBottomWidth = value;
        }
        else if (property == nameof(component.Property.BorderRadius))
        {
            component.Property.BorderRadius = value;
        }
        else if (property == nameof(component.Property.BorderRadiusTopLeft))
        {
            component.Property.BorderRadiusTopLeft = value;
        }
        else if (property == nameof(component.Property.BorderRadiusTopRight))
        {
            component.Property.BorderRadiusTopRight = value;
        }
        else if (property == nameof(component.Property.BorderRadiusBottomLeft))
        {
            component.Property.BorderRadiusBottomLeft = value;
        }
        else if (property == nameof(component.Property.BorderRadiusBottomRight))
        {
            component.Property.BorderRadiusBottomRight = value;
        }
        else if (property == nameof(component.Property.ShadowDepth))
        {
            component.Property.ShadowDepth = value;
        }
        else if (property == nameof(component.Property.ShadowBlurRadius))
        {
            component.Property.ShadowBlurRadius = value;
        }
        else if (property == nameof(component.Property.ShadowDirection))
        {
            component.Property.ShadowDirection = value;
        }
        else if (property == nameof(component.Property.ShadowColor))
        {
            component.Property.ShadowColor = value;
        }
    }
}