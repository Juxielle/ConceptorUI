using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    internal class TextSingleModel : Component
    {
        public TextSingleModel()
        {
            ChildContent = new TextBlock();
            
            Name = ComponentList.TextSingle;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 0;
            
            OnInitialize();
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
            var text = ChildContent as TextBlock;
            if (propertyName == PropertyNames.FontFamily.ToString())
            {
                text!.FontFamily = ManageEnums.Instance.GetFontFamily(value);
            }
            else if (propertyName == PropertyNames.FontWeight.ToString())
            {
                text!.FontWeight = value == "0" ? FontWeights.Normal : FontWeights.Bold;
            }
            else if (propertyName == PropertyNames.FontStyle.ToString())
            {
                text!.FontStyle = value == "0" ? FontStyles.Normal : FontStyles.Italic;
            }
            else if (propertyName == PropertyNames.FontSize.ToString())
            {
                var vd = Helper.ConvertToDouble(value);
                vd = vd == 0 ? 10 : vd;
                text!.FontSize = vd;
            }
            else if (propertyName == PropertyNames.AlignLeft.ToString())
            {
                text!.TextAlignment = TextAlignment.Left;
            }
            else if (propertyName == PropertyNames.AlignCenter.ToString())
            {
                text!.TextAlignment = TextAlignment.Center;
            }
            else if (propertyName == PropertyNames.AlignRight.ToString())
            {
                text!.TextAlignment = TextAlignment.Right;
            }
            else if (propertyName == PropertyNames.AlignJustify.ToString())
            {
                text!.TextAlignment = TextAlignment.Justify;
            }
            else if (propertyName == PropertyNames.TextUnderline.ToString())
            {
                text!.TextDecorations = value == "1" ? TextDecorations.Underline : null;
            }
            else if (propertyName == PropertyNames.TextOverline.ToString())
            {
                text!.TextDecorations = value == "1" ? TextDecorations.OverLine : null;
            }
            else if (propertyName == PropertyNames.TextThrough.ToString())
            {
                text!.TextDecorations = value == "1" ? TextDecorations.Strikethrough : null;
            }
            else if (propertyName == PropertyNames.Color.ToString())
            {
                text!.Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;
            }
            else if (propertyName == PropertyNames.Text.ToString())
            {
                text!.Text = value;
            }
            else if (propertyName == PropertyNames.TextWrap.ToString())
            {
                text!.TextWrapping = value == "0" ? TextWrapping.NoWrap : TextWrapping.Wrap;
            }
            else if (propertyName == PropertyNames.LineSpacing.ToString())
            {
                var vd = Helper.ConvertToDouble(value);
                vd = vd == 0 ? 1 : vd;
                text!.LineHeight = vd;
            }
        }

        protected override void SelfConstraints()
        {
            /* Global */
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            /* Transform */
            /* Text */
            SetGroupVisibility(GroupNames.Text);
            /* Appearance */
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Padding, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.BorderWidth, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.BorderRadius, false);
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
            return isWidth;
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

        protected override void WhenFileLoaded(string value)
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
