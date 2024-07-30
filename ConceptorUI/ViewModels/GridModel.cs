using ConceptorUI.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace ConceptorUI.ViewModels
{
    class GridModel : Component
    {
        private List<List<Component>> Rows { get; set; }
        private List<Component> MergedCells { get; set; }
        private readonly Grid _grid;

        public GridModel()
        {
            _grid = new Grid();
            Rows = new List<List<Component>>();
            MergedCells = new List<Component>();

            ChildContent = _grid;

            Name = ComponentList.Grid;

            OnInitialize();
        }

        protected override void SelfConstraints()
        {
            /* Global */
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "20");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "20");
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Padding, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.BorderWidth, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.BorderRadius, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.FillColor, false);
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
