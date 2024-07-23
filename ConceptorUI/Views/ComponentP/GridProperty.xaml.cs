using ConceptorUI.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;





namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour GridProperty.xaml
    /// </summary>
    public partial class GridProperty : UserControl
    {
        static GridProperty _obj;
        private int firstCount3;

        public GridProperty()
        {
            firstCount3 = 0;
            InitializeComponent();
            _obj = this;
        }

        public static GridProperty Instance => _obj == null! ? new GridProperty() : _obj;

        public void FeedProps()
        {
            int[] pos = Properties.GetPosition(GroupNames.GridProperty, PropertyNames.Row);
            foreach (var prop in Properties.groupProps![pos[0]].Properties)
            {
                if (prop.Visibility == VisibilityValue.Visible.ToString())
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
                        int value = 0;
                        if (prop.Value == ESelectedElement.Row.ToString()) value = 1;
                        else if (prop.Value == ESelectedElement.Column.ToString()) value = 2;
                        CSelectedElement.SelectedIndex = Convert.ToInt32(value);
                    }
                    else if (prop.Name == PropertyNames.HideBorder.ToString())
                    {
                        BHideBorder.BorderBrush = HideBorder.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                        HideBorder.Kind = prop.Value == "1" ? PackIconKind.EyeOutline : PackIconKind.EyeOffOutline;
                    }
                }
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button)!.Tag.ToString()!;

            int[] pos = Properties.GetPosition(GroupNames.GridProperty, PropertyNames.HideBorder);
            int idG = pos[0], idP = pos[1];
            switch (tag)
            {
                case "Add": PanelProperty.ReactToProps(GroupNames.GridProperty, PropertyNames.Add, "1"); break;
                case "Merge": PanelProperty.ReactToProps(GroupNames.GridProperty, PropertyNames.Merged, "1"); break;
                case "HideBorder":
                    string OldValue = Properties.groupProps![idG].Properties[idP].Value;
                    BHideBorder.BorderBrush = HideBorder.Foreground = new BrushConverter().ConvertFrom(OldValue == "1" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    HideBorder.Kind = OldValue == "0" ? PackIconKind.EyeOutline : PackIconKind.EyeOffOutline;
                    PanelProperty.ReactToProps(GroupNames.GridProperty, PropertyNames.HideBorder, OldValue == "0" ? "1" : "0");
                    break;
            }
        }


        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox)!;
            string tag = comboBox!.Tag != null ? comboBox.Tag.ToString()! : "";
            PropertyNames idP = PropertyNames.None;

            if (firstCount3 > 0)
            {
                switch (tag)
                {
                    case "SelectedElement":
                        var value = comboBox.SelectedIndex == 0 ? ESelectedElement.Cell : (comboBox.SelectedIndex == 1 ? ESelectedElement.Row : ESelectedElement.Column);
                        PanelProperty.ReactToProps(GroupNames.GridProperty, PropertyNames.SelectedElement, value.ToString()); break;
                }
            }
            else firstCount3++;
        }
    }
}
