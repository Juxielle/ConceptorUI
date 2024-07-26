using System;
using ConceptorUI.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using ConceptorUI.Interfaces;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    abstract class Component : IRefreshPropertyView
    {
        public ComponentList Name { get; set; }
        public List<GroupProperties>? PropertyGroups { get; set; }

        public bool Selected = false;
        protected bool CanSelect = true;
        protected bool HasChildren = false;
        protected bool IsVertical = true;
        protected int AddedChildrenCount = 0;
        
        protected FrameworkElement ComponentView;
        public Component Parent;
        protected List<Component> Children;
        
        protected Border SelectedContent;
        protected Border Content;
        protected Grid ChildrenContent;
        protected FrameworkElement SingleChild;
        
        public event EventHandler? PreSelectedEvent;
        private readonly object _selectedLock = new();
        
        public event EventHandler? PreRefreshPropertyPanelEvent;
        private readonly object _refreshPropertyPanelLock = new();
        
        public event EventHandler? PreRefreshStructuralViewEvent;
        private readonly object _refreshStructuralViewlLock = new();

        protected Component()
        {
            Content = new Border();
            SelectedContent = new Border{ Child = Content };
            ComponentView = SelectedContent;
            ComponentView.PreviewMouseDown += OnMouseDown;
            ComponentView.MouseEnter += OnMouseEnter;
            
            PropertyGroups = new List<GroupProperties>();
            Children = new List<Component>();
            
            OnInit();
            OnInitialize();
        }
        
        event EventHandler IRefreshPropertyView.OnSelected
        {
            add
            {
                lock (_selectedLock)
                {
                    PreSelectedEvent += value;
                }
            }
            remove
            {
                lock (_selectedLock)
                {
                    PreSelectedEvent -= value;
                }
            }
        }
        
        event EventHandler IRefreshPropertyView.OnRefreshPropertyPanel
        {
            add
            {
                lock (_refreshPropertyPanelLock)
                {
                    PreRefreshPropertyPanelEvent += value;
                }
            }
            remove
            {
                lock (_refreshPropertyPanelLock)
                {
                    PreRefreshPropertyPanelEvent -= value;
                }
            }
        }
        
        event EventHandler IRefreshPropertyView.OnRefreshStructuralView
        {
            add
            {
                lock (_refreshStructuralViewlLock)
                {
                    PreRefreshStructuralViewEvent += value;
                }
            }
            remove
            {
                lock (_refreshStructuralViewlLock)
                {
                    PreRefreshStructuralViewEvent -= value;
                }
            }
        }
        
        protected abstract void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false);
        protected abstract void WhenAlignmentChanged(PropertyNames propertyName, string value);
        protected abstract void Delete();
        protected abstract void WhenWidthChanged();
        protected abstract void WhenHeightChanged();
        protected abstract void OnMoveLeft();
        protected abstract void OnMoveRight();
        protected abstract void OnMoveTop();
        protected abstract void OnMoveBottom();
        protected abstract void OnMouseEnter(object sender, MouseEventArgs e);
        
        protected bool OnSelected(bool isInterne = false)
        {
            SelectedContent.BorderBrush = Brushes.DodgerBlue;
            SelectedContent.BorderThickness = new Thickness(1);
            
            PreSelectedEvent!.Invoke(
                new Dictionary<string, dynamic>
                {
                    {"selected", true},
                    {"propertyGroups", PropertyGroups!},
                    {"componentName", Name}
                },
                EventArgs.Empty
            );
            Selected = true;
            return true;
        }

        protected bool OnChildSelected()
        {
            var found = false;
            foreach (var child in Children)
            {
                found = child.OnChildSelected();
                if (found) break;
            }
            return Selected || found;
        }

        protected void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            SelectedContent.BorderBrush = Brushes.Transparent;
            SelectedContent.BorderThickness = new Thickness(0);
            
            foreach (var child in Children)
                child.OnUnselected(isInterne);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.CanSelect) != CanSelectValues.None.ToString() ||
                 (!e.OriginalSource.Equals(SelectedContent) && !e.OriginalSource.Equals(Content) && !e.OriginalSource.Equals(ChildrenContent)))) return;
            
            OnSelected();
            PreSelectedEvent!.Invoke(
                new Dictionary<string, dynamic>
                {
                    {"selected", false},
                    {"propertyGroups", PropertyGroups!},
                    {"componentName", Name}
                },
                EventArgs.Empty
            );
        }
        
        public void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            if (Selected || allow)
            {
                if (!Selected && !allow) return;
                
                #region Alignement
                if (groupName == GroupNames.Alignment)
                {
                    WhenAlignmentChanged(propertyName, value);
                }
                else if (groupName == GroupNames.SelfAlignment)
                {
                    if (propertyName is PropertyNames.HL or PropertyNames.HC or PropertyNames.HR)
                    {
                        var value0 = value == "1" ? "0" : GetGroupProperties(groupName).GetValue(PropertyNames.HL);
                        var value1 = value == "1" ? "0" : GetGroupProperties(groupName).GetValue(PropertyNames.HC);
                        var value2 = value == "1" ? "0" : GetGroupProperties(groupName).GetValue(PropertyNames.HR);
                            
                        SetPropertyValue(groupName, PropertyNames.HL, value0);
                        SetPropertyValue(groupName, PropertyNames.HC, value1);
                        SetPropertyValue(groupName, PropertyNames.HR, value2);
                        SetPropertyValue(groupName, propertyName, value);

                        SelectedContent.HorizontalAlignment = propertyName switch
                        {
                            PropertyNames.HL => value == "0"
                                ? SelectedContent.HorizontalAlignment
                                : HorizontalAlignment.Left,
                            PropertyNames.HC => value == "0"
                                ? SelectedContent.HorizontalAlignment
                                : HorizontalAlignment.Center,
                            _ => value == "0" ? SelectedContent.HorizontalAlignment : HorizontalAlignment.Right
                        };
                    }
                    else if (propertyName is PropertyNames.VT or  PropertyNames.VC or  PropertyNames.VB)
                    {
                        var value0 = value == "1" ? "0" : GetGroupProperties(groupName).GetValue(PropertyNames.VT);
                        var value1 = value == "1" ? "0" : GetGroupProperties(groupName).GetValue(PropertyNames.VC);
                        var value2 = value == "1" ? "0" : GetGroupProperties(groupName).GetValue(PropertyNames.VB);
                            
                        SetPropertyValue(groupName, PropertyNames.VT, value0);
                        SetPropertyValue(groupName, PropertyNames.VC, value1);
                        SetPropertyValue(groupName, PropertyNames.VB, value2);
                        SetPropertyValue(groupName, propertyName, value);

                        SelectedContent.VerticalAlignment = propertyName switch
                        {
                            PropertyNames.VT => value == "0"
                                ? SelectedContent.VerticalAlignment
                                : VerticalAlignment.Top,
                            PropertyNames.VC => value == "0"
                                ? SelectedContent.VerticalAlignment
                                : VerticalAlignment.Center,
                            _ => value == "0" ? SelectedContent.VerticalAlignment : VerticalAlignment.Bottom
                        };
                    }
                }
                #endregion
                
                #region Transform
                else if (propertyName == PropertyNames.Width)
                {
                    if (value == SizeValue.Expand.ToString())
                    {
                        SelectedContent.Width = double.NaN;
                        SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        
                        SetPropertyValue(groupName, propertyName, SizeValue.Expand.ToString());
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HL, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HC, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HR, "0");
                    }
                    else if (value == SizeValue.Auto.ToString() || value != SizeValue.Old.ToString())
                    {
                        if (value == SizeValue.Auto.ToString())
                        {
                            SelectedContent.Width = double.NaN;
                            SetPropertyValue(groupName, propertyName, SizeValue.Auto.ToString());
                        }
                        else
                        {
                            var vd = Helper.ConvertToDouble(value);
                            SelectedContent.Width = vd;
                        }
                        
                        if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HL) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HC) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HR) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                        else
                        {
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HL, "1");
                            //Evenement pour remplacer la vue des props
                        }
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    if (value == SizeValue.Expand.ToString())
                    {
                        SelectedContent.Height = double.NaN;
                        SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
                        
                        SetPropertyValue(groupName, propertyName, SizeValue.Expand.ToString());
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VT, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VC, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VB, "0");
                    }
                    else if (value == SizeValue.Auto.ToString() || value != SizeValue.Old.ToString())
                    {
                        if (value == SizeValue.Auto.ToString())
                        {
                            SelectedContent.Height = double.NaN;
                            SetPropertyValue(groupName, propertyName, SizeValue.Auto.ToString());
                        }
                        else
                        {
                            var vd = Helper.ConvertToDouble(value);
                            SelectedContent.Height = vd;
                        }
                        
                        if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VT) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VC) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Center;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VB) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                        else
                        {
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VT, "1");
                        }
                    }
                }
                else if (propertyName is PropertyNames.HE or PropertyNames.VE)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.MoveParentToChild)
                {
                    if (Children.Count <= 0) return;
                    OnUnselected();
                    Children[0].OnSelected();
                }
                #endregion
                
                #region Appearance
                else if (propertyName == PropertyNames.FillColor)
                {
                    Content.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                        new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Opacity)
                {
                    var vd = Helper.ConvertToDouble(value);
                    Content.Opacity = vd;
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.CMargin)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.MarginBtnActif)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Margin)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, PropertyNames.Margin, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.MarginLeft, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.MarginRight, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.MarginTop, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.MarginBottom, vd.ToString(CultureInfo.InvariantCulture));
                    
                    SetPropertyValue(groupName, PropertyNames.CMargin, "1");
                    
                    SelectedContent.Margin = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.MarginLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(vd, SelectedContent.Margin.Top, SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginTop)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, vd, SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top, vd, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginBottom)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top, SelectedContent.Margin.Right, vd);
                }
                else if (propertyName == PropertyNames.CPadding)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.PaddingBtnActif)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Padding)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, PropertyNames.Padding, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.PaddingLeft, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.PaddingRight, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.PaddingTop, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.PaddingBottom, vd.ToString(CultureInfo.InvariantCulture));
                    
                    SetPropertyValue(groupName, PropertyNames.CPadding, "1");

                    Content.Padding = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.PaddingLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = new Thickness(vd, Content.Padding.Top, Content.Padding.Right, Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingTop)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = new Thickness(Content.Padding.Left, vd, Content.Padding.Right, Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top, vd, Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingBottom)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top, Content.Padding.Right, vd);
                }
                else if (propertyName == PropertyNames.CBorder)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderBtnActif)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, PropertyNames.BorderWidth, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderLeftWidth, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRightWidth, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderTopWidth, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderBottomWidth, vd.ToString(CultureInfo.InvariantCulture));
                    
                    SetPropertyValue(groupName, PropertyNames.CBorder, "1");

                    Content.BorderThickness = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.BorderColor)
                {
                    Content.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                        new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderLeftWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(vd, Content.BorderThickness.Top, Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderTopWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, vd * 0.65, Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderRightWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top, vd, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderBottomWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top, Content.BorderThickness.Right, vd * 0.65);
                }
                else if (propertyName == PropertyNames.CBorderRadius)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderRadBtnActif)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderRadius)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, PropertyNames.BorderRadius, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusTopLeft, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusTopRight, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusBottomLeft, vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusBottomRight, vd.ToString(CultureInfo.InvariantCulture));
                    
                    SetPropertyValue(groupName, PropertyNames.CBorderRadius, "1");

                    Content.CornerRadius = new CornerRadius(vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = new CornerRadius(vd, Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft, Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight, vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft, vd,
                        Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft, Content.CornerRadius.TopRight,
                        vd, Content.CornerRadius.BottomLeft);
                }
                #endregion
                
                #region Global
                else if (propertyName == PropertyNames.CanSelect)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Focus)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.SelectedMode)
                {
                    SetPropertyValue(groupName, propertyName, value);
                }
                #endregion
                
                #region Shadow Property
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowDepth)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    SetPropertyValue(groupName, propertyName, value);
                    
                    if (Content.Effect == null!)
                        Content.Effect = new DropShadowEffect();
                    
                    (Content.Effect as DropShadowEffect)!.ShadowDepth = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.BlurRadius)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    SetPropertyValue(groupName, propertyName, value);
                    
                    if (Content.Effect == null!)
                        Content.Effect = new DropShadowEffect();
                    
                    (Content.Effect as DropShadowEffect)!.BlurRadius = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowDirection)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    SetPropertyValue(groupName, propertyName, value);
                    (Content.Effect as DropShadowEffect)!.Direction = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowColor)
                {
                    if (value == "0") value = ColorValue.Transparent.ToString();
                    SetPropertyValue(groupName, propertyName, value);
                    
                    if (Content.Effect == null!)
                        Content.Effect = new DropShadowEffect();
                    
                    (Content.Effect as DropShadowEffect)!.Color = value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent.Color : (new BrushConverter().ConvertFrom(value) as SolidColorBrush)!.Color;
                }
                #endregion
            }
            else
            {
                #region
                if (propertyName == PropertyNames.MoveChildToParent)
                {
                    foreach (var child in Children.Where(child => child.Selected))
                    {
                        child.OnUnselected();
                        OnSelected();
                        break;
                    }
                }
                else if (propertyName == PropertyNames.Trash)
                {
                    Delete();
                }
                else if (propertyName == PropertyNames.Height)
                {
                    WhenHeightChanged();
                }
                else if (propertyName == PropertyNames.Width)
                {
                    WhenWidthChanged();
                }
                else if (propertyName == PropertyNames.MoveLeft)
                {
                    OnMoveLeft();
                }
                else if (propertyName == PropertyNames.MoveRight)
                {
                    OnMoveRight();
                }
                else if (propertyName == PropertyNames.MoveTop)
                {
                    OnMoveTop();
                }
                else if (propertyName == PropertyNames.MoveBottom)
                {
                    OnMoveBottom();
                }
                
                foreach (var child in Children)
                    child.OnUpdated(groupName, propertyName, value);
                #endregion
            }
        }

        public void OnInitialize()
        {
            foreach (var group in PropertyGroups!)
            {
                foreach (var prop in group.Properties)
                {
                    if (prop.Visibility != VisibilityValue.Visible.ToString())
                        continue;

                    #region Alignement
                    if (prop.Name == PropertyNames.HL.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                    }
                    else if (prop.Name == PropertyNames.HC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                    }
                    else if (prop.Name == PropertyNames.HR.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    else if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                    }
                    else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Center;
                    }
                    else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                    }
                    #endregion
                    
                    #region Transform
                    else if (prop.Name == PropertyNames.Width.ToString())
                    {
                        var selfAlignGroup = GetGroupProperties(GroupNames.SelfAlignment);
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            SelectedContent.Width = double.NaN;
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString() || prop.Value != SizeValue.Old.ToString())
                        {
                            if (prop.Value != SizeValue.Old.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                SelectedContent.Width = vd;
                            }
                            else SelectedContent.Width = double.NaN;
                                
                            if (selfAlignGroup.GetValue(PropertyNames.HL) == "1")
                                SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (selfAlignGroup.GetValue(PropertyNames.HC) == "1")
                                SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (selfAlignGroup.GetValue(PropertyNames.HR) == "1")
                                SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                            else SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }
                    else if (prop.Name == PropertyNames.Height.ToString())
                    {
                        var selfAlignGroup = GetGroupProperties(GroupNames.SelfAlignment);
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            SelectedContent.Height = double.NaN;
                            SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString() || prop.Value != SizeValue.Old.ToString())
                        {
                            if (prop.Value != SizeValue.Old.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                SelectedContent.Height = vd;
                            }
                            else SelectedContent.Height = double.NaN;
                                
                            if (selfAlignGroup.GetValue(PropertyNames.VT) == "1")
                                SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                            else if (selfAlignGroup.GetValue(PropertyNames.VC) == "1")
                                SelectedContent.VerticalAlignment = VerticalAlignment.Center;
                            else if (selfAlignGroup.GetValue(PropertyNames.VB) == "1")
                                SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                            else SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                        }
                    }
                    #endregion
                    
                    #region Text
                    if (group.Name == GroupNames.Text.ToString())
                    {
                        var text = SingleChild as TextBlock;
                        if (prop.Name == PropertyNames.FontFamily.ToString())
                        {
                            text!.FontFamily = ManageEnums.Instance.GetFontFamily(prop.Value);
                        }
                        else if (prop.Name == PropertyNames.FontWeight.ToString())
                        {
                            text!.FontWeight = prop.Value == "0" ? FontWeights.Normal : FontWeights.Bold;
                        }
                        else if (prop.Name == PropertyNames.FontStyle.ToString())
                        {
                            text!.FontStyle = prop.Value == "0" ? FontStyles.Normal : FontStyles.Italic;
                        }
                        else if (prop.Name == PropertyNames.FontSize.ToString())
                        {
                            var vd = Helper.ConvertToDouble(prop.Value);
                            text!.FontSize = vd;
                        }
                        else if (prop.Name == PropertyNames.AlignLeft.ToString())
                        {
                            if (prop.Value == "1")
                                text!.TextAlignment = TextAlignment.Left;
                        }
                        else if (prop.Name == PropertyNames.AlignCenter.ToString())
                        {
                            if (prop.Value == "1")
                                text!.TextAlignment = TextAlignment.Center;
                        }
                        else if (prop.Name == PropertyNames.AlignRight.ToString())
                        {
                            if (prop.Value == "1")
                                text!.TextAlignment = TextAlignment.Right;
                        }
                        else if (prop.Name == PropertyNames.AlignJustify.ToString())
                        {
                            if (prop.Value == "1")
                                text!.TextAlignment = TextAlignment.Justify;
                        }
                        else if (prop.Name == PropertyNames.TextUnderline.ToString())
                        {
                            if (prop.Value == "1")
                                text!.TextDecorations = TextDecorations.Underline;
                        }
                        else if (prop.Name == PropertyNames.TextOverline.ToString())
                        {
                            if (prop.Value == "1")
                                text!.TextDecorations = TextDecorations.OverLine;
                        }
                        else if (prop.Name == PropertyNames.TextThrough.ToString())
                        {
                            if (prop.Value == "1")
                                text!.TextDecorations = TextDecorations.Strikethrough;
                        }
                        else if (prop.Name == PropertyNames.Color.ToString())
                        {
                            text!.Foreground = prop.Value == ColorValue.Transparent.ToString()
                                ? Brushes.Transparent
                                : new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                        }
                        else if (prop.Name == PropertyNames.Text.ToString())
                        {
                            text!.Text = prop.Value;
                        }
                        else if (prop.Name == PropertyNames.TextWrap.ToString())
                        {
                            text!.TextWrapping = prop.Value == "0" ? TextWrapping.NoWrap : TextWrapping.Wrap;
                        }
                        else if (prop.Name == PropertyNames.LineSpacing.ToString())
                        {
                            var vd = Helper.ConvertToDouble(prop.Value);
                            vd = vd == 0 ? 1 : vd;
                            text!.LineHeight = vd;
                        }
                    }
                    #endregion
                    
                    #region Appearance
                    else if (prop.Name == PropertyNames.FillColor.ToString())
                    {
                        SelectedContent.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                            new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.Opacity.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Opacity = vd;
                    }
                    else if (prop.Name == PropertyNames.Margin.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.MarginLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(vd, SelectedContent.Margin.Top, SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, vd, SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top, vd, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Margin = new Thickness(Content.Margin.Left, Content.Margin.Top, Content.Margin.Right, vd);
                    }
                    else if (prop.Name == PropertyNames.Padding.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = new Thickness(vd, Content.Padding.Top, Content.Padding.Right, Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = new Thickness(Content.Padding.Left, vd, Content.Padding.Right, Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top, vd, Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top, Content.Padding.Right, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderColor.ToString())
                    {
                        Content.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                            new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.BorderLeftWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(vd, Content.BorderThickness.Top, Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderTopWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left, vd * 0.65, Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderRightWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top, vd, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderBottomWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top, Content.BorderThickness.Right, vd * 0.65);
                    }
                    else if (prop.Name == PropertyNames.BorderRadius.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = new CornerRadius(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = new CornerRadius(vd, Content.CornerRadius.TopRight,
                            Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft, Content.CornerRadius.TopRight,
                            Content.CornerRadius.BottomRight, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft, vd,
                            Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft, Content.CornerRadius.TopRight,
                            vd, Content.CornerRadius.BottomLeft);
                    }
                    #endregion
                    
                    #region Shadow Property
                    else if (prop.Name == PropertyNames.ShadowDepth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (Content.Effect == null!) Content.Effect = new DropShadowEffect();
                        (Content.Effect as DropShadowEffect)!.ShadowDepth = vd;
                    }
                    else if (prop.Name == PropertyNames.BlurRadius.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (Content.Effect == null!) Content.Effect = new DropShadowEffect();
                        (Content.Effect as DropShadowEffect)!.BlurRadius = vd;
                    }
                    else if (prop.Name == PropertyNames.ShadowDirection.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (Content.Effect == null!) Content.Effect = new DropShadowEffect();
                        (Content.Effect as DropShadowEffect)!.Direction = vd;
                    }
                    else if (prop.Name == PropertyNames.ShadowColor.ToString())
                    {
                        if (prop.Value == "0") prop.Value = ColorValue.Transparent.ToString();
                        if (Content.Effect == null!) Content.Effect = new DropShadowEffect();
                        (Content.Effect as DropShadowEffect)!.Color = prop.Value == ColorValue.Transparent.ToString()
                            ? Brushes.Transparent.Color : (new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush)!.Color;
                    }
                    #endregion
                }
            }
        }

        public string OnCopyOrPaste(string value = null!, bool isPaste = false)
        {
            CompSerialiser valueD = null!;
            if(value != null!)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerialiser>(value)!;

            if (Selected && isPaste && valueD != null!)
            {
                var name = valueD.Name!;
                var component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this;
                component.OnDeserializer(valueD);

                var expanded = false;
                foreach (var child in Children)
                {
                    var w = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                    expanded = expanded || w == SizeValue.Expand.ToString();
                }

                ChildrenContent.Children.Add(component.ComponentView!);
                Children.Add(component);

                LayoutConstraints(Children.Count - 1, false, expanded);
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(OnSerializer());
            else if (!Selected)
                foreach (var child in Children)
                {
                    var comp = child.OnCopyOrPaste(value!, isPaste);
                    if (comp != null!) return comp;
                }
            return null!;
        }

        public CompSerialiser OnSerializer()
        {
            var children = new List<CompSerialiser>();
            foreach (var child in Children)
                children.Add(child.OnSerializer());
            return new CompSerialiser
            {
                Name = Name.ToString(),
                Properties = PropertyGroups,
                Children = Children.Count > 0 ? children : null
            };
        }

        public void OnDeserializer(CompSerialiser compSerializer)
        {
            PropertyGroups = compSerializer.Properties;
            #region
            if (compSerializer.Children != null)
            {
                AddedChildrenCount = compSerializer.Children.Count;

                var components = new List<Component>();
                var expanded = false;

                foreach (var child in compSerializer.Children)
                {
                    var component = ManageEnums.Instance.GetComponent(child.Name!);
                    component.Parent = this;
                    component.OnDeserializer(child);
                    components.Add(component);
                    var w = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                    expanded = expanded || w == SizeValue.Expand.ToString();
                }

                foreach (var component in components)
                {
                    ChildrenContent.Children.Add(component.ComponentView);
                    Children.Add(component);
                    LayoutConstraints(Children.Count - 1, true, expanded);
                }
            }
            #endregion
            OnInitialize();
        }

        public StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = Name.ToString()!;
            structuralElement.Selected = Selected;
            structuralElement.Children = new List<StructuralElement>();
            
            foreach (var child in Children)
                structuralElement.Children.Add(child.AddToStructuralView());

            return structuralElement;
        }

        public void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PreSelectedEvent!.Invoke(
                    new Dictionary<string, dynamic>
                    {
                        {"selected", false},
                        {"propertyGroups", PropertyGroups!},
                        {"componentName", Name}
                    },
                    EventArgs.Empty
                );
                OnSelected();
            }
            else if (structuralElement.Children != null! && structuralElement.Children.Count == Children.Count)
            {
                for (var i = 0; i < structuralElement.Children!.Count; i++)
                    Children[i].SelectFromStructuralView(structuralElement.Children[i]);
            }
        }

        public GroupProperties GetGroupProperties(GroupNames groupName)
        {
            foreach (var propertyGroup in PropertyGroups!.Where(propertyGroup => propertyGroup.Name == groupName.ToString()))
                return propertyGroup;
            return new GroupProperties();
        }

        protected void SetPropertyValue(GroupNames groupName, PropertyNames propertyName, string value)
        {
            var i = -1;
            foreach (var group in PropertyGroups!)
            {
                i++;
                var j = -1;
                if(group.Name != groupName.ToString()) continue;
                foreach (var property in group.Properties)
                {
                    j++;
                    if(property.Name != propertyName.ToString()) continue;
                    PropertyGroups[i].Properties[j].Value = value;
                    return;
                }
            }
        }

        public void SetGroupVisibility(GroupNames groupName, bool isVisible = true)
        {
            var i = -1;
            foreach (var group in PropertyGroups!)
            {
                i++;
                if(group.Name != groupName.ToString()) continue;
                PropertyGroups[i].Visibility = isVisible ? VisibilityValue.Visible.ToString() : VisibilityValue.Collapsed.ToString();
                break;
            }
        }

        public void SetPropertyVisibility(GroupNames groupName, PropertyNames propertyName, bool isVisible = true)
        {
            var i = -1;
            foreach (var group in PropertyGroups!)
            {
                i++;
                var j = -1;
                if(group.Name != groupName.ToString()) continue;
                foreach (var property in group.Properties)
                {
                    j++;
                    if(property.Name != propertyName.ToString()) continue;
                    PropertyGroups[i].Properties[j].Visibility = isVisible ? VisibilityValue.Visible.ToString() : VisibilityValue.Collapsed.ToString();
                    return;
                }
            }
        }

        protected void OnInit()
        {
            #region
            PropertyGroups = new List<GroupProperties>
            {
                new ()
                {
                    Name = GroupNames.Alignment.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new ()
                        {
                            Name = PropertyNames.HL.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.HC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.HR.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.VT.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.VC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.VB.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.SpaceBetween.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.SpaceAround.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.SpaceEvery.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        }
                    }
                },
                new ()
                {
                    Name = GroupNames.Transform.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new ()
                        {
                            Name = PropertyNames.Width.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = SizeValue.Expand.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.Height.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "100",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.X.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.Y.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.ROT.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.HE.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.VE.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new ()
                        {
                            Name = PropertyNames.HVE.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                    }
                },
                new()
                {
                    Name = GroupNames.Text.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new()
                        {
                            Name = PropertyNames.FontFamily.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.FontWeight.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.FontStyle.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.FontSize.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "20",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.AlignLeft.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.AlignCenter.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.AlignRight.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.AlignJustify.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.ListOrd.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.ListNOrd.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TabLeft.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TabRight.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextUnderline.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextOverline.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextThrough.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextError.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextIndex.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextExpo.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Color.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "#6739b7",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Text.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "Text",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.DisplayText.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextWrap.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.LineSpacing.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        }
                    }
                },
                new () 
                {
                    Name = GroupNames.Appearance.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.FillColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "#FFE0E0E0",
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
                            Value = "0",
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
                new ()
                {
                    Name = GroupNames.SelfAlignment.ToString(),
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
                new ()
                {
                    Name = GroupNames.GridProperty.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.Row.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Column.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Fusion.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Merged.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.ColumnSpan.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.RowSpan.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
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
                new ()
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
