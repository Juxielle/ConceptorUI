using ConceptorUI.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Interfaces;
using ConceptorUI.Utils;


namespace ConceptorUI.Views.ComponentP
{
    public partial class AppearanceProperty : IAppearance
    {
        private static AppearanceProperty? _obj;
        private int _firstCount;
        private int _firstCount2;
        private readonly int _firstCount3;
        private const string ColorOn = "#6739b7";
        private const string ColorOff = "#8c8c8a";
        
        private GroupProperties _properties;

        private double _opacity;
        
        public event EventHandler? PreMouseDownEvent;
        private readonly object _mouseDownLock = new();
        
        public event EventHandler? PreTextChangeEvent;
        private readonly object _textChangeLock = new();
        
        public event EventHandler? PreCheckEvent;
        private readonly object _checkLock = new();

        public AppearanceProperty()
        {
            _firstCount = _firstCount2 = _firstCount3 = 0;
            InitializeComponent();
            _obj = this;
            _opacity = 1;
        }
        
        event EventHandler IAppearance.OnMouseDown
        {
            add
            {
                lock (_mouseDownLock)
                {
                    PreMouseDownEvent += value;
                }
            }
            remove
            {
                lock (_mouseDownLock)
                {
                    PreMouseDownEvent -= value;
                }
            }
        }
        
        event EventHandler IAppearance.OnTextChange
        {
            add
            {
                lock (_textChangeLock)
                {
                    PreTextChangeEvent += value;
                }
            }
            remove
            {
                lock (_textChangeLock)
                {
                    PreTextChangeEvent -= value;
                }
            }
        }
        
        event EventHandler IAppearance.OnCheck
        {
            add
            {
                lock (_checkLock)
                {
                    PreCheckEvent += value;
                }
            }
            remove
            {
                lock (_checkLock)
                {
                    PreCheckEvent -= value;
                }
            }
        }

        public static AppearanceProperty Instance => _obj == null! ? new AppearanceProperty() : _obj;

        public void FeedProps(object properties)
        {
            SFillColor.Visibility = SMargin.Visibility = SPadding.Visibility = SBorder.Visibility = SBorderRad.Visibility =
            SBorderRadBtn.Visibility = CBorderRad.Visibility = CFillColor.Visibility = Visibility.Collapsed;
            _properties = (properties as GroupProperties)!;
            
            #region
            foreach (var prop in _properties.Properties.Where(prop => prop.Visibility == VisibilityValue.Visible.ToString()))
            {
                if (prop.Name == PropertyNames.FillColor.ToString())
                {
                    SFillColor.Visibility = CFillColor.Visibility = Visibility.Visible;
                    CFillColor.IsChecked = prop.Value != ColorValue.Transparent.ToString();
                    BFillColor.Background = ManageEnums.GetColor(prop.Value);
                }
                else if (prop.Name == PropertyNames.Opacity.ToString())
                {
                    _opacity = Helper.ConvertToDouble(prop.Value);
                }
                else if (prop.Name == PropertyNames.Margin.ToString())
                {
                    SMargin.Visibility = Visibility.Visible;
                    if (prop.Value == "1")
                    {
                        BMarginLeft.Background = BMarginTop.Background =  BMarginRight.Background = 
                            BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        TMargin.Text = prop.Value.Replace(",", ".");
                        CMargin.IsChecked = true;
                    }
                    else
                    {
                        CMargin.IsChecked = false;
                    }
                }
                else if (prop.Name == PropertyNames.MarginLeft.ToString())
                {
                    if (prop.Value != "1") continue;
                    BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BMarginTop.Background = BMarginRight.Background =  BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TMargin.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.MarginTop.ToString())
                {
                    if (prop.Value != "1") continue;
                    BMarginTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BMarginLeft.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TMargin.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.MarginRight.ToString())
                {
                    if (prop.Value != "1") continue;
                    BMarginRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BMarginLeft.Background = BMarginTop.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TMargin.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.MarginBottom.ToString())
                {
                    if (Properties.groupProps[idG].Properties[1].Value == "0" && Properties.groupProps[idG].Properties[2].Value == "MarginBottom")
                    {
                        BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginLeft.Background = BMarginRight.Background = BMarginTop.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TMargin.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.Padding.ToString())
                {
                    SPadding.Visibility = Visibility.Visible;
                    if (Properties.groupProps[idG].Properties[8].Value == "1")
                    {
                        BPaddingLeft.Background = BPaddingTop.Background = BPaddingRight.Background =
                            BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        TPadding.Text = prop.Value.Replace(",", ".");
                        CPadding.IsChecked = true;
                    }
                    else
                    {
                        CPadding.IsChecked = false;
                    }
                }
                else if (prop.Name == PropertyNames.PaddingLeft.ToString())
                {
                    if (Properties.groupProps[idG].Properties[8].Value == "0" && Properties.groupProps[idG].Properties[9].Value == "PaddingLeft")
                    {
                        BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.PaddingTop.ToString())
                {
                    if (Properties.groupProps[idG].Properties[8].Value == "0" && Properties.groupProps[idG].Properties[9].Value == "PaddingTop")
                    {
                        BPaddingTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingLeft.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.PaddingRight.ToString())
                {
                    if (Properties.groupProps[idG].Properties[8].Value == "0" && Properties.groupProps[idG].Properties[9].Value == "PaddingRight")
                    {
                        BPaddingRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingLeft.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                {
                    if (Properties.groupProps[idG].Properties[8].Value == "0" && Properties.groupProps[idG].Properties[9].Value == "PaddingBottom")
                    {
                        BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingRight.Background = BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderWidth.ToString())
                {
                    SBorder.Visibility = Visibility.Visible;
                    if (Properties.groupProps[idG].Properties[15].Value == "1")
                    {
                        BBorderLeft.Background = BBorderTop.Background = BBorderRight.Background =
                            BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        TBorderW.Text = prop.Value.Replace(",", ".");
                        CBorder.IsChecked = true;
                    }
                    else
                    {
                        CBorder.IsChecked = false;
                    }
                }
                else if (prop.Name == PropertyNames.BorderColor.ToString())
                {
                    CBorderC.IsChecked = prop.Value != ColorValue.Transparent.ToString();
                    BBorderC.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                        new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.BorderStyle.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "1")  CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderLeftWidth.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderLeft")
                    {
                        BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderLeftStyle.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderLeft")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderTopWidth.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderTop")
                    {
                        BBorderTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderLeft.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderTopStyle.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderTop")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderRightWidth.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderRight")
                    {
                        BBorderRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderLeft.Background = BBorderLeft.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderRightStyle.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderRight")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderBottomWidth.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderBottom")
                    {
                        BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderRight.Background = BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderBottomStyle.ToString())
                {
                    if (Properties.groupProps[idG].Properties[15].Value == "0" && Properties.groupProps[idG].Properties[16].Value == "BorderBottom")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderRadius.ToString())
                {
                    SBorderRad.Visibility = SBorderRadBtn.Visibility = CBorderRad.Visibility = Visibility.Visible;
                    if (Properties.groupProps[idG].Properties[32].Value == "1")
                    {
                        BBorderRadTL.BorderBrush = BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush =
                            BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        TBorderRad.Text = prop.Value.Replace(",", ".");
                        CBorderRad.IsChecked = true;
                    }
                    else
                    {
                        CBorderRad.IsChecked = false;
                    }
                }
                else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString())
                {
                    if (Properties.groupProps[idG].Properties[32].Value == "0" && Properties.groupProps[idG].Properties[33].Value == "BorderRadiusTopLeft")
                    {
                        BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                {
                    if (Properties.groupProps[idG].Properties[32].Value == "0" && Properties.groupProps[idG].Properties[33].Value == "BorderRadiusBottomLeft")
                    {
                        BBorderRadBL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadTL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                {
                    if (Properties.groupProps[idG].Properties[32].Value == "0" && Properties.groupProps[idG].Properties[33].Value == "BorderRadiusTopRight")
                    {
                        BBorderRadTR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTL.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = prop.Value.Replace(",", ".");
                    }
                }
                else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                {
                    if (Properties.groupProps[idG].Properties[32].Value == "0" && Properties.groupProps[idG].Properties[33].Value == "BorderRadiusBottomRight")
                    {
                        BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = prop.Value.Replace(",", ".");
                    }
                }
            }
            #endregion
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (sender as ComboBox)!;
            var tag = comboBox!.Tag != null ? comboBox.Tag.ToString()! : "";
            var idP = PropertyNames.None;

            if (_firstCount3 > 0)
            {
                var pos = Properties.GetPosition(GroupNames.Appearance, PropertyNames.FillColor);
                var idG = pos[0];
                switch (tag)
                {
                    case "BorderStyle":
                        if (Properties.groupProps![3].Properties[15].Value == "1")
                        {
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderStyle, comboBox.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftStyle, comboBox.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopStyle, comboBox.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightStyle, comboBox.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomStyle, comboBox.SelectedIndex + "");
                        }
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderLeft") idP = PropertyNames.BorderLeftStyle;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderTop") idP = PropertyNames.BorderTopStyle;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderRight") idP = PropertyNames.BorderRightStyle;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderBottom") idP = PropertyNames.BorderBottomStyle;
                        idP = PropertyNames.BorderColor; break;
                }
            }
            if (idP != PropertyNames.None)
            {
                var value = (comboBox.SelectedValue as ComboBoxItem)!.Content.ToString()!;
                if (_firstCount3 > 0) PanelProperty.ReactToProps(GroupNames.Appearance, idP, comboBox.SelectedIndex + "");
            }
            if (_firstCount3 < 1) _firstCount++;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox)!;
            var tag = textBox.Tag != null ? textBox.Tag.ToString()! : "";
            var idP = PropertyNames.None;
            var found = true;

            textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
            var value = textBox.Text == "" || textBox.Text == "-" ? "0" : textBox.Text;
            
            if(value[^1] == '.') return;
            
            double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
            
            if (_firstCount2 >= 4)
            {
                var pos = Properties.GetPosition(GroupNames.Appearance, PropertyNames.FillColor);
                var idG = pos[0];
                switch (tag)
                {
                    case "Margin":
                        if (Properties.groupProps![idG].Properties[1].Value == "1")
                        {
                            found = false;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.Margin, vd + "");
                        }
                        else if (Properties.groupProps[idG].Properties[2].Value == "MarginLeft") idP = PropertyNames.MarginLeft;
                        else if (Properties.groupProps[idG].Properties[2].Value == "MarginTop") idP = PropertyNames.MarginTop;
                        else if (Properties.groupProps[idG].Properties[2].Value == "MarginRight") idP = PropertyNames.MarginRight;
                        else if (Properties.groupProps[idG].Properties[2].Value == "MarginBottom") idP = PropertyNames.MarginBottom;
                        break;
                    case "Padding":
                        if (Properties.groupProps![idG].Properties[8].Value == "1")
                        {
                            found = false;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.Padding, vd + "");
                        }
                        else if (Properties.groupProps[idG].Properties[9].Value == "PaddingLeft") idP = PropertyNames.PaddingLeft;
                        else if (Properties.groupProps[idG].Properties[9].Value == "PaddingTop") idP = PropertyNames.PaddingTop;
                        else if (Properties.groupProps[idG].Properties[9].Value == "PaddingRight") idP = PropertyNames.PaddingRight;
                        else if (Properties.groupProps[idG].Properties[9].Value == "PaddingBottom") idP = PropertyNames.PaddingBottom;
                        break;
                    case "BorderW":
                        if (Properties.groupProps![idG].Properties[15].Value == "1")
                        {
                            found = false;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderWidth, vd + "");
                        }
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderLeft") idP = PropertyNames.BorderLeftWidth;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderTop") idP = PropertyNames.BorderTopWidth;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderRight") idP = PropertyNames.BorderRightWidth;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderBottom") idP = PropertyNames.BorderBottomWidth;
                        break;
                    case "BorderRad":
                        if (Properties.groupProps![idG].Properties[32].Value == "1")
                        {
                            found = false;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadius, vd + "");
                        }
                        else if (Properties.groupProps[idG].Properties[33].Value == "BorderRadiusTopLeft") idP = PropertyNames.BorderRadiusTopLeft;
                        else if (Properties.groupProps[idG].Properties[33].Value == "BorderRadiusBottomLeft") idP = PropertyNames.BorderRadiusBottomLeft;
                        else if (Properties.groupProps[idG].Properties[33].Value == "BorderRadiusTopRight") idP = PropertyNames.BorderRadiusTopRight;
                        else if (Properties.groupProps[idG].Properties[33].Value == "BorderRadiusBottomRight") idP = PropertyNames.BorderRadiusBottomRight;
                        break;
                }
            }
            
            if (found)
            {
                if(_firstCount2 >= 4) PanelProperty.ReactToProps(GroupNames.Appearance, idP, vd+"");
                if (_firstCount2 < 5) _firstCount2++;
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;

            var pos = Properties.GetPosition(GroupNames.Appearance, PropertyNames.FillColor);
            var idG = pos[0];
            #region
            switch (tag)
            {
                case "MarginLeft":
                    if (Properties.groupProps![idG].Properties[1].Value == "0")
                    {
                        BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginTop.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBtnActif, "MarginLeft");
                        TMargin.Text = Properties.groupProps[idG].Properties[4].Value.Replace(",", ".");

                    }
                    break;
                case "MarginRight":
                    if (Properties.groupProps![idG].Properties[1].Value == "0")
                    {
                        BMarginRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginTop.Background = BMarginLeft.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBtnActif, "MarginRight");
                        TMargin.Text = Properties.groupProps[idG].Properties[6].Value.Replace(",", ".");
                    }
                    break;
                case "MarginTop":
                    if (Properties.groupProps![idG].Properties[1].Value == "0")
                    {
                        BMarginTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginLeft.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBtnActif, "MarginTop");
                        TMargin.Text = Properties.groupProps[idG].Properties[5].Value.Replace(",", ".");
                    }
                    break;
                case "MarginBot":
                    if (Properties.groupProps![idG].Properties[1].Value == "0")
                    {
                        BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginTop.Background = BMarginRight.Background = BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBtnActif, "MarginBottom");
                        TMargin.Text = Properties.groupProps[idG].Properties[7].Value.Replace(",", ".");
                    }
                    break;
                case "PaddingLeft":
                    if (Properties.groupProps![idG].Properties[8].Value == "0")
                    {
                        BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingBtnActif, "PaddingLeft");
                        TPadding.Text = Properties.groupProps[idG].Properties[11].Value.Replace(",", ".");
                    }
                    break;
                case "PaddingRight":
                    if (Properties.groupProps![idG].Properties[8].Value == "0")
                    {
                        BPaddingRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingLeft.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingBtnActif, "PaddingRight");
                        TPadding.Text = Properties.groupProps[idG].Properties[13].Value;
                    }
                    break;
                case "PaddingTop":
                    if (Properties.groupProps![idG].Properties[8].Value == "0")
                    {
                        BPaddingTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingLeft.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingBtnActif, "PaddingTop");
                        TPadding.Text = Properties.groupProps[idG].Properties[12].Value.Replace(",", ".");
                    }
                    break;
                case "PaddingBot":
                    if (Properties.groupProps![idG].Properties[8].Value == "0")
                    {
                        BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingRight.Background = BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingBtnActif, "PaddingBottom");
                        TPadding.Text = Properties.groupProps[idG].Properties[14].Value.Replace(",", ".");
                    }
                    break;
                case "BorderLeft":
                    if (Properties.groupProps![idG].Properties[15].Value == "0")
                    {
                        BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBtnActif, "BorderLeft");
                        TBorderW.Text = Properties.groupProps[idG].Properties[20].Value.Replace(",", ".");
                    }
                    break;
                case "BorderRight":
                    if (Properties.groupProps![idG].Properties[15].Value == "0")
                    {
                        BBorderRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderLeft.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBtnActif, "BorderRight");
                        TBorderW.Text = Properties.groupProps[idG].Properties[26].Value.Replace(",", ".");
                    }
                    break;
                case "BorderTop":
                    if (Properties.groupProps![idG].Properties[15].Value == "0")
                    {
                        BBorderTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderLeft.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBtnActif, "BorderTop");
                        TBorderW.Text = Properties.groupProps[idG].Properties[23].Value.Replace(",", ".");
                    }
                    break;
                case "BorderBot":
                    if (Properties.groupProps![idG].Properties[15].Value == "0")
                    {
                        BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderRight.Background = BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBtnActif, "BorderBottom");
                        TBorderW.Text = Properties.groupProps[idG].Properties[29].Value.Replace(",", ".");
                    }
                    break;
                case "BorderRadTL":
                    if (Properties.groupProps![idG].Properties[32].Value == "0")
                    {
                        BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadBtnActif, "BorderRadiusTopLeft");
                        TBorderRad.Text = Properties.groupProps[idG].Properties[35].Value.Replace(",", ".");
                    }
                    break;
                case "BorderRadBL":
                    if (Properties.groupProps![idG].Properties[32].Value == "0")
                    {
                        BBorderRadBL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadTL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadBtnActif, "BorderRadiusBottomLeft");
                        TBorderRad.Text = Properties.groupProps[idG].Properties[36].Value.Replace(",", ".");
                    }
                    break;
                case "BorderRadTR":
                    if (Properties.groupProps![idG].Properties[32].Value == "0")
                    {
                        BBorderRadTR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTL.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadBtnActif, "BorderRadiusTopRight");
                        TBorderRad.Text = Properties.groupProps[idG].Properties[37].Value.Replace(",", ".");
                    }
                    break;
                case "BorderRadBR":
                    if (Properties.groupProps![idG].Properties[32].Value == "0")
                    {
                        BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadBtnActif, "BorderRadiusBottomRight");
                        TBorderRad.Text = Properties.groupProps[idG].Properties[38].Value.Replace(",", ".");
                    }
                    break;
                case "BorderC":
                    if(CBorderC.IsChecked == true)
                    {
                        MainWindow.Instance.DisplayColorPalette(BBorderC.Background, !ColorPalette.Instance.IsOpened, tag);
                    }
                    break;
                case "FillColor":
                    if(CFillColor.IsChecked == true)
                    {
                        MainWindow.Instance.DisplayColorPalette(BFillColor.Background, !ColorPalette.Instance.IsOpened, tag, _opacity);
                        //Modals.ColorPicker.Instance.Show();
                    }
                    break;
            }
            #endregion
        }

        private void OnColorChecked(object sender, RoutedEventArgs e)
        {
            var cb = (sender as CheckBox)!;
            var tag = cb!.Tag != null ? cb.Tag.ToString()! : "";
            var value = "";
            var idP = PropertyNames.ElementSize;
            var found = false;

            if (_firstCount > 6)
            {//Souce potentielle de bug
                var pos = Properties.GetPosition(GroupNames.Appearance, PropertyNames.FillColor);
                var idG = pos[0];
                switch (tag)
                {
                    case "Margin":
                        if (cb.IsChecked == true)
                        {
                            BMarginLeft.Background = BMarginTop.Background = BMarginRight.Background =
                                                      BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            double vd = 0; double.TryParse(TMargin.Text, out vd);
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.Margin, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginLeft, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginTop, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginRight, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBottom, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CMargin, "1");
                        }
                        else
                        {
                            BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BMarginTop.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CMargin, "0");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBtnActif, "MarginLeft");
                        }
                        break;
                    case "Padding":
                        if (cb.IsChecked == true)
                        {
                            BPaddingLeft.Background = BPaddingTop.Background = BPaddingRight.Background =
                                                      BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            double vd = 0; double.TryParse(TPadding.Text, out vd);
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.Padding, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingLeft, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingTop, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingRight, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingBottom, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CPadding, "1");
                        }
                        else
                        {
                            BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BPaddingTop.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CPadding, "0");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.PaddingBtnActif, "PaddingLeft");
                        }
                        break;
                    case "Border":
                        if (cb.IsChecked == true)
                        {
                            BBorderLeft.Background = BBorderTop.Background = BBorderRight.Background =
                                                     BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            double vd = 0; double.TryParse(TPadding.Text, out vd);
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderWidth, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderStyle, CBorderStyle.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftWidth, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftStyle, CBorderStyle.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopWidth, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopStyle, CBorderStyle.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightWidth, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightStyle, CBorderStyle.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomWidth, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomStyle, CBorderStyle.SelectedIndex + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CBorder, "1");
                        }
                        else
                        {
                            BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BBorderTop.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CBorder, "0");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBtnActif, "BorderLeft");
                        }
                        break;
                    case "BorderRad":
                        if (cb.IsChecked == true)
                        {
                            BBorderRadTL.BorderBrush = BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush =
                                                       BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            double vd = 0; double.TryParse(TBorderRad.Text, out vd);
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadius, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadiusTopLeft, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadiusTopRight, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadiusBottomLeft, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadiusBottomRight, vd + "");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CBorderRadius, "1");
                        }
                        else
                        {
                            BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CBorderRadius, "0");
                            PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRadBtnActif, "BorderRadiusTopLeft");
                        }
                        break;
                    case "BorderC":
                        if (Properties.groupProps![idG].Properties[15].Value == "1") idP = PropertyNames.BorderColor;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderLeft") idP = PropertyNames.BorderLeftColor;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderTop") idP = PropertyNames.BorderTopColor;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderRight") idP = PropertyNames.BorderRightColor;
                        else if (Properties.groupProps[idG].Properties[16].Value == "BorderBottom") idP = PropertyNames.BorderBottomColor;
                        if (cb.IsChecked == false)
                        {
                            BBorderC.Background = Brushes.Transparent;
                            value = "Transparent"; found = true;
                        }
                        break;
                    case "FillColor":
                        idP = PropertyNames.FillColor;
                        if (cb.IsChecked == false)
                        {
                            BFillColor.Background = Brushes.Transparent;
                            value = "Transparent"; found = true;
                        }
                        break;
                }
            }
            if (found)
            {
                if (_firstCount > 6) PanelProperty.ReactToProps(GroupNames.Appearance, idP, value);
            }
            if (_firstCount < 7) _firstCount++;
        }

        public void LoadValue(int idP, string value, string btnName)
        {

        }

        public void SetColor(Brush color, int id)
        {
            var pos = Properties.GetPosition(GroupNames.Appearance, PropertyNames.FillColor);
            var idG = pos[0];
            var idP = PropertyNames.FillColor;
            if (id == 0)
            {
                BBorderC.Background = color;
                if (Properties.groupProps![idG].Properties[15].Value == "1")
                {
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomColor, color.ToString());
                }
                else if (Properties.groupProps[idG].Properties[16].Value == "BorderLeft") idP = PropertyNames.BorderColor;
                else if (Properties.groupProps[idG].Properties[16].Value == "BorderTop") idP = PropertyNames.BorderColor;
                else if (Properties.groupProps[idG].Properties[16].Value == "BorderRight") idP = PropertyNames.BorderColor;
                else if (Properties.groupProps[idG].Properties[16].Value == "BorderBottom") idP = PropertyNames.BorderColor;
                if(idP != PropertyNames.FillColor) PanelProperty.ReactToProps(GroupNames.Appearance, idP, color.ToString());
            }
            else if (id == 1)
            {
                BFillColor.Background = color;
                PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.FillColor, color.ToString());
            }
        }

        public void SetOpacity(double value)
        {
            PanelProperty.ReactToProps(
                GroupNames.Appearance,
                PropertyNames.Opacity,
                value.ToString(CultureInfo.InvariantCulture)
            );
        }
    }
}
