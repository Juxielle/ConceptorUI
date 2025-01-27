using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using UiDesigner.Interfaces;

namespace UiDesigner.Views.Widgets;

public partial class IconButton : IValue
{
    public event EventHandler? OnValueChangedEvent;
    private readonly object _valueChangedLock = new();

    public IconButton()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(string), typeof(IconButton),
            new PropertyMetadata("0"));

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(nameof(Kind), typeof(PackIconKind), typeof(IconButton),
            new PropertyMetadata(PackIconKind.Apple));

    public PackIconKind Kind
    {
        get => (PackIconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    public static readonly DependencyProperty BorderWidthProperty =
        DependencyProperty.Register(nameof(BorderWidth), typeof(Thickness), typeof(IconButton),
            new PropertyMetadata(new Thickness(0)));

    public Thickness BorderWidth
    {
        get => (Thickness)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
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
        OnValueChangedEvent?.Invoke(
            Value,
            EventArgs.Empty
        );
    }
}