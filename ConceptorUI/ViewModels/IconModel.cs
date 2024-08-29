using System;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Utils;
using MaterialDesignThemes.Wpf;
using FontAwesome.WPF;


namespace ConceptorUi.ViewModels
{
    internal class IconModel : Component
    {
        private readonly Grid _grid;
        private readonly PackIcon _materialIcon;
        private readonly ImageAwesome _awesomeIcon;

        public IconModel(bool allowConstraints = false)
        {
            OnInit();

            _grid = new Grid();
            _materialIcon = new PackIcon();
            _awesomeIcon = new ImageAwesome { Visibility = Visibility.Collapsed };

            _grid.Children.Add(_materialIcon);
            _grid.Children.Add(_awesomeIcon);
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
            SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "20");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "20");
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetGroupVisibility(GroupNames.Appearance, false);
            SetGroupOnlyVisibility(GroupNames.Appearance);
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.Margin);
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.FillColor);
            SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#000000");
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
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

            var myDataObject = new Icon { Code = found ? iValue![0] : value };
            var myBinding = new Binding("Code");
            myBinding.Source = myDataObject;

            if (iValue!.Length > 0)
                switch (iValue[1])
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

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return e.Source.Equals(_materialIcon) || e.OriginalSource.Equals(_awesomeIcon);
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (propertyName == PropertyNames.FillColor)
            {
                _materialIcon.Foreground = _awesomeIcon.Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;

                Content.Background = Brushes.Transparent;
            }
            else if ((propertyName is PropertyNames.Width or PropertyNames.Height) &&
                     value != SizeValue.Expand.ToString() && value != SizeValue.Auto.ToString())
            {
                var vd = Helper.ConvertToDouble(value);
                SelectedContent.Width = vd;
                SelectedContent.Height = vd;

                _materialIcon.Width = _materialIcon.Height = vd - 2;
                _awesomeIcon.Width = _awesomeIcon.Height = vd - 2;

                SetPropertyValue(GroupNames.Transform, PropertyNames.Width, value);
                SetPropertyValue(GroupNames.Transform, PropertyNames.Height, value);
            }
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
            if (propertyName == PropertyNames.FillColor.ToString())
            {
                _materialIcon.Foreground = _awesomeIcon.Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;

                Content.Background = Brushes.Transparent;
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
            }
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child)
        {
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return false;
        }

        protected override void Delete()
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
    }
}