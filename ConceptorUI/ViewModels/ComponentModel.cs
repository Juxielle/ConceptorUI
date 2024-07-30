using System.Windows;
using ConceptorUI.Models;


namespace ConceptorUI.ViewModels
{
    internal class ComponentModel : Component
    {
        public string Address { get; set; }

        public ComponentModel()
        {
            Name = ComponentList.Component;
            Address = string.Empty;
            IsInComponent = true;

            OnInitialize();
        }

        protected override void SelfConstraints()
        {
            /* Global */
            SetGroupVisibility(GroupNames.Global);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment);
            /* Self Alignment */
            SetGroupVisibility(GroupNames.SelfAlignment, false);
            /* Transform */
            SetGroupVisibility(GroupNames.Transform);
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "100");
            /* Text */
            SetGroupVisibility(GroupNames.Text);
            /* Appearance */
            SetGroupVisibility(GroupNames.Appearance);
            SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFF");
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow);
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
