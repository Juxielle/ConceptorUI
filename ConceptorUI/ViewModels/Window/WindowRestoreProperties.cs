using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;

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

        if (statusH != SizeValue.Expand.ToString() && statusH != SizeValue.Auto.ToString())
        {
            window.Statusbar.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "25");
        }

        window.Statusbar.Synchronize();
        
        /* Body */
        var bodyW = window.Body.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        var bodyH = window.Body.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var isBodyHorizontal = Alignment.IsHorizontal(window.Body);
        var isBodyVertical = Alignment.IsVertical(window.Body);

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

        if (layoutW != SizeValue.Expand.ToString())
        {
            window.Layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
        }

        if (layoutH != SizeValue.Expand.ToString())
        {
            window.Layout.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
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

        if (width != "310")
        {
            window.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "310");
        }

        if (height != "660")
        {
            window.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "660");
        }

        window.Synchronize();
    }
}