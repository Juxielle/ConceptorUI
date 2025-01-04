using System.Windows;
using System.Windows.Controls;
using ConceptorUI.Models;
using ConceptorUi.ViewModels;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUI.ViewModels.Row;

static class ColumnRestoreProperties
{
    public static void RestoreProperties(this Component component, bool isHorizontal, bool isVertical, PropertyNames ah,
        PropertyNames av)
    {
        var ahc = SelfAlignment.Horizontal(component);
        var isChildHorizontal = SelfAlignment.IsHorizontal(component);

        var avc = SelfAlignment.Vertical(component);
        var isChildVertical = SelfAlignment.IsVertical(component);

        var heightChild = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var widthChild = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isVertical)
        {
            if (isChildVertical)
            {
                if (heightChild == SizeValue.Expand.ToString())
                {
                    SelfAlignment.SetVerticalValue(component, av, "1");
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Height,
                        SizeValue.Auto.ToString());
                }
                else if (av != avc)
                    SelfAlignment.SetVerticalValue(component, av, "1");
            }
            else
            {
                if (heightChild == SizeValue.Expand.ToString() && component.AllowExpanded())
                {
                    SelfAlignment.SetVerticalValue(component, av, "1");
                }
                else if (heightChild == SizeValue.Expand.ToString() && !component.AllowExpanded())
                {
                    SelfAlignment.SetVerticalValue(component, av, "1");
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Height,
                        SizeValue.Auto.ToString());
                }
                else SelfAlignment.SetVerticalValue(component, av, "1");
            }
        }
        else
        {
            if (isChildVertical)
            {
                if (heightChild == SizeValue.Expand.ToString() && component.AllowExpanded())
                {
                    SelfAlignment.SetVerticalOnNull(component);
                }
                else if (heightChild == SizeValue.Expand.ToString() && !component.AllowExpanded())
                {
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
                }
                else SelfAlignment.SetVerticalValue(component, avc, "1");
            }
            else
            {
                if (heightChild == SizeValue.Expand.ToString() && !component.AllowExpanded())
                {
                    SelfAlignment.SetVerticalValue(component, PropertyNames.Vt, "1");
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
                }
                else if (heightChild != SizeValue.Expand.ToString())
                {
                    SelfAlignment.SetVerticalValue(component, PropertyNames.Vt, "1");
                }
            }
        }

        if (isHorizontal)
        {
            var componentRowIndex = Grid.GetRow(component.ComponentView);
            if (componentRowIndex % 2 == 1)
            {
            }

            if (isChildHorizontal)
            {
                if (widthChild == SizeValue.Expand.ToString())
                {
                    SelfAlignment.SetHorizontalValue(component, ah, "1");
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                }
                else if (ah != ahc)
                    SelfAlignment.SetHorizontalValue(component, ah, "1");
            }
            else
            {
                if (widthChild == SizeValue.Expand.ToString() && component.AllowExpanded(false))
                {
                    Alignment.SetHorizontalOnNull(component.Parent);
                }
                else if (widthChild == SizeValue.Expand.ToString() && !component.AllowExpanded(false))
                {
                    SelfAlignment.SetHorizontalValue(component, ah, "1");
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                }
                else SelfAlignment.SetHorizontalValue(component, ah, "1");
            }
        }
        else
        {
            if (isChildHorizontal)
            {
                if (widthChild == SizeValue.Expand.ToString() && component.AllowExpanded())
                {
                    SelfAlignment.SetHorizontalOnNull(component);
                }
                else if (widthChild == SizeValue.Expand.ToString() && !component.AllowExpanded(false))
                {
                    Alignment.SetHorizontalValue(component.Parent, ahc, "1");
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                }
                else SelfAlignment.SetHorizontalValue(component, ahc, "1");
            }
            else
            {
                if (widthChild == SizeValue.Expand.ToString() && !component.AllowExpanded(false))
                {
                    Alignment.SetHorizontalValue(component.Parent, PropertyNames.Hl, "1");
                    SelfAlignment.SetHorizontalValue(component, PropertyNames.Hl, "1");
                    component.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
                }
                else if (widthChild != SizeValue.Expand.ToString())
                {
                    Alignment.SetHorizontalValue(component.Parent, PropertyNames.Hl, "1");
                    SelfAlignment.SetHorizontalValue(component, PropertyNames.Hl, "1");
                }
            }
        }
        
        var row = (RowModel)component.Parent;
        var nr = row.Grid.ColumnDefinitions.Count;
        
        for (var i = 0; i < nr; i++)
        {
            if (ah == PropertyNames.Hl)
            {
                if (i == nr - 1 && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Star)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                else if (row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Auto)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(0, GridUnitType.Auto);
            }
            else if (ah == PropertyNames.Hr)
            {
                if (i == 0 && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Star)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                else if (row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Auto)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(0, GridUnitType.Auto);
            }
            else if (ah == PropertyNames.Hc)
            {
                if (i == 0 && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Star)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                else if (i == nr - 1 && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Star)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                else if (row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Auto)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(0, GridUnitType.Auto);
            }
            else if (ah == PropertyNames.SpaceBetween)
            {
                if ((i == 0 || i == nr - 1) && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Auto)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(0, GridUnitType.Auto);
                else if (i % 2 == 0 && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Star)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
            }
            else if (ah == PropertyNames.SpaceAround)
            {
                if (i % 2 == 0 && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Star)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
            }
            else if (ah == PropertyNames.SpaceEvery)
            {
                if (i % 2 == 0 && row.Grid.ColumnDefinitions[i].Width.GridUnitType != GridUnitType.Star)
                    row.Grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
            }
        }
    }
}