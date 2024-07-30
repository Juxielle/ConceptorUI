using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour NativeComponentPanel.xaml
    /// </summary>
    public partial class NativeComponentPanel : UserControl
    {
        public NativeComponentPanel()
        {
            InitializeComponent();

            OnInit();
        }

        private void OnInit()
        {
            Container.Refresh(text: "Container", isIcon: true, iconKind: PackIconKind.Square);
            Stack.Refresh(text: "Relative Layout", isIcon: true, iconKind: PackIconKind.Layers);
            Row.Refresh(text: "Row", isIcon: true, iconKind: PackIconKind.LandRowsHorizontal);
            Column.Refresh(text: "Column", isIcon: true, iconKind: PackIconKind.LandRowsVertical);
            ListView.Refresh(text: "List View", isIcon: true, iconKind: PackIconKind.ReorderHorizontal);
            GridView.Refresh(text: "Grid View", isIcon: true, iconKind: PackIconKind.ViewGrid);

            text.Refresh(text: "Text", isIcon: false, image: "text.png");
            image.Refresh(text: "Image", isIcon: true, iconKind: PackIconKind.Image);
            icon.Refresh(text: "Icon", isIcon: true, iconKind: PackIconKind.SimpleIcons);
            button.Refresh(text: "Button", isIcon: false, image: "button.png");

            textInput.Refresh(text: "Text Input", isIcon: false, image: "input.png");
            comboBox.Refresh(text: "Combo Box", isIcon: true, iconKind: PackIconKind.Card);
            radioButton.Refresh(text: "Radio Button", isIcon: true, iconKind: PackIconKind.RadioButtonChecked);
            checkBox.Refresh(text: "Check Box", isIcon: true, iconKind: PackIconKind.CheckBox);

            appbar.Refresh(text: "App bar", isIcon: false, image: "appbar.png");
            drawer.Refresh(text: "Drawer", isIcon: false, image: "drawer.png");
        }
    }
}
