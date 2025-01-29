using ConceptorUI.ViewModels.ListView;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.ListView;

static class ListViewRestoreProperties
{
    public static void RestoreProperties(this ListViewModel listView)
    {
        var ah = Alignment.Horizontal(listView);
        var isHorizontal = Alignment.IsHorizontal(listView);

        var av = Alignment.Vertical(listView);
        var isVertical = Alignment.IsVertical(listView);

        Alignment.SetSeveralActivations(listView);
        Alignment.SetInvalidValues(listView);

        if (isHorizontal && listView.Children.Count == 0)
        {
            Alignment.SetHorizontalOnNull(listView);
        }

        if (isVertical && listView.Children.Count == 0)
        {
            Alignment.SetVerticalOnNull(listView);
        }

        foreach (var child in listView.Children)
        {
            var ahc = SelfAlignment.Horizontal(child);
            var isChildHorizontal = SelfAlignment.IsHorizontal(child);

            var avc = SelfAlignment.Vertical(child);
            var isChildVertical = SelfAlignment.IsVertical(child);

            var heightChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            var widthChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

            if (isHorizontal && listView.IsVertical)
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
            else if (listView.IsVertical)
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

            if (isVertical && !listView.IsVertical)
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
            else if (!listView.IsVertical)
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
        SelfAlignment.SetSeveralActivations(listView);
        SelfAlignment.SetInvalidValues(listView);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(listView);
        var isSelfVertical = SelfAlignment.IsVertical(listView);

        var height = listView.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = listView.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            if (width == SizeValue.Expand.ToString())
            {
                listView.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
            }
        }
        
        if (isSelfVertical)
        {
            if (height == SizeValue.Expand.ToString())
            {
                listView.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            }
        }

        listView.Synchronize();
    }
}