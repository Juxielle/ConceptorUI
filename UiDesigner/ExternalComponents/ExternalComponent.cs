using System;
using System.Collections.Generic;
using System.Text.Json;
using ConceptorUI.ExternalComponents.InverseConverters;
using ConceptorUI.Models;
using ConceptorUI.Views.Component;
using DesignForge.ExternalComponents.PropertySettings;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public class ExternalComponent
{
    public string? Name { get; set; }
    public ExternalProperty? Property { get; set; }
    public List<ExternalComponent>? Children { get; set; }
    public bool IsReference { get; set; }
    public Dictionary<string, string>? ExternalProperty { get; set; }

    public CompSerializer ConvertToCompSerializer()
    {
        if (IsReference)
        {
            var externalComponentString = PageView.Instance.GetExternalComponent(Name!);
            if(externalComponentString == null!) return null!;
            var externalComponent = JsonSerializer.Deserialize<ExternalComponent>(externalComponentString);

            if (ExternalProperty != null && ExternalProperty.Count != 0)
            {
                externalComponent?.BothSet(this);
                externalComponent?.SetPropertyValues(ExternalProperty!);
            }
            
            return externalComponent!.ConvertToCompSerializer();
        }
        
        var compSerializer = new CompSerializer
        {
            Properties = [],
            Children = [],
            Name = Name
        };

        var alignmentGroup = ExternalAlignment.Convert(this);
        if (alignmentGroup.Properties.Count > 0)
            compSerializer.Properties.Add(alignmentGroup);

        var selfAlignmentGroup = ExternalSelfAlignment.Convert(this);
        if (selfAlignmentGroup.Properties.Count > 0)
            compSerializer.Properties.Add(selfAlignmentGroup);

        var transformGroup = ExternalTransform.Convert(this);
        if (transformGroup.Properties.Count > 0)
            compSerializer.Properties.Add(transformGroup);

        var textGroup = ExternalText.Convert(this);
        if (textGroup.Properties.Count > 0)
            compSerializer.Properties.Add(textGroup);

        var appearanceGroup = ExternalAppearance.Convert(this);
        if (appearanceGroup.Properties.Count > 0)
            compSerializer.Properties.Add(appearanceGroup);

        var shadowGroup = ExternalShadow.Convert(this);
        if (shadowGroup.Properties.Count > 0)
            compSerializer.Properties.Add(shadowGroup);

        var globalGroup = ExternalGlobal.Convert(this);
        if (globalGroup.Properties.Count > 0)
            compSerializer.Properties.Add(globalGroup);
        
        if (Children == null || Children.Count == 0)
            return compSerializer;
        
        foreach (var child in Children)
        {
            var childCompSerializer = child.ConvertToCompSerializer();
            if(childCompSerializer == null!) continue;
            
            compSerializer.Children.Add(childCompSerializer);
        }

        return compSerializer;
    }

    public string ConvertToComponent(CompSerializer compSerializer, string parentName = "", string space = "", bool isLast = true)
    {
        var jsonText = $"{space}" + "{\n";
        var first = true;

        jsonText += $"{space}    \"Name\": \"{compSerializer.Name}\",\n";
        var property = $"{space}    \"Property\": " + "{\n";

        var value = AlignmentConverter.Convert(compSerializer, $"{space}        ", first);
        var propertyValue = value;
        first = value == string.Empty;

        if(parentName != ComponentList.Container.ToString() && parentName != string.Empty)
        {
            value = SelfAlignmentConverter.Convert(compSerializer, parentName, $"{space}        ", first);
            propertyValue += value;
            first = first && value == string.Empty;
        }

        value = TransformConverter.Convert(compSerializer, $"{space}        ", first);
        propertyValue += value;
        first = first && value == string.Empty;

        value = TextConverter.Convert(compSerializer, $"{space}        ", first);
        propertyValue += value;
        first = first && value == string.Empty;

        value = AppearanceConverter.Convert(compSerializer, $"{space}        ", first);
        propertyValue += value;
        first = first && value == string.Empty;

        value = ShadowConverter.Convert(compSerializer, $"{space}        ", first);
        propertyValue += value;
        first = first && value == string.Empty;

        value = GlobalConverter.Convert(compSerializer, $"{space}        ", first);
        propertyValue += value;

        string virgule;
        if (propertyValue != string.Empty)
        {
            jsonText += property + propertyValue;
            //virgule = compSerializer.Children == null || compSerializer.Children.Count == 0 ? "" : ",\n";
            jsonText += "\n" + $"{space}    " + "},\n";
        }

        if (compSerializer.Children == null || compSerializer.Children.Count == 0)
        {
            virgule = isLast ? "" : ",";
            jsonText += $"{space}    \"Children\": " + "[]";
            jsonText += "\n" + $"{space}" + "}" + $"{virgule}\n";
            return jsonText;
        }

        jsonText += $"{space}    \"Children\": " + "[\n";
        var index = 0;
        foreach (var child in compSerializer.Children)
        {
            var isL = index == compSerializer.Children.Count - 1;
            jsonText += ConvertToComponent(child, compSerializer.Name!, $"{space}        ", isL);
            index++;
        }

        jsonText += $"{space}   " + "]";

        virgule = isLast ? "" : ",";
        jsonText += "\n" + $"{space}" + "}" + $"{virgule}\n";

        return jsonText;
    }

    private void SetPropertyValues(Dictionary<string, string> properties)
    {
        if (ExternalProperty is { Count: > 0 })
        {
            foreach (var key in ExternalProperty.Keys)
            {
                if(!properties.TryGetValue(key, out var property)) continue;
                //if(this.IsProperty(property)) return;
                this.Set(ExternalProperty[key], property);
            }
        }
        
        if(Children == null || Children.Count == 0) return;
        foreach (var child in Children)
        {
            child.SetPropertyValues(properties);
        }
    }
}