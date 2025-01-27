using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.Stack;

static class StackRestoreProperties
{
    public static void RestoreProperties(this StackModel stack)
    {
        var ah = Alignment.Horizontal(stack);
        var isHorizontal = Alignment.IsHorizontal(stack);

        var av = Alignment.Vertical(stack);
        var isVertical = Alignment.IsVertical(stack);

        Alignment.SetSeveralActivations(stack);
        Alignment.SetInvalidValues(stack);

        if (isHorizontal && stack.Children.Count == 0)
        {
            Alignment.SetHorizontalOnNull(stack);
        }

        if (isVertical && stack.Children.Count == 0)
        {
            Alignment.SetVerticalOnNull(stack);
        }

        foreach (var child in stack.Children)
        {
            var ahc = SelfAlignment.Horizontal(child);
            var isChildHorizontal = SelfAlignment.IsHorizontal(child);

            var avc = SelfAlignment.Vertical(child);
            var isChildVertical = SelfAlignment.IsVertical(child);

            var heightChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            var widthChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

            if (isHorizontal && stack.IsVertical)
            {
                if (isChildHorizontal)
                {
                    if (widthChild == SizeValue.Expand.ToString())
                    {
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                    }
                    else if (ah != ahc)
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
                }
                else
                {
                    if (widthChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
                    }
                    else if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetHorizontalValue(child, ah, "1");
                }
            }
            else if (stack.IsVertical)
            {
                if (isChildHorizontal)
                {
                    if (widthChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalOnNull(child);
                    }
                    else if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetHorizontalValue(child, ahc, "1");
                }
                else
                {
                    if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalValue(child, PropertyNames.Hl, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                    }
                    else if (widthChild != SizeValue.Expand.ToString())
                    {
                        SelfAlignment.SetHorizontalValue(child, PropertyNames.Hl, "1");
                    }
                }
            }

            if (isVertical && !stack.IsVertical)
            {
                if (isChildVertical)
                {
                    if (heightChild == SizeValue.Expand.ToString())
                    {
                        SelfAlignment.SetVerticalValue(child, av, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
                    }
                    else if (av != avc)
                        SelfAlignment.SetVerticalValue(child, av, "1");
                }
                else
                {
                    if (heightChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetVerticalValue(child, av, "1");
                    }
                    else if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        SelfAlignment.SetVerticalValue(child, av, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetVerticalValue(child, av, "1");
                }
            }
            else if (!stack.IsVertical)
            {
                if (isChildHorizontal)
                {
                    if (heightChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetVerticalOnNull(child);
                    }
                    else if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetVerticalValue(child, avc, "1");
                }
                else
                {
                    if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        SelfAlignment.SetVerticalValue(child, PropertyNames.Vt, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
                    }
                    else if (heightChild != SizeValue.Expand.ToString())
                    {
                        SelfAlignment.SetVerticalValue(child, PropertyNames.Vt, "1");
                    }
                }
            }

            child.Synchronize();
        }

        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(stack);
        SelfAlignment.SetInvalidValues(stack);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(stack);
        var isSelfVertical = SelfAlignment.IsVertical(stack);

        var height = stack.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = stack.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            if (width == SizeValue.Expand.ToString())
            {
                stack.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
            }
        }
        
        if (isSelfVertical)
        {
            if (height == SizeValue.Expand.ToString())
            {
                stack.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            }
        }
        
        stack.Synchronize();
    }
}