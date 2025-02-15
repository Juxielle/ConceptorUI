﻿using System.Collections.Generic;
using UiDesigner.Models;

namespace UiDesigner.Platforms;

public abstract class Platform
{
    public PlatformEnums PlatformType { get; init; }
    public List<PlatformPage> Pages { get; init; }
    public static string WhiteSpace = "    ";
    
    public abstract void GenerateCode(CompSerializer compSerializer, string path);
}