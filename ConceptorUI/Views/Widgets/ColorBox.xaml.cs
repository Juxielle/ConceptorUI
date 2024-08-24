
using System;
using System.Windows;
using System.Windows.Media;
using ConceptorUI.Interfaces;
using ConceptorUI.Models;
using ConceptorUI.Views.Modals;

namespace ConceptorUI.Views.Widgets;

public partial class ColorBox : IValue
{
    public event EventHandler? OnValueChangedEvent;
    private readonly object _valueChangedLock = new();

    public ColorBox()
    {
        InitializeComponent();
    }
        
    event EventHandler IValue.OnValueChanged
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

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        var propertyName = PropertyNames.None;
        var value = string.Empty;
        
        var colorPicker = new ColorPicker(BFillColor.Background, 1);
        colorPicker.PreOpacityChangedEvent += (opacity, _) =>
        {
            OnValueChangedEvent?.Invoke(
                new dynamic[]{GroupNames.Appearance, PropertyNames.Opacity, opacity!.ToString()!},
                EventArgs.Empty
            );
        };
                    
        colorPicker.PreColorSelectedEvent += (color, _) =>
        {
            OnValueChangedEvent?.Invoke(
                new dynamic[]{GroupNames.Appearance, PropertyNames.FillColor, color!.ToString()!},
                EventArgs.Empty
            );
                        
            BFillColor.Background = new BrushConverter().ConvertFrom(color!.ToString()!) as SolidColorBrush;
        };
        colorPicker.Show();
    }
}