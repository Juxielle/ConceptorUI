using ConceptorUI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using ConceptorUI.Views.ComponentP;
using System.Globalization;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    internal class ColumnModel : Component
    {
        public List<Component> Children { get; set; }
        Border contentP;
        Border content;
        Grid grid;

        private int columnCount;

        public ColumnModel(bool isConstraints = false)
        {
            columnCount = 0;

            contentP = new Border();
            content = new Border();
            grid = new Grid();
            Children = new List<Component>();
            ComponentView = contentP;
            contentP.Child = content;
            content.Child = grid; EnumName = ComponentList.Column;
            ComponentView.PreviewMouseDown += OnMouseDown;
            OnConfigured();
            LoadIds();
            if (!isConstraints) OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            contentP.BorderBrush = Brushes.DodgerBlue;
            contentP.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Column;
            PanelProperty.Instance.FeedProps();
            Selected = true;
            return true;
        }

        public override void OnUnselected(bool isInterne = false) {
            Selected = false;
            contentP.BorderBrush = Brushes.Transparent;
            contentP.BorderThickness = new Thickness(0);
            foreach (var child in Children)
            {
                child.OnUnselected(isInterne);
            }
        }

        public override bool OnChildSelected()
        {
            var found = false;
            foreach (var child in Children)
            {
                found = child.OnChildSelected();
                if (found) break;
            }

            return Selected || found;
        }

        private void OnMouseDown(object sender,  MouseButtonEventArgs e)
        {
            var pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            if (groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(contentP) || 
                e.OriginalSource.Equals(content) || e.OriginalSource.Equals(grid)))
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
                PageView.Instance.OnSelected();
                PageView.Instance.RefreshStructuralView();
            }
        }

        public override void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            var pos = GetPosition(groupName, propertyName);
            int idG = pos[0], idP = pos[1];
            if (pos[0] != -1 && pos[1] != -1 && (Selected || allow))
            {
                var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
                var idGAl = posAl[0];

                var propName = "";
                try { propName = groupProps![idG].Properties[idP].Name; }
                catch (Exception e) { }

                if (Selected || allow)
                {
                    /* Alignement */
                    #region
                    if (propName == PropertyNames.VT.ToString() && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            groupProps![idG].Properties[idP + 2].Value = "0";
                            foreach (var child in Children)
                            {
                                string h = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                                if (value == "1")
                                {
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                                    if (h == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                                }
                                else if (h != SizeValue.Expand.ToString())
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                            }
                        }
                    }
                    else if (propName == PropertyNames.VC.ToString() && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            foreach (var child in Children)
                            {
                                string h = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                                if (value == "1")
                                {
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
                                    if (h == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                                }
                                else if (h != SizeValue.Expand.ToString())
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                            }
                        }
                    }
                    else if (propName == PropertyNames.VB.ToString() && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP - 2].Value = "0";
                            foreach (var child in Children)
                            {
                                string h = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                                if (value == "1")
                                {
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                                    if (h == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                                }
                                else if (h != SizeValue.Expand.ToString())
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                            }
                        }
                    }
                    else if (propName == PropertyNames.HL.ToString() && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            //Le cas de désactivation n'est pas prise en compte.
                            int nl = grid.ColumnDefinitions.Count;
                            int nc = Children.Count;
                            if (value == "1" && nc != 0 && nl == nc)
                            {
                                foreach (var row in grid.ColumnDefinitions)
                                {
                                    row.Width = new GridLength(row.Width.GridUnitType == GridUnitType.Star ? 0 : row.Width.Value,
                                                row.Width.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Width.GridUnitType);
                                }
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP + 2].Value == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(0);
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 2 && groupProps![idG].Properties[idP + 1].Value == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            groupProps![idG].Properties[idP + 2].Value = "0";
                            groupProps![idG].Properties[idP + 6].Value = "0";
                            groupProps![idG].Properties[idP + 7].Value = "0";
                            groupProps![idG].Properties[idP + 8].Value = "0";
                        }
                    }
                    else if (propName == PropertyNames.HC.ToString() && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            int nl = grid.ColumnDefinitions.Count;
                            int nc = Children.Count;
                            if (value == "1" && nc != 0 && nl == nc)
                            {
                                foreach (var row in grid.ColumnDefinitions)
                                {
                                    row.Width = new GridLength(row.Width.GridUnitType == GridUnitType.Star ? 0 : row.Width.Value,
                                        row.Width.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Width.GridUnitType);
                                }
                                grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP - 1].Value == "1")
                            {
                                grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP + 1].Value == "1")
                            {
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            }
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            groupProps![idG].Properties[idP + 5].Value = "0";
                            groupProps![idG].Properties[idP + 6].Value = "0";
                            groupProps![idG].Properties[idP + 7].Value = "0";
                        }
                    }
                    else if (propName == PropertyNames.HR.ToString() && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            int nl = grid.ColumnDefinitions.Count;
                            int nc = Children.Count;
                            if (value == "1" && nc != 0 && nl == nc)
                            {
                                foreach (var row in grid.ColumnDefinitions)
                                {
                                    row.Width = new GridLength(row.Width.GridUnitType == GridUnitType.Star ? 0 : row.Width.Value,
                                        row.Width.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Width.GridUnitType);
                                }
                                grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP - 2].Value == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                                grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 2 && groupProps![idG].Properties[idP - 1].Value == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                            }
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 2].Value = "0";
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP + 4].Value = "0";
                            groupProps![idG].Properties[idP + 5].Value = "0";
                            groupProps![idG].Properties[idP + 6].Value = "0";
                        }
                    }
                    if (propName == PropertyNames.HL.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.HorizontalAlignment = value == "0" ? contentP.HorizontalAlignment : HorizontalAlignment.Left;
                    }
                    else if (propName == PropertyNames.HC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.HorizontalAlignment = value == "0" ? contentP.HorizontalAlignment : HorizontalAlignment.Center;
                    }
                    else if (propName == PropertyNames.HR.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.HorizontalAlignment = value == "0" ? contentP.HorizontalAlignment : HorizontalAlignment.Right;
                    }
                    else if (propName == PropertyNames.VT.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.VerticalAlignment = value == "0" ? contentP.VerticalAlignment : VerticalAlignment.Top;
                    }
                    else if (propName == PropertyNames.VC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.VerticalAlignment = value == "0" ? contentP.VerticalAlignment : VerticalAlignment.Center;
                    }
                    else if (propName == PropertyNames.VB.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.VerticalAlignment = value == "0" ? contentP.VerticalAlignment : VerticalAlignment.Bottom;
                    }
                    else if (propName == PropertyNames.SpaceBetween.ToString())
                    {
                        if (Children.Count > 0)
                        {
                            string hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                            string hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                            string hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                            groupProps[idG].Properties[0].Value = groupProps[idG].Properties[1].Value = groupProps[idG].Properties[2].Value = "0";
                            groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = groupProps[idG].Properties[8].Value = "0";
                            groupProps[idG].Properties[idP].Value = "1";
                            int nl = grid.ColumnDefinitions.Count;
                            int nc = Children.Count;
                            if (nl == nc + 1 && hl == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                            }
                            else if (nl == nc + 1 && hr == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            else if (nl == nc + 2 && hc == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                                grid.ColumnDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            foreach (var rd in grid.ColumnDefinitions)
                            {
                                rd.Width = new GridLength(1, GridUnitType.Star);
                                int i = grid.ColumnDefinitions.IndexOf(rd);
                                if (i == 0) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                                else if (i == grid.ColumnDefinitions.Count - 1) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
                                else Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
                            }
                        }
                    }
                    else if (propName == PropertyNames.SpaceAround.ToString())
                    {
                        if (Children.Count > 0)
                        {
                            string hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                            string hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                            string hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                            groupProps[idG].Properties[0].Value = groupProps[idG].Properties[1].Value = groupProps[idG].Properties[2].Value = "0";
                            groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = groupProps[idG].Properties[8].Value = "0";
                            groupProps[idG].Properties[idP].Value = "1";
                            int nl = grid.ColumnDefinitions.Count;
                            int nc = Children.Count;
                            if (nl == nc + 1 && hl == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                            }
                            else if (nl == nc + 1 && hr == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            else if (nl == nc + 2 && hc == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                                grid.ColumnDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            foreach (var rd in grid.ColumnDefinitions)
                            {
                                rd.Width = new GridLength(1, GridUnitType.Star);
                                Children[grid.ColumnDefinitions.IndexOf(rd)].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
                            }
                        }
                    }
                    else if (propName == PropertyNames.SpaceEvery.ToString())
                    {
                        if (Children.Count > 0)
                        {
                            string hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                            string hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                            string hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                            groupProps[idG].Properties[0].Value = groupProps[idG].Properties[1].Value = groupProps[idG].Properties[2].Value = "0";
                            groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = groupProps[idG].Properties[8].Value = "0";
                            groupProps[idG].Properties[idP].Value = "1";
                            int nl = grid.ColumnDefinitions.Count;
                            int nc = Children.Count;
                            if (nl == nc + 1 && hl == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                            }
                            else if (nl == nc + 1 && hr == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            else if (nl == nc + 2 && hc == "1")
                            {
                                grid.ColumnDefinitions.RemoveAt(nl - 1);
                                grid.ColumnDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetColumn(Children[i].ComponentView, i);
                            }
                            foreach (var rd in grid.ColumnDefinitions)
                            {
                                rd.Width = new GridLength(1, GridUnitType.Star);
                                int i = grid.ColumnDefinitions.IndexOf(rd);
                                if (i == 0) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
                                else if (i == grid.ColumnDefinitions.Count - 1) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                                else Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
                            }
                        }
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (propName == PropertyNames.Width.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            contentP.Width = double.NaN; contentP.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[0].Value = "0";
                            groupProps[idGAl].Properties[1].Value = "0";
                            groupProps[idGAl].Properties[2].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            contentP.Width = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[0].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                contentP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            var vd = Helper.ConvertToDouble(value);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            contentP.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                contentP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propName == PropertyNames.Height.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            contentP.Height = double.NaN; contentP.VerticalAlignment = VerticalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[3].Value = "0";
                            groupProps[idGAl].Properties[4].Value = "0";
                            groupProps[idGAl].Properties[5].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            contentP.Height = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[3].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                contentP.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            var vd = Helper.ConvertToDouble(value);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            contentP.Height = vd;
                            if (groupProps[idGAl].Properties[3].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                contentP.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString() || propName == PropertyNames.VE.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.MoveParentToChild.ToString())
                    {
                        if (Children.Count > 0)
                        {
                            OnUnselected();
                            Children[0].OnSelected();
                        }
                    }
                    #endregion
                    /* Appearance */
                    #region
                    else if (propName == PropertyNames.FillColor.ToString())
                    {
                        content.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                      new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Opacity.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        content.Opacity = vd;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.CMargin.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.MarginBtnActif.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Margin.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        contentP.Margin = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.MarginLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(vd, contentP.Margin.Top, contentP.Margin.Right, contentP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, vd, contentP.Margin.Right, contentP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, vd, contentP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, contentP.Margin.Right, vd);
                    }
                    else if (propName == PropertyNames.CPadding.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.PaddingBtnActif.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Padding.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        content.Padding = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.PaddingLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(vd, content.Padding.Top, content.Padding.Right, content.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, vd, content.Padding.Right, content.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, vd, content.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, content.Padding.Right, vd);
                    }
                    else if (propName == PropertyNames.CBorder.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderBtnActif.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 6].Value =
                        groupProps[idG].Properties[idP + 9].Value = groupProps[idG].Properties[idP + 12].Value = vd + "";
                        content.BorderThickness = new Thickness(vd); Console.WriteLine("Border Width Updating => " + value);
                    }
                    else if (propName == PropertyNames.BorderColor.ToString())
                    {
                        content.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                      new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderLeftWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(vd, content.BorderThickness.Top, content.BorderThickness.Right, content.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderTopWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, vd * 0.65, content.BorderThickness.Right, content.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderRightWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, vd, content.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderBottomWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, content.BorderThickness.Right, vd * 0.65);
                    }
                    else if (propName == PropertyNames.CBorderRadius.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderRadBtnActif.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderRadius.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        content.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(vd, content.CornerRadius.TopRight,
                                                      content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, content.CornerRadius.TopRight,
                                                      content.CornerRadius.BottomRight, vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, vd,
                                                      content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, content.CornerRadius.TopRight,
                                                      vd, content.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Global */
                    #region
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.Focus)
                    {
                        Console.WriteLine(@"Focus: "+ value);
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    #endregion
                }
            }
            else
            {
                #region
                if (propertyName == PropertyNames.MoveChildToParent)
                {
                    foreach (var child in Children)
                    {
                        if (child.Selected)
                        {
                            child.OnUnselected();
                            OnSelected(); break;
                        }
                    }
                }
                else if (propertyName == PropertyNames.Trash)
                {
                    var i = -1;
                    foreach (var child in Children)
                    {
                        if (!child.Selected) continue;
                        i = Children.IndexOf(child); break;
                    }
                    if (i != -1)
                    {
                        _deleteChild(i);
                    }
                }
                else if (propertyName == PropertyNames.Width)
                {
                    var pos2 = GetPosition(GroupNames.Alignment, PropertyNames.SpaceBetween);
                    int idG2 = pos2[0], idP2 = pos2[1];
                    var grid = (Grid)(content).Child;
                    var i = -1;
                    var nl = grid.ColumnDefinitions.Count;
                    var nc = Children.Count;
                    var hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                    var hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                    var hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                    var sb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value;
                    var sa = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value;
                    var se = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value;


                    var oldValue = SizeValue.Auto.ToString();
                    var found = false;
                    foreach (var child in Children)
                    {
                        
                        if (child.Selected) i = Children.IndexOf(child);
                        found = found || child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup]
                            .Properties[child.Ids![GroupNames.Transform.ToString()]
                                .Props![PropertyNames.Width.ToString()]].Value == SizeValue.Expand.ToString();
                    }

                    if (i != -1 && value == SizeValue.Expand.ToString())
                    {
                        if (nl == nc + 1 && hl == "1") grid.ColumnDefinitions.RemoveAt(nl - 1);
                        else if (nl == nc + 1 && hr == "1") grid.ColumnDefinitions.RemoveAt(0);
                        else if (nl == nc + 2 && hc == "1")
                        {
                            grid.ColumnDefinitions.RemoveAt(nl - 1);
                            grid.ColumnDefinitions.RemoveAt(0);
                        }
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value = "0";
                        //foreach (var row in grid.ColumnDefinitions)
                        //    row.Width = new GridLength(0, GridUnitType.Auto); //A revoir
                        grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                    }
                    else if (i != -1 && value == SizeValue.Auto.ToString())
                    {//A revoir
                        i = i == 0 && nl >= nc + 1 && (hr == "1" || hc == "1") ? 1 : i;
                        grid.ColumnDefinitions[i].Width = new GridLength(0, GridUnitType.Auto);
                    }
                    else if (i != -1 && value != SizeValue.Old.ToString())
                    {//A revoir - revoir aussi la restitution
                        if (sb == "0" && sa == "0" && se == "0")
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            i = i == 0 && nl >= nc + 1 && (hr == "1" || hc == "1") ? 1 : i;
                            grid.ColumnDefinitions[i].Width = new GridLength(0, GridUnitType.Auto);
                        }
                    }
                    if(i != -1 && !found)
                        OnUpdated(GroupNames.Alignment, PropertyNames.HL, "1", true);
                }
                else if (propertyName == PropertyNames.MoveLeft)
                {
                    var grid = (Grid)(content).Child;
                    var nl = grid.ColumnDefinitions.Count;
                    var nc = Children.Count;
                    var hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                    var hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                    var hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                    var focus = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.Focus.ToString()]].Value == "1";
                    var k = -1;
                    foreach (var child in Children)
                    {
                        var i = Children.IndexOf(child);
                        if (child.Selected) { k = i; break; }
                    }
                    if (k != -1 && (nl == nc || (nl == nc + 1 && hl == "1" && k > 0) || (nl == nc + 1 && hr == "1" && k > 0) || (nl == nc + 2 && hc == "1" && k > 0)))
                    {
                        if (focus)
                        {
                            var fvbc = hr == "1" || hc == "1";
                            var child = Children[k];
                            var rd = grid.ColumnDefinitions[fvbc ? k + 1 : k];
                            Children.RemoveAt(k); Children.Insert(k - 1, child);
                            grid.Children.RemoveAt(k); grid.Children.Insert(k - 1, child.ComponentView!);
                            grid.ColumnDefinitions.RemoveAt(fvbc ? k + 1 : k); grid.ColumnDefinitions.Insert(fvbc ? k : k - 1, rd);
                            Grid.SetColumn(Children[k - 1].ComponentView!, fvbc ? k : k - 1); Grid.SetColumn(Children[k].ComponentView!, fvbc ? k + 1 : k);
                        }
                        else
                        {
                            Children[k].OnUnselected();
                            Children[k - 1].OnSelected();
                        }
                    }
                }
                else if (propertyName == PropertyNames.MoveRight)
                {
                    var grid = (Grid)(content).Child;
                    var nl = grid.ColumnDefinitions.Count;
                    var nc = Children.Count;
                    var hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                    var hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                    var hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                    var focus = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.Focus.ToString()]].Value == "1";
                    var k = -1;
                    foreach (var child in Children)
                    {
                        var i = Children.IndexOf(child);
                        if (child.Selected) { k = i; break; }
                    }
                    if (k != -1 && (nl == nc || (nl == nc + 1 && hr == "1" && k < nc - 1) || (nl == nc + 1 && hl == "1" && k < nc - 1) || (nl == nc + 2 && hc == "1" && k < nc - 1)))
                    {
                        if (focus)
                        {
                            var fvbc = hr == "1" || hc == "1";
                            var child = Children[k];
                            var rd = grid.ColumnDefinitions[fvbc ? k + 1 : k];
                            Children.RemoveAt(k); Children.Insert(k + 1, child);
                            grid.Children.RemoveAt(k); grid.Children.Insert(k + 1, child.ComponentView);
                            grid.ColumnDefinitions.RemoveAt(fvbc ? k + 1 : k); grid.ColumnDefinitions.Insert(fvbc ? k + 2 : k + 1, rd);
                            Grid.SetColumn(Children[k + 1].ComponentView, fvbc ? k + 2 : k + 1); Grid.SetColumn(Children[k].ComponentView, fvbc ? k + 1 : k);
                        }
                        else
                        {
                            Children[k].OnUnselected();
                            Children[k + 1].OnSelected();
                        }
                    }
                }
                foreach (var child in Children)
                {
                    child.OnUpdated(groupName, propertyName, value);
                }
                #endregion
            }
        }

        private void _deleteChild(int i)
        {
            #region Delete child
            var nc = Children.Count;
            var nl = grid.RowDefinitions.Count;
            var hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            var hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
            var fvbc = hr == "1" || hc == "1";
            if (nc == 1)
            {
                grid.ColumnDefinitions.Clear();
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value = "0";
                groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value = "0";
            }
            else
            {
                grid.ColumnDefinitions.RemoveAt(fvbc ? i + 1 : i);
            }
            grid.Children.RemoveAt(i);
            Children.RemoveAt(i);
            if (Children.Count > 0)
                for (var k = i; k < Children.Count; k++)
                    Grid.SetColumn(Children[k].ComponentView!, fvbc ? k + 1 : k);
            OnSelected();
            #endregion
        }

        public sealed override void OnInitialized()
        {
            var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
            var idGAl = posAl[0];
            foreach (var group in groupProps!)
            {
                if (group.Visibility == VisibilityValue.Visible.ToString())
                {
                    foreach (var prop in group.Properties)
                    {
                        if (prop.Visibility == VisibilityValue.Visible.ToString())
                        {
                            #region Alignements
                            if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.Alignment.ToString())
                            {
                                string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                                string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                                string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                                //foreach (var child in Children)
                                //{
                                //    string h = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                                //    string al = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                                //    string ac = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                                //    string ar = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                                //    if (prop.Value == "1")
                                //    {
                                //        child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                                //        if (h == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                                //    }
                                //    else if (vt == "0" && vc == "0" && vb == "0" && h != SizeValue.Expand.ToString())
                                //    {
                                //        if (al == "0" && ac == "0" && ar == "0") child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                                //    }
                                //}
                            }
                            else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.Alignment.ToString())
                            {
                                string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                                string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                                string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                                //foreach (var child in Children)
                                //{
                                //    string h = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                                //    string al = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                                //    string ac = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                                //    string ar = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                                //    if (prop.Value == "1")
                                //    {
                                //        child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
                                //        if (h == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                                //    }
                                //    else if (vt == "0" && vc == "0" && vb == "0" && h != SizeValue.Expand.ToString())
                                //    {
                                //        if (al == "0" && ac == "0" && ar == "0") child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                                //    }
                                //}
                            }
                            else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.Alignment.ToString())
                            {
                                string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                                string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                                string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                                //foreach (var child in Children)
                                //{
                                //    string h = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                                //    string al = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                                //    string ac = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                                //    string ar = child.groupProps![child.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[child.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                                //    if (prop.Value == "1")
                                //    {
                                //        child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                                //        if (h == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                                //    }
                                //    else if (vt == "0" && vc == "0" && vb == "0" && h != SizeValue.Expand.ToString())
                                //    {
                                //        if (al == "0" && ac == "0" && ar == "0") child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                                //    }
                                //}
                            }
                            else if (prop.Name == PropertyNames.HL.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Left;
                            }
                            else if (prop.Name == PropertyNames.HC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Center;
                            }
                            else if (prop.Name == PropertyNames.HR.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Right;
                            }
                            else if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") contentP.VerticalAlignment = VerticalAlignment.Top;
                            }
                            else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") contentP.VerticalAlignment = VerticalAlignment.Center;
                            }
                            else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") contentP.VerticalAlignment = VerticalAlignment.Bottom;
                            }
                            #endregion
                            
                            #region Transform
                            else if (prop.Name == PropertyNames.Width.ToString())
                            {
                                if (prop.Value == SizeValue.Expand.ToString())
                                {
                                    contentP.Width = double.NaN; contentP.HorizontalAlignment = HorizontalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    contentP.Width = double.NaN;
                                    if (groupProps[idGAl].Properties[0].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Left;
                                    else if (groupProps[idGAl].Properties[1].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[2].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Right;
                                    else contentP.HorizontalAlignment = HorizontalAlignment.Left;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    var vd = Helper.ConvertToDouble(prop.Value);
                                    contentP.Width = vd;
                                    if (groupProps[idGAl].Properties[0].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Left;
                                    else if (groupProps[idGAl].Properties[1].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[2].Value == "1") contentP.HorizontalAlignment = HorizontalAlignment.Right;
                                    else contentP.HorizontalAlignment = HorizontalAlignment.Left;
                                }
                            }
                            else if (prop.Name == PropertyNames.Height.ToString())
                            {
                                if (prop.Value == SizeValue.Expand.ToString())
                                {
                                    contentP.Height = double.NaN; contentP.VerticalAlignment = VerticalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    contentP.Height = double.NaN;
                                    if (groupProps[idGAl].Properties[3].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Top;
                                    else if (groupProps[idGAl].Properties[4].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[5].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Bottom;
                                    else contentP.VerticalAlignment = VerticalAlignment.Top;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    var vd = Helper.ConvertToDouble(prop.Value);
                                    contentP.Height = vd;
                                    if (groupProps[idGAl].Properties[3].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Top;
                                    else if (groupProps[idGAl].Properties[4].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[5].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Bottom;
                                    else contentP.VerticalAlignment = VerticalAlignment.Top;
                                }
                            }
                            #endregion
                            
                            #region Appearance
                            else if (prop.Name == PropertyNames.FillColor.ToString())
                            {
                                content.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                                   new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                            }
                            else if (prop.Name == PropertyNames.Opacity.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.Opacity = vd;
                            }
                            else if (prop.Name == PropertyNames.Margin.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                contentP.Margin = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.MarginLeft.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                contentP.Margin = new Thickness(vd, contentP.Margin.Top, contentP.Margin.Right, contentP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginTop.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                contentP.Margin = new Thickness(contentP.Margin.Left, vd, contentP.Margin.Right, contentP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginRight.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, vd, contentP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginBottom.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, contentP.Margin.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.Padding.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.Padding = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.Padding = new Thickness(vd, content.Padding.Top, content.Padding.Right, content.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingTop.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.Padding = new Thickness(content.Padding.Left, vd, content.Padding.Right, content.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingRight.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, vd, content.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, content.Padding.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderWidth.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.BorderThickness = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderColor.ToString())
                            {
                                content.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                        new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                            }
                            else if (prop.Name == PropertyNames.BorderLeftWidth.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.BorderThickness = new Thickness(vd, content.BorderThickness.Top, content.BorderThickness.Right, content.BorderThickness.Bottom);
                            }
                            else if (prop.Name == PropertyNames.BorderTopWidth.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.BorderThickness = new Thickness(content.BorderThickness.Left, vd * 0.65, content.BorderThickness.Right, content.BorderThickness.Bottom);
                            }
                            else if (prop.Name == PropertyNames.BorderRightWidth.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, vd, content.BorderThickness.Bottom);
                            }
                            else if (prop.Name == PropertyNames.BorderBottomWidth.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, content.BorderThickness.Right, vd * 0.65);
                            }
                            else if (prop.Name == PropertyNames.BorderRadius.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.CornerRadius = new CornerRadius(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.CornerRadius = new CornerRadius(vd, content.CornerRadius.TopRight,
                                                              content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, content.CornerRadius.TopRight,
                                                              content.CornerRadius.BottomRight, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, vd,
                                                              content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, content.CornerRadius.TopRight,
                                                              vd, content.CornerRadius.BottomLeft);
                            }
                            #endregion
                        }
                    }
                }
            }
        }

        public override CompSerialiser OnSerialiser()
        {
            var children = new List<CompSerialiser>();
            foreach (var child in Children)
                children.Add(child.OnSerialiser());
            return new CompSerialiser
            {
                Name = EnumName.ToString(),
                Properties = groupProps,
                Children = Children.Count > 0 ? children : null
            };
        }

        public override void OnDeserialiser(CompSerialiser compSerialiser)
        {
            groupProps = compSerialiser.Properties;
            #region
            if (compSerialiser.Children != null)
            {
                columnCount = compSerialiser.Children.Count;
                int i = 0;

                var components = new List<Component>();
                bool expanded = false;

                foreach (var child in compSerialiser.Children)
                {
                    Component component = ManageEnums.Instance.GetComponent(child.Name!);
                    component.Parent = this; component.Added = true;
                    component.OnDeserialiser(child);
                    components.Add(component);
                    string w = component.groupProps![component.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[component.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                    expanded = expanded || w == SizeValue.Expand.ToString();
                }

                foreach (var component in components)
                {
                    grid.Children.Add(component.ComponentView);
                    Children.Add(component); i++;
                    LayoutConstraints(Children.Count - 1, true, expanded);
                }
            }
            #endregion
            OnInitialized();
        }

        public override void OnAddConfig(GroupNames groupName, PropertyNames propertyName, string value, bool isGroup = true)
        {
            var pos = GetPosition(groupName, propertyName);
            if (pos[0] == -1 || pos[1] == -1) return;
            int idG = pos[0], idP = pos[1];
            if (isGroup)
            {
                groupProps![idG].Visibility = value;
                if (value == VisibilityValue.Collapsed.ToString())
                    for (var i = 0; i < groupProps[idG].Properties.Count; i++)
                        groupProps[idG].Properties[i].Visibility = value;
            }
            else
            {
                groupProps![idG].Properties[idP].Visibility = value;
            }
        }

        public override void OnConfiguOut(int id)
        {
            if (Selected)
            {
                var first = false;
                if(Properties.ComponentOutsUsing![id].Name == ComponentList.Column.ToString() && !Added)
                {
                    Added = first = true;
                    foreach (var group in Properties.ComponentOutsUsing[id].Component)
                    {
                        var i = Properties.ComponentOutsUsing[id].Component.IndexOf(group);
                        groupProps![i].Visibility = group.Visibility;
                        foreach (var prop in group.Properties)
                        {
                            var j = group.Properties.IndexOf(prop);
                            groupProps[i].Properties[j].Visibility = prop.Visibility;
                            groupProps[i].Properties[j].Value = prop.Value;
                        }
                    }
                }
                
                #region
                foreach (var child in Properties.ComponentOutsUsing)
                {
                    if (child.ParentId == id && !first)
                    {
                        var component = ManageEnums.Instance.GetComponent(child.Name); component.Selected = true;
                        component.OnConfiguOut(Properties.ComponentOutsUsing.IndexOf(child)); component.Selected = false;
                        
                        grid.Children.Add(component.ComponentView!);
                        Children.Add(component);
                        LayoutConstraints(Children.Count - 1);
                        if (Selected) break;
                    }
                }
                #endregion
            }
            else
            {
                foreach(var child in Children)
                {
                    child.OnConfiguOut(id);
                }
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
            if(value != null!)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerialiser>(value)!;

            if (Selected && isPaste && valueD != null!)
            {
                var name = valueD.Name!;
                var component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this; component.Added = true;
                component.OnDeserialiser(valueD);

                var expanded = false;
                foreach (var child in Children)
                {
                    var w = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                    expanded = expanded || w == SizeValue.Expand.ToString();
                }

                grid.Children.Add(component.ComponentView!);
                Children.Add(component);

                LayoutConstraints(Children.Count - 1, false, expanded);
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerialiser());
            else if (!Selected)
                foreach (var child in Children)
                {
                    var comp = child.OnCopyOrPaste(value!, isPaste);
                    if (comp != null!) return comp;
                }
            return null!;
        }

        public override void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
            }
            else if (structuralElement.Children != null! && structuralElement.Children.Count == Children.Count)
            {
                for (var i = 0; i < structuralElement.Children!.Count; i++)
                {
                    Children[i].SelectFromStructuralView(structuralElement.Children[i]);
                }
            }
        }

        public override StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = EnumName.ToString()!;
            structuralElement.Selected = Selected;
            structuralElement.Children = new List<StructuralElement>();
            foreach (var child in Children)
                structuralElement.Children.Add(child.AddToStructuralView());

            return structuralElement;
        }

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            #region Contraintes de mise en page
            var cd = new ColumnDefinition(); cd.Width = new GridLength(0, GridUnitType.Auto);

            var component = Children[id];
            if(!isDeserialize)
            {
                component.Parent = this;
                //Restrictions actuelles de columns
                component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Visible.ToString());
                component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.VT, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.VC, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.VB, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Global, PropertyNames.MoveLeft, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Global, PropertyNames.MoveRight, VisibilityValue.Visible.ToString(), false);
                component.OnAddConfig(GroupNames.Global, PropertyNames.Trash, VisibilityValue.Visible.ToString(), false);
                component.OnInitialized();
            }

            var nl = grid.ColumnDefinitions.Count;
            var nc = Children.Count - 1;
            var vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            var vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            var vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;

            var sb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value;
            var sa = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value;
            var se = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value;

            var hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            var hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            var hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;

            var w = component.groupProps![component.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[component.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
            var h = component.groupProps![component.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[component.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;

            var vte = component.groupProps![component.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[component.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            var vce = component.groupProps![component.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[component.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            var vbe = component.groupProps![component.Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[component.Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;

            if (vt == "1" || vte == "1") component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
            else if (vc == "1" || vce == "1") component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
            else if (vb == "1" || vbe == "1") component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
            else if (!isDeserialize && vt == "0" && vc == "0" && vb == "0" && h != SizeValue.Expand.ToString())
            {
                component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
            }


            if ((nl == nc + 1 && hl == "1") || (isDeserialize && hl == "1"))
            {
                Grid.SetColumn(component.ComponentView!, isDeserialize ? id : grid.ColumnDefinitions.Count - 1);
                grid.ColumnDefinitions.Insert(isDeserialize ? id : grid.ColumnDefinitions.Count - 1, cd);
                if (!isDeserialize && w == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                else if(isDeserialize && id == columnCount - 1)
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            else if ((nl == nc + 1 && hr == "1") && (isDeserialize && hr == "1"))
            {
                if (isDeserialize && id == 0)
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                grid.ColumnDefinitions.Add(cd);
                Grid.SetColumn(component.ComponentView!, isDeserialize ? id + 1 : grid.ColumnDefinitions.Count - 1);
                if (!isDeserialize && w == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
            }
            else if ((nl == nc + 2 && hc == "1") || (isDeserialize && hc == "1"))
            {
                if (isDeserialize && id == 0)
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                if(isDeserialize) grid.ColumnDefinitions.Add(cd);

                Grid.SetColumn(component.ComponentView!, isDeserialize ? id + 1 : grid.ColumnDefinitions.Count);
                if(isDeserialize) grid.ColumnDefinitions.Insert(grid.ColumnDefinitions.Count - 1, cd);
                if (!isDeserialize && w == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                else if (isDeserialize && id == columnCount - 1)
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            else if (sb == "1")
            {
                if (isDeserialize && id == 0) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                else if (isDeserialize && id == columnCount - 1) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
                else if(isDeserialize) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);

                if (!isDeserialize && Children.Count > 0)
                    Children[Children.Count - 1].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
                cd.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Insert(isDeserialize ? id : grid.ColumnDefinitions.Count - 1, cd);
                Grid.SetColumn(component.ComponentView!, isDeserialize ? id : grid.ColumnDefinitions.Count - 1);
                if(!isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
            }
            else if (sa == "1")
            {
                cd.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Insert(isDeserialize ? id : grid.ColumnDefinitions.Count - 1, cd);
                Grid.SetColumn(component.ComponentView!, isDeserialize ? id : grid.ColumnDefinitions.Count - 1);
                component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
            }
            else if (se == "1")
            {
                if (isDeserialize && id == 0) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
                else if (isDeserialize && id == columnCount - 1) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                else if (isDeserialize) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);

                if (!isDeserialize && Children.Count > 0)
                    Children[Children.Count - 1].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
                cd.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Insert(isDeserialize ? id : grid.ColumnDefinitions.Count - 1, cd);
                Grid.SetColumn(component.ComponentView!, isDeserialize ? id : grid.ColumnDefinitions.Count - 1);
                if (!isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
            }
            else if (nl == nc)
            {
                if (w == SizeValue.Expand.ToString())
                {
                    cd.Width = new GridLength(1, GridUnitType.Star);
                    grid.ColumnDefinitions.Add(cd);
                    Grid.SetColumn(component.ComponentView!, isDeserialize ? id : grid.ColumnDefinitions.Count - 1);
                }
                else if ((w == SizeValue.Auto.ToString() || w != SizeValue.Old.ToString()) && existExpaned)
                {
                    grid.ColumnDefinitions.Add(cd);
                    Grid.SetColumn(component.ComponentView!, isDeserialize ? id : grid.ColumnDefinitions.Count - 1);
                }
                else
                {
                    Grid.SetColumn(component.ComponentView!, isDeserialize ? id : grid.ColumnDefinitions.Count);
                    grid.ColumnDefinitions.Add(cd);
                    ColumnDefinition cd2 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd2);
                    groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value = "1";
                    //OnSelected();
                }
            }
            
            if (Children[id].AllowFixSize() && hl == "1" && (w == SizeValue.Auto.ToString() || w == SizeValue.Expand.ToString()))
                Children[id].OnUpdated(GroupNames.Transform, PropertyNames.Width, "20", true);
            #endregion
        }

        public override bool AllowFixSize(bool isHeight = true)
        {
            return true;
        }

        public override void UpdateComponent(List<ComponentRef> refs, GroupNames groupName, PropertyNames propertyName, string value, int i = 0, bool found = false)
        {
            for (var k = 0; k < Children.Count; k++)
            {
                if (found && refs[i].ControlName == Children[k].EnumName && refs[i].ControlPosition == k)
                {
                    Children[k].OnUpdated(groupName, propertyName, value, true);
                }
                else
                {
                    if (Children[k].GetType() == typeof(ComponentModel))
                        found = true;
                    Children[k].UpdateComponent(refs, groupName, propertyName, value, i, found);
                }
            }
        }

        public override void AddComponent(List<ComponentRef> refs, string data, int i = 0, bool found = false)
        {
            for (var k = 0; k < Children.Count; k++)
            {
                if (found && refs[i].ControlName == Children[k].EnumName && refs[i].ControlPosition == k)
                {
                    Children[k].OnCopyOrPaste(data, true);
                }
                else
                {
                    if (Children[k].GetType() == typeof(ComponentModel))
                        found = true;
                    Children[k].AddComponent(refs, data, i, found);
                }
            }
        }

        public override void DeleteComponent(List<ComponentRef> refs, int i = 0, bool found = false)
        {
            for (var k = 0; k < Children.Count; k++)
            {
                if (found && refs[i].ControlName == Children[k].EnumName && refs[i].ControlPosition == k)
                {
                    _deleteChild(k);
                }
                else
                {
                    if (Children[k].GetType() == typeof(ComponentModel))
                        found = true;
                    Children[k].DeleteComponent(refs, i, found);
                }
            }
        }

        public override List<ComponentRef> BuildComponentRefs(int i)
        {
            if (Selected)
                return new List<ComponentRef> { new() { ControlName = EnumName, ControlPosition = i } };

            List<ComponentRef> compRefs = null!;
            for (var k = 0; k < Children.Count; k++)
            {
                compRefs = Children[k].BuildComponentRefs(k);

                if (compRefs == null!) continue;
                compRefs.Insert(0, new ComponentRef { ControlName = EnumName, ControlPosition = i });
                break;
            }
            return compRefs!;
        }

        public override ReactComponent BuildReactComponent(string tab, int n, string id)
        {
            #region build react component
            var comp = new ReactComponent();
            var imports = new Dictionary<string, Dictionary<string, bool>> {
                {"react-native", new Dictionary<string, bool>{ { "StyleSheet", false }, { "View", false}}}
            };
            comp.Imports = imports;
            var childComps = new List<ReactComponent>();
            var childrenBody = "";
            tab = tab != "" ? Tabs(n + 1) + tab + "\n" : "";
            for (var i = 0; i < Children.Count; i++)
            {
                childComps.Add(Children[i].BuildReactComponent("", n + 1, id + i));
                childrenBody += childComps[i].Body + (i != Children.Count - 1 ? "\n" : "");
            }
            var styleName = $"column{id}";
            comp.Body = $"{Tabs(n)}<View \n{Tabs(n+1)}style={"{styles."}{styleName}{"}"}>\n{childrenBody}\n{tab}{Tabs(n)}</View>";
            #region
            const string invalide = "invalide";
            /* Content alignement */
            string hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            string hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            string hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
            string sb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value;
            string sa = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value;
            string se = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value;
            string justifyContent = hl == "1" ? "flex-start" : (hc == "1" ? "center" : (hr == "1" ? "flex-end" :
                                   (sb == "1" ? "space-between" : (sa == "1" ? "space-around" : (se == "1" ? "space-evenly" : invalide)))));
            string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
            string alignItems = vt == "1" ? "flex-start" : (vc == "1" ? "center" : (vb == "1" ? "flex-end" : invalide));
            /* Self alignement */
            string selfAlign;
            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                string hls = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                string hcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                string hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                selfAlign = hls == "1" ? "flex-start" : (hcs == "1" ? "center" : (hrs == "1" ? "flex-end" : invalide));
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                string vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                string vcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                string vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                selfAlign = vts == "1" ? "flex-start" : (vcs == "1" ? "center" : (vbs == "1" ? "flex-end" : invalide));
            }
            else selfAlign = invalide;
            /* Transform */
            string wd = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
            string hd = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
            string width;
            string height;
            string flex = invalide;
            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                flex = hd == SizeValue.Expand.ToString() ? "1" : invalide;
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                flex = wd == SizeValue.Expand.ToString() ? "1" : invalide;
            }
            else if(Parent != null && Parent!.EnumName == ComponentList.ListV)
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
            }
            else
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                flex = wd == SizeValue.Expand.ToString() || hd == SizeValue.Expand.ToString() ? "1" : invalide;
            }
            /* Appearance */
            string backgroundColor = Tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            string cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            string margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            string marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            string marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            string marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            string marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;

            string cpadding = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CPadding.ToString()]].Value;
            string padding = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Padding.ToString()]].Value;
            string paddingLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingLeft.ToString()]].Value;
            string paddingTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingTop.ToString()]].Value;
            string paddingRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingRight.ToString()]].Value;
            string paddingBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingBottom.ToString()]].Value;

            string cborder = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CBorder.ToString()]].Value;
            string borderWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderWidth.ToString()]].Value;
            string borderColor = Tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderColor.ToString()]].Value);
            string borderLeftWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderLeftWidth.ToString()]].Value;
            string borderTopWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderTopWidth.ToString()]].Value;
            string borderRightWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRightWidth.ToString()]].Value;
            string borderBottomWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderBottomWidth.ToString()]].Value;

            string cborderRadius = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CBorderRadius.ToString()]].Value;
            string borderRadius = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadius.ToString()]].Value;
            string borderRadiusTopLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusTopLeft.ToString()]].Value;
            string borderRadiusTopRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusTopRight.ToString()]].Value;
            string borderRadiusBottomLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusBottomLeft.ToString()]].Value;
            string borderRadiusBottomRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusBottomRight.ToString()]].Value;
            #endregion
            comp.Styles = new Dictionary<string, Dictionary<string, string>> {
                {
                    styleName,
                    new Dictionary<string, string>
                    {
                        /* Content alignement */
                        { "flexDirection", "'row'" },
                        { "justifyContent", justifyContent != invalide ? "'"+justifyContent+"'" : invalide },
                        { "alignItems", alignItems != invalide ? "'"+alignItems+"'" : invalide },
                        /* Self alignement */
                        { "alignSelf", selfAlign != invalide ? "'"+selfAlign+"'" : selfAlign },
                        /* Transform */
                        { "flex", flex},
                        { "width", width },
                        { "height", height },
                        /* Appearance */
                        { "backgroundColor", "'"+backgroundColor+"'" },
                        { "margin", cmargin == "1" && margin != "0" ? margin : invalide },
                        { "marginLeft", cmargin == "0" && marginLeft != "0" ? marginLeft : invalide },
                        { "marginTop", cmargin == "0" && marginTop != "0" ? marginTop : invalide },
                        { "marginRight", cmargin == "0" && marginRight != "0" ? marginRight : invalide },
                        { "marginBottom", cmargin == "0" && marginBottom != "0" ? marginBottom : invalide },

                        { "padding", cpadding == "1" && padding != "0" ? padding : invalide },
                        { "paddingLeft", cpadding == "0" && paddingLeft != "0" ? paddingLeft : invalide },
                        { "paddingTop", cpadding == "0" && paddingTop != "0" ? paddingTop : invalide },
                        { "paddingRight", cpadding == "0" && paddingRight != "0" ? paddingRight : invalide },
                        { "paddingBottom", cpadding == "0" && paddingBottom != "0" ? paddingBottom : invalide },

                        { "borderColor", borderWidth == "0" || (cborder == "0" && borderWidth == "0" && borderWidth == "0" && borderWidth == "0" && borderWidth == "0") ? invalide : "'"+borderColor+"'" },
                        { "borderWidth", cborder == "1" && borderWidth != "0" ? borderWidth : invalide },
                        { "borderLeftWidth", cborder == "0" && borderLeftWidth != "0" ? borderLeftWidth : invalide },
                        { "borderTopWidth", cborder == "0" && borderTopWidth != "0" ? borderTopWidth : invalide },
                        { "borderRightWidth", cborder == "0" && borderRightWidth != "0" ? borderRightWidth : invalide },
                        { "borderBottomWidth", cborder == "0" && borderBottomWidth != "0" ? borderBottomWidth : invalide },

                        { "borderRadius", cborderRadius == "1" && borderRadius != "0" ? borderRadius : invalide },
                        { "borderRadiusTopLeft", cborderRadius == "0" && borderRadiusTopLeft != "0" ? borderRadiusTopLeft : invalide },
                        { "borderRadiusTopRight", cborderRadius == "0" && borderRadiusTopRight != "0" ? borderRadiusTopRight : invalide },
                        { "borderRadiusBottomLeft", cborderRadius == "0" && borderRadiusBottomLeft != "0" ? borderRadiusBottomLeft : invalide },
                        { "borderRadiusBottomRight", cborderRadius == "0" && borderRadiusBottomRight != "0" ? borderRadiusBottomRight : invalide },
                    }
                }
            };
            for (var i = 0; i < childComps.Count; i++)
            {
                foreach (var key in childComps[i]!.Styles!.Keys)
                    comp.Styles.Add(key, childComps[i].Styles![key]);
                comp.AddImports(childComps[i].Imports!);
            }
            return comp;
            #endregion
        }

        public override WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams)
        {
            #region Build web component
            var comp = new WebComponent();
            var childComps = new List<WebComponent>();
            #region
            double w = Width(this), h = Height(this);
            double pw = Parent!.Width(Parent), ph = Parent.Height(Parent);
            double childrenWidth = 0;
            foreach (var child in Children)
            {
                childrenWidth += child.Width(child);
            }

            double bourrageWidth = 0;
            string bourrageStyleName = $"bourrage{id}";

            const string invalide = "invalide";
            /* Content alignement */
            string hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            string hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            string hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
            string sb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value;
            string sa = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value;
            string se = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value;

            if (hl == "1" || hr == "1") bourrageWidth = w - childrenWidth;
            else if (hc == "1") bourrageWidth = (w - childrenWidth) / 2;
            else if (sb == "1" || sa == "1" || se == "1") bourrageWidth = w / Children.Count;
            if (hl == "1" || hc == "1" || hr == "1" || sb == "1" || sa == "1" || se == "1")
            {
                styles!.Add(
                    bourrageStyleName,
                    new Dictionary<string, string> {
                        { "float", "left" },
                        { "width", $"{bourrageWidth}px".Replace(",", ".") },
                        { "height", $"{h}px".Replace(",", ".") },
                    }
                );
            }
            #endregion
            var styleName = $"column{id}";
            double cmarginLeft = 0, cmarginTop = 0;
            #region
            /* Self alignement */
            var position = invalide;
            var left = invalide;
            var top = invalide;
            var floatv = invalide;
            var hls = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            var hcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            var hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;

            var vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            var vcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            var vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;

            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                cmarginLeft = hls == "1" ? 0 : (hcs == "1" ? (pw - w) / 2 : (hrs == "1" ? pw - w : 0));
                var i = Convert.ToInt32(lparams[2]);
                var end = Convert.ToBoolean(lparams[3]);
                switch (lparams[0])
                {
                    case "sb" when i == 0:
                        cmarginTop = 0;
                        break;
                    case "sb" when end:
                        cmarginTop = Convert.ToDouble(lparams[1]) - h;
                        break;
                    case "sb":
                    case "sa":
                        cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                        break;
                    default:
                    {
                        if (lparams[0] == "sa")
                        {
                            if (i == 0) cmarginTop = Convert.ToDouble(lparams[1]) - h;
                            else if (end) cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                            else cmarginTop = 0;
                        }

                        break;
                    }
                }
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                cmarginTop = vts == "1" ? 0 : (vcs == "1" ? (ph - h) / 2 : (vbs == "1" ? ph - h : 0));
                var i = Convert.ToInt32(lparams[2]);
                var end = Convert.ToBoolean(lparams[3]);
                switch (lparams[0])
                {
                    case "sb" when i == 0:
                        cmarginLeft = 0;
                        break;
                    case "sb" when end:
                        cmarginLeft = Convert.ToDouble(lparams[1]) - w;
                        break;
                    case "sb":
                    case "sa":
                        cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                        break;
                    default:
                    {
                        if (lparams[0] == "sa")
                        {
                            if (i == 0) cmarginLeft = Convert.ToDouble(lparams[1]) - w;
                            else if (end) cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                            else cmarginLeft = 0;
                        }
                        else floatv = "left";

                        break;
                    }
                }
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
            var backgroundColor = Tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            var cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            var margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            var marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            var marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            var marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            var marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;

            var cpadding = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CPadding.ToString()]].Value;
            var padding = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Padding.ToString()]].Value;
            var paddingLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingLeft.ToString()]].Value;
            var paddingTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingTop.ToString()]].Value;
            var paddingRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingRight.ToString()]].Value;
            var paddingBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingBottom.ToString()]].Value;

            var cborder = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CBorder.ToString()]].Value;
            var borderWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderWidth.ToString()]].Value;
            var borderColor = Tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderColor.ToString()]].Value);
            var borderLeftWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderLeftWidth.ToString()]].Value;
            var borderTopWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderTopWidth.ToString()]].Value;
            var borderRightWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRightWidth.ToString()]].Value;
            var borderBottomWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderBottomWidth.ToString()]].Value;

            var cborderRadius = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CBorderRadius.ToString()]].Value;
            var borderRadius = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadius.ToString()]].Value;
            var borderRadiusTopLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusTopLeft.ToString()]].Value;
            var borderRadiusTopRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusTopRight.ToString()]].Value;
            var borderRadiusBottomLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusBottomLeft.ToString()]].Value;
            var borderRadiusBottomRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRadiusBottomRight.ToString()]].Value;
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

                        { "padding", cpadding == "1" && padding != "0" ? padding+"px" : invalide },
                        { "padding-left", cpadding == "0" && paddingLeft != "0" ? paddingLeft+"px" : invalide },
                        { "padding-top", cpadding == "0" && paddingTop != "0" ? paddingTop+"px" : invalide },
                        { "padding-right", cpadding == "0" && paddingRight != "0" ? paddingRight+"px" : invalide },
                        { "padding-bottom", cpadding == "0" && paddingBottom != "0" ? paddingBottom+"px" : invalide },

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

                        { "border-radius", cborderRadius == "1" && borderRadius != "0" ? borderRadius+"px" : invalide },
                        { "border-top-left-radius", cborderRadius == "0" && borderRadiusTopLeft != "0" ? borderRadiusTopLeft+"px" : invalide },
                        { "border-top-right-radius", cborderRadius == "0" && borderRadiusTopRight != "0" ? borderRadiusTopRight+"px" : invalide },
                        { "border-bottom-left-radius", cborderRadius == "0" && borderRadiusBottomLeft != "0" ? borderRadiusBottomLeft+"px" : invalide },
                        { "border-bottom-right-radius", cborderRadius == "0" && borderRadiusBottomRight != "0" ? borderRadiusBottomRight+"px" : invalide },
                    }
                }
            };
            #endregion
            tab = tab != "" ? Tabs(n + 1) + tab + "\n" : "";
            var childrenBody = "";
            if (hc == "1") childrenBody += $"\n{Tabs(n + 1)}<div class=\"{bourrageStyleName}\"></div>\n";
            else if (hr == "1") childrenBody += $"\n{Tabs(n + 1)}<div class=\"{bourrageStyleName}\"></div>\n";
            #region
            var mode = hl == "1" ? "hl" : (hc == "1" ? "hc" : (hr == "1" ? "hr" : (sb == "1" ? "sb" : (sa == "1" ? "sa" : (se == "1" ? "se" : "st")))));
            var styles2 = styles;
            for (int i = 0; i < Children.Count; i++)
            {
                var child = Children[i].BuildWebComponent(styles2, "", (sb == "1" || sa == "1" || se == "1") ? n + 2 : n + 1, id + i,
                    new List<string> { mode, $"{bourrageWidth}", $"{i}", $"{i == Children.Count - 1}" });
                childComps.Add(child);
                if (sb == "1" || sa == "1" || se == "1")
                {
                    childrenBody += $"\n{Tabs(n + 1)}<div class=\"{bourrageStyleName}\">\n" +
                        $"{childComps[i].Body + (i != Children.Count - 1 ? "\n" : "")}\n{Tabs(n + 1)}</div>";
                }
                else childrenBody += childComps[i].Body + (i != Children.Count - 1 ? "\n" : "");
                styles2 = child.Styles;
            }
            if (hl == "1") childrenBody += $"\n{Tabs(n + 1)}<div class=\"{bourrageStyleName}\"></div>";
            else if (hc == "1") childrenBody += $"\n{Tabs(n + 1)}<div class=\"{bourrageStyleName}\"></div>";
            comp.Body = $"{Tabs(n)}<div class=\"{styleName}\">\n{childrenBody}\n{tab}{Tabs(n)}</div>";

            var found = false;
            var keyStyle = string.Empty;
            foreach (var key in styles2!.Keys)
            {
                if (comp.EqualStyles(comp.Styles[styleName], styles2[key]))
                {
                    found = true; keyStyle = key;
                }
                comp.Styles.Add(key, styles2[key]);
            }

            if (!found) return comp;
            comp.Styles.Remove(styleName); styleName = keyStyle;
            #endregion
            return comp;
            #endregion
        }

        public override ReactComponent BuildFlutterComponent(string tab, int n, string id)
        {
            var comp = new ReactComponent();
            return comp;
        }

        public override ReactComponent BuildAndroidXmlComponent(string tab, int n, string id)
        {
            var comp = new ReactComponent();
            return comp;
        }

        public override ReactComponent BuildAndroidComposeComponent(string tab, int n, string id)
        {
            var comp = new ReactComponent();
            return comp;
        }

        private string Tabs(int n)
        {
            var tab = "";
            for (int i = 0; i < n; i++)
                tab += "    ";
            return tab;
        }

        private string Tcolor(string color)
        {
            return color.Length == 9 ? "#" + color.Substring(3).ToLower() : color.ToLower();
        }

        public sealed override void LoadIds()
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
            #region Width calcul
            double w = 0, wp = 0;
            var paddingLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingLeft.ToString()]].Value;
            var paddingRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingRight.ToString()]].Value;
            var borderLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderLeftWidth.ToString()]].Value;
            var borderRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRightWidth.ToString()]].Value;
            if (ComponentView!.Width.Equals(double.NaN)) wp = Parent!.Width(this);
            else wp = ComponentView!.Width;
            wp -= (Convert.ToDouble(borderLeft) + Convert.ToDouble(borderRight)) +
                  (Convert.ToDouble(paddingLeft) + Convert.ToDouble(paddingRight));
            if (component.Equals(this)) w = wp;
            else
            {
                var n = 0;
                foreach (var child in Children)
                {
                    var marginLeft = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
                    var marginRight = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
                    wp -= Convert.ToDouble(marginLeft) + Convert.ToDouble(marginRight);
                    if (component.Equals(child))
                    {
                        n++;
                    }
                    else
                    {
                        if (child.ComponentView!.Width.Equals(double.NaN) && child.ComponentView!.ActualWidth == 0) n++;
                        else if (child.ComponentView!.ActualWidth != 0 && child.GetType().ToString() == "ConceptorUI.ViewModels.TextSingleModel") wp -= child.Width(null!);
                        else if (child.ComponentView!.ActualWidth != 0 && child.GetType().ToString() != "ConceptorUI.ViewModels.TextSingleModel") n++;
                        else wp -= child.ComponentView!.Width;
                    }
                }
                w = n > 0 ? wp / n : wp;
            }
            return w;
            #endregion
        }
        
        public override double Height(Component component)
        {
            double h = 0;
            var paddingTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingTop.ToString()]].Value;
            var paddingBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingBottom.ToString()]].Value;
            var borderTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderTopWidth.ToString()]].Value;
            var borderBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderBottomWidth.ToString()]].Value;
            if (ComponentView!.Height.Equals(double.NaN) && component != null!) h = Parent!.Height(this);
            else if (!ComponentView!.Height.Equals(double.NaN)) h = ComponentView!.Height;
            if (component == null || component == this)
                return h - (Convert.ToDouble(borderTop) + Convert.ToDouble(borderBottom)) -
                       (Convert.ToDouble(paddingTop) + Convert.ToDouble(paddingBottom));
            var marginTop= component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            var marginBottom = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
            h -= Convert.ToDouble(marginTop) + Convert.ToDouble(marginBottom);
            return h - (Convert.ToDouble(borderTop) + Convert.ToDouble(borderBottom)) -
                   (Convert.ToDouble(paddingTop) + Convert.ToDouble(paddingBottom));
        }

        public sealed override void OnConfigured()
        {
            #region
            groupProps = new List<GroupProperties>
            {
                new GroupProperties()
                {
                    Name = GroupNames.Alignment.ToString(),
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
                            Value = SizeValue.Expand.ToString(),
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
                            Value = "#FFF5F5F5",
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
                        new Property()
                        {
                            Name = PropertyNames.Opacity.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
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
                        }
                    }
                },
                new ()
                {
                    Name = GroupNames.Shadow.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new ()
                        {
                            Name = PropertyNames.ShadowDepth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.BlurRadius.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.ShadowDirection.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.ShadowColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                    }
                },
            };
            #endregion
        }
    }
}
