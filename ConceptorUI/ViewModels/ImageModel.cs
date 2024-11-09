using ConceptorUI.Models;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ConceptorUI.Enums;
using ConceptorUI.Utils;


namespace ConceptorUi.ViewModels
{
    class ImageModel : Component
    {
        private readonly ImageBrush _child;

        public ImageModel(bool allowConstraints = false)
        {
            OnInit();

            _child = new ImageBrush
            {
                Stretch = Stretch.Fill
            };
            Content.Child = new Border { Background = _child };

            Name = ComponentList.Image;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 0;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
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
                bitmap.UriSource = new Uri("pack://application:,,,/Assets/image.png", UriKind.Absolute);
                bitmap.EndInit();
                _child.ImageSource = bitmap;
            }
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker);
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
            SetGroupVisibility(GroupNames.Appearance, false);
            SetGroupOnlyVisibility(GroupNames.Appearance);
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.Margin);
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.BorderRadius);
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        protected override bool CanSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override bool CanChildSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void RestoreProperties()
        {
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (groupName == GroupNames.Appearance)
            {
                if (propertyName == PropertyNames.BorderRadius)
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(vd,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight,
                        Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomLeft)
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight,
                        vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        vd,
                        Content.CornerRadius.BottomRight,
                        Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomRight)
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
                        vd,
                        Content.CornerRadius.BottomLeft);
                }
            }
            else if (groupName == GroupNames.Transform)
            {
                if (propertyName == PropertyNames.Stretch)
                {
                    _child.Stretch = value == ImageStretch.Fill.ToString() ? Stretch.Fill :
                        value == ImageStretch.Uniform.ToString() ? Stretch.Uniform :
                        value == ImageStretch.UniformToFill.ToString() ? Stretch.UniformToFill : Stretch.None;
                }
            }
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
            if (groupName == GroupNames.Appearance.ToString())
            {
                if (propertyName == PropertyNames.BorderRadius.ToString())
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopLeft.ToString())
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(vd,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight,
                        Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomLeft.ToString())
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
                        Content.CornerRadius.BottomRight,
                        vd);
                }
                else if (propertyName == PropertyNames.BorderRadiusTopRight.ToString())
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        vd,
                        Content.CornerRadius.BottomRight,
                        Content.CornerRadius.BottomLeft);
                }
                else if (propertyName == PropertyNames.BorderRadiusBottomRight.ToString())
                {
                    var vd = Helper.ConvertToDouble(value);
                    (Content.Child as Border)!.CornerRadius = new CornerRadius(Content.CornerRadius.TopLeft,
                        Content.CornerRadius.TopRight,
                        vd,
                        Content.CornerRadius.BottomLeft);
                }
            }
            else if (groupName == GroupNames.Transform.ToString())
            {
                if (propertyName == PropertyNames.Stretch.ToString())
                {
                    _child.Stretch = value == ImageStretch.Fill.ToString() ? Stretch.Fill :
                        value == ImageStretch.Uniform.ToString() ? Stretch.Uniform :
                        value == ImageStretch.UniformToFill.ToString() ? Stretch.UniformToFill : Stretch.None;
                }
            }
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

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        protected override void Delete(int k = -1)
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