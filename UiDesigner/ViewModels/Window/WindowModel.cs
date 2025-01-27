using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Application.Dto.UiDto;
using UiDesigner.Models;
using UiDesigner.Services;
using UiDesigner.Utils;
using UiDesigner.ViewModels.Components;
using UiDesigner.ViewModels.Container;
using UiDesigner.ViewModels.Row;
using UiDesigner.ViewModels.Window;

namespace ConceptorUI.ViewModels.Window
{
    internal class WindowModel : Component
    {
        public ContainerModel Statusbar;
        public ContainerModel Body;
        public RowModel Layout;

        private readonly Grid _grid;
        private readonly Border _border;
        private readonly System.Windows.Controls.Image _image;
        public double Width;
        public double Height;
        private double _ratio;

        public WindowModel(bool allowConstraints = false)
        {
            OnInit();

            _grid = new Grid();
            _image = new System.Windows.Controls.Image { Stretch = Stretch.Fill };
            LoadImage();

            Name = ComponentList.Window;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 1;
            Width = 280;
            _ratio = 2.022106631989597;
            Height = Width * _ratio;

            Statusbar = new ContainerModel();
            Body = new ContainerModel();
            Layout = new RowModel();

            _border = new Border
            {
                Padding = new Thickness(10, 58, 12, 56),
                Child = Layout.ComponentView
            };

            _grid.Children.Add(_image);
            _grid.Children.Add(_border);
            Content.Child = _grid;

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
            return e.OriginalSource.Equals(_grid) || e.OriginalSource.Equals(_border) ||
                   e.OriginalSource.Equals(_image);
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            this.SetGroupVisibility(GroupNames.Global, false);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            this.SetGroupVisibility(GroupNames.SelfAlignment, false);
            this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hc, "1");
            /* Transform */
            this.SetGroupVisibility(GroupNames.Transform, false);
            this.SetGroupOnlyVisibility(GroupNames.Transform);
            this.SetPropertyVisibility(GroupNames.Transform, PropertyNames.X);
            this.SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y);
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, $"{Width}");
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, $"{Height}");
            /* Text */
            this.SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            this.SetGroupVisibility(GroupNames.Appearance, false);
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow, false);
        }

        private void LoadImage()
        {
            _image.Source = ReadScreenImageService.GetImage("default");
        }

        public void ChangeScreen(object screen, bool isSaving = true)
        {
            var screenUi = (ScreenUiDto)screen;
            var screenJson = JsonSerializer.Serialize(screenUi);

            _border.Padding = new Thickness(screenUi.MarginLeft,
                screenUi.MarginTop,
                screenUi.MarginRight,
                screenUi.MarginBottom);

            _image.Source = ReadScreenImageService.GetImage(screenUi.Label);

            Width = screenUi.Width;
            _ratio = screenUi.Ratio;
            Height = Width * _ratio;
            
            if (!isSaving) return;

            OnUpdated(GroupNames.Transform, PropertyNames.Width, $"{Width}", true);
            OnUpdated(GroupNames.Transform, PropertyNames.Height, $"{Height}", true);
            this.SetPropertyValue(GroupNames.Global, PropertyNames.Screen, screenJson);
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
            if (groupName == GroupNames.Global.ToString() && propertyName == PropertyNames.Screen.ToString())
            {
                if (!Helper.IsDeserializable<ScreenUiDto>(value)) return;
                var screenUi = Helper.Deserialize<ScreenUiDto>(value);
                ChangeScreen(screenUi, false);
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
            _border.Child = child;

            Layout = (RowModel)Children[0];
            Statusbar = (ContainerModel)Layout.Children[0];
            Body = (ContainerModel)Layout.Children[1];
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