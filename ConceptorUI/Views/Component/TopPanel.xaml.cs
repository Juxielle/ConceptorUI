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
    /// Logique d'interaction pour TopPanel.xaml
    /// </summary>
    public partial class TopPanel : UserControl
    {
        private bool clicado = false;
        private Point lm = new Point();

        public TopPanel()
        {
            InitializeComponent();
        }

        private void BtnScreenClick(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            switch (tag)
            {
                case "Minimize": MainWindow.Instance.WindowState = WindowState.Minimized; break;
                case "Maximize":
                    MainWindow.Instance.WindowState = MainWindow.Instance.WindowState == WindowState.Normal ?
                                                      WindowState.Maximized : WindowState.Normal;
                    MaxIcon.Kind = MainWindow.Instance.WindowState == WindowState.Normal ?
                                   PackIconKind.Maximize : PackIconKind.WindowRestore; break;
                case "Close": MainWindow.Instance.Close(); break;
            }
        }

        void PnMouseDown(object sender, MouseEventArgs e)
        {
            if (clicado)
            {
                var MousePosition = e.GetPosition(this);
                double dx = (MousePosition.X - this.lm.X);
                double dy = (MousePosition.Y - this.lm.Y);
                this.lm = MousePosition;
                //MainWindow.Instance.Left += dx;
                //MainWindow.Instance.Top += dy;
            }
        }

        void PnMouseUp(object sender, MouseEventArgs e)
        {
            clicado = false;
        }

        void PnMouseMove(object sender, MouseEventArgs e)
        {
            clicado = true;
            this.lm = e.GetPosition(this);
        }
    }
}
