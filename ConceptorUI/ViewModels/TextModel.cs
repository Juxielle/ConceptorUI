using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Models;
using ConceptorUI.Utils;

namespace ConceptorUi.ViewModels
{
    internal class TextModel : Component
    {
        private readonly TextBlock _text;
        private List<TextBlock> _textBlocks;

        public TextModel(bool allowConstraints = false)
        {
            OnInit();

            _textBlocks = [];
            _text = new TextBlock();
            _text.SizeChanged -= OnTextSizeChanged;
            _text.SizeChanged += OnTextSizeChanged;

            Content.Child = _text;
            Name = ComponentList.Text;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
            AddFirstChild();
        }

        private void OnTextSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var control = sender as TextBlock;

            SelectedContent.Height = control!.ActualHeight;

            var width = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if (width != SizeValue.Expand.ToString())
                SelectedContent.Width = control.ActualWidth;
        }

        public override void WhenTextChanged(string propertyName, string value)
        {
            if (propertyName == PropertyNames.AlignLeft.ToString() && value == "1")
            {
                _text.TextAlignment = TextAlignment.Left;
            }
            else if (propertyName == PropertyNames.AlignCenter.ToString() && value == "1")
            {
                _text.TextAlignment = TextAlignment.Center;
            }
            else if (propertyName == PropertyNames.AlignRight.ToString() && value == "1")
            {
                _text.TextAlignment = TextAlignment.Right;
            }
            else if (propertyName == PropertyNames.AlignJustify.ToString() && value == "1")
            {
                _text.TextAlignment = TextAlignment.Justify;
            }
            else if (propertyName == PropertyNames.TextWrap.ToString())
            {
                _text.TextWrapping = value == "0" ? TextWrapping.NoWrap : TextWrapping.Wrap;
            }
            else if (propertyName == PropertyNames.LineSpacing.ToString())
            {
                var vd = Helper.ConvertToDouble(value);
                vd = vd == 0 ? 1 : vd;
                _text.LineHeight = vd;
            }
            else
            {
                for (var i = 0; i < Children.Count; i++)
                {
                    Children[i].WhenTextChanged(propertyName, value);
                    var textSource = GetSource(Children[i].ComponentView);
                    var textTarget = _textBlocks[i];
                    
                    WhenTextChangedOwn(textSource, textTarget);
                }
            }
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.VT, "1");
            /* Transform */
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Height, false);
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.VE, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.HE);
            /* Text */
            SetGroupVisibility(GroupNames.Text);
            SetPropertyValue(GroupNames.Text, PropertyNames.Color, "#000000");
            /* Appearance */
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.Padding, false);
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.BorderWidth, false);
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.BorderRadius, false);
            SetPropertyVisibility(GroupNames.Appearance, PropertyNames.FillColor, false);
            SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString());
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
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

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            try
            {
                var textSource = GetSource(child);
                var textTarget = new TextBlock();

                WhenTextChangedOwn(textSource, textTarget);
                _text.Inlines.Add(textTarget);
                _textBlocks.Add(textTarget);
            }
            catch (Exception)
            {
                //
            }
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return isWidth;
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

        private void AddFirstChild()
        {
            var textComponent = new TextSingleModel();
            Children.Add(textComponent);
            AddIntoChildContent(textComponent.ComponentView);
        }

        private TextBlock GetSource(FrameworkElement element)
        {
            return ((((element as Border)!.Child as Grid)!.Children[1] as Border)!.Child as TextBlock)!;
        }

        private static void WhenTextChangedOwn(TextBlock textSource, TextBlock textTarget)
        {
            textTarget.FontFamily = textSource.FontFamily;
            textTarget.FontWeight = textSource.FontWeight;
            textTarget.FontStyle = textSource.FontStyle;
            textTarget.FontSize = textSource.FontSize;
            textTarget.Foreground = textSource.Foreground;
            textTarget.Text = textSource.Text;
            textTarget.TextDecorations = textSource.TextDecorations;
        }
    }
}