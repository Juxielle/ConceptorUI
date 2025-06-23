using System;
using System.Text.Json;
using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

class ExternalMetaComponent
{
    public string? X { get; set; }
    public string? Y { get; set; }
    public string? Width { get; set; }
    public string? Height { get; set; }
    public string? Background { get; set; }
    public string? HorizontalAlignment { get; set; }
    public string? VerticalAlignment { get; set; }
    
    public bool IsPage { get; set; }
    public string? InterpretationFile { get; set; }
    public ExternalComponent? Component { get; set; }
    
    public Component CreateComponent()
    {
        var compSerializer = Component?.ConvertToCompSerializer();
        
        var alignmentGroup = new GroupProperties
        {
            Name = GroupNames.Alignment.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (HorizontalAlignment != null)
        {
            var value = HorizontalAlignment;
            var alignmentName = value == "left" ? "Hl" : value == "right" ? "Hr" : "Hc";
            alignmentGroup.Properties.Add(new Property
            {
                Name = alignmentName,
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (VerticalAlignment != null)
        {
            var value = VerticalAlignment;
            var alignmentName = value == "top" ? "Vt" : value == "bottom" ? "Vb" : "Vc";
            alignmentGroup.Properties.Add(new Property
            {
                Name = alignmentName,
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }

        /*if (alignmentGroup.Properties.Count > 0)
            compSerializer?.Properties?.Add(alignmentGroup);*/
        
        var component = ComponentHelper.GetComponent(compSerializer?.Name!);
        component.OnDeserializer(compSerializer!);
        
        return component;
    }
    
    public string OnSerialize(string mappingValue, Component component)
    {
        var x = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.X);
        var y = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Y);
        var width = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        var height = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var background = component.GetGroupProperties(GroupNames.Appearance).GetValue(PropertyNames.FillColor);
        
        var hl = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);
        
        var vt = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);
        
        var interpretationFile = component.InterpretationFile;
        
        var compSerialize = component.OnSerializer();
        var childString = new ExternalComponent().ConvertToComponent(compSerialize, space: "    ");
        
        var jsonText = "{\n";
        jsonText += $"    \"X\": \"{x}\",\n";
        jsonText += $"    \"Y\": \"{y}\",\n";
        jsonText += $"    \"Width\": \"{width}\",\n";
        jsonText += $"    \"Height\": \"{height}\",\n";
        
        if(background != "Transparent")
            jsonText += $"    \"Background\": \"{background}\",\n";
        
        if(hl == "1")
            jsonText += $"    \"HorizontalAlignment\": \"{hl}\",\n";
        else if(hc == "1")
            jsonText += $"    \"HorizontalAlignment\": \"{hc}\",\n";
        else if(hr == "1")
            jsonText += $"    \"HorizontalAlignment\": \"{hr}\",\n";
        
        if(vt == "1")
            jsonText += $"    \"VerticalAlignment\": \"{vt}\",\n";
        else if(vc == "1")
            jsonText += $"    \"VerticalAlignment\": \"{vc}\",\n";
        else if(vb == "1")
            jsonText += $"    \"VerticalAlignment\": \"{vb}\",\n";
        
        jsonText += $"    \"InterpretationFile\": \"{interpretationFile}\",\n";
        jsonText += $"    \"Component\": {childString}";
        jsonText += "}";
        
        return jsonText;
    }
}