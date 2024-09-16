using System;
using System.Windows.Media;
using System.Globalization;


namespace ConceptorUI.Models
{
    internal class ManageEnums
    {
        private static ManageEnums? _obj;
        public static readonly double CellHeight = 23;

        public ManageEnums()
        {
            _obj = this;
        }

        public static ManageEnums Instance => _obj == null! ? new ManageEnums() : _obj;

        public static Brush GetColor(string color)
        {
            return color switch
            {
                "Transparent" => Brushes.Transparent,
                _ => (new BrushConverter().ConvertFrom(color) as SolidColorBrush)!
            };
        }

        public static string GetNumberFieldValue(string value)
        {
            var sd = "";
            var k = 0;
            
            const string characters = ".-0123456789";
            for (var i = 0; i < value.Length; i++)
            {
                for (var j = 0; j < characters.Length; j++)
                {
                    if ((value[i] == '-' && i != 0) || (value[i] == '.' && k > 0) ||
                        (i == 0 && value[i] == '.')) break;
                    if (value[i] != characters[j]) continue;
                    if (value[i] == '.') k++;
                    sd += value[i]; break;
                }
            }
            return sd;
        }

        public static string GetIntegerFieldValue(string value)
        {
            var sd = "";
            const string characters = "-0123456789";
            
            for (var i = 0; i < value.Length; i++)
            {
                for (var j = 0; j < characters.Length; j++)
                {
                    if (value[i] == '-' && i != 0) break;
                    if (value[i] != characters[j]) continue;
                    sd += value[i]; break;
                }
            }
            return sd;
        }

        public static string SetNumber(string value, bool up = true, bool allowNagativeValue = false)
        {
            double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var number);
            if (number.ToString(CultureInfo.InvariantCulture) != value) return value;
            number = up ? number + 1 : (allowNagativeValue ? number - 1 : (number == 0 ? number : number - 1));
            return number.ToString(CultureInfo.InvariantCulture);

        }

        public FontFamily GetFontFamily(string source)
        {
            var ff = new FontFamily();
            var i = 0;
            
            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                if(string.Equals(fontFamily.Source, source, StringComparison.CurrentCultureIgnoreCase)) return fontFamily;
                if(i == 0) ff = fontFamily;
                i++;
            }
            return ff;
        }
        public static int GetFfIndex(string source)
        {
            var i = 0;
            
            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                if (fontFamily.Source == source) return i;
                i++;
            }
            return 0;
        }
    }

    public enum Alignments
    {
        Start, Center, End, Stretch
    }
    public enum PropertyNames
    {
        HorizontalAlignment, VerticalAlignment, Width, Height, None,
        Margin, MarginLeft, MarginTop, MarginRight, MarginBottom,
        Padding, PaddingLeft, PaddingTop, PaddingRight, PaddingBottom,
        BorderWidth, BorderColor, BorderStyle, BorderLeftWidth, BorderLeftColor, BorderLeftStyle,
        BorderTopWidth, BorderTopColor, BorderTopStyle, BorderRightWidth, BorderRightColor, BorderRightStyle,
        BorderBottomWidth, BorderBottomColor, BorderBottomStyle, FillColor,
        BorderRadius, BorderRadiusTopLeft, BorderRadiusTopRight, BorderRadiusBottomLeft, BorderRadiusBottomRight,
        Color, FontSize, FontWeight, FontFamily, FontStyle, TextUnderline, TextOverline, TextThrough,
        AlignLeft, AlignCenter, AlignRight, AlignJustify, ListOrd, ListNOrd, TabLeft, TabRight, TextError, TextIndex, TextExpo,
        SelectedMode, SelectedElement, ElementSize, Add,
        Row, Column, RowSpan, ColumnSpan, MoveLeft, MoveRight, MoveTop, MoveBottom,
        HL, HC, HR, VT, VC, VB, HE, VE, HVE, ROT, X, Y, CMargin, CPadding, CBorder, CBorderRadius,
        MarginBtnActif, PaddingBtnActif, BorderBtnActif, BorderRadBtnActif, Opacity,
        SpaceBetween, SpaceAround, SpaceEvery, Text, AddText, EditText, RemoveText, DisplayText,
        Focus, MoveParentToChild, MoveChildToParent, Trash, Fusion, Merged, TextWrap, LineSpacing, FilePicker,
        CanSelect, HideBorder, ShadowDepth, BlurRadius, ShadowColor, ShadowDirection, Stretch, Copy, Paste
    }
    public enum GroupNames
    {
        Alignment, Transform, Appearance, GridProperty, Text, ParentProperty, SelfAlignment, Global, Shadow
    }
    public enum PropertyTypes
    {
        String, List, Action
    }
    public enum SizeValue
    {
        Define, Auto, Old, Expand
    }
    public enum ColorValue
    {
        Define, Transparent
    }
    public enum ESelectedMode
    {
        Single, Multiple
    }
    public enum ESelectedElement
    {
        Row, Column, Cell
    }
    public enum CanSelectValues
    {
        This, Parent, None
    }
    public enum VisibilityValue
    {
        Visible, Hidden, Collapsed
    }
    public enum ComponentList
    {
        Text, TextSingle, Button, Row, Column, Container, Icon, Table, Stack, 
        Window, Cell, Grid, Page, Image, ListV, ListH, Component
    }
}
