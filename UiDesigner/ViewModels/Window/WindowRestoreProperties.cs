using System.Windows;
using System.Windows.Controls;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace ConceptorUI.ViewModels.Window;

static class WindowRestoreProperties
{
    public static void RestoreProperties(this WindowModel window)
    {
        var isHorizontal = Alignment.IsHorizontal(window);

        var isVertical = Alignment.IsVertical(window);

        Alignment.SetSeveralActivations(window);
        Alignment.SetInvalidValues(window);

        if (isHorizontal)
        {
            Alignment.SetHorizontalOnNull(window);
        }

        if (isVertical)
        {
            Alignment.SetVerticalOnNull(window);
        }
        
        /* Status Bar */
        var statusW = window.Statusbar.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        var statusH = window.Statusbar.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var isStatusHorizontal = Alignment.IsHorizontal(window.Statusbar);
        var isStatusVertical = Alignment.IsVertical(window.Statusbar);

        Alignment.SetSeveralActivations(window.Statusbar);
        Alignment.SetInvalidValues(window.Statusbar);

        if (isStatusHorizontal)
        {
            Alignment.SetHorizontalOnNull(window.Statusbar);
        }

        if (isStatusVertical)
        {
            Alignment.SetVerticalOnNull(window.Statusbar);
        }

        SelfAlignment.SetSeveralActivations(window.Statusbar);
        SelfAlignment.SetInvalidValues(window.Statusbar);

        if (statusW != SizeValue.Expand.ToString())
        {
            window.Statusbar.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
        }

        if (statusH != $"{window.StatusHeight}")
        {
            window.Statusbar.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, $"{window.StatusHeight}");
        }

        window.Statusbar.Synchronize();
        
        /* Body */
        var bodyW = window.Body.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        var bodyH = window.Body.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var isBodyHorizontal = Alignment.IsHorizontal(window.Body);
        var isBodyVertical = Alignment.IsVertical(window.Body);
        var isBodySelfHorizontal = SelfAlignment.IsHorizontal(window.Body);
        var isBodySelfVertical = SelfAlignment.IsVertical(window.Body);

        Alignment.SetSeveralActivations(window.Body);
        Alignment.SetInvalidValues(window.Body);

        if (isBodyHorizontal)
        {
            Alignment.SetHorizontalOnNull(window.Body);
        }

        if (isBodyVertical)
        {
            Alignment.SetVerticalOnNull(window.Body);
        }

        SelfAlignment.SetSeveralActivations(window.Body);
        SelfAlignment.SetInvalidValues(window.Body);

        if (isBodySelfHorizontal)
        {
            SelfAlignment.SetHorizontalOnNull(window.Body);
        }

        if (isBodySelfVertical)
        {
            SelfAlignment.SetVerticalOnNull(window.Body);
        }

        if (bodyW != SizeValue.Expand.ToString())
        {
            window.Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
        }

        if (bodyH != SizeValue.Expand.ToString())
        {
            window.Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
        }

        window.Body.Synchronize();

        /* Layout */
        var layoutW = window.Layout.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        var layoutH = window.Layout.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var isLayoutHorizontal = Alignment.IsHorizontal(window.Layout);
        var isLayoutVertical = Alignment.IsVertical(window.Layout);
        var isLayoutSelfHorizontal = SelfAlignment.IsHorizontal(window.Layout);
        var isLayoutSelfVertical = SelfAlignment.IsVertical(window.Layout);

        Alignment.SetSeveralActivations(window.Layout);
        Alignment.SetInvalidValues(window.Layout);

        if (isLayoutHorizontal)
        {
            Alignment.SetHorizontalOnNull(window.Layout);
        }

        if (isLayoutVertical)
        {
            Alignment.SetVerticalOnNull(window.Layout);
        }

        SelfAlignment.SetSeveralActivations(window.Layout);
        SelfAlignment.SetInvalidValues(window.Layout);

        if (isLayoutSelfHorizontal)
        {
            SelfAlignment.SetHorizontalOnNull(window.Layout);
        }

        if (isLayoutSelfVertical)
        {
            SelfAlignment.SetVerticalOnNull(window.Layout);
        }

        if (layoutW != SizeValue.Expand.ToString())
        {
            window.Layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
        }

        if (layoutH != SizeValue.Expand.ToString())
        {
            window.Layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
        }

        if (window.Layout.Grid.RowDefinitions.Count >= 2 &&
            window.Layout.Grid.RowDefinitions[1].Height.GridUnitType != GridUnitType.Auto)
        {
            window.Layout.Grid.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Auto);
        }

        var rowIndex = Grid.GetRow(window.Body.ComponentView);
        if (window.Layout.Grid.RowDefinitions.Count >= rowIndex &&
            window.Layout.Grid.RowDefinitions[rowIndex].Height.GridUnitType == GridUnitType.Auto)
        {
            window.Layout.Grid.RowDefinitions[rowIndex].Height = new GridLength(1, GridUnitType.Star);
        }

        window.Layout.Synchronize();

        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(window);
        SelfAlignment.SetInvalidValues(window);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(window);
        var isSelfVertical = SelfAlignment.IsVertical(window);

        var height = window.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = window.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (!isSelfHorizontal)
        {
            SelfAlignment.SetHorizontalValue(window, PropertyNames.Hc, "1");
        }

        if (isSelfVertical)
        {
            SelfAlignment.SetVerticalOnNull(window);
        }

        if (width != $"{window.Width}")
        {
            window.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, $"{window.Width}");
        }

        if (height != $"{window.Height}")
        {
            window.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, $"{window.Height}");
        }

        window.Synchronize();
    }
}