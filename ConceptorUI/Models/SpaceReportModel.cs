using System;
using System.Collections.Generic;
using ConceptorUI.ViewModels;

namespace ConceptorUI.Models;

class SpaceReportModel
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime Date { get; set; }
    public List<ReportModel> ReportModels;
    public List<WindowModel> ReportMns;
}