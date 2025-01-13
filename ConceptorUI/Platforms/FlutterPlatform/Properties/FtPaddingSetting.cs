using System;
using ConceptorUI.Models;
using ConceptorUI.Utils;

namespace ConceptorUI.Platforms.FlutterPlatform.Properties;

public class FtPaddingSetting
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

    public static void SetPadding(FtComponent component, CompSerializer compSerializer)
    {
        var paddingEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CPadding);
        if (paddingEnable == "1")
        {
            var padding = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.Padding);
            var paddingValue = Helper.ConvertToDouble(padding);

            if (paddingValue == 0) return;
            component.FtStyles.Add(new PlatformProperty { Key = "padding", Value = ToStringAll(component.Space) });
        }
        else
        {
            var paddingLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingLeft);
            var paddingRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingRight);
            var paddingTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingTop);
            var paddingBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingBottom);

            var paddingLeftValue = Helper.ConvertToDouble(paddingLeft);
            var paddingRightValue = Helper.ConvertToDouble(paddingRight);
            var paddingTopValue = Helper.ConvertToDouble(paddingTop);
            var paddingBottomValue = Helper.ConvertToDouble(paddingBottom);

            if (paddingLeftValue == 0 && paddingRightValue == 0 && paddingTopValue == 0 && paddingBottomValue == 0)
                return;
            if (paddingLeftValue != 0 && Math.Abs(paddingLeftValue - paddingRightValue) == 0)
            {
                _isHorizontal = true;
                _horizontal = Helper.FormatString(paddingLeft);
            }

            if (paddingTopValue != 0 && Math.Abs(paddingTopValue - paddingBottomValue) == 0)
            {
                _isVertical = true;
                _vertical = Helper.FormatString(paddingTop);
            }
            else if (Math.Abs(paddingLeftValue - paddingRightValue) == 0 &&
                     Math.Abs(paddingLeftValue - paddingTopValue) == 0 &&
                     Math.Abs(paddingLeftValue - paddingBottomValue) == 0)
            {
                _all = Helper.FormatString(paddingLeft);
                component.FtStyles.Add(new PlatformProperty { Key = "padding", Value = ToStringAll(component.Space) });
            }
            else
            {
                _left = Helper.FormatString(paddingLeft);
                _right = Helper.FormatString(paddingRight);
                _top = Helper.FormatString(paddingTop);
                _bottom = Helper.FormatString(paddingBottom);
                component.FtStyles.Add(
                    new PlatformProperty { Key = "padding", Value = ToStringCorner(component.Space) });
            }

            if (_isHorizontal || _isVertical)
                component.FtStyles.Add(new PlatformProperty
                    { Key = "padding", Value = ToStringLateral(component.Space) });
        }
    }

    public static bool HavePadding(FtComponent component, CompSerializer compSerializer)
    {
        var paddingLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingLeft);
        var paddingRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingRight);
        var paddingTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingTop);
        var paddingBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingBottom);

        var paddingLeftValue = Helper.ConvertToDouble(paddingLeft);
        var paddingRightValue = Helper.ConvertToDouble(paddingRight);
        var paddingTopValue = Helper.ConvertToDouble(paddingTop);
        var paddingBottomValue = Helper.ConvertToDouble(paddingBottom);

        return paddingLeftValue != 0 || paddingRightValue != 0 || paddingTopValue != 0 || paddingBottomValue != 0;
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