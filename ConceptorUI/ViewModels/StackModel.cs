using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    internal class StackModel : Component
    {
        public List<Component> Children { get; set; }
        Border borderP;
        Border border;
        Grid grid;

        public StackModel(bool isConstraints = false)
        {
            borderP = new Border();
            border = new Border();
            grid = new Grid();
            ComponentView = borderP;
            borderP.Child = border;
            border.Child = grid;
            ComponentView.PreviewMouseDown += OnMouseDown;
            Children = new List<Component>(); EnumName = ComponentList.Stack;
            OnConfigured();
            LoadIds();
            if (!isConstraints) OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            Selected = true;
            borderP.BorderBrush = Brushes.DodgerBlue;
            borderP.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Stack;
            PanelProperty.Instance.FeedProps();
            return true;
        }

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            borderP.BorderBrush = Brushes.Transparent;
            borderP.BorderThickness = new Thickness(0);
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

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            if (groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(borderP) || 
                e.OriginalSource.Equals(border) || e.OriginalSource.Equals(grid)))
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
            var propName = "";
            if (pos[0] != -1 && pos[1] != -1 && (Selected || allow))
            {
                int idG = pos[0], idP = pos[1];
                var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
                var idGAl = posAl[0];

                try { propName = groupProps![idG].Properties[idP].Name; }
                catch (Exception e) { }

                if (Selected || allow)
                {
                    /* Alignement */
                    #region
                    if (propName == PropertyNames.HL.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        border.HorizontalAlignment = value == "0" ? border.HorizontalAlignment : HorizontalAlignment.Left;
                    }
                    else if (propName == PropertyNames.HC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        border.HorizontalAlignment = value == "0" ? border.HorizontalAlignment : HorizontalAlignment.Center;
                    }
                    else if (propName == PropertyNames.HR.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        border.HorizontalAlignment = value == "0" ? border.HorizontalAlignment : HorizontalAlignment.Right;
                    }
                    else if (propName == PropertyNames.VT.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        border.VerticalAlignment = value == "0" ? border.VerticalAlignment : VerticalAlignment.Top;
                    }
                    else if (propName == PropertyNames.VC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        border.VerticalAlignment = value == "0" ? border.VerticalAlignment : VerticalAlignment.Center;
                    }
                    else if (propName == PropertyNames.VB.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        border.VerticalAlignment = value == "0" ? border.VerticalAlignment : VerticalAlignment.Bottom;
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (propName == PropertyNames.Width.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps[idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[0].Value = "0";
                            groupProps[idGAl].Properties[1].Value = "0";
                            groupProps[idGAl].Properties[2].Value = "0";
                            PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Width = double.NaN;
                            groupProps[idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[0].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                borderP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[4].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps[idG].Properties[idP].Value = vd + "";
                            borderP.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                borderP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propName == PropertyNames.Height.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Stretch;
                            groupProps[idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[3].Value = "0";
                            groupProps[idGAl].Properties[4].Value = "0";
                            groupProps[idGAl].Properties[5].Value = "0";
                            PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Height = 10;
                            groupProps[idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[3].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                borderP.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps[idG].Properties[idP].Value = vd + "";
                            borderP.Height = vd;
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString() || propName == PropertyNames.VE.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
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
                    /* Text */
                    #region
                    #endregion
                    /* Appearance */
                    #region
                    else if (propName == PropertyNames.FillColor.ToString())
                    {
                        border.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                     new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Opacity.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        border.Opacity = vd;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.CMargin.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.MarginBtnActif.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Margin.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        borderP.Margin = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.MarginLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(vd, borderP.Margin.Top, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginTop.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, vd, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, vd, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginBottom.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, borderP.Margin.Right, vd);
                    }
                    else if (propName == PropertyNames.CPadding.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.PaddingBtnActif.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Padding.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        border.Padding = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.PaddingLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(vd, border.Padding.Top, border.Padding.Right, border.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingTop.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(border.Padding.Left, vd, border.Padding.Right, border.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, vd, border.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingBottom.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, border.Padding.Right, vd);
                    }
                    else if (propName == PropertyNames.CBorder.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderBtnActif.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 6].Value =
                        groupProps[idG].Properties[idP + 9].Value = groupProps[idG].Properties[idP + 12].Value = vd + "";
                        border.BorderThickness = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.BorderColor.ToString())
                    {
                        border.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                          new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderLeftWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(vd, border.BorderThickness.Top, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderTopWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, vd * 0.65, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderRightWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, vd, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderBottomWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, border.BorderThickness.Right, vd * 0.65);
                    }
                    else if (propName == PropertyNames.CBorderRadius.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderRadBtnActif.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderRadius.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        border.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(vd, border.CornerRadius.TopRight,
                                                      border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                      border.CornerRadius.BottomRight, vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, vd,
                                                      border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                      vd, border.CornerRadius.BottomLeft);
                    }
                    #endregion
                }
            }
            else
            {
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
                    var i = -1;
                    foreach (var child in Children.Where(child => child.Selected))
                    {
                        i = Children.IndexOf(child);
                        break;
                    }

                    if (i != -1)
                    {
                        grid.Children.RemoveAt(i);
                        Children.RemoveAt(i);
                        OnSelected();
                    }
                }

                foreach (var child in Children)
                    child.OnUpdated(groupName, propertyName, value);
            }
        }

        public override void OnInitialized()
        {
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
                            #endregion
                            /* Transform */
                            #region
                            if (prop.Name == PropertyNames.Width.ToString())
                            {
                                if (prop.Value == SizeValue.Expand.ToString())
                                {
                                    borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Left;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    double vd = 0; double.TryParse(prop.Value, out vd);
                                    borderP.Width = vd;
                                }
                            }
                            else if (prop.Name == PropertyNames.Height.ToString())
                            {
                                if (prop.Value == SizeValue.Expand.ToString())
                                {
                                    borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Top;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    double vd = 0; double.TryParse(prop.Value, out vd);
                                    borderP.Height = vd;
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
                                border.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                               new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                            }
                            else if (prop.Name == PropertyNames.Opacity.ToString())
                            {
                                var vd = Helper.ConvertToDouble(prop.Value);
                                border.Opacity = vd;
                            }
                            else if (prop.Name == PropertyNames.Margin.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                borderP.Margin = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.MarginLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                borderP.Margin = new Thickness(vd, borderP.Margin.Top, borderP.Margin.Right, borderP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                borderP.Margin = new Thickness(borderP.Margin.Left, vd, borderP.Margin.Right, borderP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, vd, borderP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, borderP.Margin.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.Padding.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.Padding = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.Padding = new Thickness(vd, border.Padding.Top, border.Padding.Right, border.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.Padding = new Thickness(border.Padding.Left, vd, border.Padding.Right, border.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, vd, border.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, border.Padding.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderWidth.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.BorderThickness = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderColor.ToString())
                            {
                                border.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                                      new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                            }
                            else if (prop.Name == PropertyNames.BorderRadius.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.CornerRadius = new CornerRadius(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.CornerRadius = new CornerRadius(vd, border.CornerRadius.TopRight,
                                                              border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                              border.CornerRadius.BottomRight, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, vd,
                                                              border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                              vd, border.CornerRadius.BottomLeft);
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
            if (compSerialiser.Children != null)
            {
                foreach (var child in compSerialiser.Children)
                {
                    Component component = ManageEnums.Instance.GetComponent(child.Name!);
                    component.Parent = this; component.Added = true;
                    component.OnDeserialiser(child);
                    grid.Children.Add(component.ComponentView!);
                    Children.Add(component);
                }
            }
            
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
                var first = false;
                if (Properties.ComponentOutsUsing![id].Name == ComponentList.Stack.ToString() && !Added)
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
            }
            else
            {
                foreach (var child in Children)
                {
                    child.OnConfiguOut(id);
                }
            }
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

                grid.Children.Add(component.ComponentView!);
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

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            Children[id].Parent = this;
            //Restrictions actuelles de columns
            //component.OnAddConfig(GroupNames.Alignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Children[id].OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Visible.ToString());
            Children[id].OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Visible.ToString(), false);
            Children[id].OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HC, VisibilityValue.Collapsed.ToString(), false);
            Children[id].OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HR, VisibilityValue.Visible.ToString(), false);
            Children[id].OnAddConfig(GroupNames.SelfAlignment, PropertyNames.VT, VisibilityValue.Visible.ToString(), false);
            Children[id].OnAddConfig(GroupNames.SelfAlignment, PropertyNames.VC, VisibilityValue.Collapsed.ToString(), false);
            Children[id].OnAddConfig(GroupNames.SelfAlignment, PropertyNames.VB, VisibilityValue.Visible.ToString(), false);
            Children[id].OnAddConfig(GroupNames.Transform, PropertyNames.MoveTop, VisibilityValue.Visible.ToString(), false);
            Children[id].OnAddConfig(GroupNames.Transform, PropertyNames.MoveBottom, VisibilityValue.Visible.ToString(), false);
            Children[id].OnInitialized();

            string w = Children[id].groupProps![Children[id].Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Children[id].Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
            string h = Children[id].groupProps![Children[id].Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Children[id].Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;

            if (w != SizeValue.Expand.ToString())
            {
                Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, "1", true);
            }
            if (h != SizeValue.Expand.ToString())
            {
                Children[id].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, "1", true);
            }
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

        public override ReactComponent BuildReactComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            var imports = new Dictionary<string, Dictionary<string, bool>> {
                {"react-native", new Dictionary<string, bool>{ { "StyleSheet", false }, { "View", false} }}
            };
            comp.Imports = imports;
            List<ReactComponent> childComps = new List<ReactComponent>();
            string childrenBody = "";
            tab = tab != "" ? tabs(n + 1) + tab + "\n" : "";
            for (int i = 0; i < Children.Count; i++)
            {
                childComps.Add(Children[i].BuildReactComponent("", n + 1, id + i));
                childrenBody += childComps[i].Body + (i != Children.Count - 1 ? "\n" : "");
            }
            string styleName = $"stack{id}";
            comp.Body = $"{tabs(n)}<View \n{tabs(n + 1)}style={"{styles."}{styleName}{"}"}>\n{childrenBody}\n{tab}{tabs(n)}</View>";
            #region
            const string invalide = "invalide";
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
            string flex;
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
            else
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                flex = wd == SizeValue.Expand.ToString() || hd == SizeValue.Expand.ToString() ? "1" : invalide;
            }
            /* Appearance */
            string backgroundColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
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
                        { "position", "'relative'" },
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
            for (int i = 0; i < childComps.Count; i++)
            {
                foreach (var key in childComps[i]!.Styles!.Keys)
                    comp.Styles.Add(key, childComps[i].Styles![key]);
                comp.AddImports(childComps[i].Imports!);
            }
            #endregion
            return comp;
        }

        public override WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams)
        {
            WebComponent comp = new WebComponent();
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
            double w = 0;
            string paddingLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingLeft.ToString()]].Value;
            string paddingRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingRight.ToString()]].Value;
            string borderLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderLeftWidth.ToString()]].Value;
            string borderRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderRightWidth.ToString()]].Value;
            if (ComponentView!.Width.Equals(double.NaN) && component != null) w = Parent!.Width(this);
            else if (!ComponentView!.Width.Equals(double.NaN)) w = ComponentView!.Width;
            if (component != null && component != this)
            {
                string marginLeft = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
                string marginRight = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
                //w -= Convert.ToDouble(marginLeft) + Convert.ToDouble(marginRight);
            }
            return w - (Convert.ToDouble(borderLeft) + Convert.ToDouble(borderRight)) -
                       (Convert.ToDouble(paddingLeft) + Convert.ToDouble(paddingRight));
        }
        public override double Height(Component component)
        {
            double h = 0;
            string paddingTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingTop.ToString()]].Value;
            string paddingBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingBottom.ToString()]].Value;
            string borderTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderTopWidth.ToString()]].Value;
            string borderBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.BorderBottomWidth.ToString()]].Value;
            if (ComponentView!.Height.Equals(double.NaN) && component != null) h = Parent!.Height(this);
            else if (!ComponentView!.Height.Equals(double.NaN)) h = ComponentView!.Height;
            if (component != null && component != this)
            {
                string marginTop = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
                string marginBottom = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
                //h -= Convert.ToDouble(marginTop) + Convert.ToDouble(marginBottom);
            }
            return h - (Convert.ToDouble(borderTop) + Convert.ToDouble(borderBottom)) -
                       (Convert.ToDouble(paddingTop) + Convert.ToDouble(paddingBottom));
        }

        public override void OnConfigured()
        {
            groupProps = new List<GroupProperties>
            {
                new ()
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
                            Value = "1",
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
                            Value = "1",
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
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.SpaceAround.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
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
                            Value = "100",
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
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveBottom.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Focus.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Visible.ToString()
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
        }
    }
}
