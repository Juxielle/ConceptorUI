using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ConceptorUI.Enums;
using ConceptorUI.Senders;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;
using UiDesigner.Utils;

namespace ConceptorUI.ViewModels.Text
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
        }

        private void OnTextSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_isEventCanHandled) return;
            var control = sender as TextBlock;

            var height = control!.ActualHeight;
            SelectedContent.Height = height;

            var width = this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

            if (width != SizeValue.Expand.ToString())
                SelectedContent.Width = control.ActualWidth;
            _isEventCanHandled = false;
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
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
            else if (propertyName == PropertyNames.TextTrimming.ToString())
            {
                _text.TextTrimming = value == "0" ? TextTrimming.None : TextTrimming.CharacterEllipsis;
            }
            else if (!isInitialize)
            {
                _isEventCanHandled = true;

                var index = Convert.ToInt32(
                    this.GetGroupProperties(GroupNames.Text).GetValue(PropertyNames.CurrentTextIndex));
                if (index >= Children.Count) return;

                Children[index].WhenTextChanged(propertyName, value);
                var textSource = GetSource(Children[index].ComponentView);
                var textTarget = _runs[index];

                WhenTextChangedOwn(textSource, textTarget);
            }

            Refresh();
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            this.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Vt, "1");
            /* Transform */
            this.SetPropertyVisibility(GroupNames.Transform, PropertyNames.Height, false);
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            this.SetPropertyVisibility(GroupNames.Transform, PropertyNames.Ve, false);
            this.SetPropertyVisibility(GroupNames.Transform, PropertyNames.He);
            /* Text */
            this.SetGroupVisibility(GroupNames.Text);
            this.SetPropertyValue(GroupNames.Text, PropertyNames.Color, "#000000");
            /* Appearance */
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.Padding, false);
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.BorderWidth, false);
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.BorderRadius, false);
            this.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.FillColor, false);
            this.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString());
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow, false);
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

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
        }

        public override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        public override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            try
            {
                var textSource = GetSource(child);
                var textTarget = new Run();
                textTarget.PreviewMouseLeftButtonDown += OnTextItemMouseDown;

                WhenTextChangedOwn(textSource, textTarget);
                _text.Inlines.Add(textTarget);
                _runs.Add(textTarget);
            }
            catch (Exception)
            {
                //
            }
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return isWidth;
        }

        public override bool AllowAuto(bool isWidth = true)
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

        public override void Delete(int k = -1)
        {
            if (Children.Count == 1 || k > -1 && Children.Count >= k) return;
            var index = k != -1
                ? k
                : Convert.ToInt32(
                    this.GetGroupProperties(GroupNames.Text).GetValue(PropertyNames.CurrentTextIndex));

            Children.RemoveAt(index);
            _text.Inlines.Remove(_runs[index]);
            _runs.RemoveAt(index);

            Refresh();
            WhenTextChanged(PropertyNames.CurrentTextIndex.ToString(), "0");
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

        public void AddFirstChild()
        {
            _text.Text = string.Empty;
            var textComponent = new TextSingleModel();

            Children.Add(textComponent);
            AddIntoChildContent(textComponent.ComponentView);
        }

        private void Refresh()
        {
            var maxItemSize = _runs.Select(run => run.FontSize).Prepend(0.0).Max();
            if (maxItemSize <= 0) return;

            var textSize = _text.FontSize;
            if (maxItemSize < textSize)
                SelectedContent.Height = 0;

            _isEventCanHandled = true;
            _text.FontSize = maxItemSize;
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

        private void OnTextItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            var found = false;
            foreach (var inline in _text.Inlines)
            {
                var run = inline as Run;
                if(!e.OriginalSource.Equals(run)) continue;
                found = true;
                break;
            }
            if(!found) return;
            
            if (!ComponentHelper.IsMultiSelectionEnable)
            {
                SelectedCommand?.Execute(
                    new SelectComponentSender
                    {
                        Id = Id!,
                        SelectComponentAction = SelectComponentActions.Unselect,
                        PropertyGroups = GetPropertyGroups(),
                        ComponentName = Name
                    }
                );
            }

            OnSelected(e.ClickCount);
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
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
        }

        protected override void CheckVisibilities()
        {
        }
    }
}