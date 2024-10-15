using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ConceptorUI.Models;
using ConceptorUI.Utils;

namespace ConceptorUi.ViewModels
{
    internal class TextModel : Component
    {
        private readonly TextBlock _text;
        private readonly List<Run> _runs;
        private bool _isEventCanHandled;

        public TextModel(bool allowConstraints = false)
        {
            OnInit();

            _runs = [];
            _text = new TextBlock();
            _text.SizeChanged -= OnTextSizeChanged;
            _text.SizeChanged += OnTextSizeChanged;
            _isEventCanHandled = false;

            Content.Child = _text;
            Name = ComponentList.Text;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
            //AddFirstChild();
        }

        private void OnTextSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_isEventCanHandled) return;
            var control = sender as TextBlock;

            var height = control!.ActualHeight;
            // foreach (var child in Children)
            // {
            //     var text = GetSource(child.ComponentView);
            //     if(height < text.ActualHeight)
            //         height = text.ActualHeight;
            //     Console.WriteLine($@"Text child height: {text.ActualHeight}");
            // }
            SelectedContent.Height = height;

            var width = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

            if (width != SizeValue.Expand.ToString())
                SelectedContent.Width = control.ActualWidth;
            _isEventCanHandled = false;
            Console.WriteLine($@"Text Height: {height}");
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
                _isEventCanHandled = true;
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
                // _isEventCanHandled = propertyName == PropertyNames.FontSize.ToString() ||
                //                      propertyName == PropertyNames.FontFamily.ToString() ||
                //                      propertyName == PropertyNames.FontWeight.ToString() ||
                //                      propertyName == PropertyNames.Text.ToString();
                _isEventCanHandled = true;

                var index = Convert.ToInt32(
                    GetGroupProperties(GroupNames.Text).GetValue(PropertyNames.CurrentTextIndex));
                if (index >= Children.Count) return;

                Children[index].WhenTextChanged(propertyName, value);
                var textSource = GetSource(Children[index].ComponentView);
                var textTarget = _runs[index];

                WhenTextChangedOwn(textSource, textTarget);
            }

            if (propertyName == PropertyNames.FontSize.ToString())
            {
                _isEventCanHandled = true;
                _text.FontSize = Helper.ConvertToDouble(value);
                SelectedContent.Height += 0.000000001;
            }
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vt, "1");
            /* Transform */
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Height, false);
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Ve, false);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.He);
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
                //if (Children.Count <= _runs.Count) return;

                var textSource = GetSource(child);
                var textTarget = new Run();
                
                WhenTextChangedOwn(textSource, textTarget);
                _text.Inlines.Add(textTarget);
                _runs.Add(textTarget);
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

        protected override object GetPropertyGroups()
        {
            var groups = new List<List<GroupProperties>>
            {
                PropertyGroups!
            };
            groups.AddRange(Children.Select(child => child.PropertyGroups!));

            return groups;
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
            _text.Text = string.Empty;
            var textComponent = new TextSingleModel();
            
            Children.Add(textComponent);
            AddIntoChildContent(textComponent.ComponentView);
        }

        private TextBlock GetSource(FrameworkElement element)
        {
            return ((((element as Border)!.Child as Grid)!.Children[2] as Border)!.Child as TextBlock)!;
        }

        private static void WhenTextChangedOwn(TextBlock textSource, Run textTarget)
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