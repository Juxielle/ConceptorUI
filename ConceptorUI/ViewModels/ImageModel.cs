using ConceptorUI.Models;
using ConceptorUI.Views.Component;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using ConceptorUI.Constants;


namespace ConceptorUi.ViewModels
{
    class ImageModel : Component
    {
        private readonly ImageBrush _child;
        
        public ImageModel()
        {
            _child = new ImageBrush();
            _child.Stretch = Stretch.Fill;
            Content.Child = new Border();
            Content.Background = _child;
            
            Name = ComponentList.Image;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 0;
            
            OnInitialize();
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
            
        }

        protected override void WhenFileLoaded(string value)
        {
            try
            {
                var path = ComponentHelper.ProjectPath + "/Medias/" + value;
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                _child.ImageSource = bitmap;
            }
            catch (Exception)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(@"pack://application:,,,/Assets/image.png", UriKind.Absolute);
                bitmap.EndInit();
                _child.ImageSource = bitmap;
            }
        }

        protected override void SelfConstraints()
        {
            /* Global */
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "40");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "40");
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch);
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Padding, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.BorderWidth, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.FillColor, false);
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
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
            return true;
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
            
        }
    }
}
