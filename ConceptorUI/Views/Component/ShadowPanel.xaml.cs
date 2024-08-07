using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Interfaces;
using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;

namespace ConceptorUI.Views.Component;

public class ShadowPanel : IShadow
{ 
    private static ShadowPanel? _obj;
    private int index;
    private GroupProperties _properties;
        
    public event EventHandler? OnValueChangedEvent;
    private readonly object _valueChangedLock = new();

    public ShadowPanel()
    {
        InitializeComponent();
        _obj = this;
        index = 0;
        _properties = new GroupProperties();
    }
    
    public static ShadowPanel Instance => _obj == null! ? new ShadowPanel() : _obj;
        
    event EventHandler IShadow.OnValueChanged
    {
        add
        {
            lock (_valueChangedLock)
            {
                OnValueChangedEvent += value;
            }
        }
        remove
        {
            lock (_valueChangedLock)
            {
                OnValueChangedEvent -= value;
            }
        }
    }
    
    public void FeedProps(bool value)
    {
        SColor.Visibility = SDepth.Visibility = SRadius.Visibility = SDirection.Visibility = Visibility.Collapsed;
        _properties = (properties as GroupProperties)!;
        
        #region
        foreach (var prop in _properties.Properties.Where(prop => prop.Visibility == VisibilityValue.Visible.ToString()))
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
    }

    private void OnChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = (sender as TextBox)!;
        var tag = textBox!.Tag != null ? textBox.Tag.ToString()! : "";
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
            OnValueChangedEvent!.Invoke(
                new dynamic[]{GroupNames.Shadow, propertyName, value[^1] == '.' ? value.Substring(0, value.Length - 1) : value}, 
                EventArgs.Empty
            );
        }
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        var tag = (sender as Button)!.Tag.ToString()!;
        switch (tag)
        {
            case "ShadowColor":
                if(CColor.IsChecked == true)
                {
                    MainWindow.Instance.DisplayColorPalette(BColor.Background, !ColorPalette.Instance.IsOpened, tag);
                }
                break;
        }
    }

    private void OnColorChecked(object sender, RoutedEventArgs e)
    {
        var cb = (sender as CheckBox)!;
        if (cb.IsChecked != false) return;
        BColor.Background = Brushes.Transparent;
        
        OnValueChangedEvent!.Invoke(
            new dynamic[]{GroupNames.Shadow, PropertyNames.ShadowColor, "Transparent"}, 
            EventArgs.Empty
        );
    }

    public void SetColor(Brush color, int id)
    {
        BColor.Background = color;
        
        OnValueChangedEvent!.Invoke(
            new dynamic[]{GroupNames.Shadow, PropertyNames.ShadowColor, color.ToString()}, 
            EventArgs.Empty
        );
    }
}