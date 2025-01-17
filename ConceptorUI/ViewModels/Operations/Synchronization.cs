using System;
using System.Windows;
using ConceptorUI.Models;
using ConceptorUI.Utils;

namespace ConceptorUi.ViewModels.Operations;

static class Synchronization
{
    public static void Synchronize(this Component component)
    {
        var hl = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr);

        var vt = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb);

        //var ah = SelfAlignment.Horizontal(component);
        var isHorizontal = SelfAlignment.IsHorizontal(component);

        //var av = SelfAlignment.Vertical(component);
        var isVertical = SelfAlignment.IsVertical(component);

        var height = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (hl == "1" && component.SelectedContent.HorizontalAlignment != HorizontalAlignment.Left)
            component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
        else if (hc == "1" && component.SelectedContent.HorizontalAlignment != HorizontalAlignment.Center)
            component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
        else if (hr == "1" && component.SelectedContent.HorizontalAlignment != HorizontalAlignment.Right)
            component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
        else if (!isHorizontal && component.SelectedContent.HorizontalAlignment != HorizontalAlignment.Stretch)
            component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;

        if (vt == "1" && component.SelectedContent.VerticalAlignment != VerticalAlignment.Top)
            component.SelectedContent.VerticalAlignment = VerticalAlignment.Top;
        else if (vc == "1" && component.SelectedContent.VerticalAlignment != VerticalAlignment.Center)
            component.SelectedContent.VerticalAlignment = VerticalAlignment.Center;
        else if (vb == "1" && component.SelectedContent.VerticalAlignment != VerticalAlignment.Bottom)
            component.SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
        else if (!isVertical && component.SelectedContent.VerticalAlignment != VerticalAlignment.Stretch)
            component.SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;

        if (width == SizeValue.Expand.ToString() && !double.IsNaN(component.SelectedContent.Width))
        {
            component.SelectedContent.Width = double.NaN;
            component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        else if (width == SizeValue.Auto.ToString() && !double.IsNaN(component.SelectedContent.Width))
            component.SelectedContent.Width = double.NaN;
        else if (width != SizeValue.Expand.ToString() && width != SizeValue.Auto.ToString() &&
                 (double.IsNaN(component.SelectedContent.Width) ||
                  Math.Abs(Convert.ToDouble(width) - component.SelectedContent.Width) != 0))
        {
            var vd = Helper.ConvertToDouble(width);
            component.SelectedContent.Width = vd;
        }

        if (height == SizeValue.Expand.ToString() && !double.IsNaN(component.SelectedContent.Height))
        {
            component.SelectedContent.Height = double.NaN;
            component.SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
        }
        else if (height == SizeValue.Auto.ToString() && !double.IsNaN(component.SelectedContent.Height))
            component.SelectedContent.Height = double.NaN;
        else if (height != SizeValue.Expand.ToString() && height != SizeValue.Auto.ToString() &&
                 (double.IsNaN(component.SelectedContent.Height) ||
                  Math.Abs(Convert.ToDouble(height) - component.SelectedContent.Height) != 0))
        {
            var vd = Helper.ConvertToDouble(height);
            component.SelectedContent.Height = vd;
        }
    }
}