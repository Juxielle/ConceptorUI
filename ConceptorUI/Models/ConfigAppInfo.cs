using System;
using System.Collections.Generic;



namespace ConceptorUI.Models
{
    internal class ConfigAppInfo
    {
        public List<SpaceModel> Spaces { get; set; }
    }

    internal class SpaceModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public List<ReportInfo> Reports { get; set; }
    }
}
