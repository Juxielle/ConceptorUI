using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.InverseConverters;

public static class TransformConverter
{
    public static string Convert(CompSerializer compSerializer, string space, bool isFirst = false)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        var jsonText = string.Empty;

        for (var i = 0; i < component.PropertyGroups!.Count; i++)
        {
            if (compSerializer.Properties![i].Name != GroupNames.Transform.ToString()) continue;
            
            for (var j = 0; j < component.PropertyGroups[i].Properties.Count; j++)
            {
                if (component.PropertyGroups[i].Properties[j].Name !=
                    compSerializer.Properties![i].Properties[j].Name ||
                    component.PropertyGroups[i].Properties[j].Value ==
                    compSerializer.Properties![i].Properties[j].Value) continue;

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;

                if (name == PropertyNames.Width.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Width\": ";
                    
                    value = value == "Expand" ? "expand" :
                            value == "Auto" ? "auto" : value;
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.Height.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Height\": ";
                    
                    value = value == "Expand" ? "expand" :
                            value == "Auto" ? "auto" : value;
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.X.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"X\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.Y.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Y\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.Gap.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Gap\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name == PropertyNames.Stretch.ToString())
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Stretch\": ";
                    
                    value = value == "Uniform" ? "contain" :
                            value == "UniformToFill" ? "cover" : "fill";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
            }
            break;
        }

        return jsonText;
    }
}