using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.InverseConverters;

public static class AppearanceConverter
{
    public static string Convert(CompSerializer compSerializer, string space, bool isFirst = false)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        var jsonText = string.Empty;
        var cMargin = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CMargin);
        var cPadding = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CPadding);
        var cBorder = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CBorder);
        var cBorderRadius = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CBorderRadius);
        
        for (var i = 0; i < component.PropertyGroups!.Count; i++)
        {
            if (compSerializer.Properties![i].Name != GroupNames.Appearance.ToString()) continue;
            
            for (var j = 0; j < component.PropertyGroups[i].Properties.Count; j++)
            {
                if (component.PropertyGroups[i].Properties[j].Name !=
                    compSerializer.Properties![i].Properties[j].Name ||
                    component.PropertyGroups[i].Properties[j].Value ==
                    compSerializer.Properties![i].Properties[j].Value) continue;

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;

                if (name == PropertyNames.FillColor.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Background\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.Opacity.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Opacity\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.Margin.ToString() && cMargin == "1")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Margin\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.MarginLeft.ToString() && cMargin == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"MarginLeft\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.MarginRight.ToString() && cMargin == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"MarginRight\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.MarginTop.ToString() && cMargin == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"MarginTop\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.MarginBottom.ToString() && cMargin == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"MarginBottom\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.Padding.ToString() && cPadding == "1")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Padding\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.PaddingLeft.ToString() && cPadding == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"PaddingLeft\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.PaddingRight.ToString() && cPadding == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"PaddingRight\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.PaddingTop.ToString() && cPadding == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"PaddingTop\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.PaddingBottom.ToString() && cPadding == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"PaddingBottom\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderColor.ToString() && 
                         value != ColorValue.Transparent.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderColor\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderWidth.ToString() && cBorder == "1")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderWidth\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderLeftWidth.ToString() && cBorder == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderLeftWidth\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderRightWidth.ToString() && cBorder == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderRightWidth\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderTopWidth.ToString() && cBorder == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderTopWidth\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderBottomWidth.ToString() && cBorder == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderBottomWidth\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderRadius.ToString() && cBorderRadius == "1")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderRadius\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderRadiusTopLeft.ToString() && cBorderRadius == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderRadiusTopLeft\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderRadiusTopRight.ToString() && cBorderRadius == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderRadiusTopRight\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderRadiusBottomLeft.ToString() && cBorderRadius == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderRadiusBottomLeft\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name  == PropertyNames.BorderRadiusBottomRight.ToString() && cBorderRadius == "0")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"BorderRadiusBottomRight\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
            }
            break;
        }

        return jsonText;
    }
}