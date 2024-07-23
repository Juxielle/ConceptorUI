using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace ConceptorUI.ViewModels
{
    internal class ButtonModel : Component
    {
        Component text { get; set; }
        Border btnP;
        Border btn;

        public ButtonModel(bool isConstraints = false)
        {
            btnP = new Border();
            btn = new Border();
            ComponentView = btnP;
            btnP.Child = btn; EnumName = ComponentList.Button;
            ComponentView.PreviewMouseDown += new MouseButtonEventHandler(OnMouseDown);
            OnConfigured();
            LoadIds();
            if (!isConstraints) OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            Selected = true;
            btnP.BorderBrush = Brushes.DodgerBlue;
            btnP.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Button;
            PanelProperty.Instance.FeedProps();
            return true;
        }

        public override void OnUnselected(bool isInterne = false) {
            Selected = false;
            btnP.BorderBrush = Brushes.Transparent;
            btnP.BorderThickness = new Thickness(0);
            if (text != null) text.OnUnselected(isInterne);
        }

        public override bool OnChildSelected()
        {
            return Selected || (text != null! && text.OnChildSelected());
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int[] pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            if (groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(btnP) || e.OriginalSource.Equals(btn)))
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
                PageView.Instance.OnSelected();
                PageView.Instance.RefreshStructuralView();
            }
        }

        public override void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            int[] pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1)
            {
                int idG = pos[0], idP = pos[1];
                string propName = "";
                try { propName = groupProps![idG].Properties[idP].Name; }
                catch (Exception e) { }

                if (Selected || allow)
                {
                    /* Alignement */
                    #region
                    if (propName == PropertyNames.HL.ToString() && idG == 4)
                    {
                        groupProps[idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        btnP.HorizontalAlignment = HorizontalAlignment.Left;
                    }
                    else if (propName == PropertyNames.HC.ToString() && idG == 4)
                    {
                        groupProps[idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        btnP.HorizontalAlignment = HorizontalAlignment.Center;
                    }
                    else if (propName == PropertyNames.HR.ToString() && idG == 4)
                    {
                        groupProps[idG].Properties[0].Value = "0";
                        groupProps[idG].Properties[1].Value = "0";
                        groupProps[idG].Properties[2].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        btnP.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    else if (propName == PropertyNames.VT.ToString() && idG == 4)
                    {
                        groupProps[idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        btnP.VerticalAlignment = VerticalAlignment.Top;
                    }
                    else if (propName == PropertyNames.VC.ToString() && idG == 4)
                    {
                        groupProps[idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        btnP.VerticalAlignment = VerticalAlignment.Center;
                    }
                    else if (propName == PropertyNames.VB.ToString() && idG == 4)
                    {
                        groupProps[idG].Properties[3].Value = "0";
                        groupProps[idG].Properties[4].Value = "0";
                        groupProps[idG].Properties[5].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                        btnP.VerticalAlignment = VerticalAlignment.Bottom;
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (propName == PropertyNames.Width.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            btnP.Width = double.NaN; btnP.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps[idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[4].Properties[0].Value = "0";
                            groupProps[4].Properties[1].Value = "0";
                            groupProps[4].Properties[2].Value = "0";
                            PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            btnP.Width = double.NaN;
                            groupProps[idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[4].Properties[0].Value == "1") btnP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[4].Properties[1].Value == "1") btnP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[4].Properties[2].Value == "1") btnP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                btnP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[4].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps[idG].Properties[idP].Value = vd + "";
                            btnP.Width = vd;
                            if (groupProps[4].Properties[0].Value == "1") btnP.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[4].Properties[1].Value == "1") btnP.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[4].Properties[2].Value == "1") btnP.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                btnP.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[4].Properties[0].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propName == PropertyNames.Height.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            //btnP.Height = double.NaN; btnP.VerticalAlignment = VerticalAlignment.Top;
                            //groupProps[idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            //groupProps[4].Properties[3].Value = "0";
                            //groupProps[4].Properties[4].Value = "0";
                            //groupProps[4].Properties[5].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            btnP.Height = double.NaN;
                            groupProps[idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[4].Properties[3].Value == "1") btnP.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[4].Properties[4].Value == "1") btnP.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[4].Properties[5].Value == "1") btnP.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                btnP.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[4].Properties[3].Value = "1";
                                PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps[idG].Properties[idP].Value = vd + "";
                            btnP.Height = vd;
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString() || propName == PropertyNames.VE.ToString())
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.MoveParentToChild.ToString())
                    {
                        if (text != null)
                        {
                            OnUnselected();
                            text.OnSelected();
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
                        btn.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                         (SolidColorBrush)new BrushConverter().ConvertFrom(value);
                        groupProps[idG].Properties[idP].Value = value;
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
                        btnP.Margin = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.MarginLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btnP.Margin = new Thickness(vd, btnP.Margin.Top, btnP.Margin.Right, btnP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginTop.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btnP.Margin = new Thickness(btnP.Margin.Left, vd, btnP.Margin.Right, btnP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btnP.Margin = new Thickness(btnP.Margin.Left, btnP.Margin.Top, vd, btnP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginBottom.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btnP.Margin = new Thickness(btnP.Margin.Left, btnP.Margin.Top, btnP.Margin.Right, vd);
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
                        btn.Padding = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.PaddingLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.Padding = new Thickness(vd, btn.Padding.Top, btn.Padding.Right, btn.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingTop.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.Padding = new Thickness(btn.Padding.Left, vd, btn.Padding.Right, btn.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.Padding = new Thickness(btn.Padding.Left, btn.Padding.Top, vd, btn.Padding.Bottom);
                    }
                    else if (propName == PropertyNames.PaddingBottom.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.Padding = new Thickness(btn.Padding.Left, btn.Padding.Top, btn.Padding.Right, vd);
                    }
                    else if (propName == PropertyNames.BorderWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 6].Value =
                        groupProps[idG].Properties[idP + 9].Value = groupProps[idG].Properties[idP + 12].Value = vd + "";
                        btn.BorderThickness = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.BorderColor.ToString())
                    {
                        btn.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                          (SolidColorBrush)new BrushConverter().ConvertFrom(value);
                        groupProps[idG].Properties[idP].Value = value;
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
                        btn.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.CornerRadius = new CornerRadius(vd, btn.CornerRadius.TopRight,
                                                      btn.CornerRadius.BottomRight, btn.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.CornerRadius = new CornerRadius(btn.CornerRadius.TopLeft, btn.CornerRadius.TopRight,
                                                      btn.CornerRadius.BottomRight, vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.CornerRadius = new CornerRadius(btn.CornerRadius.TopLeft, vd,
                                                      btn.CornerRadius.BottomRight, btn.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        btn.CornerRadius = new CornerRadius(btn.CornerRadius.TopLeft, btn.CornerRadius.TopRight,
                                                      vd, btn.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Global */
                    #region
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    #endregion
                }
                else
                {
                    if (text != null)
                    {
                        text.OnUpdated(groupName, propertyName, value);
                        if (text.Selected && propName == PropertyNames.MoveChildToParent.ToString())
                        {
                            OnSelected();
                        }
                        else if (propName == PropertyNames.Trash.ToString())
                        {
                            text = null;
                            btn.Child = null;
                            OnSelected();
                        }
                    }
                }
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
                                    btnP.Width = double.NaN; btnP.HorizontalAlignment = HorizontalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    btnP.Width = double.NaN; btnP.HorizontalAlignment = HorizontalAlignment.Left;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    double vd = 0; double.TryParse(prop.Value, out vd);
                                    btnP.Width = vd;
                                }
                            }
                            else if (prop.Name == PropertyNames.Height.ToString())
                            {
                                if (prop.Value == SizeValue.Expand.ToString())
                                {
                                    btnP.Height = double.NaN; btnP.VerticalAlignment = VerticalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    btnP.Height = double.NaN; btnP.VerticalAlignment = VerticalAlignment.Top;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    double vd = 0; double.TryParse(prop.Value, out vd);
                                    btnP.Height = vd;
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
                                btn.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                               new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                            }
                            else if (prop.Name == PropertyNames.Margin.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btnP.Margin = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.MarginLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btnP.Margin = new Thickness(vd, btnP.Margin.Top, btnP.Margin.Right, btnP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btnP.Margin = new Thickness(btnP.Margin.Left, vd, btnP.Margin.Right, btnP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btnP.Margin = new Thickness(btnP.Margin.Left, btnP.Margin.Top, vd, btnP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btnP.Margin = new Thickness(btnP.Margin.Left, btnP.Margin.Top, btnP.Margin.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.Padding.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.Padding = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.Padding = new Thickness(vd, btn.Padding.Top, btn.Padding.Right, btn.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.Padding = new Thickness(btn.Padding.Left, vd, btn.Padding.Right, btn.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.Padding = new Thickness(btn.Padding.Left, btn.Padding.Top, vd, btn.Padding.Bottom);
                            }
                            else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.Padding = new Thickness(btn.Padding.Left, btn.Padding.Top, btn.Padding.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderWidth.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.BorderThickness = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderColor.ToString())
                            {
                                btn.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                                      (SolidColorBrush)new BrushConverter().ConvertFrom(prop.Value);
                            }
                            else if (prop.Name == PropertyNames.BorderRadius.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.CornerRadius = new CornerRadius(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.CornerRadius = new CornerRadius(vd, btn.CornerRadius.TopRight,
                                                              btn.CornerRadius.BottomRight, btn.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.CornerRadius = new CornerRadius(btn.CornerRadius.TopLeft, btn.CornerRadius.TopRight,
                                                              btn.CornerRadius.BottomRight, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.CornerRadius = new CornerRadius(btn.CornerRadius.TopLeft, vd,
                                                              btn.CornerRadius.BottomRight, btn.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                btn.CornerRadius = new CornerRadius(btn.CornerRadius.TopLeft, btn.CornerRadius.TopRight,
                                                              vd, btn.CornerRadius.BottomLeft);
                            }
                            #endregion
                        }
                    }
                }
            }
        }

        public override CompSerialiser OnSerialiser()
        {
            return new CompSerialiser
            {
                Name = EnumName.ToString(),
                Properties = groupProps,
                Children = text != null ? new List<CompSerialiser> { text.OnSerialiser() } : null
            };
        }

        public override void OnDeserialiser(CompSerialiser compSerialiser)
        {
            groupProps = compSerialiser.Properties;
            if (compSerialiser.Children != null)
            {
                string name = compSerialiser.Children[0].Name!;
                Component component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this;
                component.OnDeserialiser(compSerialiser.Children[0]);
                text = component;
                btn.Child = text.ComponentView;
            }
            OnInitialized();
        }

        public override void OnAddConfig(GroupNames groupName, PropertyNames propertyName, string value, bool isGroup = true)
        {
            int[] pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1)
            {
                int idG = pos[0], idP = pos[1];
                if (isGroup)
                {
                    groupProps![idG].Visibility = value;
                    if (value == VisibilityValue.Collapsed.ToString())
                        for (int i = 0; i < groupProps[idG].Properties.Count; i++)
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
            if (Selected || Properties.ComponentOutsUsing![id].Name == ComponentList.Button.ToString())
            {
                if (id >= 0 && Properties.ComponentOutsUsing![id].Name == ComponentList.Button.ToString() && !Selected)
                {
                    foreach (var group in Properties.ComponentOutsUsing[id].Component)
                    {
                        int i = Properties.ComponentOutsUsing[id].Component.IndexOf(group);
                        groupProps![i].Visibility = group.Visibility + "";
                        foreach (var prop in group.Properties)
                        {
                            int j = group.Properties.IndexOf(prop);
                            groupProps[i].Properties[j].Visibility = prop.Visibility + "";
                            groupProps[i].Properties[j].Value = prop.Value + "";
                        }
                    }
                }

                if (text == null)
                {
                    foreach (var child in Properties.ComponentOutsUsing!)
                    {
                        if (child.ParentId == id && child.Name != ComponentList.Button.ToString())
                        {
                            Component component = ManageEnums.Instance.GetComponent(child.Name);
                            component.OnConfiguOut(Properties.ComponentOutsUsing.IndexOf(child));
                            component.Parent = this;
                            //Restrictions actuelles de button
                            component.OnInitialized();
                            component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, "1", true); 
                            component.OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, "1", true);
                            text = component; btn.Child = text.ComponentView; break;
                        }
                    }
                }
            }
            else
            {
                if (text.Selected) text.OnConfiguOut(id);
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
            CompSerialiser valueD = null;
            if (value != null)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerialiser>(value)!;

            if (Selected && isPaste && text != null && value != null && valueD.Children != null)
            {
                string name = valueD.Name!;
                Component component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this; component.Added = true;
                component.OnDeserialiser(valueD);
                //Il faut enfin appliquer à ce composant, les contraintes de mise en page.
                //text = component;
                //text.Child = text.ComponentView;
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerialiser());
            else if (!Selected && text != null)
                return text.OnCopyOrPaste(value!, isPaste);
            return null!;
        }

        public override StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = EnumName.ToString()!;
            structuralElement.Selected = Selected;
            if (text != null)
                structuralElement.Children = new List<StructuralElement> { text.AddToStructuralView() };

            return structuralElement;
        }

        public override void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
            }
            else if (structuralElement.Children != null! && text != null!)
            {
                foreach (var child in structuralElement.Children!)
                {
                    text.SelectFromStructuralView(child);
                }
            }
        }

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            return;
        }

        public override bool AllowFixSize(bool isHeight = true)
        {
            return false;
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
            ReactComponent comp = new ReactComponent();
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
            return ComponentView!.ActualWidth;
        }
        public override double Height(Component component)
        {
            return ComponentView!.Height;
        }

        public override void OnConfigured()
        {
            groupProps = new List<GroupProperties>
            {
                new GroupProperties()
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
                            Value = "0",
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
                            Value = "50",
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
                            Value = "#FF26C6DA",
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
                        }
                    }
                }
            };
        }

    }
}
