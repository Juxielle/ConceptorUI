﻿using System.Collections.Generic;

namespace ConceptorUI.Classes;

public class Project
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Version { get; set; } = "1.0";
    public string Password { get; set; }
    public string Color { get; set; }
    public string LastOpen { get; set; }
    public string Image { get; set; }
    public List<Space> Spaces { get; set; }
    public string Created { get; set; }
    public string Updated { get; set; }
}