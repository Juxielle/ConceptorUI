using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Models;

namespace ConceptorUI.ViewModels;

class ListHModel : Component
{
    private readonly StackPanel _stack;

    public ListHModel()
    {
        _stack = new StackPanel{ Orientation = Orientation.Horizontal };
        Children = new List<Component>();

        ChildContent = new ScrollViewer
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
            Content = _stack
        };
            
        Name = ComponentList.ListH;
        IsVertical = false;
        ComponentView.PreviewMouseWheel += OnMouseWheel;

        OnInitialize();
    }

    private void OnMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var scrollView = ComponentView as ScrollViewer;
        scrollView!.ScrollToHorizontalOffset(scrollView.HorizontalOffset - e.Delta);
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
    
    protected override void WhenWidthChanged()
    {
        
    }
    
    protected override void WhenHeightChanged()
    {
        
    }

    protected override void WhenFileLoaded(string value)
    {
            
    }
    
    protected override void OnMoveLeft()
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
    
    protected override void OnMoveRight()
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
    
    protected override void OnMoveTop()
    {
        
    }
    
    protected override void OnMoveBottom()
    {
        
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
        Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveTop, false);
        Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveBottom, false);
        Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
        
        /* Content Alignment */
        /* Self Alignment */
        Children[id].SetGroupVisibility(GroupNames.SelfAlignment);
        Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.HL, false);
        Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.HC, false);
        Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.HR, false);
        
        /* Transform */
        Children[id].SetGroupVisibility(GroupNames.Transform);
        Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.ROT, false);
        Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
        Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
        Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);
        Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.HVE, false);
        Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.HE, false);
        
        /* Text */
        /* Appearance */
        Children[id].SetGroupVisibility(GroupNames.Appearance);
        /* Shadow */
        
        Children[id].OnInitialize();
        Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
        
        var h = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        if(h != SizeValue.Expand.ToString() && Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Horizontal"))
            Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
    }
}