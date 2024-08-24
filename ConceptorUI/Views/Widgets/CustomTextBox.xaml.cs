
using System;
using System.Windows;
using System.Windows.Controls;
using ConceptorUI.Interfaces;
using ConceptorUI.Models;

namespace ConceptorUI.Views.Widgets;

public partial class CustomTextBox : IValue
{
    
    public event EventHandler? OnValueChangedEvent;
    private readonly object _valueChangedLock = new();
    
    public CustomTextBox()
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
        var tag = (sender as Button)!.Tag.ToString()!;
        const PropertyNames propertyName = PropertyNames.None;
        var value = string.Empty;
            
        switch (tag)
        {
            case "UpValue":
                TextBox.Text = ManageEnums.SetNumber(TextBox.Text).Replace(",", ".");
                break;
            case "DownValue":
                TextBox.Text = ManageEnums.SetNumber(TextBox.Text, false).Replace(",", ".");
                break;
        }
        
        OnValueChangedEvent?.Invoke(
            new dynamic[]{GroupNames.Transform, propertyName, value},
            EventArgs.Empty
        );
    }

    private void OnChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = (sender as TextBox)!;
            
        textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
        var value = textBox.Text is "" or "-" ? SizeValue.Old.ToString() : textBox.Text;
        var propertyName = PropertyNames.None;
        
        OnValueChangedEvent?.Invoke(
            new dynamic[]{GroupNames.Transform, propertyName, value[^1] == '.' ? value[..^1] : value},
            EventArgs.Empty
        );
    }
}