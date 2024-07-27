using System;
using ConceptorUI.Views.ComponentP;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using ConceptorUI.Models;
using System.Globalization;
using System.Linq;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    class ListVModel : Component
    {
        public List<Component> Children { get; set; }
        Border contentP;
        Border content;
        StackPanel stack;
        ScrollViewer ScrollContent;

        public ListVModel(bool isConstraints = false)
        {
            contentP = new Border();
            content = new Border();
            stack = new StackPanel();
            stack.Orientation = Orientation.Vertical;
            Children = new List<Component>();

            ScrollContent = new ScrollViewer();
            ScrollContent.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            ScrollContent.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            ScrollContent.Content = stack;

            ComponentView = contentP;
            contentP.Child = content;
            content.Child = ScrollContent; EnumName = ComponentList.ListV;

            ComponentView.PreviewMouseDown += OnMouseDown;
            ComponentView.PreviewMouseWheel += OnMouseWheel;

            OnConfigured();
            LoadIds();
            if (!isConstraints)
                OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            contentP.BorderBrush = Brushes.DodgerBlue;
            contentP.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.ListV;
            PanelProperty.Instance.FeedProps();
            Selected = true;
            return true;
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

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            contentP.BorderBrush = Brushes.Transparent;
            contentP.BorderThickness = new Thickness(0);
            foreach (var child in Children)
            {
                child.OnUnselected(isInterne);
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            if (groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(contentP) ||
                e.OriginalSource.Equals(content) || e.OriginalSource.Equals(stack) || e.OriginalSource.Equals(ScrollContent)))
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
                PageView.Instance.OnSelected();
                PageView.Instance.RefreshStructuralView();
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollContent.ScrollToVerticalOffset(ScrollContent.VerticalOffset - e.Delta);
        }

        public override void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            var pos = GetPosition(groupName, propertyName);
            int idG = pos[0], idP = pos[1];
            var ht = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
            if (pos[0] != -1 && pos[1] != -1 && (Selected || allow))
            {
                var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
                var idGAl = posAl[0];

                if (Selected || allow)
                {
                    /* Alignement */
                    #region
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
                            double vd = 0; double.TryParse(value, out vd);
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
                            double vd = 0; double.TryParse(value, out vd);
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
                    /* Text */
                    #region
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
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        contentP.Margin = new Thickness(vd);
                    }
                    else if (propertyName == PropertyNames.MarginLeft)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(vd, contentP.Margin.Top, contentP.Margin.Right, contentP.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginTop)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, vd, contentP.Margin.Right, contentP.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginRight)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, vd, contentP.Margin.Bottom);
                    }
                    else if (propertyName == PropertyNames.MarginBottom)
                    {
                        double vd = 0; double.TryParse(value, out vd);
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
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        content.Padding = new Thickness(vd);
                    }
                    else if (propertyName == PropertyNames.PaddingLeft)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(vd, content.Padding.Top, content.Padding.Right, content.Padding.Bottom);
                    }
                    else if (propertyName == PropertyNames.PaddingTop)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, vd, content.Padding.Right, content.Padding.Bottom);
                    }
                    else if (propertyName == PropertyNames.PaddingRight)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, vd, content.Padding.Bottom);
                    }
                    else if (propertyName == PropertyNames.PaddingBottom)
                    {
                        double vd = 0; double.TryParse(value, out vd);
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
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
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
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(vd, content.BorderThickness.Top, content.BorderThickness.Right, content.BorderThickness.Bottom);
                    }
                    else if (propertyName == PropertyNames.BorderTopWidth)
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, vd * 0.65, content.BorderThickness.Right, content.BorderThickness.Bottom);
                    }
                    else if (propertyName == PropertyNames.BorderRightWidth)
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, vd, content.BorderThickness.Bottom);
                    }
                    else if (propertyName == PropertyNames.BorderBottomWidth)
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
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
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        content.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusTopLeft)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(vd, content.CornerRadius.TopRight,
                                                      content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusBottomLeft)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, content.CornerRadius.TopRight,
                                                      content.CornerRadius.BottomRight, vd);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusTopRight)
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, vd,
                                                      content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                    }
                    else if (propertyName == PropertyNames.BorderRadiusBottomRight)
                    {
                        double vd = 0; double.TryParse(value, out vd);
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
                        stack.Children.RemoveAt(i);
                        Children.RemoveAt(i);
                        OnSelected();
                    }
                }
                else if (propertyName == PropertyNames.MoveTop)
                {
                    var focus = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.Focus.ToString()]].Value == "1";
                    var k = -1;
                    foreach (var child in Children)
                        if (child.Selected) { k = Children.IndexOf(child); break; }

                    if (k != -1)
                    {
                        if (focus)
                        {
                            var child = Children[k];
                            Children.RemoveAt(k); Children.Insert(k - 1, child);
                            stack.Children.RemoveAt(k); stack.Children.Insert(k - 1, child.ComponentView!);
                        }
                        else
                        {
                            Children[k].OnUnselected();
                            Children[k - 1].OnSelected();
                        }
                    }
                }
                else if (propertyName == PropertyNames.MoveBottom)
                {
                    var focus = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.Focus.ToString()]].Value == "1";
                    var k = -1;
                    foreach (var child in Children)
                    {
                        if (child.Selected) { k = Children.IndexOf(child); break; }
                    }
                    if (k != -1)
                    {
                        if (focus)
                        {
                            var child = Children[k];
                            Children.RemoveAt(k); Children.Insert(k + 1, child);
                            stack.Children.RemoveAt(k); stack.Children.Insert(k + 1, child.ComponentView!);
                        }
                        else
                        {
                            Children[k].OnUnselected();
                            Children[k + 1].OnSelected();
                        }
                    }
                }

                foreach (var child in Children)
                    child.OnUpdated(groupName, propertyName, value);

                if (ht == SizeValue.Auto.ToString() || ht == SizeValue.Expand.ToString()) return;
                var height = Children.Sum(child => child.Height(null!));
                if (Height(this) < height)
                    OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
                #endregion
            }
        }

        public override void OnInitialized()
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
                            /* Alignement */
                            #region
                            if (prop.Name == PropertyNames.HL.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
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
                            /* Transform */
                            #region
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
                                    double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
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
                                    double vd = 0; double.TryParse(prop.Value, out vd);
                                    contentP.Height = vd;
                                    if (groupProps[idGAl].Properties[3].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Top;
                                    else if (groupProps[idGAl].Properties[4].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[5].Value == "1") contentP.VerticalAlignment = VerticalAlignment.Bottom;
                                    else contentP.VerticalAlignment = VerticalAlignment.Top;
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
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                contentP.Margin = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.MarginLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                contentP.Margin = new Thickness(vd, contentP.Margin.Top, contentP.Margin.Right, contentP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                contentP.Margin = new Thickness(contentP.Margin.Left, vd, contentP.Margin.Right, contentP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, vd, contentP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                contentP.Margin = new Thickness(contentP.Margin.Left, contentP.Margin.Top, contentP.Margin.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.Padding.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.Padding = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.Padding = new Thickness(vd, content.Padding.Top, content.Padding.Right, content.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.Padding = new Thickness(content.Padding.Left, vd, content.Padding.Right, content.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, vd, content.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.Padding = new Thickness(content.Padding.Left, content.Padding.Top, content.Padding.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderWidth.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                content.BorderThickness = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderColor.ToString())
                            {
                                content.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                        new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                            }
                            else if (prop.Name == PropertyNames.BorderLeftWidth.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                content.BorderThickness = new Thickness(vd, content.BorderThickness.Top, content.BorderThickness.Right, content.BorderThickness.Bottom);
                            }
                            else if (prop.Name == PropertyNames.BorderTopWidth.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                content.BorderThickness = new Thickness(content.BorderThickness.Left, vd * 0.65, content.BorderThickness.Right, content.BorderThickness.Bottom);
                            }
                            else if (prop.Name == PropertyNames.BorderRightWidth.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, vd, content.BorderThickness.Bottom);
                            }
                            else if (prop.Name == PropertyNames.BorderBottomWidth.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                content.BorderThickness = new Thickness(content.BorderThickness.Left, content.BorderThickness.Top, content.BorderThickness.Right, vd * 0.65);
                            }
                            else if (prop.Name == PropertyNames.BorderRadius.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.CornerRadius = new CornerRadius(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.CornerRadius = new CornerRadius(vd, content.CornerRadius.TopRight,
                                                              content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, content.CornerRadius.TopRight,
                                                              content.CornerRadius.BottomRight, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                content.CornerRadius = new CornerRadius(content.CornerRadius.TopLeft, vd,
                                                              content.CornerRadius.BottomRight, content.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
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
                foreach (var child in compSerialiser.Children)
                {
                    Component component = ManageEnums.Instance.GetComponent(child.Name!);
                    component.Parent = this; component.Added = true;
                    component.OnDeserialiser(child);

                    stack.Children.Add(component.ComponentView);
                    Children.Add(component);
                }
            }
            #endregion
            OnInitialized();
        }

        public override void OnAddConfig(GroupNames groupName, PropertyNames propertyName, string value, bool isGroup = true)
        {
            var pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1)
            {
                int idG = pos[0], idP = pos[1];
                if (isGroup)
                {
                    groupProps![idG].Visibility = value;
                    if (value == VisibilityValue.Collapsed.ToString())
                        for (var i = 0; i < groupProps[idG].Properties.Count; i++)
                            groupProps[idG].Properties[i].Visibility = value;
                }
                else groupProps![idG].Properties[idP].Visibility = value;
            }
        }

        public override void OnConfiguOut(int id)
        {
            if (Selected)
            {
                var first = false;
                if (Properties.ComponentOutsUsing![id].Name == ComponentList.ListV.ToString() && !Added)
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

                        stack.Children.Add(component.ComponentView!);
                        Children.Add(component);

                        LayoutConstraints(Children.Count - 1);
                        if (Selected) break;
                    }
                }
                #endregion
            }
            else
            {
                foreach (var child in Children)
                {
                    child.OnConfiguOut(id);
                }
            }

            var ht = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
            if (ht != SizeValue.Auto.ToString() && ht != SizeValue.Expand.ToString())
            {
                double height = 0;
                foreach (var child in Children)
                    height += child.Height(null!);
                if (Height(this) < height)
                    OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
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

            if (Selected && isPaste && value != null)
            {
                var name = valueD.Name!;
                var component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this; component.Added = true;
                component.OnDeserialiser(valueD);

                stack.Children.Add(component.ComponentView!);
                Children.Add(component);

                LayoutConstraints(Children.Count - 1);
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

        public override void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
            }
            else if (structuralElement.Children != null! && structuralElement.Children.Count == Children.Count)
            {
                for (int i = 0; i < structuralElement.Children!.Count; i++)
                {
                    Children[i].SelectFromStructuralView(structuralElement.Children[i]);
                }
            }
        }

        protected override void SelfConstraints()
        {
            /* Global */
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            /* Text */
            SetGroupVisibility(GroupNames.Text);
            /* Appearance */
            /* Shadow */
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
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
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.HVE, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.VE, false);
            
            /* Appearance */
            Children[id].SetGroupVisibility(GroupNames.Appearance);
            /* Shadow */
            Children[id].SetGroupVisibility(GroupNames.Shadow);
            
            Children[id].OnInitialize();
            Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
            
            var w = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if(w != SizeValue.Expand.ToString() && Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Vertical"))
                Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
        }

        public override bool AllowFixSize(bool isHeight = true)
        {
            return true;
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
            var imports = new Dictionary<string, Dictionary<string, bool>> {
                {"react-native", new Dictionary<string, bool>{ { "StyleSheet", false }, { "View", false} }}
            };
            comp.Imports = imports;
            var childComps = new List<ReactComponent>();
            var childrenBody = "";
            tab = tab != "" ? tabs(n + 1) + tab + "\n" : "";
            for (int i = 0; i < Children.Count; i++)
            {
                childComps.Add(Children[i].BuildReactComponent("", n + 1, id + i));
                childrenBody += childComps[i].Body + (i != Children.Count - 1 ? "\n" : "");
            }
            var styleName = $"row{id}";
            comp.Body = $"{tabs(n)}<View \n{tabs(n + 1)}style={"{styles."}{styleName}{"}"}>\n{childrenBody}\n{tab}{tabs(n)}</View>";
            #region
            const string invalide = "invalide";
            /* Self alignement */
            string selfAlign = invalide;
            string left = invalide;
            string top = invalide;
            string right = invalide;
            string bottom = invalide;
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
            else if (Parent != null && Parent!.EnumName == ComponentList.Stack)
            {
                string hls = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                string hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                left = hls == "1" ? "0" : invalide;
                right = hrs == "1" ? "0" : invalide;

                string vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                string vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                top = vts == "1" ? "0" : invalide;
                bottom = vbs == "1" ? "0" : invalide;
            }
            /* Appearance */
            string backgroundColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            string cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            string margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            string marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            string marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            string marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            string marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;

            /* Transform */
            string wd = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
            string hd = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
            string width;
            string height;
            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Stack)
            {
                double pht = Parent.Height(Parent);
                double pwt = Parent.Width(Parent);
                double mld = (Convert.ToDouble(marginLeft) / pht) * 100;
                double mrd = (Convert.ToDouble(marginRight) / pht) * 100;
                double mtd = (Convert.ToDouble(marginTop) / pht) * 100;
                double mbd = (Convert.ToDouble(marginBottom) / pht) * 100;

                double htd = (Height(this) / pht) * 100 - (mtd + mbd);
                double wtd = (Width(this) / pwt) * 100 - (mld + mrd);

                left = marginLeft != "0" ? $"'{mld}%'" : (left != invalide ? left : invalide);
                right = marginRight != "0" ? $"'{mrd}%'" : (right != invalide ? right : invalide);
                top = marginTop != "0" ? $"'{mtd}%'" : (top != invalide ? top : invalide);
                bottom = marginBottom != "0" ? $"'{mbd}%'" : (bottom != invalide ? bottom : invalide);

                marginLeft = marginRight = marginTop = marginBottom = invalide;

                height = hd == SizeValue.Expand.ToString() ? "'" + htd + "%'" : (hd == SizeValue.Auto.ToString() ? invalide : hd);
                width = wd == SizeValue.Expand.ToString() ? "'" + wtd + "%'" : (wd == SizeValue.Auto.ToString() ? invalide : wd);
            }
            else
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
            }
            /* Appearance */
            string cpadding = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CPadding.ToString()]].Value;
            string padding = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Padding.ToString()]].Value;
            string paddingLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingLeft.ToString()]].Value;
            string paddingTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingTop.ToString()]].Value;
            string paddingRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingRight.ToString()]].Value;
            string paddingBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingBottom.ToString()]].Value;

            string cborder = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CBorder.ToString()]].Value;
            string borderWidth = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderWidth.ToString()]].Value;
            string borderColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderColor.ToString()]].Value);
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
            #region
            comp.Styles = new Dictionary<string, Dictionary<string, string>> {
                {
                    styleName,
                    new Dictionary<string, string>
                    {
                        /* Content alignement */
                        { "flexDirection", "'column'" },
                        { "position", Parent!.EnumName == ComponentList.Stack ? "'absolute'" : invalide },
                        { "left", left.Replace(",", ".")},
                        { "top", top.Replace(",", ".")},
                        { "right", right.Replace(",", ".")},
                        { "bottom", bottom.Replace(",", ".")},
                        /* Self alignement */
                        { "alignSelf", selfAlign != invalide ? "'"+selfAlign+"'" : selfAlign },
                        /* Transform */
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
            #endregion
            for (var i = 0; i < childComps.Count; i++)
            {
                foreach (var key in childComps[i]!.Styles!.Keys)
                    comp.Styles.Add(key, childComps[i].Styles![key]);
                comp.AddImports(childComps[i].Imports!);
            }
            return comp;
        }

        public override WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams)
        {
            var comp = new WebComponent();
            return comp;
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

        private string tabs(int n)
        {
            var tab = "";
            for (int i = 0; i < n; i++)
                tab += "    ";
            return tab;
        }

        private static string tcolor(string color)
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
            double w = 0;
            var paddingLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingLeft.ToString()]].Value;
            var paddingRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingRight.ToString()]].Value;
            var borderLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderLeftWidth.ToString()]].Value;
            var borderRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRightWidth.ToString()]].Value;
            if (ComponentView!.Width.Equals(double.NaN) && component != null!) w = Parent!.Width(this);
            else if (!ComponentView!.Width.Equals(double.NaN)) w = ComponentView!.Width;
            if (component != null && component != this)
            {
                var marginLeft = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
                var marginRight = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
                w -= Convert.ToDouble(marginLeft) + Convert.ToDouble(marginRight);
            }
            return w - (Convert.ToDouble(borderLeft) + Convert.ToDouble(borderRight)) -
                       (Convert.ToDouble(paddingLeft) + Convert.ToDouble(paddingRight));
        }

        public override double Height(Component component)
        {
            double h = 0, hp = 0;
            var paddingTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingTop.ToString()]].Value;
            var paddingBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingBottom.ToString()]].Value;
            var borderTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderTopWidth.ToString()]].Value;
            var borderBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderBottomWidth.ToString()]].Value;

            hp -= (Convert.ToDouble(borderTop) + Convert.ToDouble(borderBottom)) +
                  (Convert.ToDouble(paddingTop) + Convert.ToDouble(paddingBottom));

            if (component.Equals(this))
            {
                if (ComponentView!.Height.Equals(double.NaN))
                {
                    foreach (var child in Children)
                        hp += child.Height(null!);
                }
                else hp = ComponentView!.Height;
                h = hp;
            }
            return h;
        }

        public override void OnConfigured()
        {
            #region
            groupProps = new List<GroupProperties>
            {
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
                            Value = ColorValue.Transparent.ToString(),
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
 