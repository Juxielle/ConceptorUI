using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Models;
using ConceptorUI.Senders;
using ConceptorUI.Services;
using ConceptorUI.Views.Modals;
using UiDesigner.Inputs;
using UiDesigner.Models;
using UiDesigner.Views.Modals;

namespace ConceptorUI.Views.Component;

public partial class ShadowPanel
{
    private GroupProperties _properties;
    private bool _allowSetField;

    public ICommand? MouseDownCommand;
    public ICommand? PickColorCommand { get; }

    public ShadowPanel()
    {
        _allowSetField = false;
        InitializeComponent();

        _properties = new GroupProperties();
        PickColorCommand = new RelayCommand(OnPickColor);
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
                BColor.Color = ManageEnums.GetColor(prop.Value);
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
                    var colorPicker = new ColorPicker(BColor.Color, 1);
                    colorPicker.ColorSelectedCommand = new RelayCommand((color) =>
                    {
                        MouseDownCommand?.Execute(
                            new dynamic[] { GroupNames.Shadow, PropertyNames.ShadowColor, color!.ToString()! }
                        );
                        
                        BColor.Color = (new BrushConverter().ConvertFrom(color!.ToString()!) as SolidColorBrush)!;
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
        BColor.Color = Brushes.Transparent;

        MouseDownCommand?.Execute(
            new dynamic[] { GroupNames.Shadow, PropertyNames.ShadowColor, ColorValue.Transparent.ToString() }
        );
    }

    private void OnSettingClick(object sender, RoutedEventArgs e)
    {
        var componentSetting = PropertiesConfigService.GetConfigs(_properties);
        ComponentPropertyConfig.Instance.Refresh(componentSetting, "SHADOW PROPERTIES", GroupNames.Shadow);
    }

    private void OnPickColor(object sender)
    {
        var propertyName = PropertyNames.None;
        if(sender is not PropertyActionSender propertyActionSender) return;
            
        if (propertyActionSender.Name == "ShadowColor")
            propertyName = PropertyNames.ShadowColor;
            
        if(propertyName == PropertyNames.None) return;
        MouseDownCommand?.Execute(
            new dynamic[]
            {
                GroupNames.Shadow,
                propertyName,
                propertyActionSender.Value
            }
        );
    }
}