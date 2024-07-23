using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System;

namespace ConceptorUI.ViewModels
{
    internal class TextSingleModel : Component
    {

        Border border;
        TextBlock text;
        public TextSingleModel(bool isConstraints = false)
        {
            border = new Border();
            text = new TextBlock();
            border.Child = text;
            //border.HorizontalAlignment = HorizontalAlignment.Left;
            ComponentView = border; EnumName = ComponentList.TextSingle;
            ComponentView.PreviewMouseDown += OnMouseDown;
            OnConfigured();
            LoadIds();
            if (!isConstraints) OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            border.BorderBrush = Brushes.DodgerBlue;
            border.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.TextSingle;
            PanelProperty.Instance.FeedProps();
            Selected = true;
            return true;
        }

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            border.BorderBrush = Brushes.Transparent;
            border.BorderThickness = new Thickness(0);
        }

        public override bool OnChildSelected()
        {
            return Selected;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int[] pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            if (groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(text) || e.OriginalSource.Equals(border)))
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
            if (pos[0] != -1 && pos[1] != -1)
            {
                int idG = pos[0], idP = pos[1];
                var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
                var idGAl = posAl[0];
                if (Selected || allow)
                {
                    string propName = groupProps![idG].Properties[idP].Name;
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
                            border.Width = double.NaN;
                            border.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[0].Value = "0";
                            groupProps[idGAl].Properties[1].Value = "0";
                            groupProps[idGAl].Properties[2].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            border.Width = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[0].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                border.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0;
                            double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            border.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                border.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                            }
                        }
                    }
                    else if (propName == PropertyNames.Height.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            border.Height = double.NaN; border.VerticalAlignment = VerticalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            groupProps[idGAl].Properties[3].Value = "0";
                            groupProps[idGAl].Properties[4].Value = "0";
                            groupProps[idGAl].Properties[5].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            border.Height = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps[idGAl].Properties[3].Value == "1") border.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") border.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") border.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                border.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            border.Height = vd;
                            if (groupProps[idGAl].Properties[3].Value == "1") border.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") border.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") border.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                border.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                            }
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    #endregion
                    /* Text */
                    #region
                    if (propName == PropertyNames.FontFamily.ToString())
                    {
                        text.FontFamily = ManageEnums.Instance.GetFontFamily(value);
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.FontWeight.ToString())
                    {
                        text.FontWeight = value == "0" ? FontWeights.Normal : FontWeights.Bold;
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.FontStyle.ToString())
                    {
                        text.FontStyle = value == "0" ? FontStyles.Normal : FontStyles.Italic;
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.FontSize.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        vd = vd == 0 ? 10 : vd;
                        groupProps[idG].Properties[idP].Value = vd + "";
                        text.FontSize = vd;
                    }
                    else if (propName == PropertyNames.AlignLeft.ToString())
                    {
                        text.TextAlignment = TextAlignment.Left;
                        groupProps[idG].Properties[4].Value = groupProps[idG].Properties[5].Value =
                        groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.AlignCenter.ToString())
                    {
                        text.TextAlignment = TextAlignment.Center;
                        groupProps[idG].Properties[4].Value = groupProps[idG].Properties[5].Value =
                        groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.AlignRight.ToString())
                    {
                        text.TextAlignment = TextAlignment.Right;
                        groupProps[idG].Properties[4].Value = groupProps[idG].Properties[5].Value =
                        groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.AlignJustify.ToString())
                    {
                        text.TextAlignment = TextAlignment.Justify;
                        groupProps[idG].Properties[4].Value = groupProps[idG].Properties[5].Value =
                        groupProps[idG].Properties[6].Value = groupProps[idG].Properties[7].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.TextUnderline.ToString())
                    {
                        text.TextDecorations = value == "1" ? TextDecorations.Underline : null;
                        groupProps[idG].Properties[12].Value = groupProps[idG].Properties[13].Value =
                        groupProps[idG].Properties[14].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.TextOverline.ToString())
                    {
                        text.TextDecorations = value == "1" ? TextDecorations.OverLine : null;
                        groupProps[idG].Properties[12].Value = groupProps[idG].Properties[13].Value =
                        groupProps[idG].Properties[14].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.TextThrough.ToString())
                    {
                        text.TextDecorations = value == "1" ? TextDecorations.Strikethrough : null;
                        groupProps[idG].Properties[12].Value = groupProps[idG].Properties[13].Value =
                        groupProps[idG].Properties[14].Value = "0";
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Color.ToString())
                    {
                        text.Foreground = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                   new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.Text.ToString())
                    {
                        text.Text = value;
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.TextWrap.ToString())
                    {
                        text.TextWrapping = value == "0" ? TextWrapping.NoWrap : TextWrapping.Wrap;
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.LineSpacing.ToString())
                    {
                        double.TryParse(value, out var vd);
                        vd = vd == 0 ? 1 : vd;
                        text.LineHeight = vd;
                        groupProps[idG].Properties[idP].Value = vd + "";
                    }
                    #endregion
                    /* Appearance */
                    #region
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
                        border.Margin = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.MarginLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Margin = new Thickness(vd, border.Margin.Top, border.Margin.Right, border.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginTop.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Margin = new Thickness(border.Margin.Left, vd, border.Margin.Right, border.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Margin = new Thickness(border.Margin.Left, border.Margin.Top, vd, border.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginBottom.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps[idG].Properties[idP].Value = vd + "";
                        border.Margin = new Thickness(border.Margin.Left, border.Margin.Top, border.Margin.Right, vd);
                    }
                    #endregion
                    /* Global */
                    #region
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps[idG].Properties[idP].Value = value;
                    }
                    #endregion
                }
            }
        }

        public override void OnInitialized()
        {
            var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
            var idGAl = posAl[0];
            foreach (var group in groupProps!)
            {
                foreach (var prop in group.Properties)
                {
                    /* Alignement */
                    # region
                    if (prop.Name == PropertyNames.HL.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") border.HorizontalAlignment = HorizontalAlignment.Left;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.HC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") border.HorizontalAlignment = HorizontalAlignment.Center;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.HR.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") border.HorizontalAlignment = HorizontalAlignment.Right;
                        if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                            border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") border.VerticalAlignment = VerticalAlignment.Top;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            border.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") border.VerticalAlignment = VerticalAlignment.Center;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            border.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (prop.Value == "1") border.VerticalAlignment = VerticalAlignment.Bottom;
                        if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                            border.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (prop.Name == PropertyNames.Width.ToString())
                    {
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            border.Width = double.NaN;
                            border.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString())
                        {
                            border.Width = double.NaN;
                            if (groupProps[idGAl].Properties[0].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Right;
                            else border.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                            border.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") border.HorizontalAlignment = HorizontalAlignment.Right;
                            else border.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }
                    else if (prop.Name == PropertyNames.Height.ToString())
                    {
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            border.Height = double.NaN; border.VerticalAlignment = VerticalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString())
                        {
                            border.Height = double.NaN;
                            if (groupProps[idGAl].Properties[3].Value == "1") border.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") border.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") border.VerticalAlignment = VerticalAlignment.Bottom;
                            else border.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, NumberStyles.Any, new CultureInfo("en-US"), out vd);
                            border.Height = vd;
                            if (groupProps[idGAl].Properties[3].Value == "1") border.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") border.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") border.VerticalAlignment = VerticalAlignment.Bottom;
                            else border.VerticalAlignment = VerticalAlignment.Top;
                        }
                    }
                    #endregion
                    /* Text */
                    #region
                    if (prop.Name == PropertyNames.FontFamily.ToString())
                    {
                        text.FontFamily = ManageEnums.Instance.GetFontFamily(prop.Value);
                    }
                    else if (prop.Name == PropertyNames.FontWeight.ToString())
                    {
                        text.FontWeight = prop.Value == "0" ? FontWeights.Normal : FontWeights.Bold;
                    }
                    else if (prop.Name == PropertyNames.FontStyle.ToString())
                    {
                        text.FontStyle = prop.Value == "0" ? FontStyles.Normal : FontStyles.Italic;
                    }
                    else if (prop.Name == PropertyNames.FontSize.ToString())
                    {
                        double.TryParse(prop.Value, out var vd);
                        text.FontSize = vd;
                    }
                    else if (prop.Name == PropertyNames.AlignLeft.ToString())
                    {
                        if(prop.Value == "1") text.TextAlignment = TextAlignment.Left;
                    }
                    else if (prop.Name == PropertyNames.AlignCenter.ToString())
                    {
                        if (prop.Value == "1") text.TextAlignment = TextAlignment.Center;
                    }
                    else if (prop.Name == PropertyNames.AlignRight.ToString())
                    {
                        if (prop.Value == "1") text.TextAlignment = TextAlignment.Right;
                    }
                    else if (prop.Name == PropertyNames.AlignJustify.ToString())
                    {
                        if (prop.Value == "1") text.TextAlignment = TextAlignment.Justify;
                    }
                    else if (prop.Name == PropertyNames.TextUnderline.ToString())
                    {
                        if (prop.Value == "1") text.TextDecorations = TextDecorations.Underline;
                    }
                    else if (prop.Name == PropertyNames.TextOverline.ToString())
                    {
                        if (prop.Value == "1") text.TextDecorations = TextDecorations.OverLine;
                    }
                    else if (prop.Name == PropertyNames.TextThrough.ToString())
                    {
                        if (prop.Value == "1") text.TextDecorations = TextDecorations.Strikethrough;
                    }
                    else if (prop.Name == PropertyNames.Color.ToString())
                    {
                        text.Foreground = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                        new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.Text.ToString())
                    {
                        text.Text = prop.Value;
                    }
                    else if (prop.Name == PropertyNames.TextWrap.ToString())
                    {
                        text.TextWrapping = prop.Value == "0" ? TextWrapping.NoWrap : TextWrapping.Wrap;
                    }
                    else if (prop.Name == PropertyNames.LineSpacing.ToString())
                    {
                        double.TryParse(prop.Value, out var vd);
                        vd = vd == 0 ? 1 : vd;
                        text.LineHeight = vd;
                    }
                    #endregion
                    /* Appearance */
                    #region
                    else if (prop.Name == PropertyNames.Margin.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.Margin = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.MarginLeft.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.Margin = new Thickness(vd, border.Margin.Top, border.Margin.Right, border.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginTop.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.Margin = new Thickness(border.Margin.Left, vd, border.Margin.Right, border.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginRight.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.Margin = new Thickness(border.Margin.Left, border.Margin.Top, vd, border.Margin.Bottom);
                    }
                    else if (prop.Name == PropertyNames.MarginBottom.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.Margin = new Thickness(border.Margin.Left, border.Margin.Top, border.Margin.Right, vd);
                    }
                    #endregion
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
            var pos = GetPosition(groupName, propertyName);
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
                else if(propertyName != PropertyNames.Height)
                {
                    groupProps![idG].Properties[idP].Visibility = value;
                }
            }
        }

        public override void OnConfiguOut(int id)
        {
            if (Selected && Properties.ComponentOutsUsing![id].Name == ComponentList.TextSingle.ToString())
            {
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
            structuralElement.Node = EnumName.ToString();
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
            return Selected ? new List<ComponentRef> { new() { ControlName = EnumName, ControlPosition = i } } : null!;
        }

        public override ReactComponent BuildReactComponent(string tab, int n, string id)
        {
            var comp = new ReactComponent();
            var imports = new Dictionary<string, Dictionary<string, bool>> {
                {"react-native", new Dictionary<string, bool>{{ "StyleSheet", false}, {"Text", false}}}
            };
            comp.Imports = imports;
            string styleName = $"text{id}";
            #region
            const string invalide = "invalide";
            /* Self alignement */
            string selfAlign;
            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                string hls = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                string hcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                string hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                selfAlign = hls == "1" ? "flex-start" : (hcs == "1" ? "center" : (hrs == "1" ? "flex-end" : "stretch"));
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                string vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                string vcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                string vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                selfAlign = vts == "1" ? "flex-start" : (vcs == "1" ? "center" : (vbs == "1" ? "flex-end" : "stretch"));
            }
            else selfAlign = invalide;
            /* Text */
            string fontSize = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.FontSize.ToString()]].Value;
            string fontWeight = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.FontWeight.ToString()]].Value;
            string fontStyle = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.FontStyle.ToString()]].Value;
            string color = tcolor(groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.Color.ToString()]].Value);
            string text = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.Text.ToString()]].Value;

            string al = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignLeft.ToString()]].Value;
            string ac = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignCenter.ToString()]].Value;
            string ar = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignRight.ToString()]].Value;
            string aj = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignJustify.ToString()]].Value;
            string textAlign = al == "1" ? "left" : (ac == "1" ? "center" : (ar == "1" ? "right" : (aj == "1" ? "justify" : invalide)));

            string tdu = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.TextUnderline.ToString()]].Value;
            string tdo = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.TextThrough.ToString()]].Value;
            string textDecoration = tdu == "1" ? "underline" : (tdo == "1" ? "line-through" : invalide);
            /* Appearance */
            string cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            string margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            string marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            string marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            string marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            string marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
            #endregion
            comp.Styles = new Dictionary<string, Dictionary<string, string>> {
                {
                    styleName,
                    new Dictionary<string, string>
                    {
                        /* Self alignement */
                        { "alignSelf", selfAlign != invalide ? "'"+selfAlign+"'" : invalide },
                        /* Text */
                        { "fontSize", fontSize},
                        { "fontWeight", fontWeight == "0" ? invalide : "'bold'"},
                        { "fontStyle", fontStyle == "0" ? invalide : "'italic'"},
                        { "textAlign", textAlign == invalide ? invalide : "'"+textAlign+"'"},
                        { "color", "'"+color+"'"},
                        { "textDecorationLine", textDecoration == invalide ? invalide : "'"+textDecoration+"'"},
                        /* Appearance */
                        { "margin", cmargin == "1" && margin != "0" ? margin : invalide },
                        { "marginLeft", cmargin == "0" && marginLeft != "0" ? marginLeft : invalide },
                        { "marginTop", cmargin == "0" && marginTop != "0" ? marginTop : invalide },
                        { "marginRight", cmargin == "0" && marginRight != "0" ? marginRight : invalide },
                        { "marginBottom", cmargin == "0" && marginBottom != "0" ? marginBottom : invalide },
                    }
                }
            };
            comp.Body = $"{tabs(n)}<Text \n{tabs(n + 1)}style={"{styles."}{styleName}{"}"}>\n{tabs(n + 1) + text}\n{tabs(n)}</Text>";
            return comp;
        }

        public override WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams)
        {
            var comp = new WebComponent();
            tab = tab != "" ? tabs(n + 1) + tab + "\n" : "";
            double w = Width(this), h = Height(this);
            double pw = Parent!.Width(Parent), ph = Parent.Height(Parent);
            var styleName = $"text{id}";
            #region
            const string invalide = "invalide";
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
            /* Text */
            string fontFamily = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.FontFamily.ToString()]].Value;
            string fontSize = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.FontSize.ToString()]].Value;
            string fontWeight = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.FontWeight.ToString()]].Value;
            string fontStyle = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.FontStyle.ToString()]].Value;
            string color = tcolor(groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.Color.ToString()]].Value);
            string text = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.Text.ToString()]].Value;

            string al = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignLeft.ToString()]].Value;
            string ac = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignCenter.ToString()]].Value;
            string ar = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignRight.ToString()]].Value;
            string aj = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.AlignJustify.ToString()]].Value;
            string textAlign = al == "1" ? "left" : (ac == "1" ? "center" : (ar == "1" ? "right" : (aj == "1" ? "justify" : invalide)));

            string tdu = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.TextUnderline.ToString()]].Value;
            string tdt = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.TextThrough.ToString()]].Value;
            string tdo = groupProps![Ids![GroupNames.Text.ToString()].IdGroup].Properties[Ids![GroupNames.Text.ToString()].Props![PropertyNames.TextOverline.ToString()]].Value;
            string textDecoration = tdu == "1" ? "underline" : (tdt == "1" ? "line-through" : (tdo == "1" ? "overline" : invalide));
            /* Appearance */
            string backgroundColor = tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            string cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            string margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            string marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            string marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            string marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            string marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
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
                        //{ "width", $"{w}px".Replace(",", ".") },
                        //{ "height", $"{h}px".Replace(",", ".") },
                        /* Text */
                        { "font-family", fontFamily == "" ? invalide : $"{fontFamily.ToLower()}, sans-serif"},
                        { "font-size", $"{fontSize}px".Replace(",", ".")},
                        { "font-weight", fontWeight == "0" ? invalide : "bold"},
                        { "font-style", fontStyle == "0" ? invalide : "italic"},
                        { "text-align", textAlign == invalide ? invalide : textAlign},
                        { "color", color},
                        { "text-decoration", textDecoration == invalide ? invalide : textDecoration},
                        /* Appearance */
                        //{ "background-color", backgroundColor },
                        { "margin", cmargin == "1" && margin != "0" ? margin : invalide },
                        { "margin-left", (cmargin == "0" && marginLeft != "0") || cmarginLeft != 0 ? $"{Convert.ToDouble(marginLeft) + cmarginLeft}px".Replace(",", ".") : invalide },
                        { "margin-top", (cmargin == "0" && marginTop != "0") || cmarginTop != 0 ? $"{Convert.ToDouble(marginTop) + cmarginTop}px".Replace(",", ".") : invalide },
                        { "margin-right", cmargin == "0" && marginRight != "0" ? marginRight+"px" : invalide },
                        { "margin-bottom", cmargin == "0" && marginBottom != "0" ? marginBottom+"px" : invalide },
                    }
                }
            };
            #endregion
            var styles2 = styles;
            var found = false;
            var keyStyle = string.Empty;
            foreach (var key in styles2!.Keys)
            {
                if (comp.EqualStyles(comp.Styles[styleName], styles2[key]))
                {
                    found = true; keyStyle = key;
                }
                comp.Styles.Add(key, styles2[key]);
            }
            if (found) { comp.Styles.Remove(styleName); styleName = keyStyle; }
            comp.Body = $"{tabs(n)}<div class=\"{styleName}\">\n{tabs(n + 1) + text}\n{tab}{tabs(n)}</div>";
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
            return text!.ActualWidth;
        }
        public override double Height(Component component)
        {
            return text.ActualHeight;
        }

        public override void OnConfigured()
        {
            #region
            groupProps = new List<GroupProperties>
            {
                new()
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
                new()
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
                            Value = SizeValue.Auto.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                new()
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
                            Value = "1",
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
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadius.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusTopRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                new()
                {
                    Name = GroupNames.SelfAlignment.ToString(),
                    Visibility = VisibilityValue.Visible.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.HL.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "1",
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
                new()
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
                }
            };
            #endregion
        }
    }
}
