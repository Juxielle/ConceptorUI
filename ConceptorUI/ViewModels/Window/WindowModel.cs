﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Models;
using ConceptorUI.Services;
using ConceptorUi.ViewModels;
using ConceptorUI.ViewModels.Container;
using ConceptorUI.ViewModels.Row;

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

            Statusbar = new ContainerModel();
            Body = new ContainerModel();
            Layout = new RowModel();

            _border = new Border
            {
                Padding = new Thickness(10, 60, 12, 60),
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

        private void LoadImage()
        {
            // var bitmap = new BitmapImage();
            // bitmap.BeginInit();
            // bitmap.UriSource = new Uri("pack://application:,,,/Assets/default_mobile_traite.png", UriKind.Absolute);
            // bitmap.EndInit();
            _image.Source = ReadScreenImageService.GetImage("default");
        }

        public void ChangeScreen(object screen)
        {
            var screenUi = (ScreenUiDto)screen;
            
            _border.Padding = new Thickness(screenUi.MarginLeft,
                screenUi.MarginTop,
                screenUi.MarginRight,
                screenUi.MarginBottom);
            
            _image.Source = ReadScreenImageService.GetImage(screenUi.Label);
            
            OnUpdated(GroupNames.Transform, PropertyNames.Width, $"{screenUi.Width}", true);
            OnUpdated(GroupNames.Transform, PropertyNames.Height, $"{screenUi.Height}", true);
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