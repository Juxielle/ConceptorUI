using System;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using FontAwesome.WPF;


namespace ConceptorUi.ViewModels
{
    internal class IconModel : Component
    {
        private readonly Grid _grid;
        private readonly PackIcon _materialIcon;
        private readonly ImageAwesome _awesomeIcon;
        
        public IconModel()
        {
            _grid = new Grid();
            _materialIcon = new PackIcon();
            _awesomeIcon = new ImageAwesome{ Visibility = Visibility.Collapsed };

            _grid.Children.Add(_materialIcon);
            _grid.Children.Add(_awesomeIcon);
            Content.Child = _grid;
            
            Name = ComponentList.Icon;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 0;
            
            OnInitialize();
        }

        public override void SelfConstraints()
        {
            /* Global */
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "20");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "20");
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Padding, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.BorderWidth, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.BorderRadius, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.FillColor, false);
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
        }
        
        protected override void WhenFileLoaded(string value)
        {
            var found = false;
            var iValue = Array.Empty<string>();
            if (value.Length > 0 && value[0] == '[')
            {
                iValue = System.Text.Json.JsonSerializer.Deserialize<string[]>(value);
                found = true;
            }
            
            var myDataObject = new Icon{Code = found ? iValue![0] : value};
            var myBinding = new Binding("Code");
            myBinding.Source = myDataObject;

            if(iValue!.Length > 0)
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
