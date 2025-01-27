using UiDesigner.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UiDesigner.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;


namespace ConceptorUi.ViewModels
{
    class GridModel : Component
    {
        private List<List<Component>> Rows { get; set; }
        private List<Component> MergedCells { get; set; }
        private readonly Grid _grid;

        public GridModel()
        {
            _grid = new Grid();
            Rows = [];
            MergedCells = [];

            Content.Child = _grid;
            Name = ComponentList.Grid;

            OnInitialize();
        }

        public override void SelfConstraints()
        {
            /* Global */
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            this.SetGroupVisibility(GroupNames.Transform);
            /* Text */
            this.SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
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

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        public override bool AllowAuto(bool isWidth = true)
        {
            return true;
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
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

        protected override void CheckVisibilities()
        {
        }
    }
}