using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Senders;
using ConceptorUI.Services;
using ConceptorUI.Views.Modals;
using UiDesigner.Inputs;
using UiDesigner.Models;
using UiDesigner.Views.Modals;

namespace ConceptorUI.Views.Component
{
    public partial class TextProperty
    {
        private int _firstCount;
        private GroupProperties _properties;
        private bool _allowSetField;

        public ICommand? MouseDownCommand;
        public ICommand? PickColorCommand { get; }

        public TextProperty()
        {
            _allowSetField = false;
            _firstCount = 0;
            InitializeComponent();

            _properties = new GroupProperties();

            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                CFontFamily.Items.Add(
                    new ComboBoxItem { Content = fontFamily.Source }
                );
            }

            PickColorCommand = new RelayCommand(OnPickColor);
        }

        public void FeedProps(object value)
        {
            SFontFamily.Visibility = SFontWeight.Visibility = BFontStyle.Visibility = SColor.Visibility =
                SFontSize.Visibility = BListOrd.Visibility = BTextTrimming.Visibility =
                    BTabRight.Visibility = SLineSpacing.Visibility = Visibility.Collapsed;
            _properties = (value as GroupProperties)!;
            _allowSetField = false;

            #region

            foreach (var prop in _properties.Properties.Where(prop =>
                         prop.Visibility == VisibilityValue.Visible.ToString()))
            {
                if (prop.Name == PropertyNames.FontFamily.ToString())
                {
                    SFontFamily.Visibility = Visibility.Visible;
                    CFontFamily.SelectedIndex = Convert.ToInt32(ManageEnums.GetFfIndex(prop.Value));
                }
                else if (prop.Name == PropertyNames.FontWeight.ToString())
                {
                    SFontWeight.Visibility = Visibility.Visible;
                    CFontWeight.SelectedIndex = Convert.ToInt32(prop.Value);
                    FontWeight.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.FontStyle.ToString())
                {
                    BFontStyle.Visibility = Visibility.Visible;
                    FontStyle.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.Color.ToString())
                {
                    CColor.IsChecked = prop.Value != ColorValue.Transparent.ToString();

                    SColor.Visibility = Visibility.Visible;
                    BColor.Color = (prop.Value == ColorValue.Transparent.ToString()
                        ? Brushes.Transparent
                        : new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush)!;
                }
                else if (prop.Name == PropertyNames.FontSize.ToString())
                {
                    SFontSize.Visibility = Visibility.Visible;
                    TFontSize.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.AlignLeft.ToString())
                {
                    BAlignLeft.Visibility = Visibility.Visible;
                    AlignLeft.Foreground = BAlignLeft.BorderBrush =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.AlignCenter.ToString())
                {
                    BAlignCenter.Visibility = Visibility.Visible;
                    AlignCenter.Foreground = BAlignCenter.BorderBrush =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.AlignRight.ToString())
                {
                    BAlignRight.Visibility = Visibility.Visible;
                    AlignRight.Foreground = BAlignRight.BorderBrush =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.AlignJustify.ToString())
                {
                    BAlignJustify.Visibility = Visibility.Visible;
                    AlignJustify.Foreground = BAlignJustify.BorderBrush =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.ListOrd.ToString())
                {
                    BListOrd.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.TabLeft.ToString())
                {
                    BTabLeft.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.TabRight.ToString())
                {
                    BTabRight.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.TextUnderline.ToString())
                {
                    BTextUnderline.Visibility = Visibility.Visible;
                    TextUnderline.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.TextOverline.ToString())
                {
                    BTextOverline.Visibility = Visibility.Visible;
                    TextOverline.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.TextThrough.ToString())
                {
                    BTextThrough.Visibility = Visibility.Visible;
                    TextThrough.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.TextError.ToString())
                {
                    BTextError.Visibility = Visibility.Visible;
                    TextError.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.TextIndex.ToString())
                {
                    BTextIndex.Visibility = Visibility.Visible;
                    TextIndex.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.TextExpo.ToString())
                {
                    BTextExpo.Visibility = Visibility.Visible;
                    TextExpo.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.TextWrap.ToString())
                {
                    BTextWrap.Visibility = Visibility.Visible;
                    TextWrap.Foreground = BTextWrap.BorderBrush =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.TextTrimming.ToString())
                {
                    BTextTrimming.Visibility = Visibility.Visible;
                    TextTrimming.Foreground = BTextTrimming.BorderBrush =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.LineSpacing.ToString())
                {
                    SLineSpacing.Visibility = Visibility.Visible;
                    TLineSpacing.Text = prop.Value.Replace(",", ".");
                }
            }

            #endregion

            _allowSetField = true;
        }

        private void OnChanged(object sender, EventArgs e)
        {
            if (!_allowSetField) return;

            var textBox = (sender as TextBox)!;
            var tag = textBox.Tag != null ? textBox.Tag.ToString()! : "";
            var propertyName = PropertyNames.None;

            switch (tag)
            {
                case "FontSize":
                    propertyName = PropertyNames.FontSize;
                    break;
                case "LineSpacing":
                    propertyName = PropertyNames.LineSpacing;
                    break;
            }

            if (propertyName == PropertyNames.None) return;

            textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
            var value = textBox.Text == "" ? "0" : textBox.Text;

            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Text, propertyName, value[^1] == '.' ? value[..^1] : value }
            );
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_allowSetField) return;

            var comboBox = (sender as ComboBox)!;
            var tag = comboBox.Tag != null ? comboBox.Tag.ToString()! : "";
            var propertyName = PropertyNames.None;
            string value = null!;

            switch (tag)
            {
                case "FontFamily":
                    propertyName = PropertyNames.FontFamily;
                    value = (comboBox.SelectedValue as ComboBoxItem) != null
                        ? (comboBox.SelectedValue as ComboBoxItem)!.Content.ToString()!
                        : null!;
                    break;
                case "FontWeight":
                    propertyName = PropertyNames.FontWeight;
                    value = comboBox.SelectedIndex.ToString();
                    break;
            }

            if (propertyName == PropertyNames.None) return;

            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Text, propertyName, value }
            );
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            int idP = -1, index = 1;
            var btnName = 'B' + tag;
            var propertyName = PropertyNames.None;

            switch (tag)
            {
                case "AlignLeft":
                    idP = 4;
                    propertyName = PropertyNames.AlignLeft;
                    break;
                case "AlignCenter":
                    idP = 5;
                    propertyName = PropertyNames.AlignCenter;
                    break;
                case "AlignRight":
                    idP = 6;
                    propertyName = PropertyNames.AlignRight;
                    break;
                case "AlignJustify":
                    idP = 7;
                    propertyName = PropertyNames.AlignJustify;
                    break;
                case "ListOrd":
                    idP = 8;
                    propertyName = PropertyNames.ListOrd;
                    break;
                case "ListNOrd":
                    idP = 9;
                    propertyName = PropertyNames.ListNOrd;
                    break;
                case "TabLeft":
                    idP = 10;
                    propertyName = PropertyNames.TabLeft;
                    break;
                case "TabRight":
                    idP = 11;
                    propertyName = PropertyNames.TabRight;
                    break;
                case "Color":
                    idP = 18;
                    propertyName = PropertyNames.Color;
                    if (CColor.IsChecked == true)
                    {
                        var colorPicker = new ColorPicker(BColor.Color, 1)
                        {
                            ColorSelectedCommand = new RelayCommand((color) =>
                            {
                                MouseDownCommand?.Execute(
                                    new dynamic[] { GroupNames.Text, PropertyNames.Color, color.ToString()! }
                                );

                                BColor.Color = (new BrushConverter().ConvertFrom(color.ToString()!) as SolidColorBrush)!;
                            })
                        };
                        colorPicker.Show();
                    }

                    break;
                case "DisplayText":
                    //MainWindow.Instance.DisplayTextPage(true);
                    break;
                case "TextWrap":
                    idP = 21;
                    propertyName = PropertyNames.TextWrap;
                    index = _properties.GetValue(PropertyNames.TextWrap) == "0" ? 1 : 0;
                    break;
                case "TextTrimming":
                    idP = 22;
                    propertyName = PropertyNames.TextTrimming;
                    index = _properties.GetValue(PropertyNames.TextTrimming) == "0" ? 1 : 0;
                    break;
                case "UpValueF":
                    TFontSize.Text = ManageEnums.SetNumber(TFontSize.Text).Replace(",", ".");
                    break;
                case "DownValueF":
                    TFontSize.Text = ManageEnums.SetNumber(TFontSize.Text, false).Replace(",", ".");
                    break;
            }

            if (idP is -1 or 18) return;
            LoadValue(idP, index + "", btnName);
            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Text, propertyName, index + "" }
            );
        }

        private void BtnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as Border)!.Tag.ToString()!;
            int index = 0, idP = -1;
            var btnName = 'B' + tag;
            var propertyName = PropertyNames.None;

            switch (tag)
            {
                case "FontWeight":
                    idP = 1;
                    propertyName = PropertyNames.FontWeight;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
                case "FontStyle":
                    idP = 2;
                    propertyName = PropertyNames.FontStyle;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
                case "TextOverline":
                    idP = 13;
                    propertyName = PropertyNames.TextOverline;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
                case "TextUnderline":
                    idP = 12;
                    propertyName = PropertyNames.TextUnderline;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
                case "TextThrough":
                    idP = 14;
                    propertyName = PropertyNames.TextThrough;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
                case "TextError":
                    idP = 15;
                    propertyName = PropertyNames.TextError;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
                case "TextIndex":
                    idP = 16;
                    propertyName = PropertyNames.TextIndex;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
                case "TextExpo":
                    idP = 17;
                    propertyName = PropertyNames.TextExpo;
                    index = _properties.GetValue(propertyName) == "0" ? 1 : 0;
                    break;
            }

            if (propertyName == PropertyNames.None) return;

            LoadValue(idP, index + "", btnName);
            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Text, propertyName, index + "" }
            );
        }

        private void OnPickColor(object sender)
        {
            var propertyName = PropertyNames.None;
            if(sender is not PropertyActionSender propertyActionSender) return;
            
            if (propertyActionSender.Name == "Color")
                propertyName = PropertyNames.Color;
            
            if(propertyName == PropertyNames.None) return;
            MouseDownCommand?.Execute(
                new dynamic[]
                {
                    GroupNames.Text,
                    propertyName,
                    propertyActionSender.Value
                }
            );
        }

        private void OnColorChecked(object sender, RoutedEventArgs e)
        {
            if (!_allowSetField) return;

            var cb = (sender as CheckBox)!;
            if (cb.IsChecked != false) return;
            BColor.Color = Brushes.Transparent;

            MouseDownCommand?.Execute(
                new dynamic[]
                {
                    GroupNames.Text, PropertyNames.Color, ColorValue.Transparent.ToString()
                }
            );
        }

        private void LoadValue(int idP, string value, string btnName)
        {
            var color = value == "0" ? "#8c8c8a" : "#6739b7";

            if (btnName is "BAlignLeft" or "BAlignCenter" or "BAlignRight" or "BAlignJustify")
            {
                AlignLeft.Foreground = BAlignLeft.BorderBrush =
                    new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                AlignCenter.Foreground = BAlignCenter.BorderBrush =
                    new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                AlignRight.Foreground = BAlignRight.BorderBrush =
                    new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                AlignJustify.Foreground = BAlignJustify.BorderBrush =
                    new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                switch (idP)
                {
                    case 4:
                        AlignLeft.Foreground = BAlignLeft.BorderBrush =
                            new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                    case 5:
                        AlignCenter.Foreground = BAlignCenter.BorderBrush =
                            new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                    case 6:
                        AlignRight.Foreground = BAlignRight.BorderBrush =
                            new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                    case 7:
                        AlignJustify.Foreground = BAlignJustify.BorderBrush =
                            new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                }
            }
            else if (btnName == "BFontWeight")
            {
                FontWeight.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if (btnName == "BFontStyle")
            {
                FontStyle.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if (btnName == "BTextUnderline" || btnName == "BTextOverline" || btnName == "BTextThrough")
            {
                TextUnderline.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                TextOverline.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                TextThrough.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                switch (idP)
                {
                    case 12:
                        TextUnderline.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
                        break;
                    case 13:
                        TextOverline.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
                        break;
                    case 14:
                        TextThrough.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
                        break;
                }
            }
            else if (btnName == "BTextError")
            {
                TextError.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if (btnName == "BTextIndex")
            {
                TextIndex.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if (btnName == "BTextExpo")
            {
                TextExpo.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if (btnName == "BTextWrap")
            {
                TextWrap.Foreground =
                    BTextWrap.BorderBrush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if (btnName == "BTextTrimming")
            {
                TextTrimming.Foreground =
                    BTextTrimming.BorderBrush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
        }

        private void OnSettingClick(object sender, RoutedEventArgs e)
        {
            var componentSetting = PropertiesConfigService.GetConfigs(_properties);
            ComponentPropertyConfig.Instance.Refresh(componentSetting, "TEXT PROPERTIES", GroupNames.Text);
        }
    }
}