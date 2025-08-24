using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using ConceptorUI.Enums;
using ConceptorUI.Models;
using ConceptorUI.Senders;
using ConceptorUI.ViewModels.Components.GroupProperty;
using ConceptorUI.ViewModels.Container;
using ConceptorUi.ViewModels.Operations;
using ConceptorUI.ViewModels.Text;
using UiDesigner.Inputs;
using UiDesigner.Models;
using UiDesigner.Utils;
using TransformGroup = ConceptorUI.ViewModels.Components.GroupProperty.TransformGroup;

namespace ConceptorUI.ViewModels.Components
{
    internal abstract class Component
    {
        /*
         * ------------------------------------
         * Mettre en place un système de remplacement de composants -> 1
         * Donner une mise en page à un composant -> 2
         * Mettre en place le mécanisme du glisser déposer des composants -> implique le 1 et 2.
         * Multi selections - En cours
         * Annuler et Restaurer -> En cours
         * Composant reutilisable -> En cours
         * Zoom de l'écran -> En cours
         * Margin horizontal
         * ------------------------------------
         * Système de dimensions apparentes
         * - C'est un problème qui se pose lorsqu'on crée un composant
         * qui n'est pas une page, de façon indépendante;
         * - Ces dimensions ne sont pas visibles à l'utilisateur, mais sont définies directement sur
         * les controls du composant le plus externe;
         * - Ces dimensions doivent être détruites lors de l'appel du composant;
         * - Eviter tout simplement que ces dimensions soient completement nulles,
         * sinon le composant sera invisivible à l'utilisateur;
         * ------------------------------------
         * Ctrl + Deplacement de la souris ou Ctrl + Fleches de direction, permet de
         * déplacer le composant sélectionné;
         * ------------------------------------
         * - Mettre en place un algorithme de placement automatique des composants dans des zones
         * disponibles pour éviter le chevauchement;
         * - Mettre en place un algorithme de réarrangement automatique des composants dans des zones
         * disponibles pour éviter le chevauchement;
         */
        public ComponentList Name { get; set; }
        public List<GroupProperties>? PropertyGroups { get; set; }

        public bool Selected;
        public string? Id;
        public string? InterpretationFile;

        private bool _canSelect = true;
        protected bool HasChildren = true;
        public bool IsVertical = true;
        private int _addedChildrenCount;
        protected bool CanAddIntoChildContent = true;
        public bool IsAllowSizeChange = false;
        protected int ChildContentLimit = 100;

        protected bool IsInComponent;
        private bool _isOriginalComponent;
        protected bool IsForceAlignment;

        public FrameworkElement ComponentView;
        public Component Parent;
        public List<Component> Children;

        public Border SelectedContent;
        public Border Content;
        public Grid ParentContent;
        public Border ShadowContent;
        protected Rectangle BorderContent;

        public ICommand? SelectedCommand;
        public ICommand? MouseWheelCommand;

        public abstract void SelfConstraints();
        public abstract void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false);
        public abstract void WhenAlignmentChanged(PropertyNames propertyName, string value);
        public abstract void WhenTextChanged(string propertyName, string value, bool isInitialize = false);
        protected abstract void WhenFileLoaded(string value);
        protected abstract void InitChildContent();
        public abstract void AddIntoChildContent(FrameworkElement child, int k = -1);
        public abstract bool AllowExpanded(bool isWidth = true);
        public abstract bool AllowAuto(bool isWidth = true);
        public abstract void Delete(int k = -1);
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

        public void OnSelected(int clickCount = 1)
        {
            BorderContent.Stroke = new BrushConverter().ConvertFrom("#000000") as SolidColorBrush;
            BorderContent.StrokeThickness = 0.4;
            ShadowContent.Margin = Content.Margin = new Thickness(0.8);

            if (Selected && ComponentHelper.IsMultiSelectionEnable)
            {
                OnUnselected(false);
                return;
            }

            Selected = true;

            SelectedCommand?.Execute(
                new SelectComponentSender
                {
                    Id = Id!,
                    SelectComponentAction =
                        clickCount == 1 ? SelectComponentActions.Select : SelectComponentActions.DoubleClick,
                    PropertyGroups = GetPropertyGroups(),
                    ComponentName = Name
                }
            );
        }

        public void OnSelectedHandle(object sender)
        {
            SelectedCommand?.Execute(sender);
        }
        
        public void OnMouseWheelHandle(object sender)
        {
            MouseWheelCommand?.Execute(sender);
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

        public void OnUnselected(bool isDeep = true)
        {
            Selected = false;
            BorderContent.Stroke = Brushes.Transparent;
            BorderContent.StrokeThickness = 0;
            ShadowContent.Margin = Content.Margin = new Thickness(0);

            if (!isDeep) return;
            foreach (var child in Children)
                child.OnUnselected();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_canSelect || this.GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.CanSelect) !=
                CanSelectValues.None.ToString() ||
                (!e.OriginalSource.Equals(SelectedContent) && !e.OriginalSource.Equals(Content) &&
                 !e.OriginalSource.Equals(Content.Child) && !IsSelected(e))) return;

            if (!ComponentHelper.IsMultiSelectionEnable)
            {
                SelectedCommand?.Execute(
                    new SelectComponentSender
                    {
                        Id = Id!,
                        SelectComponentAction = SelectComponentActions.Unselect,
                        PropertyGroups = GetPropertyGroups(),
                        ComponentName = Name
                    }
                );
            }

            OnSelected(e.ClickCount);
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (GetType().Name != nameof(ContainerModel) || !IsAllowSizeChange) return;
            
            IsAllowSizeChange = false;
            var shape = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Shape);
            if(shape == Shapes.Rectangle.ToString()) return;
            this.RestoreShape();
        }

        public void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false,
            bool allowSavingAction = false)
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
                        this.SetPropertyValue(groupName, PropertyNames.Hl, "0");
                        this.SetPropertyValue(groupName, PropertyNames.Hc, "0");
                        this.SetPropertyValue(groupName, PropertyNames.Hr, "0");
                        this.SetPropertyValue(groupName, propertyName, value);

                        SelectedContent.HorizontalAlignment = propertyName switch
                        {
                            PropertyNames.Hl when value == "1" || (value == "0" && !AllowExpanded()) =>
                                HorizontalAlignment.Left,
                            PropertyNames.Hc when value == "1" => HorizontalAlignment.Center,
                            _ when value == "1" => HorizontalAlignment.Right,
                            _ => HorizontalAlignment.Left
                        };

                        // var oldValue = value == "1" ? "0" : "1";
                        // ComponentHelper.SaveUndoRedoAction(groupName, propertyName, oldValue, value);

                        var w = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        if (value == "0" && w == SizeValue.Auto.ToString() && AllowExpanded())
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
                        }
                        else if (value == "1" && w == SizeValue.Expand.ToString())
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                        }
                        else if (value == "0" && !AllowExpanded())
                            this.SetPropertyValue(groupName, PropertyNames.Hl, "1");
                    }
                    else if (propertyName is PropertyNames.Vt or PropertyNames.Vc or PropertyNames.Vb)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.Vt, "0");
                        this.SetPropertyValue(groupName, PropertyNames.Vc, "0");
                        this.SetPropertyValue(groupName, PropertyNames.Vb, "0");
                        this.SetPropertyValue(groupName, propertyName, value);

                        SelectedContent.VerticalAlignment = propertyName switch
                        {
                            PropertyNames.Vt when value == "1" || (value == "0" && !AllowExpanded(false)) =>
                                VerticalAlignment.Top,
                            PropertyNames.Vc when value == "1" => VerticalAlignment.Center,
                            _ when value == "1" => VerticalAlignment.Bottom,
                            _ => VerticalAlignment.Top
                        };

                        var h = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        if (value == "0" && h == SizeValue.Auto.ToString() && AllowExpanded(false))
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
                        }
                        else if (value == "1" && h == SizeValue.Expand.ToString())
                        {
                            OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                        }
                        else if (value == "0" && !AllowExpanded(false))
                            this.SetPropertyValue(groupName, PropertyNames.Vt, "1");
                    }
                }

                #endregion

                #region Transform

                else if (propertyName == PropertyNames.Width)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                    if (value == SizeValue.Expand.ToString() && AllowExpanded())
                    {
                        SelectedContent.Width = double.NaN;
                        SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;

                        this.SetPropertyValue(groupName, propertyName, value);
                        this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hl, "0");
                        this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hc, "0");
                        this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hr, "0");

                        var h = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        this.SetPropertyValue(GroupNames.Transform, PropertyNames.He, "1");
                        this.SetPropertyValue(GroupNames.Transform, PropertyNames.Hve,
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

                        this.SetPropertyValue(GroupNames.Transform, PropertyNames.He, "0");

                        if (this.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                        else if (this.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Center;
                        else if (this.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr) == "1")
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Right;
                        else
                        {
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Left;
                            this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hl, "1");
                            //Evenement pour remplacer la vue des props
                        }
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                    if (value == SizeValue.Expand.ToString() && AllowExpanded(false))
                    {
                        SelectedContent.Height = double.NaN;
                        SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;

                        this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vt, "0");
                        this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vc, "0");
                        this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vb, "0");

                        var w = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        this.SetPropertyValue(GroupNames.Transform, PropertyNames.Ve, "1");
                        this.SetPropertyValue(GroupNames.Transform, PropertyNames.Hve,
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

                        this.SetPropertyValue(GroupNames.Transform, PropertyNames.Ve, "0");

                        if (this.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                        else if (this.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Center;
                        else if (this.GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb) == "1")
                            SelectedContent.VerticalAlignment = VerticalAlignment.Bottom;
                        else
                        {
                            SelectedContent.VerticalAlignment = VerticalAlignment.Top;
                            this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vt, "1");
                        }
                    }
                }
                else if (propertyName is PropertyNames.He or PropertyNames.Ve)
                {
                    if (value == "1" && ((propertyName == PropertyNames.He && !AllowExpanded()) ||
                                         (propertyName == PropertyNames.Ve && !AllowExpanded(false)))) return;

                    this.SetPropertyValue(groupName, propertyName, value);
                    OnUpdated(groupName,
                        propertyName is PropertyNames.He ? PropertyNames.Width : PropertyNames.Height,
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue.Auto.ToString(), true);
                }
                else if (propertyName is PropertyNames.Hve)
                {
                    if (value == "1" && !AllowExpanded() && !AllowExpanded(false)) return;

                    OnUpdated(groupName, PropertyNames.Width,
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue.Auto.ToString());
                    OnUpdated(groupName, PropertyNames.Height,
                        value == "1" ? SizeValue.Expand.ToString() : SizeValue.Auto.ToString());
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.MoveParentToChild)
                {
                    if (Children.Count <= 0) return;

                    SelectedCommand?.Execute(
                        new SelectComponentSender
                        {
                            Id = Id!,
                            SelectComponentAction = SelectComponentActions.Unselect,
                            PropertyGroups = GetPropertyGroups(),
                            ComponentName = Name
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
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.FontWeight)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.FontStyle)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.FontSize)
                    {
                        var vd = Helper.ConvertToDouble(value);
                        vd = vd == 0 ? 10 : vd;
                        this.SetPropertyValue(groupName, propertyName, vd + "");
                    }
                    else if (propertyName == PropertyNames.AlignLeft)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.AlignRight, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignCenter, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignJustify, "0");
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.AlignCenter)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.AlignLeft, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignRight, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignJustify, "0");
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.AlignRight)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.AlignLeft, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignCenter, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignJustify, "0");
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.AlignJustify)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.AlignLeft, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignRight, "0");
                        this.SetPropertyValue(groupName, PropertyNames.AlignCenter, "0");
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextUnderline)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.TextOverline, "0");
                        this.SetPropertyValue(groupName, PropertyNames.TextThrough, "0");
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextOverline)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.TextUnderline, "0");
                        this.SetPropertyValue(groupName, PropertyNames.TextThrough, "0");
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextThrough)
                    {
                        this.SetPropertyValue(groupName, PropertyNames.TextUnderline, "0");
                        this.SetPropertyValue(groupName, PropertyNames.TextOverline, "0");
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.Color)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.Text)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextWrap)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.TextTrimming)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.LineSpacing)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                    else if (propertyName == PropertyNames.CurrentTextIndex)
                    {
                        this.SetPropertyValue(groupName, propertyName, value);
                    }
                }

                #endregion

                #region Appearance

                else if (propertyName == PropertyNames.FillColor)
                {
                    Content.Background = ShadowContent.Background = value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent
                        : new BrushConverter().ConvertFrom(value) as SolidColorBrush;

                    if (allowSavingAction)
                    {
                        var oldValue = this.GetGroupProperties(groupName).GetValue(propertyName);
                        ComponentHelper.SaveUndoRedoAction(this, groupName, propertyName, oldValue, value);
                    }

                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Opacity)
                {
                    var vd = Helper.ConvertToDouble(value);
                    Content.Opacity = ShadowContent.Opacity = vd;
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.CMargin)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.MarginBtnActif)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Margin)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, PropertyNames.Margin, vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.MarginLeft,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.MarginRight,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.MarginTop,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.MarginBottom,
                        vd.ToString(CultureInfo.InvariantCulture));

                    this.SetPropertyValue(groupName, PropertyNames.CMargin, "1");
                    var gap =
                        Helper.ConvertToDouble(
                            this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Gap));
                    var marginBottom = IsVertical ? gap + vd : vd;
                    var marginRight = !IsVertical ? gap + vd : vd;

                    SelectedContent.Margin = new Thickness(vd, vd, marginRight, marginBottom);
                }
                else if (propertyName == PropertyNames.MarginLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(vd, SelectedContent.Margin.Top,
                        SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginTop)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, vd,
                        SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    var gap =
                        Helper.ConvertToDouble(
                            this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Gap));
                    var marginRight = !IsVertical ? gap + vd : vd;

                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top,
                        marginRight, SelectedContent.Margin.Bottom);
                }
                else if (propertyName == PropertyNames.MarginBottom)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    var gap =
                        Helper.ConvertToDouble(
                            this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Gap));
                    var marginBottom = IsVertical ? gap + vd : vd;

                    SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top,
                        SelectedContent.Margin.Right, marginBottom);
                }
                else if (propertyName == PropertyNames.CPadding)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.PaddingBtnActif)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Padding)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, PropertyNames.Padding, vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.PaddingLeft,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.PaddingRight,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.PaddingTop,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.PaddingBottom,
                        vd.ToString(CultureInfo.InvariantCulture));

                    this.SetPropertyValue(groupName, PropertyNames.CPadding, "1");

                    Content.Padding = ShadowContent.Padding = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.PaddingLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(vd, Content.Padding.Top,
                        Content.Padding.Right,
                        Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingTop)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, vd,
                        Content.Padding.Right,
                        Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top,
                        vd,
                        Content.Padding.Bottom);
                }
                else if (propertyName == PropertyNames.PaddingBottom)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, Content.Padding.Top,
                        Content.Padding.Right,
                        vd);
                }
                else if (propertyName == PropertyNames.CBorder)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderBtnActif)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, PropertyNames.BorderWidth,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderLeftWidth,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderRightWidth,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderTopWidth,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderBottomWidth,
                        vd.ToString(CultureInfo.InvariantCulture));

                    this.SetPropertyValue(groupName, PropertyNames.CBorder, "1");

                    Content.BorderThickness = new Thickness(vd);
                }
                else if (propertyName == PropertyNames.BorderColor)
                {
                    Content.BorderBrush = value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent
                        : new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderLeftWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(vd, Content.BorderThickness.Top,
                        Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderTopWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, vd * 0.65,
                        Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderRightWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top,
                        vd, Content.BorderThickness.Bottom);
                }
                else if (propertyName == PropertyNames.BorderBottomWidth)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.BorderThickness = new Thickness(Content.BorderThickness.Left, Content.BorderThickness.Top,
                        Content.BorderThickness.Right, vd * 0.65);
                }
                else if (propertyName == PropertyNames.CBorderRadius)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderRadBtnActif)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.BorderRadius)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, PropertyNames.BorderRadius,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderRadiusTopLeft,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderRadiusTopRight,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderRadiusBottomLeft,
                        vd.ToString(CultureInfo.InvariantCulture));
                    this.SetPropertyValue(groupName, PropertyNames.BorderRadiusBottomRight,
                        vd.ToString(CultureInfo.InvariantCulture));

                    this.SetPropertyValue(groupName, PropertyNames.CBorderRadius, "1");

                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight, vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        vd,
                        Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    this.SetPropertyValue(groupName, propertyName, vd.ToString(CultureInfo.InvariantCulture));
                    Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
                        vd, Content.CornerRadius.BottomLeft);
                }

                #endregion

                #region Global

                else if (propertyName == PropertyNames.CanSelect)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.Focus)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.SelectedMode)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                }
                else if (propertyName == PropertyNames.FilePicker)
                {
                    this.SetPropertyValue(groupName, propertyName, value);
                    WhenFileLoaded(value);
                }

                #endregion

                #region Shadow Property

                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowDepth)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    this.SetPropertyValue(groupName, propertyName, value);

                    if (ShadowContent.Effect == null!)
                        ShadowContent.Effect = new DropShadowEffect();

                    (ShadowContent.Effect as DropShadowEffect)!.ShadowDepth = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.BlurRadius)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    this.SetPropertyValue(groupName, propertyName, value);

                    if (ShadowContent.Effect == null!)
                        ShadowContent.Effect = new DropShadowEffect();

                    (ShadowContent.Effect as DropShadowEffect)!.BlurRadius = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowDirection)
                {
                    double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                    this.SetPropertyValue(groupName, propertyName, value);
                    (ShadowContent.Effect as DropShadowEffect)!.Direction = vd;
                }
                else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowColor)
                {
                    if (value == "0") value = ColorValue.Transparent.ToString();
                    this.SetPropertyValue(groupName, propertyName, value);

                    if (ShadowContent.Effect == null!)
                        ShadowContent.Effect = new DropShadowEffect();

                    (ShadowContent.Effect as DropShadowEffect)!.Color = value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent.Color
                        : (new BrushConverter().ConvertFrom(value) as SolidColorBrush)!.Color;
                }

                #endregion

                ContinueToUpdate(groupName, propertyName, value);
                Parent?.CallBack(groupName, propertyName, value);

                CheckVisibilities();
                RestoreProperties();
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
                else if (propertyName is PropertyNames.Width or PropertyNames.He or PropertyNames.Hve)
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
                    child.OnUpdated(groupName, propertyName, value, allowSavingAction: allowSavingAction);

                #endregion
            }
        }

        public void OnInitialize()
        {
            var cMargin = this.GetGroupProperties(GroupNames.Appearance).GetValue(PropertyNames.CMargin);
            var cPadding = this.GetGroupProperties(GroupNames.Appearance).GetValue(PropertyNames.CPadding);
            var cBorder = this.GetGroupProperties(GroupNames.Appearance).GetValue(PropertyNames.CBorder);
            var cBorderRadius = this.GetGroupProperties(GroupNames.Appearance).GetValue(PropertyNames.CBorderRadius);
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
                        var w = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                        if (hl == "0" && hc == "0" && hr == "0" && w == SizeValue.Expand.ToString())
                            SelectedContent.HorizontalAlignment = HorizontalAlignment.Stretch;

                        var vt = group.GetValue(PropertyNames.Vt);
                        var vc = group.GetValue(PropertyNames.Vc);
                        var vb = group.GetValue(PropertyNames.Vb);
                        var h = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                        if (vt == "0" && vc == "0" && vb == "0" && h == SizeValue.Expand.ToString())
                            SelectedContent.VerticalAlignment = VerticalAlignment.Stretch;
                    }

                    #endregion
                    
                    #region Transform

                    else if (prop.Name == PropertyNames.Width.ToString())
                    {
                        var selfAlignGroup = this.GetGroupProperties(GroupNames.SelfAlignment);
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
                        var selfAlignGroup = this.GetGroupProperties(GroupNames.SelfAlignment);
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
                    else if (prop.Name == PropertyNames.Margin.ToString() && cMargin == "1")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        var gap =
                            Helper.ConvertToDouble(this.GetGroupProperties(GroupNames.Transform)
                                .GetValue(PropertyNames.Gap));
                        var marginBottom = IsVertical ? gap + vd : vd;
                        var marginRight = !IsVertical ? gap + vd : vd;

                        SelectedContent.Margin = new Thickness(vd, vd, marginRight, marginBottom);
                    }
                    else if (prop.Name == PropertyNames.MarginLeft.ToString() && cMargin == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(vd, SelectedContent.Margin.Top,
                            SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginTop.ToString() && cMargin == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, vd,
                            SelectedContent.Margin.Right, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginRight.ToString() && cMargin == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        var gap =
                            Helper.ConvertToDouble(this.GetGroupProperties(GroupNames.Transform)
                                .GetValue(PropertyNames.Gap));
                        var marginRight = !IsVertical ? gap + vd : vd;

                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left,
                            SelectedContent.Margin.Top, marginRight, SelectedContent.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginBottom.ToString() && cMargin == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        var gap =
                            Helper.ConvertToDouble(this.GetGroupProperties(GroupNames.Transform)
                                .GetValue(PropertyNames.Gap));
                        var marginBottom = IsVertical ? gap + vd : vd;

                        SelectedContent.Margin = new Thickness(SelectedContent.Margin.Left, SelectedContent.Margin.Top,
                            SelectedContent.Margin.Right, marginBottom);
                    }
                    else if (prop.Name == PropertyNames.Padding.ToString() && cPadding == "1")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.PaddingLeft.ToString() && cPadding == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(vd, Content.Padding.Top,
                            Content.Padding.Right,
                            Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingTop.ToString() && cPadding == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left, vd,
                            Content.Padding.Right,
                            Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingRight.ToString() && cPadding == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left,
                            Content.Padding.Top, vd,
                            Content.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingBottom.ToString() && cPadding == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.Padding = ShadowContent.Padding = new Thickness(Content.Padding.Left,
                            Content.Padding.Top,
                            Content.Padding.Right, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderWidth.ToString() && cBorder == "1")
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
                    else if (prop.Name == PropertyNames.BorderLeftWidth.ToString() && cBorder == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(vd, Content.BorderThickness.Top,
                            Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderTopWidth.ToString() && cBorder == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left, vd * 0.65,
                            Content.BorderThickness.Right, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderRightWidth.ToString() && cBorder == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left,
                            Content.BorderThickness.Top, vd, Content.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderBottomWidth.ToString() && cBorder == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.BorderThickness = new Thickness(Content.BorderThickness.Left,
                            Content.BorderThickness.Top, Content.BorderThickness.Right, vd * 0.65);
                    }
                    else if (prop.Name == PropertyNames.BorderRadius.ToString() && cBorderRadius == "1")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString() && cBorderRadius == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(vd,
                            Content.CornerRadius.TopRight,
                            Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString() && cBorderRadius == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(
                            Content.CornerRadius.TopLeft,
                            Content.CornerRadius.TopRight,
                            Content.CornerRadius.BottomRight, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString() && cBorderRadius == "0")
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        Content.CornerRadius = ShadowContent.CornerRadius = new CornerRadius(
                            Content.CornerRadius.TopLeft, vd,
                            Content.CornerRadius.BottomRight, Content.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString() && cBorderRadius == "0")
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
        
        public void OnAdd(Component component, bool isExpanded = false)
        {
            if (!HasChildren || Children.Count >= ChildContentLimit) return;
            component.Parent = this;
            component.SelectedCommand = new RelayCommand(OnSelectedHandle);
            component.MouseWheelCommand = new RelayCommand(OnMouseWheelHandle);

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
                valueD = JsonSerializer.Deserialize<CompSerializer>(value)!;

            if (Selected && isPaste && valueD != null! && CanAddIntoChildContent && Children.Count < ChildContentLimit)
            {
                var name = valueD.Name!;
                var component = ComponentHelper.GetComponent(name);
                component.Parent = this;
                component.SelectedCommand = new RelayCommand(OnSelectedHandle);
                component.MouseWheelCommand = new RelayCommand(OnMouseWheelHandle);

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
                return JsonSerializer.Serialize(OnSerializer());
            else if (!Selected)
                foreach (var child in Children)
                {
                    var comp = child.OnCopyOrPaste(value!, isPaste, isComponent);
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
            //PropertyGroups = compSerializer.Properties;
            this.AddMissingProperties(compSerializer.Properties!);
            
            if (compSerializer.Name == ComponentList.Container.ToString() && compSerializer.Children?.Count > 0)
            {
                if (compSerializer.Children[0].Name == ComponentList.Text.ToString())
                {
                    var group = compSerializer.GetGroup(GroupNames.Alignment);
                    var vt = group.GetValue(PropertyNames.Vt);
                    var vc = group.GetValue(PropertyNames.Vc);
                    var vb = group.GetValue(PropertyNames.Vb);
                    Console.WriteLine($"vt = {vt} - vc = {vc} - vb = {vb}");
                }
            }

            /*HasChildren = compSerializer.HasChildren;
            IsVertical = compSerializer.IsVertical;
            _addedChildrenCount = compSerializer.AddedChildrenCount;
            CanAddIntoChildContent = compSerializer.CanAddIntoChildContent;
            ChildContentLimit = compSerializer.ChildContentLimit;
            IsInComponent = compSerializer.IsInComponent;
            _isOriginalComponent = compSerializer.IsOriginalComponent;
            IsForceAlignment = compSerializer.IsForceAlignment;*/

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
                    component.MouseWheelCommand = new RelayCommand(OnMouseWheelHandle);
                    //component.CreateId();

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
                    new SelectComponentSender
                    {
                        Id = Id!,
                        SelectComponentAction = SelectComponentActions.Unselect,
                        PropertyGroups = GetPropertyGroups(),
                        ComponentName = Name
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

        public void ResetChildAlignment(int i)
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

        public bool IsNullAlignment(GroupNames align, string direction)
        {
            var group = this.GetGroupProperties(align == GroupNames.Alignment ? align : GroupNames.SelfAlignment);
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
                StrokeDashArray = new DoubleCollection([10.0, 3.0])
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
            ComponentView.PreviewMouseLeftButtonDown += OnMouseDown;
            ComponentView.MouseEnter += OnMouseEnter;
            ComponentView.SizeChanged += OnSizeChanged;

            Children = [];

            PropertyGroups =
            [
                new AlignmentGroup().GetContentAlignment(),
                new TransformGroup().GetTransformGroup(),
                new TextGroup().GetTextGroup(),
                new AppearanceGroup().GetAppearanceGroup(),
                new SelfAlignmentGroup().GetSelfAlignmentGroup(),
                new GridPropertyGroup().GetGridPropertyGroup(),
                new GlobalGroup().GetGlobalGroup(),
                new ShadowGroup().GetShadowGroup()
            ];
        }
    }
}