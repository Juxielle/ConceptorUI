using ConceptorUI.Models;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;


namespace ConceptorUi.ViewModels
{
    internal class StackModel : Component
    {
        private readonly Grid _grid;

        public StackModel(bool allowConstraints = false)
        {
            OnInit();

            _grid = new Grid();
            Content.Child = _grid;

            Name = ComponentList.Stack;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            Children[id].Parent = this;
            /* Global */
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveLeft, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveRight, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);

            /* Content Alignment */
            /* Self Alignment */
            Children[id].SetGroupVisibility(GroupNames.SelfAlignment);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.HC, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.VC, false);
            /* Transform */
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.ROT, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);

            /* Appearance */
            /* Shadow */

            Children[id].OnInitialize();

            var w = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if (w != SizeValue.Expand.ToString() &&
                (Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Horizontal") || IsForceAlignment))
                Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);

            var h = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            if (h != SizeValue.Expand.ToString() &&
                (Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Vertical") || IsForceAlignment))
                Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);

            IsForceAlignment = false;
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
        }

        protected override void WhenFileLoaded(string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child)
        {
            _grid.Children.Add(child);
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        protected override void Delete()
        {
            var i = -1;
            foreach (var child in Children.Where(child => child.Selected))
            {
                i = Children.IndexOf(child);
                break;
            }

            if (i == -1) return;

            _grid.Children.RemoveAt(i);
            Children.RemoveAt(i);

            OnSelected();
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