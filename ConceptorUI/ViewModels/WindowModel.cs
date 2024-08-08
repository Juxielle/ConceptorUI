using ConceptorUI.Models;
using System.Collections.Generic;
using System.Windows;


namespace ConceptorUI.ViewModels
{
    internal class WindowModel : Component
    {
        private readonly ContainerModel _statusbar;
        private readonly ContainerModel _body;
        private readonly RowModel _layout;

        public WindowModel()
        {
            _statusbar = new ContainerModel();
            _body = new ContainerModel();
            _layout = new RowModel();
            _init();
            
            ChildContent = _layout.ComponentView;
            Name = ComponentList.Window;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 0;
            
            OnInitialize();
        }

        private void _init()
        {
            _statusbar.OnUpdated(GroupNames.Transform, PropertyNames.Height, "25");
            _statusbar.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            _statusbar.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FF008975");
            
            _body.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            _body.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
            _body.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF");
            
            _layout.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            _layout.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
            _layout.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF");
            _layout.OnUpdated(GroupNames.Shadow, PropertyNames.ShadowColor, "#000000");
            _layout.OnUpdated(GroupNames.Shadow, PropertyNames.ShadowDepth, "0");
            _layout.OnUpdated(GroupNames.Shadow, PropertyNames.BlurRadius, "10");
            _layout.OnAdd(_statusbar);
            _layout.OnAdd(_body);
        }

        protected override void SelfConstraints()
        {
            /* Global */
            SetGroupVisibility(GroupNames.Global, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            SetGroupVisibility(GroupNames.SelfAlignment, false);
            /* Transform */
            SetGroupVisibility(GroupNames.Transform, false);
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "650");
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetGroupVisibility(GroupNames.Appearance, false);
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
        }
        
        protected override void WhenFileLoaded(string value)
        {
            
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
            
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
            
        }

        protected override void InitChildContent()
        {
            
        }
        
        protected override void AddIntoChildContent(FrameworkElement child)
        {
            
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return false;
        }

        protected override void Delete()
        {
            
        }
        
        protected override void WhenWidthChanged(string value)
        {
            
        }
        
        protected override void WhenHeightChanged(string value)
        {
            
        }
        
        protected override void OnMoveLeft()
        {
            
        }
        
        protected override void OnMoveRight()
        {
            
        }
        
        protected override void OnMoveTop()
        {
            
        }
        
        protected override void OnMoveBottom()
        {
            
        }

        public string OnCopyOrPaste(string value = null!, bool isPaste = false)
        {
            CompSerializer valueD = null!;
            if (value != null!)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerializer>(value)!;

            if (Selected && isPaste && value != null && valueD.Children != null)
            {
                var name = valueD.Name!;
                var component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this;
                component.OnDeserializer(valueD);
                //Il faut enfin appliquer à ce composant, les contraintes de mise en page.
                //Child = component;
                //border.Child = Child.ComponentView;
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerializer());
            else if (!Selected)
                return _body.OnCopyOrPaste(value!, isPaste);
            return null!;
        }

        public StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = Name.ToString()!;
            structuralElement.Selected = Selected;

            structuralElement.Children = 
                new List<StructuralElement> { _statusbar.AddToStructuralView(), _body.AddToStructuralView() };

            return structuralElement;
        }

        public void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                //PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
            }
            else if (structuralElement.Children != null!)
            {
                foreach (var child in structuralElement.Children!)
                {
                    _body.SelectFromStructuralView(child);
                }
            }
        }
    }
}
