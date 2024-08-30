﻿using ConceptorUI.Models;
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
            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HC, "1");
            /* Transform */
            SetGroupVisibility(GroupNames.Transform);
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
            Content.Child = child;
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