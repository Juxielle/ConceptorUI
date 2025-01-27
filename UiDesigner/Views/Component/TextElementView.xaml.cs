using System.Windows;
using System.Windows.Controls;


namespace UiDesigner.Views.Component
{
    public partial class TextElementView : UserControl
    {
        public TextElementView()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            switch (tag)
            {
                case "Edit": break;
                case "Remove": break;
            }
        }
    }
}
