using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.InverseConverters;

public static class TextConverter
{
    public static string Convert(CompSerializer compSerializer, string space, bool isFirst = false)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        var jsonText = string.Empty;

        for (var i = 0; i < component.PropertyGroups!.Count; i++)
        {
            if (compSerializer.Properties![i].Name != GroupNames.Text.ToString()) continue;

            for (var j = 0; j < component.PropertyGroups[i].Properties.Count; j++)
            {
                if (component.PropertyGroups[i].Properties[j].Name !=
                    compSerializer.Properties![i].Properties[j].Name ||
                    component.PropertyGroups[i].Properties[j].Value ==
                    compSerializer.Properties![i].Properties[j].Value) continue;

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;
                
                if (name == PropertyNames.Text.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Text\": ";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.FontFamily.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"FontFamily\": ";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.FontWeight.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"FontWeight\": ";
                    value = value == "1" ? "bold" : "normal";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.FontStyle.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"FontStyle\": ";
                    value = value == "1" ? "italic" : "normal";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.FontSize.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"FontSize\": ";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name is "AlignLeft" or "AlignRight" or "AlignCenter" or "AlignJustify")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"FontStyle\": ";
                    value = value == "AlignLeft" ? "left" :
                        value == "AlignRight" ? "right" :
                        value == "AlignCenter" ? "center" : "justify";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name is "TextUnderline" or "TextOverline" or "TextThrough")
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"FontStyle\": ";
                    value = value == "TextUnderline" ? "underline" :
                        value == "TextOverline" ? "overline" :
                        value == "TextThrough" ? "through" : "underline";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.Color.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Foreground\": ";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.TextWrap.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"TextWrapping\": ";
                    value = value == "1" ? "wrap" : "normal";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.LineSpacing.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"LineSpacing\": ";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.TextTrimming.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"TextTrimming\": ";
                    value = value == "1" ? "ellipsis" : "normal";

                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
            }

            break;
        }

        return jsonText;
    }
}