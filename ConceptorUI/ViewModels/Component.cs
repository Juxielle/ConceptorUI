﻿using System;
using ConceptorUI.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using ConceptorUI.Enums;
using ConceptorUI.Inputs;
using ConceptorUI.Utils;
using ConceptorUI.ViewModels.Text;


namespace ConceptorUi.ViewModels
{
    internal abstract class Component
    {
        /*
         * Mettre en place un système de remplacement de composants
         * Donner une mise en page à un composant
         */
        protected ComponentList Name { get; set; }
        public List<GroupProperties>? PropertyGroups { get; set; }

        public bool Selected;
        public string? Id;

        private bool _canSelect = true;
        protected bool HasChildren = true;
        public bool IsVertical = true;
        private int _addedChildrenCount;
        protected bool CanAddIntoChildContent = true;
        protected int ChildContentLimit = 100;

        protected bool IsInComponent;
        private bool _isOriginalComponent;
        protected bool IsForceAlignment;

        public FrameworkElement ComponentView;
        public Component Parent;
        public List<Component> Children;

        public Border SelectedContent;
        protected Border Content;
        public Grid ParentContent;
        protected Border ShadowContent;
        protected Rectangle BorderContent;

        public ICommand? SelectedCommand;

        public abstract void SelfConstraints();
        protected abstract void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false);
        protected abstract void WhenAlignmentChanged(PropertyNames propertyName, string value);
        public abstract void WhenTextChanged(string propertyName, string value, bool isInitialize = false);
        protected abstract void WhenFileLoaded(string value);
        protected abstract void InitChildContent();
        protected abstract void AddIntoChildContent(FrameworkElement child, int k = -1);
        public abstract bool AllowExpanded(bool isWidth = true);
        public abstract bool AllowAuto(bool isWidth = true);
        protected abstract void Delete(int k = -1);
        protected abstract void WhenWidthChanged(string value);
        protected abstract void WhenHeightChanged(string value);
        protected abstract void OnMoveLeft();
        protected abstract void OnMoveRight();
        protected abstract void OnMoveTop();
        protected abstract void OnMoveBottom();
        protected abstract bool IsSelected(MouseButtonEventArgs e);
        protected abstract bool CanSetProperty(GroupNames groupName, PropertyNames propertyName, string value);
        protected abstract bool CanChildSetProperty(GroupNames groupName, PropertyNames propertyName, string value);
        protected abstract void RestoreProperties();
        protected abstract void CheckVisibilities();
        protected abstract void CallBack(GroupNames groupName, PropertyNames propertyName, string value);

        protected abstract void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value);
        protected abstract void ContinueToInitialize(string groupName, string propertyName, string value);
        protected abstract object GetPropertyGroups();

        public void OnSelected()
        {
            BorderContent.Stroke = new BrushConverter().ConvertFrom("#000000") as SolidColorBrush;
            BorderContent.StrokeThickness = 0.6;
            ShadowContent.Margin = Content.Margin = new Thickness(1);
            Selected = true;

            SelectedCommand?.Execute(
                new Dictionary<string, dynamic>
                {
                    { "Id", Id! },
                    { "selected", true },
                    { "propertyGroups", GetPropertyGroups() },
                    { "componentName", Name }
                }
            );
        }

        private void OnSelectedHandle(object sender)
        {
            SelectedCommand?.Execute(sender);
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
            BorderContent.Stroke = Brushes.Transparent;
            BorderContent.StrokeThickness = 0;
            ShadowContent.Margin = Content.Margin = new Thickness(0);

            foreach (var child in Children)
                child.OnUnselected();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_canSelect || GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.CanSelect) !=
                CanSelectValues.None.ToString() ||
                (!e.OriginalSource.Equals(SelectedContent) && !e.OriginalSource.Equals(Content) &&
                 !e.OriginalSource.Equals(Content.Child) && !IsSelected(e))) return;

            SelectedCommand?.Execute(
                new Dictionary<string, dynamic>
                {
                    { "Id", Id! },
                    { "selected", false },
                    { "propertyGroups", GetPropertyGroups() },
                    { "componentName", Name }
                }
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
                    if (propertyName is PropertyNames.Hl or PropertyNames.Hc or PropertyNames.Hr)
                    {
                        SetPropertyValue(groupName, PropertyNames.Hl, "0");
                        SetPropertyValue(groupName, PropertyNames.Hc, "0");
                        SetPropertyValue(groupName, PropertyNames.Hr, "0");
                        SetPropertyValue(groupName, propertyName, value);

                        SelectedContent.HorizontalAlignment = propertyName switch
                        {
                            PropertyNames.Hl when value == "1" || (value == "0" && !AllowExpanded()) =>
                                HorizontalAlignment.Left,
                            PropertyNames.Hc when value == "1" => HorizontalAlignment.Center,
                            _ when value == "1" => HorizontalAlignment.Right,
                            _ => HorizontalAlignment.Left
                        };

                        var w = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        if (value == "0" && w == SizeValue.Auto.ToString() && AllowExpanded())
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
                        }
                        else if (value == "1" && w == SizeValue.Expand.ToString())
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                        }
                        else if (value == "0" && !AllowExpanded())
                            SetPropertyValue(groupName, PropertyNames.Hl, "1");
                    }
                    else if (propertyName is PropertyNames.Vt or PropertyNames.Vc or PropertyNames.Vb)
                    {
                        SetPropertyValue(groupName, PropertyNames.Vt, "0");
                        SetPropertyValue(groupName, PropertyNames.Vc, "0");
                        SetPropertyValue(groupName, PropertyNames.Vb, "0");
                        SetPropertyValue(groupName, propertyName, value);

                        SelectedContent.VerticalAlignment = propertyName switch
                        {
                            PropertyNames.Vt when value == "1" || (value == "0" && !AllowExpanded(false)) =>
                                VerticalAlignment.Top,
                            PropertyNames.Vc when value == "1" => VerticalAlignment.Center,
                            _ when value == "1" => VerticalAlignment.Bottom,
                            _ => VerticalAlignment.Top
                        };

                        var h = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        if (value == "0" && h == SizeValue.Auto.ToString() && AllowExpanded(false))
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                        }
                        else if (value == "1" && h == SizeValue.Expand.ToString())
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                        }
                        else if (value == "0" && !AllowExpanded(false))
                            SetPropertyValue(groupName, PropertyNames.Vt, "1");
                    }
                }

                #endregion

                #region Transform

                else if (propertyName == PropertyNames.Width)
                {
                    SetPropertyValue(groupName, propertyName, value);
                    if (value == SizeValue.Expand.ToString() && AllowExpanded())
                    {
                        SelectedContent.Width = double.NaN;
                        SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;

                        SetPropertyValue(groupName, propertyName, value);
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hl, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hc, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hr, "0");

                        var h = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        SetPropertyValue(GroupNames.Transform, PropertyNames.He, "1");
                        SetPropertyValue(GroupNames.Transform, PropertyNames.Hve,
                            h == SizeValue.Expand.ToString() ? "1" : "0");
                    }
                    else if (value == SizeValue.Auto.ToString() || value != SizeValue.Expand.ToString())
                    {
                        if (value == SizeValue.Auto.ToString())
                        {
                            SelectedContent.Width = double.NaN;
                        }
                        else
                        {
                            var vd = Helper.ConvertToDouble(value);
                            SelectedContent.Width = vd;
                        }

                        SetPropertyValue(GroupNames.Transform, PropertyNames.He, "0");

                        if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                        else
                        {
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hl, "1");
                            //Evenement pour remplacer la vue des props
                        }
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    SetPropertyValue(groupName, propertyName, value);
                    if (value == SizeValue.Expand.ToString() && AllowExpanded(false))
                    {
                        SelectedContent.Height = double.NaN;
                        SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;

                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vt, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vc, "0");
                        SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vb, "0");

                        var w = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        SetPropertyValue(GroupNames.Transform, PropertyNames.Ve, "1");
                        SetPropertyValue(GroupNames.Transform, PropertyNames.Hve,
                            w == SizeValue.Expand.ToString() ? "1" : "0");
                    }
                    else if (value == SizeValue.Auto.ToString() || value != SizeValue.Expand.ToString())
                    {
                        if (value == SizeValue.Auto.ToString())
                        {
                            SelectedContent.Height = double.NaN;
                        }
                        else
                        {
                            var vd = Helper.ConvertToDouble(value);
                            SelectedContent.Height = vd;
                        }

                        SetPropertyValue(GroupNames.Transform, PropertyNames.Ve, "0");

                        if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Center;
                        else if (GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                        else
                        {
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vt, "1");
                        }
                    }
                }
                else if (propertyName is PropertyNames.He or PropertyNames.Ve)
                {
                    if (value == "1" && ((propertyName == PropertyNames.He && !AllowExpanded()) ||
                                         (propertyName == PropertyNames.Ve && !AllowExpanded(false)))) return;

                    SetPropertyValue(groupName, propertyName, value);
                    OnUpdated(groupName,
                        propertyName is PropertyNames.He ? PropertyNames.Width : PropertyNames.Height,
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue.Auto.ToString());
                }
                else if (propertyName is PropertyNames.Hve)
                {
                    if (value == "1" && !AllowExpanded() && !AllowExpanded(false)) return;
                    
                    OnUpdated(groupName, PropertyNames.Width,
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue.Auto.ToString());
                    OnUpdated(groupName, PropertyNames.Height,
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue.Auto.ToString());
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.MoveParentToChild)
                {
                    if (Children.Count <= 0) return;

                    SelectedCommand?.Execute(
                        new Dictionary<string, dynamic>
                        {
                            { "Id", Id! },
                            { "selected", false },
                            { "propertyGroups", GetPropertyGroups() },
                            { "componentName", Name }
                        }
                    );
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
                        SetPropertyValue(groupName, propertyName, vd + "");
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
                    else if (propertyName == PropertyNames.Color)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.Text)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextWrap)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextTrimming)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.LineSpacing)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.CurrentTextIndex)
                    {
                        SetPropertyValue(groupName, propertyName, value);
                    }
                }

                #endregion

                #region Appearance

                else if (propertyName == PropertyNames.FillColor)
                {
                    Content.Background = ShadowContent.Background = value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent
                        : new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Opacity)
                {
                    var vd = Helper.ConvertToDouble(value);
                    Content.Opacity = ShadowContent.Opacity = vd;
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
                    var gap =
                        Helper.ConvertToDouble(GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Gap));
                    var marginBottom = IsVertical ? gap + vd : vd;
                    var marginRight = !IsVertical ? gap + vd : vd;

                    SelectedContent.Margin = new Thickness(vd, vd, marginRight, marginBottom);
                }
                else if (propertyName == PropertyNames.MarginLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(vd, SelectedContent.Margin.Top,
                        SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginTop)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, vd,
                        SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    var gap =
                        Helper.ConvertToDouble(GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Gap));
                    var marginRight = !IsVertical ? gap + vd : vd;

                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top,
                        marginRight, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginBottom)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    var gap =
                        Helper.ConvertToDouble(GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Gap));
                    var marginBottom = IsVertical ? gap + vd : vd;

                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top,
                        SelectedContent.Margin.Right, marginBottom);
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

                    Content.Padding = ShadowContent.Padding = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.PaddingLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(vd, Content.Padding.Top,
                        Content.Padding.Right,
                        Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingTop)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, vd,
                        Content.Padding.Right,
                        Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top,
                        vd,
                        Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingBottom)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top,
                        Content.Padding.Right,
                        vd);
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
                    SetPropertyValue(groupName, PropertyNames.BorderLeftWidth,
                        vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRightWidth,
                        vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderTopWidth,
                        vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderBottomWidth,
                        vd.ToString(CultureInfo.InvariantCulture));

                    SetPropertyValue(groupName, PropertyNames.CBorder, "1");

                    Content.BorderThickness = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.BorderColor)
                {
                    Content.BorderBrush = value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent
                        : new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                    SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderLeftWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(vd, Content.BorderThickness.Top,
                        Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderTopWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, vd * 0.65,
                        Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderRightWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top,
                        vd, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderBottomWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top,
                        Content.BorderThickness.Right, vd * 0.65);
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
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusTopLeft,
                        vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusTopRight,
                        vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusBottomLeft,
                        vd.ToString(CultureInfo.InvariantCulture));
                    SetPropertyValue(groupName, PropertyNames.BorderRadiusBottomRight,
                        vd.ToString(CultureInfo.InvariantCulture));

                    SetPropertyValue(groupName, PropertyNames.CBorderRadius, "1");

                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight, vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        vd,
                        Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
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

                    if (ShadowContent.Effect == null!)
                        ShadowContent.Effect = new DropShadowEffect();

                    (ShadowContent.Effect as DropShadowEffect)!.ShadowDepth = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.BlurRadius)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    SetPropertyValue(groupName, propertyName, value);

                    if (ShadowContent.Effect == null!)
                        ShadowContent.Effect = new DropShadowEffect();

                    (ShadowContent.Effect as DropShadowEffect)!.BlurRadius = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowDirection)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    SetPropertyValue(groupName, propertyName, value);
                    (ShadowContent.Effect as DropShadowEffect)!.Direction = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowColor)
                {
                    if (value == "0") value = ColorValue.Transparent.ToString();
                    SetPropertyValue(groupName, propertyName, value);

                    if (ShadowContent.Effect == null!)
                        ShadowContent.Effect = new DropShadowEffect();

                    (ShadowContent.Effect as DropShadowEffect)!.Color = value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent.Color
                        : (new BrushConverter().ConvertFrom(value) as SolidColorBrush)!.Color;
                }

                #endregion

                ContinueToUpdate(groupName, propertyName, value);
                Parent?.CallBack(groupName, propertyName, value);
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
                else if (propertyName is PropertyNames.Height or PropertyNames.Ve or PropertyNames.Hve)
                {
                    var value2 = propertyName is PropertyNames.Height ? value :
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue
                            .Auto.ToString();
                    WhenHeightChanged(value2);
                }

                if (propertyName is PropertyNames.Width or PropertyNames.He or PropertyNames.Hve)
                {
                    var value2 = propertyName is PropertyNames.Width ? value :
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue
                            .Auto.ToString();
                    WhenWidthChanged(value2);
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
                //var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);
                foreach (var prop in group.Properties)
                {
                    //var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), prop.Name);

                    #region Alignement

                    if (group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Name == PropertyNames.Hl.ToString() && prop.Value == "1")
                        {
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else if (prop.Name == PropertyNames.Hc.ToString() && prop.Value == "1")
                        {
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                        }
                        else if (prop.Name == PropertyNames.Hr.ToString() && prop.Value == "1")
                        {
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                        }
                        else if (prop.Name == PropertyNames.Vt.ToString() && prop.Value == "1")
                        {
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (prop.Name == PropertyNames.Vc.ToString() && prop.Value == "1")
                        {
                            SelectedContent.VerticalAlignment = VerticalAlignment.Center;
                        }
                        else if (prop.Name == PropertyNames.Vb.ToString() && prop.Value == "1")
                        {
                            SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                        }

                        var hl = group.GetValue(PropertyNames.Hl);
                        var hc = group.GetValue(PropertyNames.Hc);
                        var hr = group.GetValue(PropertyNames.Hr);
                        var w = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        if (hl == "0" && hc == "0" && hr == "0" && w == SizeValue.Expand.ToString())
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;

                        var vt = group.GetValue(PropertyNames.Vt);
                        var vc = group.GetValue(PropertyNames.Vc);
                        var vb = group.GetValue(PropertyNames.Vb);
                        var h = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        if (vt == "0" && vc == "0" && vb == "0" && h == SizeValue.Expand.ToString())
                            SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
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
                        else if (prop.Value == SizeValue.Auto.ToString() || prop.Value != SizeValue.Expand.ToString())
                        {
                            if (prop.Value == SizeValue.Auto.ToString())
                            {
                                SelectedContent.Width = double.NaN;
                            }
                            else
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                SelectedContent.Width = vd;
                            }

                            if (selfAlignGroup.GetValue(PropertyNames.Hl) == "1")
                                SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (selfAlignGroup.GetValue(PropertyNames.Hc) == "1")
                                SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (selfAlignGroup.GetValue(PropertyNames.Hr) == "1")
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
                        else if (prop.Value == SizeValue.Auto.ToString() || prop.Value != SizeValue.Expand.ToString())
                        {
                            if (prop.Value == SizeValue.Auto.ToString())
                            {
                                SelectedContent.Height = double.NaN;
                            }
                            else
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                SelectedContent.Height = vd;
                            }

                            if (selfAlignGroup.GetValue(PropertyNames.Vt) == "1")
                                SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                            else if (selfAlignGroup.GetValue(PropertyNames.Vc) == "1")
                                SelectedContent.VerticalAlignment = VerticalAlignment.Center;
                            else if (selfAlignGroup.GetValue(PropertyNames.Vb) == "1")
                                SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                            else SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                        }
                    }

                    #endregion

                    #region Text

                    else if (group.Name == GroupNames.Text.ToString())
                    {
                        WhenTextChanged(prop.Name, prop.Value, true);
                    }

                    #endregion

                    #region Appearance

                    else if (prop.Name == PropertyNames.FillColor.ToString())
                    {
                        Content.Background = ShadowContent.Background = prop.Value == ColorValue.Transparent.ToString()
                            ? Brushes.Transparent
                            : new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.Opacity.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Opacity = ShadowContent.Opacity = vd;
                    }
                    else if (prop.Name == PropertyNames.Margin.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        var gap =
                            Helper.ConvertToDouble(GetGroupProperties(GroupNames.Transform)
                                .GetValue(PropertyNames.Gap));
                        var marginBottom = IsVertical ? gap + vd : vd;
                        var marginRight = !IsVertical ? gap + vd : vd;

                        SelectedContent.Margin = new Thickness(vd, vd, marginRight, marginBottom);
                    }
                    else if (prop.Name == PropertyNames.MarginLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(vd, SelectedContent.Margin.Top,
                            SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, vd,
                            SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        var gap =
                            Helper.ConvertToDouble(GetGroupProperties(GroupNames.Transform)
                                .GetValue(PropertyNames.Gap));
                        var marginRight = !IsVertical ? gap + vd : vd;

                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left,
                            SelectedContent.Margin.Top, marginRight, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        var gap =
                            Helper.ConvertToDouble(GetGroupProperties(GroupNames.Transform)
                                .GetValue(PropertyNames.Gap));
                        var marginBottom = IsVertical ? gap + vd : vd;

                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top,
                            SelectedContent.Margin.Right, marginBottom);
                    }
                    else if (prop.Name == PropertyNames.Padding.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(vd, Content.Padding.Top,
                            Content.Padding.Right,
                            Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, vd,
                            Content.Padding.Right,
                            Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left,
                            Content.Padding.Top, vd,
                            Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left,
                            Content.Padding.Top,
                            Content.Padding.Right, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderColor.ToString())
                    {
                        Content.BorderBrush = prop.Value == ColorValue.Transparent.ToString()
                            ? Brushes.Transparent
                            : new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.BorderLeftWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(vd, Content.BorderThickness.Top,
                            Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderTopWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left, vd * 0.65,
                            Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderRightWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left,
                            Content.BorderThickness.Top, vd, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderBottomWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left,
                            Content.BorderThickness.Top, Content.BorderThickness.Right, vd * 0.65);
                    }
                    else if (prop.Name == PropertyNames.BorderRadius.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd,
                            Content.CornerRadius.TopRight,
                            Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(
                            Content.CornerRadius.TopLeft,
                            Content.CornerRadius.TopRight,
                            Content.CornerRadius.BottomRight, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(
                            Content.CornerRadius.TopLeft, vd,
                            Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(
                            Content.CornerRadius.TopLeft,
                            Content.CornerRadius.TopRight,
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
                        if (ShadowContent.Effect == null!) ShadowContent.Effect = new DropShadowEffect();
                        (ShadowContent.Effect as DropShadowEffect)!.ShadowDepth = vd;
                    }
                    else if (prop.Name == PropertyNames.BlurRadius.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (ShadowContent.Effect == null!) ShadowContent.Effect = new DropShadowEffect();
                        (ShadowContent.Effect as DropShadowEffect)!.BlurRadius = vd;
                    }
                    else if (prop.Name == PropertyNames.ShadowDirection.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (ShadowContent.Effect == null!) ShadowContent.Effect = new DropShadowEffect();
                        (ShadowContent.Effect as DropShadowEffect)!.Direction = vd;
                    }
                    else if (prop.Name == PropertyNames.ShadowColor.ToString())
                    {
                        if (prop.Value == "0") prop.Value = ColorValue.Transparent.ToString();
                        if (ShadowContent.Effect == null!) ShadowContent.Effect = new DropShadowEffect();
                        (ShadowContent.Effect as DropShadowEffect)!.Color =
                            prop.Value == ColorValue.Transparent.ToString()
                                ? Brushes.Transparent.Color
                                : (new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush)!.Color;
                    }

                    #endregion

                    ContinueToInitialize(group.Name, prop.Name, prop.Value);
                    //CallBack(groupName, propertyName, prop.Value);
                }
            }
        }

        public void OnUpdateComponent(CompSerializer sender)
        {
            if (Children.Count == 0) return;

            var index = new List<int>();
            var found = false;
            foreach (var child in Children)
            {
                if (sender.Id != child.Id)
                {
                    child.OnUpdateComponent(sender);
                    continue;
                }

                found = true;
                index.Add(Children.IndexOf(child));
            }

            if (!found) return;

            foreach (var i in index)
            {
                var compSerialize = System.Text.Json.JsonSerializer.Serialize(Children[i].OnSerializer());
                var senderSerialize = System.Text.Json.JsonSerializer.Serialize(sender);
                var senderDeSerialize = System.Text.Json.JsonSerializer.Deserialize<CompSerializer>(senderSerialize);

                var component = ComponentHelper.GetComponent(senderDeSerialize!.Name!);
                component.SelectedCommand = new RelayCommand(OnSelectedHandle);
                component.OnDeserializer(senderDeSerialize);
                Delete(i);
                Children.Insert(i, component);
                AddIntoChildContent(component.ComponentView, i);
                ResetChildAlignment(i);
                LayoutConstraints(i);

                var compDeSerialize = System.Text.Json.JsonSerializer.Deserialize<CompSerializer>(compSerialize);
                OnUpdateProperties(compDeSerialize!, i);
            }
        }

        private void OnUpdateProperties(CompSerializer sender, int i)
        {
            var groups = sender.Properties;
            foreach (var group in groups!)
            {
                var groupEnum = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);
                foreach (var property in group.Properties)
                {
                    if (property.ComponentVisibility != VisibilityValue.Visible.ToString()) return;
                    var propertyEnum = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                    if (groupEnum == GroupNames.SelfAlignment && property.Value == "1")
                    {
                        Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                    }
                    else if (groupEnum == GroupNames.Global)
                    {
                        if (propertyEnum == PropertyNames.FilePicker)
                            Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                    }
                    else if (groupEnum == GroupNames.Transform)
                    {
                        if (propertyEnum is PropertyNames.Width or PropertyNames.Height)
                            Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                    }
                    else if (groupEnum == GroupNames.Appearance)
                    {
                        if (propertyEnum is PropertyNames.MarginLeft or
                            PropertyNames.MarginRight or
                            PropertyNames.MarginTop or
                            PropertyNames.MarginBottom or
                            PropertyNames.FillColor)
                            Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                    }
                    else if (groupEnum == GroupNames.Shadow)
                    {
                        if (propertyEnum is PropertyNames.ShadowColor or
                            PropertyNames.ShadowDepth or
                            PropertyNames.ShadowDirection)
                            Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                    }
                }
            }

            for (var k = 0; k < Children[k].Children.Count; k++)
            {
                if (k >= sender.Children!.Count ||
                    sender.Children[k].GetType().Name == Children[i].Children[k].GetType().Name) continue;
                Children[i].Children[k].OnUpdateProperties(sender.Children![k], k);
            }
        }

        public void OnAdd(Component component, bool isExpanded = false)
        {
            if (!HasChildren || Children.Count >= ChildContentLimit) return;
            component.Parent = this;
            component.SelectedCommand = new RelayCommand(OnSelectedHandle);

            AddIntoChildContent(component.ComponentView);
            Children.Add(component);

            var expanded = isExpanded;
            foreach (var child in Children)
            {
                var d = child.GetGroupProperties(GroupNames.Transform)
                    .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                expanded = expanded || d == SizeValue.Expand.ToString();
            }

            LayoutConstraints(Children.Count - 1, false, expanded);
        }

        public string OnCopyOrPaste(string value = null!, bool isPaste = false, bool isComponent = false)
        {
            CompSerializer valueD = null!;
            if (value != null!)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerializer>(value)!;

            if (Selected && isPaste && valueD != null! && CanAddIntoChildContent && Children.Count < ChildContentLimit)
            {
                var name = valueD.Name!;
                var component = ComponentHelper.GetComponent(name);
                component.Parent = this;
                component.SelectedCommand = new RelayCommand(OnSelectedHandle);

                if (!isComponent)
                {
                    component.CreateId(false);
                    valueD.Id = component.Id;
                }

                component.OnDeserializer(valueD);

                var expanded = false;
                foreach (var child in Children)
                {
                    var d = child.GetGroupProperties(GroupNames.Transform)
                        .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                    expanded = expanded || d == SizeValue.Expand.ToString();
                }

                AddIntoChildContent(component.ComponentView);
                Children.Add(component);

                IsForceAlignment = true;
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
                Id = Id,
                Name = Name.ToString(),
                HasChildren = HasChildren,
                IsVertical = IsVertical,
                AddedChildrenCount = _addedChildrenCount,
                CanAddIntoChildContent = CanAddIntoChildContent,
                ChildContentLimit = ChildContentLimit,
                IsInComponent = IsInComponent,
                IsOriginalComponent = _isOriginalComponent,
                IsForceAlignment = IsForceAlignment,
                Properties = PropertyGroups,
                Children = Children.Count > 0 ? children : null
            };
        }

        public void OnDeserializer(CompSerializer compSerializer)
        {
            PropertyGroups = compSerializer.Properties;
            HasChildren = compSerializer.HasChildren;
            IsVertical = compSerializer.IsVertical;
            _addedChildrenCount = compSerializer.AddedChildrenCount;
            CanAddIntoChildContent = compSerializer.CanAddIntoChildContent;
            ChildContentLimit = compSerializer.ChildContentLimit;
            IsInComponent = compSerializer.IsInComponent;
            _isOriginalComponent = compSerializer.IsOriginalComponent;
            IsForceAlignment = compSerializer.IsForceAlignment;

            if (compSerializer.Id == null)
            {
                CreateId();
            }
            else
            {
                Id = compSerializer.Id;
                ComponentHelper.SaveId(Id);
            }

            #region

            if (compSerializer.Children != null)
            {
                _addedChildrenCount = compSerializer.Children.Count;

                var components = new List<Component>();
                var expanded = false;

                foreach (var child in compSerializer.Children)
                {
                    var component = ComponentHelper.GetComponent(child.Name!);
                    component.Parent = this;
                    component.SelectedCommand = new RelayCommand(OnSelectedHandle);
                    component.CreateId();

                    component.OnDeserializer(child);
                    components.Add(component);
                    var d = component.GetGroupProperties(GroupNames.Transform)
                        .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                    expanded = expanded || d == SizeValue.Expand.ToString();
                }

                Children.Clear();
                foreach (var component in components)
                {
                    Children.Add(component);
                    AddIntoChildContent(component.ComponentView);
                    LayoutConstraints(Children.Count - 1, true, expanded);
                }
            }

            #endregion

            if (Name == ComponentList.Text && Children.Count == 0)
                (this as TextModel)?.AddFirstChild();

            OnInitialize();

            CheckVisibilities();
            RestoreProperties();
        }

        public StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement
            {
                Node = Name.ToString(),
                Selected = Selected,
                Children = [],
                IsSimpleElement = HasChildren
            };

            foreach (var child in Children)
                structuralElement.Children.Add(child.AddToStructuralView());

            return structuralElement;
        }

        public void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                SelectedCommand?.Execute(
                    new Dictionary<string, dynamic>
                    {
                        { "Id", Id! },
                        { "selected", false },
                        { "propertyGroups", GetPropertyGroups() },
                        { "componentName", Name }
                    }
                );
                OnSelected();
            }
            else if (structuralElement.Children != null! && structuralElement.Children.Count == Children.Count)
            {
                for (var i = 0; i < structuralElement.Children.Count; i++)
                    Children[i].SelectFromStructuralView(structuralElement.Children[i]);
            }
        }

        private void ResetChildAlignment(int i)
        {
            Children[i].SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hl, "0");
            Children[i].SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hc, "0");
            Children[i].SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hr, "0");

            Children[i].SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vt, "0");
            Children[i].SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vc, "0");
            Children[i].SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vb, "0");
        }

        public void SetChildGap(int i, double oldGap, double newGap)
        {
            var marginBottom = Children[i].SelectedContent.Margin.Bottom - (IsVertical ? oldGap - newGap : 0);
            var marginRight = Children[i].SelectedContent.Margin.Right - (!IsVertical ? oldGap - newGap : 0);

            Children[i].SelectedContent.Margin = new Thickness(Children[i].SelectedContent.Margin.Left,
                Children[i].SelectedContent.Margin.Top, marginRight, marginBottom);

            Children[i].SetPropertyValue(GroupNames.Transform, PropertyNames.Gap, $"{newGap}");
        }

        public GroupProperties GetGroupProperties(GroupNames groupName)
        {
            foreach (var propertyGroup in PropertyGroups!.Where(propertyGroup =>
                         propertyGroup.Name == groupName.ToString()))
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
                if (group.Name != groupName.ToString()) continue;
                foreach (var property in group.Properties)
                {
                    j++;
                    if (property.Name != propertyName.ToString()) continue;
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
                if (group.Name != groupName.ToString()) continue;
                PropertyGroups[i].Visibility =
                    isVisible ? VisibilityValue.Visible.ToString() : VisibilityValue.Collapsed.ToString();
                foreach (var property in group.Properties)
                {
                    var j = group.Properties.IndexOf(property);
                    PropertyGroups[i].Properties[j].Visibility = isVisible
                        ? VisibilityValue.Visible.ToString()
                        : VisibilityValue.Collapsed.ToString();
                }

                return;
            }
        }

        public void SetGroupOnlyVisibility(GroupNames groupName, bool isVisible = true)
        {
            var i = -1;
            foreach (var group in PropertyGroups!)
            {
                i++;
                if (group.Name != groupName.ToString()) continue;
                PropertyGroups[i].Visibility =
                    isVisible ? VisibilityValue.Visible.ToString() : VisibilityValue.Collapsed.ToString();
                return;
            }
        }

        public void SetPropertiesOnlyVisibility(GroupNames groupName, bool isVisible = true)
        {
            var i = -1;
            foreach (var group in PropertyGroups!)
            {
                i++;
                if (group.Name != groupName.ToString()) continue;
                foreach (var property in group.Properties)
                {
                    var j = group.Properties.IndexOf(property);
                    PropertyGroups[i].Properties[j].Visibility = isVisible
                        ? VisibilityValue.Visible.ToString()
                        : VisibilityValue.Collapsed.ToString();
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
                if (group.Name != groupName.ToString()) continue;
                var j = -1;
                foreach (var property in group.Properties)
                {
                    j++;
                    if (property.Name != propertyName.ToString()) continue;
                    PropertyGroups[i].Properties[j].Visibility = isVisible
                        ? VisibilityValue.Visible.ToString()
                        : VisibilityValue.Collapsed.ToString();
                    return;
                }
            }
        }

        public bool IsNullAlignment(GroupNames align, string direction)
        {
            var group = GetGroupProperties(align == GroupNames.Alignment ? align : GroupNames.SelfAlignment);
            var propNames = direction == "Horizontal"
                ? ["HL", "HC", "HR"]
                : new List<string> { "VT", "VC", "VB" };

            var isNull = false;
            foreach (var property in group.Properties)
            {
                var found = propNames.Any(name => property.Name == name);

                if (!found) continue;

                isNull = property.Value == "0";
                if (!isNull) break;
            }

            return isNull;
        }

        private void CreateId(bool isMyself = true)
        {
            if (Id != null)
                ComponentHelper.DeleteId(Id);

            Id = ComponentHelper.GenerateId();

            if (isMyself) return;

            foreach (var child in Children)
                child.CreateId(isMyself);
        }

        public void CheckIsExistId()
        {
            if (Id == null) CreateId();

            foreach (var child in Children)
                child.CheckIsExistId();
        }

        public void CanSelectAll(bool isCan = false)
        {
            _canSelect = isCan;
            foreach (var child in Children)
                child.CanSelectAll(isCan);
        }

        protected void OnInit()
        {
            InitChildContent();

            Content = new Border();
            ShadowContent = new Border();
            BorderContent = new Rectangle
            {
                StrokeDashArray = new DoubleCollection([6.0, 3.0])
            };

            ParentContent = new Grid();
            ParentContent.Children.Add(ShadowContent);
            ParentContent.Children.Add(BorderContent);
            ParentContent.Children.Add(Content);

            SelectedContent = new Border
            {
                Child = ParentContent
            };

            ComponentView = SelectedContent;
            ComponentView.PreviewMouseDown += OnMouseDown;
            ComponentView.MouseEnter += OnMouseEnter;

            PropertyGroups = [];
            Children = [];

            #region

            PropertyGroups = new List<GroupProperties>
            {
                new()
                {
                    Name = GroupNames.Alignment.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new()
                        {
                            Name = PropertyNames.Hl.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Hc.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Hr.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Vt.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Vc.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Vb.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.SpaceBetween.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.SpaceAround.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.SpaceEvery.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        }
                    }
                },
                new()
                {
                    Name = GroupNames.Transform.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new()
                        {
                            Name = PropertyNames.Width.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = SizeValue.Expand.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Height.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = SizeValue.Expand.ToString(),
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.X.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Y.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Rot.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.He.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Ve.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Hve.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Stretch.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = ImageStretch.Fill.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Gap.ToString(),
                            Type = PropertyTypes.String.ToString(),
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
                            Value = "Arial",
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
                            Value = "#000000",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Text.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "My Text",
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
                        },
                        new()
                        {
                            Name = PropertyNames.CurrentTextIndex.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.TextTrimming.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        }
                    }
                },
                new()
                {
                    Name = GroupNames.Appearance.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new()
                        {
                            Name = PropertyNames.FillColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
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
                            Value = "1",
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
                new()
                {
                    Name = GroupNames.SelfAlignment.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString(),
                    Properties = new List<Property>
                    {
                        new()
                        {
                            Name = PropertyNames.Hl.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Hc.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Hr.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Vt.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Vc.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.Vb.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        }
                    }
                },
                new()
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
                new()
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
                new()
                {
                    Name = GroupNames.Shadow.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new()
                        {
                            Name = PropertyNames.ShadowDepth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
                        {
                            Name = PropertyNames.BlurRadius.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new()
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