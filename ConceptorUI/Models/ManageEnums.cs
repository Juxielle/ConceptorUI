using System.Windows.Media;
using System.Windows;
using ConceptorUI.ViewModels;
using System.Globalization;


namespace ConceptorUI.Models
{
    class ManageEnums
    {
        private static ManageEnums? _obj;
        public static readonly double CellHeight = 23;

        public ManageEnums()
        {
            _obj = this;
        }

        public static ManageEnums Instance => _obj == null! ? new ManageEnums() : _obj;

        public static HorizontalAlignment HAlignment(Alignments value)
        {
            return value switch
            {
                Alignments.Start => HorizontalAlignment.Left,
                Alignments.Center => HorizontalAlignment.Center,
                Alignments.End => HorizontalAlignment.Right,
                Alignments.Stretch => HorizontalAlignment.Stretch,
                _ => HorizontalAlignment.Stretch
            };
        }

        public static VerticalAlignment VAlignment(Alignments value)
        {
            return value switch
            {
                Alignments.Start => VerticalAlignment.Top,
                Alignments.Center => VerticalAlignment.Center,
                Alignments.End => VerticalAlignment.Bottom,
                Alignments.Stretch => VerticalAlignment.Stretch,
                _ => VerticalAlignment.Stretch
            };
        }

        public static double GetSize(SizeValue value, double d)
        {
            return value switch
            {
                SizeValue.Define => d,
                SizeValue.Auto => double.NaN,
                _ => double.NaN
            };
        }

        public static Brush GetColor(string color)
        {
            return color switch
            {
                "Transparent" => Brushes.Transparent,
                _ => (new BrushConverter().ConvertFrom(color) as SolidColorBrush)!
            };
        }

        public static bool IsSelectedModeValue(string value)
        {
            return value == ESelectedMode.Single.ToString() || value == ESelectedMode.Multiple.ToString();
        }
        public static bool IsSelectedElementValue(string value)
        {
            return value == ESelectedElement.Cell.ToString() || value == ESelectedElement.Row.ToString()
                   || value == ESelectedElement.Column.ToString();
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

        public Component GetComponent(string name)
        {
            if (name == ComponentList.TextSingle.ToString()) return new TextSingleModel();
            if (name == ComponentList.Text.ToString()) return new TextSingleModel();
            if (name == ComponentList.Image.ToString()) return new ImageModel();
            if (name == ComponentList.Column.ToString()) return new ColumnModel();
            if (name == ComponentList.Row.ToString()) return new RowModel();
            if (name == ComponentList.Container.ToString()) return new ContainerModel();
            if (name == ComponentList.Icon.ToString()) return new IconModel();
            if (name == ComponentList.Grid.ToString()) return new GridModel();
            if (name == ComponentList.Window.ToString()) return new WindowModel();
            if (name == ComponentList.Stack.ToString()) return new StackModel();
            if (name == ComponentList.ListV.ToString()) return new ListVModel();
            return name == ComponentList.ListH.ToString() ? new ListHModel() : null!;
        }

        public FontFamily GetFontFamily(string source)
        {
            var ff = new FontFamily();
            var i = 0;
            
            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                if(fontFamily.Source == source) return fontFamily;
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
        CanSelect, HideBorder, ShadowDepth, BlurRadius, ShadowColor, ShadowDirection, Stretch,
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
    public enum GridActions
    {
        Add, Delete, MoveToStart, MoveToEnd
    }
    public enum VisibilityValue
    {
        Visible, Hidden, Collapsed
    }
    public enum ActionValue
    {
        Clicked, NotClicked
    }
    public enum ComponentList
    {
        Text, TextSingle, Button, Row, Column, Container, Icon, Table, Stack, 
        Window, Cell, Grid, Page, Image, ListV, ListH, Component
    }

    public enum EntityType
    {
        Varchar, Number, Integer, Text, Boolean, Enumeration, Reference, Image, Date, Time
    }

    public enum Plateforms
    {
        ReactNative, Flutter, Compose, AndroidXMl, WPF, UWP, Swing, Tinker, Scheme
    }
}
