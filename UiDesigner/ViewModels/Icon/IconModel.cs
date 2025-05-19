using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;
using FontAwesome.WPF;
using MaterialDesignThemes.Wpf;
using UiDesigner.Assets.GoogleFontIcons;
using UiDesigner.Models;
using UiDesigner.Utils;
using UiDesigner.ViewModels.Icon;

namespace ConceptorUI.ViewModels.Icon
{
    internal class IconModel : Component
    {
        private readonly Grid _grid;
        private readonly PackIcon _materialIcon;
        private readonly ImageAwesome _awesomeIcon;
        private readonly GoogleFontIcon _googleFontIcon;

        public IconModel(bool allowConstraints = false)
        {
            OnInit();

            _grid = new Grid();
            _materialIcon = new PackIcon
            {
                Margin = new Thickness(1, 1, 0, 0),
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            _awesomeIcon = new ImageAwesome
            {
                Visibility = Visibility.Collapsed
            };
            _googleFontIcon = new GoogleFontIcon
            {
                Visibility = Visibility.Collapsed,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 1),
            };

            _grid.Children.Add(_materialIcon);
            _grid.Children.Add(_awesomeIcon);
            _grid.Children.Add(_googleFontIcon);
            Content.Child = _grid;

            Name = ComponentList.Icon;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 0;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "20");
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "20");
            /* Text */
            this.SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            this.SetGroupVisibility(GroupNames.Appearance, false);
            this.SetGroupOnlyVisibility(GroupNames.Appearance);
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.Margin);
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.FillColor);
            this.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#000000");
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow, false);
        }

        protected override void WhenFileLoaded(string value)
        {
            var found = false;
            var iValue = Array.Empty<string>();
            value = value == "0" ? "Apple" : value;

            if (value.Length > 0 && value[0] == '[')
            {
                iValue = System.Text.Json.JsonSerializer.Deserialize<string[]>(value);
                found = true;
            }

            var myDataObject = new UiDesigner.Models.Icon { Code = found ? iValue![0] : value };
            var myBinding = new System.Windows.Data.Binding("Code")
            {
                Source = myDataObject
            };

            if (iValue!.Length > 0)
                switch (iValue[1])
                {
                    case "Material":
                        _awesomeIcon.Visibility = Visibility.Collapsed;
                        _googleFontIcon.Visibility = Visibility.Collapsed;
                        _materialIcon.Visibility = Visibility.Visible;
                        BindingOperations.SetBinding(_materialIcon, PackIcon.KindProperty, myBinding);
                        break;
                    case "FontAwesome":
                        _materialIcon.Visibility = Visibility.Collapsed;
                        _googleFontIcon.Visibility = Visibility.Collapsed;
                        _awesomeIcon.Visibility = Visibility.Visible;
                        BindingOperations.SetBinding(_awesomeIcon, ImageAwesome.IconProperty, myBinding);
                        break;
                    case "GoogleFontIcons":
                        _materialIcon.Visibility = Visibility.Collapsed;
                        _awesomeIcon.Visibility = Visibility.Collapsed;
                        _googleFontIcon.Visibility = Visibility.Visible;
                        BindingOperations.SetBinding(_googleFontIcon, GoogleFontIcon.IconNameProperty, myBinding);

                        var googleFontIcon = _googleFontIcon.GetIcon();
                        if (googleFontIcon == null!) return;
                        var width = Convert.ToInt32(this.GetGroupProperties(GroupNames.Transform)
                            .GetValue(PropertyNames.Width));
                        var color = this.GetGroupProperties(GroupNames.Appearance)
                            .GetValue(PropertyNames.FillColor);

                        ((TextBlock)googleFontIcon).FontSize = width;
                        ((TextBlock)googleFontIcon).Foreground = color == ColorValue.Transparent.ToString()
                            ? Brushes.Transparent
                            : new BrushConverter().ConvertFrom(color) as SolidColorBrush;
                        break;
                }
            else BindingOperations.SetBinding(_materialIcon, PackIcon.KindProperty, myBinding);
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            var googleFontIcon = _googleFontIcon.GetIcon();

            return e.Source.Equals(_materialIcon) || e.OriginalSource.Equals(_awesomeIcon) ||
                   e.OriginalSource.Equals(_googleFontIcon) || e.OriginalSource.Equals(_grid) ||
                   (googleFontIcon != null! && e.OriginalSource.Equals((TextBlock)googleFontIcon));
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (propertyName == PropertyNames.FillColor)
            {
                _materialIcon.Foreground = _awesomeIcon.Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;

                Content.Background = ShadowContent.Background = Brushes.Transparent;

                var googleFontIcon = _googleFontIcon.GetIcon();
                if (googleFontIcon == null!) return;
                ((TextBlock)googleFontIcon).Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;
            }
            else if ((propertyName is PropertyNames.Width or PropertyNames.Height) &&
                     value != SizeValue.Expand.ToString() && value != SizeValue.Auto.ToString())
            {
                var vd = Helper.ConvertToDouble(value);
                if (vd < 0) return;

                SelectedContent.Width = vd;
                SelectedContent.Height = vd;

                var vd2 = vd - 2;
                if (vd2 < 0) return;

                _materialIcon.Width = _materialIcon.Height = vd2;
                _awesomeIcon.Width = _awesomeIcon.Height = vd2;
                _googleFontIcon.Width = _googleFontIcon.Height = vd2;

                this.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, value);
                this.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, value);

                var googleFontIcon = _googleFontIcon.GetIcon();
                if (googleFontIcon == null!) return;
                ((TextBlock)googleFontIcon).FontSize = vd;
            }
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
            if (propertyName == PropertyNames.FillColor.ToString())
            {
                _materialIcon.Foreground = _awesomeIcon.Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;

                Content.Background = ShadowContent.Background = Brushes.Transparent;

                var googleFontIcon = _googleFontIcon.GetIcon();
                if (googleFontIcon == null!) return;
                ((TextBlock)googleFontIcon).Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;
            }
            else if ((propertyName == PropertyNames.Width.ToString() ||
                      propertyName == PropertyNames.Height.ToString()) && value != SizeValue.Expand.ToString() &&
                     value != SizeValue.Auto.ToString())
            {
                var vd = Helper.ConvertToDouble(value);
                SelectedContent.Width = vd;
                SelectedContent.Height = vd;

                _materialIcon.Width = _materialIcon.Height = vd - 2;
                _awesomeIcon.Width = _awesomeIcon.Height = vd - 2;
                _googleFontIcon.Width = _googleFontIcon.Height = vd - 2;

                var googleFontIcon = _googleFontIcon.GetIcon();
                if (googleFontIcon == null!) return;
                ((TextBlock)googleFontIcon).FontSize = vd - 2;
            }
        }

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
        }

        public override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        public override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return false;
        }

        public override bool AllowAuto(bool isWidth = true)
        {
            return false;
        }

        protected override List<GroupProperties> GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        public override void Delete(int k = -1)
        {
        }

        protected override void WhenWidthChanged(string value)
        {
        }

        protected override void WhenHeightChanged(string value)
        {
        }

        protected override void OnMoveLeft()
        {
        }

        protected override void OnMoveRight()
        {
        }

        protected override void OnMoveTop()
        {
        }

        protected override void OnMoveBottom()
        {
            /*
                _materialIcon.Height = vd;
                _awesomeIcon.Height = vd;
                _materialIcon.Foreground = _awesomeIcon.Foreground = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                   new BrushConverter().ConvertFrom(value) as SolidColorBrush;
             */
        }

        protected override bool CanSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override bool CanChildSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override void RestoreProperties()
        {
            IconRestoreProperties.RestoreProperties(this);
        }

        protected override void CheckVisibilities()
        {
            IconVisibility.SetVisibilities(this);
        }
    }
}