using ConceptorUI.Models;
using ConceptorUI.ViewModels.Container;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUi.ViewModels.Container;

static class ContainerRestoreProperties
{
    public static void RestoreProperties(this ContainerModel container)
    {
        var ah = Alignment.Horizontal(container);
        var isHorizontal = Alignment.IsHorizontal(container);

        var av = Alignment.Vertical(container);
        var isVertical = Alignment.IsVertical(container);

        // Alignment.SetSeveralActivations(container);
        // Alignment.SetInvalidValues(container);

        if (isHorizontal && container.Children.Count == 0)
        {
            Alignment.SetHorizontalOnNull(container);
        }

        if (isVertical && container.Children.Count == 0)
        {
            Alignment.SetVerticalOnNull(container);
        }

        foreach (var child in container.Children)
        {
            var ahc = SelfAlignment.Horizontal(child);
            var isChildHorizontal = SelfAlignment.IsHorizontal(child);
        
            var avc = SelfAlignment.Vertical(child);
            var isChildVertical = SelfAlignment.IsVertical(child);
        
            var heightChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            var widthChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        
            if (isHorizontal)
            {
                if (isChildHorizontal)
                {
                    if (widthChild == SizeValue.Expand.ToString())
                    {
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else if (ah != ahc)
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
                }
                else
                {
                    if (widthChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        Alignment.SetHorizontalOnNull(container);
                    }
                    else if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetHorizontalValue(child, ah, "1");
                }
            }
            else
            {
                if (isChildHorizontal)
                {
                    if (widthChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalOnNull(child);
                    }
                    else if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        Alignment.SetHorizontalValue(container, ahc, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetHorizontalValue(child, ahc, "1");
                }
                else
                {
                    if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        Alignment.SetHorizontalValue(container, PropertyNames.Hl, "1");
                        SelfAlignment.SetHorizontalValue(child, PropertyNames.Hl, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else if (widthChild != SizeValue.Expand.ToString())
                    {
                        Alignment.SetHorizontalValue(container, PropertyNames.Hl, "1");
                        SelfAlignment.SetHorizontalValue(child, PropertyNames.Hl, "1");
                    }
                }
            }
            
            if (isVertical)
            {
                if (isChildVertical)
                {
                    if (heightChild == SizeValue.Expand.ToString())
                    {
                        SelfAlignment.SetVerticalValue(child, av, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height,
                            SizeValue.Auto.ToString());
                    }
                    else if (av != avc)
                        SelfAlignment.SetVerticalValue(child, av, "1");
                }
                else
                {
                    if (heightChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        Alignment.SetVerticalOnNull(container);
                    }
                    else if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        SelfAlignment.SetVerticalValue(child, av, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height,
                            SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetVerticalValue(child, av, "1");
                }
            }
            else
            {
                if (isChildVertical)
                {
                    if (heightChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetVerticalOnNull(child);
                    }
                    else if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        Alignment.SetVerticalValue(container, avc, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetVerticalValue(child, avc, "1");
                }
                else
                {
                    if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        Alignment.SetVerticalValue(container, PropertyNames.Hl, "1");
                        SelfAlignment.SetVerticalValue(child, PropertyNames.Hl, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else if (heightChild != SizeValue.Expand.ToString())
                    {
                        Alignment.SetVerticalValue(container, PropertyNames.Hl, "1");
                        SelfAlignment.SetVerticalValue(child, PropertyNames.Hl, "1");
                    }
                }
            }
        
            //child.Synchronize();
        }
        
        /* SelfAlignment */
        // SelfAlignment.SetSeveralActivations(container);
        // SelfAlignment.SetInvalidValues(container);
        // var height = container.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        // var width = container.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        //
        // if (isHorizontal)
        // {
        //     if (width == SizeValue.Expand.ToString())
        //     {
        //         container.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
        //     }
        // }
        // if (isVertical)
        // {
        //     if (height == SizeValue.Expand.ToString())
        //     {
        //         container.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
        //     }
        // }
        
        container.Synchronize();
    }
}