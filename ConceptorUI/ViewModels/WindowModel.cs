using ConceptorUI.Models;
using System.Windows;


namespace ConceptorUi.ViewModels
{
    internal class WindowModel : Component
    {
        private readonly ContainerModel _statusbar;
        private readonly ContainerModel _body;
        private readonly RowModel _layout;
        
        public WindowModel()
        {
            OnInit();
            
            Name = ComponentList.Window;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 1;
            
            SelfConstraints();
            OnInitialize();
            
            _statusbar = new ContainerModel();
            _body = new ContainerModel();
            _layout = new RowModel();
            ChildContent = _layout.ComponentView;
            _init();
        }

        private void _init()
        {
            _statusbar.OnUpdated(GroupNames.Transform, PropertyNames.Height, "25", true);
            _statusbar.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
            _statusbar.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FF008975", true);
            
            _body.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
            _body.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
            _body.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF", true);
            
            _layout.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
            _layout.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
            _layout.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF", true);
            _layout.OnUpdated(GroupNames.Shadow, PropertyNames.ShadowColor, "#000000", true);
            _layout.OnUpdated(GroupNames.Shadow, PropertyNames.ShadowDepth, "0", true);
            _layout.OnUpdated(GroupNames.Shadow, PropertyNames.BlurRadius, "10", true);
            
            _layout.OnAdd(_statusbar);
            _layout.OnAdd(_body);
            
            Children.Add(_layout);
        }

        protected sealed override void SelfConstraints()
        {
            /* Global */
            SetGroupVisibility(GroupNames.Global, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            SetGroupVisibility(GroupNames.SelfAlignment, false);
            /* Transform */
            SetGroupVisibility(GroupNames.Transform);
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "650");
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetGroupVisibility(GroupNames.Appearance);
            SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#ffffff");
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
    }
}
