using ConceptorUI.Models;
using System.Linq;
using System.Windows.Controls;
using System.Windows;


namespace ConceptorUi.ViewModels
{
    internal class RowModel : Component
    {
        private readonly Grid _grid;
        
        public RowModel(bool isVertical = true)
        {
            _grid = new Grid();
            IsVertical = isVertical;
            
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
            #region When Alignment Changed
            if ((IsVertical && propertyName is PropertyNames.HL or PropertyNames.HC or PropertyNames.HR) || 
                propertyName is PropertyNames.VT or PropertyNames.VC or PropertyNames.VB)
            {
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.HL : PropertyNames.VT, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.HC : PropertyNames.VC, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.HR : PropertyNames.VB, "0");
                SetPropertyValue(GroupNames.Alignment, propertyName, value);
                
                foreach (var child in Children)
                {
                    var d = child.GetGroupProperties(GroupNames.Transform).GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);
                    if (value == "1")
                    {
                        child.OnUpdated(GroupNames.SelfAlignment, propertyName, "1", true);
                        if (d == SizeValue.Expand.ToString())
                            child.OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Width : PropertyNames.Height, SizeValue.Auto.ToString(), true);
                    }
                    else if (d != SizeValue.Expand.ToString())
                        child.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HL : PropertyNames.VT, "1", true);
                }
            }
            else if ((IsVertical && propertyName == PropertyNames.VT) || propertyName == PropertyNames.HL)
            {
                if (Children.Count > 0)
                {
                    var nl = GetSpaceCount();
                    var nc = Children.Count;
                    
                    if (value == "1" && nc != 0 && nl == nc)
                    {
                        if(IsVertical)
                            foreach (var row in _grid.RowDefinitions)
                                row.Height = new GridLength(row.Height.GridUnitType == GridUnitType.Star ? 0 : row.Height.Value,
                                    row.Height.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Height.GridUnitType);
                        else
                            foreach (var column in _grid.ColumnDefinitions)
                                column.Width = new GridLength(column.Width.GridUnitType == GridUnitType.Star ? 0 : column.Width.Value,
                                    column.Width.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : column.Width.GridUnitType);
                        
                        AddSpace(new GridLength(1, GridUnitType.Star), 0);
                    }
                    else if (value == "1" && nc != 0 && nl == nc + 1 && GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR) == "1")
                    {
                        _grid.RowDefinitions.RemoveAt(0);
                        RemoveSpace(0);
                        AddSpace(new GridLength(1, GridUnitType.Star), 0);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    else if (value == "1" && nc != 0 && nl == nc + 2 && GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC) == "1")
                    {
                        RemoveSpace(0);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
            }
            else if ((IsVertical && propertyName == PropertyNames.VC) || propertyName == PropertyNames.HC)
            {
                if (Children.Count > 0)
                {
                    var nl = GetSpaceCount();
                    var nc = Children.Count;
                    
                    if (value == "1" && nc != 0 && nl == nc)
                    {
                        if (IsVertical)
                            foreach (var row in _grid.RowDefinitions)
                                row.Height = new GridLength(row.Height.GridUnitType == GridUnitType.Star ? 0 : row.Height.Value,
                                    row.Height.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Height.GridUnitType);
                        else
                            foreach (var column in _grid.ColumnDefinitions)
                                column.Width = new GridLength(column.Width.GridUnitType == GridUnitType.Star ? 0 : column.Width.Value,
                                    column.Width.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : column.Width.GridUnitType);
                        
                        AddSpace(new GridLength(1, GridUnitType.Star), 0, false);
                        AddSpace( new GridLength(1, GridUnitType.Star), 0);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i + 1, Children[i].ComponentView);
                    }
                    else if (value == "1" && nc != 0 && nl == nc + 1 && GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL) == "1")
                    {
                        AddSpace(new GridLength(1, GridUnitType.Star) , 0, false);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i + 1, Children[i].ComponentView);
                    }
                    else if (value == "1" && nc != 0 && nl == nc + 1 && GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR) == "1")
                        AddSpace(new GridLength(1, GridUnitType.Star) , 0);
                    
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
            }
            else if ((IsVertical && propertyName == PropertyNames.VB) || propertyName == PropertyNames.HR)
            {
                if (Children.Count > 0)
                {
                    var nl = GetSpaceCount();
                    var nc = Children.Count;
                    
                    if (value == "1" && nc != 0 && nl == nc)
                    {
                        if(IsVertical)
                            foreach (var row in _grid.RowDefinitions)
                                row.Height = new GridLength(row.Height.GridUnitType == GridUnitType.Star ? 0 : row.Height.Value,
                                    row.Height.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : row.Height.GridUnitType);
                        else
                            foreach (var column in _grid.ColumnDefinitions)
                                column.Width = new GridLength(column.Width.GridUnitType == GridUnitType.Star ? 0 : column.Width.Value,
                                    column.Width.GridUnitType == GridUnitType.Star ? GridUnitType.Auto : column.Width.GridUnitType);
                        
                        AddSpace(new GridLength(1, GridUnitType.Star), 0, false);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i + 1, Children[i].ComponentView);
                    }
                    else if (value == "1" && nc != 0 && nl == nc + 1 && GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.VT) == "1")
                    {
                        RemoveSpace(nl - 1);
                        AddSpace(new GridLength(1, GridUnitType.Star), 0, false);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i + 1, Children[i].ComponentView);
                    }
                    else if (value == "1" && nc != 0 && nl == nc + 2 && GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.VC) == "1")
                        RemoveSpace(nl - 1);
                    
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
            }
            else if (propertyName == PropertyNames.SpaceBetween)
            {
                if (Children.Count > 0)
                {
                    var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
                    var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
                    var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);
                    
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
                    
                    var nl = GetSpaceCount();
                    var nc = Children.Count;
                    
                    if (nl == nc + 1 && vt == "1")
                    {
                        RemoveSpace(nl - 1);
                    }
                    else if (nl == nc + 1 && vb == "1")
                    {
                        RemoveSpace(0);
                        for(var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    else if (nl == nc + 2 && vc == "1")
                    {
                        RemoveSpace(nl - 1);
                        RemoveSpace(0);
                        
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    
                    for (var i = 0; i < (IsVertical ? _grid.RowDefinitions.Count : _grid.ColumnDefinitions.Count); i++)
                    {
                        if(IsVertical) _grid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
                        else _grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                        
                        if (i == 0)
                            Children[i].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1", true);
                        else if (i == (IsVertical ? _grid.RowDefinitions.Count : _grid.ColumnDefinitions.Count) - 1)
                            Children[i].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VB : PropertyNames.HC, "1", true);
                        else
                            Children[i].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HR, "1", true);
                    }
                }
            }
            else if (propertyName == PropertyNames.SpaceAround)
            {
                if (Children.Count > 0)
                {
                    var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
                    var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
                    var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);
                    
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
                    
                    var nl = GetSpaceCount();
                    var nc = Children.Count;
                    
                    if (nl == nc + 1 && vt == "1")
                    {
                        RemoveSpace(nl - 1);
                    }
                    else if (nl == nc + 1 && vb == "1")
                    {
                        RemoveSpace(0);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    else if (nl == nc + 2 && vc == "1")
                    {
                        RemoveSpace(nl - 1);
                        RemoveSpace(0);
                        
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    
                    for (var i = 0; i < (IsVertical ? _grid.RowDefinitions.Count : _grid.ColumnDefinitions.Count); i++)
                    {
                        if(IsVertical) _grid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
                        else _grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                        Children[i].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "1", true);
                    }
                }
            }
            else if (propertyName == PropertyNames.SpaceEvery)
            {
                if (Children.Count > 0)
                {
                    var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
                    var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
                    var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);
                    
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
                    
                    var nl = GetSpaceCount();
                    var nc = Children.Count;
                    
                    if (nl == nc + 1 && vt == "1")
                    {
                        RemoveSpace(nl - 1);
                    }
                    else if (nl == nc + 1 && vb == "1")
                    {
                        RemoveSpace(0);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    else if (nl == nc + 2 && vc == "1")
                    {
                        RemoveSpace(nl - 1);
                        RemoveSpace(0);
                        for (var i = 0; i < Children.Count; i++)
                            SetPosition(i, Children[i].ComponentView);
                    }
                    
                    for (var i = 0; i < (IsVertical ? _grid.RowDefinitions.Count : _grid.ColumnDefinitions.Count); i++)
                    {
                        if(IsVertical) _grid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
                        else _grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                        
                        if (i == 0)
                            Children[i].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "1", true);
                        else if (i == GetSpaceCount() - 1)
                            Children[i].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1", true);
                        else Children[i].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "1", true);
                    }
                }
            }
            #endregion
        }
        
        protected override void InitChildContent()
        {
            
        }
        
        protected override void AddIntoChildContent(FrameworkElement child)
        {
            _grid.Children.Add(child);
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        protected override void Delete()
        {
            var i = -1;
            foreach (var child in Children.Where(child => child.Selected))
            {
                i = Children.IndexOf(child);
                break;
            }
            
            if (i == -1) return;
            
            var nc = Children.Count;
            //var nl = IsVertical ? _grid.RowDefinitions.Count : _grid.ColumnDefinitions.Count;
            
            //var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
            var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
            var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);
            //var sb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
            //var sa = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
            //var se = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);
            var fvbc = vb == "1" || vc == "1";
            
            if (nc == 1)
            {
                if(IsVertical) _grid.RowDefinitions.Clear();
                else _grid.ColumnDefinitions.Clear();
                
                SetPropertyValue(GroupNames.Alignment, PropertyNames.VT, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.VC, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.VB, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.HL, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.HC, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.HR, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
            }
            else if(IsVertical) _grid.RowDefinitions.RemoveAt(fvbc ? i + 1 : i);
            else _grid.ColumnDefinitions.RemoveAt(fvbc ? i + 1 : i);
            
            Children[i].DetacheSelectedHandle();
            _grid.Children.RemoveAt(i);
            Children.RemoveAt(i);
            
            if (Children.Count > 0)
                for (var k = i; k < Children.Count; k++)
                {
                    if(IsVertical) Grid.SetRow(Children[k].ComponentView, fvbc ? k + 1 : k);
                    else Grid.SetColumn(Children[k].ComponentView, fvbc ? k + 1 : k);
                }

            OnSelected();
        }
        
        protected override void WhenWidthChanged(string value)
        {
            WhenHeightChanged(value);
        }
        
        protected override void WhenHeightChanged(string value)
        {
            var i = -1;
            var nl = GetSpaceCount();
            var nc = Children.Count;
            
            var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
            var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
            var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);
            var sb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
            var sa = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
            var se = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);
            var found = false;
            
            foreach (var child in Children)
            {
                if (child.Selected) i = Children.IndexOf(child);
                found = found || GetGroupProperties(GroupNames.Transform)
                    .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width) == SizeValue.Expand.ToString();
            }

            if (i != -1 && value == SizeValue.Expand.ToString())
            {
                if (nl == nc + 1 && vt == "1") RemoveSpace(nl - 1);
                else if (nl == nc + 1 && vb == "1") RemoveSpace(0);
                else if (nl == nc + 2 && vc == "1")
                {
                    RemoveSpace(nl - 1);
                    RemoveSpace(0);
                }
                
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");

                for (var k =0; k < GetSpaceCount(); k++)
                    SetDimension(k, new GridLength(0, GridUnitType.Auto));
                
                SetDimension(i, new GridLength(1, GridUnitType.Star));
            }
            else if (i != -1 && value == SizeValue.Auto.ToString())
            {
                i = i == 0 && nl >= nc + 1 && (vb == "1" || vc == "1") ? 1 : i;
                SetDimension(i, new GridLength(0, GridUnitType.Auto));
            }
            else if (i != -1 && value != SizeValue.Old.ToString())
            {
                if(sb == "0" && sa == "0" && se == "0")
                {
                    i = i == 0 && nl >= nc + 1 && (vb == "1" || vc == "1") ? 1 : i;
                    SetDimension(i, new GridLength(0, GridUnitType.Auto));
                }
            }
            
            if (!found && i != -1)
            {
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1");
            }
        }
        
        protected override void OnMoveLeft()
        {
            OnMoveTop();
        }
        
        protected override void OnMoveRight()
        {
            OnMoveBottom();
        }
        
        protected override void OnMoveTop()
        {
            var nl = GetSpaceCount();
            var nc = Children.Count;
            
            var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
            var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
            var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);
            
            var focus = GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;
            
            foreach (var i in from child in Children let i = Children.IndexOf(child) where child.Selected select i)
            {
                k = i;
                break;
            }
            
            if (k != -1 && (nl == nc || (nl == nc + 1 && vt == "1" && k > 0) || (nl == nc + 1 && vb == "1" && k > 0) || (nl == nc + 2 && vc == "1" && k > 0)))
            {
                if (focus)
                {
                    var fvbc = vb == "1" || vc == "1";
                    var child = Children[k];

                    var rd = new RowDefinition();
                    var cd = new ColumnDefinition();
                    if(IsVertical) rd = _grid.RowDefinitions[fvbc ? k + 1 : k];
                    else cd = _grid.ColumnDefinitions[fvbc ? k + 1 : k];
                    
                    Children.RemoveAt(k);
                    Children.Insert(k - 1, child);
                    _grid.Children.RemoveAt(k);
                    _grid.Children.Insert(k - 1, child.ComponentView);
                    
                    RemoveSpace(fvbc ? k + 1 : k);
                    if(IsVertical) _grid.RowDefinitions.Insert(fvbc ? k : k - 1, rd);
                    else _grid.ColumnDefinitions.Insert(fvbc ? k : k - 1, cd);
                    
                    SetPosition(fvbc ? k : k - 1, Children[k - 1].ComponentView);
                    SetPosition(fvbc ? k + 1 : k, Children[k].ComponentView);
                }
                else
                {
                    Children[k].OnUnselected();
                    Children[k-1].OnSelected();
                }
            }
        }
        
        protected override void OnMoveBottom()
        {
            var nl = _grid.RowDefinitions.Count;
            var nc = Children.Count;
            
            var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
            var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
            var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);
            
            var focus = GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;
            foreach (var i in from child in Children let i = Children.IndexOf(child) where child.Selected select i)
            {
                k = i;
                break;
            }
            
            if (k != -1 && (nl == nc || (nl == nc + 1 && vb == "1" && k < nc - 1) || (nl == nc + 1 && vt == "1" && k < nc - 1) || (nl == nc + 2 && vc == "1" && k < nc - 1)))
            {
                if (focus)
                {
                    var fvbc = vb == "1" || vc == "1";
                    var child = Children[k];
                    
                    var rd = new RowDefinition();
                    var cd = new ColumnDefinition();
                    if(IsVertical) rd = _grid.RowDefinitions[fvbc ? k + 1 : k];
                    else cd = _grid.ColumnDefinitions[fvbc ? k + 1 : k];
                    
                    Children.RemoveAt(k);
                    Children.Insert(k + 1, child);
                    _grid.Children.RemoveAt(k);
                    _grid.Children.Insert(k + 1, child.ComponentView);
                    
                    RemoveSpace(fvbc ? k + 1 : k);
                    
                    if(IsVertical) _grid.RowDefinitions.Insert(fvbc ? k + 2 : k + 1, rd);
                    else _grid.ColumnDefinitions.Insert(fvbc ? k + 2 : k + 1, cd);
                    
                    SetPosition(fvbc ? k + 2 : k + 1, Children[k + 1].ComponentView);
                    SetPosition(fvbc ? k + 1 : k, Children[k].ComponentView);
                }
                else
                {
                    Children[k].OnUnselected();
                    Children[k + 1].OnSelected();
                }
            }
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            #region Contraintes de mise en page
            Children[id].Parent = this;
            /* Global */
            Children[id].SetGroupVisibility(GroupNames.Global);
            Children[id].SetPropertyVisibility(GroupNames.Global, IsVertical ? PropertyNames.MoveLeft : PropertyNames.MoveTop, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, IsVertical ? PropertyNames.MoveRight : PropertyNames.MoveBottom, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            
            /* Content Alignment */
            /* Self Alignment */
            Children[id].SetGroupVisibility(GroupNames.SelfAlignment);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, false);
            Children[id].SetPropertyVisibility(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, false);
            
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
            Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1", true);
            
            var rd = new RowDefinition{ Height = new GridLength(0, GridUnitType.Auto) };

            var component = Children[id];

            var nl = GetSpaceCount();
            var nc = Children.Count - 1;
            
            var vt = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VT : PropertyNames.HL);
            var vc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VC : PropertyNames.HC);
            var vb = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.VB : PropertyNames.HR);

            var sb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
            var sa = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
            var se = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

            var hl = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.HL : PropertyNames.VT);
            var hc = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.HC : PropertyNames.VC);
            var hr = GetGroupProperties(GroupNames.Alignment).GetValue(IsVertical ? PropertyNames.HR : PropertyNames.VB);

            var w = component.GetGroupProperties(GroupNames.Transform).GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);
            var h = component.GetGroupProperties(GroupNames.Transform).GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
            
            if (hl == "1") component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HL : PropertyNames.VT, hl, true);
            if (hc == "1") component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HC : PropertyNames.VC, hc, true);
            if (hr == "1") component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HR : PropertyNames.VB, hr, true);
            else if (!isDeserialize && hl == "0" && hc == "0" && hr == "0" && w != SizeValue.Expand.ToString())
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HL : PropertyNames.VT, "1", true);


            if ((nl == nc + 1 && vt == "1") || (isDeserialize && vt == "1"))
            {
                SetPosition(isDeserialize ? id : GetSpaceCount() - 1, component.ComponentView);
                AddSpace(rd.Height, isDeserialize ? id : GetSpaceCount() - 1, false);
                
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width, SizeValue.Auto.ToString(), true);
                else if (isDeserialize && id == AddedChildrenCount - 1)
                    AddSpace(new GridLength(1, GridUnitType.Star), 0);
            }
            else if ((nl == nc + 1 && vb == "1") && (isDeserialize && vb == "1"))
            {
                if (isDeserialize && id == 0)
                    AddSpace(new GridLength(1, GridUnitType.Star), 0);

                AddSpace(rd.Height, 0);
                SetPosition(isDeserialize ? id + 1 : GetSpaceCount() - 1, component.ComponentView);
                
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width, SizeValue.Auto.ToString(), true);
            }
            else if ((nl == nc + 2 && vc == "1") || (isDeserialize && vc == "1"))
            {
                if (isDeserialize && id == 0)
                    AddSpace(new GridLength(1, GridUnitType.Star), 0);
                
                AddSpace(rd.Height, 0);

                SetPosition(isDeserialize ? id + 1 : _grid.RowDefinitions.Count, component.ComponentView);
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width, SizeValue.Auto.ToString(), true);
                else if (isDeserialize && id == AddedChildrenCount - 1)
                    AddSpace(new GridLength(1, GridUnitType.Star), 0);
            }
            else if (sb == "1")
            {
                if (isDeserialize && id == 0)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1", true);
                else if (isDeserialize && id == AddedChildrenCount - 1)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "1", true);
                else if (isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "1", true);

                if (!isDeserialize && Children.Count > 0)
                    Children[^1].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "1", true);
                
                AddSpace(new GridLength(1, GridUnitType.Star), isDeserialize ? id : GetSpaceCount() - 1, false);
                
                SetPosition(isDeserialize ? id : GetSpaceCount() - 1, component.ComponentView);
                
                if (!isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "1", true);
            }
            else if (sa == "1")
            {
                if(IsVertical)
                    AddSpace(new GridLength(1, GridUnitType.Star), isDeserialize ? id : _grid.RowDefinitions.Count - 1, false);
                else
                    AddSpace(new GridLength(1, GridUnitType.Star), isDeserialize ? id : _grid.ColumnDefinitions.Count - 1, false);
                
                SetPosition(isDeserialize ? id : _grid.RowDefinitions.Count - 1, component.ComponentView);
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HC : PropertyNames.VC, "1", true);
            }
            else if (se == "1")
            {
                if (isDeserialize && id == 0)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VB : PropertyNames.HR, "1", true);
                else if (isDeserialize && id == AddedChildrenCount - 1)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1", true);
                else if (isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VC : PropertyNames.HC, "1", true);

                if (!isDeserialize && Children.Count > 0)
                    Children[^1].OnUpdated(GroupNames.SelfAlignment,IsVertical ? PropertyNames.VC : PropertyNames.HC, "1", true);
                
                AddSpace(new GridLength(1, GridUnitType.Star), isDeserialize ? id : _grid.RowDefinitions.Count - 1, false);
                SetPosition(isDeserialize ? id : _grid.RowDefinitions.Count - 1, component.ComponentView);
                
                if (!isDeserialize)
                    component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.HR : PropertyNames.VB, "1", true);
            }
            else if (nl == nc)
            {
                if (h == SizeValue.Expand.ToString())
                {
                    AddSpace(new GridLength(1, GridUnitType.Star), 0);
                    SetPosition(isDeserialize ? id : GetSpaceCount() - 1, component.ComponentView);
                }
                else if ((h == SizeValue.Auto.ToString() || h != SizeValue.Old.ToString()) && existExpand)
                {
                    AddSpace(rd.Height, 0);
                    SetPosition(isDeserialize ? id : GetSpaceCount() - 1, component.ComponentView);
                }
                else
                {
                    SetPosition(isDeserialize ? id : _grid.RowDefinitions.Count, component.ComponentView);
                    
                    AddSpace(rd.Height, 0);
                    AddSpace(new GridLength(1, GridUnitType.Star), 0);
                    
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1");
                }
            }
            
            if (vt == "1" && (h == SizeValue.Auto.ToString() || h == SizeValue.Expand.ToString()))
                Children[id].OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width, SizeValue.Auto.ToString(), true);
            #endregion
        }

        private void AddSpace(GridLength dimension, int i, bool isAdding = true)
        {
            if (IsVertical)
            {
                if(isAdding) _grid.RowDefinitions.Add(new RowDefinition { Height = dimension });
                else _grid.RowDefinitions.Insert(i, new RowDefinition { Height = dimension });
            }
            else
            {
                if(isAdding) _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = dimension });
                else _grid.ColumnDefinitions.Insert(i, new ColumnDefinition { Width = dimension });
            }
        }

        private void RemoveSpace(int i)
        {
            if(IsVertical) _grid.RowDefinitions.RemoveAt(i);
            else _grid.ColumnDefinitions.RemoveAt(i);
        }

        private int GetSpaceCount()
        {
            return IsVertical ? _grid.RowDefinitions.Count : _grid.ColumnDefinitions.Count;
        }

        private void SetPosition(int i, UIElement view)
        {
            if(IsVertical) Grid.SetRow(view, i);
            else Grid.SetColumn(view, i);
        }

        private void SetDimension(int i, GridLength dimension)
        {
            if (IsVertical)
                _grid.RowDefinitions[i].Height = dimension;
            else _grid.ColumnDefinitions[i].Width = dimension;
        }
    }
}
