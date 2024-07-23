using ConceptorUI.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ConceptorUI.ViewModels
{
    abstract class Component
    {
        public Dictionary<string, PropStringToIndex>? Ids;
        public string Name { get; set; } = "Component";
        public string CoName { get; set; } = "None";
        public ComponentList EnumName { get; set; }
        public List<GroupProperties>? groupProps { get; set; }
        public FrameworkElement? ComponentView;
        public Component? Parent;
        public bool Selected = false;
        public bool Added { get; set; } = false;
        public Brush? SavedColor;
        public Thickness SavedThickness;
        public abstract void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false);
        public abstract bool OnSelected(bool isInterne = false);
        public abstract bool OnChildSelected();
        public abstract void OnUnselected(bool isInterne = false);
        public abstract void OnInitialized();
        public abstract void OnConfigured();
        public abstract void OnAddConfig(GroupNames groupName, PropertyNames propertyName, string value, bool isGroup = true);
        public abstract int[] GetPosition(GroupNames groupName, PropertyNames propertyName);
        public abstract void OnConfiguOut(int id);
        public abstract void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false);
        public abstract string OnCopyOrPaste(string value = null!, bool isPaste = false);
        public abstract void LoadIds();
        public abstract ReactComponent BuildReactComponent(string tab, int n, string id);
        public abstract WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams);
        public abstract ReactComponent BuildFlutterComponent(string tab, int n, string id);
        public abstract ReactComponent BuildAndroidXmlComponent(string tab, int n, string id);
        public abstract ReactComponent BuildAndroidComposeComponent(string tab, int n, string id);
        public abstract double Width(Component component);
        public abstract double Height(Component component);
        public abstract CompSerialiser OnSerialiser();
        public abstract void OnDeserialiser(CompSerialiser compSerialiser);
        public abstract StructuralElement AddToStructuralView();
        public abstract void SelectFromStructuralView(StructuralElement structuralElement);
        public abstract void UpdateComponent(List<ComponentRef> refs, GroupNames groupName, PropertyNames propertyName, string value, int i = 0, bool found = false);
        public abstract void AddComponent(List<ComponentRef> refs, string data, int i = 0, bool found = false);
        public abstract void DeleteComponent(List<ComponentRef> refs, int i = 0, bool found = false);
        public abstract List<ComponentRef> BuildComponentRefs(int i);
        public abstract bool AllowFixSize(bool isHeight = true);
    }
}
