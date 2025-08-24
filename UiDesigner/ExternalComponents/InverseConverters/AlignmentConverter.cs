using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.InverseConverters;

public static class AlignmentConverter
{
    public static string Convert(CompSerializer compSerializer, string space, bool isFirst = false)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        var jsonText = string.Empty;

        for (var i = 0; i < compSerializer.Properties!.Count; i++)
        {
            if (compSerializer.Properties![i].Name != GroupNames.Alignment.ToString()) continue;
            
            for (var j = 0; j < compSerializer.Properties[i].Properties.Count; j++)
            {
                /*if (component.PropertyGroups[i].Properties[j].Name !=
                    compSerializer.Properties![i].Properties[j].Name ||
                    component.PropertyGroups[i].Properties[j].Value ==
                    compSerializer.Properties![i].Properties[j].Value) continue;*/

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;

                if (name is "Hl" or "Hc" or "Hr")
                {
                    if(value == "0") continue;
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"HorizontalAlignment\": ";
                    
                    value = name == "Hl" ? "left" :
                        name == "Hr" ? "right" :
                        name == "Hc" ? "center" :
                        name == "SpaceBetween" ? "between" :
                        name == "SpaceAround" ? "around" :
                        name == "SpaceEvery" ? "every" : "none";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
                else if (name is "Vt" or "Vc" or "Vb")
                {
                    if(value == "0") continue;
                    var virgule = isFirst ? space : $",\n{space}";
                    jsonText += $"{virgule}\"VerticalAlignment\": ";
                    
                    value = name == "Vt" ? "top" :
                        name == "Vc" ? "center" :
                        name == "Vb" ? "bottom" :
                        name == "SpaceBetween" ? "between" :
                        name == "SpaceAround" ? "around" :
                        name == "SpaceEvery" ? "every" : "none";
                    
                    jsonText += $"\"{value}\"";
                    isFirst = false;
                }
            }
            break;
        }

        return jsonText;
    }
}