using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using ConceptorUI.ViewModels;
using System.Data.Common;
using ConceptorUI.Views.Components;
using System.Globalization;

namespace ConceptorUI.Models
{
    class ManageEnums
    {
        private static ManageEnums _obj;
        public static double CellHeight = 23;

        public ManageEnums()
        {
            _obj = this;
        }

        public static ManageEnums Instance
        {
            get { return _obj == null ? new ManageEnums() : _obj; }
        }

        public static HorizontalAlignment HAlignment(Alignments value)
        {
            switch (value)
            {
                case Alignments.Start: return HorizontalAlignment.Left;
                case Alignments.Center: return HorizontalAlignment.Center;
                case Alignments.End: return HorizontalAlignment.Right;
                case Alignments.Stretch: return HorizontalAlignment.Stretch;
                default: return HorizontalAlignment.Stretch;
            }
        }

        public static VerticalAlignment VAlignment(Alignments value)
        {
            switch (value)
            {
                case Alignments.Start: return VerticalAlignment.Top;
                case Alignments.Center: return VerticalAlignment.Center;
                case Alignments.End: return VerticalAlignment.Bottom;
                case Alignments.Stretch: return VerticalAlignment.Stretch;
                default: return VerticalAlignment.Stretch;
            }
        }

        public static double GetSize(SizeValue value, double d)
        {
            switch (value)
            {
                case SizeValue.Define: return d;
                case SizeValue.Auto: return double.NaN;
                default: return double.NaN;
            }
        }

        public static Brush GetColor(string color)
        {
            switch (color)
            {
                case "Transparent": return Brushes.Transparent;
                default: return (new BrushConverter().ConvertFrom(color) as SolidColorBrush)!;
            }
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
            string sd = "";
            int k = 0;
            const string characters = ".-0123456789";
            for (int i = 0; i < value.Length; i++)
            {
                for (int j = 0; j < characters.Length; j++)
                {
                    if ((value[i] == '-' && i != 0) || (value[i] == '.' && k > 0) ||
                        (i == 0 && value[i] == '.')) break;
                    if (value[i] == characters[j])
                    {
                        if (value[i] == '.') k++;
                        sd += value[i]; break;
                    }
                }
            }
            return sd;
        }

        public static string GetIntegerFieldValue(string value)
        {
            string sd = "";
            const string characters = "-0123456789";
            for (int i = 0; i < value.Length; i++)
            {
                for (int j = 0; j < characters.Length; j++)
                {
                    if (value[i] == '-' && i != 0) break;
                    if (value[i] == characters[j])
                    {
                        sd += value[i]; break;
                    }
                }
            }
            return sd;
        }

        public static string SetNumber(string value, bool up = true, bool allowNagativeValue = false)
        {
            double number = 0;
            double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out number);
            if (number.ToString(CultureInfo.InvariantCulture) == value)
            {
                number = up ? number + 1 : (allowNagativeValue ? number - 1 : (number == 0 ? number : number - 1));
                return number.ToString(CultureInfo.InvariantCulture);
            } else return value;
        }

        public Component GetComponent(string name)
        {
            if (name == ComponentList.TextSingle.ToString()) return new TextSingleModel(true);
            if (name == ComponentList.Text.ToString()) return new TextSingleModel(true);
            if (name == ComponentList.Image.ToString()) return new ImageModel(true);
            if (name == ComponentList.Column.ToString()) return new ColumnModel(true);
            if (name == ComponentList.Row.ToString()) return new RowModel(true);
            if (name == ComponentList.Container.ToString()) return new ContainerModel(true);
            if (name == ComponentList.Button.ToString()) return new ButtonModel(true);
            if (name == ComponentList.Icon.ToString()) return new IconModel(true);
            if (name == ComponentList.Grid.ToString()) return new GridModel(true);
            if (name == ComponentList.Page.ToString()) return new PageModel(true);
            if (name == ComponentList.Window.ToString()) return new WindowModel(true);
            if (name == ComponentList.Table.ToString()) return new TableModel(true);
            if (name == ComponentList.Stack.ToString()) return new StackModel(true);
            if (name == ComponentList.ListV.ToString()) return new ListVModel(true);
            return name == ComponentList.ListH.ToString() ? new ListHModel(true) : null!;
        }

        public FontFamily GetFontFamily(string source)
        {
            FontFamily ff = new FontFamily();
            int i = 0;
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                if(fontFamily.Source == source) return fontFamily;
                if(i == 0) ff = fontFamily;
                i++;
            }
            return ff;
        }
        public int GetFFIndex(string source)
        {
            int i = 0;
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
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
