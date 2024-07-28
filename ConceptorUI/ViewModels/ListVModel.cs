using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using ConceptorUI.Models;
using System.Linq;


namespace ConceptorUI.ViewModels
{
    class ListVModel : Component
    {
        private readonly StackPanel _stack;

        public ListVModel(bool isVertical = true)
        {
            _stack = new StackPanel{ Orientation = Orientation.Vertical };
            Children = new List<Component>();
            IsVertical = isVertical;

            ChildContent = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                Content = _stack
            };
            
            Name = ComponentList.ListV;
            ComponentView.PreviewMouseWheel += OnMouseWheel;

            OnInitialize();
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollView = ComponentView as ScrollViewer;
            if(IsVertical) scrollView!.ScrollToVerticalOffset(scrollView.VerticalOffset - e.Delta);
            else scrollView!.ScrollToHorizontalOffset(scrollView.HorizontalOffset - e.Delta);
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
            _stack.Children.Add(child);
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
            _stack.Children.RemoveAt(i);
            Children.RemoveAt(i);
            OnSelected();
        }
        
        protected override void WhenWidthChanged(string value)
        {
            
        }
        
        protected override void WhenHeightChanged(string value)
        {
            
        }
        
        protected override void OnMoveLeft()
        {
            OnMoveTop();
        }
        
        protected override void OnMoveRight()
        {
            OnMoveBottom();
        }
        
        protected override void OnMoveTop()
        {
            var focus = GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;
            foreach (var child in Children.Where(child => child.Selected))
            {
                k = Children.IndexOf(child);
                break;
            }

            if (k == -1) return;
            if (focus)
            {
                var child = Children[k];
                Children.RemoveAt(k);
                Children.Insert(k - 1, child);
                _stack.Children.RemoveAt(k);
                _stack.Children.Insert(k - 1, child.ComponentView);
            }
            else
            {
                Children[k].OnUnselected();
                Children[k - 1].OnSelected();
            }
        }
        
        protected override void OnMoveBottom()
        {
            var focus = GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;
            foreach (var child in Children.Where(child => child.Selected))
            {
                k = Children.IndexOf(child);
                break;
            }

            if (k == -1) return;
            if (focus)
            {
                var child = Children[k];
                Children.RemoveAt(k);
                Children.Insert(k + 1, child);
                _stack.Children.RemoveAt(k);
                _stack.Children.Insert(k + 1, child.ComponentView);
            }
            else
            {
                Children[k].OnUnselected();
                Children[k + 1].OnSelected();
            }
        }

        protected override void SelfConstraints()
        {
            /* Global */
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            /* Text */
            SetGroupVisibility(GroupNames.Text);
            /* Appearance */
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            Children[id].Parent = this;
            /* Global */
            Children[id].SetGroupVisibility(GroupNames.Global);
            Children[id].SetPropertyVisibility(GroupNames.Global, IsVertical ? PropertyNames.MoveLeft : PropertyNames.MoveTop, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, IsVertical ? PropertyNames.MoveRight : PropertyNames.MoveBottom, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            
            /* Content Alignment */
            /* Self Alignment */
            Children[id].SetGroupVisibility(GroupNames.SelfAlignment);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, false);
            
            /* Transform */
            Children[id].SetGroupVisibility(GroupNames.Transform);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.ROT, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.HVE, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, IsVertical ? PropertyNames.VE : PropertyNames.HE, false);
            
            /* Appearance */
            Children[id].SetGroupVisibility(GroupNames.Appearance);
            /* Shadow */
            
            Children[id].OnInitialize();
            Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1", true);
            
            var d = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);
            if(d != SizeValue.Expand.ToString() && Children[id].IsNullAlignment(GroupNames.SelfAlignment, IsVertical ? "Vertical" : "Horizontal"))
                Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HL : PropertyNames.VT, "1", true);
        }
    }
}
 