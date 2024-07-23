using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using ConceptorUI.Constants;


namespace ConceptorUI.ViewModels
{
    class ImageModel : Component
    {
        public ImageBrush Child;
        //public Border borderP;
        public Border borderP;
        
        public ImageModel(bool isConstraints = false)
        {
            Child = new ImageBrush();
            Child.Stretch = Stretch.Fill;
            borderP = new Border();
            borderP.Background = Child;
            //borderP = new Border();
            //borderP.Child = border;
            ComponentView = borderP;
            ComponentView.PreviewMouseDown += OnMouseDown;
            EnumName = ComponentList.Image;
            OnConfigured();
            LoadIds();
            if (!isConstraints)
                OnInitialized();
        }
        
        public override bool OnSelected(bool isInterne = false)
        {
            borderP.BorderBrush = Brushes.DodgerBlue;
            borderP.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Image;
            PanelProperty.Instance.FeedProps();
            Selected = true;
            return true;
        }

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            borderP.BorderBrush = Brushes.Transparent;
            borderP.BorderThickness = new Thickness(0);
        }

        public override bool OnChildSelected()
        {
            return Selected;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!e.OriginalSource.Equals(borderP) && !e.OriginalSource.Equals(Child)) return;
            PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
            OnSelected();
            PageView.Instance.OnSelected();
            PageView.Instance.RefreshStructuralView();
        }

        public override void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            var pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1 && (Selected || allow))
            {
                int idG = pos[0], idP = pos[1];
                var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
                var idGAl = posAl[0];

                var propName = "";
                try { propName = groupProps![idG].Properties[idP].Name; }
                catch (Exception e)
                {
                    // ignored
                }

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
                        borderP.HorizontalAlignment = value == "0" ? borderP.HorizontalAlignment : HorizontalAlignment.Left;
                    }
                    else if (propName == PropertyNames.HC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        Console.WriteLine(@"Entre bien dans HC");
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
                    /* Transform */
                    #region
                    else if (propName == PropertyNames.Width.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[0].Value = "0";
                            groupProps[idGAl].Properties[1].Value = "0";
                            groupProps[idGAl].Properties[2].Value = "0";
                            //PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                            //groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps![idGAl].Properties[0].Value == "1") borderP.HorizontalAlignment = HorizontalAlignment.Left;
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
                            double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
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
                            borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[3].Value = "0";
                            groupProps[idGAl].Properties[4].Value = "0";
                            groupProps[idGAl].Properties[5].Value = "0";
                            //PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Stretch;
                            //groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps![idGAl].Properties[3].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
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
                            double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            borderP.Height = vd;
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString() || propName == PropertyNames.VE.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.Stretch)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                        Child.Stretch = value == "Uniform" ? Stretch.Uniform : (
                            value == "UniformToFill" ? Stretch.UniformToFill : (
                                value == "Fill" ? Stretch.Fill : Stretch.None));
                    }
                    #endregion
                    /* Text */
                    #region
                    #endregion
                    /* Appearance */
                    #region
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
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        borderP.Margin = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.MarginLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(vd, borderP.Margin.Top, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginTop.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, vd, borderP.Margin.Right, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, vd, borderP.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginBottom.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, borderP.Margin.Right, vd);
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
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        borderP.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.CornerRadius = new CornerRadius(vd, borderP.CornerRadius.TopRight,
                                                      borderP.CornerRadius.BottomRight, borderP.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.CornerRadius = new CornerRadius(borderP.CornerRadius.TopLeft, borderP.CornerRadius.TopRight,
                                                      borderP.CornerRadius.BottomRight, vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.CornerRadius = new CornerRadius(borderP.CornerRadius.TopLeft, vd,
                                                      borderP.CornerRadius.BottomRight, borderP.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        borderP.CornerRadius = new CornerRadius(borderP.CornerRadius.TopLeft, borderP.CornerRadius.TopRight,
                                                      vd, borderP.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Global Property */
                    #region
                    else if (propertyName == PropertyNames.FilePicker)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                        var path = Env.pemcFile($"Project{PageView.Instance.application.ID}", "Medias", value);
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                        bitmap.EndInit();
                        Child.ImageSource = bitmap;
                    }
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.SelectedMode)
                    {
                        groupProps![idG].Properties[idP].Value = value == ESelectedMode.Multiple.ToString() ? value : ESelectedMode.Single.ToString();
                    }
                    #endregion
                }
            }
        }

        public sealed override void OnInitialized()
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
                                    double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                    borderP.Height = vd;
                                    if (groupProps[idGAl].Properties[3].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Top;
                                    else if (groupProps[idGAl].Properties[4].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[5].Value == "1") borderP.VerticalAlignment = VerticalAlignment.Bottom;
                                    else borderP.VerticalAlignment = VerticalAlignment.Top;
                                }
                            }
                            else if (prop.Name == PropertyNames.Stretch.ToString())
                            {
                                Child.Stretch = prop.Value == "Uniform" ? Stretch.Uniform : (
                                    prop.Value == "UniformToFill" ? Stretch.UniformToFill : (
                                        prop.Value == "Fill" ? Stretch.Fill : Stretch.None));
                            }
                            #endregion
                            /* Text */
                            #region
                            #endregion
                            /* Appearance */
                            #region
                            else if (prop.Name == PropertyNames.Margin.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.Margin = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.MarginLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.Margin = new Thickness(vd, borderP.Margin.Top, borderP.Margin.Right, borderP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.Margin = new Thickness(borderP.Margin.Left, vd, borderP.Margin.Right, borderP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, vd, borderP.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.Margin = new Thickness(borderP.Margin.Left, borderP.Margin.Top, borderP.Margin.Right, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadius.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.CornerRadius = new CornerRadius(vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.CornerRadius = new CornerRadius(vd, borderP.CornerRadius.TopRight,
                                                              borderP.CornerRadius.BottomRight, borderP.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.CornerRadius = new CornerRadius(borderP.CornerRadius.TopLeft, borderP.CornerRadius.TopRight,
                                                              borderP.CornerRadius.BottomRight, vd);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.CornerRadius = new CornerRadius(borderP.CornerRadius.TopLeft, vd,
                                                              borderP.CornerRadius.BottomRight, borderP.CornerRadius.BottomLeft);
                            }
                            else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                                borderP.CornerRadius = new CornerRadius(borderP.CornerRadius.TopLeft, borderP.CornerRadius.TopRight,
                                                              vd, borderP.CornerRadius.BottomLeft);
                            }
                            #endregion
                            /* Global Property */
                            #region
                            else if (prop.Name == PropertyNames.FilePicker.ToString())
                            {
                                try
                                {
                                    var path = Env.pemcFile($"Project{PageView.Instance.application.ID}", "Medias", prop.Value);
                                    var bitmap = new BitmapImage();
                                    bitmap.BeginInit();
                                    bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                                    bitmap.EndInit();
                                    Child.ImageSource = bitmap;
                                }
                                catch (Exception e)
                                {
                                    var path = prop.Value;
                                    var bitmap = new BitmapImage();
                                    bitmap.BeginInit();
                                    bitmap.UriSource = new Uri(@"pack://application:,,,/Assets/image.png", UriKind.Absolute);
                                    bitmap.EndInit();
                                    Child.ImageSource = bitmap;
                                }
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
            };
        }

        public override void OnDeserialiser(CompSerialiser compSerialiser)
        {
            groupProps = compSerialiser.Properties;
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
            if (Selected)
            {
                if (Properties.ComponentOutsUsing![id].Name == ComponentList.Image.ToString())
                {
                    foreach (var group in Properties.ComponentOutsUsing[id].Component)
                    {
                        int i = Properties.ComponentOutsUsing[id].Component.IndexOf(group);
                        groupProps![i].Visibility = group.Visibility;
                        foreach (var prop in group.Properties)
                        {
                            int j = group.Properties.IndexOf(prop);
                            groupProps[i].Properties[j].Visibility = prop.Visibility;
                            groupProps[i].Properties[j].Value = prop.Value;
                        }
                    }
                }
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
            if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerialiser());
            return null!;
        }

        public override StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = EnumName.ToString()!;
            structuralElement.Selected = Selected;
            structuralElement.IsSimpleElement = true;

            return structuralElement;
        }

        public override void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
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
            var comp = new ReactComponent();
            var imports = new Dictionary<string, Dictionary<string, bool>> {
                {"react-native", new Dictionary<string, bool>{{ "StyleSheet", false}, {"View", false}}}
            };
            tab = tab != "" ? tabs(n + 1) + tab + "\n" : "";
            comp.Imports = imports;
            var childComp = Child != null ? new ReactComponent() : null;
            var styleName = $"container{id}";
            comp.Body = $"{tabs(n)}<View \n{tabs(n + 1)}style={"{styles."}{styleName}{"}"}>\n{(Child != null ? childComp!.Body : "")}\n{tab}{tabs(n)}</View>";
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
            double leftv = 0, topv = 0, rightv = 0, bottomv = 0;
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
                string hcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                string hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                leftv = (((pw - w) / 2) / pw) * 100; rightv = 0;
                left = hls == "1" ? "'0%'" : (hcs == "1" ? "'" + leftv + "%'" : invalide);
                right = hrs == "1" ? "'0%'" : invalide;

                string vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                string vcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                string vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                topv = (((ph - h) / 2) / ph) * 100; bottomv = 0;
                top = vts == "1" ? "'0%'" : (vcs == "1" ? "'" + topv + "%'" : invalide);
                bottom = vbs == "1" ? "'0%'" : invalide;
            }
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
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                flex = wd == SizeValue.Expand.ToString() ? "1" : invalide;
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Stack)
            {
                height = hd == SizeValue.Expand.ToString() ? "'100%'" : (hd == SizeValue.Auto.ToString() ? invalide : hd);
                width = wd == SizeValue.Expand.ToString() ? "'100%'" : (wd == SizeValue.Auto.ToString() ? invalide : wd);
                flex = invalide;
            }
            else
            {
                height = hd == SizeValue.Expand.ToString() || hd == SizeValue.Auto.ToString() ? invalide : hd;
                width = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? invalide : wd;
                flex = invalide;
            }
            /* Appearance */
            string backgroundColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            string cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            string margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            string marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            string marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            string marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            string marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
            if (Parent != null && Parent!.EnumName == ComponentList.Stack)
            {
                //Ici les marges sont des positions: left, right, top, bottom.
                //Il ne s'agit pas d'une modification de ces positions, mais d'un ajout de valeur.

            }

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
            comp.Styles = new Dictionary<string, Dictionary<string, string>> {
                {
                    styleName,
                    new Dictionary<string, string>
                    {
                        /* Content alignement */
                        { "position", Parent!.EnumName == ComponentList.Stack ? "'absolute'" : invalide },
                        { "left", left},
                        { "top", top},
                        { "right", right},
                        { "bottom", bottom},
                        { "justifyContent", justifyContent != invalide ? "'"+justifyContent+"'" : invalide },
                        { "alignItems", alignItems != invalide ? "'"+alignItems+"'" : invalide },
                        /* Self alignement */
                        { "alignSelf", selfAlign != invalide ? "'"+selfAlign+"'" : invalide },
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

            return comp;
        }

        public override WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams)
        {
            WebComponent comp = new WebComponent();
            tab = tab != "" ? tabs(n + 1) + tab + "\n" : "";
            double w = Width(this), h = Height(this);
            double pw = Parent!.Width(Parent), ph = Parent.Height(Parent);
            string styleName = $"image{id}";
            #region
            const string invalide = "invalide";
            /* Global */
            string imagePath = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.FilePicker.ToString()]].Value;
            /* Self alignement */
            string position = invalide;
            string left = invalide;
            string top = invalide;
            string floatv = invalide;
            double cmarginLeft = 0, cmarginTop = 0;
            string hls = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
            string hcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
            string hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;

            string vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
            string vcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
            string vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;

            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                cmarginLeft = hls == "1" ? 0 : (hcs == "1" ? (pw - w) / 2 : (hrs == "1" ? pw - w : 0));
                int i = Convert.ToInt32(lparams[2]);
                bool end = Convert.ToBoolean(lparams[3]);
                if (lparams[0] == "sb")
                {
                    if (i == 0) cmarginTop = 0;
                    else if (end) cmarginTop = Convert.ToDouble(lparams[1]) - h;
                    else cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    if (i == 0) cmarginTop = Convert.ToDouble(lparams[1]) - h;
                    else if (end) cmarginTop = (Convert.ToDouble(lparams[1]) - h) / 2;
                    else cmarginTop = 0;
                }
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                cmarginTop = vts == "1" ? 0 : (vcs == "1" ? (ph - h) / 2 : (vbs == "1" ? ph - h : 0));
                int i = Convert.ToInt32(lparams[2]);
                bool end = Convert.ToBoolean(lparams[3]);
                if (lparams[0] == "sb")
                {
                    if (i == 0) cmarginLeft = 0;
                    else if (end) cmarginLeft = Convert.ToDouble(lparams[1]) - w;
                    else cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                }
                else if (lparams[0] == "sa")
                {
                    if (i == 0) cmarginLeft = Convert.ToDouble(lparams[1]) - w;
                    else if (end) cmarginLeft = (Convert.ToDouble(lparams[1]) - w) / 2;
                    else cmarginLeft = 0;
                }
                else floatv = "left";
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Container)
            {
                //cmarginTop = vts == "1" ? 0 : (vcs == "1" ? (ph - h) / 2 : (vbs == "1" ? ph - h : 0));
                cmarginLeft = hls == "1" ? 0 : (hcs == "1" ? (pw - w) / 2 : (hrs == "1" ? pw - w : 0));
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Stack)
            {
                cmarginTop = vts == "1" ? 0 : (vcs == "1" ? (ph - h) / 2 : (vbs == "1" ? ph - h : 0));
                cmarginLeft = hls == "1" ? 0 : (hcs == "1" ? (pw - w) / 2 : (hrs == "1" ? pw - w : 0));
                position = "absolute";
                left = $"{cmarginLeft}";
                top = $"{cmarginTop}";
            }
            /* Appearance */
            string backgroundColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            string cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            string margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            string marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            string marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            string marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            string marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
            
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
                        /* Global */
                        { "position", position},
                        { "float", floatv},
                        /* Transform */
                        { "width", $"{w}px".Replace(",", ".") },
                        { "height", $"{h}px".Replace(",", ".") },
                        /* Appearance */
                        { "background-color", invalide },
                        { "margin", cmargin == "1" && margin != "0" ? margin : invalide },
                        { "margin-left", (cmargin == "0" && marginLeft != "0") || cmarginLeft != 0 ? $"{Convert.ToDouble(marginLeft) + cmarginLeft}px".Replace(",", ".") : invalide },
                        { "margin-top", (cmargin == "0" && marginTop != "0") || cmarginTop != 0 ? $"{Convert.ToDouble(marginTop) + cmarginTop}px".Replace(",", ".") : invalide },
                        { "margin-right", cmargin == "0" && marginRight != "0" ? marginRight+"px" : invalide },
                        { "margin-bottom", cmargin == "0" && marginBottom != "0" ? marginBottom+"px" : invalide },

                        { "border-radius", cborderRadius == "1" && borderRadius != "0" ? borderRadius+"px" : invalide },
                        { "border-top-left-radius", cborderRadius == "0" && borderRadiusTopLeft != "0" ? borderRadiusTopLeft+"px" : invalide },
                        { "border-top-right-radius", cborderRadius == "0" && borderRadiusTopRight != "0" ? borderRadiusTopRight+"px" : invalide },
                        { "border-bottom-left-radius", cborderRadius == "0" && borderRadiusBottomLeft != "0" ? borderRadiusBottomLeft+"px" : invalide },
                        { "border-bottom-right-radius", cborderRadius == "0" && borderRadiusBottomRight != "0" ? borderRadiusBottomRight+"px" : invalide },
                    }
                }
            };
            #endregion
            var styles2 = styles;
            bool found = false; string keyStyle = string.Empty;
            foreach (var key in styles2!.Keys)
            {
                if (comp.EqualStyles(comp.Styles[styleName], styles2[key]))
                {
                    found = true; keyStyle = key;
                }
                comp.Styles.Add(key, styles2[key]);
            }
            if (found) { comp.Styles.Remove(styleName); styleName = keyStyle; }
            comp.Body = $"{tabs(n)}<img class=\"{styleName}\" src=\"{imagePath}\" alt=\"img\"/>";
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

        public override double Width(Component component)
        {
            return ComponentView!.ActualWidth;
        }
        public override double Height(Component component)
        {
            return ComponentView!.ActualHeight;
        }

        public sealed override void OnConfigured()
        {
            #region
            groupProps = new List<GroupProperties>
            {
                new GroupProperties()
                {
                    Name = GroupNames.Alignment.ToString(),
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
                            Value = "50",
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
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                            Name = PropertyNames.Stretch.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "Uniform",
                            Visibility = VisibilityValue.Visible.ToString()
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
                            Value = "#FFE0E0E0",
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.PaddingLeft.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Padding.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingTop.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.PaddingBottom.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.CBorder.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "BorderLeft",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderLeftWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderLeftColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderLeftStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderTopWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderTopColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderTopStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRightWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRightColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRightStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBottomWidth.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBottomColor.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = ColorValue.Transparent.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderBottomStyle.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                            Value = "/Assets/image.png",
                            Visibility = VisibilityValue.Visible.ToString()
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
                }
            };
            #endregion
        }
    }
}
