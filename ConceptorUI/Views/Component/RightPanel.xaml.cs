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



namespace ConceptorUI.Views.Component
{
    /// <summary>
    /// Logique d'interaction pour RightPanel.xaml
    /// </summary>
    public partial class RightPanel : UserControl
    {
        public RightPanel()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, MouseButtonEventArgs e)
        {
            string tag = (sender as Border)!.Tag.ToString()!;
            switch (tag)
            {
                case "Props": MainWindow.Instance.OpenRightPanel(); break;
                case "StructView":
                    MainWindow.Instance.OpenRightPanel(isStructuralView: true);
                    break;
            }
        }

        private void BtnMouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void BtnMouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
