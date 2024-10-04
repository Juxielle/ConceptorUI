using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using ConceptorUI.Utils;


namespace ConceptorUi.ViewModels
{
    internal class TextSingleModel : Component
    {
        private readonly TextBlock _text;
        
        public TextSingleModel(bool allowConstraints = false)
        {
            OnInit();

            _text = new TextBlock();
            
            Content.Child = _text;

            Name = ComponentList.TextSingle;
            HasChildren = false;
            CanAddIntoChildContent = false;
            ChildContentLimit = 0;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
            if (propertyName == PropertyNames.FontFamily.ToString())
            {
                _text.FontFamily = ManageEnums.Instance.GetFontFamily(value);
            }
            else if (propertyName == PropertyNames.FontWeight.ToString())
            {
                _text.FontWeight = value == "0" ? FontWeights.Normal : FontWeights.Bold;
            }
            else if (propertyName == PropertyNames.FontStyle.ToString())
            {
                _text.FontStyle = value == "0" ? FontStyles.Normal : FontStyles.Italic;
            }
            else if (propertyName == PropertyNames.FontSize.ToString())
            {
                var vd = Helper.ConvertToDouble(value);
                vd = vd == 0 ? 10 : vd;
                _text.FontSize = vd;
            }
            else if (propertyName == PropertyNames.TextUnderline.ToString())
            {
                _text.TextDecorations = value == "1" ? TextDecorations.Underline : null;
            }
            else if (propertyName == PropertyNames.TextOverline.ToString())
            {
                _text.TextDecorations = value == "1" ? TextDecorations.OverLine : null;
            }
            else if (propertyName == PropertyNames.TextThrough.ToString())
            {
                _text.TextDecorations = value == "1" ? TextDecorations.Strikethrough : null;
            }
            else if (propertyName == PropertyNames.Color.ToString())
            {
                _text.Foreground = value == ColorValue.Transparent.ToString()
                    ? Brushes.Transparent
                    : new BrushConverter().ConvertFrom(value) as SolidColorBrush;
            }
            else if (propertyName == PropertyNames.Text.ToString())
            {
                _text.Text = value;
            }
        }

        public sealed override void SelfConstraints()
        {
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
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