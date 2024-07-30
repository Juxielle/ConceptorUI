using System.Linq;
using ConceptorUI.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour TransformProperty.xaml
    /// </summary>
    public partial class TransformProperty
    {
        private static TransformProperty _obj;
        private int index;
        private int _firstCount;

        public TransformProperty()
        {
            _firstCount = 0;
            InitializeComponent();
            _obj = this;
            index = 0;
        }

        public static TransformProperty Instance => _obj == null! ? new TransformProperty() : _obj;

        public void FeedProps()
        {
            W.Visibility = H.Visibility = X.Visibility = SStretch.Visibility = 
                Y.Visibility = R.Visibility = BHE.Visibility = BVE.Visibility = BHVE.Visibility = Visibility.Collapsed;
            var pos = Properties.GetPosition(GroupNames.Transform, PropertyNames.Width);
            #region
            foreach (var prop in Properties.groupProps![pos[0]].Properties)
            {
                if (prop.Visibility == VisibilityValue.Visible.ToString())
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
                        BHE.Visibility = Visibility.Visible; //LoadValue(5, prop.Value);
                    }
                    else if (prop.Name == PropertyNames.VE.ToString())
                    {
                        BVE.Visibility = Visibility.Visible; //LoadValue(6, prop.Value);
                    }
                    else if (prop.Name == PropertyNames.HVE.ToString())
                    {
                        BHVE.Visibility = Visibility.Visible; LoadValue(7, prop.Value);
                    }
                }
            }
            #endregion
        }

        private void OnChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (sender as TextBox)!;
            var tag = textBox!.Tag != null ? textBox.Tag.ToString()! : string.Empty;
            var idP = PropertyNames.None;
            textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
            var value = textBox.Text == "" || textBox.Text == "-" ? SizeValue.Old.ToString() : textBox.Text;
            
            switch (tag)
            {
                case "W": 
                    idP = PropertyNames.Width;
                    if(value != SizeValue.Old.ToString())
                        HE.Foreground = BHE.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    if (Properties.ComponentName == ComponentList.Icon && WTB.Text != HTB.Text)
                    {
                        var vd = value[value.Length - 1] == '.' ? value.Substring(0, value.Length - 1) : value;
                        PanelProperty.ReactToProps(GroupNames.Transform, PropertyNames.Height, vd);
                        HTB.Text = vd;
                    }
                    break;
                case "H":
                    idP = PropertyNames.Height;
                    if (value != SizeValue.Old.ToString())
                        VE.Foreground = BVE.BorderBrush = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    if (Properties.ComponentName == ComponentList.Icon && WTB.Text != HTB.Text)
                    {
                        var vd = value[value.Length - 1] == '.' ? value.Substring(0, value.Length - 1) : value;
                        PanelProperty.ReactToProps(GroupNames.Transform, PropertyNames.Width, vd);
                        WTB.Text = vd;
                    }
                    break;
                case "X": idP = PropertyNames.X; break;
                case "Y": idP = PropertyNames.Y; break;
                case "R": idP = PropertyNames.ROT; break;
            }
            if (idP != PropertyNames.None)
            {
                PanelProperty.ReactToProps(GroupNames.Transform, idP, value[value.Length - 1] == '.' ? value.Substring(0, value.Length - 1) : value);
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var idP = PropertyNames.None;
            var value = string.Empty;
            switch (tag)
            {
                case "HE":
                    WTB.Text = "";
                    value = Properties.groupProps![1].Properties[5].Value;
                    PanelProperty.ReactToProps(GroupNames.Transform, PropertyNames.Width, value == "0" ? "Expand" : "Auto");
                    PanelProperty.ReactToProps(GroupNames.Transform, PropertyNames.HE, value == "0" ? "1" : "0");
                    HE.Foreground = BHE.BorderBrush = new BrushConverter().ConvertFrom(value == "0" ? "#6739b7" : "#8c8c8a") as SolidColorBrush; break;
                case "VE":
                    HTB.Text = "";
                    value = Properties.groupProps![1].Properties[6].Value;
                    PanelProperty.ReactToProps(GroupNames.Transform, PropertyNames.VE, value == "0" ? "1" : "0");
                    PanelProperty.ReactToProps(GroupNames.Transform, PropertyNames.Height, value == "0" ? "Expand" : "Auto");
                    VE.Foreground = BVE.BorderBrush = new BrushConverter().ConvertFrom(value == "0" ? "#6739b7" : "#8c8c8a") as SolidColorBrush; break;
                case "HVE": idP = PropertyNames.HVE; break;
                case "UpValueW": WTB.Text = ManageEnums.SetNumber(WTB.Text).Replace(",", "."); break;
                case "DownValueW": WTB.Text = ManageEnums.SetNumber(WTB.Text, false).Replace(",", "."); break;
                case "UpValueH": HTB.Text = ManageEnums.SetNumber(HTB.Text).Replace(",", "."); break;
                case "DownValueH": HTB.Text = ManageEnums.SetNumber(HTB.Text, false).Replace(",", "."); break;
            }
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (sender as ComboBox)!;
            var tag = comboBox.Tag != null ? comboBox.Tag.ToString()!: "";
            var idP = PropertyNames.None;
            string value = null!;
            switch (tag)
            {
                case "Stretch": 
                    idP = PropertyNames.Stretch;
                    value = (comboBox.SelectedValue as ComboBoxItem) != null ? (comboBox.SelectedValue as ComboBoxItem)!.Content.ToString()! : null!;
                    break;
            }
            
            if (idP == PropertyNames.None && value != null!) return;
            if(_firstCount > 1 && value != null)
                PanelProperty.ReactToProps(GroupNames.Transform, idP, value!);
            if (_firstCount < 2)  _firstCount++;
        }

        private void LoadValue(int idP, string value)
        {
            switch (index)
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
            index = value == "0" ? -1 : idP;
        }
    }
}
