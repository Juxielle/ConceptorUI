﻿using System.Linq;
using ConceptorUI.Models;

namespace ConceptorUI.Platforms.FlutterPlatform.Components;

public class FtContainer : FtComponent
{
    public string Border { get; set; }
    public string BorderLeft { get; set; }
    public string BorderRight { get; set; }
    public string BorderTop { get; set; }
    public string BorderBottom { get; set; }

    public string Margin { get; set; }

    public string Padding { get; set; }

    public string BorderRadius { get; set; }
    public string BorderBottomLeftRadius { get; set; }
    public string BorderBottomRightRadius { get; set; }
    public string BorderTopLeftRadius { get; set; }
    public string BorderTopRightRadius { get; set; }

    public FtComponent Child { get; set; }

    protected override void Build(CompSerializer compSerializer)
    {
        throw new System.NotImplementedException();
    }

    protected override void BuildSingle(FtComponent child)
    {
        throw new System.NotImplementedException();
    }

    protected override string ConvertToString(string space)
    {
        var newSpace = space + Platform.WhiteSpace;
        var properties = string.Join("\n", Properties.Select(p => p));
        
        return space + "Container(\n" +
               $"{properties}\n" +
               space + ")";
    }
}