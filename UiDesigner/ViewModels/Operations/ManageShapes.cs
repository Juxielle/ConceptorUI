using System;
using System.Windows;
using ConceptorUI.Enums;
using ConceptorUI.Models;
using ConceptorUI.Utils;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUi.ViewModels.Operations;

static class ManageShapes
{
    public static void SetShape(this Component component, string value)
    {
        if (value == Shapes.Circular.ToString())
        {
            var width = component.SelectedContent.Width;
            var height = component.SelectedContent.Height;
            var actualWidth = component.SelectedContent.ActualWidth;
            var actualHeight = component.SelectedContent.ActualHeight;
            var selectedWidth = 0.0;
            var selectedHeight = 0.0;

            if (!double.IsNaN(width) && width != 0) selectedWidth = width;
            else if (!double.IsNaN(actualWidth) && actualWidth != 0) selectedWidth = actualWidth;

            if (!double.IsNaN(height) && height != 0) selectedHeight = height;
            else if (!double.IsNaN(actualHeight) && actualHeight != 0) selectedHeight = actualHeight;
            if (selectedWidth == 0 || selectedHeight == 0) return;

            var size = selectedWidth < selectedHeight ? selectedWidth : selectedHeight;
            component.SelectedContent.Width = size;
            component.SelectedContent.Height = size;
            component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(size);

            var hl = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl);
            var hc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc);
            var hr = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr);

            if (hl == "0" && hc == "0" && hr == "0")
                component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;

            var vt = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt);
            var vc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc);
            var vb = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb);

            if (vt == "0" && vc == "0" && vb == "0")
                component.SelectedContent.VerticalAlignment = VerticalAlignment.Center;
        }
        else if (value == Shapes.Ellipse.ToString())
        {
            var shape = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Shape);
            if (shape == Shapes.Circular.ToString())
                SetShape(component, Shapes.Rectangle.ToString());
            var width = component.SelectedContent.Width;
            var height = component.SelectedContent.Height;
            var actualWidth = component.SelectedContent.ActualWidth;
            var actualHeight = component.SelectedContent.ActualHeight;
            var selectedWidth = 0.0;
            var selectedHeight = 0.0;

            if (!double.IsNaN(width) && width != 0) selectedWidth = width;
            else if (!double.IsNaN(actualWidth) && actualWidth != 0) selectedWidth = actualWidth;

            if (!double.IsNaN(height) && height != 0) selectedHeight = height;
            else if (!double.IsNaN(actualHeight) && actualHeight != 0) selectedHeight = actualHeight;
            if (selectedWidth == 0 || selectedHeight == 0) return;

            var maxSize = selectedWidth > selectedHeight ? selectedWidth : selectedHeight;
            component.SelectedContent.Width = selectedWidth;
            component.SelectedContent.Height = selectedHeight;
            component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(maxSize);
        }
        else if (value == Shapes.Rectangle.ToString())
        {
            var width = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            var height = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            var shape = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Shape);
            var cBorderRadius = component.GetGroupProperties(GroupNames.Appearance)
                .GetValue(PropertyNames.CBorderRadius);

            if (shape == Shapes.Rectangle.ToString()) return;
            if (width == SizeValue.Auto.ToString() || width == SizeValue.Expand.ToString())
            {
                component.SelectedContent.Width = double.NaN;
            }
            else
            {
                var vd = Helper.ConvertToDouble(value);
                component.SelectedContent.Width = vd;
            }

            if (height == SizeValue.Auto.ToString() || height == SizeValue.Expand.ToString())
            {
                component.SelectedContent.Height = double.NaN;
            }
            else
            {
                var vd = Helper.ConvertToDouble(value);
                component.SelectedContent.Height = vd;
            }

            if (cBorderRadius == "1")
            {
                var radiusString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadius);
                var radius = Helper.ConvertToDouble(radiusString);
                component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(radius);
            }
            else
            {
                var radiusTlString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusTopLeft);
                var radiusTrString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusTopRight);
                var radiusBlString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusBottomLeft);
                var radiusBrString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusBottomRight);

                var radiusTl = Helper.ConvertToDouble(radiusTlString);
                var radiusTr = Helper.ConvertToDouble(radiusTrString);
                var radiusBl = Helper.ConvertToDouble(radiusBlString);
                var radiusBr = Helper.ConvertToDouble(radiusBrString);
                component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(
                    radiusTl,
                    radiusTr,
                    radiusBr,
                    radiusBl
                );
            }

            var hl = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl);
            var hc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc);
            var hr = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr);

            if (hl == "1") component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
            else if (hc == "1") component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
            else if (hr == "1") component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
            else component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;

            var vt = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt);
            var vc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc);
            var vb = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb);

            if (vt == "1") component.SelectedContent.VerticalAlignment = VerticalAlignment.Top;
            else if (vc == "1") component.SelectedContent.VerticalAlignment = VerticalAlignment.Center;
            else if (vb == "1") component.SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
            else component.SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
        }

        component.SetPropertyValue(GroupNames.Transform, PropertyNames.Shape, value);
    }
    
    public static void RestoreShape(this Component component)
    {
        var shape = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Shape);
        if (shape == Shapes.Circular.ToString())
        {
            var width = component.SelectedContent.Width;
            var height = component.SelectedContent.Height;
            var actualWidth = component.SelectedContent.ActualWidth;
            var actualHeight = component.SelectedContent.ActualHeight;
            double selectedWidth;
            double selectedHeight;
            var size = 0.0;

            if ((width == 0 || double.IsNaN(width)) || 
                (height == 0 || double.IsNaN(height)))
            {
                if ((double.IsNaN(actualWidth) || actualWidth == 0) ||
                    (double.IsNaN(actualHeight) || actualHeight == 0))
                {
                    //SetShape(component, Shapes.Rectangle.ToString());
                    return;
                }

                selectedWidth = actualWidth;
                selectedHeight = actualHeight;
                if (Math.Abs(selectedWidth - selectedHeight) != 0)
                {
                    size = selectedWidth < selectedHeight ? selectedWidth : selectedHeight;
                    component.SelectedContent.Width = size;
                    component.SelectedContent.Height = size;
                }
            }
            else if (Math.Abs(width - height) != 0)
            {
                if (width == 0 || height == 0)
                {
                    //SetShape(component, Shapes.Rectangle.ToString());
                    return;
                }

                size = width < height ? width : height;
                component.SelectedContent.Width = size;
                component.SelectedContent.Height = size;
            }

            var radius = component.Content.CornerRadius.BottomLeft;
            if (size != 0 && Math.Abs(radius - size) != 0)
                component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(size);
            if (component.SelectedContent.HorizontalAlignment == HorizontalAlignment.Stretch)
                component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
            if (component.SelectedContent.VerticalAlignment == VerticalAlignment.Stretch)
                component.SelectedContent.VerticalAlignment = VerticalAlignment.Center;
        }
        else if (shape == Shapes.Ellipse.ToString())
        {
            var width = component.SelectedContent.Width;
            var height = component.SelectedContent.Height;
            var actualWidth = component.SelectedContent.ActualWidth;
            var actualHeight = component.SelectedContent.ActualHeight;

            if (double.IsNaN(width))
            {
                if ((double.IsNaN(actualWidth) || actualWidth == 0) ||
                    (double.IsNaN(actualHeight) || actualHeight == 0))
                {
                    //SetShape(component, Shapes.Rectangle.ToString());
                    return;
                }

                component.SelectedContent.Width = actualWidth;
                component.SelectedContent.Height = actualHeight;
            }
            else if (double.IsNaN(height))
            {
                if ((double.IsNaN(actualWidth) || actualWidth == 0) ||
                    (double.IsNaN(actualHeight) || actualHeight == 0))
                {
                    //SetShape(component, Shapes.Rectangle.ToString());
                    return;
                }

                component.SelectedContent.Width = actualWidth;
                component.SelectedContent.Height = actualHeight;
            }
            else
            {
                if (width == 0 || height == 0)
                {
                    //SetShape(component, Shapes.Rectangle.ToString());
                    return;
                }

                component.SelectedContent.Width = width;
                component.SelectedContent.Height = height;
            }

            var size = actualWidth < actualHeight ? actualWidth : actualHeight;
            var radius = component.Content.CornerRadius.BottomLeft;
            if (size != 0 && Math.Abs(radius - size) != 0)
                component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(size);
        }
        else if (shape == Shapes.Rectangle.ToString())
        {
            var widthString = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            var heightString = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            var apparentWidth = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.ApparentWidth);
            var apparentHeight = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.ApparentHeight);
            var cBorderRadius = component.GetGroupProperties(GroupNames.Appearance).GetValue(PropertyNames.CBorderRadius);

            if ((widthString == SizeValue.Expand.ToString() || widthString == SizeValue.Auto.ToString()) &&
                !double.IsNaN(component.SelectedContent.Width) && apparentWidth == "0")
            {
                component.SelectedContent.Width = double.NaN;
            }
            else if(widthString != SizeValue.Expand.ToString() && widthString != SizeValue.Auto.ToString())
            {
                var width = Helper.ConvertToDouble(widthString);
                component.SelectedContent.Width = width;
            }

            if ((heightString == SizeValue.Expand.ToString() || heightString == SizeValue.Auto.ToString()) &&
                !double.IsNaN(component.SelectedContent.Height) && apparentHeight == "0")
            {
                component.SelectedContent.Height = double.NaN;
            }
            else if(heightString != SizeValue.Expand.ToString() && heightString != SizeValue.Auto.ToString())
            {
                var height = Helper.ConvertToDouble(heightString);
                component.SelectedContent.Height = height;
            }

            if (cBorderRadius == "1")
            {
                var radiusString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadius);
                var radius = Helper.ConvertToDouble(radiusString);
                if (Math.Abs(radius - component.Content.CornerRadius.BottomLeft) != 0)
                    component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(radius);
            }
            else
            {
                var radiusTlString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusTopLeft);
                var radiusTrString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusTopRight);
                var radiusBlString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusBottomLeft);
                var radiusBrString = component.GetGroupProperties(GroupNames.Appearance)
                    .GetValue(PropertyNames.BorderRadiusBottomRight);

                var radiusTl = Helper.ConvertToDouble(radiusTlString);
                var radiusTr = Helper.ConvertToDouble(radiusTrString);
                var radiusBl = Helper.ConvertToDouble(radiusBlString);
                var radiusBr = Helper.ConvertToDouble(radiusBrString);

                if (Math.Abs(radiusTl - component.Content.CornerRadius.TopLeft) != 0 ||
                    Math.Abs(radiusTr - component.Content.CornerRadius.TopRight) != 0 ||
                    Math.Abs(radiusBr - component.Content.CornerRadius.BottomRight) != 0 ||
                    Math.Abs(radiusBl - component.Content.CornerRadius.BottomLeft) != 0)
                    component.Content.CornerRadius = component.ShadowContent.CornerRadius = new CornerRadius(
                        radiusTl,
                        radiusTr,
                        radiusBr,
                        radiusBl
                    );
            }

            var hl = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl);
            var hc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc);
            var hr = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr);

            if ((hl == "0" && hc == "0" && hr == "0") &&
                component.SelectedContent.HorizontalAlignment != HorizontalAlignment.Stretch)
                component.SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;

            var vt = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt);
            var vc = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc);
            var vb = component.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb);

            if ((vt == "0" && vc == "0" && vb == "0") &&
                component.SelectedContent.VerticalAlignment != VerticalAlignment.Stretch)
                component.SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
        }
    }
}