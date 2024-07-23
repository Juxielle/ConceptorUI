using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Data;
using FontAwesome.WPF;


namespace ConceptorUI.ViewModels
{
    internal class IconModel : Component
    {
        private readonly Border _border;
        private readonly Grid _grid;
        readonly PackIcon _materialIcon;
        readonly ImageAwesome _awesomeIcon;
        
        public IconModel(bool isConstraints = false)
        {
            _border = new Border();
            _grid = new Grid();
            _materialIcon = new PackIcon();
            _awesomeIcon = new ImageAwesome{Visibility = Visibility.Collapsed};

            _grid.Children.Add(_materialIcon);
            _grid.Children.Add(_awesomeIcon);
            _border.Child = _grid;
            ComponentView = _border; EnumName = ComponentList.Icon;
            ComponentView.PreviewMouseDown += OnMouseDown;
            
            OnConfigured();
            LoadIds();
            
            if (!isConstraints) OnInitialized();
        }

        public override bool OnSelected(bool isInterne = false)
        {
            _border.BorderBrush = Brushes.DodgerBlue;
            _border.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Icon;
            PanelProperty.Instance.FeedProps();
            Selected = true;
            return true;
        }

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            _border.BorderBrush = Brushes.Transparent;
            _border.BorderThickness = new Thickness(0);
        }

        public override bool OnChildSelected()
        {
            return Selected;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            
            if (groupProps![idG].Properties[idP].Value != CanSelectValues.None.ToString() ||
                (!e.Source.Equals(_materialIcon) && !e.Source.Equals(_awesomeIcon) && !e.OriginalSource.Equals(_border))) return;
            PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
            OnSelected();
            PageView.Instance.OnSelected();
            PageView.Instance.RefreshStructuralView();
        }

        public override void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            var pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1)
            {
                int idG = pos[0], idP = pos[1];
                var posAl = GetPosition(GroupNames.SelfAlignment, PropertyNames.HL);
                var idGAl = posAl[0];

                var propName = groupProps![idG].Properties[idP].Name;

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
                        _border.HorizontalAlignment = value == "0" ? _border.HorizontalAlignment : HorizontalAlignment.Left;
                    }
                    else if (propName == PropertyNames.HC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        _border.HorizontalAlignment = value == "0" ? _border.HorizontalAlignment : HorizontalAlignment.Center;
                    }
                    else if (propName == PropertyNames.HR.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[0].Value = value == "1" ? "0" : groupProps![idG].Properties[0].Value;
                        groupProps[idG].Properties[1].Value = value == "1" ? "0" : groupProps![idG].Properties[1].Value;
                        groupProps[idG].Properties[2].Value = value == "1" ? "0" : groupProps![idG].Properties[2].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        _border.HorizontalAlignment = value == "0" ? _border.HorizontalAlignment : HorizontalAlignment.Right;
                    }
                    else if (propName == PropertyNames.VT.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        _border.VerticalAlignment = value == "0" ? _border.VerticalAlignment : VerticalAlignment.Top;
                    }
                    else if (propName == PropertyNames.VC.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        _border.VerticalAlignment = value == "0" ? _border.VerticalAlignment : VerticalAlignment.Center;
                    }
                    else if (propName == PropertyNames.VB.ToString() && groupName == GroupNames.SelfAlignment)
                    {
                        groupProps![idG].Properties[3].Value = value == "1" ? "0" : groupProps![idG].Properties[3].Value;
                        groupProps[idG].Properties[4].Value = value == "1" ? "0" : groupProps![idG].Properties[4].Value;
                        groupProps[idG].Properties[5].Value = value == "1" ? "0" : groupProps![idG].Properties[5].Value;
                        groupProps[idG].Properties[idP].Value = value;
                        _border.VerticalAlignment = value == "0" ? _border.VerticalAlignment : VerticalAlignment.Bottom;
                    }
                    #endregion
                    /* Transform */
                    #region
                    else if (propName == PropertyNames.Width.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            //border.Width = double.NaN; border.HorizontalAlignment = HorizontalAlignment.Stretch;
                            //groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            //groupProps[idGAl].Properties[0].Value = "0";
                            //groupProps[idGAl].Properties[1].Value = "0";
                            //groupProps[idGAl].Properties[2].Value = "0";
                            //PanelProperty.Instance.FeedProps();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            _border.Width = double.NaN;
                            //groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps![idGAl].Properties[0].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                _border.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                //PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            _materialIcon.Width = _awesomeIcon.Width = vd;
                            if (groupProps[idGAl].Properties[0].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Left;
                            else if (groupProps[idGAl].Properties[1].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (groupProps[idGAl].Properties[2].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Right;
                            else
                            {
                                _border.HorizontalAlignment = HorizontalAlignment.Left;
                                groupProps[idGAl].Properties[0].Value = "1";
                                //PanelProperty.Instance.FeedProps();
                            }
                        }
                    }
                    else if (propName == PropertyNames.Height.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            //border.Height = double.NaN; border.VerticalAlignment = VerticalAlignment.Top;
                            //groupProps[idG].Properties[idP].Value = SizeValue.Expand.ToString();
                            //groupProps[idGAl].Properties[3].Value = "0";
                            //groupProps[idGAl].Properties[4].Value = "0";
                            //groupProps[idGAl].Properties[5].Value = "0";
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            _border.Height = double.NaN;
                            //groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            if (groupProps![idGAl].Properties[3].Value == "1") _border.VerticalAlignment = VerticalAlignment.Top;
                            else if (groupProps[idGAl].Properties[4].Value == "1") _border.VerticalAlignment = VerticalAlignment.Center;
                            else if (groupProps[idGAl].Properties[5].Value == "1") _border.VerticalAlignment = VerticalAlignment.Bottom;
                            else
                            {
                                _border.VerticalAlignment = VerticalAlignment.Top;
                                groupProps[idGAl].Properties[3].Value = "1";
                                //PanelProperty.Instance.FeedProps();
                            }
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            _materialIcon.Height = vd;
                            _awesomeIcon.Height = vd;
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString() || propName == PropertyNames.VE.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    #endregion
                    /* Appearance */
                    #region
                    else if (propName == PropertyNames.FillColor.ToString())
                    {
                        _materialIcon.Foreground = _awesomeIcon.Foreground = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                   new BrushConverter().ConvertFrom(value) as SolidColorBrush;
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
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        _border.Margin = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.MarginLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        _border.Margin = new Thickness(vd, _border.Margin.Top, _border.Margin.Right, _border.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginTop.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        _border.Margin = new Thickness(_border.Margin.Left, vd, _border.Margin.Right, _border.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        _border.Margin = new Thickness(_border.Margin.Left, _border.Margin.Top, vd, _border.Margin.Bottom);
                    }
                    else if (propName == PropertyNames.MarginBottom.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        _border.Margin = new Thickness(_border.Margin.Left, _border.Margin.Top, _border.Margin.Right, vd);
                    }
                    else if (propName == PropertyNames.CPadding.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.PaddingBtnActif.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    #endregion
                    #region Global
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propertyName == PropertyNames.FilePicker)
                    {
                        var found = false;
                        var iValue = Array.Empty<string>();
                        if (value.Length > 0 && value[0] == '[')
                        {
                            iValue = System.Text.Json.JsonSerializer.Deserialize<string[]>(value);
                            found = true;
                        }
                        
                        groupProps![idG].Properties[idP].Value = value;
                        var myDataObject = new Icon{Code = found ? iValue![0] : value};
                        var myBinding = new Binding("Code");
                        myBinding.Source = myDataObject;

                        if(iValue!.Length > 0)
                            switch (iValue![1])
                            {
                                case "Material":
                                    _awesomeIcon.Visibility = Visibility.Collapsed;
                                    _materialIcon.Visibility = Visibility.Visible;
                                    BindingOperations.SetBinding(_materialIcon, PackIcon.KindProperty, myBinding);
                                    break;
                                case "FontAwesome":
                                    _materialIcon.Visibility = Visibility.Collapsed;
                                    _awesomeIcon.Visibility = Visibility.Visible;
                                    BindingOperations.SetBinding(_awesomeIcon, ImageAwesome.IconProperty, myBinding);
                                    break;
                            }
                        else BindingOperations.SetBinding(_materialIcon, PackIcon.KindProperty, myBinding);
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
                                if (prop.Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Left;
                                if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                                    _border.HorizontalAlignment = HorizontalAlignment.Stretch;
                            }
                            else if (prop.Name == PropertyNames.HC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Center;
                                if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                                    _border.HorizontalAlignment = HorizontalAlignment.Stretch;
                            }
                            else if (prop.Name == PropertyNames.HR.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Right;
                                if (groupProps[idGAl].Properties[0].Value == "0" && groupProps[idGAl].Properties[1].Value == "0" && groupProps[idGAl].Properties[2].Value == "0")
                                    _border.HorizontalAlignment = HorizontalAlignment.Stretch;
                            }
                            else if (prop.Name == PropertyNames.VT.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") _border.VerticalAlignment = VerticalAlignment.Top;
                                if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                                    _border.VerticalAlignment = VerticalAlignment.Stretch;
                            }
                            else if (prop.Name == PropertyNames.VC.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") _border.VerticalAlignment = VerticalAlignment.Center;
                                if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                                    _border.VerticalAlignment = VerticalAlignment.Stretch;
                            }
                            else if (prop.Name == PropertyNames.VB.ToString() && group.Name == GroupNames.SelfAlignment.ToString())
                            {
                                if (prop.Value == "1") _border.VerticalAlignment = VerticalAlignment.Bottom;
                                if (groupProps[idGAl].Properties[3].Value == "0" && groupProps[idGAl].Properties[4].Value == "0" && groupProps[idGAl].Properties[5].Value == "0")
                                    _border.VerticalAlignment = VerticalAlignment.Stretch;
                            }
                            #endregion
                            /* Transform */
                            #region
                            else if (prop.Name == PropertyNames.Width.ToString())
                            {
                                if (prop.Value == SizeValue.Expand.ToString())
                                {
                                    //border.Width = double.NaN; border.HorizontalAlignment = HorizontalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    _border.Width = double.NaN;
                                    if (groupProps[idGAl].Properties[0].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Left;
                                    else if (groupProps[idGAl].Properties[1].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[2].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Right;
                                    else _border.HorizontalAlignment = HorizontalAlignment.Left;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    double vd = 0; double.TryParse(prop.Value, out vd);
                                    _materialIcon.Width = vd;
                                    _awesomeIcon.Width = vd;
                                    if (groupProps[idGAl].Properties[0].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Left;
                                    else if (groupProps[idGAl].Properties[1].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[2].Value == "1") _border.HorizontalAlignment = HorizontalAlignment.Right;
                                    else _border.HorizontalAlignment = HorizontalAlignment.Left;
                                }
                            }
                            else if (prop.Name == PropertyNames.Height.ToString())
                            {
                                if (prop.Value == SizeValue.Expand.ToString())
                                {
                                    //border.Height = double.NaN; border.VerticalAlignment = VerticalAlignment.Stretch;
                                }
                                else if (prop.Value == SizeValue.Auto.ToString())
                                {
                                    _border.Height = double.NaN;
                                    if (groupProps[idGAl].Properties[3].Value == "1") _border.VerticalAlignment = VerticalAlignment.Top;
                                    else if (groupProps[idGAl].Properties[4].Value == "1") _border.VerticalAlignment = VerticalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[5].Value == "1") _border.VerticalAlignment = VerticalAlignment.Bottom;
                                    else _border.VerticalAlignment = VerticalAlignment.Top;
                                }
                                else if (prop.Value != SizeValue.Old.ToString())
                                {
                                    double vd = 0; double.TryParse(prop.Value, out vd);
                                    _materialIcon.Height = vd;
                                    _awesomeIcon.Height = vd;
                                    if (groupProps[idGAl].Properties[3].Value == "1") _border.VerticalAlignment = VerticalAlignment.Top;
                                    else if (groupProps[idGAl].Properties[4].Value == "1") _border.VerticalAlignment = VerticalAlignment.Center;
                                    else if (groupProps[idGAl].Properties[5].Value == "1") _border.VerticalAlignment = VerticalAlignment.Bottom;
                                    else _border.VerticalAlignment = VerticalAlignment.Top;
                                }
                            }
                            #endregion
                            /* Appearance */
                            #region
                            else if (prop.Name == PropertyNames.FillColor.ToString())
                            {
                                _materialIcon.Foreground = _awesomeIcon.Foreground = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                               new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                            }
                            else if (prop.Name == PropertyNames.Margin.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                _border.Margin = new Thickness(vd);
                            }
                            else if (prop.Name == PropertyNames.MarginLeft.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                _border.Margin = new Thickness(vd, _border.Margin.Top, _border.Margin.Right, _border.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginTop.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                _border.Margin = new Thickness(_border.Margin.Left, vd, _border.Margin.Right, _border.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginRight.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                _border.Margin = new Thickness(_border.Margin.Left, _border.Margin.Top, vd, _border.Margin.Bottom);
                            }
                            else if (prop.Name == PropertyNames.MarginBottom.ToString())
                            {
                                double vd = 0; double.TryParse(prop.Value, out vd);
                                _border.Margin = new Thickness(_border.Margin.Left, _border.Margin.Top, _border.Margin.Right, vd);
                            }
                            #endregion
                            #region Global
                            else if (prop.Name == PropertyNames.FilePicker.ToString())
                            {
                                var found = false;
                                var iValue = Array.Empty<string>();
                                if (prop.Value.Length > 0 && prop.Value[0] == '[')
                                {
                                    iValue = System.Text.Json.JsonSerializer.Deserialize<string[]>(prop.Value);
                                    found = true;
                                }

                                var myDataObject = new Icon{Code = found ? iValue![0] : prop.Value};
                                var myBinding = new Binding("Code");
                                myBinding.Source = myDataObject;

                                if(iValue!.Length > 0)
                                    switch (iValue![1])
                                    {
                                        case "Material":
                                            _awesomeIcon.Visibility = Visibility.Collapsed;
                                            _materialIcon.Visibility = Visibility.Visible;
                                            BindingOperations.SetBinding(_materialIcon, PackIcon.KindProperty, myBinding);
                                            break;
                                        case "FontAwesome":
                                            _materialIcon.Visibility = Visibility.Collapsed;
                                            _awesomeIcon.Visibility = Visibility.Visible;
                                            BindingOperations.SetBinding(_awesomeIcon, ImageAwesome.IconProperty, myBinding);
                                            break;
                                    }
                                else BindingOperations.SetBinding(_materialIcon, PackIcon.KindProperty, myBinding);
                            }
                            #endregion
                        }
                    }
                }
            }
            //OnAddConfig(GroupNames.Global, PropertyNames.FilePicker, VisibilityValue.Visible.ToString(), false);
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
            if (Selected && Properties.ComponentOutsUsing![id].Name == ComponentList.Icon.ToString())
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
                if(group.Name == groupName.ToString())
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
            {
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerialiser());
            }
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
            return Selected ? new List<ComponentRef> { new() { ControlName = EnumName, ControlPosition = i } } : null!;
        }

        public override ReactComponent BuildReactComponent(string tab, int n, string id)
        {
            var comp = new ReactComponent();
            var value = groupProps![Ids![GroupNames.Global.ToString()].IdGroup].Properties[Ids![GroupNames.Global.ToString()].Props![PropertyNames.FilePicker.ToString()]].Value;
            var icond = Properties.Instance.GetIcon(value);
            var name = icond.IconPlateform![0].Name!;
            var component = icond.IconPlateform![0].Component!;
            var library = icond.IconPlateform![0].Library!;
            var imports = new Dictionary<string, Dictionary<string, bool>> {
                {library, new Dictionary<string, bool>{{ "FontAwesome", true}}}
            };
            comp.Imports = imports;
            var styleName = $"icon{id}";
            #region
            const string invalide = "invalide";
            /* Self alignement */
            string selfAlign;
            if (Parent != null && Parent!.EnumName == ComponentList.Row)
            {
                var hls = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HL.ToString()]].Value;
                var hcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HC.ToString()]].Value;
                var hrs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.HR.ToString()]].Value;
                selfAlign = hls == "1" ? "flex-start" : (hcs == "1" ? "center" : (hrs == "1" ? "flex-end" : "stretch"));
            }
            else if (Parent != null && Parent!.EnumName == ComponentList.Column)
            {
                var vts = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VT.ToString()]].Value;
                var vcs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VC.ToString()]].Value;
                var vbs = groupProps![Ids![GroupNames.SelfAlignment.ToString()].IdGroup].Properties[Ids![GroupNames.SelfAlignment.ToString()].Props![PropertyNames.VB.ToString()]].Value;
                selfAlign = vts == "1" ? "flex-start" : (vcs == "1" ? "center" : (vbs == "1" ? "flex-end" : "stretch"));
            }
            else selfAlign = invalide;
            /* Text */
            var color = Tcolor(groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.FillColor.ToString()]].Value);
            
            /* Transform */
            var wd = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Width.ToString()]].Value;
            var hd = groupProps![Ids![GroupNames.Transform.ToString()].IdGroup].Properties[Ids![GroupNames.Transform.ToString()].Props![PropertyNames.Height.ToString()]].Value;
            var size = wd == SizeValue.Expand.ToString() || wd == SizeValue.Auto.ToString() ? "10" : wd;
            /* Appearance */
            var cmargin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.CMargin.ToString()]].Value;
            var margin = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.Margin.ToString()]].Value;
            var marginLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginLeft.ToString()]].Value;
            var marginTop = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginTop.ToString()]].Value;
            var marginRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginRight.ToString()]].Value;
            var marginBottom = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.MarginBottom.ToString()]].Value;
            #endregion
            #region
            comp.Styles = new Dictionary<string, Dictionary<string, string>> {
                {
                    styleName,
                    new Dictionary<string, string>
                    {
                        /* Self alignement */
                        { "alignSelf", selfAlign != invalide ? "'"+selfAlign+"'" : invalide },
                        /* Appearance */
                        { "margin", cmargin == "1" && margin != "0" ? margin : invalide },
                        { "marginLeft", cmargin == "0" && marginLeft != "0" ? marginLeft : invalide },
                        { "marginTop", cmargin == "0" && marginTop != "0" ? marginTop : invalide },
                        { "marginRight", cmargin == "0" && marginRight != "0" ? marginRight : invalide },
                        { "marginBottom", cmargin == "0" && marginBottom != "0" ? marginBottom : invalide },
                    }
                }
            };
            #endregion
            comp.Body = $"{tabs(n)}<{component} style={"{styles."}{styleName}{"}"} name='{name}' size={"{"+size+"}"} color='{color}'/>";
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
            for (var i = 0; i < n; i++)
                tab += "    ";
            return tab;
        }

        private static string Tcolor(string color)
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
                            Value = "30",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Height.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "30",
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
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.VE.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                    Visibility = VisibilityValue.Collapsed.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.HL.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HC.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.HR.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                            Value = "Music",
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
                        }
                    }
                }
            };
            #endregion
        }
    }
}
