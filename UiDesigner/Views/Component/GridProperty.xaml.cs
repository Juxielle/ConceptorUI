using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UiDesigner.Interfaces;
using UiDesigner.Models;


namespace UiDesigner.Views.Component
{
    public partial class GridProperty : IValue
    {
        private static GridProperty? _obj;
        private int _firstCount3;
        private GroupProperties _properties;
        
        public event EventHandler? OnValueChangedEvent;
        private readonly object _valueChangedLock = new();

        public GridProperty()
        {
            _firstCount3 = 0;
            InitializeComponent();
            
            _properties = new GroupProperties();
            _obj = this;
        }

        public static GridProperty Instance => _obj == null! ? new GridProperty() : _obj;
        
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

        public void FeedProps(object value)
        {
            _properties = (value as GroupProperties)!;

            foreach (var prop in _properties.Properties.Where(prop => prop.Visibility == VisibilityValue.Visible.ToString()))
            {
                if (prop.Name == PropertyNames.Row.ToString())
                {

                }
                else if (prop.Name == PropertyNames.Column.ToString())
                {

                }
                else if (prop.Name == PropertyNames.RowSpan.ToString())
                {

                }
                else if (prop.Name == PropertyNames.ColumnSpan.ToString())
                {

                }
                else if (prop.Name == PropertyNames.SelectedElement.ToString())
                {
                    var val = 0;
                    if (prop.Value == ESelectedElement.Row.ToString()) val = 1;
                    else if (prop.Value == ESelectedElement.Column.ToString()) val = 2;
                    CSelectedElement.SelectedIndex = Convert.ToInt32(val);
                }
                else if (prop.Name == PropertyNames.HideBorder.ToString())
                {
                    BHideBorder.BorderBrush = HideBorder.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    HideBorder.Kind = prop.Value == "1" ? PackIconKind.EyeOutline : PackIconKind.EyeOffOutline;
                }
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var propertyName = PropertyNames.None;
            
            switch (tag)
            {
                case "Add":
                    propertyName = PropertyNames.Add;
                    break;
                case "Merge":
                    propertyName = PropertyNames.Merged;
                    break;
                case "HideBorder":
                    var oldValue = _properties.GetValue(PropertyNames.HideBorder);
                    BHideBorder.BorderBrush = HideBorder.Foreground = new BrushConverter().ConvertFrom(oldValue == "1" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    HideBorder.Kind = oldValue == "0" ? PackIconKind.EyeOutline : PackIconKind.EyeOffOutline;
                    propertyName = PropertyNames.HideBorder;
                    break;
            }

            if (propertyName == PropertyNames.None) return;
            OnValueChangedEvent!.Invoke(
                new dynamic[]{GroupNames.GridProperty, propertyName, "0"},
                EventArgs.Empty
            );
        }


        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (sender as ComboBox)!;
            var tag = comboBox.Tag != null ? comboBox.Tag.ToString()! : "";

            if (_firstCount3 > 0)
            {
                switch (tag)
                {
                    case "SelectedElement":
                        var value = comboBox.SelectedIndex == 0 ? ESelectedElement.Cell : 
                            (comboBox.SelectedIndex == 1 ? ESelectedElement.Row : ESelectedElement.Column);
                        
                        OnValueChangedEvent!.Invoke(
                            new dynamic[]{GroupNames.GridProperty, PropertyNames.SelectedElement, value},
                            EventArgs.Empty
                        );
                        break;
                }
            }
            else _firstCount3++;
        }
    }
}
