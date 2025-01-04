using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;
using ConceptorUI.ViewModels.Row;

namespace ConceptorUi.ViewModels.Row;

static class RowRestoreProperties
{
    public static void RestoreProperties(this RowModel row)
    {
        var ah = row.Horizontal2();
        var isHorizontal = row.IsHorizontal2();

        var av = row.Vertical2();
        var isVertical = row.IsVertical2();

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
            if(row.IsVertical)
                LineRestoreProperties.RestoreProperties(child, isHorizontal, isVertical, ah, av);
            else ColumnRestoreProperties.RestoreProperties(child, isHorizontal, isVertical, ah, av);
            
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