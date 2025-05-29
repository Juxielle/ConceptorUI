using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.InverseConverters;

public static class ShadowConverter
{
    public static string Convert(CompSerializer compSerializer, string space, bool isFirst = false)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        var jsonText = string.Empty;

        for (var i = 0; i < component.PropertyGroups!.Count; i++)
        {
            if (compSerializer.Properties![i].Name != GroupNames.Shadow.ToString()) continue;
            
            for (var j = 0; j < component.PropertyGroups[i].Properties.Count; j++)
            {
                if (component.PropertyGroups[i].Properties[j].Name !=
                    compSerializer.Properties![i].Properties[j].Name ||
                    component.PropertyGroups[i].Properties[j].Value ==
                    compSerializer.Properties![i].Properties[j].Value) continue;

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;

                if (name == PropertyNames.ShadowDepth.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"ShadowDepth\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.BlurRadius.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"ShadowBlurRadius\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.ShadowDirection.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"ShadowDirection\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.ShadowColor.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"ShadowColor\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
            }
            break;
        }

        return jsonText;
    }
}