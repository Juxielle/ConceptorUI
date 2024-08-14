using System;
using System.Linq;
using ConceptorUI.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Interfaces;


namespace ConceptorUI.Views.Component
{
    public partial class TransformProperty : IValue
    {
        private static TransformProperty? _obj;
        private GroupProperties _properties;
        private int _index;
        private int _firstCount;
        
        public event EventHandler? OnValueChangedEvent;
        private readonly object _valueChangedLock = new();

        public TransformProperty()
        {
            _firstCount = 0;
            InitializeComponent();
            
            _obj = this;
            _properties = new GroupProperties();
            _index = 0;
        }

        public static TransformProperty Instance => _obj == null! ? new TransformProperty() : _obj;
        
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

        public void FeedProps(object properties)
        {
            W.Visibility = H.Visibility = X.Visibility = SStretch.Visibility = 
                Y.Visibility = R.Visibility = BHE.Visibility = BVE.Visibility = BHVE.Visibility = Visibility.Collapsed;
            _properties = (properties as GroupProperties)!;
            
            #region
            foreach (var prop in _properties.Properties.Where(prop => prop.Visibility == VisibilityValue.Visible.ToString()))
            {
                if (prop.Name == PropertyNames.Width.ToString())
                {
                    W.Visibility = Visibility.Visible; WTB.Text = prop.Value.Replace(",", ".");
                    HE.Foreground = BHE.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == SizeValue.Expand.ToString() ? "#6739b7" : "#8c8c8a") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.Height.ToString())
                {
                    H.Visibility = Visibility.Visible; HTB.Text = prop.Value.Replace(",", ".");
                    VE.Foreground = BVE.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == SizeValue.Expand.ToString() ? "#6739b7" : "#8c8c8a") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.Stretch.ToString())
                {
                    SStretch.Visibility = Visibility.Visible;
                    var cbStretchItem = CbStretch.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == prop.Value);
                    CbStretch.SelectedIndex = CbStretch.Items.IndexOf(cbStretchItem!);
                }
                else if (prop.Name == PropertyNames.X.ToString())
                {
                    X.Visibility = Visibility.Visible; XTB.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.Y.ToString())
                {
                    Y.Visibility = Visibility.Visible; YTB.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.ROT.ToString())
                {
                    R.Visibility = Visibility.Visible; RTB.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.HE.ToString())
                {
                    BHE.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.VE.ToString())
                {
                    BVE.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.HVE.ToString())
                {
                    BHVE.Visibility = Visibility.Visible;
                    LoadValue(7, prop.Value);
                }
            }
            #endregion
        }

        private void OnChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (sender as TextBox)!;
            var tag = textBox.Tag != null ? textBox.Tag.ToString()! : string.Empty;
            
            textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
            var value = textBox.Text is "" or "-" ? SizeValue.Old.ToString() : textBox.Text;
            var propertyName = PropertyNames.None;
            
            switch (tag)
            {
                case "W":
                    if(value != SizeValue.Old.ToString())
                        HE.Foreground = BHE.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    propertyName = PropertyNames.Width;
                    break;
                case "H":
                    if (value != SizeValue.Old.ToString())
                        VE.Foreground = BVE.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    propertyName = PropertyNames.Height;
                    break;
                case "X":
                    propertyName = PropertyNames.X;
                    break;
                case "Y":
                    propertyName = PropertyNames.Y;
                    break;
                case "R":
                    propertyName = PropertyNames.ROT;
                    break;
            }
            
            if (propertyName == PropertyNames.None) return;
            OnValueChangedEvent?.Invoke(
                new dynamic[]{GroupNames.Transform, propertyName, value[^1] == '.' ? value[..^1] : value},
                EventArgs.Empty
            );
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var propertyName = PropertyNames.None;
            var value = string.Empty;
            
            switch (tag)
            {
                case "HE":
                    WTB.Text = "";
                    propertyName = PropertyNames.HE;
                    value = _properties.GetValue(PropertyNames.Width);
                    HE.Foreground = BHE.BorderBrush = new BrushConverter().ConvertFrom(value == "0" ? "#6739b7" : "#8c8c8a") as SolidColorBrush;
                    break;
                case "VE":
                    HTB.Text = "";
                    propertyName = PropertyNames.VE;
                    value = _properties.GetValue(PropertyNames.Width);
                    VE.Foreground = BVE.BorderBrush = new BrushConverter().ConvertFrom(value == "0" ? "#6739b7" : "#8c8c8a") as SolidColorBrush;
                    break;
                case "HVE":
                    break;
                case "UpValueW":
                    WTB.Text = ManageEnums.SetNumber(WTB.Text).Replace(",", ".");
                    break;
                case "DownValueW":
                    WTB.Text = ManageEnums.SetNumber(WTB.Text, false).Replace(",", ".");
                    break;
                case "UpValueH":
                    HTB.Text = ManageEnums.SetNumber(HTB.Text).Replace(",", ".");
                    break;
                case "DownValueH":
                    HTB.Text = ManageEnums.SetNumber(HTB.Text, false).Replace(",", ".");
                    break;
            }
            
            if (propertyName == PropertyNames.None) return;
            OnValueChangedEvent?.Invoke(
                new dynamic[]{GroupNames.Transform, propertyName, value},
                EventArgs.Empty
            );
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
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
            if(_firstCount > 1 && value != null)
                OnValueChangedEvent?.Invoke(
                    new dynamic[]{GroupNames.Transform, propertyName, value},
                    EventArgs.Empty
                );
            if (_firstCount < 2) _firstCount++;
        }

        private void LoadValue(int idP, string value)
        {
            switch (_index)
            {
                case 5:
                    HE.Foreground = BHE.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case 6:
                    VE.Foreground = BVE.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case 7:
                    HVE.Foreground = BHVE.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
            }
            var color = value == "0" ? "#8c8c8a" : "#6739b7";
            switch (idP)
            {
                case 5:
                    HE.Foreground = BHE.BorderBrush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
                    break;
                case 6:
                    VE.Foreground = BVE.BorderBrush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
                    break;
                case 7:
                    HVE.Foreground = BHVE.BorderBrush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;
                    break;
            }
            _index = value == "0" ? -1 : idP;
        }
    }
}
