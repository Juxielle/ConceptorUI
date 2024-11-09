using ConceptorUI.Models;
using System.Windows;
using System.Windows.Input;


namespace ConceptorUi.ViewModels
{
    internal class WindowModel : Component
    {
        private readonly ContainerModel _statusbar;
        private readonly ContainerModel _body;
        private readonly RowModel _layout;

        public WindowModel(bool allowConstraints = false)
        {
            OnInit();

            Name = ComponentList.Window;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 1;

            _statusbar = new ContainerModel();
            _body = new ContainerModel();
            _layout = new RowModel();
            Content.Child = _layout.ComponentView;

            if (!allowConstraints) _init();

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        private void _init()
        {
            _statusbar.SelfConstraints();
            _statusbar.SetGroupVisibility(GroupNames.Global, false);
            _statusbar.SetGroupVisibility(GroupNames.Alignment, false);
            _statusbar.SetGroupVisibility(GroupNames.SelfAlignment, false);
            _statusbar.SetGroupVisibility(GroupNames.Transform, false);
            _statusbar.SetGroupVisibility(GroupNames.Text, false);
            _statusbar.SetGroupVisibility(GroupNames.Shadow, false);
            _statusbar.SetGroupVisibility(GroupNames.Appearance, false);
            _statusbar.SetGroupOnlyVisibility(GroupNames.Appearance);
            _statusbar.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.FillColor);
            _statusbar.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "25");
            _statusbar.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            _statusbar.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FF008975");
            _statusbar.OnInitialize();

            _body.SelfConstraints();
            _body.SetGroupVisibility(GroupNames.Alignment, false);
            _body.SetGroupVisibility(GroupNames.SelfAlignment, false);
            _body.SetGroupVisibility(GroupNames.Transform, false);
            _body.SetGroupVisibility(GroupNames.Text, false);
            _body.SetGroupVisibility(GroupNames.Appearance, false);
            _body.SetGroupVisibility(GroupNames.Shadow, false);
            _body.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            _body.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
            _body.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF");
            _body.OnInitialize();

            _layout.SelfConstraints();
            _layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            _layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
            _layout.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF");
            _layout.SetPropertyValue(GroupNames.Shadow, PropertyNames.ShadowColor, "#dddddd");
            _layout.OnInitialize();

            _layout.OnAdd(_statusbar, true);
            _layout.OnAdd(_body, true);

            Children.Add(_layout);
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            SetGroupVisibility(GroupNames.Global, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            SetGroupVisibility(GroupNames.SelfAlignment, false);
            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hc, "1");
            /* Transform */
            SetGroupVisibility(GroupNames.Transform, false);
            SetGroupOnlyVisibility(GroupNames.Transform);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.X);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y);
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "620");
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetGroupVisibility(GroupNames.Appearance, false);
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
        }

        protected override void WhenFileLoaded(string value)
        {
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            // var hl = _body.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
            // var hc = _body.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
            // var hr = _body.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);
            // var isHorizontal = hl == "1" || hc == "1" || hr == "1";
            //
            // var vt = _body.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
            // var vc = _body.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
            // var vb = _body.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);
            // var isVertical = vt == "1" || vc == "1" || vb == "1";
            //
            // if (isHorizontal)
            // {
            //     SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
            //     SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
            //     SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
            //     _body.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            // }
            // else if (isVertical)
            // {
            //     SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
            //     SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
            //     SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
            //     _body.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
            // }
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            Content.Child = child;
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return false;
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        protected override bool CanSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override bool CanChildSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override void RestoreProperties()
        {
        }

        protected override void Delete(int k = -1)
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