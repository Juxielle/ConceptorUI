
using System;
using System.Windows.Controls;
using UiDesigner.Interfaces;
using UiDesigner.Models;

namespace UiDesigner.Views.Widgets;

public partial class CustomComboBox : IValue
{
    private bool _canExecute;
    
    public event EventHandler? OnValueChangedEvent;
    private readonly object _valueChangedLock = new();

    public CustomComboBox()
    {
        _canExecute = false;
        
        InitializeComponent();
        _canExecute = true;
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

    private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
    {
        if(!_canExecute) return;
        
        var comboBox = (sender as ComboBox)!;
        var tag = comboBox.Tag != null ? comboBox.Tag.ToString()!: "";
        var propertyName = PropertyNames.None;
        string value = null!;
            
        switch (tag)
        {
            case "Stretch": 
                propertyName = PropertyNames.Stretch;
                value = (comboBox.SelectedValue as ComboBoxItem) != null ? (comboBox.SelectedValue as ComboBoxItem)!.Content.ToString()! : null!;
                break;
        }
        
        if (propertyName == PropertyNames.None && value != null!) return;
        
        OnValueChangedEvent?.Invoke(
            new dynamic[]{GroupNames.Transform, propertyName, value!},
            EventArgs.Empty
        );
    }
}