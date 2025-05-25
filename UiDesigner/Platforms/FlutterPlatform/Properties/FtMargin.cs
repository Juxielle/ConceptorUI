using System;
using ConceptorUI.Models;
using UiDesigner.Models;
using UiDesigner.Utils;

namespace UiDesigner.Platforms.FlutterPlatform.Properties;

static class FtMargin
{
    private static string _left = null!;
    private static string _right = null!;
    private static string _top = null!;
    private static string _bottom = null!;

    private static string _all = null!;
    private static string _horizontal = null!;
    private static string _vertical = null!;

    private static bool _isHorizontal;
    private static bool _isVertical;
    
    public static void SetMargin(FtComponent component, CompSerializer compSerializer)
    {
        var marginEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CMargin);
        if (marginEnable == "1")
        {
            var margin = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.Margin);
            var marginValue = Helper.ConvertToDouble(margin);

            if (marginValue == 0) return;
            component.FtStyles.Add(new PlatformProperty { Key = "margin", Value = ToStringAll(component.Space) });
        }
        else
        {
            var marginLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginLeft);
            var marginRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginRight);
            var marginTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginTop);
            var marginBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginBottom);

            var marginLeftValue = Helper.ConvertToDouble(marginLeft);
            var marginRightValue = Helper.ConvertToDouble(marginRight);
            var marginTopValue = Helper.ConvertToDouble(marginTop);
            var marginBottomValue = Helper.ConvertToDouble(marginBottom);

            if (marginLeftValue == 0 && marginRightValue == 0 && marginTopValue == 0 && marginBottomValue == 0)
                return;
            if (marginLeftValue != 0 && Math.Abs(marginLeftValue - marginRightValue) == 0)
            {
                _isHorizontal = true;
                _horizontal = Helper.FormatString(marginLeft);
            }
            if (marginTopValue != 0 && Math.Abs(marginTopValue - marginBottomValue) == 0)
            {
                _isVertical = true;
                _vertical = Helper.FormatString(marginTop);
            }
            else if (Math.Abs(marginLeftValue - marginRightValue) == 0 &&
                     Math.Abs(marginLeftValue - marginTopValue) == 0 &&
                     Math.Abs(marginLeftValue - marginBottomValue) == 0)
            {
                _all = Helper.FormatString(marginLeft);
                component.FtStyles.Add(new PlatformProperty { Key = "margin", Value = ToStringAll(component.Space) });
            }
            else
            {
                _left = Helper.FormatString(marginLeft);
                _right = Helper.FormatString(marginRight);
                _top = Helper.FormatString(marginTop);
                _bottom = Helper.FormatString(marginBottom);
                component.FtStyles.Add(new PlatformProperty { Key = "margin", Value = ToStringCorner(component.Space) });
            }
            
            if(_isHorizontal || _isVertical)
                component.FtStyles.Add(new PlatformProperty { Key = "margin", Value = ToStringLateral(component.Space) });
        }
    }

    public static bool HaveMargin(FtComponent component, CompSerializer compSerializer)
    {
        var marginLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginLeft);
        var marginRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginRight);
        var marginTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginTop);
        var marginBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginBottom);

        var marginLeftValue = Helper.ConvertToDouble(marginLeft);
        var marginRightValue = Helper.ConvertToDouble(marginRight);
        var marginTopValue = Helper.ConvertToDouble(marginTop);
        var marginBottomValue = Helper.ConvertToDouble(marginBottom);

        return marginLeftValue == 0 || marginRightValue == 0 || marginTopValue == 0 || marginBottomValue == 0;
    }

    private static string ToStringCorner(string space)
    {
        return $"{space}EdgeInsets.only(" +
               $"left: {_left}, " +
               $"right: {_right}, " +
               $"top: {_top}, " +
               $"bottom: {_bottom}" +
               ")";
    }

    private static string ToStringLateral(string space)
    {
        return $"{space}EdgeInsets.symmetric(" +
               (_isHorizontal ? $"horizontal: {_horizontal}" : "") +
               (_isHorizontal && _isVertical ? ", " : "") +
               (_isVertical ? $"vertical: {_vertical}" : "") +
               ")";
    }

    private static string ToStringAll(string space)
    {
        return $"{space}EdgeInsets.all({_all})";
    }
}