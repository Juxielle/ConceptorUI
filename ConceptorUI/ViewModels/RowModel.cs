using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    internal class RowModel : Component
    {
        private readonly Grid _grid;
        
        public RowModel()
        {
            _grid = new Grid();
            ChildContent = _grid;
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

                if (Selected || allow)
                {
                    /* Alignement */
                    #region
                    if (propertyName == PropertyNames.HL && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            groupProps![idG].Properties[idP + 2].Value = "0";
                            foreach (var child in Children)
                            {
                                string w = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                                if (value == "1")
                                {
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                                    if (w == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                                }
                                else if (w != SizeValue.Expand.ToString())
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.HC && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            foreach (var child in Children)
                            {
                                string w = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                                if (value == "1")
                                {
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
                                    if (w == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                                }
                                else if (w != SizeValue.Expand.ToString())
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.HR && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP - 2].Value = "0";
                            foreach (var child in Children)
                            {
                                string w = child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                                if (value == "1")
                                {
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
                                    if (w == SizeValue.Expand.ToString()) child.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                                }
                                else if (w != SizeValue.Expand.ToString())
                                    child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.VT && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            //Le cas de désactivation n'est pas prise en compte.
                            int nl = _grid.RowDefinitions.Count;
                            int nc = Children.Count;
                            if (value == "1" && nc != 0 && nl == nc)
                            {
                                foreach (var row in _grid.RowDefinitions)
                                {
                                    row.Height = new GridLength(row.Height.GridUnitType == GridUnitType.Star ? 0 : row.Height.Value,
                                        row.Height.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Height.GridUnitType);
                                }
                                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP + 2].Value == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(0);
                                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 2 && groupProps![idG].Properties[idP + 1].Value == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            groupProps![idG].Properties[idP + 2].Value = "0";
                            groupProps![idG].Properties[idP + 3].Value = "0";
                            groupProps![idG].Properties[idP + 4].Value = "0";
                            groupProps![idG].Properties[idP + 5].Value = "0";
                        }
                    }
                    else if (propertyName == PropertyNames.VC && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            int nl = _grid.RowDefinitions.Count;
                            int nc = Children.Count;
                            if (value == "1" && nc != 0 && nl == nc)
                            {
                                foreach (var row in _grid.RowDefinitions)
                                {
                                    row.Height = new GridLength(row.Height.GridUnitType == GridUnitType.Star ? 0 : row.Height.Value,
                                        row.Height.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Height.GridUnitType);
                                }
                                _grid.RowDefinitions.Insert(0, new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP - 1].Value == "1")
                            {
                                _grid.RowDefinitions.Insert(0, new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP + 1].Value == "1")
                            {
                                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                            }
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            groupProps![idG].Properties[idP + 2].Value = "0";
                            groupProps![idG].Properties[idP + 3].Value = "0";
                            groupProps![idG].Properties[idP + 4].Value = "0";
                        }
                    }
                    else if (propertyName == PropertyNames.VB && groupName == GroupNames.Alignment)
                    {
                        if (Children.Count > 0)
                        {
                            int nl = _grid.RowDefinitions.Count;
                            int nc = Children.Count;
                            if (value == "1" && nc != 0 && nl == nc)
                            {
                                foreach (var row in _grid.RowDefinitions)
                                {
                                    row.Height = new GridLength(row.Height.GridUnitType == GridUnitType.Star ? 0 : row.Height.Value,
                                        row.Height.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Height.GridUnitType);
                                }
                                _grid.RowDefinitions.Insert(0, new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 1 && groupProps![idG].Properties[idP - 2].Value == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                                _grid.RowDefinitions.Insert(0, new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i + 1);
                            }
                            else if (value == "1" && nc != 0 && nl == nc + 2 && groupProps![idG].Properties[idP - 1].Value == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                            }
                            groupProps![idG].Properties[idP].Value = value;
                            groupProps![idG].Properties[idP - 2].Value = "0";
                            groupProps![idG].Properties[idP - 1].Value = "0";
                            groupProps![idG].Properties[idP + 1].Value = "0";
                            groupProps![idG].Properties[idP + 2].Value = "0";
                            groupProps![idG].Properties[idP + 3].Value = "0";
                        }
                    }
                    if (propertyName == PropertyNames.HL && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.HorizontalAlignment = value == "0" ? contentP.HorizontalAlignment : HorizontalAlignment.Left;
                    }
                    else if (propertyName == PropertyNames.HC && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.HorizontalAlignment = value == "0" ? contentP.HorizontalAlignment : HorizontalAlignment.Center;
                    }
                    else if (propertyName == PropertyNames.HR && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.HorizontalAlignment = value == "0" ? contentP.HorizontalAlignment : HorizontalAlignment.Right;
                    }
                    else if (propertyName == PropertyNames.VT && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.VerticalAlignment = value == "0" ? contentP.VerticalAlignment : VerticalAlignment.Top;
                    }
                    else if (propertyName == PropertyNames.VC && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.VerticalAlignment = value == "0" ? contentP.VerticalAlignment : VerticalAlignment.Center;
                    }
                    else if (propertyName == PropertyNames.VB && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        contentP.VerticalAlignment = value == "0" ? contentP.VerticalAlignment : VerticalAlignment.Bottom;
                    }
                    else if (propertyName == PropertyNames.SpaceBetween)
                    {
                        if (Children.Count > 0)
                        {
                            string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                            string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                            string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                            //groupProps![idG].Properties[0].Value = groupProps[idG].Properties[1].Value = groupProps[idG].Properties[2].Value = "0";
                            groupProps[idG].Properties[3].Value = groupProps[idG].Properties[4].Value = groupProps[idG].Properties[5].Value = "0";
                            groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = groupProps[idG].Properties[8].Value = "0";
                            groupProps[idG].Properties[idP].Value = "1";
                            int nl = _grid.RowDefinitions.Count;
                            int nc = Children.Count;
                            if (nl == nc + 1 && vt == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                            }
                            else if (nl == nc + 1 && vb == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(0);
                                for(int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            else if (nl == nc + 2 && vc == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                                _grid.RowDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            foreach (var rd in _grid.RowDefinitions)
                            {
                                rd.Height = new GridLength(1, GridUnitType.Star);
                                int i = _grid.RowDefinitions.IndexOf(rd);
                                if (i == 0) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                                else if (i == _grid.RowDefinitions.Count - 1) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                                else Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.SpaceAround)
                    {
                        if (Children.Count > 0)
                        {
                            string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                            string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                            string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                            //groupProps![idG].Properties[0].Value = groupProps[idG].Properties[1].Value = groupProps[idG].Properties[2].Value = "0";
                            groupProps[idG].Properties[3].Value = groupProps[idG].Properties[4].Value = groupProps[idG].Properties[5].Value = "0";
                            groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = groupProps[idG].Properties[8].Value = "0";
                            groupProps[idG].Properties[idP].Value = "1";
                            int nl = _grid.RowDefinitions.Count;
                            int nc = Children.Count;
                            if (nl == nc + 1 && vt == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                            }
                            else if (nl == nc + 1 && vb == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            else if (nl == nc + 2 && vc == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                                _grid.RowDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            foreach (var rd in _grid.RowDefinitions)
                            {
                                rd.Height = new GridLength(1, GridUnitType.Star);
                                Children[_grid.RowDefinitions.IndexOf(rd)].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.SpaceEvery)
                    {
                        if (Children.Count > 0)
                        {
                            string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                            string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                            string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                            //groupProps![idG].Properties[0].Value = groupProps[idG].Properties[1].Value = groupProps[idG].Properties[2].Value = "0";
                            groupProps[idG].Properties[3].Value = groupProps[idG].Properties[4].Value = groupProps[idG].Properties[5].Value = "0";
                            groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = groupProps[idG].Properties[8].Value = "0";
                            groupProps[idG].Properties[idP].Value = "1";
                            int nl = _grid.RowDefinitions.Count;
                            int nc = Children.Count;
                            if (nl == nc + 1 && vt == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                            }
                            else if (nl == nc + 1 && vb == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            else if (nl == nc + 2 && vc == "1")
                            {
                                _grid.RowDefinitions.RemoveAt(nl - 1);
                                _grid.RowDefinitions.RemoveAt(0);
                                for (int i = 0; i < Children.Count; i++)
                                    Grid.SetRow(Children[i].ComponentView, i);
                            }
                            foreach (var rd in _grid.RowDefinitions)
                            {
                                rd.Height = new GridLength(1, GridUnitType.Star);
                                int i = _grid.RowDefinitions.IndexOf(rd);
                                if (i == 0) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                                else if (i == _grid.RowDefinitions.Count - 1) Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                                else Children[i].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
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
                                groupProps![idGAl].Properties[0].Value = "1";
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
                                groupProps![idGAl].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propertyName == PropertyNames.Height)
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
                    else if (propertyName == PropertyNames.HE || propertyName == PropertyNames.VE)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.MoveParentToChild)
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
                    else if (propertyName == PropertyNames.FillColor)
                    {
                        content.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                      new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.Opacity)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        content.Opacity = vd;
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
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        contentP.Margin = new Thickness(vd);
                    }
                    else if (propertyName == PropertyNames.MarginLeft)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(vd, contentP.Margin.Top, contentP.Margin.Right, contentP.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginTop)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, vd, contentP.Margin.Right, contentP.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginRight)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, vd, contentP.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginBottom)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, contentP.Margin.Right, vd);
                    }
                    else if (propertyName == PropertyNames.CPadding)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.PaddingBtnActif)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.Padding)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        content.Padding = new Thickness(vd);
                    }
                    else if (propertyName == PropertyNames.PaddingLeft)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(vd, content.Padding.Top, content.Padding.Right, content.Padding.Bottom);
                    }
                    else if (propertyName == PropertyNames.PaddingTop)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, vd, content.Padding.Right, content.Padding.Bottom);
                    }
                    else if (propertyName == PropertyNames.PaddingRight)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, vd, content.Padding.Bottom);
                    }
                    else if (propertyName == PropertyNames.PaddingBottom)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, content.Padding.Right, vd);
                    }
                    else if (propertyName == PropertyNames.CBorder)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.BorderBtnActif)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.BorderWidth)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 6].Value =
                        groupProps[idG].Properties[idP + 9].Value = groupProps[idG].Properties[idP + 12].Value = vd + "";
                        content.BorderThickness = new Thickness(vd); Console.WriteLine("Border Width Updating => " + value);
                    }
                    else if (propertyName == PropertyNames.BorderColor)
                    {
                        content.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                      new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.BorderLeftWidth)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(vd, content.BorderThickness.Top, content.BorderThickness.Right, content.BorderThickness.Bottom);
                    }
                    else if (propertyName == PropertyNames.BorderTopWidth)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, vd * 0.65, content.BorderThickness.Right, content.BorderThickness.Bottom);
                    }
                    else if (propertyName == PropertyNames.BorderRightWidth)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, vd, content.BorderThickness.Bottom);
                    }
                    else if (propertyName == PropertyNames.BorderBottomWidth)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, content.BorderThickness.Right, vd * 0.65);
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
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        content.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusTopLeft)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(vd, content.CornerRadius.TopRight,
                                                      content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusBottomLeft)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, content.CornerRadius.TopRight,
                                                      content.CornerRadius.BottomRight, vd);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusTopRight)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, vd,
                                                      content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusBottomRight)
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
                        if (child.Selected)
                        {
                            i = Children.IndexOf(child); break;
                        }
                    }
                    if (i != -1)
                    {
                        var grid = (Grid)(content).Child;
                        var nc = Children.Count;
                        var nl = grid.RowDefinitions.Count;
                        var vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                        var vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                        var vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                        var sb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value;
                        var sa = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value;
                        var se = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value;
                        var fvbc = vb == "1" || vc == "1";
                        if (nc == 1)
                        {
                            grid.RowDefinitions.Clear();
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
                            grid.RowDefinitions.RemoveAt(fvbc ? i + 1 : i);
                        }
                        grid.Children.RemoveAt(i);
                        Children.RemoveAt(i);
                        if (Children.Count > 0)
                            for (int k = i; k < Children.Count; k++)
                                Grid.SetRow(Children[k].ComponentView!, fvbc ? k + 1 : k);

                        OnSelected();
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    var pos2 = GetPosition(GroupNames.Alignment, PropertyNames.SpaceBetween);
                    int idG2 = pos2[0], idP2 = pos2[1];
                    var grid = (Grid)(content).Child;
                    var i = -1;
                    var nl = grid.RowDefinitions.Count;
                    var nc = Children.Count;
                    var vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                    var vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                    var vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                    var sb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value;
                    var sa = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value;
                    var se = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value;
                    var found = false;
                    foreach (var child in Children)
                    {
                        if (child.Selected) i = Children.IndexOf(child);
                        found = found || child.groupProps![child.Ids![GroupNames.Transform.ToString()].IdGroup]
                            .Properties[child.Ids![GroupNames.Transform.ToString()]
                            .Props![PropertyNames.Height.ToString()]].Value == SizeValue.Expand.ToString();
                    }

                    if (i != -1 && value == SizeValue.Expand.ToString())
                    {
                        if (nl == nc + 1 && vt == "1") grid.RowDefinitions.RemoveAt(nl - 1);
                        else if (nl == nc + 1 && vb == "1") grid.RowDefinitions.RemoveAt(0);
                        else if (nl == nc + 2 && vc == "1")
                        {
                            grid.RowDefinitions.RemoveAt(nl - 1);
                            grid.RowDefinitions.RemoveAt(0);
                        }
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceBetween.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceAround.ToString()]].Value = "0";
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.SpaceEvery.ToString()]].Value = "0";
                        foreach (var row in grid.RowDefinitions)
                            row.Height = new GridLength(0, GridUnitType.Auto);
                        grid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
                    }
                    else if (i != -1 && value == SizeValue.Auto.ToString())
                    {
                        i = i == 0 && nl >= nc + 1 && (vb == "1" || vc == "1") ? 1 : i;
                        grid.RowDefinitions[i].Height = new GridLength(0, GridUnitType.Auto);
                    }
                    else if (i != -1 && value != SizeValue.Old.ToString())
                    {
                        if(sb == "0" && sa == "0" && se == "0")
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            i = i == 0 && nl >= nc + 1 && (vb == "1" || vc == "1") ? 1 : i;
                            grid.RowDefinitions[i].Height = new GridLength(0, GridUnitType.Auto);
                        }
                    }
                    if (!found && i != -1)
                    {
                        groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup]
                            .Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]]
                            .Value = "1";
                        Console.WriteLine(@"Entre Ici lors du changement de la hauteur.");
                    }

                }
                else if (propertyName == PropertyNames.MoveTop)
                {
                    var grid = (Grid)(content).Child;
                    var nl = grid.RowDefinitions.Count;
                    var nc = Children.Count;
                    var vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                    var vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                    var vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                    var focus = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.Focus.ToString()]].Value == "1";
                    var k = -1;
                    foreach (var child in Children)
                    {
                        var i = Children.IndexOf(child);
                        if (child.Selected) { k = i; break; }
                    }
                    if (k != -1 && (nl == nc || (nl == nc + 1 && vt == "1" && k > 0) || (nl == nc + 1 && vb == "1" && k > 0) || (nl == nc + 2 && vc == "1" && k > 0)))
                    {
                        if (focus)
                        {
                            var fvbc = vb == "1" || vc == "1";
                            var child = Children[k];
                            var rd = grid.RowDefinitions[fvbc ? k + 1 : k];
                            Children.RemoveAt(k); Children.Insert(k - 1, child);
                            grid.Children.RemoveAt(k); grid.Children.Insert(k - 1, child.ComponentView!);
                            grid.RowDefinitions.RemoveAt(fvbc ? k + 1 : k); grid.RowDefinitions.Insert(fvbc ? k : k - 1, rd);
                            Grid.SetRow(Children[k - 1].ComponentView!, fvbc ? k : k - 1); Grid.SetRow(Children[k].ComponentView!, fvbc ? k + 1 : k);
                        }
                        else
                        {
                            Children[k].OnUnselected();
                            Children[k-1].OnSelected();
                        }
                    }
                }
                else if (propertyName == PropertyNames.MoveBottom)
                {
                    var grid = (Grid)(content).Child;
                    var nl = grid.RowDefinitions.Count;
                    var nc = Children.Count;
                    var vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                    var vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                    var vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                    var focus = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.Focus.ToString()]].Value == "1";
                    var k = -1;
                    foreach (var child in Children)
                    {
                        int i = Children.IndexOf(child);
                        if (child.Selected) { k = i; break; }
                    }
                    if (k != -1 && (nl == nc || (nl == nc + 1 && vb == "1" && k < nc - 1) || (nl == nc + 1 && vt == "1" && k < nc - 1) || (nl == nc + 2 && vc == "1" && k < nc - 1)))
                    {
                        if (focus)
                        {
                            var fvbc = vb == "1" || vc == "1";
                            var child = Children[k];
                            var rd = grid.RowDefinitions[fvbc ? k + 1 : k];
                            Children.RemoveAt(k); Children.Insert(k + 1, child);
                            grid.Children.RemoveAt(k); grid.Children.Insert(k + 1, child.ComponentView!);
                            grid.RowDefinitions.RemoveAt(fvbc ? k + 1 : k); grid.RowDefinitions.Insert(fvbc ? k + 2 : k + 1, rd);
                            Grid.SetRow(Children[k + 1].ComponentView!, fvbc ? k + 2 : k + 1); Grid.SetRow(Children[k].ComponentView!, fvbc ? k + 1 : k);
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

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            #region Contraintes de mise en page
            Children[id].Parent = this;
            /* Global */
            Children[id].SetGroupVisibility(GroupNames.Global);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveLeft, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveRight, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            
            /* Content Alignment */
            /* Self Alignment */
            Children[id].SetGroupVisibility(GroupNames.SelfAlignment);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.VT, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.VC, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, PropertyNames.VB, false);
            
            /* Transform */
            Children[id].SetGroupVisibility(GroupNames.Transform);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.ROT, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);
            
            /* Appearance */
            Children[id].SetGroupVisibility(GroupNames.Appearance);
            /* Shadow */
            
            Children[id].OnInitialize();
            Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
            
            var w = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if(w != SizeValue.Expand.ToString() && Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Vertical"))
                Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
            
            var rd = new RowDefinition();
            rd.Height = new GridLength(0, GridUnitType.Auto);

            var component = Children[id];

            var nl = _grid.RowDefinitions.Count;
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

            var w = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            var h = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            
            if (hl == "1") component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, hl, true);
            if (hc == "1") component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, hc, true);
            if (hr == "1") component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, hr, true);
            else if (!isDeserialize && hl == "0" && hc == "0" && hr == "0" && w != SizeValue.Expand.ToString())
            {
                component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
            }


            if ((nl == nc + 1 && vt == "1") || (isDeserialize && vt == "1"))
            {
                Grid.SetRow(component.ComponentView!, isDeserialize ? id : _grid.RowDefinitions.Count - 1);
                _grid.RowDefinitions.Insert(isDeserialize ? id : _grid.RowDefinitions.Count - 1, rd);
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                else if (isDeserialize && id == AddedChildrenCount - 1)
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            else if ((nl == nc + 1 && vb == "1") && (isDeserialize && vb == "1"))
            {
                if (isDeserialize && id == 0)
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                _grid.RowDefinitions.Add(rd);
                Grid.SetRow(component.ComponentView!, isDeserialize ? id + 1 : _grid.RowDefinitions.Count - 1);
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
            }
            else if ((nl == nc + 2 && vc == "1") || (isDeserialize && vc == "1"))
            {
                if (isDeserialize && id == 0)
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                if (isDeserialize) _grid.RowDefinitions.Add(rd);

                Grid.SetRow(component.ComponentView!, isDeserialize ? id + 1 : _grid.RowDefinitions.Count);
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                else if (isDeserialize && id == AddedChildrenCount - 1)
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            else if (sb == "1")
            {
                if (isDeserialize && id == 0) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                else if (isDeserialize && id == AddedChildrenCount - 1) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                else if (isDeserialize) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);

                if (!isDeserialize && Children.Count > 0)
                    Children[^1].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
                rd.Height = new GridLength(1, GridUnitType.Star);
                _grid.RowDefinitions.Insert(isDeserialize ? id : _grid.RowDefinitions.Count - 1, rd);
                Grid.SetRow(component.ComponentView!, isDeserialize ? id : _grid.RowDefinitions.Count - 1);
                if (!isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
            }
            else if (sa == "1")
            {
                rd.Height = new GridLength(1, GridUnitType.Star);
                _grid.RowDefinitions.Insert(isDeserialize ? id : _grid.RowDefinitions.Count - 1, rd);
                Grid.SetRow(component.ComponentView!, isDeserialize ? id : _grid.RowDefinitions.Count - 1);
                component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true);
            }
            else if (se == "1")
            {
                if (isDeserialize && id == 0) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                else if (isDeserialize && id == AddedChildrenCount - 1) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
                else if (isDeserialize) component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);

                if (!isDeserialize && Children.Count > 0)
                    Children[^1].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
                rd.Height = new GridLength(1, GridUnitType.Star);
                _grid.RowDefinitions.Insert(isDeserialize ? id : _grid.RowDefinitions.Count - 1, rd);
                Grid.SetRow(component.ComponentView!, isDeserialize ? id : _grid.RowDefinitions.Count - 1);
                if (!isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, "1", true);
            }
            else if (nl == nc)
            {
                if (h == SizeValue.Expand.ToString())
                {
                    rd.Height = new GridLength(1, GridUnitType.Star);
                    _grid.RowDefinitions.Add(rd);
                    Grid.SetRow(component.ComponentView!, isDeserialize ? id : _grid.RowDefinitions.Count - 1);
                }
                else if ((h == SizeValue.Auto.ToString() || h != SizeValue.Old.ToString()) && existExpaned)
                {
                    _grid.RowDefinitions.Add(rd);
                    Grid.SetRow(component.ComponentView!, isDeserialize ? id : _grid.RowDefinitions.Count - 1);
                }
                else
                {
                    Grid.SetRow(component.ComponentView!, isDeserialize ? id : _grid.RowDefinitions.Count);
                    _grid.RowDefinitions.Add(rd);
                    var cd2 = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                    _grid.RowDefinitions.Add(cd2);
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.VT, "1");
                }
            }
            
            if (vt == "1" && (h == SizeValue.Auto.ToString() || h == SizeValue.Expand.ToString()))
                Children[id].OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
            #endregion
        }
    }
}
