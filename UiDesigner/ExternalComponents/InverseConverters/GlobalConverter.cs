using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.InverseConverters;

public static class GlobalConverter
{
    public static string Convert(CompSerializer compSerializer, string space, bool isFirst = false)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        var jsonText = string.Empty;

        for (var i = 0; i < component.PropertyGroups!.Count; i++)
        {
            if (compSerializer.Properties![i].Name != GroupNames.Global.ToString()) continue;
            
            for (var j = 0; j < component.PropertyGroups[i].Properties.Count; j++)
            {
                if (component.PropertyGroups[i].Properties[j].Name !=
                    compSerializer.Properties![i].Properties[j].Name ||
                    component.PropertyGroups[i].Properties[j].Value ==
                    compSerializer.Properties![i].Properties[j].Value) continue;

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;

                if (name == PropertyNames.FilePicker.ToString() && component.Name == ComponentList.Icon)
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Icon\": ";
                    
                    if (value.Length > 0 && value[0] == '[')
                    {
                        var values = System.Text.Json.JsonSerializer.Deserialize<string[]>(value);
                        if (values == null || values.Length < 2) value = "Apple";
                        else value = $"\"[\\\"{values![0]}\\\", \\\"{values![1]}\\\"]\"";
                    }
                    else value = $"\"{value}\"";
                    
                    jsonText += value;
                    isFirst = false;
                }
                else if (name == PropertyNames.FilePicker.ToString() && component.Name == ComponentList.Image)
                {
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"Image\": ";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
            }
            break;
        }

        return jsonText;
    }
}