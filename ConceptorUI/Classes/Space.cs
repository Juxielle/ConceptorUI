using System;
using System.Collections.Generic;

namespace ConceptorUI.Classes;

public class Space
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Status { get; set; }
    public List<Report> Reports { get; set; }
    public DateTime Date { get; set; }
}