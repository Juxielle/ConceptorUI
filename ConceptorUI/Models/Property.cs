

namespace ConceptorUI.Models
{
    public class Property
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string PreviewValue { get; set; } = null!;
        public string EvalValue { get; set; } = null!;
        public string[] Items { get; set; } = null!;
        public string Visibility { get; set; } = VisibilityValue.Collapsed.ToString();
    }
}
