using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Models;

namespace ConceptorUI.Views.ComponentP;

public partial class ShadowPanel
{ 
    static ShadowPanel _obj;
    private int index;

    public ShadowPanel()
    {
        InitializeComponent();
        _obj = this;
        index = 0;
    }
    
    public static ShadowPanel Instance => _obj == null! ? new ShadowPanel() : _obj;
    
    public void FeedProps()
    {
        SColor.Visibility = SDepth.Visibility = SRadius.Visibility = SDirection.Visibility = Visibility.Collapsed;
        var pos = Properties.GetPosition(GroupNames.Shadow, PropertyNames.ShadowDepth);
        #region
        foreach (var prop in Properties.groupProps![pos[0]].Properties)
        {
            if (prop.Visibility != VisibilityValue.Visible.ToString()) continue;
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
        var idP = PropertyNames.None;
        textBox.Text = ManageEnums.GetNumberFieldValue(textBox.Text);
        var value = textBox.Text is "" or "-" ? SizeValue.Old.ToString() : textBox.Text;

        switch (tag)
        {
            case "Depth": 
                idP = PropertyNames.ShadowDepth;
                break;
            case "Radius":
                idP = PropertyNames.BlurRadius;
                break;
            case "Direction":
                idP = PropertyNames.ShadowDirection;
                break;
        }
        if (idP != PropertyNames.None)
        {
            PanelProperty.Instance.ReactToProps(GroupNames.Shadow, idP, value[^1] == '.' ? value.Substring(0, value.Length - 1) : value);
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
        PanelProperty.Instance.ReactToProps(GroupNames.Shadow, PropertyNames.ShadowColor, "Transparent");
    }

    public void SetColor(Brush color, int id)
    {
        BColor.Background = color;
        PanelProperty.Instance.ReactToProps(GroupNames.Shadow, PropertyNames.ShadowColor, color.ToString());
    }
}