using ConceptorUI.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour TextProperty.xaml
    /// </summary>
    public partial class TextProperty
    {
        private static TextProperty _obj;
        private int _firstCount;

        public TextProperty()
        {
            _firstCount = 0;
            InitializeComponent();
            _obj = this;

            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                CFontFamily.Items.Add(
                    new ComboBoxItem { Content = fontFamily.Source }
                );
            }
        }

        public static TextProperty Instance => _obj == null! ? new TextProperty() : _obj;

        public void FeedProps()
        {
            SFontFamily.Visibility = SFontWeight.Visibility = BFontStyle.Visibility = SColor.Visibility =
            SFontSize.Visibility = BListOrd.Visibility = BListNOrd.Visibility = BTabRight.Visibility = SLineSpacing.Visibility = Visibility.Collapsed;

            var pos = Properties.GetPosition(GroupNames.Text, PropertyNames.FontFamily);
            #region
            foreach (var prop in Properties.groupProps![pos[0]].Properties)
            {
                if (prop.Visibility == VisibilityValue.Visible.ToString())
                {
                    if (prop.Name == PropertyNames.FontFamily.ToString())
                    {
                        SFontFamily.Visibility = Visibility.Visible;
                        CFontFamily.SelectedIndex = Convert.ToInt32(ManageEnums.Instance.GetFFIndex(prop.Value));
                    }
                    else if (prop.Name == PropertyNames.FontWeight.ToString())
                    {
                        SFontWeight.Visibility = Visibility.Visible; CFontWeight.SelectedIndex = Convert.ToInt32(prop.Value);
                        FontWeight.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.FontStyle.ToString())
                    {
                        BFontStyle.Visibility = Visibility.Visible;
                        FontStyle.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.Color.ToString())
                    {
                        if (prop.Value == ColorValue.Transparent.ToString()) CColor.IsChecked = false;
                        else CColor.IsChecked = true;
                        SColor.Visibility = Visibility.Visible;
                        BColor.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                          new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.FontSize.ToString())
                    {
                        SFontSize.Visibility = Visibility.Visible;
                        TFontSize.Text = prop.Value.Replace(",", ".");
                    }
                    else if (prop.Name == PropertyNames.AlignLeft.ToString())
                    {
                        BAlignLeft.Visibility = Visibility.Visible;
                        AlignLeft.Foreground = BAlignLeft.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.AlignCenter.ToString())
                    {
                        BAlignCenter.Visibility = Visibility.Visible;
                        AlignCenter.Foreground = BAlignCenter.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.AlignRight.ToString())
                    {
                        BAlignRight.Visibility = Visibility.Visible;
                        AlignRight.Foreground = BAlignRight.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.AlignJustify.ToString())
                    {
                        BAlignJustify.Visibility = Visibility.Visible;
                        AlignJustify.Foreground = BAlignJustify.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.ListOrd.ToString())
                    {
                        BListOrd.Visibility = Visibility.Visible;
                    }
                    else if (prop.Name == PropertyNames.ListNOrd.ToString())
                    {
                        BListNOrd.Visibility = Visibility.Visible;
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
                        TextUnderline.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.TextOverline.ToString())
                    {
                        BTextOverline.Visibility = Visibility.Visible;
                        TextOverline.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.TextThrough.ToString())
                    {
                        BTextThrough.Visibility = Visibility.Visible;
                        TextThrough.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.TextError.ToString())
                    {
                        BTextError.Visibility = Visibility.Visible;
                        TextError.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.TextIndex.ToString())
                    {
                        BTextIndex.Visibility = Visibility.Visible;
                        TextIndex.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.TextExpo.ToString())
                    {
                        BTextExpo.Visibility = Visibility.Visible;
                        TextExpo.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.TextWrap.ToString())
                    {
                        BTextWrap.Visibility = Visibility.Visible;
                        TextWrap.Foreground = BTextWrap.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.LineSpacing.ToString())
                    {
                        SLineSpacing.Visibility = Visibility.Visible;
                        TLineSpacing.Text = prop.Value.Replace(",", ".");
                    }
                }
            }
            #endregion
        }

        private void OnChanged(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox)!;
            var tag = textBox.Tag != null ? textBox.Tag.ToString()! : "";
            var idP = PropertyNames.None;
            switch (tag)
            {
                case "FontSize":
                    idP = PropertyNames.FontSize;
                    break;
                case "LineSpacing":
                    idP = PropertyNames.LineSpacing;
                    break;
            }
            if (idP != PropertyNames.None)
            {
                textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
                var value = textBox.Text == "" ? "0" : textBox.Text;
                PanelProperty.Instance.ReactToProps(GroupNames.Text, idP, value[value.Length - 1] == '.' ? value.Substring(0, value.Length - 1) : value);
            }
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (sender as ComboBox)!;
            var tag = comboBox.Tag != null ? comboBox.Tag.ToString()!: "";
            var idP = PropertyNames.None;
            string value = null!;
            switch (tag)
            {
                case "FontFamily": 
                    idP = PropertyNames.FontFamily;
                    value = (comboBox.SelectedValue as ComboBoxItem) != null ? (comboBox.SelectedValue as ComboBoxItem)!.Content.ToString()! : null!;
                    break;
                case "FontWeight": 
                    idP = PropertyNames.FontWeight;
                    value = comboBox.SelectedIndex.ToString()!;
                    break;
            }
            if (idP != PropertyNames.None)
            {
                if(_firstCount > 1 && value != null) PanelProperty.Instance.ReactToProps(GroupNames.Text, idP, value);
                if (_firstCount < 2)  _firstCount++;
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            int idP = -1, index = 1;
            var btnName = 'B' + tag;
            var idPN = PropertyNames.None;

            var pos = Properties.GetPosition(GroupNames.Text, PropertyNames.FillColor);
            var idG = pos[0];
            switch (tag)
            {
                case "AlignLeft": idP = 4; idPN = PropertyNames.AlignLeft; break;
                case "AlignCenter": idP = 5; idPN = PropertyNames.AlignCenter; break;
                case "AlignRight": idP = 6; idPN = PropertyNames.AlignRight; break;
                case "AlignJustify": idP = 7; idPN = PropertyNames.AlignJustify; break;
                case "ListOrd": idP = 8; idPN = PropertyNames.ListOrd; break;
                case "ListNOrd": idP = 9; idPN = PropertyNames.ListNOrd; break;
                case "TabLeft": idP = 10; idPN = PropertyNames.TabLeft; break;
                case "TabRight": idP = 11; idPN = PropertyNames.TabRight; break;
                case "Color": idP = 18; idPN = PropertyNames.Color;
                    if (CColor.IsChecked == true)
                    {
                        MainWindow.Instance.DisplayColorPalette(BColor.Background, !ColorPalette.Instance.IsOpened, tag);
                    }
                    break;
                case "DisplayText":
                    MainWindow.Instance.DisplayTextPage(true); break;
                case "TextWrap": idP = 21; idPN = PropertyNames.TextWrap;
                    index = Properties.groupProps![idG].Properties[21].Value == "0" ? 1 : 0; break;
                case "UpValueF": TFontSize.Text = ManageEnums.SetNumber(TFontSize.Text).Replace(",", "."); break;
                case "DownValueF": TFontSize.Text = ManageEnums.SetNumber(TFontSize.Text, false).Replace(",", "."); break;
            }
            if (idP != -1 && idP != 18)
            {
                PanelProperty.Instance.ReactToProps(GroupNames.Text, idPN, index + "");
                LoadValue(idP, index + "", btnName);
            }
        }

        private void BtnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as Border)!.Tag.ToString()!;
            int index = 0, idP = -1;
            var btnName = 'B' + tag;
            var idPN = PropertyNames.None;

            var pos = Properties.GetPosition(GroupNames.Text, PropertyNames.FillColor);
            var idG = pos[0];
            switch (tag)
            {
                case "FontWeight": idP = 1; idPN = PropertyNames.FontWeight; index = Properties.groupProps![idG].Properties[1].Value == "0" ? 1 : 0; break;
                case "FontStyle": idP = 2; idPN = PropertyNames.FontStyle; index = Properties.groupProps![idG].Properties[2].Value == "0" ? 1 : 0; break;
                case "TextOverline": idP = 13; idPN = PropertyNames.TextOverline; index = Properties.groupProps![idG].Properties[13].Value == "0" ? 1 : 0; break;
                case "TextUnderline": idP = 12; idPN = PropertyNames.TextUnderline; index = Properties.groupProps![idG].Properties[12].Value == "0" ? 1 : 0; break;
                case "TextThrough": idP = 14; idPN = PropertyNames.TextThrough; index = Properties.groupProps![idG].Properties[14].Value == "0" ? 1 : 0; break;
                case "TextError": idP = 15; idPN = PropertyNames.TextError; index = Properties.groupProps![idG].Properties[15].Value == "0" ? 1 : 0; break;
                case "TextIndex": idP = 16; idPN = PropertyNames.TextIndex; index = Properties.groupProps![idG].Properties[16].Value == "0" ? 1 : 0; break;
                case "TextExpo": idP = 17; idPN = PropertyNames.TextExpo; index = Properties.groupProps![idG].Properties[17].Value == "0" ? 1 : 0; break;
            }
            if (idPN != PropertyNames.None)
            {
                PanelProperty.Instance.ReactToProps(GroupNames.Text, idPN, index + "");
                LoadValue(idP, index + "", btnName);

            }
        }

        private void OnColorChecked(object sender, RoutedEventArgs e)
        {
            var cb = (sender as CheckBox)!;
            if (cb.IsChecked == false)
            {
                BColor.Background = Brushes.Transparent;
                PanelProperty.Instance.ReactToProps(GroupNames.Text, PropertyNames.Color, "Transparent");
            }
        }

        public void LoadValue(int idP, string value, string btnName)
        {
            var color = value == "0" ? "#8c8c8a" : "#6739b7";

            if (btnName == "BAlignLeft" || btnName == "BAlignCenter" || btnName == "BAlignRight" || btnName == "BAlignJustify")
            {
                AlignLeft.Foreground = BAlignLeft.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                AlignCenter.Foreground = BAlignCenter.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                AlignRight.Foreground = BAlignRight.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                AlignJustify.Foreground = BAlignJustify.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                switch (idP)
                {
                    case 4:
                        AlignLeft.Foreground = BAlignLeft.BorderBrush = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                    case 5:
                        AlignCenter.Foreground = BAlignCenter.BorderBrush = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                    case 6:
                        AlignRight.Foreground = BAlignRight.BorderBrush = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                    case 7:
                        AlignJustify.Foreground = BAlignJustify.BorderBrush = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                        break;
                }
            }
            else if (btnName == "BFontWeight")
            {
                FontWeight.Foreground = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if(btnName == "BFontStyle")
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
                TextExpo.Foreground  = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
            else if (btnName == "BTextWrap")
            {
                TextWrap.Foreground = BTextWrap.BorderBrush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
            }
        }

        public void SetColor(Brush color)
        {
            BColor.Background = color;
            PanelProperty.Instance.ReactToProps(GroupNames.Text, PropertyNames.Color, color.ToString());
        }
    }
}
