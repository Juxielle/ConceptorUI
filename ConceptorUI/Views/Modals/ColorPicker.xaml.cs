using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Classes;

namespace ConceptorUI.Views.Modals;


public partial class ColorPicker
{
    public string PropOriginColor { get; set; }
    private Brush _brush;
    private Button _btnIntermed;
    public bool IsOpened;
    private Dictionary<int, string[]> _colors;
    private List<Button> _colorButtons;
    private bool _canSetFieldColor;
    private int _pasteCount;
    private readonly List<ColorModel> _gradientColors;
    private CustomColor _customColor;
    
    public ICommand? ColorSelectedCommand;
        
    public ICommand? OpacityChangedCommand;

    public ColorPicker(Brush color, double opacity)
    {
        InitializeComponent();
        
        PropOriginColor = "";
        _brush = null!;
        _btnIntermed = null!;
        IsOpened = false;
        _canSetFieldColor = true;
        _pasteCount = 0;
        _customColor = new CustomColor();
        
        _gradientColors = new List<ColorModel>
        {
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
            new (){ Color = "#6200EA" },
        };
        LvColors.ItemsSource = _gradientColors;
        
        InitColors();
        LoadColorValue(color, false, opacity);
    }

    private void ValueChanged(object sender, EventArgs e)
    {
        var value = (sender as Slider)!.Value.ToString(CultureInfo.InvariantCulture);
        if (OpacityValue != null) OpacityValue.Text = value + "%";
        if (ColorBox == null) return;

        var vd = Convert.ToDouble(value.Replace(",", ".")) / 100;
        ColorBox.Background.Opacity = vd;
        TbA.Text = ColorBox.Background.Opacity.ToString(CultureInfo.InvariantCulture);
        
        OpacityChangedCommand?.Execute(vd);
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        var btn = (sender as Button)!;
        var color = btn.Background;
        if(_btnIntermed != null!)
        {
            _btnIntermed.BorderBrush = _brush;
            _btnIntermed.BorderThickness = new Thickness(1);
        }
        _btnIntermed = btn;
        _brush = color.Clone();
        btn.BorderBrush = Brushes.DodgerBlue;
        btn.BorderThickness = new Thickness(4);
        ColorBox.Background = color.Clone();
        OpacityValue.Text = (ColorBox.Background.Opacity * 100) + "%";
        SlOpacity.Value = 100;
        SendColorValue(ColorBox.Background);
        LoadColorValue(ColorBox.Background, true);
    }

    private void SendColorValue(Brush color)
    {
        ColorSelectedCommand?.Execute(color);
        
        if (_btnIntermed == null!) return;
        _btnIntermed.BorderBrush = _brush;
        _btnIntermed.BorderThickness = new Thickness(1);
        _brush = null!; _btnIntermed = null!;
    }

    private void LoadColorValue(Brush color, bool isInterne, double opacity = 1.0)
    {
        _canSetFieldColor = false;
        TbHexa.Text = color.ToString();
        OpacityValue.Text = (color.Opacity * 100) + "%";
        
        var sb = (SolidColorBrush)color;
        TbR.Text = sb.Color.R.ToString();
        TbG.Text = sb.Color.G.ToString();
        TbB.Text = sb.Color.B.ToString();
        TbA.Text = color.Opacity.ToString(CultureInfo.InvariantCulture);
        IsOpened = true;
        
        SelectColor(color);
        ColorBox.Background = color;

        SlOpacity.Value = opacity * 100;
        
        if (isInterne) return;
        
        CheckColor(color);
    }

    private void OnClose(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    private void OnTextChanged(object sender, RoutedEventArgs e)
    {
        var textBox = sender as TextBox;
        var textColor = textBox!.Text;
        
        if (!_canSetFieldColor)
        {
            _canSetFieldColor = true;
            return;
        }
        
        try
        {
            textColor = _pasteCount == 0 && textColor.Length == 6 ? "#FF" + textColor : textColor;
            textColor = _pasteCount == 0 && textColor.Length == 7 ? textColor.Replace("#", "#FF") : textColor;
            
            var color = new BrushConverter().ConvertFrom(textColor) as SolidColorBrush;
            
            SendColorValue(color!);
            LoadColorValue(color!, true);
            _pasteCount = 0;
        }
        catch (Exception)
        {
            _pasteCount++;
        }
    }

    private void CheckColor(Brush color)
    {
        if (!color.ToString().Equals(Btn00.Background.ToString())) return;
        Btn00.BorderBrush = Brushes.DodgerBlue;
        Btn00.BorderThickness = new Thickness(4);
    }

    private void SelectColor(Brush color)
    {
        foreach (var colorButton in _colorButtons)
        {
            if (color.ToString().Equals(colorButton.Background.ToString()))
            {
                colorButton.BorderBrush = Brushes.DodgerBlue;
                colorButton.BorderThickness = new Thickness(4);
            }
            else
            {
                colorButton.BorderBrush = colorButton.Background;
                colorButton.BorderThickness = new Thickness(0);
            }
        }
    }

    private void InitColors()
    {
        #region MyRegion
        _colorButtons = new List<Button>
        {
            Btn00, Btn01, Btn02, Btn03, Btn04, Btn05, Btn06, Btn07, Btn08, Btn09, Btn010, Btn011, Btn012, Btn013, Btn014,
            Btn10, Btn11, Btn12, Btn13, Btn14, Btn15, Btn16, Btn17, Btn18, Btn19, Btn110, Btn111, Btn112, Btn113, Btn114,
            Btn20, Btn21, Btn22, Btn23, Btn24, Btn25, Btn26, Btn27, Btn28, Btn29, Btn210, Btn211, Btn212, Btn213, Btn214,
            Btn30, Btn31, Btn32, Btn33, Btn34, Btn35, Btn36, Btn37, Btn38, Btn39, Btn310, Btn311, Btn312, Btn313, Btn314,
            Btn40, Btn41, Btn42, Btn43, Btn44, Btn45, Btn46, Btn47, Btn48, Btn49, Btn410, Btn411, Btn412, Btn413, Btn414,
            Btn50, Btn51, Btn52, Btn53, Btn54, Btn55, Btn56, Btn57, Btn58, Btn59, Btn510, Btn511, Btn512, Btn513, Btn514,
            Btn60, Btn61, Btn62, Btn63, Btn64, Btn65, Btn66, Btn67, Btn68, Btn69, Btn610, Btn611, Btn612, Btn613, Btn614,
            Btn70, Btn71, Btn72, Btn73, Btn74, Btn75, Btn76, Btn77, Btn78, Btn79, Btn710, Btn711, Btn712, Btn713, Btn714,
            Btn80, Btn81, Btn82, Btn83, Btn84, Btn85, Btn86, Btn87, Btn88, Btn89, Btn810, Btn811, Btn812, Btn813, Btn814,
            Btn90, Btn91, Btn92, Btn93, Btn94, Btn95, Btn96, Btn97, Btn98, Btn99, Btn910, Btn911, Btn912, Btn913, Btn914,
            Btn100, Btn101, Btn102, Btn103, Btn104, Btn105, Btn106, Btn107, Btn108, Btn109, Btn1010, Btn1011, Btn1012, Btn1013, Btn1014,
            Btn1100, Btn1101, Btn1102, Btn1103, Btn1104, Btn1105, Btn1106, Btn1107, Btn1108, Btn1109, Btn1110, Btn1111, Btn1112, Btn1113, Btn1114,
            Btn120, Btn121, Btn122, Btn123, Btn124, Btn125, Btn126, Btn127, Btn128, Btn129, Btn1210, Btn1211, Btn1212, Btn1213, Btn1214,
            Btn130, Btn131, Btn132, Btn133, Btn134, Btn135, Btn136, Btn137, Btn138, Btn139, Btn1310, Btn1311, Btn1312, Btn1313, Btn1314,
            Btn140, Btn141, Btn142, Btn143, Btn144, Btn145, Btn146, Btn147, Btn148, Btn149, Btn1410, Btn1411, Btn1412, Btn1413, Btn1414,
            Btn150, Btn151, Btn152, Btn153, Btn154, Btn155, Btn156, Btn157, Btn158, Btn159, Btn1510, Btn1511, Btn1512, Btn1513, Btn1514,
            Btn160, Btn161, Btn162, Btn163, Btn164, Btn165, Btn166, Btn167, Btn168, Btn169, Btn1610, Btn1611, Btn1612,
            Btn170, Btn171, Btn172, Btn173, Btn174, Btn175, Btn176, Btn177, Btn178, Btn179, Btn1710,
            Btn180, Btn181, Btn182, Btn183, Btn184, Btn185, Btn186, Btn187, Btn188, Btn189, Btn1810,
        };
        #endregion
    }
    
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
        try
        {
            //if(e.OriginalSource.Equals(TopBar))
            DragMove();
        }
        catch (Exception)
        {
            //
        }
    }
}

public class ColorModel
{
    public string Name { get; set; }
    public string Color { get; set; }
}