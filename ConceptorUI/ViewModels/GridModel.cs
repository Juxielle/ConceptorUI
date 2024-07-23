using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Linq;




namespace ConceptorUI.ViewModels
{
    class GridModel : Component
    {
        public List<List<Component>> Rows { get; set; }
        public List<Component> MergedCells { get; set; }
        private Border SelectionBorder;
        private Border ParentBorder;
        private Grid grid;

        public GridModel(bool isConstraints = false)
        {
            SelectionBorder = new Border();
            ParentBorder = new Border();
            grid = new Grid();
            Rows = new List<List<Component>>();
            MergedCells = new List<Component>();

            ComponentView = SelectionBorder;
            SelectionBorder.Child = ParentBorder;
            ParentBorder.Child = grid;

            EnumName = ComponentList.Grid;
            ComponentView.PreviewMouseDown += new MouseButtonEventHandler(OnMouseDown);

            OnConfigured();
            LoadIds();
            if (!isConstraints) OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            SelectionBorder.BorderBrush = Brushes.DodgerBlue;
            SelectionBorder.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Grid;
            PanelProperty.Instance.FeedProps();
            Selected = true;
            return true;
        }

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            SelectionBorder.BorderBrush = Brushes.Transparent;
            SelectionBorder.BorderThickness = new Thickness(0);

            int[] pos = GetPosition(GroupNames.Global, PropertyNames.SelectedMode);
            int idG = pos[0], idP = pos[1], idGD = -1;
            if (Rows.Count > 0 && Rows[0].Count > 0)
            {
                int[] posD = Rows[0][0].GetPosition(GroupNames.GridProperty, PropertyNames.Row);
                idGD = posD[0];
            }

            foreach (var row in Rows)
            {
                foreach (var cell in row)
                    if (cell.groupProps![idGD].Properties[3].Value == "0" && (groupProps![idG].Properties[idP].Value == ESelectedMode.Single.ToString() || !isInterne))
                        cell.OnUnselected(isInterne);
            }
            foreach (var cell in MergedCells)
            {
                if (groupProps![idG].Properties[idP].Value == ESelectedMode.Single.ToString() || !isInterne)
                    cell.OnUnselected(isInterne);
            }
        }

        public override bool OnChildSelected()
        {
            var found = false;
            foreach (var row in Rows)
            {
                found = false;
                foreach (var cell in row)
                {
                    found = cell.OnChildSelected();
                    if (found) break;
                }
                if (found) break;
            }
            return Selected || found;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int[] pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            if (groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(SelectionBorder) ||
                e.OriginalSource.Equals(ParentBorder) || e.OriginalSource.Equals(grid)))
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
                PageView.Instance.OnSelected();
                PageView.Instance.RefreshStructuralView();
            }
        }

        public override void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            int[] pos = GetPosition(groupName, propertyName);
            int idG = pos[0], idP = pos[1];
            if (pos[0] != -1 && pos[1] != -1 && (Selected || allow))
            {
                int[] posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
                int idGAl = posAl[0];

                if (Selected || allow)
                {
                    /* Alignement */
                    #region
                    if (propertyName == PropertyNames.HL && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        SelectionBorder.HorizontalAlignment = value == "0" ? HorizontalAlignment.Stretch : HorizontalAlignment.Left;
                    }
                    else if (propertyName == PropertyNames.HC && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        SelectionBorder.HorizontalAlignment = value == "0" ? HorizontalAlignment.Stretch : HorizontalAlignment.Center;
                    }
                    else if (propertyName == PropertyNames.HR && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        SelectionBorder.HorizontalAlignment = value == "0" ? HorizontalAlignment.Stretch : HorizontalAlignment.Right;
                    }
                    else if (propertyName == PropertyNames.VT && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = "1";
                        SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                    }
                    else if (propertyName == PropertyNames.VC && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = "1";
                        SelectionBorder.VerticalAlignment = VerticalAlignment.Center;
                    }
                    else if (propertyName == PropertyNames.VB && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = "1";
                        SelectionBorder.VerticalAlignment = VerticalAlignment.Bottom;
                    }
                    else if (propertyName == PropertyNames.HL)
                    {
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                            {

                            }
                        }
                    }
                    else if (propertyName == PropertyNames.HC)
                    {
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                            {

                            }
                        }
                    }
                    else if (propertyName == PropertyNames.HR)
                    {
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                            {

                            }
                        }
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (propertyName == PropertyNames.Width)
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            SelectionBorder.Width = double.NaN; SelectionBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[0].Value = "0";
                            groupProps[idGAl].Properties[1].Value = "0";
                            groupProps[idGAl].Properties[2].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            SelectionBorder.Width = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[0].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            SelectionBorder.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.Height)
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            SelectionBorder.Height = double.NaN; SelectionBorder.VerticalAlignment = VerticalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[3].Value = "0";
                            groupProps[idGAl].Properties[4].Value = "0";
                            groupProps[idGAl].Properties[5].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            SelectionBorder.Height = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[3].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            SelectionBorder.Height = vd;
                            if (groupProps[idGAl].Properties[3].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.HE || propertyName == PropertyNames.VE)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.MoveParentToChild)
                    {
                        if (Rows.Count > 0 && Rows[0].Count > 0)
                        {
                            OnUnselected();
                            Rows[0][0].OnSelected();
                        }
                    }
                    else if (propertyName == PropertyNames.SelectedMode)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                        int[] posS = Rows[0][0].GetPosition(GroupNames.GridProperty, PropertyNames.SelectedMode);
                        int idGS = posS[0], idPS = posS[1];
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                            {
                                cell.groupProps![idGS].Properties[idPS].Value = value;
                            }
                        }
                        foreach (var cell in MergedCells)
                        {
                            cell.groupProps![idGS].Properties[idPS].Value = value;
                        }
                    }
                    else if (propertyName == PropertyNames.SelectedElement)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                        int[] posS = Rows[0][0].GetPosition(GroupNames.GridProperty, PropertyNames.SelectedElement);
                        int idGS = posS[0], idPS = posS[1];
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                            {
                                cell.groupProps![idGS].Properties[idPS].Value = value;
                            }
                        }
                        foreach (var cell in MergedCells)
                        {
                            cell.groupProps![idGS].Properties[idPS].Value = value;
                        }
                    }
                    #endregion
                    /* Text */
                    #region
                    #endregion
                    /* Appearance */
                    #region
                    else if (propertyName == PropertyNames.FillColor)
                    {
                        ParentBorder.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                           new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.CMargin)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.MarginBtnActif)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.Margin)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        SelectionBorder.Margin = new Thickness(vd);
                    }
                    else if (propertyName == PropertyNames.MarginLeft)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        SelectionBorder.Margin = new Thickness(vd, SelectionBorder.Margin.Top, SelectionBorder.Margin.Right, SelectionBorder.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginTop)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        SelectionBorder.Margin = new Thickness(SelectionBorder.Margin.Left, vd, SelectionBorder.Margin.Right, SelectionBorder.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginRight)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        SelectionBorder.Margin = new Thickness(SelectionBorder.Margin.Left, SelectionBorder.Margin.Top, vd, SelectionBorder.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginBottom)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        SelectionBorder.Margin = new Thickness(SelectionBorder.Margin.Left, SelectionBorder.Margin.Top, SelectionBorder.Margin.Right, vd);
                    }
                    else if (propertyName == PropertyNames.BorderWidth)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 6].Value =
                        groupProps[idG].Properties[idP + 9].Value = groupProps[idG].Properties[idP + 12].Value = vd + "";
                        ParentBorder.BorderThickness = new Thickness(vd);
                    }
                    else if (propertyName == PropertyNames.BorderColor)
                    {
                        ParentBorder.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                          new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.CBorderRadius)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.BorderRadBtnActif)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.BorderRadius)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        ParentBorder.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusTopLeft)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        ParentBorder.CornerRadius = new CornerRadius(vd, ParentBorder.CornerRadius.TopRight,
                                                      ParentBorder.CornerRadius.BottomRight, ParentBorder.CornerRadius.BottomLeft);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusBottomLeft)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        ParentBorder.CornerRadius = new CornerRadius(ParentBorder.CornerRadius.TopLeft, ParentBorder.CornerRadius.TopRight,
                                                      ParentBorder.CornerRadius.BottomRight, vd);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusTopRight)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        ParentBorder.CornerRadius = new CornerRadius(ParentBorder.CornerRadius.TopLeft, vd,
                                                      ParentBorder.CornerRadius.BottomRight, ParentBorder.CornerRadius.BottomLeft);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusBottomRight)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        ParentBorder.CornerRadius = new CornerRadius(ParentBorder.CornerRadius.TopLeft, ParentBorder.CornerRadius.TopRight,
                                                      vd, ParentBorder.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Global */
                    #region
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.HideBorder)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                            {
                                int i = Rows.IndexOf(row);
                                int j = row.IndexOf(cell);
                                cell.OnUpdated(groupName, propertyName, value, true);
                                if (j == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, value == "1" ? "0.4" : "0", true);
                                if (i == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, value == "1" ? "0.4" : "0", true);
                                cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, value == "1" ? "0.4" : "0", true);
                                cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, value == "1" ? "0.4" : "0", true);
                            }
                        }
                        foreach (var cell in MergedCells)
                        {
                            int[] pos2 = cell.GetPosition(GroupNames.GridProperty, PropertyNames.Row);
                            int idG2 = pos2[0], idP2 = pos2[1];
                            cell.OnUpdated(groupName, propertyName, value, true);
                            int i = Convert.ToInt32(cell.groupProps![idG2].Properties[idP2].Value);
                            int j = Convert.ToInt32(cell.groupProps![idG2].Properties[idP2 + 1].Value);
                            if (j == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, value == "1" ? "0.4" : "0", true);
                            if (i == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, value == "1" ? "0.4" : "0", true);
                            cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, value == "1" ? "0.4" : "0", true);
                            cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, value == "1" ? "0.4" : "0", true);
                        }
                    }
                    #endregion
                }
            }
            else
            {
                #region
                if (propertyName == PropertyNames.MoveChildToParent)
                {
                    foreach (var row in Rows)
                    {
                        foreach (var cell in row)
                        {
                            if (cell.Selected)
                            {
                                cell.OnUnselected();
                                OnSelected(); break;
                            }
                        }
                    }
                    foreach (var cell in MergedCells)
                    {
                        if (cell.Selected)
                        {
                            cell.OnUnselected();
                            OnSelected(); break;
                        }
                    }
                }
                else if (propertyName == PropertyNames.Add)
                {
                    bool found = false;
                    foreach (var row in Rows)
                        foreach (var cell in row)
                            if (cell.Selected) { found = true; break; }
                    foreach (var cell in MergedCells)
                        if (cell.Selected) { found = true; break; }
                    if (found)
                    {
                        int[] posA = GetPosition(GroupNames.GridProperty, PropertyNames.SelectedElement);
                        int idGA = posA[0], idPA = posA[1];
                        if (groupProps![idGA].Properties[idPA].Value == PropertyNames.Row.ToString())
                        {
                            AddRow();
                        }
                        else if (groupProps![idGA].Properties[idPA].Value == PropertyNames.Column.ToString())
                        {
                            AddColumn();
                        }
                    }
                }
                else if (propertyName == PropertyNames.SelectedMode)
                {
                    bool found = false;
                    foreach (var row in Rows)
                        foreach (var cell in row)
                            if (cell.Selected) { found = true; break; }
                    foreach (var cell in MergedCells)
                        if (cell.Selected) { found = true; break; }
                    if(found) groupProps![idG].Properties[idP].Value = value;
                }
                else if (propertyName == PropertyNames.SelectedElement)
                {
                    bool found = false;
                    foreach (var row in Rows)
                        foreach (var cell in row)
                            if (cell.Selected) { found = true; break; }
                    foreach (var cell in MergedCells)
                        if (cell.Selected) { found = true; break; }
                    if(found) groupProps![idG].Properties[idP].Value = value;
                }
                else if (propertyName == PropertyNames.HideBorder)
                {
                    bool found = false;
                    foreach (var row in Rows)
                        foreach (var cell in row)
                            if (cell.Selected) { found = true; break; }
                    foreach (var cell in MergedCells)
                        if (cell.Selected) { found = true; break; }
                    if (found)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                            {
                                int i = Rows.IndexOf(row);
                                int j = row.IndexOf(cell);
                                if (!cell.Selected) cell.OnUpdated(groupName, propertyName, value, true);
                                if (j == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, value == "1" ? "0.4" : "0", true);
                                if (i == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, value == "1" ? "0.4" : "0", true);
                                cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, value == "1" ? "0.4" : "0", true);
                                cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, value == "1" ? "0.4" : "0", true);
                            }
                        }
                        foreach (var cell in MergedCells)
                        {
                            int[] pos2 = cell.GetPosition(GroupNames.GridProperty, PropertyNames.Row);
                            int idG2 = pos2[0], idP2 = pos2[1];
                            if (!cell.Selected) cell.OnUpdated(groupName, propertyName, value, true);
                            int i = Convert.ToInt32(cell.groupProps![idG2].Properties[idP2].Value);
                            int j = Convert.ToInt32(cell.groupProps![idG2].Properties[idP2 + 1].Value);
                            if (j == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, value == "1" ? "0.4" : "0", true);
                            if (i == 0) cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, value == "1" ? "0.4" : "0", true);
                            cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, value == "1" ? "0.4" : "0", true);
                            cell.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, value == "1" ? "0.4" : "0", true);
                        }
                    }
                }
                else if (propertyName == PropertyNames.Merged)
                {
                    bool found = false;
                    foreach (var row in Rows)
                        foreach (var cell in row)
                            if (cell.Selected) { found = true; break; }
                    foreach (var cell in MergedCells)
                        if (cell.Selected) { found = true; break; }
                    if (found)
                    {
                        dynamic[] values = CanMerged();
                        if (values[0])
                        {
                            MergeNow(values[3], values[1], values[4] - values[3] + 1, values[2] - values[1] + 1);
                        }
                    }
                }
                else if (propertyName == PropertyNames.Width)
                {
                    var grid = (Grid)(ParentBorder).Child;
                    int i = -1, j = -1;

                    foreach (var row in Rows)
                        foreach (var cell in row)
                            if (cell.Selected) { i = Rows.IndexOf(row); j = row.IndexOf(cell); break; }

                    if (i != -1 && value == SizeValue.Expand.ToString())
                    {
                        grid.ColumnDefinitions[j].Width = new GridLength(1, GridUnitType.Star);
                        OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
                        if (i != 0)
                        {
                            Rows[i][0].OnUpdated(GroupNames.Transform, PropertyNames.Width, value, true);
                            value = SizeValue.Expand.ToString();
                        }
                    }
                    else if (i != -1 && value == SizeValue.Auto.ToString())
                    {
                        grid.ColumnDefinitions[j].Width = new GridLength(0, GridUnitType.Auto);
                        if (i != 0)
                        {
                            Rows[i][0].OnUpdated(GroupNames.Transform, PropertyNames.Width, value, true);
                            value = SizeValue.Expand.ToString();
                        }
                    }
                    else if (i != -1 && value != SizeValue.Old.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        grid.ColumnDefinitions[j].Width = new GridLength(vd, GridUnitType.Pixel);
                        if (i != 0)
                        {
                            Rows[0][j].OnUpdated(GroupNames.Transform, PropertyNames.Width, value, true);
                            value = SizeValue.Expand.ToString();
                        }
                    }

                    bool allColDefined = false;
                    foreach (var cd in grid.ColumnDefinitions)
                    {
                        allColDefined = false;
                        if (cd.Width.GridUnitType == GridUnitType.Pixel || cd.Width.GridUnitType == GridUnitType.Auto) allColDefined = true;
                        if (!allColDefined) break;
                    }
                    if (allColDefined)
                    {
                        if (Parent != null) Parent!.OnUpdated(GroupNames.Alignment, PropertyNames.HL, "1", true);
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    int i = -1, j = -1;
                    bool found = false;
                    foreach (var row in Rows)
                    {
                        foreach (var cell in row)
                            if (cell.Selected) { i = Rows.IndexOf(row); j = row.IndexOf(cell); found = true; break; }
                        if (found) break;
                    }
                    found = false;
                    double vd = 0; double.TryParse(value, out vd);
                    if (i != -1 && value == SizeValue.Expand.ToString())
                    {

                    }
                    else if (i != -1 && value == SizeValue.Auto.ToString())
                    {

                    }
                    else if (i != -1 && value != SizeValue.Old.ToString())
                    {
                        if (vd >= ManageEnums.CellHeight)
                        {
                            Rows[i][0].OnUpdated(GroupNames.Transform, PropertyNames.Height, value, true);
                        }
                        if (j != 0) value = SizeValue.Expand.ToString();
                        else if (vd < ManageEnums.CellHeight) value = $"{ManageEnums.CellHeight}";
                    }
                    else if (i == -1)
                    {
                        found = false;
                        foreach (var row in Rows)
                            foreach (var cell in row)
                            {
                                if (cell.Height(cell) < vd)
                                {
                                    int k = Rows.IndexOf(row); found = true;
                                    Rows[k][0].OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                                    break;
                                }
                                if (found) break;
                            }
                    }
                }
                else if (propertyName == PropertyNames.Text || propertyName == PropertyNames.FontFamily || propertyName == PropertyNames.FontSize)
                {
                    int i = -1, j = -1;
                    foreach (var row in Rows)
                        foreach (var cell in row)
                            if (cell.Selected) { i = Rows.IndexOf(row); j = row.IndexOf(cell); break; }
                    if (i == -1)
                    {
                        bool found = false;
                        foreach (var row in Rows)
                            foreach (var cell in row)
                            {
                                if ((cell as ContainerModel)!.Child != null && cell.Height(cell) < (cell as ContainerModel)!.Child.Height(cell) && cell.OnChildSelected())
                                {
                                    int k = Rows.IndexOf(row); found = true;
                                    Rows[k][0].OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                                    break;
                                }
                                if (found) break;
                            }
                    }
                }
                else if (propertyName == PropertyNames.MoveTop)
                {

                }
                else if (propertyName == PropertyNames.MoveBottom)
                {

                }
                foreach (var row in Rows)
                    foreach (var cell in row)
                        cell.OnUpdated(groupName, propertyName, value, propertyName == PropertyNames.SelectedElement || propertyName == PropertyNames.SelectedMode);

                foreach (var cell in MergedCells)
                    cell.OnUpdated(groupName, propertyName, value, propertyName == PropertyNames.SelectedElement || propertyName == PropertyNames.SelectedMode);


                if (propertyName == PropertyNames.Trash)
                {
                    int i = -1, j = -1;
                    bool found = false;
                    foreach (var row in Rows)
                    {
                        foreach (var cell in row)
                        {
                            if (cell.Selected)
                            {
                                i = Rows.IndexOf(row);
                                j = row.IndexOf(cell);
                                found = true;
                                break;
                            }
                        }
                        if (found) break;
                    }
                    found = false;
                    foreach (var cell in MergedCells)
                    {
                        if (cell.Selected)
                        {

                        }
                    }
                    if (i != -1 && j != -1)
                    {
                        string se = groupProps![Ids![GroupNames.GridProperty.ToString()].IdGroup].Properties[Ids![GroupNames.GridProperty.ToString()].Props![PropertyNames.SelectedElement.ToString()]].Value;
                        if (se == ESelectedElement.Row.ToString())
                        {
                            var cells = grid.Children
                                            .Cast<FrameworkElement>()
                                            .Where(e => Grid.GetRow(e) == i).ToList();
                            for (int k = 0; k < cells.Count; k++)
                            {
                                grid.Children.Remove(cells[k]);
                            }
                            if (i < grid.RowDefinitions.Count - 1)
                            {
                                foreach (var row in Rows)
                                {
                                    foreach (var cell in row)
                                    {
                                        string r = cell.groupProps![cell.Ids![GroupNames.GridProperty.ToString()].IdGroup].Properties[cell.Ids![GroupNames.GridProperty.ToString()].Props![PropertyNames.Row.ToString()]].Value;
                                        int k = Convert.ToInt32(r);
                                        if (k > i)
                                        {
                                            cell.groupProps![cell.Ids![GroupNames.GridProperty.ToString()].IdGroup].Properties[cell.Ids![GroupNames.GridProperty.ToString()].Props![PropertyNames.Row.ToString()]].Value = $"{k - 1}";
                                            Grid.SetRow(cell.ComponentView, k - 1);
                                        }
                                    }
                                }
                            }
                            Rows.RemoveAt(i);
                            grid.RowDefinitions.RemoveAt(i);
                        }
                        else if (se == ESelectedElement.Column.ToString())
                        {

                        }
                    }
                    else if (i == -1)
                    {
                        foreach (var row in Rows)
                        {
                            foreach (var cell in row)
                                if (cell.Selected) { i = Rows.IndexOf(row); j = row.IndexOf(cell); found = true; break; }
                            if (found) break;
                        }
                        if (i != -1)
                        {
                            found = true;
                            foreach (var cell in Rows[i])
                            {
                                if (cell.Height(cell) > ManageEnums.CellHeight) found = false;
                            }
                            if (found)
                            {
                                Rows[i][0].OnUpdated(GroupNames.Transform, PropertyNames.Height, ManageEnums.CellHeight.ToString(), true);
                            }
                        }
                    }
                }
                #endregion
            }
        }

        public override void OnInitialized()
        {
            int[] posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
            int idGAl = posAl[0];
            foreach (var group in groupProps!)
            {
                foreach (var prop in group.Properties)
                {
                    /* Alignement */
                    #region
                    if (prop.Name == PropertyNames.HL.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            SelectionBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.HC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Center;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            SelectionBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.HR.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Right;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            SelectionBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            SelectionBorder.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Center;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            SelectionBorder.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if(prop.Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Bottom;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            SelectionBorder.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (prop.Name == PropertyNames.Width.ToString())
                    {
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            SelectionBorder.Width = double.NaN; SelectionBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString())
                        {
                            SelectionBorder.Width = double.NaN;
                            if (groupProps[idGAl].Properties[0].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Right;
                            else SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, out vd);
                            SelectionBorder.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") SelectionBorder.HorizontalAlignment = HorizontalAlignment.Right;
                            else SelectionBorder.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }
                    else if (prop.Name == PropertyNames.Height.ToString())
                    {
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            SelectionBorder.Height = double.NaN; SelectionBorder.VerticalAlignment = VerticalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString())
                        {
                            SelectionBorder.Height = double.NaN;
                            if (groupProps[idGAl].Properties[3].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Bottom;
                            else SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, out vd);
                            SelectionBorder.Height = vd;
                            if (groupProps[idGAl].Properties[3].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") SelectionBorder.VerticalAlignment = VerticalAlignment.Bottom;
                            else SelectionBorder.VerticalAlignment = VerticalAlignment.Top;
                        }
                    }
                    #endregion
                    /* Text */
                    #region
                    #endregion
                    /* Appearance */
                    #region
                    else if (prop.Name == PropertyNames.FillColor.ToString())
                    {
                        ParentBorder.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                                new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.Margin.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        SelectionBorder.Margin = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.MarginLeft.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        SelectionBorder.Margin = new Thickness(vd, SelectionBorder.Margin.Top, SelectionBorder.Margin.Right, SelectionBorder.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginTop.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        SelectionBorder.Margin = new Thickness(SelectionBorder.Margin.Left, vd, SelectionBorder.Margin.Right, SelectionBorder.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginRight.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        SelectionBorder.Margin = new Thickness(SelectionBorder.Margin.Left, SelectionBorder.Margin.Top, vd, SelectionBorder.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginBottom.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        SelectionBorder.Margin = new Thickness(SelectionBorder.Margin.Left, SelectionBorder.Margin.Top, SelectionBorder.Margin.Right, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderWidth.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        ParentBorder.BorderThickness = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderColor.ToString())
                    {
                        ParentBorder.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                                new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.BorderRadius.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        ParentBorder.CornerRadius = new CornerRadius(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        ParentBorder.CornerRadius = new CornerRadius(vd, ParentBorder.CornerRadius.TopRight,
                                                        ParentBorder.CornerRadius.BottomRight, ParentBorder.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        ParentBorder.CornerRadius = new CornerRadius(ParentBorder.CornerRadius.TopLeft, ParentBorder.CornerRadius.TopRight,
                                                        ParentBorder.CornerRadius.BottomRight, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        ParentBorder.CornerRadius = new CornerRadius(ParentBorder.CornerRadius.TopLeft, vd,
                                                        ParentBorder.CornerRadius.BottomRight, ParentBorder.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        ParentBorder.CornerRadius = new CornerRadius(ParentBorder.CornerRadius.TopLeft, ParentBorder.CornerRadius.TopRight,
                                                        vd, ParentBorder.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Grid Properties */
                    #region
                    #endregion
                }
            }
        }

        public override CompSerialiser OnSerialiser()
        {
            var cells = new List<CompSerialiser>();
            foreach(var row in Rows)
                foreach (var cell in row)
                    cells.Add(cell.OnSerialiser());

            var mcs = new List<CompSerialiser>();
            foreach (var cell in MergedCells)
                mcs.Add(cell.OnSerialiser());
            return new CompSerialiser
            {
                Name = EnumName.ToString(),
                Properties = groupProps,
                Children = new List<CompSerialiser> {
                    new CompSerialiser
                    {
                        Name = "Cells",
                        Children = Rows.Count > 0 ? cells : null,
                    },
                    new CompSerialiser
                    {
                        Name = "MergedCells",
                        Children = MergedCells.Count > 0 ? mcs : null,
                    },
                }
            };
        }

        public override void OnDeserialiser(CompSerialiser compSerialiser)
        {
            groupProps = compSerialiser.Properties;
            string hideValue = groupProps![Ids![GroupNames.GridProperty.ToString()].IdGroup].Properties[Ids![GroupNames.GridProperty.ToString()].Props![PropertyNames.HideBorder.ToString()]].Value;
            #region
            if (compSerialiser.Children![0].Children != null)
            {
                foreach (var cell in compSerialiser.Children[0].Children!)
                {
                    Component component = ManageEnums.Instance.GetComponent(cell.Name!);
                    component.Parent = this; component.Name = ComponentList.Cell.ToString(); component.Added = true;
                    component.OnDeserialiser(cell);
                    int[] pos = component.GetPosition(GroupNames.GridProperty, PropertyNames.Row);
                    int idG = pos[0], idP = pos[1];
                    int i = Convert.ToInt32(component.groupProps![idG].Properties[idP].Value);
                    int j = Convert.ToInt32(component.groupProps![idG].Properties[idP + 1].Value);
                    if (j == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, hideValue == "1" ? "0.4" : "0", true);
                    if (i == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, hideValue == "1" ? "0.4" : "0", true);
                    component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, hideValue == "1" ? "0.4" : "0", true);
                    component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, hideValue == "1" ? "0.4" : "0", true);

                    string merged = component.groupProps![component.Ids![GroupNames.GridProperty.ToString()].IdGroup].Properties[component.Ids![GroupNames.GridProperty.ToString()].Props![PropertyNames.Merged.ToString()]].Value;
                    if (merged == "1") component.ComponentView!.Visibility = Visibility.Hidden;

                    if (grid.RowDefinitions.Count - 1 < i)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                        Rows.Add(new List<Component>());
                    }
                    if (grid.ColumnDefinitions.Count - 1 < j)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    }

                    Grid.SetRow(component.ComponentView, i); Grid.SetColumn(component.ComponentView, j);
                    grid.Children.Add(component.ComponentView); Rows[i].Add(component);
                }
            }
            if (compSerialiser.Children![1].Children != null)
            {
                foreach (var cell in compSerialiser.Children[1].Children!)
                {
                    Component component = ManageEnums.Instance.GetComponent(cell.Name!);
                    component.Parent = this; component.Name = ComponentList.Cell.ToString(); component.Added = true;
                    component.OnDeserialiser(cell);
                    int[] pos = component.GetPosition(GroupNames.GridProperty, PropertyNames.Row);
                    int[] pos2 = component.GetPosition(GroupNames.GridProperty, PropertyNames.ColumnSpan);
                    int idG = pos[0], idP = pos[1];
                    int i = Convert.ToInt32(component.groupProps![idG].Properties[idP].Value);
                    int j = Convert.ToInt32(component.groupProps![idG].Properties[idP + 1].Value);
                    int columnSpan = Convert.ToInt32(component.groupProps![pos2[0]].Properties[pos2[1]].Value);
                    int rowSpan = Convert.ToInt32(component.groupProps![pos2[0]].Properties[pos2[1] + 1].Value);
                    if (j == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, hideValue == "1" ? "0.4" : "0", true);
                    if (i == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, hideValue == "1" ? "0.4" : "0", true);
                    component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, hideValue == "1" ? "0.4" : "0", true);
                    component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, hideValue == "1" ? "0.4" : "0", true);

                    Grid.SetRow(component.ComponentView, i); Grid.SetColumn(component.ComponentView, j);
                    Grid.SetRowSpan(component.ComponentView, rowSpan); Grid.SetColumnSpan(component.ComponentView, columnSpan);
                    grid.Children.Add(component.ComponentView); MergedCells.Add(component);
                }
            }
            #endregion
            OnInitialized();
        }

        public override void OnAddConfig(GroupNames groupName, PropertyNames propertyName, string value, bool isGroup = true)
        {
            int[] pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1)
            {
                int idG = pos[0], idP = pos[1];
                if (isGroup)
                {
                    groupProps![idG].Visibility = value;
                    if (value == VisibilityValue.Collapsed.ToString())
                        for (int i = 0; i < groupProps[idG].Properties.Count; i++)
                            groupProps[idG].Properties[i].Visibility = value;
                }
                else
                {
                    groupProps![idG].Properties[idP].Visibility = value;
                }
            }
        }

        public override void OnConfiguOut(int id)
        {
            if (Selected)
            {
                if (Properties.ComponentOutsUsing![id].Name == ComponentList.Grid.ToString() && Rows.Count == 0)
                {
                    foreach (var group in Properties.ComponentOutsUsing[id].Component)
                    {
                        int i = Properties.ComponentOutsUsing[id].Component.IndexOf(group);
                        groupProps![i].Visibility = group.Visibility;
                        foreach (var prop in group.Properties)
                        {
                            int j = group.Properties.IndexOf(prop);
                            groupProps[i].Properties[j].Visibility = prop.Visibility;
                            groupProps[i].Properties[j].Value = prop.Value;
                        }
                    }
                }
                #region
                foreach (var child in Properties.ComponentOutsUsing)
                {
                    if (child.ParentId == id && child.Name != ComponentList.Grid.ToString())
                    {
                        Component component = new ContainerModel(true); component.Selected = true;
                        component.OnConfiguOut(Properties.ComponentOutsUsing.IndexOf(child)); component.Selected = false;
                        component.Parent = this; component.Name = ComponentList.Cell.ToString();

                        int[] pos = GetPosition(GroupNames.GridProperty, PropertyNames.Row);
                        int i = Convert.ToInt32(component.groupProps![pos[0]].Properties[0].Value);
                        int j = Convert.ToInt32(component.groupProps![pos[0]].Properties[1].Value);

                        //Restrictions actuelles de columns
                        component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString());
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Height, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveParentToChild, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveChildToParent, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveLeft, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveRight, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveTop, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveBottom, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.HE, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderWidth, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderRadius, VisibilityValue.Collapsed.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.Margin, VisibilityValue.Collapsed.ToString(), false);
                        component.OnAddConfig(GroupNames.GridProperty, PropertyNames.Row, VisibilityValue.Visible.ToString());
                        component.OnInitialized();
                        component.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
                        component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString(), true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "10", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.CBorder, "0", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderColor, "#FF000000", true);
                        if (j == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, "0.4", true);
                        if (i == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, "0.4", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, "0.4", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, "0.4", true);
                        component.OnUpdated(GroupNames.GridProperty, PropertyNames.SelectedElement, ESelectedElement.Cell.ToString(), true);
                        component.OnUpdated(GroupNames.GridProperty, PropertyNames.Row, i.ToString(), true);
                        component.OnUpdated(GroupNames.GridProperty, PropertyNames.Column, j.ToString(), true);
                        component.OnUpdated(GroupNames.Global, PropertyNames.SelectedMode, ESelectedMode.Single.ToString(), true);

                        if (grid.RowDefinitions.Count - 1 < i)
                        {
                            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                            Rows.Add(new List<Component>());
                        }
                        if (grid.ColumnDefinitions.Count - 1 < j)
                        {
                            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                        }

                        Grid.SetRow(component.ComponentView, i); Grid.SetColumn(component.ComponentView, j);
                        grid.Children.Add(component.ComponentView); Rows[i].Add(component);
                    }
                }
                #endregion
            }
            else
            {
                int idG = 0, idP = 0;
                if (Rows.Count > 0 && Rows[0].Count > 0)
                {
                    int[] pos = Rows[0][0].GetPosition(GroupNames.Appearance, PropertyNames.Padding);
                    idG = pos[0]; idP = pos[1];
                }
                foreach (var row in Rows)
                {
                    bool found = false;
                    foreach (var cell in row)
                    {
                        cell.OnConfiguOut(id);
                        if (cell.Selected && ((ContainerModel)cell).Child != null && cell.groupProps![idG].Properties[idP].Value != "0") found = true;
                    }
                    if (found) //Faire ça egalement lors de l'ajout des nouvelles colonnes.
                        foreach (var cell in row)
                            cell.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "0", true);
                }
                foreach (var cell in MergedCells)
                {
                    cell.OnConfiguOut(id);
                    if (cell.Selected && ((ContainerModel)cell).Child != null && cell.groupProps![idG].Properties[idP].Value != "0")
                        cell.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "0", true);
                }
            }
        }

        private void AddRow()
        {
            int i = Rows.Count;
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Rows.Add(new List<Component>());

            int[] pos = GetPosition(GroupNames.Global, PropertyNames.SelectedMode);
            int idG = pos[0], idP = pos[1];
            #region
            foreach (var cell in Rows[0])
            {
                int j = Rows[0].IndexOf(cell);
                Component component = new ContainerModel(true); component.Added = true;
                component.Parent = this; component.Name = ComponentList.Cell.ToString();

                //Restrictions actuelles de columns
                component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Height, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveParentToChild, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveChildToParent, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveLeft, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveRight, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveTop, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveBottom, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.HE, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderWidth, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderRadius, VisibilityValue.Collapsed.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.Margin, VisibilityValue.Collapsed.ToString(), false);
                component.OnAddConfig(GroupNames.GridProperty, PropertyNames.Row, VisibilityValue.Visible.ToString());
                component.OnInitialized();
                component.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
                component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString(), true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "10", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.CBorder, "0", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderColor, "#FF000000", true);
                if (j == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, "0.4", true);
                if (i == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, "0.4", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, "0.4", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, "0.4", true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.SelectedElement, ESelectedElement.Row.ToString(), true);
                component.OnUpdated(GroupNames.Global, PropertyNames.SelectedMode, groupProps![idG].Properties[idP].Value, true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.Row, i.ToString(), true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.Column, j.ToString(), true);

                Grid.SetRow(component.ComponentView, i); Grid.SetColumn(component.ComponentView, j);
                grid.Children.Add(component.ComponentView); Rows[i].Add(component);
            }
            #endregion
        }

        private void AddColumn()
        {
            int j = Rows[0].Count;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            int[] pos = GetPosition(GroupNames.Global, PropertyNames.SelectedMode);
            int idG = pos[0], idP = pos[1];
            #region
            foreach (var row in Rows)
            {
                int i = Rows.IndexOf(row);
                Component component = new ContainerModel(true); component.Added = true;
                component.Parent = this; component.Name = ComponentList.Cell.ToString();

                //Restrictions actuelles de columns
                component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Height, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveParentToChild, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveChildToParent, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveLeft, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveRight, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveTop, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveBottom, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.HE, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderWidth, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderRadius, VisibilityValue.Collapsed.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.Margin, VisibilityValue.Collapsed.ToString(), false);
                component.OnAddConfig(GroupNames.GridProperty, PropertyNames.Row, VisibilityValue.Visible.ToString());
                component.OnInitialized();
                component.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
                component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString(), true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "10", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.CBorder, "0", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderColor, "#FF000000", true);
                if (j == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, "0.4", true);
                if (i == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, "0.4", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, "0.4", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, "0.4", true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.SelectedElement, ESelectedElement.Column.ToString(), true);
                component.OnUpdated(GroupNames.Global, PropertyNames.SelectedMode, groupProps![idG].Properties[idP].Value, true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.Row, i.ToString(), true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.Column, j.ToString(), true);

                Grid.SetRow(component.ComponentView, i); Grid.SetColumn(component.ComponentView, j);
                grid.Children.Add(component.ComponentView); Rows[i].Add(component);

                bool found = false;
                foreach (var cell in Rows[i])
                    if (((ContainerModel)cell).Child != null && cell.groupProps![idG].Properties[idP].Value != "0") { found = true; break; }

                if (found)
                    component.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "0", true);
            }
            #endregion
        }

        public void AddRowColumn(int nrow, int ncol)
        {
            if (Rows.Count == 0)
            {
                for (int i = 0; i < nrow; i++)
                {
                    for (int j = 0; j < ncol; j++)
                    {
                        Component component = new ContainerModel(true); component.Added = true;
                        component.Parent = this; component.Name = ComponentList.Cell.ToString();

                        //Restrictions actuelles de columns
                        #region
                        component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
                        component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString());
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.Height, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveParentToChild, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveChildToParent, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveLeft, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveRight, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveTop, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveBottom, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Transform, PropertyNames.HE, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderWidth, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderColor, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderLeftWidth, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderBottomWidth, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderTopWidth, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderRightWidth, VisibilityValue.Visible.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderRadius, VisibilityValue.Collapsed.ToString(), false);
                        component.OnAddConfig(GroupNames.Appearance, PropertyNames.Margin, VisibilityValue.Collapsed.ToString(), false);
                        component.OnAddConfig(GroupNames.GridProperty, PropertyNames.Row, VisibilityValue.Visible.ToString());
                        component.OnInitialized();
                        component.OnUpdated(GroupNames.Alignment, PropertyNames.VC, "1", true);
                        component.OnUpdated(GroupNames.Transform, PropertyNames.Width, i == 0 && j == 0 ? SizeValue.Auto.ToString() : (i == 0 ? "200" : SizeValue.Expand.ToString()), true);
                        component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString(), true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.CPadding, "0", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.PaddingLeft, i == 0 && j == 0 ? "10" : "0", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.PaddingRight, i == 0 && j == 0 ? "10" : "0", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "10", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.CBorder, "0", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderColor, "#FF000000", true);
                        if (j == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, "0.4", true);
                        if (i == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, "0.4", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, "0.4", true);
                        component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, "0.4", true);
                        component.OnUpdated(GroupNames.GridProperty, PropertyNames.SelectedElement, ESelectedElement.Cell.ToString(), true);
                        component.OnUpdated(GroupNames.Global, PropertyNames.SelectedMode, ESelectedMode.Single.ToString(), true);
                        component.OnUpdated(GroupNames.GridProperty, PropertyNames.Row, i.ToString(), true);
                        component.OnUpdated(GroupNames.GridProperty, PropertyNames.Column, j.ToString(), true);
                        #endregion
                        if (grid.RowDefinitions.Count - 1 < i)
                        {
                            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                            Rows.Add(new List<Component>());
                        }
                        if (grid.ColumnDefinitions.Count - 1 < j)
                        {
                            grid.ColumnDefinitions.Add(
                                new ColumnDefinition()
                                {
                                    Width = new GridLength(i == 0 && j == 0 ? 0 : (i == 0 ? 200 : 1), i == 0 && j == 0 ? GridUnitType.Auto : (i == 0 ? GridUnitType.Pixel : GridUnitType.Star))
                                });
                        }

                        Grid.SetRow(component.ComponentView, i); Grid.SetColumn(component.ComponentView, j);
                        grid.Children.Add(component.ComponentView); Rows[i].Add(component);
                    }
                }
            }
        }

        private dynamic[] CanMerged()
        {
            int iprec = -1, jprec = -1;
            int startcol = -1, endcol = -1;
            int startrow = -1, endrow = -1;
            dynamic[] merged = { false, startcol, endcol, startrow, endrow };
            bool firstRow = false;
            bool trouVerti = false;
            #region
            foreach (var row in Rows)
            {
                int idTrou = -1;
                bool firstCell = false;
                bool hasVerticalTrou = true;
                foreach (var cell in row)
                {
                    if (cell.Selected)
                    {
                        if (!firstRow) { startrow = Rows.IndexOf(row); firstRow = true; }
                        if (!firstCell) { iprec = row.IndexOf(cell); firstCell = true; }
                        if (iprec != startcol && startcol != -1) { return merged; }
                        jprec = row.IndexOf(cell);
                        endrow = Rows.IndexOf(row);
                        hasVerticalTrou = false;
                        if (idTrou > iprec && idTrou < jprec) { return merged; }
                    }
                    else idTrou = row.IndexOf(cell);
                }

                if (!hasVerticalTrou && !trouVerti)
                {
                    if (endcol != -1 && jprec != endcol) { return merged; }
                    startcol = iprec; endcol = jprec;
                    iprec = jprec = 0;
                }
                else if (!hasVerticalTrou && trouVerti) return merged;
                else if (firstRow) { trouVerti = true; }
            }

            foreach (var cell in MergedCells)
                if (cell.Selected) return merged;

            dynamic[] endmerged = { true, startcol, endcol, startrow, endrow };
            #endregion
            return endmerged;
        }

        private void MergeNow(int i, int j, int rowSpan, int columnSpan)
        {
            int[] pos = GetPosition(GroupNames.GridProperty, PropertyNames.SelectedElement);
            int idG = pos[0], idP = pos[1];
            if (rowSpan > 1 || columnSpan > 1)
            {
                for (int k = i; k < i + rowSpan; k++)
                {
                    for (int p = j; p < j + columnSpan; p++)
                    {
                        Rows[k][p].OnUpdated(GroupNames.GridProperty, PropertyNames.Merged, "1", true);
                        Rows[k][p].OnUnselected();
                        Rows[k][p].ComponentView!.Visibility = Visibility.Hidden;
                    }
                }
                Component component = new ContainerModel(true); component.Added = true;
                component.Parent = this; component.Name = ComponentList.Cell.ToString();
                #region
                //Restrictions actuelles de Cellule
                component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString());
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.Height, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveParentToChild, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveChildToParent, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveLeft, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveRight, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveTop, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.MoveBottom, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Transform, PropertyNames.HE, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderWidth, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.BorderRadius, VisibilityValue.Collapsed.ToString(), false);
                component.OnAddConfig(GroupNames.Appearance, PropertyNames.Margin, VisibilityValue.Collapsed.ToString(), false);
                component.OnAddConfig(GroupNames.GridProperty, PropertyNames.Row, VisibilityValue.Visible.ToString());
                component.OnInitialized();
                component.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
                component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString(), true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.Padding, "10", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.CBorder, "0", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderColor, "#FF000000", true);
                if (j == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderLeftWidth, "0.4", true);
                if (i == 0) component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderTopWidth, "0.4", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderRightWidth, "0.4", true);
                component.OnUpdated(GroupNames.Appearance, PropertyNames.BorderBottomWidth, "0.4", true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.SelectedElement, groupProps![idG].Properties[idP].Value, true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.SelectedMode, ESelectedMode.Multiple.ToString(), true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.Row, i.ToString(), true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.Column, j.ToString(), true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.RowSpan, rowSpan.ToString(), true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.ColumnSpan, columnSpan.ToString(), true);
                component.OnUpdated(GroupNames.GridProperty, PropertyNames.Fusion, "1", true);

                Grid.SetRow(component.ComponentView, i); Grid.SetColumn(component.ComponentView, j);
                Grid.SetRowSpan(component.ComponentView, rowSpan); Grid.SetColumnSpan(component.ComponentView, columnSpan);
                grid.Children.Add(component.ComponentView); MergedCells.Add(component);
                #endregion
            }
        }

        public override int[] GetPosition(GroupNames groupName, PropertyNames propertyName)
        {
            int[] position = { -1, -1 };
            foreach (var group in groupProps!)
            {
                if (group.Name == groupName.ToString())
                {
                    position[0] = groupProps.IndexOf(group);
                    foreach (var prop in group.Properties)
                    {
                        if (prop.Name == propertyName.ToString())
                        {
                            position[1] = group.Properties.IndexOf(prop);
                            break;
                        }
                    }
                    break;
                }
            }
            return position;
        }

        public override string OnCopyOrPaste(string value = null!, bool isPaste = false)
        {
            CompSerialiser valueD = null!;
            if (value != null!)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerialiser>(value)!;

            if (Selected && isPaste && value != null && valueD!.Children != null)
            {
                string name = valueD.Name!;
                Component component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this; component.Added = true;
                component.OnDeserialiser(valueD);
                //Il faut enfin appliquer à ce composant, les contraintes de mise en page.
                //Child = component;
                //border.Child = Child.ComponentView;
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerialiser());
            else if (!Selected) ;
            return null!;
        }

        public override StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = EnumName.ToString()!;
            structuralElement.Selected = Selected;
            structuralElement.Children = new List<StructuralElement>();

            return structuralElement;
        }

        public override void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
            }
            else if (structuralElement.Children != null!)
            {
                for (int i = 0; i < structuralElement.Children!.Count; i++)
                {
                    //Children[i].SelectFromStructuralView(structuralElement.Children[i]);
                }
            }
        }

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            return;
        }

        public override bool AllowFixSize(bool isHeight = true)
        {
            return false;
        }

        public override void UpdateComponent(List<ComponentRef> refs, GroupNames groupName, PropertyNames propertyName, string value, int i = 0, bool found = false)
        {
            
        }

        public override void AddComponent(List<ComponentRef> refs, string data, int i = 0, bool found = false)
        {
            
        }

        public override void DeleteComponent(List<ComponentRef> refs, int i = 0, bool found = false)
        {
            
        }

        public override List<ComponentRef> BuildComponentRefs(int i)
        {
            return null!;
        }

        public override ReactComponent BuildReactComponent(string tab, int n, string id)
        {
            var comp = new ReactComponent();
            return comp;
        }

        public override WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams)
        {
            WebComponent comp = new WebComponent();
            List<WebComponent> childComps = new List<WebComponent>();
            #region
            double w = Width(this), h = Height(this);
            double pw = Parent!.Width(Parent), ph = Parent.Height(Parent);

            const string invalide = "invalide";
            string tableStyleName = "tableCommonStyle" + id;
            #endregion
            string styleName = $"grid{id}";
            double cmarginLeft = 0, cmarginTop = 0;
            #region
            /* Self alignement */
            string position = invalide;
            string left = invalide;
            string top = invalide;
            string floatv = invalide;
            string hls = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            string hcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            string hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;

            string vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            string vcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            string vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;

            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                cmarginLeft = hls == "1" ? 0 : (hcs == "1" ? (pw - w) / 2 : (hrs == "1" ? pw - w : 0));
                int i = Convert.ToInt32(lparams[2]);
                bool end = Convert.ToBoolean(lparams[3]);
                if (lparams[0] == "sb")
                {
                    if (i == 0) cmarginTop = 0;
                    else if (end) cmarginTop = Convert.ToDouble(lparams[1]) - h;
                    else cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    if (i == 0) cmarginTop = Convert.ToDouble(lparams[1]) - h;
                    else if (end) cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                    else cmarginTop = 0;
                }
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                cmarginTop = vts == "1" ? 0 : (vcs == "1" ? (ph - h) / 2 : (vbs == "1" ? ph - h : 0));
                int i = Convert.ToInt32(lparams[2]);
                bool end = Convert.ToBoolean(lparams[3]);
                if (lparams[0] == "sb")
                {
                    if (i == 0) cmarginLeft = 0;
                    else if (end) cmarginLeft = Convert.ToDouble(lparams[1]) - w;
                    else cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    if (i == 0) cmarginLeft = Convert.ToDouble(lparams[1]) - w;
                    else if (end) cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                    else cmarginLeft = 0;
                }
                else floatv = "left";
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Container)
            {
                //cmarginTop = vts == "1" ? 0 : (vcs == "1" ? (ph - h) / 2 : (vbs == "1" ? ph - h : 0));
                cmarginLeft = hls == "1" ? 0 : (hcs == "1" ? (pw - w) / 2 : (hrs == "1" ? pw - w : 0));
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Stack)
            {
                cmarginTop = vts == "1" ? 0 : (vcs == "1" ? (ph - h) / 2 : (vbs == "1" ? ph - h : 0));
                cmarginLeft = hls == "1" ? 0 : (hcs == "1" ? (pw - w) / 2 : (hrs == "1" ? pw - w : 0));
                position = "absolute";
                left = $"{cmarginLeft}";
                top = $"{cmarginTop}";
            }
            /* Transform */
            /* Appearance */
            string backgroundColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            string cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            string margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            string marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            string marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            string marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            string marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;

            string cborder = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CBorder.ToString()]].Value;
            string borderWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderWidth.ToString()]].Value;
            string borderColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderColor.ToString()]].Value);
            string borderLeftWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderLeftWidth.ToString()]].Value;
            string borderTopWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderTopWidth.ToString()]].Value;
            string borderRightWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRightWidth.ToString()]].Value;
            string borderBottomWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderBottomWidth.ToString()]].Value;
            #endregion
            #region
            comp.Styles = new Dictionary<string, Dictionary<string, string>> {
                {
                    styleName,
                    new Dictionary<string, string>
                    {
                        /* Global */
                        { "position", position},
                        { "float", floatv},
                        /* Transform */
                        { "width", $"{w}px".Replace(",", ".") },
                        { "height", $"{h}px".Replace(",", ".") },
                        /* Appearance */
                        { "background-color", backgroundColor },
                        { "margin", cmargin == "1" && margin != "0" ? margin : invalide },
                        { "margin-left", (cmargin == "0" && marginLeft != "0") || cmarginLeft != 0 ? $"{Convert.ToDouble(marginLeft) + cmarginLeft}px".Replace(",", ".") : invalide },
                        { "margin-top", (cmargin == "0" && marginTop != "0") || cmarginTop != 0 ? $"{Convert.ToDouble(marginTop) + cmarginTop}px".Replace(",", ".") : invalide },
                        { "margin-right", cmargin == "0" && marginRight != "0" ? marginRight+"px" : invalide },
                        { "margin-bottom", cmargin == "0" && marginBottom != "0" ? marginBottom+"px" : invalide },

                        { "border-color", cborder == "1" && borderWidth != "0" ? borderColor : invalide },
                        { "border-style", cborder == "1" && borderWidth != "0" ? "solid" : invalide },
                        { "border-width", cborder == "1" && borderWidth != "0" ? borderWidth+"px" : invalide },
                        { "border-left-color", cborder == "0" && borderLeftWidth != "0" ? borderColor : invalide },
                        { "border-left-style", cborder == "0" && borderLeftWidth != "0" ? "solid" : invalide },
                        { "border-left-width", cborder == "0" && borderLeftWidth != "0" ? borderLeftWidth+"px" : invalide },
                        { "border-top-color", cborder == "0" && borderTopWidth != "0" ? borderColor : invalide },
                        { "border-top-style", cborder == "0" && borderTopWidth != "0" ? "solid" : invalide },
                        { "border-top-width", cborder == "0" && borderTopWidth != "0" ? borderTopWidth+"px" : invalide },
                        { "border-right-color", cborder == "0" && borderRightWidth != "0" ? borderColor : invalide },
                        { "border-right-style", cborder == "0" && borderRightWidth != "0" ? "solid" : invalide },
                        { "border-right-width", cborder == "0" && borderRightWidth != "0" ? borderRightWidth+"px" : invalide },
                        { "border-bottom-color", cborder == "0" && borderBottomWidth != "0" ? borderColor : invalide },
                        { "border-bottom-style", cborder == "0" && borderBottomWidth != "0" ? "solid" : invalide },
                        { "border-bottom-width", cborder == "0" && borderBottomWidth != "0" ? borderBottomWidth+"px" : invalide },
                    }
                }
            };
            #endregion
            tab = tab != "" ? tabs(n + 1) + tab + "\n" : "";
            string childrenBody = "";
            #region
            var styles2 = styles;
            for (int i = 0; i < Rows.Count; i++)
            {
                childrenBody += $"{tabs(n + 1)}<tr class=\"{tableStyleName}\">\n";
                for (int j = 0; j < Rows[i].Count; j++)
                {
                    var child = Rows[i][j].BuildWebComponent(styles2, "", n + 3, id + i + j, lparams);
                    childrenBody += $"{tabs(n + 2)}<td class=\"{tableStyleName}\">\n" + child.Body + $"\n{tabs(n + 2)}</td>";
                    childrenBody += j < Rows[i].Count - 1 ? "\n" : "";
                    styles2 = child.Styles;
                }
                childrenBody += $"\n{tabs(n + 1)}</tr>";
                childrenBody += i < Rows.Count - 1 ? "\n" : "";
            }
            comp.Body = $"{tabs(n)}<table class=\"{tableStyleName} {styleName}\">\n{childrenBody}\n{tab}{tabs(n)}</table>";

            styles2!.Add(
                tableStyleName,
                new Dictionary<string, string> {
                    { "border-collapse", "collapse" },
                    { "border", "1px solid gray" },
                    { "padding", "0px" },
                    { "margin", "0px" },
                }
            );
            bool found = false; string keyStyle = string.Empty;
            foreach (var key in styles2!.Keys)
            {
                if (comp.EqualStyles(comp.Styles[styleName], styles2[key]))
                {
                    found = true; keyStyle = key;
                }
                comp.Styles.Add(key, styles2[key]);
            }
            if (found) { comp.Styles.Remove(styleName); styleName = keyStyle; }
            #endregion
            return comp;
        }

        public override ReactComponent BuildFlutterComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            return comp;
        }

        public override ReactComponent BuildAndroidXmlComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            return comp;
        }

        public override ReactComponent BuildAndroidComposeComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            return comp;
        }

        private string tabs(int n)
        {
            string tab = "";
            for (int i = 0; i < n; i++)
                tab += "    ";
            return tab;
        }

        private string tcolor(string color)
        {
            return color.Length == 9 ? "#" + color.Substring(3).ToLower() : color.ToLower();
        }

        public override void LoadIds()
        {
            Ids = new Dictionary<string, PropStringToIndex>();
            foreach (var group in groupProps!)
            {
                Ids!.Add(group.Name, new PropStringToIndex() { IdGroup = groupProps.IndexOf(group), Props = new Dictionary<string, int>() });
                foreach (var prop in group.Properties)
                {
                    Ids[group.Name].Props!.Add(prop.Name, group.Properties.IndexOf(prop));
                }
            }
        }

        public override double Width(Component component)
        {
            double w = 0, wp = 0;
            if (ComponentView!.Width.Equals(double.NaN)) wp = Parent!.Width(this);
            else wp = ComponentView!.Width;
            if (component.Equals(this)) w = wp;
            else
            {
                int n = 0, i = 0, j = 0;
                bool found = false;
                foreach (var row in Rows)
                {
                    foreach (var cell in row)
                        if (component.Equals(cell)) { i = Rows.IndexOf(row); j = row.IndexOf(cell); found = true; break; }
                    if (found) break;
                }

                foreach (var cell in Rows[0])
                {
                    if (j == Rows[0].IndexOf(cell))
                    {
                        if (!cell.ComponentView!.Width.Equals(double.NaN)) { wp = cell.ComponentView!.Width; n = 0; break; }
                        else n++;
                    }
                    else
                    {
                        if (cell.ComponentView!.Width.Equals(double.NaN)) n++;
                        else wp -= cell.ComponentView!.Width;
                    }
                }
                w = n > 0 ? wp / n : wp;
            }
            return w;
        }
        public override double Height(Component component)
        {
            double h = 0, hp = 0;
            if (ComponentView!.Height.Equals(double.NaN)) hp = Parent!.Height(this);
            else hp = ComponentView!.Height;
            if (component.Equals(this)) h = hp;
            else
            {
                int n = 0, i = 0, j = 0;
                bool found = false;
                foreach (var row in Rows)
                {
                    foreach (var cell in row)
                        if (component.Equals(cell)) { i = Rows.IndexOf(row); j = row.IndexOf(cell); found = true; break; }
                    if (found) break;
                }

                foreach (var row in Rows)
                {
                    if (i == Rows.IndexOf(row))
                    {
                        if (!row[0].ComponentView!.Height.Equals(double.NaN)) { hp = row[0].ComponentView!.Height; n = 0; break; }
                        else n++;
                    }
                    else
                    {
                        if (row[0].ComponentView!.Height.Equals(double.NaN)) n++;
                        else hp -= row[0].ComponentView!.Height;
                    }
                }
                h = n > 0 ? hp / n : hp;
            }
            return h;
        }

        public override void OnConfigured()
        {
            groupProps = new List<GroupProperties>
            {
                #region
                new GroupProperties()
                {
                    Name = GroupNames.Alignment.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.HL.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HR.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VT.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VB.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.SpaceBetween.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.SpaceAround.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.SpaceEvery.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        }
                    }
                },
                new GroupProperties()
                {
                    Name = GroupNames.Transform.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.Width.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = SizeValue.Expand.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Height.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = SizeValue.Auto.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.X.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Y.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.ROT.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HE.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VE.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HVE.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                    }
                },
                new GroupProperties()
                {
                    Name = GroupNames.Appearance.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.FillColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "Transparent",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.CMargin.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.MarginLeft.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Margin.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginTop.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginBottom.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.CPadding.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.PaddingLeft.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Padding.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingTop.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingBottom.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.CBorder.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "BorderLeft",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderLeftWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderLeftColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderLeftStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderTopWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderTopColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderTopStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRightWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRightColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRightStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBottomWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBottomColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBottomStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.CBorderRadius.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadius.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusTopRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                    }
                },
                new GroupProperties()
                {
                    Name = GroupNames.SelfAlignment.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.HL.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HR.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VT.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VB.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        }
                    }
                },
                new GroupProperties()
                {
                    Name = GroupNames.GridProperty.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.SelectedElement.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ESelectedElement.Cell.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HideBorder.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        }
                    }
                },
                new GroupProperties()
                {
                    Name = GroupNames.Global.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.SelectedMode.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = ESelectedMode.Single.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.FilePicker.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveLeft.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveRight.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveTop.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveBottom.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Focus.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveParentToChild.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveChildToParent.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Trash.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.CanSelect.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = CanSelectValues.None.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                    }
                }
                #endregion
            };
        }
    }
}
