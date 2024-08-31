using ConceptorUI.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Interfaces;
using ConceptorUI.Utils;
using ConceptorUI.Views.Modals;


namespace ConceptorUI.Views.Component
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
        private bool _allowSetField;
        
        public event EventHandler? PreMouseDownEvent;
        private readonly object _mouseDownLock = new();

        public AppearanceProperty()
        {
            _allowSetField = false;
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

        public static AppearanceProperty Instance => _obj == null! ? new AppearanceProperty() : _obj;

        public void FeedProps(object properties)
        {
            SFillColor.Visibility = SMargin.Visibility = SPadding.Visibility = SBorder.Visibility = SBorderRad.Visibility =
            SBorderRadBtn.Visibility = CBorderRad.Visibility = CFillColor.Visibility = Visibility.Collapsed;
            _properties = (properties as GroupProperties)!;
            _allowSetField = false;
            
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
                    if (_properties.GetValue(PropertyNames.CMargin) == "1")
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
                    if (_properties.GetValue(PropertyNames.CMargin) != "0" || 
                        _properties.GetValue(PropertyNames.MarginBtnActif) != "MarginLeft") continue;
                    BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BMarginTop.Background = BMarginRight.Background =  BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TMargin.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.MarginTop.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CMargin) != "0" || 
                        _properties.GetValue(PropertyNames.MarginBtnActif) != "MarginTop") continue;
                    BMarginTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BMarginLeft.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TMargin.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.MarginRight.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CMargin) != "0" || 
                        _properties.GetValue(PropertyNames.MarginBtnActif) != "MarginRight") continue;
                    BMarginRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BMarginLeft.Background = BMarginTop.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TMargin.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.MarginBottom.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CMargin) != "0" || 
                        _properties.GetValue(PropertyNames.MarginBtnActif) != "MarginBottom") continue;
                    BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BMarginLeft.Background = BMarginRight.Background = BMarginTop.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TMargin.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.Padding.ToString())
                {
                    SPadding.Visibility = Visibility.Visible;
                    if (_properties.GetValue(PropertyNames.CPadding) == "1")
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
                    if (_properties.GetValue(PropertyNames.CPadding) != "0" || 
                        _properties.GetValue(PropertyNames.PaddingBtnActif) != "PaddingLeft") continue;
                    BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BPaddingTop.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TPadding.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.PaddingTop.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CPadding) != "0" || 
                        _properties.GetValue(PropertyNames.PaddingBtnActif) != "PaddingTop") continue;
                    BPaddingTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BPaddingLeft.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TPadding.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.PaddingRight.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CPadding) != "0" || 
                        _properties.GetValue(PropertyNames.PaddingBtnActif) != "PaddingRight") continue;
                    BPaddingRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BPaddingTop.Background = BPaddingLeft.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TPadding.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.PaddingBottom.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CPadding) != "0" || 
                        _properties.GetValue(PropertyNames.PaddingBtnActif) != "PaddingBottom") continue;
                    BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BPaddingTop.Background = BPaddingRight.Background = BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TPadding.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderWidth.ToString())
                {
                    SBorder.Visibility = Visibility.Visible;
                    if (_properties.GetValue(PropertyNames.CBorder) == "1")
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
                    if (_properties.GetValue(PropertyNames.CBorder) == "1")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderLeftWidth.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) != "0" || 
                        _properties.GetValue(PropertyNames.BorderBtnActif) != "BorderLeft") continue;
                    BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderTop.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderW.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderLeftStyle.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) == "0" && _properties.GetValue(PropertyNames.BorderBtnActif) == "BorderLeft")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderTopWidth.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) != "0" || 
                        _properties.GetValue(PropertyNames.BorderBtnActif) != "BorderTop") continue;
                    BBorderTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderLeft.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderW.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderTopStyle.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) == "0" && _properties.GetValue(PropertyNames.BorderBtnActif) == "BorderTop")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderRightWidth.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) != "0" || 
                        _properties.GetValue(PropertyNames.BorderBtnActif) != "BorderRight") continue;
                    BBorderRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderLeft.Background = BBorderLeft.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderW.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderRightStyle.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) == "0" && _properties.GetValue(PropertyNames.BorderBtnActif) == "BorderRight")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderBottomWidth.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) != "0" || 
                        _properties.GetValue(PropertyNames.BorderBtnActif) != "BorderBottom") continue;
                    BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderTop.Background = BBorderRight.Background = BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderW.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderBottomStyle.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorder) == "0" && _properties.GetValue(PropertyNames.BorderBtnActif) == "BorderBottom")
                        CBorderStyle.SelectedIndex = Convert.ToInt32(prop.Value);
                }
                else if (prop.Name == PropertyNames.BorderRadius.ToString())
                {
                    SBorderRad.Visibility = SBorderRadBtn.Visibility = CBorderRad.Visibility = Visibility.Visible;
                    if (_properties.GetValue(PropertyNames.CBorderRadius) == "1")
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
                    if (_properties.GetValue(PropertyNames.CBorderRadius) != "0" || 
                        _properties.GetValue(PropertyNames.BorderRadBtnActif) != "BorderRadiusTopLeft") continue;
                    BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderRad.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorderRadius) != "0" || 
                        _properties.GetValue(PropertyNames.BorderRadBtnActif) != "BorderRadiusBottomLeft") continue;
                    BBorderRadBL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderRadTL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderRad.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorderRadius) != "0" || 
                        _properties.GetValue(PropertyNames.BorderRadBtnActif) != "BorderRadiusTopRight") continue;
                    BBorderRadTR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderRadBL.BorderBrush = BBorderRadTL.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderRad.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString())
                {
                    if (_properties.GetValue(PropertyNames.CBorderRadius) != "0" || 
                        _properties.GetValue(PropertyNames.BorderRadBtnActif) != "BorderRadiusBottomRight") continue;
                    BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                    BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                    TBorderRad.Text = prop.Value.Replace(",", ".");
                }
            }
            #endregion
            
            _allowSetField = true;
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (sender as ComboBox)!;
            var tag = comboBox.Tag != null ? comboBox.Tag.ToString()! : "";
            var idP = PropertyNames.None;
            
            if (_firstCount3 > 0)
            {
                switch (tag)
                {
                    case "BorderStyle":
                        if (_properties.GetValue(PropertyNames.CBorder) == "1")
                        {
                            PreMouseDownEvent?.Invoke(
                                new dynamic[]{GroupNames.Appearance, PropertyNames.BorderStyle, comboBox.SelectedIndex.ToString()},
                                EventArgs.Empty
                            );
                        }
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderLeft") idP = PropertyNames.BorderLeftStyle;
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderTop") idP = PropertyNames.BorderTopStyle;
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderRight") idP = PropertyNames.BorderRightStyle;
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderBottom") idP = PropertyNames.BorderBottomStyle;
                        break;
                }
            }
            if (idP != PropertyNames.None && _firstCount3 > 0)
            {
                PreMouseDownEvent?.Invoke(
                    new dynamic[]{GroupNames.Appearance, idP, comboBox.SelectedIndex.ToString()},
                    EventArgs.Empty
                );
            }
            if (_firstCount3 < 1) _firstCount++;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if(!_allowSetField) return;
            
            var textBox = (sender as TextBox)!;
            var tag = textBox.Tag != null ? textBox.Tag.ToString()! : "";
            var idP = PropertyNames.None;
            var found = true;

            textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
            var value = textBox.Text == "" || textBox.Text == "-" ? "0" : textBox.Text;
            
            if(value[^1] == '.') return;
            
            double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var vd);
            
            switch (tag)
            {
                case "Margin":
                    if (_properties.GetValue(PropertyNames.CMargin) == "1")
                    {
                        found = false;
                        PreMouseDownEvent?.Invoke(
                            new dynamic[]{GroupNames.Appearance, PropertyNames.Margin, vd.ToString(CultureInfo.InvariantCulture)},
                            EventArgs.Empty
                        );
                    }
                    else if (_properties.GetValue(PropertyNames.MarginBtnActif) == "MarginLeft") idP = PropertyNames.MarginLeft;
                    else if (_properties.GetValue(PropertyNames.MarginBtnActif) == "MarginTop") idP = PropertyNames.MarginTop;
                    else if (_properties.GetValue(PropertyNames.MarginBtnActif) == "MarginRight") idP = PropertyNames.MarginRight;
                    else if (_properties.GetValue(PropertyNames.MarginBtnActif) == "MarginBottom") idP = PropertyNames.MarginBottom;
                    break;
                case "Padding":
                    if (_properties.GetValue(PropertyNames.CPadding) == "1")
                    {
                        found = false;
                        PreMouseDownEvent?.Invoke(
                            new dynamic[]{GroupNames.Appearance, PropertyNames.Padding, vd.ToString(CultureInfo.InvariantCulture)},
                            EventArgs.Empty
                        );
                    }
                    else if (_properties.GetValue(PropertyNames.PaddingBtnActif) == "PaddingLeft") idP = PropertyNames.PaddingLeft;
                    else if (_properties.GetValue(PropertyNames.PaddingBtnActif) == "PaddingTop") idP = PropertyNames.PaddingTop;
                    else if (_properties.GetValue(PropertyNames.PaddingBtnActif) == "PaddingRight") idP = PropertyNames.PaddingRight;
                    else if (_properties.GetValue(PropertyNames.PaddingBtnActif) == "PaddingBottom") idP = PropertyNames.PaddingBottom;
                    break;
                case "BorderW":
                    if (_properties.GetValue(PropertyNames.CBorder) == "1")
                    {
                        found = false;
                        PreMouseDownEvent?.Invoke(
                            new dynamic[]{GroupNames.Appearance, PropertyNames.BorderWidth, vd.ToString(CultureInfo.InvariantCulture)},
                            EventArgs.Empty
                        );
                    }
                    else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderLeft") idP = PropertyNames.BorderLeftWidth;
                    else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderTop") idP = PropertyNames.BorderTopWidth;
                    else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderRight") idP = PropertyNames.BorderRightWidth;
                    else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderBottom") idP = PropertyNames.BorderBottomWidth;
                    break;
                case "BorderRad":
                    if (_properties.GetValue(PropertyNames.CBorderRadius) == "1")
                    {
                        found = false;
                        PreMouseDownEvent?.Invoke(
                            new dynamic[]{GroupNames.Appearance, PropertyNames.BorderRadius, vd.ToString(CultureInfo.InvariantCulture)},
                            EventArgs.Empty
                        );
                    }
                    else if (_properties.GetValue(PropertyNames.BorderRadBtnActif) == "BorderRadiusTopLeft") idP = PropertyNames.BorderRadiusTopLeft;
                    else if (_properties.GetValue(PropertyNames.BorderRadBtnActif) == "BorderRadiusBottomLeft") idP = PropertyNames.BorderRadiusBottomLeft;
                    else if (_properties.GetValue(PropertyNames.BorderRadBtnActif) == "BorderRadiusTopRight") idP = PropertyNames.BorderRadiusTopRight;
                    else if (_properties.GetValue(PropertyNames.BorderRadBtnActif) == "BorderRadiusBottomRight") idP = PropertyNames.BorderRadiusBottomRight;
                    break;
            }

            if(found)
                PreMouseDownEvent?.Invoke(
                    new dynamic[]{GroupNames.Appearance, idP, vd.ToString(CultureInfo.InvariantCulture)},
                    EventArgs.Empty
                );
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var found = false;
            var propertyName = PropertyNames.None;
            var value = string.Empty;
            
            #region
            switch (tag)
            {
                case "MarginLeft":
                    if (_properties.GetValue(PropertyNames.CMargin) == "0")
                    {
                        BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginTop.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TMargin.Text = _properties.GetValue(PropertyNames.MarginLeft).Replace(",", ".");
                        propertyName = PropertyNames.MarginBtnActif;
                        value = PropertyNames.MarginLeft.ToString();
                        found = true;
                    }
                    break;
                case "MarginRight":
                    if (_properties.GetValue(PropertyNames.CMargin) == "0")
                    {
                        BMarginRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginTop.Background = BMarginLeft.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TMargin.Text = _properties.GetValue(PropertyNames.MarginRight).Replace(",", ".");
                        propertyName = PropertyNames.MarginBtnActif;
                        value = PropertyNames.MarginRight.ToString();
                        found = true;
                    }
                    break;
                case "MarginTop":
                    if (_properties.GetValue(PropertyNames.CMargin) == "0")
                    {
                        BMarginTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginLeft.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TMargin.Text = _properties.GetValue(PropertyNames.MarginTop).Replace(",", ".");
                        propertyName = PropertyNames.MarginBtnActif;
                        value = PropertyNames.MarginTop.ToString();
                        found = true;
                    }
                    break;
                case "MarginBot":
                    if (_properties.GetValue(PropertyNames.CMargin) == "0")
                    {
                        BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BMarginTop.Background = BMarginRight.Background = BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TMargin.Text = _properties.GetValue(PropertyNames.MarginBottom).Replace(",", ".");
                        propertyName = PropertyNames.MarginBtnActif;
                        value = PropertyNames.MarginBottom.ToString();
                        found = true;
                    }
                    break;
                case "PaddingLeft":
                    if (_properties.GetValue(PropertyNames.CPadding) == "0")
                    {
                        BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = _properties.GetValue(PropertyNames.PaddingLeft).Replace(",", ".");
                        propertyName = PropertyNames.PaddingBtnActif;
                        value = PropertyNames.PaddingLeft.ToString();
                        found = true;
                    }
                    break;
                case "PaddingRight":
                    if (_properties.GetValue(PropertyNames.CPadding) == "0")
                    {
                        BPaddingRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingLeft.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = _properties.GetValue(PropertyNames.PaddingRight);
                        propertyName = PropertyNames.PaddingBtnActif;
                        value = PropertyNames.PaddingRight.ToString();
                        found = true;
                    }
                    break;
                case "PaddingTop":
                    if (_properties.GetValue(PropertyNames.CPadding) == "0")
                    {
                        BPaddingTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingLeft.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = _properties.GetValue(PropertyNames.PaddingTop).Replace(",", ".");
                        propertyName = PropertyNames.PaddingBtnActif;
                        value = PropertyNames.PaddingTop.ToString();
                        found = true;
                    }
                    break;
                case "PaddingBot":
                    if (_properties.GetValue(PropertyNames.CPadding) == "0")
                    {
                        BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BPaddingTop.Background = BPaddingRight.Background = BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TPadding.Text = _properties.GetValue(PropertyNames.PaddingBottom).Replace(",", ".");
                        propertyName = PropertyNames.PaddingBtnActif;
                        value = PropertyNames.PaddingBottom.ToString();
                        found = true;
                    }
                    break;
                case "BorderLeft":
                    if (_properties.GetValue(PropertyNames.CBorder) == "0")
                    {
                        BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = _properties.GetValue(PropertyNames.BorderLeftWidth).Replace(",", ".");
                        propertyName = PropertyNames.BorderBtnActif;
                        value = "BorderLeft";
                        found = true;
                    }
                    break;
                case "BorderRight":
                    if (_properties.GetValue(PropertyNames.CBorder) == "0")
                    {
                        BBorderRight.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderLeft.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = _properties.GetValue(PropertyNames.BorderRightWidth).Replace(",", ".");
                        propertyName = PropertyNames.BorderBtnActif;
                        value = "BorderRight";
                        found = true;
                    }
                    break;
                case "BorderTop":
                    if (_properties.GetValue(PropertyNames.CBorder) == "0")
                    {
                        BBorderTop.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderLeft.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = _properties.GetValue(PropertyNames.BorderTopWidth).Replace(",", ".");
                        propertyName = PropertyNames.BorderBtnActif;
                        value = "BorderTop";
                        found = true;
                    }
                    break;
                case "BorderBot":
                    if (_properties.GetValue(PropertyNames.CBorder) == "0")
                    {
                        BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderTop.Background = BBorderRight.Background = BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderW.Text = _properties.GetValue(PropertyNames.BorderBottomWidth).Replace(",", ".");
                        propertyName = PropertyNames.BorderBtnActif;
                        value = "BorderBottom";
                        found = true;
                    }
                    break;
                case "BorderRadTL":
                    if (_properties.GetValue(PropertyNames.CBorderRadius) == "0")
                    {
                        BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = _properties.GetValue(PropertyNames.BorderRadiusTopLeft).Replace(",", ".");
                        propertyName = PropertyNames.BorderRadBtnActif;
                        value = PropertyNames.BorderRadiusTopLeft.ToString();
                        found = true;
                    }
                    break;
                case "BorderRadBL":
                    if (_properties.GetValue(PropertyNames.CBorderRadius) == "0")
                    {
                        BBorderRadBL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadTL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = _properties.GetValue(PropertyNames.BorderRadiusBottomLeft).Replace(",", ".");
                        propertyName = PropertyNames.BorderRadBtnActif;
                        value = PropertyNames.BorderRadiusBottomLeft.ToString();
                        found = true;
                    }
                    break;
                case "BorderRadTR":
                    if (_properties.GetValue(PropertyNames.CBorderRadius) == "0")
                    {
                        BBorderRadTR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTL.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = _properties.GetValue(PropertyNames.BorderRadiusTopRight).Replace(",", ".");
                        propertyName = PropertyNames.BorderRadBtnActif;
                        value = PropertyNames.BorderRadiusTopRight.ToString();
                        found = true;
                    }
                    break;
                case "BorderRadBR":
                    if (_properties.GetValue(PropertyNames.CBorderRadius) == "0")
                    {
                        BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        TBorderRad.Text = _properties.GetValue(PropertyNames.BorderRadiusBottomRight).Replace(",", ".");
                        propertyName = PropertyNames.BorderRadBtnActif;
                        value = PropertyNames.BorderRadiusBottomRight.ToString();
                        found = true;
                    }
                    break;
                case "BorderC":
                    if(CBorderC.IsChecked == false) return;
                    var colorPickerB = new ColorPicker(BBorderC.Background, 1);
                    colorPickerB.PreColorSelectedEvent += (color, _) =>
                    {
                        PreMouseDownEvent?.Invoke(
                            new dynamic[]{GroupNames.Appearance, PropertyNames.BorderColor, color!.ToString()!},
                            EventArgs.Empty
                        );
                        
                        BBorderC.Background = new BrushConverter().ConvertFrom(color!.ToString()!) as SolidColorBrush;
                    };
                    colorPickerB.Show();
                    break;
                case "FillColor":
                    if(CFillColor.IsChecked == true)
                    {
                        //On Essaie de detacher l'evenement avant la fermeture de ColorPicker
                        var colorPicker = new ColorPicker(BFillColor.Background, _opacity);
                        colorPicker.PreOpacityChangedEvent += (opacity, _) =>
                        {
                            PreMouseDownEvent?.Invoke(
                                new dynamic[]{GroupNames.Appearance, PropertyNames.Opacity, opacity!.ToString()!},
                                EventArgs.Empty
                            );
                        };
                        
                        colorPicker.PreColorSelectedEvent += (color, _) =>
                        {
                            PreMouseDownEvent?.Invoke(
                                new dynamic[]{GroupNames.Appearance, PropertyNames.FillColor, color!.ToString()!},
                                EventArgs.Empty
                            );
                            
                            BFillColor.Background = new BrushConverter().ConvertFrom(color!.ToString()!) as SolidColorBrush;
                        };
                        colorPicker.Show();
                    }
                    break;
            }
            
            if(!found) return;
            PreMouseDownEvent?.Invoke(
                new dynamic[]{GroupNames.Appearance, propertyName, value},
                EventArgs.Empty
            );
            #endregion
        }

        private void OnColorChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = (sender as CheckBox)!;
            var tag = checkBox.Tag != null ? checkBox.Tag.ToString()! : "";
            var value = checkBox.IsChecked == true ? "1" : "0";
            var propertyName = PropertyNames.None;
            var found = false;

            if (_firstCount > 6)
            {
                switch (tag)
                {
                    case "Margin":
                        if (checkBox.IsChecked == true)
                        {
                            BMarginLeft.Background = BMarginTop.Background = BMarginRight.Background =
                                                      BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.Margin, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginLeft, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginTop, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginRight, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBottom, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CMargin, "1");
                        }
                        else
                        {
                            BMarginLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BMarginTop.Background = BMarginRight.Background = BMarginBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CMargin, "0");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.MarginBtnActif, "MarginLeft");
                        }
                        propertyName = PropertyNames.CMargin;
                        found = true;
                        break;
                    case "Padding":
                        if (checkBox.IsChecked == true)
                        {
                            BPaddingLeft.Background = BPaddingTop.Background = BPaddingRight.Background =
                                                      BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        }
                        else
                        {
                            BPaddingLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BPaddingTop.Background = BPaddingRight.Background = BPaddingBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        }
                        propertyName = PropertyNames.CPadding;
                        found = true;
                        break;
                    case "Border":
                        if (checkBox.IsChecked == true)
                        {
                            BBorderLeft.Background = BBorderTop.Background = BBorderRight.Background =
                                                     BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            // double vd = 0; double.TryParse(TPadding.Text, out vd);
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderWidth, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderStyle, CBorderStyle.SelectedIndex + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftWidth, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftStyle, CBorderStyle.SelectedIndex + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopWidth, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopStyle, CBorderStyle.SelectedIndex + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightWidth, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightStyle, CBorderStyle.SelectedIndex + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomWidth, vd + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomColor, ManageEnums.GetColor(BBorderC.Background.ToString()).ToString());
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomStyle, CBorderStyle.SelectedIndex + "");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CBorder, "1");
                        }
                        else
                        {
                            BBorderLeft.Background = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BBorderTop.Background = BBorderRight.Background = BBorderBot.Background = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.CBorder, "0");
                            // PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBtnActif, "BorderLeft");
                        }
                        propertyName = PropertyNames.CBorder;
                        found = true;
                        break;
                    case "BorderRad":
                        if (checkBox.IsChecked == true)
                        {
                            BBorderRadTL.BorderBrush = BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush =
                                                       BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                        }
                        else
                        {
                            BBorderRadTL.BorderBrush = new BrushConverter().ConvertFrom(ColorOn) as SolidColorBrush;
                            BBorderRadBL.BorderBrush = BBorderRadTR.BorderBrush = BBorderRadBR.BorderBrush = new BrushConverter().ConvertFrom(ColorOff) as SolidColorBrush;
                        }
                        propertyName = PropertyNames.CBorderRadius;
                        found = true;
                        break;
                    case "BorderC":
                        if (_properties.GetValue(PropertyNames.CBorder) == "1") propertyName = PropertyNames.BorderColor;
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderLeft") propertyName = PropertyNames.BorderLeftColor;
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderTop") propertyName = PropertyNames.BorderTopColor;
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderRight") propertyName = PropertyNames.BorderRightColor;
                        else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderBottom") propertyName = PropertyNames.BorderBottomColor;
                        if (checkBox.IsChecked == false)
                        {
                            BBorderC.Background = Brushes.Transparent;
                            value = "Transparent"; found = true;
                        }
                        break;
                    case "FillColor":
                        propertyName = PropertyNames.FillColor;
                        if (checkBox.IsChecked == false)
                        {
                            BFillColor.Background = Brushes.Transparent;
                            value = "Transparent"; found = true;
                        }
                        break;
                }
            }
            
            if (found && _firstCount > 6)
                PreMouseDownEvent?.Invoke(
                    new dynamic[]{GroupNames.Appearance, propertyName, value},
                    EventArgs.Empty
                );
            
            if (_firstCount < 7)
                _firstCount++;
        }

        public void SetColor(Brush color, int id)
        {
            var propertyName = PropertyNames.FillColor;
            if (id == 0)
            {
                BBorderC.Background = color;
                if (_properties.GetValue(PropertyNames.CBorder) == "1")
                {
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderLeftColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderTopColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderRightColor, color.ToString());
                    PanelProperty.ReactToProps(GroupNames.Appearance, PropertyNames.BorderBottomColor, color.ToString());
                }
                else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderLeft") propertyName = PropertyNames.BorderColor;
                else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderTop") propertyName = PropertyNames.BorderColor;
                else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderRight") propertyName = PropertyNames.BorderColor;
                else if (_properties.GetValue(PropertyNames.BorderBtnActif) == "BorderBottom") propertyName = PropertyNames.BorderColor;
                
                if(propertyName != PropertyNames.FillColor)
                    PanelProperty.ReactToProps(GroupNames.Appearance, propertyName, color.ToString());
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
