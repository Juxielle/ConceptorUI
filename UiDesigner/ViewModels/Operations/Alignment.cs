using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;
using UiDesigner.ViewModels.Components;

namespace ConceptorUi.ViewModels.Operations;

static class Alignment
{
    public static PropertyNames Horizontal(this Component component)
    {
        var hl = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);

        return hl == "1" ? PropertyNames.Hl :
            hc == "1" ? PropertyNames.Hc :
            hr == "1" ? PropertyNames.Hr : PropertyNames.None;
    }

    public static PropertyNames Horizontal2(this Component component)
    {
        var hl = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);

        var sb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
        var sa = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
        var se = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

        return hl == "1" ? PropertyNames.Hl :
            hc == "1" ? PropertyNames.Hc :
            hr == "1" ? PropertyNames.Hr :
            sb == "1" && !component.IsVertical ? PropertyNames.SpaceBetween :
            sa == "1" && !component.IsVertical ? PropertyNames.SpaceAround:
            se == "1" && !component.IsVertical ? PropertyNames.SpaceEvery : PropertyNames.None;
    }

    public static bool IsHorizontal(this Component component)
    {
        var hl = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);

        return hl == "1" || hc == "1" || hr == "1";
    }

    public static bool IsHorizontal2(this Component component)
    {
        var hl = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);

        var sb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
        var sa = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
        var se = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

        return hl == "1" || hc == "1" || hr == "1" || ((sb == "1" || sa == "1" || se == "1") && !component.IsVertical);
    }

    public static PropertyNames Vertical(this Component component)
    {
        var vt = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);

        return vt == "1" ? PropertyNames.Vt :
            vc == "1" ? PropertyNames.Vc :
            vb == "1" ? PropertyNames.Vb : PropertyNames.None;
    }

    public static PropertyNames Vertical2(this Component component)
    {
        var vt = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);

        var sb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
        var sa = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
        var se = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

        return vt == "1" ? PropertyNames.Vt :
            vc == "1" ? PropertyNames.Vc :
            vb == "1" ? PropertyNames.Vb :
            sb == "1" && component.IsVertical ? PropertyNames.SpaceBetween :
            sa == "1" && component.IsVertical ? PropertyNames.SpaceAround:
            se == "1" && component.IsVertical ? PropertyNames.SpaceEvery : PropertyNames.None;
    }

    public static bool IsVertical(this Component component)
    {
        var vt = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);

        return vt == "1" || vc == "1" || vb == "1";
    }

    public static bool IsVertical2(this Component component)
    {
        var vt = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);

        var sb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
        var sa = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
        var se = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

        return vt == "1" || vc == "1" || vb == "1" || ((sb == "1" || sa == "1" || se == "1") && component.IsVertical);
    }

    public static void SetHorizontalValue(this Component component, PropertyNames name, string value)
    {
        SetHorizontalOnNull(component);
        component.SetPropertyValue(GroupNames.Alignment, name, value);
    }

    public static void SetVerticalValue(this Component component, PropertyNames name, string value)
    {
        SetVerticalOnNull(component);
        component.SetPropertyValue(GroupNames.Alignment, name, value);
    }

    public static void SetHorizontalOnNull(this Component component)
    {
        component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
        component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
        component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
    }

    public static void SetVerticalOnNull(this Component component)
    {
        component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
        component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
        component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
    }

    public static void SetSeveralActivations(this Component component)
    {
        var hl = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);

        var vt = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);

        var activationCount = 0;
        if (hl == "1") activationCount++;
        if (hc == "1") activationCount++;
        if (hr == "1") activationCount++;
        if (activationCount >= 2)
        {
            component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
        }

        activationCount = 0;
        if (vt == "1") activationCount++;
        if (vc == "1") activationCount++;
        if (vb == "1") activationCount++;
        if (activationCount >= 2)
        {
            component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
        }
    }

    public static void SetInvalidValues(this Component component)
    {
        var hl = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);

        var vt = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = component.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);

        if (hl != "0" && hl != "1") component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
        if (hc != "0" && hc != "1") component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
        if (hr != "0" && hr != "1") component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");

        if (vt != "0" && vt != "1") component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
        if (vc != "0" && vc != "1") component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
        if (vb != "0" && vb != "1") component.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
    }
}