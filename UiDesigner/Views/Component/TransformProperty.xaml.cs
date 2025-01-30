using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Services;
using ConceptorUI.Views.Modals;
using UiDesigner.Models;

namespace ConceptorUI.Views.Component
{
    public partial class TransformProperty
    {
        private GroupProperties _properties;
        private int _index;
        private int _firstCount;
        private bool _allowSetField;

        public ICommand? MouseDownCommand;

        public TransformProperty()
        {
            _allowSetField = false;
            _firstCount = 0;
            InitializeComponent();

            _properties = new GroupProperties();
            _index = 0;
        }

        public void FeedProps(object properties)
        {
            W.Visibility = H.Visibility = Gap.Visibility = SStretch.Visibility =
                Y.Visibility = R.Visibility = BHE.Visibility = BVE.Visibility = BHVE.Visibility = Visibility.Collapsed;
            _properties = (properties as GroupProperties)!;
            _allowSetField = false;

            #region

            foreach (var prop in _properties.Properties.Where(prop =>
                         prop.Visibility == VisibilityValue.Visible.ToString()))
            {
                if (prop.Name == PropertyNames.Width.ToString())
                {
                    W.Visibility = Visibility.Visible;

                    if (prop.Value == SizeValue.Expand.ToString() || prop.Value == SizeValue.Auto.ToString()) continue;

                    WTB.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.Height.ToString())
                {
                    H.Visibility = Visibility.Visible;

                    if (prop.Value == SizeValue.Expand.ToString() || prop.Value == SizeValue.Auto.ToString()) continue;

                    HTB.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.Stretch.ToString())
                {
                    SStretch.Visibility = Visibility.Visible;
                    var cbStretchItem = CbStretch.Items.OfType<ComboBoxItem>()
                        .FirstOrDefault(x => x.Content.ToString() == prop.Value);
                    CbStretch.SelectedIndex = CbStretch.Items.IndexOf(cbStretchItem!);
                }
                else if (prop.Name == PropertyNames.Gap.ToString())
                {
                    Gap.Visibility = Visibility.Visible;
                    TbGap.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.Y.ToString())
                {
                    Y.Visibility = Visibility.Visible;
                    YTB.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.Rot.ToString())
                {
                    R.Visibility = Visibility.Visible;
                    RTB.Text = prop.Value.Replace(",", ".");
                }
                else if (prop.Name == PropertyNames.He.ToString())
                {
                    BHE.Visibility = Visibility.Visible;

                    var w = _properties.GetValue(PropertyNames.Width);
                    HE.Foreground = BHE.BorderBrush =
                        new BrushConverter().ConvertFrom(w == SizeValue.Expand.ToString()
                            ? "#6739b7"
                            : "#8c8c8a") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.Ve.ToString())
                {
                    BVE.Visibility = Visibility.Visible;

                    var h = _properties.GetValue(PropertyNames.Height);
                    VE.Foreground = BVE.BorderBrush =
                        new BrushConverter().ConvertFrom(h == SizeValue.Expand.ToString()
                            ? "#6739b7"
                            : "#8c8c8a") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.Hve.ToString())
                {
                    BHVE.Visibility = Visibility.Visible;

                    var w = _properties.GetValue(PropertyNames.Width);
                    var h = _properties.GetValue(PropertyNames.Height);
                    HVE.Foreground = BHVE.BorderBrush =
                        new BrushConverter().ConvertFrom(w == SizeValue.Expand.ToString() && h == SizeValue.Expand.ToString()
                            ? "#6739b7"
                            : "#8c8c8a") as SolidColorBrush;
                }
            }

            #endregion

            _allowSetField = true;
        }

        private void OnChanged(object sender, TextChangedEventArgs e)
        {
            if (!_allowSetField) return;

            var textBox = (sender as TextBox)!;
            var tag = textBox.Tag != null ? textBox.Tag.ToString()! : string.Empty;

            textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
            var value = textBox.Text is "" or "-" ? SizeValue.Old.ToString() : textBox.Text;
            var propertyName = PropertyNames.None;

            switch (tag)
            {
                case "W":
                    if (value != SizeValue.Old.ToString())
                        HE.Foreground = BHE.BorderBrush =
                            new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    propertyName = PropertyNames.Width;
                    break;
                case "H":
                    if (value != SizeValue.Old.ToString())
                        VE.Foreground = BVE.BorderBrush =
                            new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    propertyName = PropertyNames.Height;
                    break;
                case "Gap":
                    propertyName = PropertyNames.Gap;
                    break;
                case "Y":
                    propertyName = PropertyNames.Y;
                    break;
                case "R":
                    propertyName = PropertyNames.Rot;
                    break;
            }

            if (propertyName == PropertyNames.None) return;
            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Transform, propertyName, value[^1] == '.' ? value[..^1] : value }
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
                    propertyName = PropertyNames.He;
                    value = _properties.GetValue(PropertyNames.He);
                    HE.Foreground = BHE.BorderBrush =
                        new BrushConverter().ConvertFrom(value == "0" ? "#6739b7" : "#8c8c8a") as SolidColorBrush;
                    value = value == "0" ? "1" : "0";
                    break;
                case "VE":
                    HTB.Text = "";
                    propertyName = PropertyNames.Ve;
                    value = _properties.GetValue(PropertyNames.Ve);
                    VE.Foreground = BVE.BorderBrush =
                        new BrushConverter().ConvertFrom(value == "0" ? "#6739b7" : "#8c8c8a") as SolidColorBrush;
                    value = value == "0" ? "1" : "0";
                    break;
                case "HVE":
                    HTB.Text = "";
                    WTB.Text = "";
                    propertyName = PropertyNames.Hve;
                    value = _properties.GetValue(PropertyNames.Hve);
                    HVE.Foreground = BHVE.BorderBrush =
                        new BrushConverter().ConvertFrom(value == "0" ? "#6739b7" : "#8c8c8a") as SolidColorBrush;
                    value = value == "0" ? "1" : "0";
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
            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Transform, propertyName, value }
            );
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_allowSetField) return;

            var comboBox = (sender as ComboBox)!;
            var tag = comboBox.Tag != null ? comboBox.Tag.ToString()! : "";
            var propertyName = PropertyNames.None;
            string value = null!;

            switch (tag)
            {
                case "Stretch":
                    propertyName = PropertyNames.Stretch;
                    value = (comboBox.SelectedValue as ComboBoxItem) != null
                        ? (comboBox.SelectedValue as ComboBoxItem)!.Content.ToString()!
                        : null!;
                    break;
            }

            if (propertyName == PropertyNames.None && value != null!) return;
            if (_firstCount > 1 && value != null)
                MouseDownCommand?.Execute(
                    new dynamic[] { GroupNames.Transform, propertyName, value }
                );
            if (_firstCount < 2) _firstCount++;
        }

        private void OnSettingClick(object sender, RoutedEventArgs e)
        {
            var componentSetting = PropertiesConfigService.GetConfigs(_properties);
            ComponentPropertyConfig.Instance.Refresh(componentSetting, "TRANSFORM PROPERTIES", GroupNames.Transform);
        }
    }
}