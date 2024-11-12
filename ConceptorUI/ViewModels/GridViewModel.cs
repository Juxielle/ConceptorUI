using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Models;

namespace ConceptorUi.ViewModels;

internal class GridViewModel : Component
{
    private readonly StackPanel _stack;

    public GridViewModel(bool isVertical = true, bool allowConstraints = false)
    {
        OnInit();

        _stack = new StackPanel { Orientation = Orientation.Vertical };
        Children = [];
        IsVertical = isVertical;
        //Name = isVertical ? ComponentList.ListH : ComponentList.ListV;

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
        Children[id]
            .SetPropertyVisibility(GroupNames.Transform, IsVertical ? PropertyNames.Ve : PropertyNames.He, false);
        Children[id].SetPropertyVisibility(GroupNames.Transform, IsVertical ? PropertyNames.He : PropertyNames.Ve);

        /* Appearance */
        /* Shadow */

        Children[id].OnInitialize();
        Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1",
            true);

        var d = Children[id].GetGroupProperties(GroupNames.Transform)
            .GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);

        if (d != SizeValue.Expand.ToString() && Children[id]
                .IsNullAlignment(GroupNames.SelfAlignment, IsVertical ? "Vertical" : "Horizontal"))
            Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, "1",
                true);
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
            var columnCount = 1;
            var childrenCount = 0;
            var lineCount = _stack.Children.Count;

            if (lineCount == childrenCount! / columnCount!)
            {
                var grid = new Grid();
                for (var i = 0; i < columnCount; i++)
                    grid.ColumnDefinitions.Add(
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Grid.SetColumn(child, 0);
                Grid.SetRow(child, lineCount);
                grid.Children.Add(child);
                _stack.Children.Add(grid);
            }
            else
            {
                var lastColumn = 0 + 1;
                var grid = _stack.Children[lineCount - 1] as Grid;
                Grid.SetColumn(child, lastColumn);
                Grid.SetRow(child, lineCount - 1);
                grid?.Children.Add(child);
            }
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

    protected override bool IsSelected(MouseButtonEventArgs e)
    {
        return false;
    }

    protected override bool CanSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
    {
        return true;
    }

    protected override bool CanChildSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
    {
        return true;
    }

    protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
    {
    }

    protected override void RestoreProperties()
    {
    }

    protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
    {
    }

    protected override void ContinueToInitialize(string groupName, string propertyName, string value)
    {
    }

    protected override object GetPropertyGroups()
    {
        return PropertyGroups!;
    }
}