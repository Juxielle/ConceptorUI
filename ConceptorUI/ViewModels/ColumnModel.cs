using ConceptorUI.Models;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ConceptorUI.Views.ComponentP;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    internal class ColumnModel : Component
    {
        public ColumnModel()
        {
            ChildContent = new Grid();
            Name = ComponentList.Column;
            
            OnInitialize();
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
            
        }

        protected override void WhenFileLoaded(string value)
        {
            
        }

        protected override void SelfConstraints()
        {
            /* Global */
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment);
            /* Self Alignment */
            /* Transform */
            /* Text */
            SetGroupVisibility(GroupNames.Text);
            /* Appearance */
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
            
        }
        
        protected override void InitChildContent()
        {
            
        }
        
        protected override void AddIntoChildContent(FrameworkElement child)
        {
            
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        protected override void Delete()
        {
            
        }
        
        protected override void WhenWidthChanged()
        {
            
        }
        
        protected override void WhenHeightChanged()
        {
            
        }
        
        protected override void OnMoveLeft()
        {
            
        }
        
        protected override void OnMoveRight()
        {
            
        }
        
        protected override void OnMoveTop()
        {
            
        }
        
        protected override void OnMoveBottom()
        {
            
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

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
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
    }
}
