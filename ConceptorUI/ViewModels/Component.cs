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


namespace ConceptorUi.ViewModels
{
    abstract class Component : IRefreshPropertyView
    {
        protected ComponentList Name { get; set; }
        private List<GroupProperties>? PropertyGroups { get; set; }

        public bool Selected = false;
        private bool _canSelect = true;
        protected bool HasChildren = true;
        protected bool IsVertical = true;
        protected int AddedChildrenCount = 0;
        protected bool CanAddIntoChildContent = true;
        protected int ChildContentLimit = 100;
        
        protected bool IsInComponent = false;
        public bool IsOriginalComponent = false;

        public FrameworkElement ComponentView;
        public Component Parent;
        protected List<Component> Children;

        private Border _selectedContent;
        protected Border Content;
        
        public event EventHandler? OnSelectedEvent;
        private readonly object _selectedLock = new();
        
        public event EventHandler? OnRefreshPropertyPanelEvent;
        private readonly object _refreshPropertyPanelLock = new();
        
        public event EventHandler? OnRefreshStructuralViewEvent;
        private readonly object _refreshStructuralViewlLock = new();
        
        event EventHandler IRefreshPropertyView.OnSelected
        {
            add
            {
                lock (_selectedLock)
                {
                    OnSelectedEvent += value;
                }
            }
            remove
            {
                lock (_selectedLock)
                {
                    OnSelectedEvent -= value;
                }
            }
        }
        
        event EventHandler IRefreshPropertyView.OnRefreshPropertyPanel
        {
            add
            {
                lock (_refreshPropertyPanelLock)
                {
                    OnRefreshPropertyPanelEvent += value;
                }
            }
            remove
            {
                lock (_refreshPropertyPanelLock)
                {
                    OnRefreshPropertyPanelEvent -= value;
                }
            }
        }
        
        event EventHandler IRefreshPropertyView.OnRefreshStructuralView
        {
            add
            {
                lock (_refreshStructuralViewlLock)
                {
                    OnRefreshStructuralViewEvent += value;
                }
            }
            remove
            {
                lock (_refreshStructuralViewlLock)
                {
                    OnRefreshStructuralViewEvent -= value;
                }
            }
        }
        
        protected abstract void SelfConstraints();
        protected abstract void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false);
        protected abstract void WhenAlignmentChanged(PropertyNames propertyName, string value);
        protected abstract void WhenTextChanged(string propertyName, string value);
        protected abstract void WhenFileLoaded(string value);
        protected abstract void InitChildContent();
        protected abstract void AddIntoChildContent(FrameworkElement child);
        protected abstract bool AllowExpanded(bool isWidth = true);
        protected abstract void Delete();
        protected abstract void WhenWidthChanged(string value);
        protected abstract void WhenHeightChanged(string value);
        protected abstract void OnMoveLeft();
        protected abstract void OnMoveRight();
        protected abstract void OnMoveTop();
        protected abstract void OnMoveBottom();
        
        public void OnSelected()
        {
            _selectedContent.BorderBrush = Brushes.SeaGreen;
            _selectedContent.BorderThickness = new Thickness(0.8);
            
            OnSelectedEvent!.Invoke(
                new Dictionary<string, dynamic>
                {
                    {"selected", true},
                    {"propertyGroups", PropertyGroups!},
                    {"componentName", Name}
                },
                EventArgs.Empty
            );
            Selected = true;
        }

        public void OnSelectedHandle(object sender, EventArgs e)
        {
            OnSelectedEvent!.Invoke(
                sender,
                EventArgs.Empty
            );
        }

        public void DetacheSelectedHandle()
        {
            OnSelectedEvent -= OnSelectedHandle!;
        }

        public bool OnChildSelected()
        {
            var found = false;
            foreach (var child in Children)
            {
                found = child.OnChildSelected();
                if (found) break;
            }
            return Selected || found;
        }

        public void OnUnselected()
        {
            Selected = false;
            _selectedContent.BorderBrush = Brushes.Transparent;
            _selectedContent.BorderThickness = new Thickness(0);
            
            foreach (var child in Children)
                child.OnUnselected();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.CanSelect) != CanSelectValues.None.ToString() ||
               (!e.OriginalSource.Equals(_selectedContent) && !e.OriginalSource.Equals(Content) && !e.OriginalSource.Equals(Content.Child))) return;
            
            OnSelectedEvent!.Invoke(
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

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            
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

                        _selectedContent.HorizontalAlignment = propertyName switch
                        {
                            PropertyNames.HL => value == "0"
                                ? _selectedContent.HorizontalAlignment
                                : HorizontalAlignment.Left,
                            PropertyNames.HC => value == "0"
                                ? _selectedContent.HorizontalAlignment
                                : HorizontalAlignment.Center,
                            _ => value == "0" ? _selectedContent.HorizontalAlignment : HorizontalAlignment.Right
                        };
                        
                        var w = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        if(value == "0" && value0 == "0" && value1 == "0" && value2 == "0" && w == SizeValue.Expand.ToString())
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
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

                        _selectedContent.VerticalAlignment = propertyName switch
                        {
                            PropertyNames.VT => value == "0"
                                ? _selectedContent.VerticalAlignment
                                : VerticalAlignment.Top,
                            PropertyNames.VC => value == "0"
                                ? _selectedContent.VerticalAlignment
                                : VerticalAlignment.Center,
                            _ => value == "0" ? _selectedContent.VerticalAlignment : VerticalAlignment.Bottom
                        };
                        
                        var h = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        if(value == "0" && value0 == "0" && value1 == "0" && value2 == "0" && h == SizeValue.Expand.ToString())
                            _selectedContent.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                }
                #endregion
                
                #region Transform
                else if (propertyName == PropertyNames.Width)
                {
                    SetPropertyValue(groupName, propertyName, value);
                    if (value == SizeValue.Expand.ToString() && AllowExpanded())
                    {
                        _selectedContent.Width = double.NaN;
                        _selectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        
                        SetPropertyValue(groupName, propertyName, SizeValue.Expand.ToString());
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HL, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HC, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HR, "0");
                    }
                    else if (value == SizeValue.Auto.ToString() || value != SizeValue.Old.ToString())
                    {
                        if (value == SizeValue.Auto.ToString())
                        {
                            _selectedContent.Width = double.NaN;
                        }
                        else
                        {
                            var vd = Helper.ConvertToDouble(value);
                            _selectedContent.Width = vd;
                        }
                        
                        if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HL) == "1")
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HC) == "1")
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HR) == "1")
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                        else
                        {
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.HL, "1");
                            //Evenement pour remplacer la vue des props
                        }
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    SetPropertyValue(groupName, propertyName, value);
                    if (value == SizeValue.Expand.ToString() && AllowExpanded(false))
                    {
                        _selectedContent.Height = double.NaN;
                        _selectedContent.VerticalAlignment = VerticalAlignment.Stretch;
                        
                        SetPropertyValue(groupName, propertyName, SizeValue.Expand.ToString());
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VT, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VC, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VB, "0");
                    }
                    else if (value == SizeValue.Auto.ToString() || value != SizeValue.Old.ToString())
                    {
                        if (value == SizeValue.Auto.ToString())
                        {
                            _selectedContent.Height = double.NaN;
                        }
                        else
                        {
                            var vd = Helper.ConvertToDouble(value);
                            _selectedContent.Height = vd;
                        }
                        
                        if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VT) == "1")
                            _selectedContent.VerticalAlignment = VerticalAlignment.Top;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VC) == "1")
                            _selectedContent.VerticalAlignment = VerticalAlignment.Center;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VB) == "1")
                            _selectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                        else
                        {
                            _selectedContent.VerticalAlignment = VerticalAlignment.Top;
                            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VT, "1");
                        }
                    }
                }
                else if (propertyName is PropertyNames.HE or PropertyNames.VE)
                {
                    SetPropertyValue(groupName, propertyName, value);
                    SetPropertyValue(groupName, propertyName is PropertyNames.HE ? PropertyNames.Width : PropertyNames.Height,
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue.Auto.ToString());
                }
                else if (propertyName == PropertyNames.MoveParentToChild)
                {
                    if (Children.Count <= 0) return;
                    OnUnselected();
                    Children[0].OnSelected();
                }
                #endregion
                
                #region Text
                else if (groupName == GroupNames.Text)
                {
                    WhenTextChanged(propertyName.ToString(), value);
                    if (propertyName == PropertyNames.FontFamily)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.FontWeight)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.FontStyle)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.FontSize)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        vd = vd == 0 ? 10 : vd;
                        SetPropertyValue(groupName, propertyName, vd+"");
                    }
                    else if (propertyName == PropertyNames.AlignLeft)
                    {
                        SetPropertyValue(groupName, PropertyNames.AlignRight, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignCenter, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignJustify, "0");
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.AlignCenter)
                    {
                        SetPropertyValue(groupName, PropertyNames.AlignLeft, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignRight, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignJustify, "0");
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.AlignRight)
                    {
                        SetPropertyValue(groupName, PropertyNames.AlignLeft, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignCenter, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignJustify, "0");
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.AlignJustify)
                    {
                        SetPropertyValue(groupName, PropertyNames.AlignLeft, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignRight, "0");
                        SetPropertyValue(groupName, PropertyNames.AlignCenter, "0");
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextUnderline)
                    {
                        SetPropertyValue(groupName, PropertyNames.TextOverline, "0");
                        SetPropertyValue(groupName, PropertyNames.TextThrough, "0");
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextOverline)
                    {
                        SetPropertyValue(groupName, PropertyNames.TextUnderline, "0");
                        SetPropertyValue(groupName, PropertyNames.TextThrough, "0");
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextThrough)
                    {
                        SetPropertyValue(groupName, PropertyNames.TextUnderline, "0");
                        SetPropertyValue(groupName, PropertyNames.TextOverline, "0");
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.Color){ }
                    else if (propertyName == PropertyNames.Text){ }
                    else if (propertyName == PropertyNames.TextWrap){ }
                    else if (propertyName == PropertyNames.LineSpacing){ }
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
                    
                    _selectedContent.Margin = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.MarginLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    _selectedContent.Margin = new Thickness(vd, _selectedContent.Margin.Top, _selectedContent.Margin.Right, _selectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginTop)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    _selectedContent.Margin = new Thickness(_selectedContent.Margin.Left, vd, _selectedContent.Margin.Right, _selectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    _selectedContent.Margin = new Thickness(_selectedContent.Margin.Left, _selectedContent.Margin.Top, vd, _selectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginBottom)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    _selectedContent.Margin = new Thickness(_selectedContent.Margin.Left, _selectedContent.Margin.Top, _selectedContent.Margin.Right, vd);
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
                else if (propertyName == PropertyNames.FilePicker)
                {
                    SetPropertyValue(groupName, propertyName, value);
                    WhenFileLoaded(value);
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
                    WhenHeightChanged(value);
                }
                else if (propertyName == PropertyNames.Width)
                {
                    WhenWidthChanged(value);
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
                    if (group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Name == PropertyNames.HL.ToString() && prop.Value == "1")
                        {
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else if (prop.Name == PropertyNames.HC.ToString() && prop.Value == "1")
                        {
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                        }
                        else if (prop.Name == PropertyNames.HR.ToString() && prop.Value == "1")
                        {
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                        }
                        else if (prop.Name == PropertyNames.VT.ToString() && prop.Value == "1")
                        {
                            _selectedContent.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (prop.Name == PropertyNames.VC.ToString() && prop.Value == "1")
                        {
                            _selectedContent.VerticalAlignment = VerticalAlignment.Center;
                        }
                        else if (prop.Name == PropertyNames.VB.ToString() && prop.Value == "1")
                        {
                            _selectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                        }
                        
                        var hl = group.GetValue(PropertyNames.HL);
                        var hc = group.GetValue(PropertyNames.HC);
                        var hr = group.GetValue(PropertyNames.HR);
                        var w = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        if(hl == "0" && hc == "0" && hr == "0" && w == SizeValue.Expand.ToString())
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        
                        var vt = group.GetValue(PropertyNames.VT);
                        var vc = group.GetValue(PropertyNames.VC);
                        var vb = group.GetValue(PropertyNames.VB);
                        var h = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        if(vt == "0" && vc == "0" && vb == "0" && h == SizeValue.Expand.ToString())
                            _selectedContent.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    #endregion

                    #region Transform
                    else if (prop.Name == PropertyNames.Width.ToString())
                    {
                        var selfAlignGroup = GetGroupProperties(GroupNames.SelfAlignment);
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            _selectedContent.Width = double.NaN;
                            _selectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString() || prop.Value != SizeValue.Old.ToString())
                        {
                            if (prop.Value != SizeValue.Old.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                _selectedContent.Width = vd;
                            }
                            else _selectedContent.Width = double.NaN;
                                
                            if (selfAlignGroup.GetValue(PropertyNames.HL) == "1")
                                _selectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (selfAlignGroup.GetValue(PropertyNames.HC) == "1")
                                _selectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (selfAlignGroup.GetValue(PropertyNames.HR) == "1")
                                _selectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                            else _selectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }
                    else if (prop.Name == PropertyNames.Height.ToString())
                    {
                        var selfAlignGroup = GetGroupProperties(GroupNames.SelfAlignment);
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            _selectedContent.Height = double.NaN;
                            _selectedContent.VerticalAlignment = VerticalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString() || prop.Value != SizeValue.Old.ToString())
                        {
                            if (prop.Value != SizeValue.Old.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                _selectedContent.Height = vd;
                            }
                            else _selectedContent.Height = double.NaN;
                                
                            if (selfAlignGroup.GetValue(PropertyNames.VT) == "1")
                                _selectedContent.VerticalAlignment = VerticalAlignment.Top;
                            else if (selfAlignGroup.GetValue(PropertyNames.VC) == "1")
                                _selectedContent.VerticalAlignment = VerticalAlignment.Center;
                            else if (selfAlignGroup.GetValue(PropertyNames.VB) == "1")
                                _selectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                            else _selectedContent.VerticalAlignment = VerticalAlignment.Top;
                        }
                    }
                    #endregion
                    
                    #region Text
                    else if (group.Name == GroupNames.Text.ToString())
                    {
                        WhenTextChanged(prop.Name, prop.Value);
                    }
                    #endregion
                    
                    #region Appearance
                    else if (prop.Name == PropertyNames.FillColor.ToString())
                    {
                        _selectedContent.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                            new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.Opacity.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        _selectedContent.Opacity = vd;
                    }
                    else if (prop.Name == PropertyNames.Margin.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        _selectedContent.Margin = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.MarginLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        _selectedContent.Margin = new Thickness(vd, _selectedContent.Margin.Top, _selectedContent.Margin.Right, _selectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        _selectedContent.Margin = new Thickness(_selectedContent.Margin.Left, vd, _selectedContent.Margin.Right, _selectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        _selectedContent.Margin = new Thickness(_selectedContent.Margin.Left, _selectedContent.Margin.Top, vd, _selectedContent.Margin.Bottom);
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
                    
                    #region Global
                    else if (prop.Name == PropertyNames.FilePicker.ToString())
                    {
                        WhenFileLoaded(prop.Value);
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

        public void OnAdd(Component component)
        {
            if (!HasChildren || Children.Count >= ChildContentLimit) return;
            component.Parent = this;
            component.OnSelectedEvent += OnSelectedHandle!;
            
            AddIntoChildContent(component.ComponentView);
            Children.Add(component);

            var expanded = false;
            foreach (var child in Children)
            {
                var d = child.GetGroupProperties(GroupNames.Transform).GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                expanded = expanded || d == SizeValue.Expand.ToString();
            }
            
            LayoutConstraints(Children.Count - 1, false, expanded);
        }

        public string OnCopyOrPaste(string value = null!, bool isPaste = false)
        {
            CompSerializer valueD = null!;
            if(value != null!)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerializer>(value)!;

            if (Selected && isPaste && valueD != null! && CanAddIntoChildContent && Children.Count < ChildContentLimit)
            {
                var name = valueD.Name!;
                var component = ComponentHelper.GetComponent(name);
                component.Parent = this;
                component.OnSelectedEvent += OnSelectedHandle!;
                
                component.OnDeserializer(valueD);

                var expanded = false;
                foreach (var child in Children)
                {
                    var d = child.GetGroupProperties(GroupNames.Transform).GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                    expanded = expanded || d == SizeValue.Expand.ToString();
                }
                
                AddIntoChildContent(component.ComponentView);
                Children.Add(component);
                
                LayoutConstraints(Children.Count - 1, false, expanded);
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(OnSerializer());
            else if (!Selected)
                foreach (var child in Children)
                {
                    var comp = child.OnCopyOrPaste(value!, isPaste);
                    if (comp != null!)
                        return comp;
                }
            return null!;
        }

        public CompSerializer OnSerializer()
        {
            var children = new List<CompSerializer>();
            foreach (var child in Children)
                children.Add(child.OnSerializer());
            
            return new CompSerializer
            {
                Name = Name.ToString(),
                Properties = PropertyGroups,
                Children = Children.Count > 0 ? children : null
            };
        }

        public void OnDeserializer(CompSerializer compSerializer)
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
                    var component = ComponentHelper.GetComponent(child.Name!);
                    component.Parent = this;
                    component.OnSelectedEvent += OnSelectedHandle!;
                    
                    component.OnDeserializer(child);
                    components.Add(component);
                    var d = component.GetGroupProperties(GroupNames.Transform).GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                    expanded = expanded || d == SizeValue.Expand.ToString();
                }
                
                foreach (var component in components)
                {
                    AddIntoChildContent(component.ComponentView);
                    Children.Add(component);
                    LayoutConstraints(Children.Count - 1, true, expanded);
                }
            }
            #endregion
            OnInitialize();

            if (Name == ComponentList.Window)
            {
                Console.WriteLine($@"Children Count: {Children.Count}");
                Console.WriteLine($@"Child Name: {Children[0].Name}");
                Console.WriteLine($@"Children of Child Count: {Children[0].Children.Count}");
                foreach (var child in Children[0].Children)
                {
                    Console.WriteLine($@"Child Name: {child.Name}");
                    Console.WriteLine($@"Child Width: {child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width)}");
                    Console.WriteLine($@"Child Height: {child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height)}");
                    Console.WriteLine($@"Child FillColor: {child.GetGroupProperties(GroupNames.Appearance).GetValue(PropertyNames.FillColor)}");
                }
            }
        }

        public StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = Name.ToString();
            structuralElement.Selected = Selected;
            structuralElement.Children = new List<StructuralElement>();
            
            structuralElement.IsSimpleElement = HasChildren;
            
            foreach (var child in Children)
                structuralElement.Children.Add(child.AddToStructuralView());

            return structuralElement;
        }

        public void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                OnSelectedEvent!.Invoke(
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
                for (var i = 0; i < structuralElement.Children.Count; i++)
                    Children[i].SelectFromStructuralView(structuralElement.Children[i]);
            }
        }

        public GroupProperties GetGroupProperties(GroupNames groupName)
        {
            foreach (var propertyGroup in PropertyGroups!.Where(propertyGroup => propertyGroup.Name == groupName.ToString()))
                return propertyGroup;
            return new GroupProperties();
        }

        public void SetPropertyValue(GroupNames groupName, PropertyNames propertyName, string value)
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
                foreach (var property in group.Properties)
                {
                    var j = group.Properties.IndexOf(property);
                    PropertyGroups[i].Properties[j].Visibility = isVisible ? VisibilityValue.Visible.ToString() : VisibilityValue.Collapsed.ToString();
                }
                return;
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

        public bool IsNullAlignment(GroupNames align, string direction)
        {
            var group = GetGroupProperties(align == GroupNames.Alignment ? align : GroupNames.SelfAlignment);
            var propNames = direction == "Horizontal" ? new List<string> { "HL", "HC", "HR" } : new List<string> { "VT", "VC", "VB" };
            
            var isNull = false;
            foreach (var property in group.Properties)
            {
                var found = propNames.Any(name => property.Name == name);

                if(found) continue;
                
                isNull = property.Value == "0";
                if (!isNull) break;
            }

            return isNull;
        }

        protected void OnInit()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            InitChildContent();
            
            Content = new Border();
            _selectedContent = new Border{ Child = Content };
            ComponentView = _selectedContent;
            ComponentView.PreviewMouseDown += OnMouseDown;
            ComponentView.MouseEnter += OnMouseEnter;
            
            PropertyGroups = new List<GroupProperties>();
            Children = new List<Component>();
            
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
                            Value = SizeValue.Expand.ToString(),
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
                            Value = "12",
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
                        new()
                        {
                            Name = PropertyNames.FillColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "#FFE0E0E0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.CMargin.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MarginBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.MarginLeft.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Margin.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MarginLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MarginTop.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MarginRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MarginBottom.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.CPadding.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.PaddingBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.PaddingLeft.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Padding.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.PaddingLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.PaddingTop.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.PaddingRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.PaddingBottom.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.CBorder.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "BorderLeft",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderLeftWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderLeftColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderLeftStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderTopWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderTopColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderTopStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRightWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRightColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRightStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderBottomWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderBottomColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderBottomStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.CBorderRadius.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRadBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRadius.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRadiusTopRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
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
                        new()
                        {
                            Name = PropertyNames.HL.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.HC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.HR.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.VT.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.VC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
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
                        new()
                        {
                            Name = PropertyNames.Row.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Column.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Fusion.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Merged.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.ColumnSpan.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.RowSpan.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.SelectedElement.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ESelectedElement.Cell.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
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
                        new()
                        {
                            Name = PropertyNames.SelectedMode.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = ESelectedMode.Single.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.FilePicker.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MoveLeft.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MoveRight.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MoveTop.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MoveBottom.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Focus.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MoveParentToChild.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.MoveChildToParent.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Trash.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
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
