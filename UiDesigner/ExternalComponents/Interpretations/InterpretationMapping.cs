using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.Interpretations;

public static class InterpretationMapping
{
    public static string Mapping(string text, CompSerializer compSerializer, string id = "1", int maxLevel = 0)
    {
        if (maxLevel == 0) maxLevel = TotalLevels(compSerializer);
        var formedId = FormatLevel(id, maxLevel);
        
        foreach (var group in compSerializer.Properties!)
        {
            foreach (var property in group.Properties)
            {
                var name = $"#{property.Name}#{formedId}";
                text = text.Replace(name, property.Value);
            }
        }
        
        if(compSerializer.Children == null || compSerializer.Children!.Count == 0)
            return text;
        
        var index = 1;
        foreach (var child in compSerializer.Children)
        {
            text = Mapping(text, child, $"{id}{index}", maxLevel);
            index++;
        }
        
        return text;
    }

    private static int TotalLevels(CompSerializer compSerializer)
    {
        var levels = 1;
        
        if(compSerializer.Children == null || compSerializer.Children!.Count == 0)
            return levels;
        
        var max = 1;
        foreach (var child in compSerializer.Children)
        {
            var maxLevels = TotalLevels(child);
            if(maxLevels > max) max = maxLevels;
        }
        
        return levels + max;
    }

    private static string FormatLevel(string level, int maxLevels)
    {
        var missingLevels = maxLevels - level.Length;
        
        for (var i = 0; i < missingLevels; i++)
        {
            level += "0";
        }

        return level;
    }
}