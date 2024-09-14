using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Inputs;
using ConceptorUI.Models;
using ConceptorUI.Views.Modals;

namespace ConceptorUI.Views.Component;

public partial class ShadowPanel
{
    private GroupProperties _properties;
    private bool _allowSetField;

    public ICommand? MouseDownCommand;

    public ShadowPanel()
    {
        _allowSetField = false;
        InitializeComponent();

        _properties = new GroupProperties();
    }

    public void FeedProps(object value)
    {
        SColor.Visibility = SDepth.Visibility = SRadius.Visibility = SDirection.Visibility = Visibility.Collapsed;
        _properties = (value as GroupProperties)!;
        _allowSetField = false;

        #region

        foreach (var prop in
                 _properties.Properties.Where(prop => prop.Visibility == VisibilityValue.Visible.ToString()))
        {
            if (prop.Name == PropertyNames.ShadowDepth.ToString())
            {
                SDepth.Visibility = Visibility.Visible;
                TbDepth.Text = prop.Value.Replace(",", ".");
            }
            else if (prop.Name == PropertyNames.ShadowColor.ToString())
            {
                SColor.Visibility = Visibility.Visible;
                CColor.IsChecked = prop.Value != ColorValue.Transparent.ToString();
                BColor.Background = ManageEnums.GetColor(prop.Value);
            }
            else if (prop.Name == PropertyNames.ShadowDirection.ToString())
            {
                SDirection.Visibility = Visibility.Visible;
                TbDirection.Text = prop.Value.Replace(",", ".");
            }
            else if (prop.Name == PropertyNames.BlurRadius.ToString())
            {
                SRadius.Visibility = Visibility.Visible;
                TbRadius.Text = prop.Value.Replace(",", ".");
            }
        }

        #endregion

        _allowSetField = true;
    }

    private void OnChanged(object sender, TextChangedEventArgs e)
    {
        if (!_allowSetField) return;

        var textBox = (sender as TextBox)!;
        var tag = textBox.Tag != null ? textBox.Tag.ToString()! : "";
        var propertyName = PropertyNames.None;
        textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
        var value = textBox.Text is "" or "-" ? SizeValue.Old.ToString() : textBox.Text;

        switch (tag)
        {
            case "Depth":
                propertyName = PropertyNames.ShadowDepth;
                break;
            case "Radius":
                propertyName = PropertyNames.BlurRadius;
                break;
            case "Direction":
                propertyName = PropertyNames.ShadowDirection;
                break;
        }

        if (propertyName != PropertyNames.None)
        {
            MouseDownCommand?.Execute(
                new dynamic[]
                {
                    GroupNames.Shadow, propertyName, value[^1] == '.' ? value.Substring(0, value.Length - 1) : value
                }
            );
        }
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        var tag = (sender as Button)!.Tag.ToString()!;
        switch (tag)
        {
            case "ShadowColor":
                if (CColor.IsChecked == true)
                {
                    var colorPicker = new ColorPicker(BColor.Background, 1);
                    colorPicker.ColorSelectedCommand = new RelayCommand((color) =>
                    {
                        MouseDownCommand?.Execute(
                            new dynamic[] { GroupNames.Shadow, PropertyNames.ShadowColor, color!.ToString()! }
                        );

                        BColor.Background = new BrushConverter().ConvertFrom(color!.ToString()!) as SolidColorBrush;
                    });
                    colorPicker.Show();
                }

                break;
        }
    }

    private void OnColorChecked(object sender, RoutedEventArgs e)
    {
        if (!_allowSetField) return;

        var cb = (sender as CheckBox)!;
        if (cb.IsChecked != false) return;
        BColor.Background = Brushes.Transparent;

        MouseDownCommand?.Execute(
            new dynamic[] { GroupNames.Shadow, PropertyNames.ShadowColor, ColorValue.Transparent.ToString() }
        );
    }
}