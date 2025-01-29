using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;
using UiDesigner.ViewModels.Components;

namespace UiDesigner.ViewModels.ListView
{
    class ListViewModel : Component
    {
        private readonly StackPanel _stack;

        public ListViewModel(bool isVertical = true, bool allowConstraints = false)
        {
            OnInit();

            _stack = new StackPanel
            {
                Orientation = isVertical ? Orientation.Vertical : Orientation.Horizontal
            };

            IsVertical = isVertical;
            Name = isVertical ? ComponentList.ListV : ComponentList.ListH;

            var scrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                Content = _stack
            };
            scrollViewer.PreviewMouseWheel += OnMouseWheel;

            Content.Child = scrollViewer;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollView = Content.Child as ScrollViewer;
            if (IsVertical) scrollView!.ScrollToVerticalOffset(scrollView.VerticalOffset - e.Delta);
            else scrollView!.ScrollToHorizontalOffset(scrollView.HorizontalOffset - e.Delta);
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void WhenFileLoaded(string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            if (k == -1)
            {
                _stack.Children.Add(child);
            }
            else _stack.Children.Insert(k, child);
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        public override bool AllowAuto(bool isWidth = true)
        {
            return true;
        }

        protected override void Delete(int k = -1)
        {
            var i = k;
            if (i == -1)
            {
                foreach (var child in Children.Where(child => child.Selected))
                {
                    i = Children.IndexOf(child);
                    break;
                }
            }

            if (i == -1) return;

            _stack.Children.RemoveAt(i);
            Children.RemoveAt(i);

            if (k == -1) OnSelected();
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
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
            var focus = this.GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
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
            var focus = this.GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
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

        public sealed override void SelfConstraints()
        {
            /* Global */
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            /* Text */
            this.SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            Children[id].Parent = this;
            /* Global */
            Children[id].SetPropertyVisibility(GroupNames.Global,
                !IsVertical ? PropertyNames.MoveLeft : PropertyNames.MoveTop);
            Children[id].SetPropertyVisibility(GroupNames.Global,
                !IsVertical ? PropertyNames.MoveRight : PropertyNames.MoveBottom);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);

            /* Content Alignment */
            /* Self Alignment */
            Children[id].SetGroupVisibility(GroupNames.SelfAlignment);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment,
                IsVertical ? PropertyNames.Vt : PropertyNames.Hl, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment,
                IsVertical ? PropertyNames.Vc : PropertyNames.Hc, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment,
                IsVertical ? PropertyNames.Vb : PropertyNames.Hr, false);

            /* Transform */
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Rot, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Hve, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, IsVertical ? PropertyNames.Ve : PropertyNames.He,
                false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, IsVertical ? PropertyNames.He : PropertyNames.Ve);

            /* Appearance */
            /* Shadow */

            Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1",
                true);

            var d = Children[id].GetGroupProperties(GroupNames.Transform)
                .GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);

            if (d != SizeValue.Expand.ToString() && Children[id]
                    .IsNullAlignment(GroupNames.SelfAlignment, IsVertical ? "Vertical" : "Horizontal"))
                Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, "1",
                    true);
            else if (d == SizeValue.Expand.ToString())
                Children[id].OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Width : PropertyNames.Height,
                    SizeValue.Expand.ToString(), true);
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
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
            ListViewRestoreProperties.RestoreProperties(this);
        }

        protected override void CheckVisibilities()
        {
            this.SetVisibilities();
        }
    }
}