using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.InverseConverters;

public static class SelfAlignmentConverter
{
    public static string Convert(CompSerializer compSerializer, string parentName, string space, bool isFirst = false)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        var jsonText = string.Empty;

        for (var i = 0; i < component.PropertyGroups!.Count; i++)
        {
            if (compSerializer.Properties![i].Name != GroupNames.SelfAlignment.ToString()) continue;
            
            for (var j = 0; j < component.PropertyGroups[i].Properties.Count; j++)
            {
                if (component.PropertyGroups[i].Properties[j].Name !=
                    compSerializer.Properties![i].Properties[j].Name ||
                    component.PropertyGroups[i].Properties[j].Value ==
                    compSerializer.Properties![i].Properties[j].Value) continue;

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;

                if ((name is "Hl" or "Hc" or "Hr") && parentName == ComponentList.Row.ToString() &&
                    parentName == ComponentList.ListV.ToString() && parentName == ComponentList.Stack.ToString())
                {
                    if(value == "0") continue;
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"SelfHorizontalAlignment\": ";
                    
                    value = name == "Hl" ? "left" :
                        name == "Hr" ? "right" :
                        name == "Hc" ? "center" : "none";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if ((name is "Vt" or "Vc" or "Vb") && parentName == ComponentList.Column.ToString() &&
                         parentName == ComponentList.ListH.ToString() && parentName == ComponentList.Stack.ToString())
                {
                    if(value == "0") continue;
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"SelfVerticalAlignment\": ";
                    
                    value = name == "Vt" ? "top" :
                        name == "Vc" ? "center" :
                        name == "Vb" ? "bottom" : "none";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
            }
            break;
        }

        return jsonText;
    }
}