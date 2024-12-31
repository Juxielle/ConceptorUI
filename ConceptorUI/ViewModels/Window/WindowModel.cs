using System.Windows;
using System.Windows.Input;
using ConceptorUI.Models;
using ConceptorUi.ViewModels;
using ConceptorUI.ViewModels.Container;
using ConceptorUI.ViewModels.Row;

namespace ConceptorUI.ViewModels.Window
{
    internal class WindowModel : Component
    {
        public readonly ContainerModel Statusbar;
        public readonly ContainerModel Body;
        public readonly RowModel Layout;

        public WindowModel(bool allowConstraints = false)
        {
            OnInit();

            Name = ComponentList.Window;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 1;

            Statusbar = new ContainerModel();
            Body = new ContainerModel();
            Layout = new RowModel();
            Content.Child = Layout.ComponentView;

            if (!allowConstraints) _init();

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        private void _init()
        {
            Statusbar.SelfConstraints();
            Statusbar.SetGroupVisibility(GroupNames.Global, false);
            Statusbar.SetGroupVisibility(GroupNames.Alignment, false);
            Statusbar.SetGroupVisibility(GroupNames.SelfAlignment, false);
            Statusbar.SetGroupVisibility(GroupNames.Transform, false);
            Statusbar.SetGroupVisibility(GroupNames.Text, false);
            Statusbar.SetGroupVisibility(GroupNames.Shadow, false);
            Statusbar.SetGroupVisibility(GroupNames.Appearance, false);
            Statusbar.SetGroupOnlyVisibility(GroupNames.Appearance);
            Statusbar.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.FillColor);
            Statusbar.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "25");
            Statusbar.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            Statusbar.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FF008975");
            Statusbar.OnInitialize();

            Body.SelfConstraints();
            Body.SetGroupVisibility(GroupNames.Alignment, false);
            Body.SetGroupVisibility(GroupNames.SelfAlignment, false);
            Body.SetGroupVisibility(GroupNames.Transform, false);
            Body.SetGroupVisibility(GroupNames.Text, false);
            Body.SetGroupVisibility(GroupNames.Appearance, false);
            Body.SetGroupVisibility(GroupNames.Shadow, false);
            Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
            Body.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF");
            Body.OnInitialize();

            Layout.SelfConstraints();
            Layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
            Layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
            Layout.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFFFF");
            Layout.SetPropertyValue(GroupNames.Shadow, PropertyNames.ShadowColor, "#dddddd");
            Layout.OnInitialize();

            Layout.OnAdd(Statusbar, true);
            Layout.OnAdd(Body, true);

            Children.Add(Layout);
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
            if (propertyName == PropertyNames.Width.ToString())
            {
                Body.SelectedContent.Width = double.NaN;
                Body.SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
            }
            else if (propertyName == PropertyNames.Height.ToString())
            {
                Body.SelectedContent.Height = double.NaN;
                Body.SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
            }
        }

        protected override void WhenFileLoaded(string value)
        {
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            
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

        public override bool AllowAuto(bool isWidth = true)
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
            WindowRestoreProperties.RestoreProperties(this);
        }

        protected override void CheckVisibilities()
        {
            this.SetVisibilities();
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