using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Enums;
using UiDesigner.Models;
using UiDesigner.Services;
using UiDesigner.Utils;
using UiDesigner.ViewModels.Image;

namespace ConceptorUI.ViewModels.Image
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

            SelfConstraints();
            if (allowConstraints) return;
            OnInitialize();
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void WhenFileLoaded(string value)
        {
            _child.ImageSource =
                ReadImageFromZipService.GetImage(ComponentHelper.ProjectPath!, ComponentHelper.ProjectName!,
                    ComponentHelper.ProjectTempId!, value);
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "40");
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "40");
            this.SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch);
            /* Text */
            this.SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            this.SetGroupVisibility(GroupNames.Appearance, false);
            this.SetGroupOnlyVisibility(GroupNames.Appearance);
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.Margin);
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.BorderRadius);
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow, false);
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        protected override List<GroupProperties> GetPropertyGroups()
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
            ImageRestoreProperties.RestoreProperties(this);
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

        public override void WhenAlignmentChanged(PropertyNames propertyName, string value)
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

        public override bool AllowAuto(bool isWidth = true)
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

        protected override void CheckVisibilities()
        {
            this.SetVisibilities();
        }
    }
}