using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.Windows.Media.Effects;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    internal class ContainerModel : Component
    {
        public Component Child { get; set; }
        Border borderP;
        Border border;

        public ContainerModel(bool isConstraints = false)
        {
            borderP = new Border();
            border = new Border();
            ComponentView = borderP;
            borderP.Child = border;
            ComponentView.PreviewMouseDown += OnMouseDown;
            Child = null!; EnumName = ComponentList.Container;
            OnConfigured();
            LoadIds();
            if(!isConstraints) OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            borderP.BorderBrush = Brushes.DodgerBlue;
            borderP.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Container;
            PanelProperty.Instance.FeedProps();
            Selected = true;

            ComponentView!.Focusable = true;
            Keyboard.Focus(ComponentView);
            //Console.WriteLine("Key Focus: "+ ComponentView!.IsKeyboardFocused);
            //if(PanelEntity.Instance != null) PanelEntity.Instance.OnSelected();

            return true;
        }

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            borderP.BorderBrush = Brushes.Transparent;
            borderP.BorderThickness = new Thickness(0);
            if (Child != null!) Child.OnUnselected(isInterne);
        }

        public override bool OnChildSelected()
        {
            return Selected || (Child != null! && Child.OnChildSelected());
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            int idGC = -1, idPC = -1;
            if(Child != null!) {
                var posC = Child.GetPosition(GroupNames.Global, PropertyNames.CanSelect);
                idGC = posC[0]; idPC = posC[1];
            }

            if ((groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(borderP) || e.OriginalSource.Equals(border))) ||
                (Child != null && Child.groupProps![idGC].Properties[idPC].Value == CanSelectValues.Parent.ToString()))
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
                catch (Exception e)
                {
                    // ignored
                }

                if (Selected || allow)
                {
                    #region Alignement
                    if (propName == PropertyNames.HL.ToString() && groupName == GroupNames.Alignment)
                    {
                        groupProps![idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        if (Child != null! && value == "1")
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Auto.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, value, true);
                        }
                        else if (Child != null)
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Expand.ToString() ? SizeValue.Expand.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, value, true);
                            //groupProps[idG].Properties[idP].Value = vd == SizeValue.Auto.ToString() ? "0" : "1";
                        }
                    }
                    else if (propName == PropertyNames.HC.ToString() && groupName == GroupNames.Alignment)
                    {
                        groupProps![idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        if (Child != null! && value == "1")
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, value, true);
                        }
                        else if (Child != null)
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Auto.ToString() ? SizeValue.Expand.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, vd == SizeValue.Auto.ToString() ? "0" : "1", true);
                            //groupProps[idG].Properties[idP].Value = vd == SizeValue.Auto.ToString() ? "0" : "1";
                        }
                    }
                    else if (propName == PropertyNames.HR.ToString() && groupName == GroupNames.Alignment)
                    {
                        groupProps![idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        if (Child != null! && value == "1")
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, value, true);
                        }
                        else if (Child != null)
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Auto.ToString() ? SizeValue.Expand.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, vd == SizeValue.Auto.ToString() ? "0" : "1", true);
                            //groupProps[idG].Properties[idP].Value = vd == SizeValue.Auto.ToString() ? "0" : "1";
                        }
                    }
                    else if (propName == PropertyNames.VT.ToString() && groupName == GroupNames.Alignment)
                    {
                        groupProps![idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        if (Child != null! && value == "1")
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, value, true);
                        }
                        else if (Child != null)
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Auto.ToString() ? SizeValue.Expand.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, vd == SizeValue.Auto.ToString() ? "0" : "1", true);
                            //groupProps[idG].Properties[idP].Value = vd == SizeValue.Auto.ToString() ? "0" : "1";
                        }
                    }
                    else if (propName == PropertyNames.VC.ToString() && groupName == GroupNames.Alignment)
                    {
                        groupProps![idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        if (Child != null! && value == "1")
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, value, true);
                        }
                        else if (Child != null)
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Auto.ToString() ? SizeValue.Expand.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, vd == SizeValue.Auto.ToString() ? "0" : "1", true);
                            //groupProps[idG].Properties[idP].Value = vd == SizeValue.Auto.ToString() ? "0" : "1";
                        }
                    }
                    else if (propName == PropertyNames.VB.ToString() && groupName == GroupNames.Alignment)
                    {
                        groupProps![idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        if (Child != null! && value == "1")
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                        }
                        else if (Child != null)
                        {
                            var vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Auto.ToString() ? SizeValue.Expand.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, vd == SizeValue.Auto.ToString() ? "0" : "1", true);
                            //groupProps[idG].Properties[idP].Value = vd == SizeValue.Auto.ToString() ? "0" : "1";
                        }
                    }
                    if (propName == PropertyNames.HL.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        borderP.HorizontalAlignment = value == "0" ? borderP.HorizontalAlignment : HorizontalAlignment.Left;
                    }
                    else if (propName == PropertyNames.HC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        borderP.HorizontalAlignment = value == "0" ? borderP.HorizontalAlignment : HorizontalAlignment.Center;
                    }
                    else if (propName == PropertyNames.HR.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        borderP.HorizontalAlignment = value == "0" ? borderP.HorizontalAlignment : HorizontalAlignment.Right;
                    }
                    else if (propName == PropertyNames.VT.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        borderP.VerticalAlignment = value == "0" ? borderP.VerticalAlignment : VerticalAlignment.Top;
                    }
                    else if (propName == PropertyNames.VC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        borderP.VerticalAlignment = value == "0" ? borderP.VerticalAlignment : VerticalAlignment.Center;
                    }
                    else if (propName == PropertyNames.VB.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        borderP.VerticalAlignment = value == "0" ? borderP.VerticalAlignment : VerticalAlignment.Bottom;
                    }
                    #endregion
                    
                    #region Transform 
                    else if (propName == PropertyNames.Width.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            borderP.Width = double.NaN;
                            borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[0].Value = "0";
                            groupProps[idGAl].Properties[1].Value = "0";
                            groupProps[idGAl].Properties[2].Value = "0";
                            //PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Width = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[0].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                borderP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                //PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            var vd = Helper.ConvertToDouble(value);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            borderP.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                borderP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                //PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propName == PropertyNames.Height.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            borderP.Height = double.NaN;
                            borderP.VerticalAlignment = VerticalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[3].Value = "0";
                            groupProps[idGAl].Properties[4].Value = "0";
                            groupProps[idGAl].Properties[5].Value = "0";
                            //PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Height = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[3].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                borderP.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                //PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            var vd = Helper.ConvertToDouble(value);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            borderP.Height = vd;
                            if (groupProps[idGAl].Properties[3].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                borderP.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                //PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString() || propName == PropertyNames.VE.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.MoveParentToChild.ToString())
                    {
                        if (Child != null)
                        {
                            OnUnselected();
                            Child.OnSelected();
                        }
                    }
                    #endregion
                    /* Appearance */
                    #region
                    else if (propName == PropertyNames.FillColor.ToString())
                    {
                        border.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                            new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Opacity.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        border.Opacity = vd;
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
                        borderP.Margin = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.MarginLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(vd, borderP.Margin.Top, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, vd, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, vd, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, borderP.Margin.Right, vd);
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
                        border.Padding = new Thickness(2 * vd, vd, 2 * vd, vd);
                    }
                    else if (propName == PropertyNames.PaddingLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(vd, border.Padding.Top, border.Padding.Right, border.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(border.Padding.Left, vd, border.Padding.Right, border.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, vd, border.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, border.Padding.Right, vd);
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
                        border.BorderThickness = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.BorderColor.ToString())
                    {
                        border.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                      new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderLeftWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(vd, border.BorderThickness.Top, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderTopWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, vd * 0.65, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderRightWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, vd, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderBottomWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, border.BorderThickness.Right, vd * 0.65);
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
                        border.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(vd, border.CornerRadius.TopRight,
                                                      border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                      border.CornerRadius.BottomRight, vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, vd,
                                                      border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(value);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                      vd, border.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Grid property */
                    #region
                    else if (propertyName == PropertyNames.SelectedElement)
                    {
                        groupProps![idG].Properties[idP].Value = value == ESelectedElement.Cell.ToString() ? value : (value == ESelectedElement.Row.ToString() ? value : ESelectedElement.Column.ToString());
                    }
                    else if (propertyName == PropertyNames.Row)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.Column)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.RowSpan)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.ColumnSpan)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.Merged)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    #endregion
                    /* Global Property */
                    #region
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps![idG].Properties![idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.HideBorder)
                    {
                        groupProps![idG].Properties![idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.SelectedMode)
                    {
                        groupProps![idG].Properties![idP].Value = value == ESelectedMode.Multiple.ToString() ? value : ESelectedMode.Single.ToString();
                    }
                    #endregion
                    /* Shadow Property */
                    #region
                    else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowDepth)
                    {
                        double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                        groupProps![idG].Properties![idP].Value = value;
                        if (border.Effect == null!) border.Effect = new DropShadowEffect();
                        (border.Effect as DropShadowEffect)!.ShadowDepth = vd;
                    }
                    else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.BlurRadius)
                    {
                        double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                        groupProps![idG].Properties![idP].Value = value;
                        if (border.Effect == null!) border.Effect = new DropShadowEffect();
                        (border.Effect as DropShadowEffect)!.BlurRadius = vd;
                    }
                    else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowDirection)
                    {
                        double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
                        groupProps![idG].Properties![idP].Value = value;
                        (border.Effect as DropShadowEffect)!.Direction = vd;
                    }
                    else if (groupName == GroupNames.Shadow && propertyName == PropertyNames.ShadowColor)
                    {
                        if (value == "0") value = ColorValue.Transparent.ToString();
                        groupProps![idG].Properties![idP].Value = value;
                        if (border.Effect == null!) border.Effect = new DropShadowEffect();
                        (border.Effect as DropShadowEffect)!.Color = value == ColorValue.Transparent.ToString()
                            ? Brushes.Transparent.Color : (new BrushConverter().ConvertFrom(value) as SolidColorBrush)!.Color;
                    }
                    #endregion
                }
            }
            else
            {
                if (Child == null!) return;
                switch (Child.Selected)
                {
                    case true when propertyName == PropertyNames.MoveChildToParent:
                        Child.OnUnselected();
                        OnSelected();
                        break;
                    case true when propertyName == PropertyNames.Trash:
                        _deleteChild();
                        break;
                }
                
                if (Child == null!) return;
                if (Child.Selected && propertyName == PropertyNames.Width && groupName == GroupNames.Transform)
                {
                    if (value == SizeValue.Expand.ToString())
                        OnUpdated(GroupNames.Alignment, PropertyNames.HL, "0", true);
                }
                else if (Child.Selected && propertyName == PropertyNames.Height && groupName == GroupNames.Transform)
                {
                    if (value == SizeValue.Expand.ToString())
                        OnUpdated(GroupNames.Alignment, PropertyNames.VT, "0", true);
                }
                Child.OnUpdated(groupName, propertyName, value);
            }
        }

        private void _deleteChild()
        {
            Child = null!;
            border.Child = null;

            groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value = "0";
            groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value = "0";
            groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value = "0";

            groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value = "0";
            groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value = "0";
            groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value = "0";

            OnSelected();
        }

        public sealed override void OnInitialized()
        {
            var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
            var idGAl = posAl[0];
            foreach (var group in groupProps!)
            {
                foreach (var prop in group.Properties)
                {
                    /* Alignement */
                    #region
                    if (prop.Name == PropertyNames.HL.ToString() && group.Name == GroupNames.Alignment.ToString())
                    {
                        if (Child != null && prop.Value == "1")
                        {
                            string vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, prop.Value, true);
                        }
                    }
                    else if (prop.Name == PropertyNames.HC.ToString() && group.Name == GroupNames.Alignment.ToString())
                    {
                        if (Child != null && prop.Value == "1")
                        {
                            string vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, prop.Value, true);
                        }
                    }
                    else if (prop.Name == PropertyNames.HR.ToString() && group.Name == GroupNames.Alignment.ToString())
                    {
                        if (Child != null && prop.Value == "1")
                        {
                            string vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Width, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, prop.Value, true);
                        }
                    }
                    else if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.Alignment.ToString())
                    {
                        if (Child != null && prop.Value == "1")
                        {
                            string vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, prop.Value, true);
                        }
                    }
                    else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.Alignment.ToString())
                    {
                        if (Child != null && prop.Value == "1")
                        {
                            string vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, prop.Value, true);
                        }
                    }
                    else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.Alignment.ToString())
                    {
                        if (Child != null && prop.Value == "1")
                        {
                            string vd = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
                            Child.OnUpdated(GroupNames.Transform, PropertyNames.Height, vd == SizeValue.Expand.ToString() ? SizeValue.Auto.ToString() : vd, true);
                            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, "1", true);
                        }
                    }
                    else if (prop.Name == PropertyNames.HL.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.HC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Center;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.HR.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Right;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            borderP.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") borderP.VerticalAlignment = VerticalAlignment.Center;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            borderP.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") borderP.VerticalAlignment = VerticalAlignment.Bottom;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            borderP.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (prop.Name == PropertyNames.Width.ToString())
                    {
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString())
                        {
                            borderP.Width = double.NaN;
                            if (groupProps[idGAl].Properties[0].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Right;
                            else borderP.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                            borderP.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Right;
                            else borderP.HorizontalAlignment = HorizontalAlignment.Left;
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
                            borderP.Height = double.NaN;
                            if (groupProps[idGAl].Properties[3].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Bottom;
                            else borderP.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, out vd);
                            borderP.Height = vd;
                            if (groupProps[idGAl].Properties[3].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Bottom;
                            else borderP.VerticalAlignment = VerticalAlignment.Top;
                        }
                    }
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
                        var vd = Helper.ConvertToDouble(prop.Value);
                        borderP.Margin = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.MarginLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        borderP.Margin = new Thickness(vd, borderP.Margin.Top, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        borderP.Margin = new Thickness(borderP.Margin.Left, vd, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, vd, borderP.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, borderP.Margin.Right, vd);
                    }
                    else if (prop.Name == PropertyNames.Padding.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.Padding = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.Padding = new Thickness(vd, border.Padding.Top, border.Padding.Right, border.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingTop.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.Padding = new Thickness(border.Padding.Left, vd, border.Padding.Right, border.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, vd, border.Padding.Bottom);
                    }
                    else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.Padding = new Thickness(border.Padding.Left, border.Padding.Top, border.Padding.Right, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.BorderThickness = new Thickness(2 * vd, vd, 2 * vd, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderColor.ToString())
                    {
                        border.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.BorderLeftWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.BorderThickness = new Thickness(vd, border.BorderThickness.Top, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderTopWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, vd * 0.65, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderRightWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, vd, border.BorderThickness.Bottom);
                    }
                    else if (prop.Name == PropertyNames.BorderBottomWidth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, border.BorderThickness.Right, vd * 0.65);
                    }
                    else if (prop.Name == PropertyNames.BorderRadius.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.CornerRadius = new CornerRadius(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.CornerRadius = new CornerRadius(vd, border.CornerRadius.TopRight,
                                                        border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                        border.CornerRadius.BottomRight, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, vd,
                                                        border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                        vd, border.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Shadow Property */
                    #region
                    else if (prop.Name == PropertyNames.ShadowDepth.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (border.Effect == null!) border.Effect = new DropShadowEffect();
                        (border.Effect as DropShadowEffect)!.ShadowDepth = vd;
                    }
                    else if (prop.Name == PropertyNames.BlurRadius.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (border.Effect == null!) border.Effect = new DropShadowEffect();
                        (border.Effect as DropShadowEffect)!.BlurRadius = vd;
                    }
                    else if (prop.Name == PropertyNames.ShadowDirection.ToString())
                    {
                        var vd = Helper.ConvertToDouble(prop.Value);
                        if (border.Effect == null!) border.Effect = new DropShadowEffect();
                        (border.Effect as DropShadowEffect)!.Direction = vd;
                    }
                    else if (prop.Name == PropertyNames.ShadowColor.ToString())
                    {
                        if (prop.Value == "0") prop.Value = ColorValue.Transparent.ToString();
                        if (border.Effect == null!) border.Effect = new DropShadowEffect();
                        (border.Effect as DropShadowEffect)!.Color = prop.Value == ColorValue.Transparent.ToString()
                            ? Brushes.Transparent.Color : (new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush)!.Color;
                    }
                    #endregion
                }
            }
        }

        public override CompSerialiser OnSerialiser()
        {
            return new CompSerialiser {
                Name = EnumName.ToString(),
                Properties = groupProps,
                Children = Child != null! ? new List<CompSerialiser> { Child.OnSerialiser() } : null
            };
        }
        
        public override void OnDeserialiser(CompSerialiser compSerialiser)
        {
            groupProps = compSerialiser.Properties;
            var name = compSerialiser.Children != null! ? compSerialiser.Children![0].Name! : string.Empty;
            var component = ManageEnums.Instance.GetComponent(name);
            if(compSerialiser.Children != null)
            {
                component.Parent = this;
                component.Added = true;
                component.OnDeserialiser(compSerialiser.Children[0]);
                Child = component;
                border.Child = Child.ComponentView;
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
                    if (value != VisibilityValue.Collapsed.ToString()) return;
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
            if (Child == null! && Selected)
            {
                var first = false;
                if (Properties.ComponentOutsUsing![id].Name == ComponentList.Container.ToString() && !Added)
                {
                    Added = first = true;
                    foreach (var group in Properties.ComponentOutsUsing[id].Component)
                    {
                        var i = Properties.ComponentOutsUsing[id].Component.IndexOf(group);
                        groupProps![i].Visibility = group.Visibility;
                        foreach (var prop in group.Properties!)
                        {
                            var j = group.Properties.IndexOf(prop);
                            groupProps[i].Properties![j].Visibility = prop.Visibility;
                            groupProps[i].Properties![j].Value = prop.Value;
                        }
                    }
                }
                
                #region
                if (Child != null) return;
                foreach (var child in Properties.ComponentOutsUsing)
                {
                    if (child.ParentId == id && ((Properties.ComponentOutsUsing.IndexOf(child) != 0 && Properties.ComponentOutsUsing.Count > 1) || (Added && !first)))
                    {
                        var component = ManageEnums.Instance.GetComponent(child.Name);
                        component.Selected = true;
                        component.OnConfiguOut(Properties.ComponentOutsUsing.IndexOf(child));
                        component.Selected = false;
                            
                        Child = component;
                        border.Child = Child.ComponentView;
                        LayoutConstraints(0);
                    }
                    break;
                }
                #endregion
            }
            else
            {
                if (Child != null) Child.OnConfiguOut(id);
            }
        }

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            #region Contraintes de mise en page des enfants
            Child.Parent = this;
            //Restrictions actuelles de button
            Child.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Child.OnInitialized();

            var hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            var hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            var hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;

            var vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            var vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            var vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
            
            var w = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
            var h = Child.groupProps![Child.Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Child.Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
            
            if(hl == "0" && hc == "0" && hr == "0" && w != SizeValue.Expand.ToString())
                OnUpdated(GroupNames.Alignment, PropertyNames.HL, "1", true);
            if (vt == "0" && vc == "0" && vb == "0" && h != SizeValue.Expand.ToString())
                OnUpdated(GroupNames.Alignment, PropertyNames.VT, "1", true);

            if (w == SizeValue.Expand.ToString())
            {
                OnUpdated(GroupNames.Alignment, PropertyNames.HL, "0", true);
                return;
            }
            if (h == SizeValue.Expand.ToString())
            {
                OnUpdated(GroupNames.Alignment, PropertyNames.VT, "0", true);
                return;
            }
            
            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, hl, true);
            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, hc, true);
            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, hr, true);

            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, vt, true);
            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, vc, true);
            Child.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, vb, true);
            #endregion
        }

        public override bool AllowFixSize(bool isHeight = true)
        {
            return true;
        }

        public override void UpdateComponent(List<ComponentRef> refs, GroupNames groupName, PropertyNames propertyName, string value, int i = 0, bool found = false)
        {
            if(Child == null!) return;
            
            if (found && refs[i].ControlName == Child.EnumName && refs[i].ControlPosition == 0)
            {
                Child.OnUpdated(groupName, propertyName, value, true);
            }
            else
            {
                /*
                    --> Une page peut être désigner comme un composant ou page
                        - Lorsqu'une page devient un composant:
                            . On peut masquer certaines propriétés ou groupes (Pour interdire les modifications)
                            . Les ca enregistrent uniquement les valeurs des propriétés modifiables
                    --> Seule les images des composants sont créées: lors de la désignation et l'enregsitrement
                        - Il suffira de sélectionner le composant pour ajouter
                    --> Procedure:
                        - Utiliser les references pour éffectuer les modifications des co dans les ca;
                        - Pour cela, il nous faut une mémoire intermédiaire:
                            . On créer un dossier pour enregistrer les fichiers de la memoire;
                            . On a un fichier de configuration pour les references;
                            . On crée un fichier pour toute reference de type Composant;
                            . Les réferences sont créées dans cette memoire;
                            . Structure des réferences:
                                _ address;
                                _ type de reference: composant, string, number, color
                                _ Créer une variable pour chaque type
                        - Les composants appélants ne feront que pointer vers cette memoire:
                            . 
                    --> Structures
                        - Modele pour une reference
                        - Composant referencié:
                            . objet reference
                            . Dans child, on affecte le composant en le recupérant depuis la liste des composants.
                              => Cette liste est construite au fur et à mesure qu'on a besoin d'un composant
                */
                if (Child.GetType() == typeof(ComponentModel))
                    found = true;
                Child.UpdateComponent(refs, groupName, propertyName, value, i, found);
            }
        }

        public override void AddComponent(List<ComponentRef> refs, string data, int i = 0, bool found = false)
        {
            if (found && refs[i].ControlName == EnumName && refs.Count - 1 == i)
            {
                OnCopyOrPaste(data, true);
            }
            else if(Child == null!)
            {
                if (Child!.GetType() == typeof(ComponentModel))
                    found = true;
                Child.AddComponent(refs, data, 0, found);
            }
        }

        public override void DeleteComponent(List<ComponentRef> refs, int i = 0, bool found = false)
        {
            if(Child == null!) return;

            if (found && refs[i].ControlName == Child.EnumName && refs[i].ControlPosition == 0)
            {
                _deleteChild();
            }
            else
            {
                if (Child.GetType() == typeof(ComponentModel))
                    found = true;
                Child.DeleteComponent(refs, i, found);
            }
        }

        public override List<ComponentRef> BuildComponentRefs(int i)
        {
            if (Selected)
                return new List<ComponentRef> { new() { ControlName = EnumName, ControlPosition = i } };
            
            if (Child == null!) return null!;
            
            var compRefs = Child.BuildComponentRefs(0);
            
            if(compRefs != null!)
                compRefs.Insert(0, new ComponentRef { ControlName = EnumName, ControlPosition = i });
            return compRefs!;
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
            if (Selected && isPaste && Child == null! && value != null!)
            {
                var valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerialiser>(value);
                var name = valueD!.Name!;
                var component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this; component.Added = true;
                component.OnDeserialiser(valueD);

                Child = component;
                border.Child = Child.ComponentView;
                LayoutConstraints(0);
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerialiser());
            else if (!Selected && Child != null)
                return Child.OnCopyOrPaste(value!, isPaste);
            return null!;
        }

        public override StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = EnumName.ToString()!;
            structuralElement.Selected = Selected;
            if (Child != null!)
                structuralElement.Children = new List<StructuralElement> { Child.AddToStructuralView() };

            return structuralElement;
        }

        public override void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
            }
            else if(structuralElement.Children != null! && Child != null!)
            {
                foreach (var child in structuralElement.Children)
                {
                    Child.SelectFromStructuralView(child);
                }
            }
        }
        
        public override ReactComponent BuildReactComponent(string tab, int n, string id)
        {
            var comp = new ReactComponent();
            var imports = new Dictionary<string, Dictionary<string, bool>> {
                {"react-native", new Dictionary<string, bool>{{ "StyleSheet", false}, {"View", false}}}
            };
            tab = tab != "" ? tabs(n + 1) + tab + "\n" : "";
            comp.Imports = imports;
            var childComp = Child != null! ? Child.BuildReactComponent("", n + 1, id+"0") : null;
            var styleName = $"container{id}";
            comp.Body = $"{tabs(n)}<View \n{tabs(n+1)}style={"{styles."}{styleName}{"}"}>\n{(Child != null ? childComp!.Body : "")}\n{tab}{tabs(n)}</View>";
            double w = Width(this), h = Height(this);
            double pw = Parent!.Width(Parent), ph = Parent.Height(Parent);
            #region
            const string invalide = "invalide";
            /* Content alignement */
            string vt = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            string vc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            string vb = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
            string justifyContent = vt == "1" ? "flex-start" : (vc == "1" ? "center" : (vb == "1" ? "flex-end" : invalide));
            string hl = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            string hc = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            string hr = groupProps![Ids![GroupNames.Alignment.ToString()].IdGroup].Properties[Ids![GroupNames.Alignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
            string alignItems = hl == "1" ? "flex-start" : (hc == "1" ? "center" : (hr == "1" ? "flex-end" : invalide));
            /* Self alignement */
            string selfAlign = invalide;
            string left = invalide;
            string top = invalide;
            string right = invalide;
            string bottom = invalide;
            if (Parent != null && Parent!.EnumName == ComponentList.Row && Parent!.EnumName == ComponentList.ListV)
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
            /* Appareance */
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
            string flex;
            if (Parent != null && (Parent!.EnumName == ComponentList.Row || Parent!.EnumName == ComponentList.Window))
            {
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                flex = hd == SizeValue.Expand.ToString() ? "1" : invalide;
            }
            else if(Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                flex = wd == SizeValue.Expand.ToString() ? "1" : invalide;
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

                left = marginLeft != "0" ? $"'{mld}%'" : (left != invalide ? left: invalide);
                right = marginRight != "0" ? $"'{mrd}%'" : (right != invalide ? right : invalide);
                top = marginTop != "0" ? $"'{mtd}%'" : (top != invalide ? top : invalide);
                bottom = marginBottom != "0" ? $"'{mbd}%'" : (bottom != invalide ? bottom : invalide);

                marginLeft = marginRight = marginTop = marginBottom = invalide;

                height = hd == SizeValue.Expand.ToString() ? "'"+ htd +"%'" : (hd == SizeValue.Auto.ToString() ? invalide : hd);
                width = wd == SizeValue.Expand.ToString() ? "'"+ wtd +"%'" : (wd == SizeValue.Auto.ToString() ? invalide : wd);
                flex = invalide;
            }
            else
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                flex = invalide;
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
                        { "position", Parent!.EnumName == ComponentList.Stack ? "'absolute'" : invalide },
                        { "left", left.Replace(",", ".")},
                        { "top", top.Replace(",", ".")},
                        { "right", right.Replace(",", ".")},
                        { "bottom", bottom.Replace(",", ".")},
                        { "justifyContent", justifyContent != invalide ? "'"+justifyContent+"'" : invalide },
                        { "alignItems", alignItems != invalide ? "'"+alignItems+"'" : invalide },
                        /* Self alignement */
                        { "alignSelf", selfAlign != invalide ? "'"+selfAlign+"'" : invalide },
                        /* Transform */
                        { "flex", flex},
                        { "width", width.Replace(",", ".") },
                        { "height", height.Replace(",", ".") },
                        /* Appearance */
                        { "backgroundColor", "'"+backgroundColor+"'" },
                        { "margin", cmargin == "1" && margin != "0" ? margin.Replace(",", ".") : invalide },
                        { "marginLeft", cmargin == "0" && marginLeft != "0" ? marginLeft.Replace(",", ".") : invalide },
                        { "marginTop", cmargin == "0" && marginTop != "0" ? marginTop.Replace(",", ".") : invalide },
                        { "marginRight", cmargin == "0" && marginRight != "0" ? marginRight.Replace(",", ".") : invalide },
                        { "marginBottom", cmargin == "0" && marginBottom != "0" ? marginBottom.Replace(",", ".") : invalide },

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
                        { "borderTopStartRadius", cborderRadius == "0" && borderRadiusTopLeft != "0" ? borderRadiusTopLeft : invalide },
                        { "borderTopEndRadius", cborderRadius == "0" && borderRadiusTopRight != "0" ? borderRadiusTopRight : invalide },
                        { "borderBottomStartRadius", cborderRadius == "0" && borderRadiusBottomLeft != "0" ? borderRadiusBottomLeft : invalide },
                        { "borderBottomEndRadius", cborderRadius == "0" && borderRadiusBottomRight != "0" ? borderRadiusBottomRight : invalide },
                    }
                }
            };
            #endregion
            if (Child != null)
            {
                foreach (var key in childComp!.Styles!.Keys)
                    comp.Styles.Add(key, childComp.Styles[key]);
                comp.AddImports(childComp.Imports!);
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
            string tab = "";
            for (int i = 0; i < n; i++)
                tab += "    ";
            return tab;
        }

        private string tcolor(string color)
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

        public void Refresh()
        {
            if(Child != null)
            {
                border.Child = Child.ComponentView;
            }
        }
        
        //Rétirer le padding du composant et le margin de son enfants.
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
                //w -= Convert.ToDouble(marginLeft) + Convert.ToDouble(marginRight);
            }
            return w - (Convert.ToDouble(borderLeft) + Convert.ToDouble(borderRight)) -
                       (Convert.ToDouble(paddingLeft) + Convert.ToDouble(paddingRight));
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
            if (component != null && component != this)
            {
                var marginTop = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
                var marginBottom = component.groupProps![component.Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[component.Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
                //h -= Convert.ToDouble(marginTop) + Convert.ToDouble(marginBottom);
            }
            return h - (Convert.ToDouble(borderTop) + Convert.ToDouble(borderBottom)) -
                       (Convert.ToDouble(paddingTop) + Convert.ToDouble(paddingBottom));
        }
        
        public sealed override void OnConfigured()
        {
            #region
            groupProps = new List<GroupProperties>
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
