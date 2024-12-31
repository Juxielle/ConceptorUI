using System.Windows.Controls;
using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;
using ConceptorUI.ViewModels.Row;

namespace ConceptorUi.ViewModels.Row;

static class RowRestoreProperties
{
    public static void RestoreProperties(this RowModel row)
    {
        var ah = Alignment.Horizontal(row);
        var isHorizontal = Alignment.IsHorizontal(row);

        var av = Alignment.Vertical(row);
        var isVertical = Alignment.IsVertical(row);

        Alignment.SetSeveralActivations(row);
        Alignment.SetInvalidValues(row);

        if (isHorizontal && row.Children.Count == 0)
        {
            Alignment.SetHorizontalOnNull(row);
        }

        if (isVertical && row.Children.Count == 0)
        {
            Alignment.SetVerticalOnNull(row);
        }

        foreach (var child in row.Children)
        {
            var ahc = SelfAlignment.Horizontal(child);
            var isChildHorizontal = SelfAlignment.IsHorizontal(child);

            var avc = SelfAlignment.Vertical(child);
            var isChildVertical = SelfAlignment.IsVertical(child);

            var heightChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            var widthChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

            if (isHorizontal && row.IsVertical)
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
                        SelfAlignment.SetHorizontalValue(child, ah, "1");
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
            else if (row.IsVertical)
            {
                if (isChildHorizontal)
                {
                    if (widthChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalOnNull(child);
                    }
                    else if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetHorizontalValue(child, ahc, "1");
                }
                else
                {
                    if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        SelfAlignment.SetHorizontalValue(child, PropertyNames.Hl, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else if (widthChild != SizeValue.Expand.ToString())
                    {
                        SelfAlignment.SetHorizontalValue(child, PropertyNames.Hl, "1");
                    }
                }
            }

            if (isVertical && row.IsVertical)
            {
                var childRowIndex = Grid.GetRow(child.ComponentView);
                if (childRowIndex % 2 == 1)
                {
                }

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
                        Alignment.SetVerticalOnNull(row);
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
            else if (row.IsVertical)
            {
                if (isChildVertical)
                {
                    if (heightChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                    {
                        SelfAlignment.SetVerticalOnNull(child);
                    }
                    else if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        Alignment.SetVerticalValue(row, avc, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else SelfAlignment.SetVerticalValue(child, avc, "1");
                }
                else
                {
                    if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                    {
                        Alignment.SetVerticalValue(row, PropertyNames.Hl, "1");
                        SelfAlignment.SetVerticalValue(child, PropertyNames.Hl, "1");
                        child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                            SizeValue.Auto.ToString());
                    }
                    else if (heightChild != SizeValue.Expand.ToString())
                    {
                        Alignment.SetVerticalValue(row, PropertyNames.Hl, "1");
                        SelfAlignment.SetVerticalValue(child, PropertyNames.Hl, "1");
                    }
                }
            }

            child.Synchronize();
        }

        if (isVertical && row.IsVertical)
        {
            var nr = row.Grid.RowDefinitions.Count;
            var nc = row.Children.Count;

            if (nr != 2 * nc + 1)
            {
            }
        }

        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(row);
        SelfAlignment.SetInvalidValues(row);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(row);
        var isSelfVertical = SelfAlignment.IsVertical(row);

        var height = row.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = row.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            if (width == SizeValue.Expand.ToString())
            {
                row.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
            }
        }

        if (isSelfVertical)
        {
            if (height == SizeValue.Expand.ToString())
            {
                row.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            }
        }

        row.Synchronize();
    }
}